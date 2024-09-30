namespace OKX.Api.Public;

/// <summary>
/// OKX Unit Convert
/// </summary>
public class OkxPublicUnitConvert
{
    /// <summary>
    /// Type
    /// </summary>
    [JsonProperty("type")]
    public OkxPublicConvertType Type { get; set; }

    /// <summary>
    /// Instrument Id
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; } = string.Empty;

    /// <summary>
    /// Price
    /// </summary>
    [JsonProperty("px")]
    public decimal Price { get; set; }

    /// <summary>
    /// Size
    /// </summary>
    [JsonProperty("sz")]
    public decimal Size { get; set; }

    /// <summary>
    /// Unit
    /// </summary>
    [JsonProperty("unit")]
    public OkxPublicConvertUnit Unit { get; set; }
}
