namespace OKX.Api.Account;

/// <summary>
/// OkxIsolatedMarginMode
/// </summary>
internal record OkxAccountIsolatedMarginModeContainer
{
    /// <summary>
    /// Isolated margin trading settings
    /// </summary>
    [JsonProperty("isoMode")]
    public OkxAccountIsolatedMarginMode Data { get; set; }
}
