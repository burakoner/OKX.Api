namespace OKX.Api.Common;

/// <summary>
/// OKX Download Link Application
/// </summary>
public record OkxDownloadApplication
{
    /// <summary>
    /// Whether there is already a download link for this section
    /// true: Existed, can check from "Get bills details (since 2021)".
    /// false: Does not exist and is generating, can check the download link after 30 hours
    /// </summary>
    [JsonProperty("result")]
    public bool Result { get; set; }

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
}
