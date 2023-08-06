namespace OKX.Api.Models.Rubik;

[JsonConverter(typeof(ArrayConverter))]
public class OkxRatio
{
    [ArrayProperty(0)]
    public long Timestamp { get; set; }

    [JsonIgnore]
    public DateTime Time { get { return Timestamp.ConvertFromMilliseconds(); } }

    [ArrayProperty(1)]
    public decimal Ratio { get; set; }
}
