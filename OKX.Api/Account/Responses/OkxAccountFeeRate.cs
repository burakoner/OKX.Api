namespace OKX.Api.Account;

/// <summary>
/// OkxFeeRate
/// </summary>
public record OkxAccountFeeRate
{
    /// <summary>
    /// Fee rate Level
    /// </summary>
    [JsonProperty("level")]
    public string Level { get; set; } = string.Empty;

    /// <summary>
    /// Fee groups.
    /// Applicable to SPOT/MARGIN/SWAP/FUTURES/OPTION
    /// </summary>
    [JsonProperty("feeGroup")]
    public List<OkxAccountFeeRateGroup> FeeGroup { get; set; } = [];

    /// <summary>
    /// Delivery fee rate
    /// </summary>
    [JsonProperty("delivery")]
    public decimal? Delivery { get; set; }

    /// <summary>
    /// Fee rate for exercising the option
    /// </summary>
    [JsonProperty("exercise")]
    public decimal? Exercise { get; set; }

    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonProperty("instType")]
    public OkxInstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Data return time, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Data return time
    /// </summary>
    [JsonIgnore]
    public DateTime Time => Timestamp.ConvertFromMilliseconds();
}

/// <summary>
/// OkxAccountFeeRateGroup
/// </summary>
public record OkxAccountFeeRateGroup
{
    /// <summary>
    /// Fiat currency
    /// </summary>
    [JsonProperty("groupId")]
    public string GroupId { get; set; } = string.Empty;
    ///             
    /// <summary>
    /// Taker fee rate
    /// </summary>
    [JsonProperty("taker")]
    public decimal Taker { get; set; }
    ///             
    /// <summary>
    /// Maker fee rate
    /// </summary>
    [JsonProperty("maker")]
    public decimal Maker { get; set; }
}
