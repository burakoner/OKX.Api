namespace OKX.Api.Spread;

/// <summary>
/// OKX Spread Order
/// </summary>
public record OkxSpreadOrder
{
    /// <summary>
    /// spread ID
    /// </summary>
    [JsonProperty("sprdId")]
    public string SpreadId { get; set; } = string.Empty;
    
    /// <summary>
    /// Order ID
    /// </summary>
    [JsonProperty("ordId")]
    public long OrderId { get; set; }
    
    /// <summary>
    /// Client Order ID as assigned by the client
    /// </summary>
    [JsonProperty("clOrdId")]
    public string ClientOrderId { get; set; } = string.Empty;

    /// <summary>
    /// Order tag.
    /// </summary>
    [JsonProperty("tag")]
    public string Tag { get; set; } = string.Empty;

    /// <summary>
    /// Price
    /// </summary>
    [JsonProperty("px")]
    [JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? Price { get; set; }

    /// <summary>
    /// Quantity to buy or sell
    /// </summary>
    [JsonProperty("sz")]
    public decimal Quantity { get; set; }
    
    /// <summary>
    /// Order type
    /// </summary>
    [JsonProperty("ordType")]
    public OkxSpreadOrderType OrderType { get; set; }
    
    /// <summary>
    /// Order side
    /// </summary>
    [JsonProperty("side")]
    public OkxTradeOrderSide OrderSide { get; set; }
    
    /// <summary>
    /// Last fill quantity
    /// </summary>
    [JsonProperty("fillSz")]
    [JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? FillQuantity { get; set; }
    
    /// <summary>
    /// Last fill price
    /// </summary>
    [JsonProperty("fillPx")]
    [JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? FillPrice { get; set; }
    
    /// <summary>
    /// Last trade ID
    /// </summary>
    [JsonProperty("tradeId")]
    [JsonConverter(typeof(LongAsStringNullableConverter))]
    public long? TradeId { get; set; }
    
    /// <summary>
    /// Accumulated fill quantity
    /// </summary>
    [JsonProperty("accFillSz")]
    [JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? AccumulatedFillQuantity { get; set; }
    
    /// <summary>
    /// Live quantity
    /// </summary>
    [JsonProperty("pendingFillSz")]
    [JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? PendingFillQuantity { get; set; }
    
    /// <summary>
    /// Quantity that's pending settlement
    /// </summary>
    [JsonProperty("pendingSettleSz")]
    [JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? PendingSettleQuantity { get; set; }

    /// <summary>
    /// Quantity canceled due order cancellations or trade rejections
    /// </summary>
    [JsonProperty("canceledSz")]
    [JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? CanceledQuantity { get; set; }
    
    /// <summary>
    /// Average filled price. If none is filled, it will return "0".
    /// </summary>
    [JsonProperty("avgPx")]
    [JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? AveragePrice { get; set; }
    
    /// <summary>
    /// State
    /// </summary>
    [JsonProperty("state")]
    public OkxSpreadOrderState OrderState { get; set; }

    /// <summary>
    /// Source of the order cancellation.
    /// </summary>
    [JsonProperty("cancelSource")]
    public OkxSpreadOrderCancelSource? OrderCancelSource { get; set; }

    /// <summary>
    /// Update time, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("uTime")]
    [JsonConverter(typeof(LongAsStringNullableConverter))]
    public long? UpdateTimestamp { get; set; }

    /// <summary>
    /// Update time
    /// </summary>
    [JsonIgnore]
    public DateTime? UpdateTime => UpdateTimestamp?.ConvertFromMilliseconds();

    /// <summary>
    /// Creation time, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("cTime")]
    public long CreateTimestamp { get; set; }

    /// <summary>
    /// Creation time
    /// </summary>
    [JsonIgnore]
    public DateTime CreateTime => CreateTimestamp.ConvertFromMilliseconds();
}
