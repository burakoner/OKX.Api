namespace OKX.Api.Financial;

/// <summary>
/// OKX Financial On Chain Earn Earning Data
/// </summary>
public record OkxFinancialOnChainEarnEarningData
{
    /// <summary>
    /// Currency type, e.g. BTC
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Earning type
    /// </summary>
    [JsonProperty("earningType")]
    public OkxFinancialOnChainEarnEarningType EarningType { get; set; }

    /// <summary>
    /// Earning amount
    /// </summary>
    [JsonProperty("earnings")]
    public decimal? Earnings { get; set; }
}