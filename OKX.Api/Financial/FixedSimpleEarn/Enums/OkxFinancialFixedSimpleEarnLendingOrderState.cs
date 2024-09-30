namespace OKX.Api.Financial.FixedSimpleEarn;

/// <summary>
/// OKX Financial Fixed Simple Earn Lending Order State
/// </summary>
public enum OkxFinancialFixedSimpleEarnLendingOrderState
{
    /// <summary>
    /// Pending
    /// </summary>
    [Map("pending")]
    Pending,

    /// <summary>
    /// Earning
    /// </summary>
    [Map("earning")]
    Earning,

    /// <summary>
    /// Expired
    /// </summary>
    [Map("expired")]
    Expired,

    /// <summary>
    /// Settled
    /// </summary>
    [Map("settled")]
    Settled,
}
