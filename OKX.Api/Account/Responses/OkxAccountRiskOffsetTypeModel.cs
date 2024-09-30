namespace OKX.Api.Account;

/// <summary>
/// OKX Risk Offset Type
/// </summary>
public class OkxAccountRiskOffsetTypeModel
{
    /// <summary>
    /// Type
    /// </summary>
    [JsonProperty("type")]
    public OkxAccountRiskOffsetType Type { get; set; }
}