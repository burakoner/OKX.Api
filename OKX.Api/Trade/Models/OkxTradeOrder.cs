using OKX.Api.Algo.Converters;
using OKX.Api.Algo.Enums;
using OKX.Api.Public.Converters;
using OKX.Api.Public.Enums;
using OKX.Api.Trade.Converters;
using OKX.Api.Trade.Enums;

namespace OKX.Api.Trade.Models;

/// <summary>
/// OKX Order
/// </summary>
public class OkxTradeOrder
{
    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonProperty("instType"), JsonConverter(typeof(OkxInstrumentTypeConverter))]
    public OkxInstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Instrument ID
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; }

    /// <summary>
    /// Order quantity unit setting for size
    /// </summary>
    [JsonProperty("tgtCcy"), JsonConverter(typeof(OkxTradeQuantityTypeConverter))]
    public OkxTradeQuantityType? QuantityType { get; set; }

    /// <summary>
    /// Margin currency. Only applicable to cross MARGIN orders in
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    /// <summary>
    /// Order ID
    /// </summary>
    [JsonProperty("ordId")]
    public long OrderId { get; set; }

    /// <summary>
    /// Client Order ID as assigned by the client
    /// </summary>
    [JsonProperty("clOrdId")]
    public string ClientOrderId { get; set; }

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
    /// Price type of options
    /// px: Place an order based on price, in the unit of coin (the unit for the request parameter px is BTC or ETH)
    /// pxVol: Place an order based on pxVol
    /// pxUsd: Place an order based on pxUsd, in the unit of USD (the unit for the request parameter px is USD)
    /// </summary>
    [JsonProperty("pxType"), JsonConverter(typeof(OkxTradeOptionsPriceTypeConverter))]
    public OkxTradeOptionsPriceType OptionsPriceType { get; set; }

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
    [JsonProperty("ordType"), JsonConverter(typeof(OkxTradeOrderTypeConverter))]
    public OkxTradeOrderType OrderType { get; set; }

    /// <summary>
    /// Order side
    /// </summary>
    [JsonProperty("side"), JsonConverter(typeof(OkxTradeOrderSideConverter))]
    public OkxTradeOrderSide OrderSide { get; set; }

    /// <summary>
    /// Position side
    /// </summary>
    [JsonProperty("posSide"), JsonConverter(typeof(OkxTradePositionSideConverter))]
    public OkxTradePositionSide? PositionSide { get; set; }

    /// <summary>
    /// Trade mode
    /// </summary>
    [JsonProperty("tdMode"), JsonConverter(typeof(OkxTradeModeConverter))]
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
    public DateTime? FillTime { get { return FillTimestamp?.ConvertFromMilliseconds(); } }

    /// <summary>
    /// Average filled price. If none is filled, it will return "".
    /// </summary>
    [JsonProperty("avgPx")]
    public decimal? AveragePrice { get; set; }

    /// <summary>
    /// State
    /// </summary>
    [JsonProperty("state"), JsonConverter(typeof(OkxTradeOrderStateConverter))]
    public OkxTradeOrderState OrderState { get; set; }

    /// <summary>
    /// Self trade prevention ID. Orders from the same master account with the same ID will be prevented from self trade.
    /// Numerical integers defined by user in the range of 1-999999999
    /// </summary>
    [Obsolete]
    [JsonProperty("stpId")]
    public long? SelfTradePreventionId { get; set; }

    /// <summary>
    /// Self trade prevention mode
    /// Return "" if self trade prevention is not applicable
    /// </summary>
    [JsonProperty("stpMode"), JsonConverter(typeof(OkxSelfTradePreventionModeConverter))]
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
    public long? AttachedAlgoClientOrderId { get; set; }

    /// <summary>
    /// Take-profit trigger price.
    /// </summary>
    [JsonProperty("tpTriggerPx")]
    public decimal? TakeProfitTriggerPrice { get; set; }

    /// <summary>
    /// Take-profit trigger price type.
    /// </summary>
    [JsonProperty("tpTriggerPxType"), JsonConverter(typeof(OkxAlgoPriceTypeConverter))]
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
    [JsonProperty("slTriggerPxType"), JsonConverter(typeof(OkxAlgoPriceTypeConverter))]
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
    public List<OkxTradeOrderAttachedAlgoOrder> AttachedAlgoOrders{ get; set; }

    /// <summary>
    /// Linked SL order detail, only applicable to the order that is placed by one-cancels-the-other (OCO) order that contains the TP limit order.
    /// </summary>
    [JsonProperty("linkedAlgoOrd")]
    public OkxTradeOrderLinkedAlgoOrder LinkedAlgoOrder { get; set; }

    /// <summary>
    /// Fee currency
    /// </summary>
    [JsonProperty("feeCcy")]
    public string FeeCurrency { get; set; }

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
    public string RebateCurrency { get; set; }

    /// <summary>
    /// Order source
    /// 13:The generated limit order after the strategy order is triggered
    /// </summary>
    [JsonProperty("source")]
    public string Source { get; set; }

    /// <summary>
    /// Rebate amount, only applicable to spot and margin, the reward of placing orders from the platform (rebate) given to user who has reached the specified trading level. If there is no rebate, this field is "".
    /// </summary>
    [JsonProperty("rebate")]
    public decimal? Rebate { get; set; }

    /// <summary>
    /// Category
    /// </summary>
    [JsonProperty("category"), JsonConverter(typeof(OkxOrderCategoryConverter))]
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
    public string CancelSource { get; set; }

    /// <summary>
    /// Reason for the cancellation.
    /// </summary>
    [JsonProperty("cancelSourceReason")]
    public string CancelSourceReason { get; set; }

    /// <summary>
    /// Quick Margin type, Only applicable to Quick Margin Mode of isolated margin
    /// </summary>
    [JsonProperty("quickMgnType"), JsonConverter(typeof(OkxQuickMarginTypeConverter))]
    public OkxQuickMarginType? QuickMarginType { get; set; }

    /// <summary>
    /// Client-supplied Algo ID. There will be a value when algo order attaching algoClOrdId is triggered, or it will be "".
    /// </summary>
    [JsonProperty("algoClOrdId")]
    public string AlgoClientOrderId { get; set; }

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
    public DateTime? UpdateTime { get { return UpdateTimestamp?.ConvertFromMilliseconds(); } }

    /// <summary>
    /// Creation time, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("cTime")]
    public long CreateTimestamp { get; set; }

    /// <summary>
    /// Creation time
    /// </summary>
    [JsonIgnore]
    public DateTime CreateTime { get { return CreateTimestamp.ConvertFromMilliseconds(); } }
}

