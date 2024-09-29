namespace OKX.Api.Account;

/// <summary>
/// OKX VIP Loan Type
/// </summary>
public enum OkxAccountVipLoanType
{
    /// <summary>
    /// Borrow
    /// </summary>
    Borrow,

    /// <summary>
    /// Repayment
    /// </summary>
    Repayment,

    /// <summary>
    /// :System Repayment
    /// </summary>
    SystemRepayment,

    /// <summary>
    /// Interest Rate Refresh
    /// </summary>
    InterestRateRefresh,
}