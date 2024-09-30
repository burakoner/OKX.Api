namespace OKX.Api.Account;

/// <summary>
/// OKX VIP Loan Type
/// </summary>
public enum OkxAccountVipLoanType
{
    /// <summary>
    /// Borrow
    /// </summary>
    [Map("1")]
    Borrow = 1,

    /// <summary>
    /// Repayment
    /// </summary>
    [Map("2")]
    Repayment = 2,

    /// <summary>
    /// :System Repayment
    /// </summary>
    [Map("3")]
    SystemRepayment = 3,

    /// <summary>
    /// Interest Rate Refresh
    /// </summary>
    [Map("4")]
    InterestRateRefresh = 4,
}