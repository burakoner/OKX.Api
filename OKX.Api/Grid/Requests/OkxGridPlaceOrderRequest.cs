namespace OKX.Api.Grid;

/// <summary>
/// OKX Grid Place Order Request
/// </summary>
public record OkxGridPlaceOrderRequest
{
    /// <summary>
    /// Instrument Id
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; } = string.Empty;

    /// <summary>
    /// Algo Order Type
    /// </summary>
    [JsonProperty("algoOrdType")]
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
    [JsonProperty("runType", NullValueHandling = NullValueHandling.Ignore)]
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
    public string AlgoClientOrderId { get; set; } = string.Empty;

    /// <summary>
    /// Order tag. Used for Broker Id
    /// </summary>
    [JsonProperty("tag", NullValueHandling = NullValueHandling.Ignore)]
    internal string Tag { get; set; } = string.Empty;

    /// <summary>
    /// Profit sharing ratio, it only supports these values
    /// 0,0.1,0.2,0.3
    /// 0.1 represents 10%
    /// </summary>
    [JsonProperty("profitSharingRatio", NullValueHandling = NullValueHandling.Ignore)]
    public decimal? ProfitSharingRatio { get; set; }

    /// <summary>
    /// Trigger Parameters
    /// </summary>
    [JsonProperty("triggerParams", NullValueHandling = NullValueHandling.Ignore)]
    public List<OkxGridTriggerParametersRequest> TriggerParameters { get; set; } = [];

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
    [JsonProperty("direction", NullValueHandling = NullValueHandling.Ignore)]
    public OkxGridContractDirection? ContractGridDirection { get; set; }

    /// <summary>
    /// Leverage
    /// </summary>
    [JsonProperty("lever", NullValueHandling = NullValueHandling.Ignore)]
    public int? Leverage { get; set; }

    /// <summary>
    /// Base Position
    /// </summary>
    [JsonProperty("basePos", NullValueHandling = NullValueHandling.Ignore)]
    public bool? BasePosition { get; set; }

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
}
