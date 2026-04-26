namespace OKX.Api.Financial;

/// <summary>
/// OKX Dual Investment Order Status
/// </summary>
public record OkxFinancialDualInvestmentOrderStatus
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
