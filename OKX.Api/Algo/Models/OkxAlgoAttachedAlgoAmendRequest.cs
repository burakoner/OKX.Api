using OKX.Api.Algo.Converters;
using OKX.Api.Algo.Enums;

namespace OKX.Api.Algo.Models;

/// <summary>
/// OKX Order Algo Request
/// </summary>
public class OkxAlgoAttachedAlgoAmendRequest
{
    /// <summary>
    /// Take-profit trigger price
    /// If you fill in this parameter, you should fill in the take-profit order price as well.
    /// </summary>
    [JsonProperty("newTpTriggerPx", NullValueHandling = NullValueHandling.Ignore)]
    public string NewTakeProfitTriggerPrice { get; set; }

    /// <summary>
    /// Take-profit trigger price type
    /// </summary>
    [JsonProperty("newTpTriggerPxType", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(OkxAlgoPriceTypeConverter))]
    public OkxAlgoPriceType? NewTakeProfitTriggerPriceType { get; set; }
    
    /// <summary>
    /// Take-profit order price
    /// If you fill in this parameter, you should fill in the take-profit trigger price as well.
    /// If the price is -1, take-profit will be executed at the market price.
    /// </summary>
    [JsonProperty("newTpOrdPx", NullValueHandling = NullValueHandling.Ignore)]
    public string NewTakeProfitOrderPrice { get; set; }

    /// <summary>
    /// Stop-loss trigger price
    /// If you fill in this parameter, you should fill in the stop-loss order price.
    /// </summary>
    [JsonProperty("newSlTriggerPx", NullValueHandling = NullValueHandling.Ignore)]
    public string NewStopLossTriggerPrice { get; set; }
    
    /// <summary>
    /// Stop-loss trigger price type
    /// </summary>
    [JsonProperty("newSlTriggerPxType", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(OkxAlgoPriceTypeConverter))]
    public OkxAlgoPriceType? NewStopLossTriggerPriceType { get; set; }
    
    /// <summary>
    /// Stop-loss order price
    /// If you fill in this parameter, you should fill in the stop-loss trigger price.
    /// If the price is -1, stop-loss will be executed at the market price.
    /// </summary>
    [JsonProperty("newSlOrdPx", NullValueHandling = NullValueHandling.Ignore)]
    public string NewStopLossOrderPrice { get; set; }
}