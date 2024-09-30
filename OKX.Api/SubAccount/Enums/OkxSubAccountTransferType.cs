namespace OKX.Api.SubAccount;

/// <summary>
/// OKX Sub-Account Transfer Type
/// </summary>
public enum OkxSubAccountTransferType
{
    /// <summary>
    /// FromMasterAccountToSubAccout
    /// </summary>
    [Map("0")]
    FromMasterAccountToSubAccout,

    /// <summary>
    /// FromSubAccountToMasterAccout
    /// </summary>
    [Map("1")]
    FromSubAccountToMasterAccout,
}