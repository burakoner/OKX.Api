namespace OKX.Api.Public;

/// <summary>
/// OKX Unit Convert
/// </summary>
public class OkxUnitConvert
{
    /// <summary>
    /// Type
    /// </summary>
    [JsonProperty("type"), JsonConverter(typeof(OkxConvertTypeConverter))]
    public OkxConvertType Type { get; set; }

    /// <summary>
    /// Instrument Id
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; }

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
    [JsonProperty("unit"), JsonConverter(typeof(OkxConvertUnitConverter))]
    public OkxConvertUnit Unit { get; set; }
}
