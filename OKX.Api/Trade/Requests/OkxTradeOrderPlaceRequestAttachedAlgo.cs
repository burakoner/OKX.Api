namespace OKX.Api.Trade;

/// <summary>
/// OKX Order Algo Request
/// </summary>
public record OkxTradeOrderPlaceRequestAttachedAlgo
{
    /// <summary>
    /// Client-supplied Algo ID when placing order attaching TP/SL
    /// A combination of case-sensitive alphanumerics, all numbers, or all letters of up to 32 characters.
    /// It will be posted to algoClOrdId when placing TP/SL order once the general order is filled completely.
    /// </summary>
    [JsonProperty("attachAlgoClOrdId", NullValueHandling = NullValueHandling.Ignore)]
    public string? AttachedAlgoClientOrderId { get; set; }

    /// <summary>
    /// Take-profit trigger price
    /// For condition TP order, if you fill in this parameter, you should fill in the take-profit order price as well.
    /// </summary>
    [JsonProperty("tpTriggerPx", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? TakeProfitTriggerPrice { get; set; }

    /// <summary>
    /// Take profit trigger ratio, 0.3 represents 30%
    /// Only one of tpTriggerPx and tpTriggerRatio can be passed
    /// Only applicable to FUTURES and SWAP.
    /// If the main order is a buy order, it must be greater than 0, and if the main order is a sell order, it must be bewteen -1 and 0.
    /// </summary>
    [JsonProperty("tpTriggerRatio", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? TakeProfitTriggerRatio { get; set; }

    /// <summary>
    /// Take-profit order price
    /// 
    /// For condition TP order, if you fill in this parameter, you should fill in the take-profit trigger price as well.
    /// For limit TP order, you need to fill in this parameter, take-profit trigger needn‘t to be filled.
    /// If the price is -1, take-profit will be executed at the market price.
    /// </summary>
    [JsonProperty("tpOrdPx", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? TakeProfitOrderPrice { get; set; }

    /// <summary>
    /// TP order kind
    /// condition
    /// limit
    /// The default is condition
    /// </summary>
    [JsonProperty("tpOrdKind", NullValueHandling = NullValueHandling.Ignore)]
    public OkxAlgoOrderKind? TakeProfitOrderKind { get; set; } = OkxAlgoOrderKind.Condition;

    /// <summary>
    /// Stop-loss trigger price
    /// If you fill in this parameter, you should fill in the stop-loss order price.
    /// </summary>
    [JsonProperty("slTriggerPx", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? StopLossTriggerPrice { get; set; }

    /// <summary>
    /// Stop profit trigger ratio, 0.3 represents 30%
    /// Only one of slTriggerPx and slTriggerRatio can be passed
    /// Only applicable to FUTURES and SWAP.
    /// If the main order is a buy order, it should be bewteen 0 and 1, and if the main order is a sell order, it must be greater than 0.
    /// </summary>
    [JsonProperty("slTriggerRatio", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? StopLossTriggerRatio { get; set; }

    /// <summary>
    /// Stop-loss order price
    /// If you fill in this parameter, you should fill in the stop-loss trigger price.
    /// If the price is -1, stop-loss will be executed at the market price.
    /// </summary>
    [JsonProperty("slOrdPx", NullValueHandling = NullValueHandling.Ignore)]
    public string? StopLossOrderPrice { get; set; }

    /// <summary>
    /// Callback ratio, e.g. 0.05 represents 5%.
    /// Either callbackRatio or callbackSpread is required. Only one can be passed.
    /// Only applicable when ordType = move_order_stop.
    /// </summary>
    [JsonProperty("callbackRatio", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? CallbackRatio { get; set; }

    /// <summary>
    /// Callback spread (price distance).
    /// Either callbackRatio or callbackSpread is required. Only one can be passed.
    /// Only applicable when ordType = move_order_stop.
    /// </summary>
    [JsonProperty("callbackSpread", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? CallbackSpread { get; set; }

    /// <summary>
    /// Activation price.
    /// If not provided, the trailing stop is activated immediately upon order placement.
    /// Only applicable when ordType = move_order_stop.
    /// </summary>
    [JsonProperty("activePx", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? ActivePrice { get; set; }

    /// <summary>
    /// Take-profit trigger price type
    /// </summary>
    [JsonProperty("tpTriggerPxType", NullValueHandling = NullValueHandling.Ignore)]
    public OkxAlgoPriceType? TakeProfitTriggerPriceType { get; set; } = OkxAlgoPriceType.Last;

    /// <summary>
    /// Stop-loss trigger price type
    /// </summary>
    [JsonProperty("slTriggerPxType", NullValueHandling = NullValueHandling.Ignore)]
    public OkxAlgoPriceType? StopLossTriggerPriceType { get; set; } = OkxAlgoPriceType.Last;

    /// <summary>
    /// Size. Only applicable to TP order of split TPs, and it is required for TP order of split TPs
    /// </summary>
    [JsonProperty("sz", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? Size { get; set; }

    /// <summary>
    /// Whether to enable Cost-price SL. Only applicable to SL order of split TPs. Whether slTriggerPx will move to avgPx when the first TP order is triggered
    /// 0: disable, the default value
    /// 1: Enable
    /// </summary>
    [JsonProperty("amendPxOnTriggerType", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(BooleanAsStringConverter), "1", "0")]
    public bool AmendPriceOnTriggerType { get; set; }
}
