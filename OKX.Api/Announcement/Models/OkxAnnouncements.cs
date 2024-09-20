using OKX.Api.Status.Converters;
using OKX.Api.Status.Enums;

namespace OKX.Api.Announcement.Models;

/// <summary>
/// OKX Announcement Wrapper
/// </summary>
public class OkxAnnouncements
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
    public List<OkxAnnouncement> Details { get; set; }=[];
}

/// <summary>
/// OKX Announcement
/// </summary>
public class OkxAnnouncement
{
    /// <summary>
    /// Announcement type
    /// </summary>
    [JsonProperty("annType")]
    public string AnnouncementType { get; set; }

    /// <summary>
    /// Announcement title
    /// </summary>
    [JsonProperty("title")]
    public string Title { get; set; }

    /// <summary>
    /// System maintenance status
    /// </summary>
    [JsonProperty("state"), JsonConverter(typeof(OkxStatusMaintenanceStateConverter))]
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
    public DateTime PublishTime { get { return PublishTimestamp.ConvertFromMilliseconds(); } }
    
    /// <summary>
    /// Announcement url
    /// </summary>
    [JsonProperty("url")]
    public string Url { get; set; }
}
