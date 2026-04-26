namespace OKX.Api.Financial;

/// <summary>
/// OKX Dual Investment Redeem Result
/// </summary>
public record OkxFinancialDualInvestmentRedeem
{
    /// <summary>
    /// Order ID
    /// </summary>
    [JsonProperty("ordId")]
    public string OrderId { get; set; } = string.Empty;

    /// <summary>
    /// Order state
    /// </summary>
    [JsonProperty("state")]
    public OkxFinancialDualInvestmentOrderState State { get; set; }
}
