namespace OKX.Api.Financial;

/// <summary>
/// OKX Financial OnChainEarn Cancel
/// </summary>
internal record OkxFinancialOnChainEarnOrderId
{
    /// <summary>
    /// Order Id
    /// </summary>
    [JsonProperty("ordId")]
    public string OrderId { get; set; } = string.Empty;
}