namespace OKX.Api.Public;

/// <summary>
/// OKX Order Book
/// </summary>
public class OkxOrderBook
{
    /// <summary>
    /// Instrument ID
    /// </summary>
    [JsonIgnore]
    public string InstrumentId { get; set; }
    
    /// <summary>
    /// Asks
    /// </summary>
    [JsonProperty("asks")]
    public List<OkxOrderBookRow> Asks { get; set; } = [];

    /// <summary>
    /// Bids
    /// </summary>
    [JsonProperty("bids")]
    public List<OkxOrderBookRow> Bids { get; set; } = [];

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
