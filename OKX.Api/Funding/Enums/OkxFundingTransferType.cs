namespace OKX.Api.Funding;

/// <summary>
/// OKX Transfer Type
/// </summary>
public enum OkxFundingTransferType
{
    /// <summary>
    /// TransferWithinAccount
    /// </summary>
    [Map("0")]
    TransferWithinAccount,

    /// <summary>
    /// MasterAccountToSubAccount
    /// </summary>
    [Map("1")]
    MasterAccountToSubAccount,

    /// <summary>
    /// SubAccountToMasterAccount (Only applicable to API Key from master account)
    /// </summary>
    [Map("2")]
    SubAccountToMasterAccount_MasterApiKey,

    /// <summary>
    /// SubAccountToMasterAccount (Only applicable to APIKey from sub-account)
    /// </summary>
    [Map("3")]
    SubAccountToMasterAccount_SubAccountApiKey,

    /// <summary>
    /// sub-account to sub-account (Only applicable to APIKey from sub-account, and target account needs to be another sub-account which belongs to same master account)
    /// </summary>
    [Map("4")]
    SubAccountToSubAccount
}