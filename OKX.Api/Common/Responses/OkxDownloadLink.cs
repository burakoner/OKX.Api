namespace OKX.Api.Common;

/// <summary>
/// Download Link
/// </summary>
public record OkxDownloadLink
{
    /// <summary>
    /// Download file link
    /// </summary>
    [JsonProperty("fileHref")]
    public string DownloadLink { get; set; } = string.Empty;

    /// <summary>
    /// Data range begin time, Unix timestamp format in milliseconds.
    /// </summary>
    [JsonProperty("beginTime"), JsonConverter(typeof(LongAsStringNullableConverter))]
    public long? BeginTimestamp { get; set; }

    /// <summary>
    /// Data range begin time.
    /// </summary>
    [JsonIgnore]
    public DateTime? BeginTime => BeginTimestamp?.ConvertFromMilliseconds();

    /// <summary>
    /// Data range end time, Unix timestamp format in milliseconds.
    /// </summary>
    [JsonProperty("endTime"), JsonConverter(typeof(LongAsStringNullableConverter))]
    public long? EndTimestamp { get; set; }

    /// <summary>
    /// Data range end time.
    /// </summary>
    [JsonIgnore]
    public DateTime? EndTime => EndTimestamp?.ConvertFromMilliseconds();

    /// <summary>
    /// The first request time for generating the download link, Unix timestamp format in milliseconds.
    /// </summary>
    [JsonProperty("cTime"), JsonConverter(typeof(LongAsStringNullableConverter))]
    public long? CreateTimestamp { get; set; }

    /// <summary>
    /// The first request time for generating the download link.
    /// </summary>
    [JsonIgnore]
    public DateTime? CreateTime => CreateTimestamp?.ConvertFromMilliseconds();

    /// <summary>
    /// Server-reported timestamp for the download request, Unix timestamp format in milliseconds, e.g. 1597026383085.
    /// Its precise lifecycle meaning is defined by the endpoint returning this shared model.
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Server-reported time for the download request.
    /// </summary>
    [JsonIgnore]
    public DateTime Time => Timestamp.ConvertFromMilliseconds();

    /// <summary>
    /// Download link status
    /// </summary>
    [JsonProperty("state")]
    public OkxDownloadLinkState State { get; set; }
}
