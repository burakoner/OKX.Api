namespace OKX.Api.Public;

/// <summary>
/// OKX Order Book
/// </summary>
public record OkxPublicOrderBookStream
{
    /// <summary>
    /// Instrument Id
    /// </summary>
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

    /// <summary>
    /// Action
    /// </summary>
    [JsonProperty("action")]
    public string Action { get; set; } = string.Empty;

    /// <summary>
    /// Deprecated checksum. For books, books-l2-tbt, and books50-l2-tbt this field remains present but is fixed to zero.
    /// It is absent from books5, bbo-tbt, books-elp, and books-rpi. Use sequence IDs to verify continuity.
    /// </summary>
    [Obsolete("OKX no longer supports checksum validation. Use PreviousSequenceId and SequenceId.")]
    [JsonProperty("checksum")]
    public long? Checksum { get; set; }

    /// <summary>
    /// Sequence ID of the last sent message for incremental order book channels.
    /// </summary>
    [JsonProperty("prevSeqId")]
    public long? PreviousSequenceId { get; set; }

    /// <summary>
    /// Sequence ID of the current message.
    /// </summary>
    [JsonProperty("seqId")]
    public long? SequenceId { get; set; }
}
