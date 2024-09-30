namespace OKX.Api.Common;

/// <summary>
/// Download Link
/// </summary>
public record OkxDownloadLink
{
    /// <summary>
    /// Download file link
    /// </summary>
    [JsonProperty("fileHref")]
    public string DownloadLink { get; set; } = string.Empty;

    /// <summary>
    /// Download link generation time, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Download link generation time
    /// </summary>
    [JsonIgnore]
    public DateTime Time => Timestamp.ConvertFromMilliseconds();

    /// <summary>
    /// Download link status
    /// </summary>
    [JsonProperty("state")]
    public OkxDownloadLinkState State { get; set; }
}
