namespace OKX.Api.Spread;

/// <summary>
/// OKX Spread Order Book
/// </summary>
public record OkxSpreadOrderBook
{
    /// <summary>
    /// Spread ID for websocket updates.
    /// </summary>
    [JsonIgnore]
    public string SpreadId { get; set; } = string.Empty;

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
    public DateTime Time => Timestamp.ConvertFromMilliseconds();

    /// <summary>
    /// Update action for websocket order book streams.
    /// </summary>
    [JsonIgnore]
    public string Action { get; set; } = string.Empty;

    /// <summary>
    /// Checksum value for websocket order book streams.
    /// </summary>
    [JsonProperty("checksum")]
    [JsonConverter(typeof(LongAsStringNullableConverter))]
    public long? Checksum { get; set; }

    /// <summary>
    /// Previous sequence ID for websocket order book streams.
    /// </summary>
    [JsonProperty("prevSeqId")]
    [JsonConverter(typeof(LongAsStringNullableConverter))]
    public long? PreviousSequenceId { get; set; }

    /// <summary>
    /// Sequence ID for websocket order book streams.
    /// </summary>
    [JsonProperty("seqId")]
    [JsonConverter(typeof(LongAsStringNullableConverter))]
    public long? SequenceId { get; set; }
}