/// <summary>
/// OKX Order Linked Algo Order
/// </summary>
public class OkxTradeOrderLinkedAlgoOrder
{
    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonProperty("algoId")]
    public long AlgoId { get; set; }
}

/// <summary>
/// OKX Order Attached Algo Order
/// </summary>
public class OkxTradeOrderAttachedAlgoOrder
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
    public string AttachedAlgoClientOrderId { get; set; }

    /// <summary>
    /// TP order kind
    /// </summary>
    [JsonProperty("tpOrdKind")]
    public string TakeProfitOrderKind { get; set; }
    
    /// <summary>
    /// Take-profit trigger price.
    /// </summary>
    [JsonProperty("tpTriggerPx")]
    public string TakeProfitTriggerPrice { get; set; }
    
    /// <summary>
    /// Take-profit trigger price type.
    /// </summary>
    [JsonProperty("tpTriggerPxType"), JsonConverter(typeof(OkxAlgoPriceTypeConverter))]
    public OkxAlgoPriceType? TakeProfitTriggerPriceType { get; set; }

    /// <summary>
    /// Take-profit order price.
    /// </summary>
    [JsonProperty("tpOrdPx")]
    public string TakeProfitOrderPrice { get; set; }

    /// <summary>
    /// Stop-loss trigger price.
    /// </summary>
    [JsonProperty("slTriggerPx")]
    public string StopLossTriggerPrice { get; set; }

    /// <summary>
    /// Stop-loss trigger price type.
    /// </summary>
    [JsonProperty("slTriggerPxType"), JsonConverter(typeof(OkxAlgoPriceTypeConverter))]
    public OkxAlgoPriceType? StopLossTriggerPriceType { get; set; }
    
    /// <summary>
    /// Stop-loss order price.
    /// </summary>
    [JsonProperty("slOrdPx")]
    public string StopLossOrderPrice { get; set; }

    /// <summary>
    /// Size. Only applicable to TP order of split TPs
    /// </summary>
    [JsonProperty("sz")]
    public string Size { get; set; }

    /// <summary>
    /// Whether to enable Cost-price SL. Only applicable to SL order of split TPs.
    /// </summary>
    [JsonProperty("amendPxOnTriggerType")]
    public string AmendPriceOnTriggerType { get; set; }
    
    /// <summary>
    /// The error code when failing to place TP/SL order, e.g. 51020
    /// The default is ""
    /// </summary>
    [JsonProperty("failCode")]
    public string FailCode { get; set; }
    
    /// <summary>
    /// The error reason when failing to place TP/SL order.
    /// The default is ""
    /// </summary>
    [JsonProperty("failReason")]
    public string FailReason { get; set; }
}

