namespace OKX.Api.Account;

/// <summary>
/// Account Level
/// </summary>
internal record OkxAccountModeContainer
{
    /// <summary>
    /// Account level
    /// </summary>
    [JsonProperty("acctLv")]
    public OkxAccountMode Payload { get; set; }
}
