namespace OKX.Api.Rubik.Models;

[JsonConverter(typeof(ArrayConverter))]
public class OkxTakerFlow
{
    [ArrayProperty(0)]
    public long Timestamp { get; set; }

    [JsonIgnore]
    public DateTime Time { get { return Timestamp.ConvertFromMilliseconds(); } }

    [ArrayProperty(1)]
    public string CallOptionBuyVolume { get; set; }

    [ArrayProperty(2)]
    public string CallOptionSellVolume { get; set; }

    [ArrayProperty(3)]
    public string PutOptionBuyVolume { get; set; }

    [ArrayProperty(4)]
    public string PutOptionSellVolume { get; set; }

    [ArrayProperty(5)]
    public decimal CallBlockVolume { get; set; }

    [ArrayProperty(6)]
    public decimal PutBlockVolume { get; set; }
}
