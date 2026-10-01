namespace OKX.Api.Base;

/// <summary>
/// Server-reported private-channel connection count or connection-limit termination notice.
/// </summary>
public record OkxSocketChannelConnectionCount
{
    /// <summary>
    /// Event: channel-conn-count or channel-conn-count-error.
    /// </summary>
    [JsonProperty("event")]
    public string Event { get; set; } = string.Empty;

    /// <summary>
    /// Channel whose connections are counted.
    /// </summary>
    [JsonProperty("channel")]
    public string Channel { get; set; } = string.Empty;

    /// <summary>
    /// Server-reported count. Null if not supplied; not a locally calculated quota.
    /// </summary>
    [JsonProperty("connCount")]
    public int? ConnectionCount { get; set; }

    /// <summary>
    /// Connection to which this notice applies.
    /// </summary>
    [JsonProperty("connId")]
    public string ConnectionId { get; set; } = string.Empty;

    /// <summary>
    /// Whether OKX reports that this connection's channel subscription has been terminated.
    /// </summary>
    [JsonIgnore]
    public bool IsLimitError => Event == "channel-conn-count-error";
}
