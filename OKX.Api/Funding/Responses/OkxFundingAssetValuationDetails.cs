namespace OKX.Api.Funding;

/// <summary>
/// OKX Asset Valuation Details
/// </summary>
public record OkxFundingAssetValuationDetails
{
    /// <summary>
    /// Funding
    /// </summary>
    [JsonProperty("funding")]
    public decimal Funding { get; set; }

    /// <summary>
    /// Trading
    /// </summary>
    [JsonProperty("trading")]
    public decimal Trading { get; set; }

    /// <summary>
    /// Earn
    /// </summary>
    [JsonProperty("earn")]
    public decimal Earn { get; set; }
}
