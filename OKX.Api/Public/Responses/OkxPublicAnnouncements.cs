namespace OKX.Api.Public;

/// <summary>
/// OKX Announcement Wrapper
/// </summary>
public record OkxPublicAnnouncements
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
    public List<OkxPublicAnnouncement> Details { get; set; } = [];
}
