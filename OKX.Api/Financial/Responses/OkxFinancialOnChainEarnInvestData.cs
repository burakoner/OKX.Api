namespace OKX.Api.Financial;

/// <summary>
/// OKX Financial On Chain Earn Invest Data
/// </summary>
public record OkxFinancialOnChainEarnInvestData
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