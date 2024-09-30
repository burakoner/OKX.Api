namespace OKX.Api.Grid;

/// <summary>
/// OKX Grid Place Trigger Parameters Request
/// </summary>
public record OkxGridTriggerParametersRequest
{
    /// <summary>
    /// Trigger Action
    /// </summary>
    [JsonProperty("triggerAction")]
    public OkxGridAlgoTriggerAction TriggerAction { get; set; }

    /// <summary>
    /// Trigger Strategy
    /// </summary>
    [JsonProperty("triggerStrategy")]
    public OkxGridTriggerStrategy TriggerStrategy { get; set; }

    /// <summary>
    /// Delay Seconds
    /// </summary>
    [JsonProperty("delaySeconds", NullValueHandling = NullValueHandling.Ignore)]
    public string? DelaySeconds { get; set; }

    /// <summary>
    /// Time Frame
    /// </summary>
    [JsonProperty("timeframe", NullValueHandling = NullValueHandling.Ignore)]
    public OkxGridAlgoTimeFrame? TimeFrame { get; set; }

    /// <summary>
    /// Threshold
    /// </summary>
    [JsonProperty("thold", NullValueHandling = NullValueHandling.Ignore)]
    public string? Threshold { get; set; }

    /// <summary>
    /// Trigger Condition
    /// </summary>
    [JsonProperty("triggerCond", NullValueHandling = NullValueHandling.Ignore)]
    public OkxGridAlgoTriggerCondition? TriggerCondition { get; set; }

    /// <summary>
    /// Time Period
    /// </summary>
    [JsonProperty("timePeriod", NullValueHandling = NullValueHandling.Ignore)]
    public string? TimePeriod { get; set; }

    /// <summary>
    /// Trigger Price
    /// </summary>
    [JsonProperty("triggerPx", NullValueHandling = NullValueHandling.Ignore)]
    public string? TriggerPrice { get; set; }

    /// <summary>
    /// Spot Algo Stop Type
    /// </summary>
    [JsonIgnore]
    public OkxGridSpotAlgoStopType? SpotAlgoStopType { get; set; }

    /// <summary>
    /// Contract Algo Stop Type
    /// </summary>
    [JsonIgnore]
    public OkxGridContractAlgoStopType? ContractAlgoStopType { get; set; }

    /// <summary>
    /// Algo Stop Type
    /// </summary>
    [JsonProperty("stopType", NullValueHandling = NullValueHandling.Ignore)]
    public string AlgoStopType
    {
        get
        {
            if (SpotAlgoStopType is not null) return MapConverter.GetString(SpotAlgoStopType);
            if (ContractAlgoStopType is not null) return MapConverter.GetString(ContractAlgoStopType);
            return string.Empty;
        }
    }

}
