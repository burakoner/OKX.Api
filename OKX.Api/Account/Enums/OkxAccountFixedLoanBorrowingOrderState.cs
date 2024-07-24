namespace OKX.Api.Account.Enums;

/// <summary>
/// OKX Fixed Loan Borrow Type
/// </summary>
public enum OkxAccountFixedLoanBorrowingOrderState
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
    /// Settled (Repaid)
    /// </summary>
    Settled,

    /// <summary>
    /// Borrow failed
    /// </summary>
    Failed,

    /// <summary>
    /// Overdue
    /// </summary>
    Overdue,

    /// <summary>
    /// Settling
    /// </summary>
    Settling,

    /// <summary>
    /// Reborrowing
    /// </summary>
    Reborrowing,
}