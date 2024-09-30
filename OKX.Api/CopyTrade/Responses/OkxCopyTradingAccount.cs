namespace OKX.Api.CopyTrade;

/// <summary>
/// OkxCopyTradingConfiguration
/// </summary>
public record OkxCopyTradingAccount
{
    /// <summary>
    /// User unique code
    /// </summary>
    [JsonProperty("uniqueCode")]
    public string UniqueCode { get; set; } = string.Empty;

    /// <summary>
    /// Nickname
    /// </summary>
    [JsonProperty("nickName")]
    public string NickName { get; set; } = string.Empty;

    /// <summary>
    /// Portrait link
    /// </summary>
    [JsonProperty("portLink")]
    public string PortraitLink { get; set; } = string.Empty;

    /// <summary>
    /// Details
    /// </summary>
    [JsonProperty("details")]
    public List<OkxCopyTradingAccountDetails> Details { get; set; } = [];
}