namespace OKX.Api.SubAccount;

/// <summary>
/// OKX Sub Account Name
/// </summary>
public class OkxSubAccountName
{
    /// <summary>
    /// Sub Account Name
    /// </summary>
    [JsonProperty("subAcct")]
    public string SubAccountName { get; set; } = string.Empty;
}
