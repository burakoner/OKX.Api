namespace OKX.Api.Dca;

/// <summary>
/// OKX DCA Amend Order Request
/// </summary>
public record OkxDcaAmendOrderRequest
{
    /// <summary>
    /// Algo order ID
    /// </summary>
    [JsonProperty("algoId")]
    public long AlgoOrderId { get; set; }

    /// <summary>
    /// Price step ratio
    /// </summary>
    [JsonProperty("pxSteps")]
    public decimal PriceStepRatio { get; set; }

    /// <summary>
    /// Price step multiplier
    /// </summary>
    [JsonProperty("pxStepsMult")]
    public decimal PriceStepMultiplier { get; set; }

    /// <summary>
    /// Amount multiplier
    /// </summary>
    [JsonProperty("volMult")]
    public decimal VolumeMultiplier { get; set; }

    /// <summary>
    /// Take-profit target
    /// </summary>
    [JsonProperty("tpPct")]
    public decimal TakeProfitTarget { get; set; }

    /// <summary>
    /// Stop-loss target
    /// </summary>
    [JsonProperty("slPct")]
    public decimal StopLossTarget { get; set; }

    /// <summary>
    /// Initial order amount
    /// </summary>
    [JsonProperty("initOrdAmt")]
    public decimal InitialOrderAmount { get; set; }

    /// <summary>
    /// Safety order amount
    /// </summary>
    [JsonProperty("safetyOrdAmt")]
    public decimal SafetyOrderAmount { get; set; }

    /// <summary>
    /// Maximum number of safety orders
    /// </summary>
    [JsonProperty("maxSafetyOrds")]
    public int MaximumSafetyOrders { get; set; }

    /// <summary>
    /// Whether to reserve all funds
    /// </summary>
    [JsonProperty("reserveFunds")]
    public bool ReserveFunds { get; set; }

    /// <summary>
    /// Trigger parameters
    /// </summary>
    [JsonProperty("triggerParams")]
    public List<OkxDcaTriggerParametersRequest> TriggerParameters { get; set; } = [];
}
