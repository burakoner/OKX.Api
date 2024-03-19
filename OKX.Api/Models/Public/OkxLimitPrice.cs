namespace OKX.Api.Models.Public;

public class OkxLimitPrice
{
    [JsonProperty("instType"), JsonConverter(typeof(InstrumentTypeConverter))]
    public OkxInstrumentType InstrumentType { get; set; }

    [JsonProperty("instId")]
    public string Instrument { get; set; }

    [JsonProperty("buyLmt")]
    public decimal BuyLimit { get; set; }

    [JsonProperty("sellLmt")]
    public decimal SellLimit { get; set; }

    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    [JsonIgnore]
    public DateTime Time { get { return Timestamp.ConvertFromMilliseconds(); } }
}
