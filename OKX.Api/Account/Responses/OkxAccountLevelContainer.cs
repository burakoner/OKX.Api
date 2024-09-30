namespace OKX.Api.Account;

/// <summary>
/// Account Level
/// </summary>
internal record OkxAccountLevelContainer
{
    /// <summary>
    /// Account level
    /// </summary>
    [JsonProperty("acctLv")]
    public OkxAccountLevel Data { get; set; }
}
