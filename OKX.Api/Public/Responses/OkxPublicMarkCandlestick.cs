﻿namespace OKX.Api.Public;

/// <summary>
/// OKX Mark Candlestick
/// </summary>
[JsonConverter(typeof(ArrayConverter))]
public record OkxPublicMarkCandlestick
{
    /// <summary>
    /// Instrument Id
    /// </summary>
    [JsonIgnore]
    public string InstrumentId { get; set; } = string.Empty;

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
    /// Confirm
    /// </summary>
    [ArrayProperty(5), JsonConverter(typeof(OkxBooleanConverter))]
    public bool Confirm { get; set; }
}
