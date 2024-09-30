namespace OKX.Api.RecurringBuy;

/// <summary>
/// Recurring Buy Sub Order
/// </summary>
public record OkxRecurringBuySubOrder
{
    /// <summary>
    /// Order Id
    /// </summary>
    [JsonProperty("algoId")]
    public long AlgoOrderId { get; set; }
    
    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonProperty("instType")]
    public OkxInstrumentType InstrumentType { get; set; }
    
    /// <summary>
    /// Instrument ID
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; } = string.Empty;
    
    /// <summary>
    /// Algo order type. recurring: recurring buy
    /// </summary>
    [JsonProperty("algoOrdType")]
    public OkxRecurringBuyType AlgoOrderType { get; set; }
    
    /// <summary>
    /// Sub order ID
    /// </summary>
    [JsonProperty("ordId")]
    public long OrderId { get; set; }
    
    /// <summary>
    /// Algo order created time, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("cTime")]
    public long CreateTimestamp { get; set; }

    /// <summary>
    /// Algo order created time
    /// </summary>
    [JsonIgnore]
    public DateTime CreateTime => CreateTimestamp.ConvertFromMilliseconds();

    /// <summary>
    /// Algo order updated  time, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("uTime")]
    public long UpdateTimestamp { get; set; }

    /// <summary>
    /// Algo order updated  time
    /// </summary>
    [JsonIgnore]
    public DateTime UpdateTime => UpdateTimestamp.ConvertFromMilliseconds();
    
    /// <summary>
    /// Sub order trade mode
    /// Margin mode : cross
    /// Non-Margin mode : cash
    /// </summary>
    [JsonProperty("tdMode")]
    public OkxTradeMode TradeMode { get; set; }
    
    /// <summary>
    /// Sub order type
    /// </summary>
    [JsonProperty("ordType")]
    public OkxTradeOrderType OrderType { get; set; }

    /// <summary>
    /// Sub order quantity to buy or sell
    /// </summary>
    [JsonProperty("sz")]
    public decimal Quantity { get; set; }
    
    /// <summary>
    /// State
    /// </summary>
    [JsonProperty("state")]
    public OkxRecurringBuyState State { get; set; }
    
    /// <summary>
    /// Sub order side
    /// </summary>
    [JsonProperty("side")]
    public OkxTradeOrderSide OrderSide { get; set; }
    
    /// <summary>
    /// Sub order limit price
    /// If it's a market order, "-1" will be return
    /// </summary>
    [JsonProperty("px")]
    public decimal Price { get; set; }
    
    /// <summary>
    /// Sub order fee
    /// </summary>
    [JsonProperty("fee")]
    public decimal Fee { get; set; }
    
    /// <summary>
    /// Sub order fee currency
    /// </summary>
    [JsonProperty("feeCcy")]
    public string FeeCurrency { get; set; } = string.Empty;
    
    /// <summary>
    /// Sub order average filled price
    /// </summary>
    [JsonProperty("avgPx")]
    public decimal AveragePrice { get; set; }
    
    /// <summary>
    /// Sub order accumulated fill quantity
    /// </summary>
    [JsonProperty("accFillSz")]
    public decimal AccumulatedFillQuantity { get; set; }
}
