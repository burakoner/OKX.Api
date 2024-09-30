namespace OKX.Api.SubAccount;

/// <summary>
/// OKX Sub Account Permission of Transfer Out
/// </summary>
public record OkxSubAccountPermissionOfTransferOut
{
    /// <summary>
    /// Name of the sub-account
    /// </summary>
    [JsonProperty("subAcct")]
    public string SubAccountName { get; set; } = string.Empty;

    /// <summary>
    /// Whether the sub-account has the right to transfer out.
    /// false: cannot transfer out
    /// true: can transfer out
    /// </summary>
    [JsonProperty("canTransOut")]
    public string CanTransferOut { get; set; } = string.Empty;
}
