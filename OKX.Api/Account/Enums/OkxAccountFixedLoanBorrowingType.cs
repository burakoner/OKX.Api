namespace OKX.Api.Account.Enums;

/// <summary>
/// OKX Fixed Loan Borrowing Type
/// </summary>
public enum OkxAccountFixedLoanBorrowingType
{
    /// <summary>
    /// normal: new order
    /// </summary>
    Borrow,

    /// <summary>
    /// reborrow: renew existing order
    /// </summary>
    Reborrow,
}