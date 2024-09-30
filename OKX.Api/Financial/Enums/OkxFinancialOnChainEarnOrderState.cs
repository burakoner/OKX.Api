namespace OKX.Api.Financial;

/// <summary>
/// OKX Financial OnChainEarn Order State
/// </summary>
public enum OkxFinancialOnChainEarnOrderState
{
    /// <summary>
    /// Pending
    /// </summary>
    [Map("8")]
    Pending = 8,

    /// <summary>
    /// Cancelling
    /// </summary>
    [Map("13")]
    Cancelling = 13,

    /// <summary>
    /// Onchain
    /// </summary>
    [Map("9")]
    Onchain = 9,

    /// <summary>
    /// Earning
    /// </summary>
    [Map("1")]
    Earning = 1,

    /// <summary>
    /// Redeeming
    /// </summary>
    [Map("2")]
    Redeeming = 2,
}