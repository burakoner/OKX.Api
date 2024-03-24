namespace OKX.Api.Rubik.Models;

[JsonConverter(typeof(ArrayConverter))]
public class OkxInterestVolumeStrike
{
    [ArrayProperty(0)]
    public long Timestamp { get; set; }

    [JsonIgnore]
    public DateTime Time { get { return Timestamp.ConvertFromMilliseconds(); } }

    [ArrayProperty(1)]
    public string Strike { get; set; }

    [ArrayProperty(2)]
    public decimal CallOpenInterest { get; set; }

    [ArrayProperty(3)]
    public decimal PutOpenInterest { get; set; }

    [ArrayProperty(4)]
    public decimal CallVolume { get; set; }

    [ArrayProperty(5)]
    public decimal PutVolume { get; set; }
}
