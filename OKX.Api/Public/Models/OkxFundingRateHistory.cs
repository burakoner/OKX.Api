using OKX.Api.Common.Converters;
using OKX.Api.Common.Enums;

namespace OKX.Api.Public.Models;

public class OkxFundingRateHistory
{
    [JsonProperty("instType"), JsonConverter(typeof(OkxInstrumentTypeConverter))]
    public OkxInstrumentType InstrumentType { get; set; }

    [JsonProperty("instId")]
    public string InstrumentId { get; set; }

    [JsonProperty("fundingRate")]
    public decimal FundingRate { get; set; }

    [JsonProperty("realizedRate")]
    public decimal RealizedRate { get; set; }

    [JsonProperty("fundingTime")]
    public long FundingTimestamp { get; set; }

    [JsonIgnore]
    public DateTime FundingTime { get { return FundingTimestamp.ConvertFromMilliseconds(); } }
}