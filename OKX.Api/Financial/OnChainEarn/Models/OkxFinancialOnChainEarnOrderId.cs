namespace OKX.Api.Financial.OnChainEarn.Models;

/// <summary>
/// OKX Financial OnChainEarn Cancel
/// </summary>
public class OkxFinancialOnChainEarnOrderId
{
    /// <summary>
    /// Order Id
    /// </summary>
    [JsonProperty("ordId")]
    public string OrderId { get; set; }
}