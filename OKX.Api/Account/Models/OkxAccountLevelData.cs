using OKX.Api.Account.Converters;
using OKX.Api.Account.Enums;

namespace OKX.Api.Account.Models;

/// <summary>
/// Account Level
/// </summary>
public class OkxAccountLevelData
{
    /// <summary>
    /// Account level
    /// </summary>
    [JsonProperty("acctLv"), JsonConverter(typeof(OkxAccountLevelConverter))]
    public OkxAccountLevel AccountLevel { get; set; }
}
