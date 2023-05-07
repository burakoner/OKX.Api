namespace OKX.Api.Models.TradingData;

[JsonConverter(typeof(ArrayConverter))]
public class OkxInterestVolumeExpiry
{
    [ArrayProperty(0), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    [ArrayProperty(1)]
    public string ExpiryTime { get; set; }

    [ArrayProperty(2)]
    public decimal CallOpenInterest { get; set; }

    [ArrayProperty(3)]
    public decimal PutOpenInterest { get; set; }

    [ArrayProperty(4)]
    public decimal CallVolume { get; set; }

    [ArrayProperty(5)]
    public decimal PutVolume { get; set; }
}
