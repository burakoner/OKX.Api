namespace OKX.Api.Models.GridTrading;

public class OkxGridPlaceOrderRequest
{
    [JsonProperty("instId")]
    public string InstrumentId { get; set; }

    [JsonProperty("algoOrdType"), JsonConverter(typeof(GridAlgoOrderTypeConverter))]
    public OkxGridAlgoOrderType AlgoOrderType { get; set; }

    [JsonProperty("maxPx")]
    public decimal MaximumPrice { get; set; }

    [JsonProperty("minPx")]
    public decimal MinimumPrice { get; set; }

    [JsonProperty("gridNum")]
    public decimal GridNumber { get; set; }

    [JsonProperty("runType", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(GridRunTypeConverter))]
    public OkxGridRunType? GridRunType { get; set; }

    [JsonProperty("tpTriggerPx", NullValueHandling = NullValueHandling.Ignore)]
    public decimal? TakeProfitTriggerPrice { get; set; }

    [JsonProperty("slTriggerPx", NullValueHandling = NullValueHandling.Ignore)]
    public decimal? StopLossTriggerPrice { get; set; }

    [JsonProperty("algoClOrdId", NullValueHandling = NullValueHandling.Ignore)]
    public string AlgoClientOrderId { get; set; }

    [JsonProperty("tag", NullValueHandling = NullValueHandling.Ignore)]
    internal string Tag { get; set; }

    [JsonProperty("triggerParams", NullValueHandling = NullValueHandling.Ignore)]
    public IEnumerable< OkxGridPlaceTriggerParameters> TriggerParameters { get; set; }

    [JsonProperty("quoteSz", NullValueHandling = NullValueHandling.Ignore)]
    public decimal? QuoteSize { get; set; }
    
    [JsonProperty("baseSz", NullValueHandling = NullValueHandling.Ignore)]
    public decimal? BaseSize { get; set; }
    
    [JsonProperty("sz", NullValueHandling = NullValueHandling.Ignore)]
    public decimal? Size { get; set; }

    [JsonProperty("direction", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(GridContractDirectionConverter))]
    public OkxGridContractDirection? ContractGridDirection { get; set; }

    [JsonProperty("lever", NullValueHandling = NullValueHandling.Ignore)]
    public decimal? Leverage { get; set; }

    [JsonProperty("basePos", NullValueHandling = NullValueHandling.Ignore)]
    public bool? BasePosition { get; set; }
}

public class OkxGridPlaceTriggerParameters
{
    [JsonProperty("triggerAction"), JsonConverter(typeof(GridAlgoTriggerActionConverter))]
    public OkxGridAlgoTriggerAction TriggerAction { get; set; }

    [JsonProperty("triggerStrategy"), JsonConverter(typeof(GridAlgoTriggerStrategyConverter))]
    public OkxGridAlgoTriggerStrategy TriggerStrategy { get; set; }

    [JsonProperty("delaySeconds", NullValueHandling = NullValueHandling.Ignore)]
    public string DelaySeconds { get; set; }

    [JsonProperty("timeframe", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(GridAlgoTimeFrameConverter))]
    public OkxGridAlgoTimeFrame? TimeFrame { get; set; }

    [JsonProperty("thold", NullValueHandling = NullValueHandling.Ignore)]
    public string Threshold { get; set; }

    [JsonProperty("triggerCond", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(GridAlgoTriggerConditionConverter))]
    public OkxGridAlgoTriggerCondition? TriggerCondition { get; set; }

    [JsonProperty("timePeriod", NullValueHandling = NullValueHandling.Ignore)]
    public string TimePeriod { get; set; }

    [JsonProperty("triggerPx", NullValueHandling = NullValueHandling.Ignore)]
    public string TriggerPrice { get; set; }

    [JsonIgnore]
    public OkxGridSpotAlgoStopType? SpotAlgoStopType { get; set; }

    [JsonIgnore]
    public OkxGridContractAlgoStopType? ContractAlgoStopType { get; set; }

    [JsonProperty("stopType", NullValueHandling = NullValueHandling.Ignore)]
    public string AlgoStopType
    {
        get
        {
            if (SpotAlgoStopType != null) return JsonConvert.SerializeObject(SpotAlgoStopType, new GridSpotAlgoStopTypeConverter(false));
            if (ContractAlgoStopType != null) return JsonConvert.SerializeObject(ContractAlgoStopType, new GridContractAlgoStopTypeConverter(false));
            return null;
        }
    }

}
