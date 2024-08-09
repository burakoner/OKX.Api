using OKX.Api.Grid.Converters;
using OKX.Api.Grid.Enums;

namespace OKX.Api.Grid.Models;

/// <summary>
/// OKX Grid Place Order Request
/// </summary>
public class OkxGridPlaceOrderRequest
{
    /// <summary>
    /// Instrument Id
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; }

    /// <summary>
    /// Algo Order Type
    /// </summary>
    [JsonProperty("algoOrdType"), JsonConverter(typeof(OkxGridAlgoOrderTypeConverter))]
    public OkxGridAlgoOrderType AlgoOrderType { get; set; }

    /// <summary>
    /// Maximum Price
    /// </summary>
    [JsonProperty("maxPx")]
    public decimal MaximumPrice { get; set; }

    /// <summary>
    /// Minimum Price
    /// </summary>
    [JsonProperty("minPx")]
    public decimal MinimumPrice { get; set; }

    /// <summary>
    /// Grid Number
    /// </summary>
    [JsonProperty("gridNum")]
    public decimal GridNumber { get; set; }

    /// <summary>
    /// Grid Run Type
    /// </summary>
    [JsonProperty("runType", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(OkxGridRunTypeConverter))]
    public OkxGridRunType? GridRunType { get; set; }

    /// <summary>
    /// Take Profit Trigger Price
    /// </summary>
    [JsonProperty("tpTriggerPx", NullValueHandling = NullValueHandling.Ignore)]
    public decimal? TakeProfitTriggerPrice { get; set; }

    /// <summary>
    /// Stop Loss Trigger Price
    /// </summary>
    [JsonProperty("slTriggerPx", NullValueHandling = NullValueHandling.Ignore)]
    public decimal? StopLossTriggerPrice { get; set; }

    /// <summary>
    /// Algo Client Order Id
    /// </summary>
    [JsonProperty("algoClOrdId", NullValueHandling = NullValueHandling.Ignore)]
    public string AlgoClientOrderId { get; set; }

    /// <summary>
    /// Order tag. Used for Broker Id
    /// </summary>
    [JsonProperty("tag", NullValueHandling = NullValueHandling.Ignore)]
    internal string Tag { get; set; }

    /// <summary>
    /// Trigger Parameters
    /// </summary>
    [JsonProperty("triggerParams", NullValueHandling = NullValueHandling.Ignore)]
    public List<OkxGridPlaceTriggerParameters> TriggerParameters { get; set; }

    /// <summary>
    /// Quote Size
    /// </summary>
    [JsonProperty("quoteSz", NullValueHandling = NullValueHandling.Ignore)]
    public decimal? QuoteSize { get; set; }

    /// <summary>
    /// Base Size
    /// </summary>
    [JsonProperty("baseSz", NullValueHandling = NullValueHandling.Ignore)]
    public decimal? BaseSize { get; set; }

    /// <summary>
    /// Size
    /// </summary>
    [JsonProperty("sz", NullValueHandling = NullValueHandling.Ignore)]
    public decimal? Size { get; set; }

    /// <summary>
    /// Contract Grid Direction
    /// </summary>
    [JsonProperty("direction", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(OkxGridContractDirectionConverter))]
    public OkxGridContractDirection? ContractGridDirection { get; set; }

    /// <summary>
    /// Leverage
    /// </summary>
    [JsonProperty("lever", NullValueHandling = NullValueHandling.Ignore)]
    public decimal? Leverage { get; set; }

    /// <summary>
    /// Base Position
    /// </summary>
    [JsonProperty("basePos", NullValueHandling = NullValueHandling.Ignore)]
    public bool? BasePosition { get; set; }
}

/// <summary>
/// OKX Grid Place Trigger Parameters
/// </summary>
public class OkxGridPlaceTriggerParameters
{
    /// <summary>
    /// Trigger Action
    /// </summary>
    [JsonProperty("triggerAction"), JsonConverter(typeof(OkxGridAlgoTriggerActionConverter))]
    public OkxGridAlgoTriggerAction TriggerAction { get; set; }

    /// <summary>
    /// Trigger Strategy
    /// </summary>
    [JsonProperty("triggerStrategy"), JsonConverter(typeof(OkxGridAlgoTriggerStrategyConverter))]
    public OkxGridAlgoTriggerStrategy TriggerStrategy { get; set; }

    /// <summary>
    /// Delay Seconds
    /// </summary>
    [JsonProperty("delaySeconds", NullValueHandling = NullValueHandling.Ignore)]
    public string DelaySeconds { get; set; }

    /// <summary>
    /// Time Frame
    /// </summary>
    [JsonProperty("timeframe", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(OkxGridAlgoTimeFrameConverter))]
    public OkxGridAlgoTimeFrame? TimeFrame { get; set; }

    /// <summary>
    /// Threshold
    /// </summary>
    [JsonProperty("thold", NullValueHandling = NullValueHandling.Ignore)]
    public string Threshold { get; set; }

    /// <summary>
    /// Trigger Condition
    /// </summary>
    [JsonProperty("triggerCond", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(OkxGridAlgoTriggerConditionConverter))]
    public OkxGridAlgoTriggerCondition? TriggerCondition { get; set; }

    /// <summary>
    /// Time Period
    /// </summary>
    [JsonProperty("timePeriod", NullValueHandling = NullValueHandling.Ignore)]
    public string TimePeriod { get; set; }

    /// <summary>
    /// Trigger Price
    /// </summary>
    [JsonProperty("triggerPx", NullValueHandling = NullValueHandling.Ignore)]
    public string TriggerPrice { get; set; }

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
            if (SpotAlgoStopType is not null) return JsonConvert.SerializeObject(SpotAlgoStopType, new OkxGridSpotAlgoStopTypeConverter(false));
            if (ContractAlgoStopType is not null) return JsonConvert.SerializeObject(ContractAlgoStopType, new OkxGridContractAlgoStopTypeConverter(false));
            return null;
        }
    }

}
