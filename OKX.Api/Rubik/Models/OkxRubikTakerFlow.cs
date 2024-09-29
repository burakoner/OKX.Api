namespace OKX.Api.Rubik;

/// <summary>
/// OKX Taker Flow
/// </summary>
[JsonConverter(typeof(ArrayConverter))]
public class OkxRubikTakerFlow
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
    public DateTime Time { get { return Timestamp.ConvertFromMilliseconds(); } }

    /// <summary>
    /// Call Option Buy Volume
    /// </summary>
    [ArrayProperty(1)]
    public string CallOptionBuyVolume { get; set; }

    /// <summary>
    /// Call Option Sell Volume
    /// </summary>
    [ArrayProperty(2)]
    public string CallOptionSellVolume { get; set; }

    /// <summary>
    /// Put Option Buy Volume
    /// </summary>
    [ArrayProperty(3)]
    public string PutOptionBuyVolume { get; set; }

    /// <summary>
    /// Put Option Sell Volume
    /// </summary>
    [ArrayProperty(4)]
    public string PutOptionSellVolume { get; set; }

    /// <summary>
    /// Call Block Volume
    /// </summary>
    [ArrayProperty(5)]
    public decimal CallBlockVolume { get; set; }

    /// <summary>
    /// Put Block Volume
    /// </summary>
    [ArrayProperty(6)]
    public decimal PutBlockVolume { get; set; }
}
