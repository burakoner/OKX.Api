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
    public OkxTradePositionMode Payload { get; set; }
}
