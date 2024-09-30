namespace OKX.Api.Account;

/// <summary>
/// OkxAccountPositionMode
/// </summary>
public class OkxAccountPositionMode
{
    /// <summary>
    /// Position mode
    /// </summary>
    [JsonProperty("posMode")]
    public OkxTradePositionMode PositionMode { get; set; }
}
