namespace OKX.Api.SignalBot;

/// <summary>
/// OKX Signal Bot Event
/// </summary>
public record OkxSignalBotEvent
{
    /// <summary>
    /// Algo ID
    /// </summary>
    [JsonProperty("algoId")]
    public long AlgoId { get; set; }

    /// <summary>
    /// Event type
    /// </summary>
    [JsonProperty("eventType")]
    public OkxSignalBotEventType EventType { get; set; }

    /// <summary>
    /// Event timestamp of creation. Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("eventCtime")]
    public long CreateTimestamp { get; set; }

    /// <summary>
    /// Event timestamp of creation. Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonIgnore]
    public DateTime CreateTime => CreateTimestamp.ConvertFromMilliseconds();

    /// <summary>
    /// Event timestamp of update. Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("eventUtime")]
    public long? UpdateTimestamp { get; set; }

    /// <summary>
    /// Event timestamp of update. Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonIgnore]
    public DateTime? UpdateTime => UpdateTimestamp?.ConvertFromMilliseconds();

    /// <summary>
    /// Event process message
    /// </summary>
    [JsonProperty("eventProcessMsg")]
    public string EventProcessMessage { get; set; } = string.Empty;

    /// <summary>
    /// Event status
    /// </summary>
    [JsonProperty("eventStatus")]
    public OkxSignalBotEventStatus EventStatus { get; set; }

    /// <summary>
    /// Triggered sub order data
    /// </summary>
    [JsonProperty("triggeredOrdData")]
    public List<OkxSignalBotTriggeredOrderData> TriggeredOrders { get; set; } = [];
}

/// <summary>
/// OKX Signal Bot Triggered sub order data
/// </summary>
public record OkxSignalBotTriggeredOrderData
{
    /// <summary>
    /// 	Sub order client-supplied id
    /// </summary>
    [JsonProperty("clOrdId")]
    public string ClientOrderId { get; set; } = string.Empty;
}
