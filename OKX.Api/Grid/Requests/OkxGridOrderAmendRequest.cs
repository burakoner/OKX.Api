namespace OKX.Api.Grid;

/// <summary>
/// OKX Grid Amend Order Request
/// </summary>
public record OkxGridOrderAmendRequest
{
    /// <summary>
    /// Algo order ID
    /// </summary>
    [JsonProperty("algoId")]
    public long AlgoOrderId { get; set; }

    /// <summary>
    /// Instrument ID
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; } = string.Empty;

    /// <summary>
    /// Take profit trigger price
    /// </summary>
    [JsonProperty("tpTriggerPx", NullValueHandling = NullValueHandling.Ignore)]
    public decimal? TakeProfitTriggerPrice { get; set; }

    /// <summary>
    /// Stop loss trigger price
    /// </summary>
    [JsonProperty("slTriggerPx", NullValueHandling = NullValueHandling.Ignore)]
    public decimal? StopLossTriggerPrice { get; set; }

    /// <summary>
    /// Take profit ratio, 0.1 represents 10%
    /// </summary>
    [JsonProperty("takeProfitRatio", NullValueHandling = NullValueHandling.Ignore)]
    public decimal? TakeProfitRatio { get; set; }

    /// <summary>
    /// Stop loss ratio, 0.1 represents 10%
    /// </summary>
    [JsonProperty("stopLossRatio", NullValueHandling = NullValueHandling.Ignore)]
    public decimal? StopLossRatio { get; set; }
    
    /// <summary>
    /// Trigger parameters
    /// </summary>
    [JsonProperty("triggerParams", NullValueHandling = NullValueHandling.Ignore)]
    public List<OkxGridOrderAmendRequestTriggerParameters> TriggerParameters { get; set; } = [];
}

/// <summary>
/// OKX Grid Amend Trigger Parameters
/// </summary>
public record OkxGridOrderAmendRequestTriggerParameters
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
    /// Trigger price
    /// </summary>
    [JsonProperty("triggerPx", NullValueHandling = NullValueHandling.Ignore)]
    public string TriggerPrice { get; set; } = string.Empty;

    /// <summary>
    /// Spot algo stop type
    /// </summary>
    [JsonIgnore]
    public OkxGridSpotAlgoStopType? SpotAlgoStopType { get; set; }

    /// <summary>
    /// Contract algo stop type
    /// </summary>
    [JsonIgnore]
    public OkxGridContractAlgoStopType? ContractAlgoStopType { get; set; }

    /// <summary>
    /// Algo stop type
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
