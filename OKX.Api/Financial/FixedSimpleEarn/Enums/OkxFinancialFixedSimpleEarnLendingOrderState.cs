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
    Pending = 1,

    /// <summary>
    /// Earning
    /// </summary>
    [Map("earning")]
    Earning = 2,

    /// <summary>
    /// Expired
    /// </summary>
    [Map("expired")]
    Expired = 3,

    /// <summary>
    /// Settled
    /// </summary>
    [Map("settled")]
    Settled = 4,
}
