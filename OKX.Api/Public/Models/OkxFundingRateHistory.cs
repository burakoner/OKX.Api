using OKX.Api.Common.Converters;
using OKX.Api.Common.Enums;

namespace OKX.Api.Public.Models;

/// <summary>
/// OKX Funding Rate History
/// </summary>
public class OkxFundingRateHistory
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
    /// Realized Rate
    /// </summary>
    [JsonProperty("realizedRate")]
    public decimal RealizedRate { get; set; }

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
}