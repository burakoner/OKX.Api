namespace OKX.Api.Models.PublicData;

public class OkxOpenInterest
{
    [JsonProperty("instType"), JsonConverter(typeof(InstrumentTypeConverter))]
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
