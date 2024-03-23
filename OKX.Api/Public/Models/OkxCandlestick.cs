namespace OKX.Api.Public.Models;

[JsonConverter(typeof(ArrayConverter))]
public class OkxCandlestick
{
    [JsonIgnore]
    public string Instrument { get; set; }

    [ArrayProperty(0)]
    public long Timestamp { get; set; }

    [JsonIgnore]
    public DateTime Time { get { return Timestamp.ConvertFromMilliseconds(); } }

    /// <summary>
    /// Open price
    /// </summary>
    [ArrayProperty(1)]
    public decimal Open { get; set; }

    /// <summary>
    /// Highest price
    /// </summary>
    [ArrayProperty(2)]
    public decimal High { get; set; }

    /// <summary>
    /// Lowest price
    /// </summary>
    [ArrayProperty(3)]
    public decimal Low { get; set; }

    /// <summary>
    /// Close price
    /// </summary>
    [ArrayProperty(4)]
    public decimal Close { get; set; }

    /// <summary>
    /// Trading volume
    /// </summary>
    [ArrayProperty(5)]
    public decimal TradingVolume { get; set; }

    [ArrayProperty(6)]
    public decimal BaseVolume { get; set; }

    [ArrayProperty(7)]
    public decimal QuoteVolume { get; set; }

    [ArrayProperty(8), JsonConverter(typeof(OkxBooleanConverter))]
    public bool Confirm { get; set; }
}
