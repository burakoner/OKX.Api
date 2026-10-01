using System.Collections.Concurrent;
using ApiSharp.Models;
using OKX.Api.Common;
using OKX.Api.Public;
using OKX.Api.Trade;

namespace OKX.Api.Tests.Trade;

public class OkxTradeRateLimiterTests
{
    private static readonly OkxTradeRateLimitOrder A = new("BTC-USDT-SWAP");
    private static readonly OkxTradeRateLimitOrder B = new("ETH-USDT-SWAP");

    [Fact]
    public void SingleAndOneOrderBatch_ShareSixtySlotsAcrossIdAndCode()
    {
        var limiter = Create();
        for (var i = 0; i < 60; i++)
            AssertAllowed(limiter, [i % 2 == 0 ? A : new(null, 101)], batch: i % 3 == 0);
        AssertBlocked(limiter, [A]);
        AssertBlocked(limiter, [new(null, 101)], batch: true);
        AssertAllowed(limiter, [B]); // Same operation, different non-Option instrument.
        AssertAllowed(limiter, [A], operation: OkxTradeRateLimitOperation.Amend);
    }

    [Fact]
    public void BatchBudget_CountsOrdersAndRemainsIndependentOfSingleBudget()
    {
        var limiter = Create();
        for (var i = 0; i < 15; i++) AssertAllowed(limiter, Enumerable.Repeat(A, 20), batch: true);
        AssertBlocked(limiter, [A, A], batch: true);
        AssertAllowed(limiter, [A]);
        AssertAllowed(limiter, [A], batch: true); // One-order batch routes to single, not exhausted batch budget.
        AssertAllowed(limiter, [A, A], batch: true, operation: OkxTradeRateLimitOperation.Amend);
    }

    [Fact]
    public void MixedBatch_WeightsEachScopeAndRejectsAtomically()
    {
        var limiter = Create();
        for (var i = 0; i < 15; i++) AssertAllowed(limiter, Enumerable.Repeat(A, 20), batch: true);
        AssertBlocked(limiter, [A, B], batch: true);
        for (var i = 0; i < 15; i++) AssertAllowed(limiter, Enumerable.Repeat(B, 20), batch: true);
        AssertBlocked(limiter, [B, B], batch: true);
    }

    [Theory]
    [InlineData(OkxInstrumentType.Option, false)]
    [InlineData(OkxInstrumentType.Futures, true)]
    public void OptionsShareFamily_OtherTypesUseInstrumentId(OkxInstrumentType type, bool secondAllowed)
    {
        var limiter = new OkxTradeRateLimiter(1000, () => TimeSpan.Zero);
        limiter.RegisterInstrument(Instrument("BTC-USD-ONE", 201, type, "BTC-USD"), false);
        limiter.RegisterInstrument(Instrument("BTC-USD-TWO", 202, type, "BTC-USD"), false);
        limiter.RegisterInstrument(Instrument("ETH-USD-ONE", 203, type, "ETH-USD"), false);
        for (var i = 0; i < 60; i++) AssertAllowed(limiter, [new("BTC-USD-ONE")]);
        var error = Acquire(limiter, [new(null, 202)], out var reservation);
        Assert.Equal(secondAllowed, error is null);
        reservation?.Dispose();
        AssertAllowed(limiter, [new(null, 203)]);
    }

    [Fact]
    public void AccountBudget_AggregatesOperationsScopesAndBatchWeightsWithoutPartialCharges()
    {
        var limiter = Create(accountLimit: 3);
        AssertAllowed(limiter, [A]);
        AssertAllowed(limiter, [B], operation: OkxTradeRateLimitOperation.Amend);
        AssertBlocked(limiter, [A, B], batch: true);
        AssertAllowed(limiter, [new(null, 101)]);
        AssertBlocked(limiter, [B], operation: OkxTradeRateLimitOperation.Amend);
    }

    [Fact]
    public void LeadInstrument_UsesFourSlotsAndBatchCountsOrders()
    {
        var limiter = Create(lead: true);
        AssertBlocked(limiter, Enumerable.Repeat(A, 5), batch: true);
        AssertAllowed(limiter, [A, A, A, A], batch: true);
        AssertBlocked(limiter, [A, A], batch: true);
        for (var i = 0; i < 4; i++) AssertAllowed(limiter, [A], batch: i % 2 == 0);
        AssertBlocked(limiter, [A]);
        for (var i = 0; i < 4; i++) AssertAllowed(limiter, [A], operation: OkxTradeRateLimitOperation.Amend);
    }

