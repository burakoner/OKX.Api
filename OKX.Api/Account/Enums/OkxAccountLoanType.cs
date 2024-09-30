namespace OKX.Api.Account;

/// <summary>
/// OKX Loan Type
/// </summary>
public enum OkxAccountLoanType
{
    /// <summary>
    /// VIP Loans
    /// </summary>
    [Map("1")]
    VIP,

    /// <summary>
    /// Market Loans
    /// </summary>
    [Map("2")]
    Market,
}