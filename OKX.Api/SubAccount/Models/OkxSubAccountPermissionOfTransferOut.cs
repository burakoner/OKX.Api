﻿namespace OKX.Api.SubAccount.Models;

/// <summary>
/// OKX Sub Account Permission of Transfer Out
/// </summary>
public class OkxSubAccountPermissionOfTransferOut
{
    /// <summary>
    /// Name of the sub-account
    /// </summary>
    [JsonProperty("subAcct")]
    public string SubAccountName { get; set; }

    /// <summary>
    /// Whether the sub-account has the right to transfer out.
    /// false: cannot transfer out
    /// true: can transfer out
    /// </summary>
    [JsonProperty("canTransOut")]
    public string CanTransferOut { get; set; }
}
