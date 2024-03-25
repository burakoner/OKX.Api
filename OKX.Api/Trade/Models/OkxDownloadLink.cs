using OKX.Api.Trade.Converters;
using OKX.Api.Trade.Enums;

namespace OKX.Api.Trade.Models;

/// <summary>
/// Download Link
/// </summary>
public class OkxDownloadLink
{
    /// <summary>
    /// Download file link
    /// </summary>
    [JsonProperty("fileHref")]
    public string DownloadLink { get; set; }
    
    /// <summary>
    /// Download link generation time, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Download link generation time
    /// </summary>
    [JsonIgnore]
    public DateTime Time { get { return Timestamp.ConvertFromMilliseconds(); } }

    /// <summary>
    /// Download link status
    /// </summary>
    [JsonProperty("state"), JsonConverter(typeof(OkxDownloadLinkStateConverter))]
    public OkxDownloadLinkState State { get; set; }
}
