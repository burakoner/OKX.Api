using OKX.Api.Account.Converters;
using OKX.Api.Account.Enums;

namespace OKX.Api.Account.Models;

/// <summary>
/// OkxIsolatedMarginMode
/// </summary>
public class OkxIsolatedMarginModeSettings
{
    /// <summary>
    /// Isolated margin trading settings
    /// </summary>
    [JsonProperty("isoMode"), JsonConverter(typeof(OkxIsolatedMarginModeConverter))]
    public OkxIsolatedMarginMode MarginMode { get; set; }
}
