namespace OKX.Api.Account;

/// <summary>
/// OkxAccountPositionMode
/// </summary>
internal record OkxAccountPositionModeContainer
{
    /// <summary>
    /// Position mode
    /// </summary>
    [JsonProperty("posMode")]
    public OkxTradePositionMode Data { get; set; }
}
