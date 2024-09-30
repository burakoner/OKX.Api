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
    Borrowing,

    /// <summary>
    /// Borrowed
    /// </summary>
    [Map("2")]
    Borrowed,

    /// <summary>
    /// Repaying
    /// </summary>
    [Map("3")]
    Repaying,

    /// <summary>
    /// Repaid
    /// </summary>
    [Map("4")]
    Repaid,

    /// <summary>
    /// Borrow failed
    /// </summary>
    [Map("5")]
    BorrowFailed,
}