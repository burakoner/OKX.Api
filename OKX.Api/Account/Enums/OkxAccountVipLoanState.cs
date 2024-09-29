namespace OKX.Api.Account;

/// <summary>
/// OKX VIP Loan State
/// </summary>
public enum OkxAccountVipLoanState
{
    /// <summary>
    /// Borrowing
    /// </summary>
    Borrowing,

    /// <summary>
    /// Borrowed
    /// </summary>
    Borrowed,

    /// <summary>
    /// Repaying
    /// </summary>
    Repaying,

    /// <summary>
    /// Repaid
    /// </summary>
    Repaid,

    /// <summary>
    /// Borrow failed
    /// </summary>
    BorrowFailed,
}