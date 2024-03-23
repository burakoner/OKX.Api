namespace OKX.Api.Funding.Enums;

/// <summary>
/// OKX Transfer Type
/// </summary>
public enum OkxTransferType
{
    /// <summary>
    /// TransferWithinAccount
    /// </summary>
    TransferWithinAccount,

    /// <summary>
    /// MasterAccountToSubAccount
    /// </summary>
    MasterAccountToSubAccount,

    /// <summary>
    /// SubAccountToMasterAccount (Only applicable to API Key from master account)
    /// </summary>
    SubAccountToMasterAccount_MasterApiKey,

    /// <summary>
    /// SubAccountToMasterAccount (Only applicable to APIKey from sub-account)
    /// </summary>
    SubAccountToMasterAccount_SubAccountApiKey,

    /// <summary>
    /// sub-account to sub-account (Only applicable to APIKey from sub-account, and target account needs to be another sub-account which belongs to same master account)
    /// </summary>
    SubAccountToSubAccount
}