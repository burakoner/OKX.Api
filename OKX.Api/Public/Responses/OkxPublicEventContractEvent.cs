namespace OKX.Api.Public;

/// <summary>
/// Event contract event.
/// </summary>
public record OkxPublicEventContractEvent
{
    /// <summary>
    /// Series ID.
    /// </summary>
    [JsonProperty("seriesId")]
    public string SeriesId { get; set; } = string.Empty;

    /// <summary>
    /// Event ID.
    /// </summary>
    [JsonProperty("eventId")]
    public string EventId { get; set; } = string.Empty;

    /// <summary>
    /// Fixing timestamp, if applicable.
    /// </summary>
    [JsonProperty("fixTime")]
    [JsonConverter(typeof(LongAsStringNullableConverter))]
    public long? FixingTimestamp { get; set; }

    /// <summary>
    /// Fixing time, if applicable.
    /// </summary>
    [JsonIgnore]
    public DateTime? FixingTime => FixingTimestamp?.ConvertFromMilliseconds();

    /// <summary>
    /// Event expiry timestamp.
    /// </summary>
    [JsonProperty("expTime")]
    [JsonConverter(typeof(LongAsStringNullableConverter))]
    public long? ExpiryTimestamp { get; set; }

    /// <summary>
    /// Event expiry time.
    /// </summary>
    [JsonIgnore]
    public DateTime? ExpiryTime => ExpiryTimestamp?.ConvertFromMilliseconds();

    /// <summary>
    /// Event state.
    /// </summary>
    [JsonProperty("state")]
    public OkxInstrumentState State { get; set; }
}
