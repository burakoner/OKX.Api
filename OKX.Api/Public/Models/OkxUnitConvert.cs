using OKX.Api.Public.Converters;
using OKX.Api.Public.Enums;

namespace OKX.Api.Public.Models;

public class OkxUnitConvert
{
    [JsonProperty("type"), JsonConverter(typeof(OkxConvertTypeConverter))]
    public OkxConvertType Type { get; set; }

    [JsonProperty("instId")]
    public string Instrument { get; set; }

    [JsonProperty("px")]
    public decimal Price { get; set; }

    [JsonProperty("sz")]
    public decimal Size { get; set; }

    [JsonProperty("unit"), JsonConverter(typeof(OkxConvertUnitConverter))]
    public OkxConvertUnit Unit { get; set; }
}
