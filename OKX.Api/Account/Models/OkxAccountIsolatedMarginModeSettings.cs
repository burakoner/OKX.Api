namespace OKX.Api.Account;

/// <summary>
/// OkxIsolatedMarginMode
/// </summary>
public class OkxAccountIsolatedMarginModeSettings
{
    /// <summary>
    /// Isolated margin trading settings
    /// </summary>
    [JsonProperty("isoMode"), JsonConverter(typeof(OkxAccountIsolatedMarginModeConverter))]
    public OkxAccountIsolatedMarginMode MarginMode { get; set; }
}
