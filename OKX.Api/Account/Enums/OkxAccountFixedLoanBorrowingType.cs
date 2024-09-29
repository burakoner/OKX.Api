namespace OKX.Api.Account;

/// <summary>
/// OKX Fixed Loan Borrowing Type
/// </summary>
public enum OkxAccountFixedLoanBorrowingType
{
    /// <summary>
    /// normal: new order
    /// </summary>
    Normal,

    /// <summary>
    /// reborrow: renew existing order
    /// </summary>
    Reborrow,
}