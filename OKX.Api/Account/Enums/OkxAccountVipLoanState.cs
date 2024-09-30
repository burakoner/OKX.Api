namespace OKX.Api.Account;

/// <summary>
/// OKX VIP Loan State
/// </summary>
public enum OkxAccountVipLoanState
{
    /// <summary>
    /// Borrowing
    /// </summary>
    [Map("1")]
    Borrowing = 1,

    /// <summary>
    /// Borrowed
    /// </summary>
    [Map("2")]
    Borrowed = 2,

    /// <summary>
    /// Repaying
    /// </summary>
    [Map("3")]
    Repaying = 3,

    /// <summary>
    /// Repaid
    /// </summary>
    [Map("4")]
    Repaid = 4,

    /// <summary>
    /// Borrow failed
    /// </summary>
    [Map("5")]
    BorrowFailed = 5,
}