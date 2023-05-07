namespace OKX.Api.Models.PublicData;

public class OkxFundingRateHistory
{
    [JsonProperty("instType"), JsonConverter(typeof(InstrumentTypeConverter))]
    public OkxInstrumentType InstrumentType { get; set; }

    [JsonProperty("instId")]
    public string Instrument { get; set; }

    [JsonProperty("fundingRate")]
    public decimal FundingRate { get; set; }

    [JsonProperty("realizedRate")]
    public decimal RealizedRate { get; set; }

    [JsonProperty("fundingTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime FundingTime { get; set; }
}
