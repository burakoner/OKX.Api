namespace OKX.Api.Dca;

/// <summary>
/// OKX DCA Create Order Request
/// </summary>
public record OkxDcaCreateOrderRequest
{
    /// <summary>
    /// Instrument ID
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; } = string.Empty;

    /// <summary>
    /// Algo order type
    /// </summary>
    [JsonProperty("algoOrdType")]
    public OkxDcaAlgoOrderType AlgoOrderType { get; set; }

    /// <summary>
    /// Initial order amount
    /// </summary>
    [JsonProperty("initOrdAmt")]
    public decimal InitialOrderAmount { get; set; }

    /// <summary>
    /// Whether to reinvest profit. Only applicable to contract DCA
    /// </summary>
    [JsonProperty("allowReinvest", NullValueHandling = NullValueHandling.Ignore)]
    public bool? AllowReinvest { get; set; }

    /// <summary>
    /// Safety order amount
    /// </summary>
    [JsonProperty("safetyOrdAmt", NullValueHandling = NullValueHandling.Ignore)]
    public decimal? SafetyOrderAmount { get; set; }

    /// <summary>
    /// Max number of safety orders
    /// </summary>
    [JsonProperty("maxSafetyOrds")]
    public int MaximumSafetyOrders { get; set; }

    /// <summary>
    /// Safety order price step
    /// </summary>
    [JsonProperty("pxSteps", NullValueHandling = NullValueHandling.Ignore)]
    public decimal? PriceStepRatio { get; set; }

    /// <summary>
    /// Price step multiplier
    /// </summary>
    [JsonProperty("pxStepsMult", NullValueHandling = NullValueHandling.Ignore)]
    public decimal? PriceStepMultiplier { get; set; }

    /// <summary>
    /// Safety order amount multiplier
    /// </summary>
    [JsonProperty("volMult", NullValueHandling = NullValueHandling.Ignore)]
    public decimal? VolumeMultiplier { get; set; }

    /// <summary>
    /// Take-profit target per cycle
    /// </summary>
    [JsonProperty("tpPct")]
    public decimal TakeProfitTarget { get; set; }

    /// <summary>
    /// Stop-loss target
    /// </summary>
    [JsonProperty("slPct", NullValueHandling = NullValueHandling.Ignore)]
    public decimal? StopLossTarget { get; set; }

    /// <summary>
    /// Stop-loss mode
    /// </summary>
    [JsonProperty("slMode", NullValueHandling = NullValueHandling.Ignore)]
    public OkxDcaStopLossMode? StopLossMode { get; set; }

    /// <summary>
    /// Contract DCA direction
    /// </summary>
    [JsonProperty("direction", NullValueHandling = NullValueHandling.Ignore)]
    public OkxDcaDirection? Direction { get; set; }

    /// <summary>
    /// Leverage. Only applicable to contract DCA
    /// </summary>
    [JsonProperty("lever", NullValueHandling = NullValueHandling.Ignore)]
    public decimal? Leverage { get; set; }

    /// <summary>
    /// Trigger parameters
    /// </summary>
    [JsonProperty("triggerParams")]
    public List<OkxDcaTriggerParametersRequest> TriggerParameters { get; set; } = [];

    /// <summary>
    /// Profit sharing ratio. Only applicable to contract DCA
    /// </summary>
    [JsonProperty("profitSharingRatio", NullValueHandling = NullValueHandling.Ignore)]
    public decimal? ProfitSharingRatio { get; set; }

    /// <summary>
    /// Tracking mode. Only applicable to contract DCA
    /// </summary>
    [JsonProperty("trackingMode", NullValueHandling = NullValueHandling.Ignore)]
    public OkxDcaTrackingMode? TrackingMode { get; set; }

    /// <summary>
    /// Client-supplied Algo ID
    /// </summary>
    [JsonProperty("algoClOrdId", NullValueHandling = NullValueHandling.Ignore)]
    public string? AlgoClientOrderId { get; set; }

    /// <summary>
    /// Quote currency for trading. Only applicable to spot DCA
    /// </summary>
    [JsonProperty("tradeQuoteCcy", NullValueHandling = NullValueHandling.Ignore)]
    public string? TradeQuoteCurrency { get; set; }
}
