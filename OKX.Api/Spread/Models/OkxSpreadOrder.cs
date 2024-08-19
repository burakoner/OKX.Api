using OKX.Api.Spread.Converters;
using OKX.Api.Spread.Enums;
using OKX.Api.Trade.Converters;
using OKX.Api.Trade.Enums;

namespace OKX.Api.Spread.Models;

/// <summary>
/// OKX Spread Order
/// </summary>
public class OkxSpreadOrder
{
    /// <summary>
    /// spread ID
    /// </summary>
    [JsonProperty("sprdId")]
    public string SpreadId { get; set; }
    
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
    /// Quantity to buy or sell
    /// </summary>
    [JsonProperty("sz")]
    public decimal Quantity { get; set; }
    
    /// <summary>
    /// Order type
    /// </summary>
    [JsonProperty("ordType"), JsonConverter(typeof(OkxSpreadOrderTypeConverter))]
    public OkxSpreadOrderType OrderType { get; set; }
    
    /// <summary>
    /// Order side
    /// </summary>
    [JsonProperty("side"), JsonConverter(typeof(OkxOrderSideConverter))]
    public OkxOrderSide OrderSide { get; set; }
    
    /// <summary>
    /// Last fill quantity
    /// </summary>
    [JsonProperty("fillSz")]
    public decimal? FillQuantity { get; set; }
    
    /// <summary>
    /// Last fill price
    /// </summary>
    [JsonProperty("fillPx")]
    public decimal? FillPrice { get; set; }
    
    /// <summary>
    /// Last trade ID
    /// </summary>
    [JsonProperty("tradeId")]
    public long? TradeId { get; set; }
    
    /// <summary>
    /// Accumulated fill quantity
    /// </summary>
    [JsonProperty("accFillSz")]
    public decimal? AccumulatedFillQuantity { get; set; }
    
    /// <summary>
    /// Live quantity
    /// </summary>
    [JsonProperty("pendingFillSz")]
    public decimal? PendingFillQuantity { get; set; }
    
    /// <summary>
    /// Quantity that's pending settlement
    /// </summary>
    [JsonProperty("pendingSettleSz")]
    public decimal? PendingSettleQuantity { get; set; }

    /// <summary>
    /// Quantity canceled due order cancellations or trade rejections
    /// </summary>
    [JsonProperty("canceledSz")]
    public decimal? CanceledQuantity { get; set; }
    
    /// <summary>
    /// Average filled price. If none is filled, it will return "0".
    /// </summary>
    [JsonProperty("avgPx")]
    public decimal? AveragePrice { get; set; }
    
    /// <summary>
    /// State
    /// </summary>
    [JsonProperty("state"), JsonConverter(typeof(OkxSpreadOrderStateConverter))]
    public OkxSpreadOrderState OrderState { get; set; }

    /// <summary>
    /// Source of the order cancellation.
    /// </summary>
    [JsonProperty("cancelSource"), JsonConverter(typeof(OkxSpreadOrderCancelSourceConverter))]
    public OkxSpreadOrderCancelSource OrderCancelSource { get; set; }

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