    [Fact]
    public void PendingReservations_NeverExpireBeforeCompletionAndRetainFullWindowAfterwards()
    {
        var clock = TimeSpan.Zero;
        var limiter = Create(() => clock, accountLimit: 1);
        Assert.Null(Acquire(limiter, [A], out var pending));
        clock = TimeSpan.FromHours(1);
        AssertBlocked(limiter, [B]);
        pending!.Dispose();
        clock += TimeSpan.FromMilliseconds(1999);
        AssertBlocked(limiter, [B]);
        pending.Dispose(); // A duplicate dispose must not move the completion time.
        clock += TimeSpan.FromMilliseconds(1);
        AssertAllowed(limiter, [B]);
    }

    [Fact]
    public void OutOfOrderCompletion_ExpiresOnlyCompletedUsageWithoutHeadOfLineBlocking()
    {
        var clock = TimeSpan.Zero;
        var limiter = Create(() => clock, accountLimit: 2);
        Assert.Null(Acquire(limiter, [A], out var first));
        Assert.Null(Acquire(limiter, [B], out var second));
        second!.Dispose();
        clock = TimeSpan.FromSeconds(2);
        AssertAllowed(limiter, [B]);
        AssertBlocked(limiter, [B]); // The first command is still pending and retains the other account slot.
        first!.Dispose();
    }

    [Fact]
    public void ConcurrentReservations_CannotOversubscribeTheSharedBudget()
    {
        var limiter = Create();
        var allowed = new ConcurrentBag<IDisposable>();
        Parallel.For(0, 200, _ =>
        {
            if (Acquire(limiter, [A], out var lease) is null) allowed.Add(lease!);
        });
        Assert.Equal(60, allowed.Count);
        AssertBlocked(limiter, [new(null, 101)]);
        foreach (var lease in allowed) lease.Dispose();
    }

    [Fact]
    public void UpdatedAccountAndLeadLimits_DoNotResetUsage()
    {
        var limiter = Create(accountLimit: 10);
        for (var i = 0; i < 4; i++) AssertAllowed(limiter, [A]);
        limiter.SetAccountRateLimit(4);
        AssertBlocked(limiter, [B]);
        limiter.SetAccountRateLimit(10);
        limiter.RegisterInstrument(Instrument("BTC-USDT-SWAP", 101), true);
        AssertBlocked(limiter, [A]);
        AssertAllowed(limiter, [B]);
    }

    [Fact]
    public void SpotMarginAndKnownMmpPlacement_AreExemptFromAccountBudgetButNotInstrumentBudget()
    {
        var limiter = Create(accountLimit: 1);
        limiter.RegisterInstrument(Instrument("BTC-USDT", 301, OkxInstrumentType.Spot), false);
        limiter.RegisterInstrument(Instrument("ETH-USDT", 302, OkxInstrumentType.Margin), false);
        limiter.RegisterInstrument(Instrument("BTC-USD-OPTION", 303, OkxInstrumentType.Option, "BTC-USD"), false);
        AssertAllowed(limiter, [A]); // Exhaust non-exempt account budget.
        for (var i = 0; i < 60; i++) AssertAllowed(limiter, [new("BTC-USDT")]);
        AssertBlocked(limiter, [new("BTC-USDT")]);
        AssertAllowed(limiter, [new("ETH-USDT")]);
        AssertAllowed(limiter, [new(null, 303, isMmp: true)]);
        // Original order type is unavailable for amendment: even an internal MMP hint cannot bypass account counting.
        AssertBlocked(limiter, [new(null, 303, isMmp: true)], operation: OkxTradeRateLimitOperation.Amend);
    }

    [Fact]
    public void NonOptionMmpHint_DoesNotBypassTheAccountBudget()
    {
        var limiter = Create(accountLimit: 1);
        AssertAllowed(limiter, [A]);
        AssertBlocked(limiter, [new(B.InstrumentId, isMmp: true)]);
    }

