namespace OKX.Api.Rubik.Models;

[JsonConverter(typeof(ArrayConverter))]
public class OkxPutCallRatio
{
    [ArrayProperty(0)]
    public long Timestamp { get; set; }

    [JsonIgnore]
    public DateTime Time { get { return Timestamp.ConvertFromMilliseconds(); } }

    [ArrayProperty(1)]
    public decimal OpenInterestRatio { get; set; }

    [ArrayProperty(2)]
    public decimal VolumeRatio { get; set; }
}
