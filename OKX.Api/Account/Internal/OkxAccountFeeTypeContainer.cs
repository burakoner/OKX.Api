namespace OKX.Api.Account;

/// <summary>
/// OkxAccountOkxTypeContainer
/// </summary>
internal record OkxAccountFeeTypeContainer
{
    /// <summary>
    /// Fee type
    /// </summary>
    [JsonProperty("feeType")]
    public OkxAccountFeeType Payload { get; set; }
}
