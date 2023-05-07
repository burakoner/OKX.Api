namespace OKX.Api.Models.PublicData;

public class OkxFundingRate
{
    [JsonProperty("instType"), JsonConverter(typeof(InstrumentTypeConverter))]
    public OkxInstrumentType InstrumentType { get; set; }

    [JsonProperty("instId")]
    public string Instrument { get; set; }

    [JsonProperty("fundingRate")]
    public decimal FundingRate { get; set; }

    [JsonProperty("nextFundingRate")]
    public decimal NextFundingRate { get; set; }

    [JsonProperty("fundingTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime FundingTime { get; set; }

    [JsonProperty("nextFundingTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime NextFundingTime { get; set; }
}
