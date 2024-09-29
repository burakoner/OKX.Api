namespace OKX.Api.Public;

/// <summary>
/// OKX Index Ticker
/// </summary>
public class OkxIndexTicker
{
    /// <summary>
    /// Instrument Id
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; }

    /// <summary>
    /// Index Price
    /// </summary>
    [JsonProperty("idxPx")]
    public decimal IndexPrice { get; set; }

    /// <summary>
    /// High 24h
    /// </summary>
    [JsonProperty("high24h")]
    public decimal High { get; set; }

    /// <summary>
    /// Low 24h
    /// </summary>
    [JsonProperty("low24h")]
    public decimal Low { get; set; }

    /// <summary>
    /// Open 24h
    /// </summary>
    [JsonProperty("open24h")]
    public decimal Open { get; set; }

    /// <summary>
    /// Open Price UTC 0
    /// </summary>
    [JsonProperty("sodUtc0")]
    public decimal OpenPriceUtc0 { get; set; }

    /// <summary>
    /// Open Price UTC 8
    /// </summary>
    [JsonProperty("sodUtc8")]
    public decimal OpenPriceUtc8 { get; set; }

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Time
    /// </summary>
    [JsonIgnore]
    public DateTime Time { get { return Timestamp.ConvertFromMilliseconds(); } }
}
