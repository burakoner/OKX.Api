namespace OKX.Api.Public;

/// <summary>
/// OKX Estimated Price
/// </summary>
public record OkxPublicEstimatedPrice
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
    /// Type
    /// </summary>
    [JsonProperty("settleType")]
    public OkxPublicSettlementType? SettlementType { get; set; }

    /// <summary>
    /// Estimated delivery/exercise/settlement price.
    /// When available, the value is derived from index price samples recorded during the last 30 minutes before the
    /// relevant expiry event.
    /// </summary>
    [JsonProperty("settlePx")]
    public decimal EstimatedPrice { get; set; }

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
