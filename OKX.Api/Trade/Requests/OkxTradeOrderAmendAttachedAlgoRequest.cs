namespace OKX.Api.Trade;

/// <summary>
/// OKX Order Algo Request
/// </summary>
public record OkxTradeOrderAmendAttachedAlgoRequest
{
    /// <summary>
    /// The order ID of attached TP/SL order. It can be used to identity the TP/SL order when amending. It will not be posted to algoId when placing TP/SL order after the general order is filled completely.
    /// </summary>
    [JsonProperty("attachAlgoId", NullValueHandling = NullValueHandling.Ignore)]
    public string? AttachedAlgoId { get; set; }

    /// <summary>
    /// Client-supplied Algo ID when placing order attaching TP/SL
    /// A combination of case-sensitive alphanumerics, all numbers, or all letters of up to 32 characters.
    /// It will be posted to algoClOrdId when placing TP/SL order once the general order is filled completely.
    /// </summary>
    [JsonProperty("attachAlgoClOrdId", NullValueHandling = NullValueHandling.Ignore)]
    public string? AttachedAlgoClientOrderId { get; set; }

    /// <summary>
    /// Take-profit trigger price.
    /// Either the take profit trigger price or order price is 0, it means that the take profit is deleted.
    /// </summary>
    [JsonProperty("newTpTriggerPx", NullValueHandling = NullValueHandling.Ignore)]
    public string? NewTakeProfitTriggerPrice { get; set; }

    /// <summary>
    /// Take-profit order price
    /// If the price is -1, take-profit will be executed at the market price.
    /// </summary>
    [JsonProperty("newTpOrdPx", NullValueHandling = NullValueHandling.Ignore)]
    public string? NewTakeProfitOrderPrice { get; set; }

    /// <summary>
    /// TP order kind
    /// </summary>
    [JsonProperty("newTpOrdKind", NullValueHandling = NullValueHandling.Ignore)]
    public string? NewTakeProfitOrderKind { get; set; }

    /// <summary>
    /// Stop-loss trigger price
    /// Either the stop loss trigger price or order price is 0, it means that the stop loss is deleted.
    /// </summary>
    [JsonProperty("newSlTriggerPx", NullValueHandling = NullValueHandling.Ignore)]
    public string? NewStopLossTriggerPrice { get; set; }

    /// <summary>
    /// Stop-loss order price
    /// If the price is -1, stop-loss will be executed at the market price.
    /// </summary>
    [JsonProperty("newSlOrdPx", NullValueHandling = NullValueHandling.Ignore)]
    public string? NewStopLossOrderPrice { get; set; }

    /// <summary>
    /// Take-profit trigger price type
    /// </summary>
    [JsonProperty("newTpTriggerPxType", NullValueHandling = NullValueHandling.Ignore)]
    public OkxAlgoPriceType? NewTakeProfitTriggerPriceType { get; set; }

    /// <summary>
    /// Stop-loss trigger price type
    /// </summary>
    [JsonProperty("newSlTriggerPxType", NullValueHandling = NullValueHandling.Ignore)]
    public OkxAlgoPriceType? NewStopLossTriggerPriceType { get; set; }

    /// <summary>
    /// Size. Only applicable to TP order of split TPs, and it is required for TP order of split TPs
    /// </summary>
    [JsonProperty("sz", NullValueHandling = NullValueHandling.Ignore)]
    public string? NewSize { get; set; }

    /// <summary>
    /// Whether to enable Cost-price SL. Only applicable to SL order of split TPs. Whether slTriggerPx will move to avgPx when the first TP order is triggered
    /// 0: disable, the default value
    /// 1: Enable
    /// </summary>
    [JsonProperty("amendPxOnTriggerType", NullValueHandling = NullValueHandling.Ignore)]
    public string? AmendPriceOnTriggerType { get; set; }
}