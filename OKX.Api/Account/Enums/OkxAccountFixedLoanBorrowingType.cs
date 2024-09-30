namespace OKX.Api.Account;

/// <summary>
/// OKX Fixed Loan Borrowing Type
/// </summary>
public enum OkxAccountFixedLoanBorrowingType
{
    /// <summary>
    /// normal: new order
    /// </summary>
    [Map("normal")]
    Normal,

    /// <summary>
    /// reborrow: renew existing order
    /// </summary>
    [Map("reborrow")]
    Reborrow,
}