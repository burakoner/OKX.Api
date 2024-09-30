namespace OKX.Api.Trade;

/// <summary>
/// OKX Cancel All After Response
/// </summary>
public class OkxTradeCancelAllAfterResponse
{
    /// <summary>
    /// CAA order tag
    /// </summary>
    [JsonProperty("tag")]
    public string Tag { get; set; } = "";

    /// <summary>
    /// The time the cancellation is triggered. triggerTime=0 means Cancel All After is disabled.
    /// </summary>
    [JsonProperty("triggerTime")]
    public long TriggerTimestamp { get; set; }
    
    /// <summary>
    /// The time the cancellation is triggered. triggerTime=0 means Cancel All After is disabled.
    /// </summary>
    [JsonIgnore]
    public DateTime TriggerTime => TriggerTimestamp.ConvertFromMilliseconds();
    
    /// <summary>
    /// The time the request is received.
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }
    
    /// <summary>
    /// The time the request is received.
    /// </summary>
    [JsonIgnore]
    public DateTime Time => Timestamp.ConvertFromMilliseconds();
}