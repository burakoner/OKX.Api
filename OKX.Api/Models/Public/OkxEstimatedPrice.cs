namespace OKX.Api.Models.Public;

public class OkxEstimatedPrice
{
    [JsonProperty("instId")]
    public string Instrument { get; set; }

    [JsonProperty("instType"), JsonConverter(typeof(InstrumentTypeConverter))]
    public OkxInstrumentType InstrumentType { get; set; }

    [JsonProperty("settlePx")]
    public decimal EstimatedPrice { get; set; }

    [JsonProperty("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }
}
