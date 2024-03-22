namespace OKX.Api.Account.Models;

/// <summary>
/// Okx VIP Loan Borrow-Repay
/// </summary>
public class OkxVipLoanBorrowRepayHistory
{
    /// <summary>
    /// Type
    /// 1: borrow
    /// 2: repay
    /// 3: Loan reversed, lack of balance for interest
    /// </summary>
    [JsonProperty("type")]
    public string Type { get; set; }

    /// <summary>
    /// Borrow/Repay amount
    /// </summary>
    [JsonProperty("tradedLoan")]
    public decimal TradedLoan { get; set; }

    /// <summary>
    /// Borrowed amount for current account
    /// </summary>
    [JsonProperty("usedLoan")]
    public decimal UsedLoan { get; set; }

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Time
    /// </summary>
    [JsonIgnore]
    public DateTime Time { get { return Timestamp.ConvertFromMilliseconds(); } }

}
