namespace OKX.Api.Models.Public;

public class OkxUnitConvert
{
    [JsonProperty("instId")]
    public string Instrument { get; set; }

    [JsonProperty("px")]
    public decimal Price { get; set; }

    [JsonProperty("sz")]
    public decimal Size { get; set; }

    [JsonProperty("type"), JsonConverter(typeof(ConvertTypeConverter))]
    public OkxConvertType Type { get; set; }

    [JsonProperty("unit"), JsonConverter(typeof(ConvertUnitConverter))]
    public OkxConvertUnit Unit { get; set; }
}
