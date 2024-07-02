using OKX.Api.Common.Converters;
using OKX.Api.Common.Enums;

namespace OKX.Api.Public.Models;

/// <summary>
/// OKX Funding Rate
/// </summary>
public class OkxFundingRate
{
    /// <summary>
    /// Instrument Type
    /// </summary>
    [JsonProperty("instType"), JsonConverter(typeof(OkxInstrumentTypeConverter))]
    public OkxInstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Instrument ID
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; }

    /// <summary>
    /// Funding Rate
    /// </summary>
    [JsonProperty("fundingRate")]
    public decimal FundingRate { get; set; }

    /// <summary>
    /// Next Funding Rate
    /// </summary>
    [JsonProperty("nextFundingRate")]
    public decimal NextFundingRate { get; set; }

    /// <summary>
    /// Funding Time
    /// </summary>
    [JsonProperty("fundingTime")]
    public long FundingTimestamp { get; set; }

    /// <summary>
    /// Funding Time
    /// </summary>
    [JsonIgnore]
    public DateTime FundingTime { get { return FundingTimestamp.ConvertFromMilliseconds(); } }

    /// <summary>
    /// Next Funding Time
    /// </summary>
    [JsonProperty("nextFundingTime")]
    public long NextFundingTimestamp { get; set; }

    /// <summary>
    /// Next Funding Time
    /// </summary>
    [JsonIgnore]
    public DateTime NextFundingTime { get { return NextFundingTimestamp.ConvertFromMilliseconds(); } }
}
