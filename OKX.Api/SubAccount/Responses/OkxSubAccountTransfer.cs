namespace OKX.Api.SubAccount;

/// <summary>
/// OKX Sub Account Transfer
/// </summary>
public record OkxSubAccountTransfer
{
    /// <summary>
    /// Transfer Id
    /// </summary>
    [JsonProperty("transId")]
    public long? TransferId { get; set; }
}
