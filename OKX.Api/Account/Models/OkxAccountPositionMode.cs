namespace OKX.Api.Account;

/// <summary>
/// OkxAccountPositionMode
/// </summary>
public class OkxAccountPositionMode
{
    /// <summary>
    /// Position mode
    /// </summary>
    [JsonProperty("posMode"), JsonConverter(typeof(OkxTradePositionModeConverter))]
    public OkxTradePositionMode PositionMode { get; set; }
}
