namespace OKX.Api.Public;

/// <summary>
/// OKX Funding Rate History
/// </summary>
public record OkxPublicFundingRateHistory
{
    /// <summary>
    /// Instrument Type.
    /// SWAP or FUTURES, where FUTURES represents X-Perps futures contracts.
    /// </summary>
    [JsonProperty("instType")]
    public OkxInstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Instrument ID.
    /// Can be a perpetual swap or an X-Perps futures contract.
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
    /// Funding rate mechanism.
    /// OKX currently documents current_period for this endpoint.
    /// next_period is kept for backwards compatibility.
    /// </summary>
    [JsonProperty("method")]
    public OkxPublicFundingRateMethod Method { get; set; }
}
