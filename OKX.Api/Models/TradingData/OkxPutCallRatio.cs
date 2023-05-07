namespace OKX.Api.Models.TradingData;

[JsonConverter(typeof(ArrayConverter))]
public class OkxPutCallRatio
{
    [ArrayProperty(0), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    [ArrayProperty(1)]
    public decimal OpenInterestRatio { get; set; }

    [ArrayProperty(2)]
    public decimal VolumeRatio { get; set; }
}
