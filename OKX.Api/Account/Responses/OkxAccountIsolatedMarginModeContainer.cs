namespace OKX.Api.Account;

/// <summary>
/// OkxIsolatedMarginMode
/// </summary>
public record OkxAccountIsolatedMarginModeContainer
{
    /// <summary>
    /// Isolated margin trading settings
    /// </summary>
    [JsonProperty("isoMode")]
    public OkxAccountIsolatedMarginMode MarginMode { get; set; }
}
