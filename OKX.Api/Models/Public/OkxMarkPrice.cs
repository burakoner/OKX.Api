namespace OKX.Api.Models.Public;

public class OkxMarkPrice
{
    [JsonProperty("instId")]
    public string Instrument { get; set; }

    [JsonProperty("instType"), JsonConverter(typeof(InstrumentTypeConverter))]
    public OkxInstrumentType InstrumentType { get; set; }

    [JsonProperty("markPx")]
    public decimal? MarkPrice { get; set; }

    [JsonProperty("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }
}
