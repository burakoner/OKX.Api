namespace OKX.Api.Funding;

/// <summary>
/// OKX Withdrawal Type
/// </summary>
public enum OkxFundingWithdrawalType
{
    /// <summary>
    /// InternalDeposit
    /// </summary>
    [Map("3")]
    InternalDeposit = 3,

    /// <summary>
    /// BlockchainDeposit
    /// </summary>
    [Map("4")]
    BlockchainDeposit = 4,
}