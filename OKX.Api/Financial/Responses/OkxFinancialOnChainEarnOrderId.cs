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
    public long? Data { get; set; }
}