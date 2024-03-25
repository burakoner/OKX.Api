using OKX.Api.Common.Converters;
using OKX.Api.Common.Enums;

namespace OKX.Api.Public.Models;

public class OkxFundingRate
{
    [JsonProperty("instType"), JsonConverter(typeof(OkxInstrumentTypeConverter))]
    public OkxInstrumentType InstrumentType { get; set; }

    [JsonProperty("instId")]
    public string InstrumentId { get; set; }

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
