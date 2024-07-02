namespace OKX.Api.SubAccount.Models;

/// <summary>
/// OKX Sub Account Transfer
/// </summary>
public class OkxSubAccountTransfer
{
    /// <summary>
    /// Transfer Id
    /// </summary>
    [JsonProperty("transId")]
    public long? TransferId { get; set; }
}
