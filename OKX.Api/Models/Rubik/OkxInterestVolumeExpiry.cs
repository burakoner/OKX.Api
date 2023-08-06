namespace OKX.Api.Models.Rubik;

[JsonConverter(typeof(ArrayConverter))]
public class OkxInterestVolumeExpiry
{
    [ArrayProperty(0)]
    public long Timestamp { get; set; }

    [JsonIgnore]
    public DateTime Time { get { return Timestamp.ConvertFromMilliseconds(); } }

    [ArrayProperty(1)]
    public long ExpiryTimestamp { get; set; }

    [JsonIgnore]
    public DateTime ExpiryTime { get { return ExpiryTimestamp.ConvertFromMilliseconds(); } }

    [ArrayProperty(2)]
    public decimal CallOpenInterest { get; set; }

    [ArrayProperty(3)]
    public decimal PutOpenInterest { get; set; }

    [ArrayProperty(4)]
    public decimal CallVolume { get; set; }

    [ArrayProperty(5)]
    public decimal PutVolume { get; set; }
}
