namespace OKX.Api.Spread;

/// <summary>
/// OKX Spread Order Book
/// </summary>
public class OkxSpreadOrderBook
{
    /// <summary>
    /// Asks
    /// </summary>
    [JsonProperty("asks")]
    public List<OkxSpreadOrderBookRow> Asks { get; set; } = [];

    /// <summary>
    /// Bids
    /// </summary>
    [JsonProperty("bids")]
    public List<OkxSpreadOrderBookRow> Bids { get; set; } = [];

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
