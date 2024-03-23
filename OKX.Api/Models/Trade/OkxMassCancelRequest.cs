namespace OKX.Api.Models.Trade;

public class OkxMassCancelRequest
{
    [JsonProperty("instType"), JsonConverter(typeof(OkxInstrumentTypeConverter))]
    public OkxInstrumentType InstrumentType { get; set; }

    [JsonProperty("instFamily")]
    public string InstrumentFamily { get; set; }
}