namespace OKX.Api.Account.Models;

/// <summary>
/// OKX Fixed Loan Borrow Order
/// </summary>
public class OkxFixedLoanBorrowingOrder
{
    /// <summary>
    /// Borrowing order ID
    /// </summary>
    [JsonProperty("ordId")]
    public long OrderId { get; set; }
}
