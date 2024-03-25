using OKX.Api.GridTrading.Converters;
using OKX.Api.GridTrading.Enums;

namespace OKX.Api.GridTrading.Models;

public class OkxGridPlaceOrderRequest
{
    [JsonProperty("instId")]
    public string InstrumentId { get; set; }

    [JsonProperty("algoOrdType"), JsonConverter(typeof(OkxGridAlgoOrderTypeConverter))]
    public OkxGridAlgoOrderType AlgoOrderType { get; set; }

    [JsonProperty("maxPx")]
    public decimal MaximumPrice { get; set; }

    [JsonProperty("minPx")]
    public decimal MinimumPrice { get; set; }

    [JsonProperty("gridNum")]
    public decimal GridNumber { get; set; }

    [JsonProperty("runType", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(OkxGridRunTypeConverter))]
    public OkxGridRunType? GridRunType { get; set; }

    [JsonProperty("tpTriggerPx", NullValueHandling = NullValueHandling.Ignore)]
    public decimal? TakeProfitTriggerPrice { get; set; }

    [JsonProperty("slTriggerPx", NullValueHandling = NullValueHandling.Ignore)]
    public decimal? StopLossTriggerPrice { get; set; }

    [JsonProperty("algoClOrdId", NullValueHandling = NullValueHandling.Ignore)]
    public string AlgoClientOrderId { get; set; }

    /// <summary>
    /// Order tag. Used for Broker Id
    /// </summary>
    [JsonProperty("tag", NullValueHandling = NullValueHandling.Ignore)]
    internal string Tag { get; set; }

    [JsonProperty("triggerParams", NullValueHandling = NullValueHandling.Ignore)]
    public List<OkxGridPlaceTriggerParameters> TriggerParameters { get; set; }

    [JsonProperty("quoteSz", NullValueHandling = NullValueHandling.Ignore)]
    public decimal? QuoteSize { get; set; }

    [JsonProperty("baseSz", NullValueHandling = NullValueHandling.Ignore)]
    public decimal? BaseSize { get; set; }

    [JsonProperty("sz", NullValueHandling = NullValueHandling.Ignore)]
    public decimal? Size { get; set; }

    [JsonProperty("direction", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(OkxGridContractDirectionConverter))]
    public OkxGridContractDirection? ContractGridDirection { get; set; }

    [JsonProperty("lever", NullValueHandling = NullValueHandling.Ignore)]
    public decimal? Leverage { get; set; }

    [JsonProperty("basePos", NullValueHandling = NullValueHandling.Ignore)]
    public bool? BasePosition { get; set; }
}

public class OkxGridPlaceTriggerParameters
{
    [JsonProperty("triggerAction"), JsonConverter(typeof(OkxGridAlgoTriggerActionConverter))]
    public OkxGridAlgoTriggerAction TriggerAction { get; set; }

    [JsonProperty("triggerStrategy"), JsonConverter(typeof(OkxGridAlgoTriggerStrategyConverter))]
    public OkxGridAlgoTriggerStrategy TriggerStrategy { get; set; }

    [JsonProperty("delaySeconds", NullValueHandling = NullValueHandling.Ignore)]
    public string DelaySeconds { get; set; }

    [JsonProperty("timeframe", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(OkxGridAlgoTimeFrameConverter))]
    public OkxGridAlgoTimeFrame? TimeFrame { get; set; }

    [JsonProperty("thold", NullValueHandling = NullValueHandling.Ignore)]
    public string Threshold { get; set; }

    [JsonProperty("triggerCond", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(OkxGridAlgoTriggerConditionConverter))]
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
            if (SpotAlgoStopType is not null) return JsonConvert.SerializeObject(SpotAlgoStopType, new OkxGridSpotAlgoStopTypeConverter(false));
            if (ContractAlgoStopType is not null) return JsonConvert.SerializeObject(ContractAlgoStopType, new OkxGridContractAlgoStopTypeConverter(false));
            return null;
        }
    }

}
