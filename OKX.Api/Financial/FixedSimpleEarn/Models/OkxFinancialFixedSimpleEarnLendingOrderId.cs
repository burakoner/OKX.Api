namespace OKX.Api.Financial.FixedSimpleEarn;

/// <summary>
/// OKX Financial Fixed Simple Earn Lending Placed Order Response
/// </summary>
public class OkxFinancialFixedSimpleEarnLendingOrderId
{
    /// <summary>
    /// Lending order ID
    /// </summary>
    [JsonProperty("ordId")]
    public long OrderId { get; set; }
}