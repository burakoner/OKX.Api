namespace OKX.Api.Dca;

/// <summary>
/// OKX DCA Order
/// </summary>
public record OkxDcaOrder
{
    /// <summary>
    /// Algo ID
    /// </summary>
    [JsonProperty("algoId")]
    public long AlgoOrderId { get; set; }

    /// <summary>
    /// Algo order type
    /// </summary>
    [JsonProperty("algoOrdType")]
    public OkxDcaAlgoOrderType AlgoOrderType { get; set; }

    /// <summary>
    /// Instrument ID
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; } = string.Empty;

    /// <summary>
    /// Copy order type
    /// </summary>
    [JsonProperty("copyType")]
    public OkxProfitSharingOrderType? CopyType { get; set; }

    /// <summary>
    /// Algo order state
    /// </summary>
    [JsonProperty("state")]
    public string State { get; set; } = string.Empty;

    /// <summary>
    /// Cancel type
    /// </summary>
    [JsonProperty("cancelType")]
    public string CancelType { get; set; } = string.Empty;

    /// <summary>
    /// Position direction
    /// </summary>
    [JsonProperty("direction")]
    public OkxDcaDirection? Direction { get; set; }

    /// <summary>
    /// Leverage
    /// </summary>
    [JsonProperty("lever"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? Leverage { get; set; }

    /// <summary>
    /// Initial order amount
    /// </summary>
    [JsonProperty("initOrdAmt"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? InitialOrderAmount { get; set; }

    /// <summary>
    /// Safety order amount
    /// </summary>
    [JsonProperty("safetyOrdAmt"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? SafetyOrderAmount { get; set; }

    /// <summary>
    /// Maximum number of safety orders
    /// </summary>
    [JsonProperty("maxSafetyOrds"), JsonConverter(typeof(IntAsStringNullableConverter))]
    public int? MaximumSafetyOrders { get; set; }

    /// <summary>
    /// Price step ratio
    /// </summary>
    [JsonProperty("pxSteps"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? PriceStepRatio { get; set; }

    /// <summary>
    /// Price step multiplier
    /// </summary>
    [JsonProperty("pxStepsMult"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? PriceStepMultiplier { get; set; }

    /// <summary>
    /// Volume multiplier
    /// </summary>
    [JsonProperty("volMult"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? VolumeMultiplier { get; set; }

    /// <summary>
    /// Take-profit target
    /// </summary>
    [JsonProperty("tpPct"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? TakeProfitTarget { get; set; }

    /// <summary>
    /// Take-profit price limit
    /// </summary>
    [JsonProperty("tpPxRange"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? TakeProfitPriceRange { get; set; }

    /// <summary>
    /// Stop-loss target
    /// </summary>
    [JsonProperty("slPct"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? StopLossTarget { get; set; }

    /// <summary>
    /// Stop-loss mode
    /// </summary>
    [JsonProperty("slMode")]
    public string StopLossMode { get; set; } = string.Empty;

    /// <summary>
    /// Whether to reinvest profit
    /// </summary>
    [JsonProperty("allowReinvest")]
    public bool? AllowReinvest { get; set; }

    /// <summary>
    /// Total PnL
    /// </summary>
    [JsonProperty("totalPnl"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? TotalProfitAndLoss { get; set; }

    /// <summary>
    /// PnL ratio
    /// </summary>
    [JsonProperty("pnlRatio"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? ProfitAndLossRatio { get; set; }

    /// <summary>
    /// Funding fee
    /// </summary>
    [JsonProperty("fundingFee"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? FundingFee { get; set; }

    // Ongoing-list currently documents the same value as totalFundingFee.
    [JsonProperty("totalFundingFee"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    private decimal? FundingFeeAlias
    {
        set
        {
            if (value.HasValue)
                FundingFee = value;
        }
    }

    /// <summary>
    /// Investment amount
    /// </summary>
    [JsonProperty("investmentAmt"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? InvestmentAmount { get; set; }

    /// <summary>
    /// Investment currency
    /// </summary>
    [JsonProperty("investmentCcy")]
    public string InvestmentCurrency { get; set; } = string.Empty;

    /// <summary>
    /// Arbitrage PnL
    /// </summary>
    [JsonProperty("arbitragePnL"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? ArbitrageProfitAndLoss { get; set; }

    /// <summary>
    /// Net transfer in margin
    /// </summary>
    [JsonProperty("transferInMargin"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? TransferInMargin { get; set; }

    /// <summary>
    /// Profit sharing ratio
    /// </summary>
    [JsonProperty("profitSharingRatio"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? ProfitSharingRatio { get; set; }

    /// <summary>
    /// Tracking mode
    /// </summary>
    [JsonProperty("trackingMode")]
    public OkxDcaTrackingMode? TrackingMode { get; set; }

    /// <summary>
    /// Trigger parameters
    /// </summary>
    [JsonProperty("triggerParams")]
    public List<OkxDcaTriggerParameters> TriggerParameters { get; set; } = [];

    /// <summary>
    /// Contract value
    /// </summary>
    [JsonProperty("ctVal"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? ContractValue { get; set; }

    /// <summary>
    /// Quote currency for trading
    /// </summary>
    [JsonProperty("tradeQuoteCcy")]
    public string? TradeQuoteCurrency { get; set; }

    /// <summary>
    /// Create time, Unix timestamp format in milliseconds
    /// </summary>
    [JsonProperty("cTime")]
    public long CreateTimestamp { get; set; }

    /// <summary>
    /// Create time
    /// </summary>
    [JsonIgnore]
    public DateTime CreateTime => CreateTimestamp.ConvertFromMilliseconds();

    /// <summary>
    /// Update time, Unix timestamp format in milliseconds
    /// </summary>
    [JsonProperty("uTime")]
    public long UpdateTimestamp { get; set; }

    /// <summary>
    /// Update time
    /// </summary>
    [JsonIgnore]
    public DateTime UpdateTime => UpdateTimestamp.ConvertFromMilliseconds();
}
