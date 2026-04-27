namespace OKX.Api.Spread;

/// <summary>
/// Spread websocket order book update wrapper.
/// </summary>
public record OkxSocketSpreadOrderBookUpdate : OkxSocketResponse
{
    /// <summary>
    /// Update action.
    /// </summary>
    [JsonProperty("action")]
    public string Action { get; set; } = string.Empty;

    /// <summary>
    /// Channel arguments.
    /// </summary>
    [JsonProperty("arg")]
    public OkxSocketUpdateArguments? Arguments { get; set; }

    /// <summary>
    /// Order book rows.
    /// </summary>
    [JsonProperty("data")]
    public List<OkxSpreadOrderBook> Data { get; set; } = [];
}
