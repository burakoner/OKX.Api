namespace OKX.Api.SubAccount;

/// <summary>
/// OKX Sub-Account Transfer Type
/// </summary>
public enum OkxSubAccountTransferType : byte
{
    /// <summary>
    /// FromMasterAccountToSubAccout
    /// </summary>
    [Map("0")]
    FromMasterAccountToSubAccout = 0,

    /// <summary>
    /// FromSubAccountToMasterAccout
    /// </summary>
    [Map("1")]
    FromSubAccountToMasterAccout = 1,
}