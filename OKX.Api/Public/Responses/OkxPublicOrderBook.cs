namespace OKX.Api.Public;

/// <summary>
/// OKX Order Book
/// </summary>
public record OkxPublicOrderBook
{
    /// <summary>
    /// Instrument ID
    /// </summary>
    [JsonIgnore]
    public string InstrumentId { get; set; } = string.Empty;
    
    /// <summary>
    /// Asks
    /// </summary>
    [JsonProperty("asks")]
    public List<OkxPublicOrderBookRow> Asks { get; set; } = [];

    /// <summary>
    /// Bids
    /// </summary>
    [JsonProperty("bids")]
    public List<OkxPublicOrderBookRow> Bids { get; set; } = [];

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
