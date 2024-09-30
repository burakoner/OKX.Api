namespace OKX.Api.Grid;

/// <summary>
/// Trigger Parameters
/// </summary>
public record OkxGridTriggerParameters
{
    /// <summary>
    /// Trigger action
    /// </summary>
    [JsonProperty("triggerAction")]
    public OkxGridAlgoTriggerAction TriggerAction { get; set; }

    /// <summary>
    /// Trigger strategy
    /// </summary>
    [JsonProperty("triggerStrategy")]
    public OkxGridTriggerStrategy TriggerStrategy { get; set; }

    /// <summary>
    /// Delay seconds after action triggered
    /// </summary>
    [JsonProperty("delaySeconds")]
    public int? DelaySeconds { get; set; }

    /// <summary>
    /// K-line type
    /// This field is only valid when triggerStrategy is rsi
    /// </summary>
    [JsonProperty("timeframe")]
    public OkxGridAlgoTimeFrame? TimeFrame { get; set; }
    
    /// <summary>
    /// Threshold
    /// The value should be an integer between 1 to 100
    /// This field is only valid when triggerStrategy is rsi
    /// </summary>
    [JsonProperty("thold")]
    public decimal? Threshold { get; set; }
    
    /// <summary>
    /// Trigger condition
    /// This field is only valid when triggerStrategy is rsi
    /// </summary>
    [JsonProperty("triggerCond")]
    public OkxGridAlgoTriggerCondition? TriggerCondition { get; set; }
    
    /// <summary>
    /// Time Period
    /// This field is only valid when triggerStrategy is rsi
    /// </summary>
    [JsonProperty("timePeriod")]
    public int? TimePeriod { get; set; }
    
    /// <summary>
    /// Trigger Price
    /// This field is only valid when triggerStrategy is rsi
    /// </summary>
    [JsonProperty("triggerPx")]
    public decimal? TriggerPrice { get; set; }
    
    /// <summary>
    /// Stop type
    /// Spot grid 1: Sell base currency 2: Keep base currency
    /// Contract grid 1: Market Close All positions 2: Keep positions
    /// This field is only valid when triggerAction is stop
    /// </summary>
    [JsonProperty("stopType")]
    public string AlgoStopType { get; set; } = string.Empty;

    /// <summary>
    /// Actual action triggered time, unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("triggerTime")]
    public long TriggerTimestamp { get; set; }

    /// Actual action triggered time, unix timestamp format in milliseconds, e.g. 1597026383085
    [JsonProperty("triggerTime")]
    public DateTime TriggerTime => TriggerTimestamp.ConvertFromMilliseconds();

    /// <summary>
    /// Actual action triggered type
    /// </summary>
    [JsonProperty("triggerType")]
    public OkxGridTriggerType? TriggerType { get; set; }
}

 
    