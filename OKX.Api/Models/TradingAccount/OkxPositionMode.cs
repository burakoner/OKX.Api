namespace OKX.Api.Models.TradingAccount;

/// <summary>
/// OkxAccountPositionMode
/// </summary>
public class OkxAccountPositionMode
{
    /// <summary>
    /// Position mode
    /// </summary>
    [JsonProperty("posMode"), JsonConverter(typeof(PositionModeConverter))]
    public OkxPositionMode PositionMode { get; set; }
}
