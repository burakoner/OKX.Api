namespace OKX.Api.Financial.EthStaking;

/// <summary>
/// OKX Financial Eth Staking Status
/// </summary>
public enum OkxFinancialEthStakingStatus
{
    /// <summary>
    /// Pending
    /// </summary>
    [Map("pending")]
    Pending,

    /// <summary>
    /// Success
    /// </summary>
    [Map("success")]
    Success,

    /// <summary>
    /// Failed
    /// </summary>
    [Map("failed")]
    Failed,
}