    [Fact]
    public void MetadataIsSnapshotted_UnknownCodesNeverFallBackToId()
    {
        var limiter = Create();
        var instrument = Instrument("NEW-SWAP", 501);
        limiter.RegisterInstrument(instrument, false);
        instrument.InstrumentId = "MUTATED";
        instrument.InstrumentIdCode = 502;
        AssertAllowed(limiter, [new("NEW-SWAP")]);
        AssertAllowed(limiter, [new(null, 501)]);
        Assert.Throws<InvalidOperationException>(() => Acquire(limiter, [new("MUTATED")], out _));
        Assert.Throws<InvalidOperationException>(() => Acquire(limiter, [new("NEW-SWAP", 502)], out _));
        Assert.Throws<InvalidOperationException>(() => Acquire(limiter, [new("UNKNOWN")], out _));
    }

    [Fact]
    public void UpdatingInstrumentCode_DoesNotClearTheExistingScopeBudget()
    {
        var limiter = Create(lead: true);
        for (var i = 0; i < 4; i++) AssertAllowed(limiter, [new(null, 101)]);
        limiter.RegisterInstrument(Instrument("BTC-USDT-SWAP", 999), true);
        AssertBlocked(limiter, [new(null, 999)]);
        Assert.Throws<InvalidOperationException>(() => Acquire(limiter, [new(null, 101)], out _));
    }

    [Fact]
    public void InvalidConfigurationAndCommandShapes_AreRejected()
    {
        var limiter = Create();
        Assert.Throws<ArgumentOutOfRangeException>(() => new OkxTradeRateLimiter(0));
        Assert.Throws<ArgumentOutOfRangeException>(() => limiter.SetAccountRateLimit(0));
        Assert.Throws<ArgumentException>(() => limiter.RegisterInstrument(Instrument("OPT", 400, OkxInstrumentType.Option), false));
        Assert.Throws<ArgumentException>(() => limiter.RegisterInstrument(Instrument("ANY", 400, OkxInstrumentType.Any), false));
        Assert.Throws<ArgumentException>(() => limiter.RegisterInstrument(Instrument("OTHER", 101), false));
        Assert.Throws<ArgumentException>(() => Acquire(limiter, [], out _));
        Assert.Throws<ArgumentException>(() => Acquire(limiter, [A, B], out _));
        Assert.Throws<ArgumentException>(() => Acquire(limiter, Enumerable.Repeat(A, 21), out _, batch: true));
    }

    private static OkxTradeRateLimiter Create(Func<TimeSpan>? clock = null, int accountLimit = 1000, bool lead = false)
    {
        var limiter = new OkxTradeRateLimiter(accountLimit, clock ?? (() => TimeSpan.Zero));
        limiter.RegisterInstrument(Instrument("BTC-USDT-SWAP", 101), lead);
        limiter.RegisterInstrument(Instrument("ETH-USDT-SWAP", 102), false);
        return limiter;
    }

    internal static OkxPublicInstrument Instrument(string id, long code, OkxInstrumentType type = OkxInstrumentType.Swap, string? family = null)
        => new() { InstrumentId = id, InstrumentIdCode = code, InstrumentType = type, InstrumentFamily = family };

    private static ClientRateLimitError? Acquire(OkxTradeRateLimiter limiter, IEnumerable<OkxTradeRateLimitOrder> orders,
        out IDisposable? reservation, bool batch = false, OkxTradeRateLimitOperation operation = OkxTradeRateLimitOperation.Place)
        => limiter.TryAcquire(operation, orders, batch, out reservation);

    private static void AssertAllowed(OkxTradeRateLimiter limiter, IEnumerable<OkxTradeRateLimitOrder> orders,
        bool batch = false, OkxTradeRateLimitOperation operation = OkxTradeRateLimitOperation.Place)
    {
        Assert.Null(Acquire(limiter, orders, out var reservation, batch, operation));
        Assert.NotNull(reservation);
        reservation.Dispose();
    }

    private static void AssertBlocked(OkxTradeRateLimiter limiter, IEnumerable<OkxTradeRateLimitOrder> orders,
        bool batch = false, OkxTradeRateLimitOperation operation = OkxTradeRateLimitOperation.Place)
    {
        Assert.IsType<ClientRateLimitError>(Acquire(limiter, orders, out var reservation, batch, operation));
        Assert.Null(reservation);
    }
}
