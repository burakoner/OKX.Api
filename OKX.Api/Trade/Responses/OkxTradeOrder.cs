namespace OKX.Api.Trade;

/// <summary>
/// OKX Order
/// </summary>
public record OkxTradeOrder
{
    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonProperty("instType")]
    public OkxInstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Instrument ID
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; } = string.Empty;

    /// <summary>
    /// Order quantity unit setting for size
    /// </summary>
    [JsonProperty("tgtCcy")]
    public OkxTradeQuantityType? QuantityType { get; set; }

    /// <summary>
    /// Margin currency. Only applicable to cross MARGIN orders in
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Order ID
    /// </summary>
    [JsonProperty("ordId")]
    public long OrderId { get; set; }

    /// <summary>
    /// Client Order ID as assigned by the client
    /// </summary>
    [JsonProperty("clOrdId")]
    public string ClientOrderId { get; set; } = string.Empty;

    /// <summary>
    /// Price
    /// </summary>
    [JsonProperty("px")]
    public decimal? Price { get; set; }

    /// <summary>
    /// Options price in USDOnly applicable to options; return "" for other instrument types
    /// </summary>
    [JsonProperty("pxUsd")]
    public decimal? PriceUsd { get; set; }

    /// <summary>
    /// Implied volatility of the options orderOnly applicable to options; return "" for other instrument types
    /// </summary>
    [JsonProperty("pxVol")]
    public decimal? PriceVolatility { get; set; }

    /// <summary>
    /// Index price at the moment of trade execution
    /// For cross currency spot pairs, it returns baseCcy-USDT index price.For example, for LTC-ETH, this field returns the index price of LTC-USDT.
    /// </summary>
    [JsonProperty("fillIdxPx")]
    public decimal? FillIndexPrice { get; set; }

    /// <summary>
    /// Price type of options
    /// px: Place an order based on price, in the unit of coin (the unit for the request parameter px is BTC or ETH)
    /// pxVol: Place an order based on pxVol
    /// pxUsd: Place an order based on pxUsd, in the unit of USD (the unit for the request parameter px is USD)
    /// </summary>
    [JsonProperty("pxType")]
    public OkxTradeOptionsPriceType? OptionsPriceType { get; set; }

    /// <summary>
    /// Quantity to buy or sell
    /// </summary>
    [JsonProperty("sz")]
    public decimal? Quantity { get; set; }

    /// <summary>
    /// Profit and loss, Applicable to orders which have a trade and aim to close position. It always is 0 in other conditions
    /// </summary>
    [JsonProperty("pnl")]
    public decimal? ProfitLoss { get; set; }

    /// <summary>
    /// Order type
    /// </summary>
    [JsonProperty("ordType")]
    public OkxTradeOrderType OrderType { get; set; }

    /// <summary>
    /// Order side
    /// </summary>
    [JsonProperty("side")]
    public OkxTradeOrderSide OrderSide { get; set; }

    /// <summary>
    /// Position side
    /// </summary>
    [JsonProperty("posSide")]
    public OkxTradePositionSide? PositionSide { get; set; }

    /// <summary>
    /// Trade mode
    /// </summary>
    [JsonProperty("tdMode")]
    public OkxTradeMode? TradeMode { get; set; }

    /// <summary>
    /// Accumulated fill quantity
    /// </summary>
    [JsonProperty("accFillSz")]
    public decimal? AccumulatedFillQuantity { get; set; }

    /// <summary>
    /// Last filled price. If none is filled, it will return "".
    /// </summary>
    [JsonProperty("fillPx")]
    public decimal? FillPrice { get; set; }

    /// <summary>
    /// Last traded ID
    /// </summary>
    [JsonProperty("tradeId")]
    public long? TradeId { get; set; }

    /// <summary>
    /// Last filled quantity
    /// </summary>
    [JsonProperty("fillSz")]
    public decimal? FillQuantity { get; set; }

    /// <summary>
    /// Last filled time
    /// </summary>
    [JsonProperty("fillTime")]
    public long? FillTimestamp { get; set; }

    /// <summary>
    /// Update time
    /// </summary>
    [JsonIgnore]
    public DateTime? FillTime => FillTimestamp?.ConvertFromMilliseconds();

    /// <summary>
    /// Average filled price. If none is filled, it will return "".
    /// </summary>
    [JsonProperty("avgPx")]
    public decimal? AveragePrice { get; set; }

    /// <summary>
    /// State
    /// </summary>
    [JsonProperty("state")]
    public OkxTradeOrderState OrderState { get; set; }

    /// <summary>
    /// Self trade prevention mode
    /// Return "" if self trade prevention is not applicable
    /// </summary>
    [JsonProperty("stpMode")]
    public OkxSelfTradePreventionMode? SelfTradePreventionMode { get; set; }

    /// <summary>
    /// Leverage, from 0.01 to 125.
    /// </summary>
    [JsonProperty("lever")]
    public decimal? Leverage { get; set; }

    /// <summary>
    /// Client-supplied Algo ID when placing order attaching TP/SL.
    /// </summary>
    [JsonProperty("attachAlgoClOrdId")]
    public string? AttachedAlgoClientOrderId { get; set; }

    /// <summary>
    /// Take-profit trigger price.
    /// </summary>
    [JsonProperty("tpTriggerPx")]
    public decimal? TakeProfitTriggerPrice { get; set; }

    /// <summary>
    /// Take-profit trigger price type.
    /// </summary>
    [JsonProperty("tpTriggerPxType")]
    public OkxAlgoPriceType? TakeProfitTriggerPriceType { get; set; }

    /// <summary>
    /// Take-profit order price.
    /// </summary>
    [JsonProperty("tpOrdPx")]
    public decimal? TakeProfitOrderPrice { get; set; }

    /// <summary>
    /// Stop-loss trigger price.
    /// </summary>
    [JsonProperty("slTriggerPx")]
    public decimal? StopLossTriggerPrice { get; set; }

    /// <summary>
    /// Stop-loss trigger price type.
    /// </summary>
    [JsonProperty("slTriggerPxType")]
    public OkxAlgoPriceType? StopLossTriggerPriceType { get; set; }

    /// <summary>
    /// Stop-loss order price.
    /// </summary>
    [JsonProperty("slOrdPx")]
    public decimal? StopLossOrderPrice { get; set; }

    /// <summary>
    /// TP/SL information attached when placing order
    /// </summary>
    [JsonProperty("attachAlgoOrds")]
    public List<OkxTradeOrderAttachedAlgoOrder> AttachedAlgoOrders { get; set; } = [];

    /// <summary>
    /// Linked SL order detail, only applicable to the order that is placed by one-cancels-the-other (OCO) order that contains the TP limit order.
    /// </summary>
    [JsonProperty("linkedAlgoOrd")]
    public OkxTradeOrderLinkedAlgoOrder? LinkedAlgoOrder { get; set; }

    /// <summary>
    /// Fee currency
    /// </summary>
    [JsonProperty("feeCcy")]
    public string FeeCurrency { get; set; } = string.Empty;

    /// <summary>
    /// Fee and rebate
    /// For spot and margin, it is accumulated fee charged by the platform.It is always negative, e.g. -0.01.
    /// For Futures, Swap and Options, it is accumulated fee and rebate
    /// </summary>
    [JsonProperty("fee")]
    public decimal? Fee { get; set; }

    /// <summary>
    /// Rebate currency
    /// </summary>
    [JsonProperty("rebateCcy")]
    public string RebateCurrency { get; set; } = string.Empty;

    /// <summary>
    /// Order source
    /// 13:The generated limit order after the strategy order is triggered
    /// </summary>
    [JsonProperty("source")]
    public string Source { get; set; } = string.Empty;

    /// <summary>
    /// Rebate amount, only applicable to spot and margin, the reward of placing orders from the platform (rebate) given to user who has reached the specified trading level. If there is no rebate, this field is "".
    /// </summary>
    [JsonProperty("rebate")]
    public decimal? Rebate { get; set; }

    /// <summary>
    /// Category
    /// </summary>
    [JsonProperty("category")]
    public OkxOrderCategory? Category { get; set; }

    /// <summary>
    /// Whether the order can only reduce the position size. Valid options: true or false.
    /// </summary>
    [JsonProperty("reduceOnly")]
    public bool? ReduceOnly { get; set; }

    /// <summary>
    /// Whether it is TP limit order. true or false
    /// </summary>
    [JsonProperty("isTpLimit")]
    public bool? IsTakeProfitLimit { get; set; }

    /// <summary>
    /// Code of the cancellation source.
    /// </summary>
    [JsonProperty("cancelSource")]
    public string CancelSource { get; set; } = string.Empty;

    /// <summary>
    /// Reason for the cancellation.
    /// </summary>
    [JsonProperty("cancelSourceReason")]
    public string CancelSourceReason { get; set; } = string.Empty;

    /// <summary>
    /// Quick Margin type, Only applicable to Quick Margin Mode of isolated margin
    /// </summary>
    [JsonProperty("quickMgnType")]
    public OkxQuickMarginType? QuickMarginType { get; set; }

    /// <summary>
    /// Client-supplied Algo ID. There will be a value when algo order attaching algoClOrdId is triggered, or it will be "".
    /// </summary>
    [JsonProperty("algoClOrdId")]
    public string AlgoClientOrderId { get; set; } = string.Empty;

    /// <summary>
    /// Algo ID. There will be a value when algo order is triggered, or it will be "".
    /// </summary>
    [JsonProperty("algoId")]
    public long? AlgoOrderId { get; set; }

    /// <summary>
    /// Update time, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("uTime")]
    public long? UpdateTimestamp { get; set; }

    /// <summary>
    /// Update time
    /// </summary>
    [JsonIgnore]
    public DateTime? UpdateTime => UpdateTimestamp?.ConvertFromMilliseconds();

    /// <summary>
    /// Creation time, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("cTime")]
    public long CreateTimestamp { get; set; }

    /// <summary>
    /// Creation time
    /// </summary>
    [JsonIgnore]
    public DateTime CreateTime => CreateTimestamp.ConvertFromMilliseconds();
}

