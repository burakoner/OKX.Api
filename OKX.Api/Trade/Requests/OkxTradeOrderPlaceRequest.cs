namespace OKX.Api.Trade;

/// <summary>
/// Order Place Request
/// </summary>
public record OkxTradeOrderPlaceRequest
{
    /// <summary>
    /// Instrument ID code.
    /// Required for WebSocket place order channels starting from 2026-03-26.
    /// If both instId and instIdCode are provided, instIdCode takes precedence.
    /// </summary>
    [JsonProperty("instIdCode")]
    public long? InstrumentIdCode { get; set; }

    /// <summary>
    /// Instrument ID.
    /// Required for REST order placement.
    /// Ignored by OKX for WebSocket place order channels starting from 2026-03-26; use instIdCode there.
    /// </summary>
    [JsonProperty("instId", NullValueHandling = NullValueHandling.Ignore)]
    public string? InstrumentId { get; set; }

    /// <summary>
    /// Trade Mode
    /// </summary>
    [JsonProperty("tdMode")]
    public OkxTradeMode TradeMode { get; set; }

    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("ccy", NullValueHandling = NullValueHandling.Ignore)]
    public string? Currency { get; set; }

    /// <summary>
    /// Client Order ID
    /// </summary>
    [JsonProperty("clOrdId", NullValueHandling = NullValueHandling.Ignore)]
    public string? ClientOrderId { get; set; }

    /// <summary>
    /// Order tag. Used for Broker Id
    /// </summary>
    [JsonProperty("tag", NullValueHandling = NullValueHandling.Ignore)]
    internal string? Tag { get; set; }

    /// <summary>
    /// Order Side
    /// </summary>
    [JsonProperty("side")]
    public OkxTradeOrderSide OrderSide { get; set; }

    /// <summary>
    /// Position Side
    /// </summary>
    [JsonProperty("posSide")]
    public OkxTradePositionSide PositionSide { get; set; }

    /// <summary>
    /// Order Type
    /// </summary>
    [JsonProperty("ordType")]
    public OkxTradeOrderType OrderType { get; set; }

    /// <summary>
    /// Size
    /// </summary>
    [JsonProperty("sz")]
    [JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? Size { get; set; }

    /// <summary>
    /// Order price. Only applicable to limit,post_only,fok,ioc,mmp,mmp_and_post_only order.
    /// When placing an option order, one of px/pxUsd/pxVol must be filled in, and only one can be filled in
    /// </summary>
    [JsonProperty("px", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? Price { get; set; }

    /// <summary>
    /// Place options orders in USD
    /// Only applicable to option
    /// When placing an option order, one of px/pxUsd/pxVol must be filled in, and only one can be filled in
    /// </summary>
    [JsonProperty("pxUsd", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? PriceUsd { get; set; }

    /// <summary>
    /// Place options orders based on implied volatility, where 1 represents 100%
    /// Only applicable to options
    /// When placing an option order, one of px/pxUsd/pxVol must be filled in, and only one can be filled in
    /// </summary>
    [JsonProperty("pxVol", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? PriceVolatility { get; set; }

    /// <summary>
    /// Whether to reduce position only or not, true false, the default is false.
    /// </summary>
    [JsonProperty("reduceOnly", NullValueHandling = NullValueHandling.Ignore)]
    public bool? ReduceOnly { get; set; }

    /// <summary>
    /// Quantity Type
    /// </summary>
    [JsonProperty("tgtCcy", NullValueHandling = NullValueHandling.Ignore)]
    public OkxTradeQuantityType? QuantityType { get; set; }

    /// <summary>
    /// Whether to disallow the system from amending the size of the SPOT Market Order.
    /// </summary>
    [JsonProperty("banAmend", NullValueHandling = NullValueHandling.Ignore)]
    public bool? BanAmend { get; set; }

    /// <summary>
    /// Price Amend Type
    /// </summary>
    [JsonProperty("pxAmendType", NullValueHandling = NullValueHandling.Ignore)]
    public OkxTradePriceAmendType? PriceAmendType { get; set; }

    /// <summary>
    /// The quote currency used for trading. Only applicable to SPOT.
    /// The default value is the quote currency of the instId, for example: for BTC-USD, the default is USD.
    /// </summary>
    [JsonProperty("tradeQuoteCcy", NullValueHandling = NullValueHandling.Ignore)]
    public string? TradeQuoteCurrency { get; set; }

    /// <summary>
    /// Maximum acceptable slippage for SPOT and SPOT margin market orders, expressed as a decimal fraction.
    /// Range 0 to 0.05 inclusive, with at most four decimal places (for example, 0.0123 means 1.23%).
    /// </summary>
    [JsonProperty("slippagePct", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? SlippagePercentage { get; set; }

    /// <summary>
    /// Self trade prevention mode
    /// Default to cancel maker
    /// cancel_maker,cancel_taker, cancel_both
    /// Cancel both does not support FOK.
    /// </summary>
    [JsonProperty("stpMode", NullValueHandling = NullValueHandling.Ignore)]
    public OkxSelfTradePreventionMode? SelfTradePreventionMode { get; set; }

    /// <summary>
    /// Deprecated ELP-named alias for <see cref="RpiTakerAccess"/>.
    /// If both fields are sent, RpiTakerAccess takes precedence.
    /// </summary>
    [Obsolete("Use RpiTakerAccess. OKX accepts isElpTakerAccess only through October 31, 2026.")]
    [JsonProperty("isElpTakerAccess", NullValueHandling = NullValueHandling.Ignore)]
    public bool? IsElpTakerAccess { get; set; }

    /// <summary>
    /// Whether the order can access RPI liquidity. Default false.
    /// Applicable to standard order types. A speed bump applies when enabled.
    /// </summary>
    [JsonProperty("rpiTakerAccess", NullValueHandling = NullValueHandling.Ignore)]
    public bool? RpiTakerAccess { get; set; }

    /// <summary>
    /// Whether an RPI maker price that violates the spacing rule may be rounded outward to the nearest placeable,
    /// non-crossing level. Default false. Effective only for rpi orders and ignored for OPTION and EVENTS.
    /// </summary>
    [JsonProperty("rpiPxRound", NullValueHandling = NullValueHandling.Ignore)]
    public bool? RpiPriceRound { get; set; }

    /// <summary>
    /// Event contract speed bump flag.
    /// Required for non-post-only EVENTS orders.
    /// </summary>
    [JsonProperty("speedBump", NullValueHandling = NullValueHandling.Ignore)]
    public OkxTradeEventSpeedBump? SpeedBump { get; set; }

    /// <summary>
    /// Event contract outcome side.
    /// Only applicable and required for EVENTS.
    /// </summary>
    [JsonProperty("outcome", NullValueHandling = NullValueHandling.Ignore)]
    public OkxTradeEventOutcome? Outcome { get; set; }

    /// <summary>
    /// TP/SL information attached when placing order
    /// Just for Rest API order placement
    /// </summary>
    [JsonProperty("attachAlgoOrds", NullValueHandling = NullValueHandling.Ignore)]
    public IEnumerable<OkxTradeOrderPlaceRequestAttachedAlgo>? AttachedAlgoOrders { get; set; }

    internal void Validate()
    {
        ValidateSlippagePercentage(SlippagePercentage, nameof(SlippagePercentage));
    }

    internal static void ValidateSlippagePercentage(decimal? slippagePercentage, string parameterName)
    {
        if (slippagePercentage is null)
            return;
        if (slippagePercentage < 0m || slippagePercentage > 0.05m)
            throw new ArgumentOutOfRangeException(parameterName, slippagePercentage, "Slippage percentage must be between 0 and 0.05 inclusive.");
        if (decimal.Round(slippagePercentage.Value, 4) != slippagePercentage.Value)
            throw new ArgumentException("Slippage percentage can have at most four decimal places.", parameterName);
    }
}
