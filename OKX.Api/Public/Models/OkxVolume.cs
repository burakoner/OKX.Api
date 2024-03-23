namespace OKX.Api.Public.Models;

public class OkxVolume
{
    [JsonProperty("volUsd")]
    public decimal VolumeUsd { get; set; }

    [JsonProperty("volCny")]
    public decimal VolumeCny { get; set; }

    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    [JsonIgnore]
    public DateTime Time { get { return Timestamp.ConvertFromMilliseconds(); } }
}
