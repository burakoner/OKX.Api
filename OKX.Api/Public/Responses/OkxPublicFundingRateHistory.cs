namespace OKX.Api.Public;

/// <summary>
/// OKX Funding Rate History
/// </summary>
public class OkxPublicFundingRateHistory
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
    public string InstrumentId { get; set; } = string.Empty;

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
    public DateTime FundingTime => FundingTimestamp.ConvertFromMilliseconds();
    
    /// <summary>
    /// Funding rate mechanism
    /// current_period
    /// next_period
    /// </summary>
    [JsonProperty("method"), JsonConverter(typeof(OkxPublicFundingRateMethodConverter))]
    public OkxPublicFundingRateMethod Method { get; set; }
}