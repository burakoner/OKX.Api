using OKX.Api.Account.Converters;
using OKX.Api.Account.Enums;

namespace OKX.Api.Account.Models;

/// <summary>
/// Okx VIP Loan Borrow-Repay
/// </summary>
public class OkxVipLoanBorrowRepay
{
    /// <summary>
    /// Loan currency, e.g. BTC
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    /// <summary>
    /// borrow repay
    /// </summary>
    [JsonProperty("side")]
    public string Side { get; set; }

    /// <summary>
    /// borrow/repay amount
    /// </summary>
    [JsonProperty("amt")]
    public decimal Amount { get; set; }

    [JsonProperty("ordId")]
    public long? OrderId { get; set; }

    [JsonProperty("state"), JsonConverter(typeof(OkxAccountVipLoanStateConverter))]
    public OkxAccountVipLoanState State { get; set; }
}
