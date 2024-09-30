namespace OKX.Api.Account;

/// <summary>
/// Okx VIP Loan Borrow-Repay
/// </summary>
public class OkxAccountVipLoanBorrowRepay
{
    /// <summary>
    /// Loan currency, e.g. BTC
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// borrow repay
    /// </summary>
    [JsonProperty("side")]
    public string Side { get; set; } = string.Empty;

    /// <summary>
    /// borrow/repay amount
    /// </summary>
    [JsonProperty("amt")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Order Id
    /// </summary>
    [JsonProperty("ordId")]
    public long? OrderId { get; set; }

    /// <summary>
    /// State
    /// </summary>
    [JsonProperty("state"), JsonConverter(typeof(OkxAccountVipLoanStateConverter))]
    public OkxAccountVipLoanState State { get; set; }
}
