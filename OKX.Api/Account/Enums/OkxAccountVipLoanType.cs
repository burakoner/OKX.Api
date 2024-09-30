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
    Borrow,

    /// <summary>
    /// Repayment
    /// </summary>
    [Map("2")]
    Repayment,

    /// <summary>
    /// :System Repayment
    /// </summary>
    [Map("3")]
    SystemRepayment,

    /// <summary>
    /// Interest Rate Refresh
    /// </summary>
    [Map("4")]
    InterestRateRefresh,
}