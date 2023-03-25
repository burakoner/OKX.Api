namespace OKX.Api.Models.Public;

public class OkxFundingRate
{
    [JsonProperty("instId")]
    public string Instrument { get; set; }

    [JsonProperty("instType"), JsonConverter(typeof(InstrumentTypeConverter))]
    public OkxInstrumentType InstrumentType { get; set; }

    [JsonProperty("fundingTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime FundingTime { get; set; }

    [JsonProperty("fundingRate")]
    public decimal FundingRate { get; set; }

    [JsonProperty("nextFundingTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime NextFundingTime { get; set; }

    [JsonProperty("nextFundingRate")]
    public decimal NextFundingRate { get; set; }
}
