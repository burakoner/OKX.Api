namespace OKX.Api.Public;

/// <summary>
/// OKX Estimated Settlement Information
/// </summary>
public record OkxPublicEstimatedSettlementInfo
{
    /// <summary>
    /// Instrument ID
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; } = string.Empty;

    /// <summary>
    /// Next settlement time, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("nextSettleTime")]
    public long? NextSettlementTimestamp { get; set; }

    /// <summary>
    /// Data return time
    /// </summary>
    [JsonIgnore]
    public DateTime? NextSettlementTime => NextSettlementTimestamp?.ConvertFromMilliseconds();

    /// <summary>
    /// Estimated settlement price
    /// </summary>
    [JsonProperty("estSettlePx")]
    public decimal EstimatedSettlementPrice { get; set; }

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
