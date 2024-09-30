namespace OKX.Api.Financial.OnChainEarn;

/// <summary>
/// OKX Financial OnChainEarn Order State
/// </summary>
public enum OkxFinancialOnChainEarnOrderState
{
    /// <summary>
    /// Pending
    /// </summary>
    [Map("8")]
    Pending,

    /// <summary>
    /// Cancelling
    /// </summary>
    [Map("13")]
    Cancelling,

    /// <summary>
    /// Onchain
    /// </summary>
    [Map("9")]
    Onchain,

    /// <summary>
    /// Earning
    /// </summary>
    [Map("1")]
    Earning,

    /// <summary>
    /// Redeeming
    /// </summary>
    [Map("2")]
    Redeeming,
}