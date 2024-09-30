namespace OKX.Api.Common;

/// <summary>
/// OKX Cancel All After
/// </summary>
public record OkxCancelAllAfter
{
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