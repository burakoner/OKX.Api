﻿namespace OKX.Api.Algo;

/// <summary>
/// OKX Algo Order
/// </summary>
public record OkxAlgoOrder
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
    /// Margin currency
    /// Only applicable to cross MARGIN orders in Single-currency margin.
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Order ID list
    /// </summary>
    [JsonProperty("ordIdList")]
    public List<long> OrderIdList { get; set; } = [];

    /// <summary>
    /// Algo ID
    /// </summary>
    [JsonProperty("algoId")]
    public long? AlgoId { get; set; }

    /// <summary>
    /// Client Order ID as assigned by the client
    /// </summary>
    [JsonProperty("clOrdId")]
    public string ClientOrderId { get; set; } = string.Empty;

    /// <summary>
    /// Quantity to buy or sell
    /// </summary>
    [JsonProperty("sz")]
    public decimal? Quantity { get; set; }

    /// <summary>
    /// Fraction of position to be closed when the algo order is triggered
    /// </summary>
    [JsonProperty("closeFraction")]
    public decimal? CloseFraction { get; set; }

    /// <summary>
    /// Order type
    /// </summary>
    [JsonProperty("ordType")]
    public OkxAlgoOrderType OrderType { get; set; }

    /// <summary>
    /// Order side
    /// </summary>
    [JsonProperty("side")]
    public OkxTradeOrderSide OrderSide { get; set; }

    /// <summary>
    /// Position side
    /// </summary>
    [JsonProperty("posSide")]
    public OkxTradePositionSide PositionSide { get; set; }

    /// <summary>
    /// Trade mode
    /// </summary>
    [JsonProperty("tdMode")]
    public OkxTradeMode TradeMode { get; set; }

    /// <summary>
    /// Order quantity unit setting for sz
    /// base_ccy: Base currency, quote_ccy: Quote currency
    /// Only applicable to SPOT Market Orders
    /// Default is quote_ccy for buy, base_ccy for sell
    /// </summary>
    [JsonProperty("tgtCcy")]
    public OkxTradeQuantityType? QuantityType { get; set; }

    /// <summary>
    /// State
    /// </summary>
    [JsonProperty("state")]
    public OkxAlgoOrderState State { get; set; }

    /// <summary>
    /// Leverage, from 0.01 to 125.
    /// Only applicable to MARGIN/FUTURES/SWAP
    /// </summary>
    [JsonProperty("lever")]
    public decimal? Leverage { get; set; }

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
    /// trigger price.
    /// </summary>
    [JsonProperty("triggerPx")]
    public decimal? TriggerPrice { get; set; }

    /// <summary>
    /// trigger price type.
    /// </summary>
    [JsonProperty("triggerPxType")]
    public OkxAlgoPriceType? TriggerPriceType { get; set; }

    /// <summary>
    /// Order price
    /// </summary>
    [JsonProperty("ordPx")]
    public decimal? OrderPrice { get; set; }

    /// <summary>
    /// Actual order quantity
    /// </summary>
    [JsonProperty("actualSz")]
    public decimal? ActualOrderQuantity { get; set; }

    /// <summary>
    /// Actual order price
    /// </summary>
    [JsonProperty("actualPx")]
    public decimal? ActualOrderPrice { get; set; }

    /// <summary>
    /// Actual trigger side, tp: take profit sl: stop loss
    /// </summary>
    [JsonProperty("actualSide")]
    public OkxAlgoActualSide? ActualSide { get; set; }

    /// <summary>
    /// Trigger time, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("triggerTime")]
    public long? TriggerTimestamp { get; set; }

    /// <summary>
    /// Trigger time
    /// </summary>
    [JsonIgnore]
    public DateTime? TriggerTime => TriggerTimestamp?.ConvertFromMilliseconds();

    /// <summary>
    /// Price ratio
    /// Only applicable to iceberg order or twap order
    /// </summary>
    [JsonProperty("pxVar")]
    public decimal? PriceVariance { get; set; }

    /// <summary>
    /// Price variance
    /// Only applicable to iceberg order or twap order
    /// </summary>
    [JsonProperty("pxSpread")]
    public decimal? PriceSpread { get; set; }

    /// <summary>
    /// Average amount
    /// Only applicable to iceberg order or twap order
    /// </summary>
    [JsonProperty("szLimit")]
    public decimal? AverageQuantity { get; set; }

    /// <summary>
    /// Price Limit
    /// Only applicable to iceberg order or twap order
    /// </summary>
    [JsonProperty("pxLimit")]
    public decimal? PriceLimit { get; set; }

    /// <summary>
    /// Time interval
    /// Only applicable to twap order
    /// </summary>
    [JsonProperty("timeInterval")]
    public long? TimeInterval { get; set; }

    /// <summary>
    /// Callback price ratio
    /// </summary>
    [JsonProperty("callbackRatio")]
    public decimal? CallbackRatio { get; set; }

    /// <summary>
    /// Callback price variance
    /// </summary>
    [JsonProperty("callbackSpread")]
    public decimal? CallbackSpread { get; set; }

    /// <summary>
    /// Active price
    /// </summary>
    [JsonProperty("activePx")]
    public decimal? ActivePrice { get; set; }

    /// <summary>
    /// Trigger price
    /// </summary>
    [JsonProperty("moveTriggerPx")]
    public decimal? MoveTriggerPrice { get; set; }

    /// <summary>
    /// Whether the order can only reduce the position size. Valid options: true or false.
    /// </summary>
    [JsonProperty("reduceOnly")]
    public bool? ReduceOnly { get; set; }

    /// <summary>
    /// Quick Margin type, Only applicable to Quick Margin Mode of isolated margin
    /// </summary>
    [JsonProperty("quickMgnType")]
    public OkxQuickMarginType? QuickMarginType { get; set; }

    /// <summary>
    /// Last filled price while placing
    /// </summary>
    [JsonProperty("last")]
    public decimal? LastPrice { get; set; }

    /// <summary>
    /// It represents that the reason that algo order fails to trigger. It is "" when the state is effective/canceled.
    /// There will be value when the state is order_failed, e.g. 51008;
    /// Only applicable to Stop Order, Trailing Stop Order, Trigger order.
    /// </summary>
    [JsonProperty("failCode")]
    public string FailureCode { get; set; } = string.Empty;

    /// <summary>
    /// Client Algo Order ID as assigned by the client.
    /// </summary>
    [JsonProperty("algoClOrdId")]
    public string AlgoClientOrderId { get; set; } = string.Empty;

    /// <summary>
    /// Whether to enable Cost-price SL. Only applicable to SL order of split TPs.
    /// 0: disable, the default value
    /// 1: Enable
    /// </summary>
    [JsonProperty("amendPxOnTriggerType")]
    public bool? AmendPriceOnTriggerType { get; set; }

    /// <summary>
    /// Attached SL/TP orders info. Applicable to Spot and futures mode/Multi-currency margin/Portfolio margin
    /// </summary>
    [JsonProperty("attachAlgoOrds")]
    public List<OkxAlgoAttachedAlgoOrder> AttachedAlgoOrders { get; set; } = [];

    /// <summary>
    /// Linked TP order detail, only applicable to SL order that comes from the one-cancels-the-other (OCO) order that contains the TP limit order.
    /// </summary>
    [JsonProperty("linkedOrd")]
    public OkxAlgoLinkedOrder? LinkedOrder { get; set; }

    /// <summary>
    /// Creation time Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("cTime")]
    public long CreateTimestamp { get; set; }

    /// <summary>
    /// Creation time
    /// </summary>
    [JsonIgnore]
    public DateTime CreateTime => CreateTimestamp.ConvertFromMilliseconds();

    /// <summary>
    /// Creation time Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("uTime")]
    public long? UpdateTimestamp { get; set; }

    /// <summary>
    /// Order updated time
    /// </summary>
    [JsonIgnore]
    public DateTime? UpdateTime => UpdateTimestamp?.ConvertFromMilliseconds();
    
    /// <summary>
    /// Whether borrowing currency automatically
    /// Only applicable to trigger order, trailing order and twap order
    /// </summary>
    [JsonProperty("isTradeBorrowMode")]
    public bool? IsTradeBorrowMode { get; set; }

    /// <summary>
    /// Chase type. Only applicable to chase order.
    /// </summary>
    [JsonProperty("chaseType")]
    public OkxAlgoChaseType? ChaseType { get; set; }

    /// <summary>
    /// Chase value. Only applicable to chase order.
    /// </summary>
    [JsonProperty("chaseVal")]
    public decimal? ChaseValue { get; set; }

    /// <summary>
    /// Maximum chase type. Only applicable to chase order.
    /// </summary>
    [JsonProperty("maxChaseType")]
    public OkxAlgoChaseType? MaxChaseType { get; set; }

    /// <summary>
    /// Maximum chase value. Only applicable to chase order.
    /// </summary>
    [JsonProperty("maxChaseVal")]
    public decimal? MaxChaseValue { get; set; }

    /// <summary>
    /// The quote currency used for trading.
    /// </summary>
    [JsonProperty("tradeQuoteCcy")]
    public string? TradeQuoteCurrency { get; set; }
}
