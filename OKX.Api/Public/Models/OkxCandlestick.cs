namespace OKX.Api.Public.Models;

/// <summary>
/// OKX Candlestick
/// </summary>
[JsonConverter(typeof(ArrayConverter))]
public class OkxCandlestick
{
    /// <summary>
    /// Instrument ID
    /// </summary>
    [JsonIgnore]
    public string InstrumentId { get; set; }

    /// <summary>
    /// Timestamp
    /// </summary>
    [ArrayProperty(0)]
    public long Timestamp { get; set; }

    /// <summary>
    /// Time
    /// </summary>
    [JsonIgnore]
    public DateTime Time => Timestamp.ConvertFromMilliseconds();

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

    /// <summary>
    /// Base volume
    /// </summary>
    [ArrayProperty(6)]
    public decimal BaseVolume { get; set; }

    /// <summary>
    /// Quote volume
    /// </summary>
    [ArrayProperty(7)]
    public decimal QuoteVolume { get; set; }

    /// <summary>
    /// Confirm
    /// </summary>
    [ArrayProperty(8), JsonConverter(typeof(OkxBooleanConverter))]
    public bool Confirm { get; set; }
}
