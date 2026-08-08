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
    /// Demo trading responses may return values that exceed 32-bit integer range.
    /// </summary>
    [JsonProperty("instIdCode")]
    public long? InstrumentIdCode { get; set; }

    /// <summary>
    /// Underlying, e.g. BTC-USD. Only applicable to MARGIN/FUTURES/SWAP/OPTION.
    /// </summary>
    [JsonProperty("uly")]
    public string? Underlying { get; set; } = string.Empty;

    /// <summary>
    /// Instrument trading fee group ID
    /// Spot:
    /// 3: Spot TRY
    /// 5: Spot BRL
    /// 7: Spot AED
    /// 8: Spot AUD
    /// 10: Spot SGD
    /// 11: Spot zero
    /// 12: Spot group one
    /// 13: Spot group two
    /// 14: Spot group three
    /// 15: Spot special rule
    /// 17: Spot stablecoin
    /// 22: Spot RWA group two
    /// 
    /// Expiry futures:
    /// 5: Expiry futures group one
    /// 6: Expiry futures group two
    /// 8: XPERP group two
    /// 10: XPERP RWA group two
    /// 
    /// Perpetual futures:
    /// 4: Perpetual futures group one
    /// 5: Perpetual futures group two
    /// 6: SWAP RWA group one
    /// 7: SWAP RWA group two
    /// 
    /// Options:
    /// 1: Options crypto-margined
    /// 
    /// instType and groupId should be used together to determine a trading fee group. Users should use this endpoint together with the fee rates endpoint to get the trading fee of a specific symbol.
    /// 
    /// Some enum values may not apply to you; the actual return values shall prevail.
    /// </summary>
    [JsonProperty("groupId")]
    public string? GroupId { get; set; }

    /// <summary>
    /// Instrument family. Only applicable to MARGIN/FUTURES/SWAP/OPTION.
    /// </summary>
    [JsonProperty("instFamily")]
    public string? InstrumentFamily { get; set; } = string.Empty;

    /// <summary>
    /// Deprecated currency category returned by the public REST endpoint and instruments channel.
    /// </summary>
    [JsonProperty("category")]
    public string? DeprecatedCategory { get; set; }

    /// <summary>
    /// Series ID, e.g. BTC-ABOVE-DAILY.
    /// Only applicable to EVENTS.
    /// </summary>
    [JsonProperty("seriesId")]
    public string? SeriesId { get; set; } = string.Empty;

    /// <summary>
    /// Base currency
    /// </summary>
    [JsonProperty("baseCcy")]
    public string? BaseCurrency { get; set; } = string.Empty;

    /// <summary>
    /// Category of the instrument's base currency.
    /// For example, for BTC-USDT-SWAP, instCategory refers to the category of BTC.
    /// </summary>
    [JsonProperty("instCategory")]
    public OkxPublicInstrumentCategory? InstrumentCategory { get; set; }

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
    /// Deprecated call-auction end timestamp. Use ContinuousTradingSwitchTimestamp instead.
    /// </summary>
    [JsonProperty("auctionEndTime")]
    public long? AuctionEndTimestamp { get; set; }

    /// <summary>
    /// Deprecated call-auction end time.
    /// </summary>
    [JsonIgnore]
    public DateTime? AuctionEndTime => AuctionEndTimestamp?.ConvertFromMilliseconds();

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
    /// The time a pre-market instrument switched to normal trading, Unix timestamp format in milliseconds, e.g. 1597026383085.
    /// Only applicable to pre-market SWAP and pre-market X-Perp FUTURES.
    /// </summary>
    [JsonProperty("preMktSwTime")]
    public long? PreMarketSwitchTimestamp { get; set; }

    /// <summary>
    /// The time a pre-market instrument switched to normal trading, Unix timestamp format in milliseconds, e.g. 1597026383085.
    /// Only applicable to pre-market SWAP and pre-market X-Perp FUTURES.
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
    /// Legacy ELP maker permission alias.
    /// 0: ELP is not enabled for this symbol
    /// 1: ELP is enabled for this symbol, but current users don't have permission to place ELP orders for it.
    /// 2: ELP is enabled for this symbol, and current users have permission to place ELP orders for it.
    /// It doesn't mean there will be ELP liquidity when elp is 1/2.
    /// </summary>
    [JsonProperty("elp")]
    public OkxPublicElpPermission? ElpMakerPermission { get; set; }

    /// <summary>
    /// RPI maker permission. Only returned by the private instruments endpoint.
    /// ELP remains accepted by OKX as a temporary alias through October 31, 2026.
    /// </summary>
    [JsonProperty("rpi")]
    public OkxPublicRpiPermission? RpiMakerPermission { get; set; }

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
    /// Exchange-defined maximum leverage ceiling for this instrument.
    /// The leverage available to an account may be lower based on VIP tier and position size.
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
    /// this_five_years
    /// next_five_years
    /// Only applicable to FUTURES
    /// Deprecated by OKX and scheduled for removal at the end of April 2026.
    /// Users are encouraged to rely on the expTime field to determine the delivery time of the contract.
    /// </summary>
    [JsonProperty("alias")]
    public string? Alias { get; set; } = string.Empty;

    /// <summary>
    /// Instrument status, e.g. live, suspend, rebase, post_only, preopen, expired, test, settling.
    /// In post_only state, only post-only orders are accepted; other order types are rejected.
    /// </summary>
    [JsonProperty("state")]
    public OkxInstrumentState State { get; set; }

    /// <summary>
    /// Trading rule types, e.g. normal, pre_market, rebase_contract, xperp
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
    /// Platform-wide maximum position value (USD) for this instrument. If platform total open interest reaches or exceeds this value, all users’ new opening orders for this instrument are rejected; otherwise, orders pass.
    /// </summary>
    [JsonProperty("maxPlatOILmt")]
    public decimal? PlatformWideMaximumPositionValue { get; set; }

    /// <summary>
    /// Platform-wide maximum position value in coins for this instrument.
    /// Only applicable to SWAP/FUTURES on the private instruments endpoint.
    /// </summary>
    [JsonProperty("maxPlatOICoinLmt")]
    public decimal? PlatformWideMaximumPositionValueInCoins { get; set; }

    /// <summary>
    /// Remaining long position value (USD) the user is permitted to open, netting all existing long positions and resting buy orders.
    /// Only applicable to the private instruments endpoint.
    /// The quota is shared across the master account and all sub-accounts.
    /// </summary>
    [JsonProperty("longPosRemainingQuota")]
    public decimal? LongPositionRemainingQuota { get; set; }

    /// <summary>
    /// Remaining short position value (USD) the user is permitted to open, netting all existing short positions and resting sell orders.
    /// Only applicable to the private instruments endpoint.
    /// The quota is shared across the master account and all sub-accounts.
    /// </summary>
    [JsonProperty("shortPosRemainingQuota")]
    public decimal? ShortPositionRemainingQuota { get; set; }

    /// <summary>
    /// Initial price-limit band applied during the first ten minutes after listing.
    /// </summary>
    [JsonProperty("initPxLmtPct")]
    public decimal? InitialPriceLimitPercentage { get; set; }

    /// <summary>
    /// Floating price-limit band applied during normal trading.
    /// </summary>
    [JsonProperty("floatPxLmtPct")]
    public decimal? FloatingPriceLimitPercentage { get; set; }

    /// <summary>
    /// Maximum price-limit deviation cap.
    /// </summary>
    [JsonProperty("maxPxLmtPct")]
    public decimal? MaximumPriceLimitPercentage { get; set; }

    /// <summary>
    /// Minimum spacing between RPI bid and ask prices in organic price levels.
    /// Only returned by the public instruments endpoint.
    /// </summary>
    [JsonProperty("rpiMinLevel")]
    public int? RpiMinimumLevel { get; set; }

    /// <summary>
    /// Minimum distance from the opposite organic best price in basis points.
    /// Only returned by the public instruments endpoint.
    /// </summary>
    [JsonProperty("rpiMinPxBand")]
    public decimal? RpiMinimumPriceBand { get; set; }

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
