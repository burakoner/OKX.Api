namespace OKX.Api.Public;

/// <summary>
/// OKX Announcement Type
/// </summary>
public record OkxPublicAnnouncementType
{
    /// <summary>
    /// Announcement type
    /// </summary>
    [JsonProperty("annType")]
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Announcement type description
    /// </summary>
    [JsonProperty("annTypeDesc")]
    public string Description { get; set; } = string.Empty;
}