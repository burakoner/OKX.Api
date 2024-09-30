namespace OKX.Api.SubAccount;

/// <summary>
/// OKX Sub Account Transfer
/// </summary>
internal record OkxSubAccountTransfer
{
    /// <summary>
    /// Transfer Id
    /// </summary>
    [JsonProperty("transId")]
    public long? Data { get; set; }
}
