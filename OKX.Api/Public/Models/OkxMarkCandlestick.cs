using OKX.Api.Common.Converters;

namespace OKX.Api.Public.Models;

[JsonConverter(typeof(ArrayConverter))]
public class OkxMarkCandlestick
{
    [JsonIgnore]
    public string InstrumentId { get; set; }

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

    [ArrayProperty(5), JsonConverter(typeof(OkxBooleanConverter))]
    public bool Confirm { get; set; }
}
