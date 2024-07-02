namespace OKX.Api.Public.Models;

/// <summary>
/// OKX Volume
/// </summary>
public class OkxVolume
{
    /// <summary>
    /// Volume in USD
    /// </summary>
    [JsonProperty("volUsd")]
    public decimal VolumeUsd { get; set; }

    /// <summary>
    /// Volume in CNY
    /// </summary>
    [JsonProperty("volCny")]
    public decimal VolumeCny { get; set; }

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Time
    /// </summary>
    [JsonIgnore]
    public DateTime Time { get { return Timestamp.ConvertFromMilliseconds(); } }
}
