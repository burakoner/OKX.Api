namespace OKX.Api.Models.Rubik;

[JsonConverter(typeof(ArrayConverter))]
public class OkxInterestVolume
{
    [ArrayProperty(0)]
    public long Timestamp { get; set; }

    [JsonIgnore]
    public DateTime Time { get { return Timestamp.ConvertFromMilliseconds(); } }

    [ArrayProperty(1)]
    public decimal OpenInterest { get; set; }

    [ArrayProperty(2)]
    public decimal Volume { get; set; }
}
