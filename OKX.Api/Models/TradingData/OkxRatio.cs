namespace OKX.Api.Models.TradingData;

[JsonConverter(typeof(ArrayConverter))]
public class OkxRatio
{
    [ArrayProperty(0), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    [ArrayProperty(1)]
    public decimal Ratio { get; set; }
}
