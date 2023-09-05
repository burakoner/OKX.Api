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

    [JsonProperty("fundingTime")]
    public long FundingTimestamp { get; set; }

    [JsonIgnore]
    public DateTime FundingTime { get { return FundingTimestamp.ConvertFromMilliseconds(); } }

    [JsonProperty("nextFundingTime")]
    public long NextFundingTimestamp { get; set; }

    [JsonIgnore]
    public DateTime NextFundingTime { get { return NextFundingTimestamp.ConvertFromMilliseconds(); } }
}
