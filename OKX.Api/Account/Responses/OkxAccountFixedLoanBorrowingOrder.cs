namespace OKX.Api.Account;

/// <summary>
/// OKX Fixed Loan Borrow Order
/// </summary>
public class OkxAccountFixedLoanBorrowingOrder
{
    /// <summary>
    /// Borrowing order ID
    /// </summary>
    [JsonProperty("ordId")]
    public long OrderId { get; set; }
}
