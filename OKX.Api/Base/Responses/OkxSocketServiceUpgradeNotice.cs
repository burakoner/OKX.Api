namespace OKX.Api.Base;

/// <summary>
/// WebSocket service upgrade disconnect notice.
/// </summary>
public record OkxSocketServiceUpgradeNotice
{
    /// <summary>
    /// Event type. The service upgrade notice value is <c>notice</c>.
    /// </summary>
    [JsonProperty("event")]
    public string Event { get; set; } = string.Empty;

    /// <summary>
    /// Notice code. The service upgrade disconnect notice code is <c>64008</c>.
    /// </summary>
    [JsonProperty("code")]
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Notice message.
    /// </summary>
    [JsonProperty("msg")]
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Connection identifier.
    /// </summary>
    [JsonProperty("connId")]
    public string ConnectionId { get; set; } = string.Empty;
}
