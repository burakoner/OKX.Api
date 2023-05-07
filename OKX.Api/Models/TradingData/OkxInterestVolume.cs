namespace OKX.Api.Models.TradingData;

[JsonConverter(typeof(ArrayConverter))]
public class OkxInterestVolume
{
    [ArrayProperty(0), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    [ArrayProperty(1)]
    public decimal OpenInterest { get; set; }

    [ArrayProperty(2)]
    public decimal Volume { get; set; }
}
