namespace OKX.Api.Account;

/// <summary>
/// OkxAccountPositionMode
/// </summary>
public record OkxAccountPositionModeContainer
{
    /// <summary>
    /// Position mode
    /// </summary>
    [JsonProperty("posMode")]
    public OkxTradePositionMode PositionMode { get; set; }
}
