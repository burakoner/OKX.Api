namespace OKX.Api.Models.MarketData;

public class Okx24HourVolume
{
    [JsonProperty("volUsd")]
    public decimal VolumeUsd { get; set; }

    [JsonProperty("volCny")]
    public decimal VolumeCny { get; set; }

    [JsonProperty("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }
}
