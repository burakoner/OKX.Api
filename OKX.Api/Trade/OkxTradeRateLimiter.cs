namespace OKX.Api.Trade;

/// <summary>
/// Optional, fail-fast Place/Amend guard shared by clients of one OKX User ID in one trading environment.
/// Share the same instance across REST/WS clients and API keys belonging to that account.
/// This process-local guard cannot observe orders submitted by other applications.
/// Cancellation and server-side in-progress amendment limits are outside its scope.
/// MMP placement is exempt from the account budget; amendments with unknown original type are counted conservatively.
/// </summary>
public sealed class OkxTradeRateLimiter
{
    private static readonly TimeSpan Window = TimeSpan.FromSeconds(2);
    private readonly object _sync = new();
    private readonly Func<TimeSpan> _elapsed;
    private readonly Dictionary<string, Instrument> _instruments = new(StringComparer.Ordinal);
    private readonly Dictionary<long, Instrument> _codes = new();
    private readonly Dictionary<(OkxTradeRateLimitOperation, bool, string), Bucket> _buckets = new();
    private readonly Bucket _account = new();
    private int _accountRateLimit;

    /// <summary>
    /// Creates a guard with the documented base account limit of 1000 orders per two seconds.
    /// A higher limit must come from the account's current accRateLimit, not nextAccRateLimit or a guessed VIP tier.
    /// Existing transport guards remain active. Pending commands are counted until completion, then for two seconds.
    /// </summary>
    /// <param name="accountRateLimit">Current account limit, or a lower application-imposed cap.</param>
    public OkxTradeRateLimiter(int accountRateLimit = 1000)
        : this(accountRateLimit, CreateClock()) { }

    internal OkxTradeRateLimiter(int accountRateLimit, Func<TimeSpan> elapsed)
    {
        if (accountRateLimit < 1) throw new ArgumentOutOfRangeException(nameof(accountRateLimit));
        _accountRateLimit = accountRateLimit;
        _elapsed = elapsed;
    }

    /// <summary>
    /// Registers a snapshot of official instrument metadata and the caller's account-specific lead classification.
    /// Options require instFamily. Unknown instruments/codes are rejected before sending when this guard is enabled.
    /// Production and demo metadata must not be mixed. No metadata is fetched and no lead status is inferred.
    /// </summary>
    /// <param name="instrument">Current instrument metadata; caller mutations do not affect the registered snapshot.</param>
    /// <param name="isLeadInstrument">Whether this is a lead instrument for this account's Copy Trading activity.</param>
    public void RegisterInstrument(OkxPublicInstrument instrument, bool isLeadInstrument)
    {
        if (instrument is null) throw new ArgumentNullException(nameof(instrument));
        if (string.IsNullOrWhiteSpace(instrument.InstrumentId))
            throw new ArgumentException("InstrumentId is required.", nameof(instrument));
        if (instrument.InstrumentType is not (OkxInstrumentType.Spot or OkxInstrumentType.Margin
            or OkxInstrumentType.Swap or OkxInstrumentType.Futures or OkxInstrumentType.Option or OkxInstrumentType.Events))
            throw new ArgumentException("A concrete instrument type is required.", nameof(instrument));
        if (instrument.InstrumentType == OkxInstrumentType.Option && string.IsNullOrWhiteSpace(instrument.InstrumentFamily))
            throw new ArgumentException("Options require the official InstrumentFamily.", nameof(instrument));

        var scope = instrument.InstrumentType == OkxInstrumentType.Option
            ? "family:" + instrument.InstrumentFamily : "instrument:" + instrument.InstrumentId;
        var snapshot = new Instrument(instrument.InstrumentId, instrument.InstrumentIdCode, scope,
            instrument.InstrumentType, isLeadInstrument);
        lock (_sync)
        {
            if (_instruments.TryGetValue(snapshot.Id, out var old) && (old.Scope != scope || old.AccountExempt != snapshot.AccountExempt))
                throw new ArgumentException("An existing instrument's rate-limit scope cannot be changed.", nameof(instrument));
            if (snapshot.Code.HasValue && _codes.TryGetValue(snapshot.Code.Value, out var mapped) && mapped.Id != snapshot.Id)
                throw new ArgumentException("InstrumentIdCode is already registered to another instrument.", nameof(instrument));
            if (old?.Code is long oldCode) _codes.Remove(oldCode);
            _instruments[snapshot.Id] = snapshot;
            if (snapshot.Code.HasValue) _codes[snapshot.Code.Value] = snapshot;
        }
    }

    /// <summary>
    /// Updates the current account budget without clearing previous usage or pending commands.
    /// Apply only a current accRateLimit or a lower application cap; no scheduled VIP transition is inferred.
    /// </summary>
    /// <param name="accountRateLimit">Current limit per two seconds.</param>
    public void SetAccountRateLimit(int accountRateLimit)
    {
        if (accountRateLimit < 1) throw new ArgumentOutOfRangeException(nameof(accountRateLimit));
        lock (_sync) _accountRateLimit = accountRateLimit;
    }

