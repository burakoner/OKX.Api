namespace OKX.Api.Algo;

/// <summary>
/// Smart iceberg start trigger parameters.
/// </summary>
public record OkxAlgoSmartIcebergTriggerParameters
{
    /// <summary>
    /// Trigger action. The current contract supports start only.
    /// </summary>
    [JsonProperty("triggerAction")]
    public OkxAlgoSmartIcebergTriggerAction TriggerAction { get; set; }

    /// <summary>
    /// Trigger strategy.
    /// </summary>
    [JsonProperty("triggerStrategy")]
    public OkxAlgoSmartIcebergTriggerStrategy TriggerStrategy { get; set; }

    /// <summary>
    /// Trigger price. Only valid for the price strategy.
    /// </summary>
    [JsonProperty("triggerPx", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? TriggerPrice { get; set; }

    /// <summary>
    /// RSI trigger condition.
    /// </summary>
    [JsonProperty("triggerCond", NullValueHandling = NullValueHandling.Ignore)]
    public OkxAlgoSmartIcebergTriggerCondition? TriggerCondition { get; set; }

    /// <summary>
    /// RSI candlestick timeframe.
    /// </summary>
    [JsonProperty("timeframe", NullValueHandling = NullValueHandling.Ignore)]
    public OkxAlgoSmartIcebergTimeFrame? TimeFrame { get; set; }

    /// <summary>
    /// RSI threshold in the inclusive range 1 through 100.
    /// </summary>
    [JsonProperty("thold", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(IntAsStringNullableConverter))]
    public int? Threshold { get; set; }

    /// <summary>
    /// RSI calculation period. The current contract defaults to and fixes this value at 14.
    /// </summary>
    [JsonProperty("timePeriod", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(IntAsStringNullableConverter))]
    public int? TimePeriod { get; set; }
}
