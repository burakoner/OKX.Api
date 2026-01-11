namespace OKX.Api.Public;

/// <summary>
/// OKX Instrument
/// </summary>
public record OkxPublicInstrument
{
    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonProperty("instType")]
    public OkxInstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Instrument ID, e.g. BTC-USD-SWAP
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; } = string.Empty;

    /// <summary>
    /// Instrument ID code.
    /// For simple binary encoding, you must use instIdCode instead of instId.
    /// For the same instId, it's value may be different between production and demo trading.
    /// </summary>
    [JsonProperty("instIdCode")]
    public int InstrumentIdCode { get; set; }

    /// <summary>
    /// Underlying, e.g. BTC-USD. Only applicable to FUTURES/SWAP/OPTION
    /// </summary>
    [JsonProperty("uly")]
    public string? Underlying { get; set; } = string.Empty;

    /// <summary>
    /// Instrument trading fee group ID
    /// Spot:
    /// 1: Spot USDT
    /// 2: Spot USDC &amp; Crypto
    /// 3: Spot TRY
    /// 4: Spot EUR
    /// 5: Spot BRL
    /// 7: Spot AED
    /// 8: Spot AUD
    /// 9: Spot USD
    /// 10: Spot SGD
    /// 11: Spot zero
    /// 12: Spot group one
    /// 13: Spot group two
    /// 14: Spot group three
    /// 15: Spot special rule
    /// 
    /// Expiry futures:
    /// 1: Expiry futures crypto-margined
    /// 2: Expiry futures USDT-margined
    /// 3: Expiry futures USDC-margined
    /// 4: Expiry futures premarket
    /// 5: Expiry futures group one
    /// 6: Expiry futures group two
    /// 
    /// Perpetual futures:
    /// 1: Perpetual futures crypto-margined
    /// 2: Perpetual futures USDT-margined
    /// 3: Perpetual futures USDC-margined
    /// 4: Perpetual futures group one
    /// 5: Perpetual futures group two
    /// 
    /// Options:
    /// 1: Options crypto-margined
    /// 2: Options USDC-margined
    /// 
    /// instType and groupId should be used together to determine a trading fee group.Users should use this endpoint together with fee rates endpoint to get the trading fee of a specific symbol.
    /// 
    /// Some enum values may not apply to you; the actual return values shall prevail.
    /// </summary>
    [JsonProperty("groupId")]
    public int? GroupId { get; set; }

    /// <summary>
    /// Instrument family
    /// </summary>
    [JsonProperty("instFamily")]
    public string? InstrumentFamily { get; set; } = string.Empty;

    /// <summary>
    /// Base currency
    /// </summary>
    [JsonProperty("baseCcy")]
    public string? BaseCurrency { get; set; } = string.Empty;

    /// <summary>
    /// Quote currency
    /// </summary>
    [JsonProperty("quoteCcy")]
    public string? QuoteCurrency { get; set; } = string.Empty;

    /// <summary>
    /// Settlement currency
    /// </summary>
    [JsonProperty("settleCcy")]
    public string? SettlementCurrency { get; set; } = string.Empty;

    /// <summary>
    /// Contract value
    /// </summary>
    [JsonProperty("ctVal")]
    public decimal? ContractValue { get; set; }

    /// <summary>
    /// Contract multiplier
    /// </summary>
    [JsonProperty("ctMult")]
    public decimal? ContractMultiplier { get; set; }

    /// <summary>
    /// Contract value currency
    /// </summary>
    [JsonProperty("ctValCcy")]
    public string? ContractValueCurrency { get; set; } = string.Empty;

    /// <summary>
    /// Option type
    /// </summary>
    [JsonProperty("optType")]
    public OkxOptionType? OptionType { get; set; }

    /// <summary>
    /// Strike price
    /// </summary>
    [JsonProperty("stk")]
    public decimal? StrikePrice { get; set; }

    /// <summary>
    /// Listing timestamp
    /// </summary>
    [JsonProperty("listTime")]
    public long? ListingTimestamp { get; set; }

    /// <summary>
    /// Listing time
    /// </summary>
    [JsonIgnore]
    public DateTime? ListingTime => ListingTimestamp?.ConvertFromMilliseconds();

    /// <summary>
    /// Continuous trading switch time. The switch time from call auction, prequote to continuous trading, Unix timestamp format in milliseconds. e.g. 1597026383085.
    /// Only applicable to SPOT/MARGIN that are listed through call auction or prequote, return "" in other cases.
    /// </summary>
    [JsonProperty("contTdSwTime")]
    public long? ContinuousTradingSwitchTimestamp { get; set; }

    /// <summary>
    /// Continuous trading switch time. The switch time from call auction, prequote to continuous trading, Unix timestamp format in milliseconds. e.g. 1597026383085.
    /// Only applicable to SPOT/MARGIN that are listed through call auction or prequote, return "" in other cases.
    /// </summary>
    [JsonIgnore]
    public DateTime? ContinuousTradingSwitchTime => ContinuousTradingSwitchTimestamp?.ConvertFromMilliseconds();

    /// <summary>
    /// The time premarket swap switched to normal swap, Unix timestamp format in milliseconds, e.g. 1597026383085.
    /// Only applicable premarket SWAP
    /// </summary>
    [JsonProperty("preMktSwTime")]
    public long? PreMarketSwitchTimestamp { get; set; }

    /// <summary>
    /// The time premarket swap switched to normal swap, Unix timestamp format in milliseconds, e.g. 1597026383085.
    /// Only applicable premarket SWAP
    /// </summary>
    [JsonIgnore]
    public DateTime? PreMarketSwitchTime => PreMarketSwitchTimestamp?.ConvertFromMilliseconds();

    /// <summary>
    /// Open type
    /// fix_price: fix price opening
    /// pre_quote: pre-quote
    /// call_auction: call auctio
    /// Only applicable to SPOT/MARGIN, return "" for all other business lines
    /// </summary>
    [JsonProperty("openType")]
    public OkxPublicOpenType? OpenType { get; set; }

    /// <summary>
    /// Expiry timestamp
    /// </summary>
    [JsonProperty("expTime")]
    public long? ExpiryTimestamp { get; set; }

    /// <summary>
    /// Expiry time
    /// </summary>
    [JsonIgnore]
    public DateTime? ExpiryTime => ExpiryTimestamp?.ConvertFromMilliseconds();

    /// <summary>
    /// Maximal leverage
    /// </summary>
    [JsonProperty("lever")]
    public decimal? MaximumLeverage { get; set; }

    /// <summary>
    /// Tick size
    /// </summary>
    [JsonProperty("tickSz")]
    public decimal? TickSize { get; set; }

    /// <summary>
    /// Lot size
    /// </summary>
    [JsonProperty("lotSz")]
    public decimal? LotSize { get; set; }

    /// <summary>
    /// Minimal order size
    /// </summary>
    [JsonProperty("minSz")]
    public decimal? MinimumOrderSize { get; set; }

    /// <summary>
    /// Contract type
    /// </summary>
    [JsonProperty("ctType")]
    public OkxContractType? ContractType { get; set; }

    /// <summary>
    /// Alias
    /// this_week
    /// next_week
    /// this_month
    /// next_month
    /// quarter
    /// next_quarter
    /// third_quarter
    /// Only applicable to FUTURES
    /// Not recommended for use, users are encouraged to rely on the expTime field to determine the delivery time of the contract
    /// </summary>
    [JsonProperty("alias")]
    public string? Alias { get; set; } = string.Empty;

    /// <summary>
    /// State
    /// </summary>
    [JsonProperty("state")]
    public OkxInstrumentState State { get; set; }

    /// <summary>
    /// Trading rule types
    /// </summary>
    [JsonProperty("ruleType")]
    public OkxInstrumentRuleType? RuleType { get; set; }

    /// <summary>
    /// Maximal limit order size
    /// </summary>
    [JsonProperty("maxLmtSz")]
    public decimal? MaximumLimitOrderSize { get; set; }

    /// <summary>
    /// Maximum market order size
    /// </summary>
    [JsonProperty("maxMktSz")]
    public decimal? MaximumMarketOrderSize { get; set; }

    /// <summary>
    /// Maximal limit order size in USD
    /// </summary>
    [JsonProperty("maxLmtAmt")]
    public decimal? MaximumLimitOrderSizeInUsd { get; set; }

    /// <summary>
    /// Maximum market order size in USD
    /// </summary>
    [JsonProperty("maxMktAmt")]
    public decimal? MaximumMarketOrderSizeInUsd { get; set; }

    /// <summary>
    /// Maximum TWAP order size
    /// </summary>
    [JsonProperty("maxTwapSz")]
    public decimal? MaximumTwapOrderSize { get; set; }

    /// <summary>
    /// Maximum iceberg order size
    /// </summary>
    [JsonProperty("maxIcebergSz")]
    public decimal? MaximumIcebergOrderSize { get; set; }

    /// <summary>
    /// Maximum trigger order size
    /// </summary>
    [JsonProperty("maxTriggerSz")]
    public decimal? MaximumTriggerOrderSize { get; set; }

    /// <summary>
    /// Maximum stop order size
    /// </summary>
    [JsonProperty("maxStopSz")]
    public decimal? MaximumStopMarketSize { get; set; }

    /// <summary>
    /// Whether daily settlement for expiry feature is enabled
    /// Applicable to FUTURES cross
    /// </summary>
    [JsonProperty("futureSettlement")]
    public bool? IsFutureSettlement { get; set; }

    /// <summary>
    /// List of quote currencies available for trading, e.g. ["USD", "USDC"].
    /// </summary>
    [JsonProperty("tradeQuoteCcyList")]
    public List<string>? TradeQuoteCurrencyList { get; set; } = [];

    /// <summary>
    /// Maximum position value (USD) for this instrument at the user level, based on the notional value of all same-direction open positions and resting orders. The effective user limit is max(posLmtAmt, oiUSD × posLmtPct). Applicable to SWAP/FUTURES.
    /// </summary>
    [JsonProperty("posLmtAmt")]
    public decimal? MaximumPositionValue { get; set; }

    /// <summary>
    /// Maximum position ratio (e.g., 30 for 30%) a user may hold relative to the platform’s current total position value. The effective user limit is max(posLmtAmt, oiUSD × posLmtPct). Applicable to SWAP/FUTURES.
    /// </summary>
    [JsonProperty("posLmtPct")]
    public decimal? MaximumPositionRatio { get; set; }

    /// <summary>
    /// Platform-wide maximum position value (USD) for this instrument. If the global position limit switch is enabled and platform total open interest reaches or exceeds this value, all users’ new opening orders for this instrument are rejected; otherwise, orders pass.
    /// </summary>
    [JsonProperty("maxPlatOILmt")]
    public decimal? PlatformWideMaximumPositionValue { get; set; }

    /// <summary>
    /// Upcoming changes. It is [] when there is no upcoming change.
    /// </summary>
    [JsonProperty("upcChg")]
    public List<OkxPublicInstrumentUpcomingChange>? UpcomingChanges { get; set; }
}

/// <summary>
/// OKX Instrument
/// </summary>
public record OkxPublicInstrumentUpcomingChange
{
    /// <summary>
    /// The parameter name to be updated.
    /// tickSz
    /// minSz
    /// maxMktSz
    /// </summary>
    [JsonProperty("param")]
    public string Parameter { get; set; } = "";

    /// <summary>
    /// The parameter value that will replace the current one.
    /// </summary>
    [JsonProperty("newValue")]
    public string NewValue { get; set; } = "";

    /// <summary>
    /// Effective timestamp
    /// </summary>
    [JsonProperty("effTime")]
    public long EffectiveTimestamp { get; set; }

    /// <summary>
    /// Effective time
    /// </summary>
    [JsonIgnore]
    public DateTime EffectiveTime => EffectiveTimestamp.ConvertFromMilliseconds();
}