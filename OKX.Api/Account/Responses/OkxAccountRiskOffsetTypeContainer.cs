namespace OKX.Api.Account;

/// <summary>
/// OKX Risk Offset Type
/// </summary>
public record OkxAccountRiskOffsetTypeContainer
{
    /// <summary>
    /// Type
    /// </summary>
    [JsonProperty("type")]
    public OkxAccountRiskOffsetType Type { get; set; }
}