namespace OKX.Api.Trade;

/// <summary>
/// Order Place Request
/// </summary>
public record OkxTradeOrderPlaceRequest
{
    /// <summary>
    /// Instrument ID code.
    /// If both instId and instIdCode are provided, instIdCode takes precedence.
    /// </summary>
    [JsonProperty("instIdCode")]
    public int? InstrumentIdCode { get; set; }

    /// <summary>
    /// Instrument ID
    /// </summary>
    [JsonProperty("instId")]
    [Obsolete("Will be deprecated on February 2026.")]
    public string InstrumentId { get; set; } = string.Empty;

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
    public string? Tag { get; set; }

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
    public string Size { get; set; } = string.Empty;

    /// <summary>
    /// Order price. Only applicable to limit,post_only,fok,ioc,mmp,mmp_and_post_only order.
    /// When placing an option order, one of px/pxUsd/pxVol must be filled in, and only one can be filled in
    /// </summary>
    [JsonProperty("px", NullValueHandling = NullValueHandling.Ignore)]
    public string? Price { get; set; }

    /// <summary>
    /// Place options orders in USD
    /// Only applicable to option
    /// When placing an option order, one of px/pxUsd/pxVol must be filled in, and only one can be filled in
    /// </summary>
    [JsonProperty("pxUsd", NullValueHandling = NullValueHandling.Ignore)]
    public string? PriceUsd { get; set; }

    /// <summary>
    /// Place options orders based on implied volatility, where 1 represents 100%
    /// Only applicable to options
    /// When placing an option order, one of px/pxUsd/pxVol must be filled in, and only one can be filled in
    /// </summary>
    [JsonProperty("pxVol", NullValueHandling = NullValueHandling.Ignore)]
    public string? PriceVolatility { get; set; }

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
    /// Self trade prevention mode
    /// Default to cancel maker
    /// cancel_maker,cancel_taker, cancel_both
    /// Cancel both does not support FOK.
    /// </summary>
    [JsonProperty("stpMode", NullValueHandling = NullValueHandling.Ignore)]
    public OkxSelfTradePreventionMode? SelfTradePreventionMode { get; set; }

    /// <summary>
    /// TP/SL information attached when placing order
    /// Just for Rest API order placement
    /// </summary>
    [JsonProperty("attachAlgoOrds", NullValueHandling = NullValueHandling.Ignore)]
    public IEnumerable<OkxTradeOrderPlaceAttachedAlgoRequest>? AttachedAlgoOrders { get; set; }
}