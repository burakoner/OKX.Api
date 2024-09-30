namespace OKX.Api.Account;

/// <summary>
/// OKX Fixed Loan Borrow Type
/// </summary>
public enum OkxAccountFixedLoanBorrowingOrderState
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
    /// Settled (Repaid)
    /// </summary>
    [Map("3")]
    Settled,

    /// <summary>
    /// Borrow failed
    /// </summary>
    [Map("4")]
    Failed,

    /// <summary>
    /// Overdue
    /// </summary>
    [Map("5")]
    Overdue,

    /// <summary>
    /// Settling
    /// </summary>
    [Map("6")]
    Settling,

    /// <summary>
    /// Reborrowing
    /// </summary>
    [Map("7")]
    Reborrowing,
}