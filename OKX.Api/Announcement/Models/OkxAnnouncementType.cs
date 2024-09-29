namespace OKX.Api.Announcement;

/// <summary>
/// OKX Announcement Type
/// </summary>
public class OkxAnnouncementType
{
    /// <summary>
    /// Announcement type
    /// </summary>
    [JsonProperty("annType")]
    public string Type { get; set; }

    /// <summary>
    /// Announcement type description
    /// </summary>
    [JsonProperty("annTypeDesc")]
    public string Description { get; set; }
}