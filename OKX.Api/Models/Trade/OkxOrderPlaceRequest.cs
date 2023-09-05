﻿namespace OKX.Api.Models.Trade;

public class OkxOrderPlaceRequest
{
    /// <summary>
    /// Instrument ID
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; }

    /// <summary>
    /// Trade Mode
    /// </summary>
    [JsonProperty("tdMode"), JsonConverter(typeof(TradeModeConverter))]
    public OkxTradeMode TradeMode { get; set; }

    /// <summary>
    /// Order Side
    /// </summary>
    [JsonProperty("side"), JsonConverter(typeof(OrderSideConverter))]
    public OkxOrderSide OrderSide { get; set; }

    /// <summary>
    /// Position Side
    /// </summary>
    [JsonProperty("posSide"), JsonConverter(typeof(PositionSideConverter))]
    public OkxPositionSide PositionSide { get; set; }

    /// <summary>
    /// Order Type
    /// </summary>
    [JsonProperty("ordType"), JsonConverter(typeof(OrderTypeConverter))]
    public OkxOrderType OrderType { get; set; }

    /// <summary>
    /// Size
    /// </summary>
    [JsonProperty("sz")]
    public string Size { get; set; }

    /// <summary>
    /// Price
    /// </summary>
    [JsonProperty("px", NullValueHandling = NullValueHandling.Ignore)]
    public string Price { get; set; }

    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    /// <summary>
    /// Client Order ID
    /// </summary>
    [JsonProperty("clOrdId")]
    public string ClientOrderId { get; set; }

    /// <summary>
    /// Order tag. Used for Broker Id
    /// </summary>
    [JsonProperty("tag")]
    internal string Tag { get; set; }

    /// <summary>
    /// Whether to reduce position only or not, true false, the default is false.
    /// </summary>
    [JsonProperty("reduceOnly", NullValueHandling = NullValueHandling.Ignore)]
    public bool? ReduceOnly { get; set; }

    /// <summary>
    /// Quantity Type
    /// </summary>
    [JsonProperty("tgtCcy", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(QuantityTypeConverter))]
    public OkxQuantityType? QuantityType { get; set; }

    /// <summary>
    /// Whether to disallow the system from amending the size of the SPOT Market Order.
    /// </summary>
    [JsonProperty("banAmend")]
    internal bool? BanAmend { get; set; }

    /// <summary>
    /// Take-profit trigger price. If you fill in this parameter, you should fill in the take-profit order price as well.
    /// </summary>
    [JsonProperty("tpTriggerPx", NullValueHandling = NullValueHandling.Ignore)]
    public string TakeProfitTriggerPrice { get; set; }

    /// <summary>
    /// Take-profit order price
    /// </summary>
    [JsonProperty("tpOrdPx", NullValueHandling = NullValueHandling.Ignore)]
    public string TakeProfitOrderPrice { get; set; }

    /// <summary>
    /// Stop-loss trigger price. If you fill in this parameter, you should fill in the stop-loss order price.
    /// </summary>
    [JsonProperty("slTriggerPx", NullValueHandling = NullValueHandling.Ignore)]
    public string StoplossTriggerPrice { get; set; }

    /// <summary>
    /// Stop-loss order price. If you fill in this parameter, you should fill in the stop-loss trigger price. If the price is -1, stop-loss will be executed at the market price.
    /// </summary>
    [JsonProperty("slOrdPx", NullValueHandling = NullValueHandling.Ignore)]
    public string StoplossOrderPrice { get; set; }

    /// <summary>
    /// Take-profit trigger price type. The Default is last
    /// </summary>
    [JsonProperty("tpTriggerPxType", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(AlgoPriceTypeConverter))]
    public OkxAlgoPriceType? TakeProfitTriggerPriceType { get; set; }

    /// <summary>
    /// Stop-loss trigger price type. The Default is last
    /// </summary>
    [JsonProperty("slTriggerPxType", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(AlgoPriceTypeConverter))]
    public OkxAlgoPriceType? StopLossTriggerPriceType { get; set; }

    /// <summary>
    /// Quick Margin type. Only applicable to Quick Margin Mode of isolated margin. The default value is manual
    /// </summary>
    [JsonProperty("quickMgnType", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(QuickMarginTypeConverter))]
    public OkxQuickMarginType? QuickMarginType { get; set; }

    /// <summary>
    /// Self trade prevention ID. Orders from the same master account with the same ID will be prevented from self trade.
    /// Numerical integers defined by user in the range of 1<= x<= 999999999
    /// </summary>
    [JsonProperty("stpId", NullValueHandling = NullValueHandling.Ignore)]
    public long? SelfTradePreventionId { get; set; }

    /// <summary>
    /// Self trade prevention mode
    /// Default to cancel maker
    /// cancel_maker,cancel_taker, cancel_both
    /// Cancel both does not support FOK.
    /// </summary>
    [JsonProperty("stpMode", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(SelfTradePreventionModeConverter))]
    public OkxSelfTradePreventionMode? SelfTradePreventionMode { get; set; }
}