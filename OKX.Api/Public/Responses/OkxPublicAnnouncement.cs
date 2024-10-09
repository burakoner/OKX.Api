namespace OKX.Api.Public;

/// <summary>
/// OKX Announcement
/// </summary>
public record OkxPublicAnnouncement
{
    /// <summary>
    /// Announcement type
    /// </summary>
    [JsonProperty("annType")]
    public string AnnouncementType { get; set; } = string.Empty;

    /// <summary>
    /// Announcement title
    /// </summary>
    [JsonProperty("title")]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// System maintenance status
    /// </summary>
    [JsonProperty("state")]
    public OkxPublicMaintenanceState Status { get; set; }

    /// <summary>
    /// Publish time. Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("pTime")]
    public long PublishTimestamp { get; set; }

    /// <summary>
    /// Publish time. Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonIgnore]
    public DateTime PublishTime => PublishTimestamp.ConvertFromMilliseconds();

    /// <summary>
    /// Announcement url
    /// </summary>
    [JsonProperty("url")]
    public string Url { get; set; } = string.Empty;
}
