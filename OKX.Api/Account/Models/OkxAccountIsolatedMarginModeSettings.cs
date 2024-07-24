using OKX.Api.Account.Converters;
using OKX.Api.Account.Enums;

namespace OKX.Api.Account.Models;

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
