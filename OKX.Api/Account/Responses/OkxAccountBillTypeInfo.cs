namespace OKX.Api.Account;

/// <summary>
/// OKX Account Bill Type Information
/// </summary>
public record OkxAccountBillTypeInfo
{
    /// <summary>
    /// Bill type identifier
    /// </summary>
    [JsonProperty("type")]
    public string TypeId { get; set; } = string.Empty;

    /// <summary>
    /// Bill type description. Empty string means the type is not enabled.
    /// </summary>
    [JsonProperty("typeDesc")]
    public string TypeDescription { get; set; } = string.Empty;

    /// <summary>
    /// Bill sub-type details
    /// </summary>
    [JsonProperty("subTypeDetails")]
    public List<OkxAccountBillSubTypeInfo> SubTypeDetails { get; set; } = [];
}

/// <summary>
/// OKX Account Bill Sub-Type Information
/// </summary>
public record OkxAccountBillSubTypeInfo
{
    /// <summary>
    /// Bill sub-type identifier
    /// </summary>
    [JsonProperty("subType")]
    public string SubTypeId { get; set; } = string.Empty;

    /// <summary>
    /// Bill sub-type description. Empty string means the type is not enabled.
    /// </summary>
    [JsonProperty("subTypeDesc")]
    public string SubTypeDescription { get; set; } = string.Empty;
}
