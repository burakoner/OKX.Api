namespace OKX.Api.Public;

/// <summary>
/// OKX Announcement
/// </summary>
public record OkxPublicAnnouncement
{
    /// <summary>
    /// Announcement title
    /// </summary>
    [JsonProperty("title")]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Announcement type
    /// </summary>
    [JsonProperty("annType")]
    public string AnnouncementType { get; set; } = string.Empty;

    /// <summary>
    /// The time displayed on the announcement page for user reference. Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("businessPTime")]
    public long AnnouncementTimestamp { get; set; }

    /// <summary>
    /// The time displayed on the announcement page for user reference. Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonIgnore]
    public DateTime AnnouncementTime => AnnouncementTimestamp.ConvertFromMilliseconds();

    /// <summary>
    /// The first actual publish time, Unix timestamp format in milliseconds, e.g. 1750228261
    /// </summary>
    [JsonProperty("pTime")]
    public long PublishTimestamp { get; set; }

    /// <summary>
    /// The first actual publish time, Unix timestamp format in milliseconds, e.g. 1750228261
    /// </summary>
    [JsonIgnore]
    public DateTime PublishTime => PublishTimestamp.ConvertFromMilliseconds();

    /// <summary>
    /// Announcement url
    /// </summary>
    [JsonProperty("url")]
    public string Url { get; set; } = string.Empty;
}
