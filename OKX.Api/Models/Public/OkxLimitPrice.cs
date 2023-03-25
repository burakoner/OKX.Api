namespace OKX.Api.Models.Public;

public class OkxLimitPrice
{
    [JsonProperty("instId")]
    public string Instrument { get; set; }

    [JsonProperty("instType"), JsonConverter(typeof(InstrumentTypeConverter))]
    public OkxInstrumentType InstrumentType { get; set; }

    [JsonProperty("buyLmt")]
    public decimal BuyLimit { get; set; }

    [JsonProperty("sellLmt")]
    public decimal SellLimit { get; set; }

    [JsonProperty("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }
}
