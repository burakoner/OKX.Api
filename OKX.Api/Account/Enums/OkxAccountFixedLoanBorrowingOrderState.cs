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
    Borrowing = 1,

    /// <summary>
    /// Borrowed
    /// </summary>
    [Map("2")]
    Borrowed = 2,

    /// <summary>
    /// Settled (Repaid)
    /// </summary>
    [Map("3")]
    Settled = 3,

    /// <summary>
    /// Borrow failed
    /// </summary>
    [Map("4")]
    Failed = 4,

    /// <summary>
    /// Overdue
    /// </summary>
    [Map("5")]
    Overdue = 5,

    /// <summary>
    /// Settling
    /// </summary>
    [Map("6")]
    Settling = 6,

    /// <summary>
    /// Reborrowing
    /// </summary>
    [Map("7")]
    Reborrowing = 7,

    /// <summary>
    /// Pending repay
    /// </summary>
    [Map("8")]
    PendingRepay = 8,
}