/// <summary>
/// OKX Order Linked Algo Order
/// </summary>
public record OkxTradeOrderLinkedAlgoOrder
{
    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonProperty("algoId")]
    public string AlgoId { get; set; } = string.Empty;
}

/// <summary>
/// OKX Order Attached Algo Order
/// </summary>
public record OkxTradeOrderAttachedAlgoOrder
{
    /// <summary>
    /// The order ID of attached TP/SL order. It can be used to identity the TP/SL order when amending. It will not be posted to algoId when placing TP/SL order after the general order is filled completely.
    /// </summary>
    [JsonProperty("attachAlgoId")]
    public long AttachedAlgoId { get; set; }

    /// <summary>
    /// Client-supplied Algo ID when placing order attaching TP/SL
    /// A combination of case-sensitive alphanumerics, all numbers, or all letters of up to 32 characters.
    /// It will be posted to algoClOrdId when placing TP/SL order once the general order is filled completely.
    /// </summary>
    [JsonProperty("attachAlgoClOrdId")]
    public string? AttachedAlgoClientOrderId { get; set; } = string.Empty;

    /// <summary>
    /// TP order kind
    /// </summary>
    [JsonProperty("tpOrdKind")]
    public string TakeProfitOrderKind { get; set; } = string.Empty;

    /// <summary>
    /// Take-profit trigger price.
    /// </summary>
    [JsonProperty("tpTriggerPx")]
    public string TakeProfitTriggerPrice { get; set; } = string.Empty;

