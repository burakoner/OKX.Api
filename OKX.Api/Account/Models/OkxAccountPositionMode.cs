using OKX.Api.Trade.Converters;
using OKX.Api.Trade.Enums;

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
