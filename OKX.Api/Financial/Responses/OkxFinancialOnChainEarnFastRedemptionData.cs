namespace OKX.Api.Financial;

/// <summary>
/// OKX Financial On Chain Earn Fast Redemption Data
/// </summary>
public record OkxFinancialOnChainEarnFastRedemptionData
{
    /// <summary>
    /// Currency type, e.g. BTC
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Redeeming amount
    /// </summary>
    [JsonProperty("redeemingAmt")]
    public decimal RedeemingAmount { get; set; }
}