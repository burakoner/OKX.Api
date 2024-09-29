namespace OKX.Api.Public;

/// <summary>
/// OKX Order Book
/// </summary>
public class OkxPublicOrderBookStream
{
    /// <summary>
    /// Instrument Id
    /// </summary>
    public string InstrumentId { get; set; }

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

    /// <summary>
    /// Action
    /// </summary>
    [JsonProperty("action")]
    public string Action { get; set; } = "";

    /// <summary>
    /// Checksum
    /// </summary>
    [JsonProperty("checksum")]
    public long? Checksum { get; set; }

    /// <summary>
    /// Previous Sequence Id
    /// </summary>
    [JsonProperty("prevSeqId")]
    public long? PreviousSequenceId { get; set; }

    /// <summary>
    /// Sequence Id
    /// </summary>
    [JsonProperty("seqId")]
    public long? SequenceId { get; set; }
}
