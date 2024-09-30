namespace OKX.Api.Financial.OnChainEarn;

/// <summary>
/// OKX Financial On Chain Earn Invest Data
/// </summary>
public class OkxFinancialOnChainEarnInvestData
{
    /// <summary>
    /// Currency type, e.g. BTC
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Investment amount
    /// </summary>
    [JsonProperty("amt")]
    public string Amount { get; set; } = string.Empty;
}