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
    /// Underlying, e.g. BTC-USD. Only applicable to FUTURES/SWAP/OPTION
    /// </summary>
    [JsonProperty("uly")]
    public string Underlying { get; set; } = string.Empty;

    /// <summary>
    /// Instrument family
    /// </summary>
    [JsonProperty("instFamily")]
    public string InstrumentFamily { get; set; } = string.Empty;

    /// <summary>
    /// Base currency
    /// </summary>
    [JsonProperty("baseCcy")]
    public string BaseCurrency { get; set; } = string.Empty;

    /// <summary>
    /// Quote currency
    /// </summary>
    [JsonProperty("quoteCcy")]
    public string QuoteCurrency { get; set; } = string.Empty;

    /// <summary>
    /// Settlement currency
    /// </summary>
    [JsonProperty("settleCcy")]
    public string SettlementCurrency { get; set; } = string.Empty;

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
    public string ContractValueCurrency { get; set; } = string.Empty;

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
    /// The end time of call auction, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// Only applicable to SPOT that are listed through call auctions, return "" in other cases
    /// </summary>
    [JsonProperty("auctionEndTime")]
    public long? AuctionEndTimestamp { get; set; }

    /// <summary>
    /// The end time of call auction, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// Only applicable to SPOT that are listed through call auctions, return "" in other cases
    /// </summary>
    [JsonIgnore]
    public DateTime? AuctionEndTime => AuctionEndTimestamp?.ConvertFromMilliseconds();

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
    public decimal TickSize { get; set; }

    /// <summary>
    /// Lot size
    /// </summary>
    [JsonProperty("lotSz")]
    public decimal LotSize { get; set; }

    /// <summary>
    /// Minimal order size
    /// </summary>
    [JsonProperty("minSz")]
    public decimal MinimumOrderSize { get; set; }

    /// <summary>
    /// Contract type
    /// </summary>
    [JsonProperty("ctType")]
    public OkxContractType? ContractType { get; set; }

    /// <summary>
    /// State
    /// </summary>
    [JsonProperty("state")]
    public OkxInstrumentState State { get; set; }

    /// <summary>
    /// Trading rule types
    /// </summary>
    [JsonProperty("ruleType")]
    public OkxInstrumentRuleType RuleType { get; set; }

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
}