    /// <summary>
    /// Take-profit trigger price type.
    /// </summary>
    [JsonProperty("tpTriggerPxType")]
    public OkxAlgoPriceType? TakeProfitTriggerPriceType { get; set; }

    /// <summary>
    /// Take-profit order price.
    /// </summary>
    [JsonProperty("tpOrdPx")]
    public string TakeProfitOrderPrice { get; set; } = string.Empty;

    /// <summary>
    /// Stop-loss trigger price.
    /// </summary>
    [JsonProperty("slTriggerPx")]
    public string StopLossTriggerPrice { get; set; } = string.Empty;

    /// <summary>
    /// Stop-loss trigger price type.
    /// </summary>
    [JsonProperty("slTriggerPxType")]
    public OkxAlgoPriceType? StopLossTriggerPriceType { get; set; }

    /// <summary>
    /// Stop-loss order price.
    /// </summary>
    [JsonProperty("slOrdPx")]
    public string StopLossOrderPrice { get; set; } = string.Empty;

    /// <summary>
    /// Size. Only applicable to TP order of split TPs
    /// </summary>
    [JsonProperty("sz")]
    public string Size { get; set; } = string.Empty;

    /// <summary>
    /// Whether to enable Cost-price SL. Only applicable to SL order of split TPs.
    /// </summary>
    [JsonProperty("amendPxOnTriggerType")]
    public bool? AmendPriceOnTriggerType { get; set; }

    /// <summary>
    /// The error code when failing to place TP/SL order, e.g. 51020
    /// The default is ""
    /// </summary>
    [JsonProperty("failCode")]
    public string FailCode { get; set; } = string.Empty;

    /// <summary>
    /// The error reason when failing to place TP/SL order.
    /// The default is ""
    /// </summary>
    [JsonProperty("failReason")]
    public string FailReason { get; set; } = string.Empty;
}

