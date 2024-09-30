namespace OKX.Api.Financial.OnChainEarn;

/// <summary>
/// OKX Financial OnChainEarn Cancel
/// </summary>
public record OkxFinancialOnChainEarnOrderId
{
    /// <summary>
    /// Order Id
    /// </summary>
    [JsonProperty("ordId")]
    public string OrderId { get; set; } = string.Empty;
}