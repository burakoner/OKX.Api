namespace OKX.Api.Algo;

/// <summary>
/// OKX Order Algo Request
/// </summary>
public record OkxAlgoAttachedAlgoAmendRequest
{
    /// <summary>
    /// Take-profit trigger price
    /// If you fill in this parameter, you should fill in the take-profit order price as well.
    /// </summary>
    [JsonProperty("newTpTriggerPx", NullValueHandling = NullValueHandling.Ignore)]
    public string? NewTakeProfitTriggerPrice { get; set; }

    /// <summary>
    /// Take-profit trigger price type
    /// </summary>
    [JsonProperty("newTpTriggerPxType", NullValueHandling = NullValueHandling.Ignore)]
    public OkxAlgoPriceType? NewTakeProfitTriggerPriceType { get; set; }
    
    /// <summary>
    /// Take-profit order price
    /// If you fill in this parameter, you should fill in the take-profit trigger price as well.
    /// If the price is -1, take-profit will be executed at the market price.
    /// </summary>
    [JsonProperty("newTpOrdPx", NullValueHandling = NullValueHandling.Ignore)]
    public string? NewTakeProfitOrderPrice { get; set; }

    /// <summary>
    /// Take-profit trigger ratio, 0.3 represents 30%.
    /// Only one of newTpTriggerPx and newTpTriggerRatio can be passed.
    /// Only applicable to FUTURES and SWAP.
    /// 0 means to delete the take-profit.
    /// </summary>
    [JsonProperty("newTpTriggerRatio", NullValueHandling = NullValueHandling.Ignore)]
    public string? NewTakeProfitTriggerRatio { get; set; }

    /// <summary>
    /// Stop-loss trigger price
    /// If you fill in this parameter, you should fill in the stop-loss order price.
    /// </summary>
    [JsonProperty("newSlTriggerPx", NullValueHandling = NullValueHandling.Ignore)]
    public string? NewStopLossTriggerPrice { get; set; }
    
    /// <summary>
    /// Stop-loss trigger price type
    /// </summary>
    [JsonProperty("newSlTriggerPxType", NullValueHandling = NullValueHandling.Ignore)]
    public OkxAlgoPriceType? NewStopLossTriggerPriceType { get; set; }
    
    /// <summary>
    /// Stop-loss order price
    /// If you fill in this parameter, you should fill in the stop-loss trigger price.
    /// If the price is -1, stop-loss will be executed at the market price.
    /// </summary>
    [JsonProperty("newSlOrdPx", NullValueHandling = NullValueHandling.Ignore)]
    public string? NewStopLossOrderPrice { get; set; }

    /// <summary>
    /// Stop-loss trigger ratio, 0.3 represents 30%.
    /// Only one of newSlTriggerPx and newSlTriggerRatio can be passed.
    /// Only applicable to FUTURES and SWAP.
    /// 0 means to delete the stop-loss.
    /// </summary>
    [JsonProperty("newSlTriggerRatio", NullValueHandling = NullValueHandling.Ignore)]
    public string? NewStopLossTriggerRatio { get; set; }

    /// <summary>
    /// New callback ratio, e.g. 0.05 represents 5%.
    /// Either newCallbackRatio or newCallbackSpread can be passed. Only one can be passed.
    /// Only applicable when ordType = move_order_stop.
    /// </summary>
    [JsonProperty("newCallbackRatio", NullValueHandling = NullValueHandling.Ignore)]
    public string? NewCallbackRatio { get; set; }

    /// <summary>
    /// New callback spread (price distance).
    /// Either newCallbackRatio or newCallbackSpread can be passed. Only one can be passed.
    /// Only applicable when ordType = move_order_stop.
    /// </summary>
    [JsonProperty("newCallbackSpread", NullValueHandling = NullValueHandling.Ignore)]
    public string? NewCallbackSpread { get; set; }

    /// <summary>
    /// New activation price.
    /// Only applicable when ordType = move_order_stop.
    /// </summary>
    [JsonProperty("newActivePx", NullValueHandling = NullValueHandling.Ignore)]
    public string? NewActivePrice { get; set; }
}
