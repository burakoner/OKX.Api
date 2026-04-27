namespace OKX.Api.Dca;

/// <summary>
/// OKX DCA Trigger Parameters
/// </summary>
public record OkxDcaTriggerParameters
{
    /// <summary>
    /// Trigger action
    /// </summary>
    [JsonProperty("triggerAction")]
    public OkxDcaTriggerAction? TriggerAction { get; set; }

    /// <summary>
    /// Trigger strategy
    /// </summary>
    [JsonProperty("triggerStrategy")]
    public OkxDcaTriggerStrategy? TriggerStrategy { get; set; }

    /// <summary>
    /// K-line type
    /// </summary>
    [JsonProperty("timeframe")]
    public OkxGridAlgoTimeFrame? TimeFrame { get; set; }

    /// <summary>
    /// Threshold
    /// </summary>
    [JsonProperty("thold"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? Threshold { get; set; }

    /// <summary>
    /// Trigger condition
    /// </summary>
    [JsonProperty("triggerCond")]
    public OkxGridAlgoTriggerCondition? TriggerCondition { get; set; }

    /// <summary>
    /// Time period
    /// </summary>
    [JsonProperty("timePeriod"), JsonConverter(typeof(IntAsStringNullableConverter))]
    public int? TimePeriod { get; set; }

    /// <summary>
    /// Trigger price
    /// </summary>
    [JsonProperty("triggerPx"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? TriggerPrice { get; set; }
}
