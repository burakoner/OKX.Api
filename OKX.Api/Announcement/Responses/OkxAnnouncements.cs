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
