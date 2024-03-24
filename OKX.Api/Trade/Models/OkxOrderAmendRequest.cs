using OKX.Api.AlgoTrade.Converters;
using OKX.Api.AlgoTrade.Enums;

namespace OKX.Api.Trade.Models;

public class OkxOrderAmendRequest
{
    [JsonProperty("ordId", NullValueHandling = NullValueHandling.Ignore)]
    public long? OrderId { get; set; }

    [JsonProperty("clOrdId")]
    public string ClientOrderId { get; set; }

    [JsonProperty("reqId")]
    public string RequestId { get; set; }

    [JsonProperty("instId")]
    public string Instrument { get; set; }

    [JsonProperty("cxlOnFail", NullValueHandling = NullValueHandling.Ignore)]
    public bool? CancelOnFail { get; set; }

    [JsonProperty("newSz", NullValueHandling = NullValueHandling.Ignore)]
    public string NewQuantity { get; set; }

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
    [JsonProperty("newTpTriggerPxType", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(AlgoPriceTypeConverter))]
    public OkxAlgoPriceType? TakeProfitTriggerPriceType { get; set; }

    /// <summary>
    /// Stop-loss trigger price type.
    /// </summary>
    [JsonProperty("newSlTriggerPxType", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(AlgoPriceTypeConverter))]
    public OkxAlgoPriceType? StopLossTriggerPriceType { get; set; }

}