namespace OKX.Api.Spread;

/// <summary>
/// OKX Candlestick
/// </summary>
[JsonConverter(typeof(ArrayConverter))]
public record OkxSpreadCandlestick
{
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
    /// Volume
    /// </summary>
    [ArrayProperty(5)]
    public decimal Volume { get; set; }

    /// <summary>
    /// Confirm
    /// </summary>
    [ArrayProperty(6), JsonConverter(typeof(OkxBooleanConverter))]
    public bool Confirm { get; set; }
}
