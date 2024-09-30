namespace OKX.Api.Financial;

/// <summary>
/// OKX Financial Fixed Simple Earn Lending Placed Order Response
/// </summary>
internal record OkxFinancialFixedSimpleEarnLendingOrderId
{
    /// <summary>
    /// Lending order ID
    /// </summary>
    [JsonProperty("ordId")]
    public long OrderId { get; set; }
}