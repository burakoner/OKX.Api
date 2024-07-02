namespace OKX.Api.Rubik.Models;

/// <summary>
/// OKX Taker Volume
/// </summary>
[JsonConverter(typeof(ArrayConverter))]
public class OkxTakerVolume
{
    /// <summary>
    /// Currency
    /// </summary>
    [JsonIgnore]
    public string Currency { get; set; }

    /// <summary>
    /// Timestamp
    /// </summary>
    [ArrayProperty(0)]
    public long Timestamp { get; set; }

    /// <summary>
    /// Time
    /// </summary>
    [JsonIgnore]
    public DateTime Time { get { return Timestamp.ConvertFromMilliseconds(); } }

    /// <summary>
    /// Sell Volume
    /// </summary>
    [ArrayProperty(1)]
    public decimal SellVolume { get; set; }

    /// <summary>
    /// Buy Volume
    /// </summary>
    [ArrayProperty(2)]
    public decimal BuyVolume { get; set; }
}
