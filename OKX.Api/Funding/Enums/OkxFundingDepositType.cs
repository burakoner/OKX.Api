namespace OKX.Api.Funding;

/// <summary>
/// OKX Deposit Type
/// </summary>
public enum OkxFundingDepositType
{
    /// <summary>
    /// InternalDeposit
    /// </summary>
    [Map("3")]
    InternalDeposit,

    /// <summary>
    /// BlockchainDeposit
    /// </summary>
    [Map("4")]
    BlockchainDeposit,
}