namespace OKX.Api.Public;

/// <summary>
/// OKX Funding Rate
/// </summary>
public record OkxPublicFundingRate
{
    /// <summary>
    /// Instrument Type
    /// </summary>
    [JsonProperty("instType")]
    public OkxInstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Instrument ID
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; } = string.Empty;
    
    /// <summary>
    /// Funding rate mechanism
    /// current_period
    /// next_period
    /// </summary>
    [JsonProperty("method")]
    public OkxPublicFundingRateMethod Method { get; set; }

    /// <summary>
    /// Funding Rate
    /// </summary>
    [JsonProperty("fundingRate")]
    public decimal FundingRate { get; set; }

    /// <summary>
    /// Next Funding Rate
    /// </summary>
    [JsonProperty("nextFundingRate")]
    public decimal? NextFundingRate { get; set; }

    /// <summary>
    /// Funding Time
    /// </summary>
    [JsonProperty("fundingTime")]
    public long FundingTimestamp { get; set; }

    /// <summary>
    /// Funding Time
    /// </summary>
    [JsonIgnore]
    public DateTime FundingTime => FundingTimestamp.ConvertFromMilliseconds();

    /// <summary>
    /// Next Funding Time
    /// </summary>
    [JsonProperty("nextFundingTime")]
    public long NextFundingTimestamp { get; set; }

    /// <summary>
    /// Next Funding Time
    /// </summary>
    [JsonIgnore]
    public DateTime NextFundingTime => NextFundingTimestamp.ConvertFromMilliseconds();

    /// <summary>
    /// The lower limit of the predicted funding rate of the next cycle
    /// </summary>
    [JsonProperty("minFundingRate")]
    public decimal MinimumFundingRate	 { get; set; }

    /// <summary>
    /// The upper limit of the predicted funding rate of the next cycle
    /// </summary>
    [JsonProperty("maxFundingRate")]
    public decimal MaximumFundingRate { get; set; }
    
    /// <summary>
    /// Settlement state of funding rate
    /// processing
    /// settled
    /// </summary>
    [JsonProperty("settState")]
    public OkxPublicSettlementState SettlementState { get; set; }

    /// <summary>
    /// If settState = processing, it is the funding rate that is being used for current settlement cycle.
    /// If settState = settled, it is the funding rate that is being used for previous settlement cycle
    /// </summary>
    [JsonProperty("settFundingRate")]
    public decimal SettlementFundingRate { get; set; }
    
    /// <summary>
    /// Premium between the mid price of perps market and the index price
    /// </summary>
    [JsonProperty("premium")]
    public decimal premium { get; set; }

    /// <summary>
    /// Data return time, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Data return time, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonIgnore]
    public DateTime Time => Timestamp.ConvertFromMilliseconds();
}
