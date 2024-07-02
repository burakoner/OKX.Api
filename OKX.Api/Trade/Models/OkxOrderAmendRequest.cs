using OKX.Api.AlgoTrading.Converters;
using OKX.Api.AlgoTrading.Enums;

namespace OKX.Api.Trade.Models;

/// <summary>
/// OKX Order Amend Request
/// </summary>
public class OkxOrderAmendRequest
{
    /// <summary>
    /// Order Id
    /// </summary>
    [JsonProperty("ordId", NullValueHandling = NullValueHandling.Ignore)]
    public long? OrderId { get; set; }

    /// <summary>
    /// Client Order Id
    /// </summary>
    [JsonProperty("clOrdId")]
    public string ClientOrderId { get; set; }

    /// <summary>
    /// Request Id
    /// </summary>
    [JsonProperty("reqId")]
    public string RequestId { get; set; }

    /// <summary>
    /// Instrument Type
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; }

    /// <summary>
    /// Cancel on fail
    /// </summary>
    [JsonProperty("cxlOnFail", NullValueHandling = NullValueHandling.Ignore)]
    public bool? CancelOnFail { get; set; }

    /// <summary>
    /// New quantity.
    /// </summary>
    [JsonProperty("newSz", NullValueHandling = NullValueHandling.Ignore)]
    public string NewQuantity { get; set; }

    /// <summary>
    /// New price.
    /// </summary>
    [JsonProperty("newPx", NullValueHandling = NullValueHandling.Ignore)]
    public decimal? NewPrice { get; set; }

    /// <summary>
    /// Take-profit trigger price.
    /// </summary>
    [JsonProperty("newTpTriggerPx", NullValueHandling = NullValueHandling.Ignore)]
    public string TakeProfitTriggerPrice { get; set; }

    /// <summary>
    /// Take-profit order price
    /// </summary>
    [JsonProperty("newTpOrdPx", NullValueHandling = NullValueHandling.Ignore)]
    public string TakeProfitOrderPrice { get; set; }

    /// <summary>
    /// Stop-loss trigger price.
    /// </summary>
    [JsonProperty("newSlTriggerPx", NullValueHandling = NullValueHandling.Ignore)]
    public string StoplossTriggerPrice { get; set; }

    /// <summary>
    /// Stop-loss order price.
    /// </summary>
    [JsonProperty("newSlOrdPx", NullValueHandling = NullValueHandling.Ignore)]
    public string StoplossOrderPrice { get; set; }

    /// <summary>
    /// Take-profit trigger price type.
    /// </summary>
    [JsonProperty("newTpTriggerPxType", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(OkxAlgoPriceTypeConverter))]
    public OkxAlgoPriceType? TakeProfitTriggerPriceType { get; set; }

    /// <summary>
    /// Stop-loss trigger price type.
    /// </summary>
    [JsonProperty("newSlTriggerPxType", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(OkxAlgoPriceTypeConverter))]
    public OkxAlgoPriceType? StopLossTriggerPriceType { get; set; }

}