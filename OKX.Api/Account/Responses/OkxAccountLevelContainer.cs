namespace OKX.Api.Account;

/// <summary>
/// Account Level
/// </summary>
public record OkxAccountLevelContainer
{
    /// <summary>
    /// Account level
    /// </summary>
    [JsonProperty("acctLv")]
    public OkxAccountLevel AccountLevel { get; set; }
}
