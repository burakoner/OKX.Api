namespace OKX.Api.Account;

/// <summary>
/// Account Level
/// </summary>
public class OkxAccountLevelData
{
    /// <summary>
    /// Account level
    /// </summary>
    [JsonProperty("acctLv")]
    public OkxAccountLevel AccountLevel { get; set; }
}
