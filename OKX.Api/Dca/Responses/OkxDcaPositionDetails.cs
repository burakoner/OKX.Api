namespace OKX.Api.Dca;

/// <summary>
/// OKX DCA Position Details
/// </summary>
public record OkxDcaPositionDetails
{
    /// <summary>
    /// Algo ID
    /// </summary>
    [JsonProperty("algoId")]
    public long AlgoOrderId { get; set; }

    /// <summary>
    /// Client-supplied Algo ID
    /// </summary>
    [JsonProperty("algoClOrdId")]
    public string AlgoClientOrderId { get; set; } = string.Empty;

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
    /// Current cycle ID
    /// </summary>
    [JsonIgnore]
    public long? CurrentCycleId { get; set; }

    [JsonProperty("curCycleld"), JsonConverter(typeof(LongAsStringNullableConverter))]
    private long? CurrentCycleIdTypo
    {
        set
        {
            if (value.HasValue)
                CurrentCycleId = value.Value;
        }
    }

    [JsonProperty("curCycleId"), JsonConverter(typeof(LongAsStringNullableConverter))]
    private long? CurrentCycleIdAlias
    {
        set
        {
            if (value.HasValue)
                CurrentCycleId = value.Value;
        }
    }

    /// <summary>
    /// Start time, Unix timestamp format in milliseconds
    /// </summary>
    [JsonProperty("startTime"), JsonConverter(typeof(LongAsStringNullableConverter))]
    public long? StartTimestamp { get; set; }

    /// <summary>
    /// Start time
    /// </summary>
    [JsonIgnore]
    public DateTime? StartTime => StartTimestamp?.ConvertFromMilliseconds();

    /// <summary>
    /// Filled manual orders
    /// </summary>
    [JsonProperty("fillManualOrds"), JsonConverter(typeof(IntAsStringNullableConverter))]
    public int? FilledManualOrders { get; set; }

    /// <summary>
    /// Filled safety orders
    /// </summary>
    [JsonProperty("fillSafetyOrds"), JsonConverter(typeof(IntAsStringNullableConverter))]
    public int? FilledSafetyOrders { get; set; }

    /// <summary>
    /// Funding fee
    /// </summary>
    [JsonProperty("fundingFee"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? FundingFee { get; set; }

    /// <summary>
    /// Initial price
    /// </summary>
    [JsonProperty("initPx"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? InitialPrice { get; set; }

    /// <summary>
    /// Notional value in USD
    /// </summary>
    [JsonProperty("notionalUsd"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? NotionalUsd { get; set; }

    /// <summary>
    /// Average price
    /// </summary>
    [JsonProperty("avgPx"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? AveragePrice { get; set; }

    /// <summary>
    /// Unrealized PnL
    /// </summary>
    [JsonProperty("upl"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? UnrealizedProfitAndLoss { get; set; }

    /// <summary>
    /// Estimated liquidation price
    /// </summary>
    [JsonProperty("liqPx"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? LiquidationPrice { get; set; }

    /// <summary>
    /// Position size in number of contracts
    /// </summary>
    [JsonProperty("sz"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? Size { get; set; }

    /// <summary>
    /// Amount of base currency held in the current cycle
    /// </summary>
    [JsonProperty("baseSz"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? BaseSize { get; set; }

    /// <summary>
    /// Amount of quote currency held in the current cycle
    /// </summary>
    [JsonProperty("quoteSz"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? QuoteSize { get; set; }

    /// <summary>
    /// Stop-loss price
    /// </summary>
    [JsonProperty("slPx"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? StopLossPrice { get; set; }

    /// <summary>
    /// Take-profit price
    /// </summary>
    [JsonProperty("tpPx"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? TakeProfitPrice { get; set; }

    /// <summary>
    /// Fee
    /// </summary>
    [JsonProperty("fee"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? Fee { get; set; }

    /// <summary>
    /// The quote currency for trading
    /// </summary>
    [JsonProperty("tradeQuoteCcy")]
    public string? TradeQuoteCurrency { get; set; }
}
