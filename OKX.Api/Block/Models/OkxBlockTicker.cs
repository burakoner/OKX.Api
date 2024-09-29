namespace OKX.Api.Block;

/// <summary>
/// OKX Block Ticker
/// </summary>
public class OkxBlockTicker
{
    /// <summary>
    /// Instrument ID
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; } = "";

    /// <summary>
    /// Instrument Type
    /// </summary>
    [JsonProperty("instType"), JsonConverter(typeof(OkxInstrumentTypeConverter))]
    public OkxInstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Base Volume
    /// </summary>
    [JsonProperty("volCcy24h")]
    public decimal VolumeCurrency { get; set; }

    /// <summary>
    /// Quote Volume
    /// </summary>
    [JsonProperty("vol24h")]
    public decimal Volume { get; set; }

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Time
    /// </summary>
    [JsonIgnore]
    public DateTime Time => Timestamp.ConvertFromMilliseconds();
}
