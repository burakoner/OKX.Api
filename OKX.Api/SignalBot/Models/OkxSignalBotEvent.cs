using OKX.Api.SignalBot.Converters;
using OKX.Api.SignalBot.Enums;

namespace OKX.Api.SignalBot.Models;

/// <summary>
/// OKX Signal Bot Event
/// </summary>
public class OkxSignalBotEvent
{
    /// <summary>
    /// Algo ID
    /// </summary>
    [JsonProperty("algoId")]
    public long AlgoId { get; set; }

    /// <summary>
    /// Event type
    /// </summary>
    [JsonProperty("eventType"), JsonConverter(typeof(OkxSignalBotEventTypeConverter))]
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
    public DateTime CreateTime { get { return CreateTimestamp.ConvertFromMilliseconds(); } }

    /// <summary>
    /// Event timestamp of update. Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("eventUtime")]
    public long? UpdateTimestamp { get; set; }

    /// <summary>
    /// Event timestamp of update. Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonIgnore]
    public DateTime? UpdateTime { get { return UpdateTimestamp?.ConvertFromMilliseconds(); } }

    /// <summary>
    /// Event process message
    /// </summary>
    [JsonProperty("eventProcessMsg")]
    public string EventProcessMessage { get; set; }

    /// <summary>
    /// Event status
    /// </summary>
    [JsonProperty("eventStatus"), JsonConverter(typeof(OkxSignalBotEventStatusConverter))]
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
public class OkxSignalBotTriggeredOrderData
{
    /// <summary>
    /// 	Sub order client-supplied id
    /// </summary>
    [JsonProperty("clOrdId")]
    public string ClientOrderId { get; set; }
}
