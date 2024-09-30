namespace OKX.Api.Account;

/// <summary>
/// OKX Risk Offset Type
/// </summary>
internal record OkxAccountRiskOffsetTypeContainer
{
    /// <summary>
    /// Type
    /// </summary>
    [JsonProperty("type")]
    public OkxAccountRiskOffsetType Data { get; set; }
}