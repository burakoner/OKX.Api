namespace OKX.Api.Models.Rubik;

[JsonConverter(typeof(ArrayConverter))]
public class OkxTakerVolume
{
    [JsonIgnore]
    public string Currency { get; set; }

    [ArrayProperty(0)]
    public long Timestamp { get; set; }

    [JsonIgnore]
    public DateTime Time { get { return Timestamp.ConvertFromMilliseconds(); } }

    [ArrayProperty(1)]
    public decimal SellVolume { get; set; }

    [ArrayProperty(2)]
    public decimal BuyVolume { get; set; }
}
