namespace OKX.Api.Dca;

/// <summary>
/// OKX DCA Trigger Parameters Request
/// </summary>
public record OkxDcaTriggerParametersRequest
{
    /// <summary>
    /// Trigger action
    /// </summary>
    [JsonProperty("triggerAction")]
    public OkxDcaTriggerAction TriggerAction { get; set; }

    /// <summary>
    /// Trigger strategy
    /// </summary>
    [JsonProperty("triggerStrategy")]
    public OkxDcaTriggerStrategy TriggerStrategy { get; set; }

    /// <summary>
    /// Candlestick type
    /// </summary>
    [JsonProperty("timeframe", NullValueHandling = NullValueHandling.Ignore)]
    public OkxGridAlgoTimeFrame? TimeFrame { get; set; }

    /// <summary>
    /// Threshold
    /// </summary>
    [JsonProperty("thold", NullValueHandling = NullValueHandling.Ignore)]
    public string? Threshold { get; set; }

    /// <summary>
    /// Trigger condition
    /// </summary>
    [JsonProperty("triggerCond", NullValueHandling = NullValueHandling.Ignore)]
    public OkxGridAlgoTriggerCondition? TriggerCondition { get; set; }

    /// <summary>
    /// Time period
    /// </summary>
    [JsonProperty("timePeriod", NullValueHandling = NullValueHandling.Ignore)]
    public string? TimePeriod { get; set; }

    /// <summary>
    /// Trigger price
    /// </summary>
    [JsonProperty("triggerPx", NullValueHandling = NullValueHandling.Ignore)]
    public string? TriggerPrice { get; set; }
}
