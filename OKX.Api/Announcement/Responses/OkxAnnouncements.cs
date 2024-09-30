namespace OKX.Api.Announcement;

/// <summary>
/// OKX Announcement Wrapper
/// </summary>
public record OkxAnnouncements
{
    /// <summary>
    /// Total number of pages
    /// </summary>
    [JsonProperty("totalPage")]
    public int PageCount { get; set; }

    /// <summary>
    /// List of announcements
    /// </summary>
    [JsonProperty("details")]
    public List<OkxAnnouncement> Details { get; set; } = [];
}

/// <summary>
/// OKX Announcement
/// </summary>
public record OkxAnnouncement
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
    public OkxStatusMaintenanceState Status { get; set; }

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