    internal ClientRateLimitError? TryAcquire(OkxTradeRateLimitOperation operation,
        IEnumerable<OkxTradeRateLimitOrder> orders, bool isBatch, out IDisposable? reservation)
    {
        reservation = null;
        var targets = orders.ToList();
        if (targets.Count is < 1 or > 20 || (!isBatch && targets.Count != 1))
            throw new ArgumentException("Single commands require one order; batches require 1-20 orders.", nameof(orders));

        lock (_sync)
        {
            var batchBudget = isBatch && targets.Count > 1;
            var demands = new Dictionary<(OkxTradeRateLimitOperation, bool, string), (int Weight, int Limit)>();
            var accountWeight = 0;
            foreach (var target in targets)
            {
                Instrument? instrument;
                if (target.InstrumentIdCode.HasValue)
                    _codes.TryGetValue(target.InstrumentIdCode.Value, out instrument);
                else if (target.InstrumentId is not null)
                    _instruments.TryGetValue(target.InstrumentId, out instrument);
                else instrument = null;
                if (instrument is null)
                    throw new InvalidOperationException("Register current instrument metadata in TradeRateLimiter before sending Place/Amend commands.");

                var key = (operation, batchBudget, instrument.Scope);
                var limit = instrument.IsLead ? 4 : batchBudget ? 300 : 60;
                if (demands.TryGetValue(key, out var demand))
                    demands[key] = (demand.Weight + 1, Math.Min(demand.Limit, limit));
                else demands[key] = (1, limit);
                // Placement identifies MMP explicitly; amendments do not expose the original order type.
                // Unknown MMP amendments are conservatively counted, never guessed to be exempt.
                if (!instrument.AccountExempt && !(operation == OkxTradeRateLimitOperation.Place
                    && instrument.Type == OkxInstrumentType.Option && target.IsMmp)) accountWeight++;
            }

            var now = _elapsed();
            var checks = new List<(Bucket Bucket, int Weight, int Limit)>();
            foreach (var demand in demands)
            {
                if (!_buckets.TryGetValue(demand.Key, out var bucket))
                    _buckets[demand.Key] = bucket = new Bucket();
                checks.Add((bucket, demand.Value.Weight, demand.Value.Limit));
            }
            if (accountWeight > 0) checks.Add((_account, accountWeight, _accountRateLimit));
            foreach (var check in checks)
            {
                check.Bucket.Prune(now);
                if (check.Weight > check.Limit - check.Bucket.Used)
                    return new ClientRateLimitError("Local shared Place/Amend rate limit reached; no command was sent. Pending commands count until completion, then for two seconds.");
            }

            var usages = new List<Usage>();
            foreach (var check in checks)
            {
                var usage = new Usage(check.Weight);
                check.Bucket.Entries.Add(usage);
                check.Bucket.Used += check.Weight;
                usages.Add(usage);
            }
            reservation = new Reservation(this, usages);
            return null;
        }
    }

    private static Func<TimeSpan> CreateClock()
    {
        var clock = Stopwatch.StartNew();
        return () => clock.Elapsed;
    }

    private sealed class Instrument(string id, long? code, string scope, OkxInstrumentType type, bool isLead)
    {
        internal string Id { get; } = id;
        internal long? Code { get; } = code;
        internal string Scope { get; } = scope;
        internal OkxInstrumentType Type { get; } = type;
        internal bool AccountExempt => Type is OkxInstrumentType.Spot or OkxInstrumentType.Margin;
        internal bool IsLead { get; } = isLead;
    }
    private sealed class Usage(int weight)
    {
        internal int Weight { get; } = weight;
        internal TimeSpan? CompletedAt { get; set; }
    }
    private sealed class Bucket
    {
        internal List<Usage> Entries { get; } = new();
        internal int Used { get; set; }
        internal void Prune(TimeSpan now)
        {
            for (var i = Entries.Count - 1; i >= 0; i--)
            {
                if (Entries[i].CompletedAt is not TimeSpan completed || now - completed < Window) continue;
                Used -= Entries[i].Weight;
                Entries.RemoveAt(i);
            }
        }
    }
    private sealed class Reservation(OkxTradeRateLimiter owner, List<Usage> usages) : IDisposable
    {
        private bool _completed;
        public void Dispose()
        {
            lock (owner._sync)
            {
                if (_completed) return;
                var now = owner._elapsed();
                foreach (var usage in usages) usage.CompletedAt = now;
                _completed = true;
            }
        }
    }
}

internal enum OkxTradeRateLimitOperation { Place, Amend }
internal sealed class OkxTradeRateLimitOrder(string? instrumentId, long? instrumentIdCode = null, bool isMmp = false)
{
    internal string? InstrumentId { get; } = instrumentId;
    internal long? InstrumentIdCode { get; } = instrumentIdCode;
    internal bool IsMmp { get; } = isMmp;
}
