namespace OKX.Api.Common;

/// <summary>
/// OKX Timestamp
/// </summary>
public record OkxTimestamp
{
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
