namespace OKX.Api.SubAccount;

/// <summary>
/// OKX Sub Account Transfer
/// </summary>
internal record OkxSubAccountTransferIdContainer
{
    /// <summary>
    /// Transfer Id
    /// </summary>
    [JsonProperty("transId")]
    public long? Payload { get; set; }
}
