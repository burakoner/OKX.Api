namespace OKX.Api.Models.TradingAccount;

/// <summary>
/// OkxIsolatedMarginMode
/// </summary>
public class OkxIsolatedMarginModeSettings
{
    /// <summary>
    /// Isolated margin trading settings
    /// </summary>
    [JsonProperty("isoMode"), JsonConverter(typeof(IsolatedMarginModeConverter))]
    public OkxIsolatedMarginMode MarginMode { get; set; }
}
