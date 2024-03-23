namespace OKX.Api.Public.Models;

public class OkxOpenInterest
{
    [JsonProperty("instType"), JsonConverter(typeof(OkxInstrumentTypeConverter))]
    public OkxInstrumentType InstrumentType { get; set; }

    [JsonProperty("instId")]
    public string Instrument { get; set; }

    [JsonProperty("oi")]
    public decimal? OpenInterestCont { get; set; }

    [JsonProperty("oiCcy")]
    public decimal? OpenInterestCoin { get; set; }

    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    [JsonIgnore]
    public DateTime Time { get { return Timestamp.ConvertFromMilliseconds(); } }
}
