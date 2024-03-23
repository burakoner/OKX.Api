using OKX.Api.Common.Converters;
using OKX.Api.Common.Enums;

namespace OKX.Api.Account.Models;

/// <summary>
/// OkxAccountPositionMode
/// </summary>
public class OkxAccountPositionMode
{
    /// <summary>
    /// Position mode
    /// </summary>
    [JsonProperty("posMode"), JsonConverter(typeof(OkxPositionModeConverter))]
    public OkxPositionMode PositionMode { get; set; }
}
