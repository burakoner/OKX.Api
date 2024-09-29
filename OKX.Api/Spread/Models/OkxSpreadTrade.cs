namespace OKX.Api.Spread;

/// <summary>
/// OKX Spread Trade
/// </summary>
public class OkxSpreadTrade
{
    /// <summary>
    /// Spread ID
    /// </summary>
    [JsonProperty("sprdId")]
    public string SpreadId { get; set; }

    /// <summary>
    /// Trade ID
    /// </summary>
    [JsonProperty("tradeId")]
    public long TradeId { get; set; }

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
    /// Filled price
    /// </summary>
    [JsonProperty("fillPx")]
    public decimal FilledPrice { get; set; }

    /// <summary>
    /// Filled quantity
    /// </summary>
    [JsonProperty("fillSz")]
    public decimal FilledQuantity { get; set; }

    /// <summary>
    /// Order side, buy sell
    /// </summary>
    [JsonProperty("side"), JsonConverter(typeof(OkxTradeOrderSideConverter))]
    public OkxTradeOrderSide OrderSide { get; set; }

    /// <summary>
    /// Trade state.
    /// Valid values are filled and rejected
    /// </summary>
    [JsonProperty("state"), JsonConverter(typeof(OkxSpreadTradeStateConverter))]
    public OkxSpreadTradeState State { get; set; }

    /// <summary>
    /// Liquidity taker or maker, T: taker M: maker
    /// </summary>
    [JsonProperty("execType"), JsonConverter(typeof(OkxTradeOrderRoleConverter))]
    public OkxTradeOrderRole Role { get; set; }

    /// <summary>
    /// Data generation time, Unix timestamp format in milliseconds, e.g. 1597026383085.
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Data generation time
    /// </summary>
    [JsonIgnore]
    public DateTime Time => Timestamp.ConvertFromMilliseconds();
    
    /// <summary>
    /// Legs of trade
    /// </summary>
    [JsonProperty("legs")]
    public List<OkxSpreadTradeLeg> Legs { get; set; }=[];

    /// <summary>
    /// Error Code
    /// </summary>
    [JsonProperty("code")]
    public string ErrorCode { get; set; }

    /// <summary>
    /// Error Message
    /// </summary>
    [JsonProperty("msg")]
    public string ErrorMessage { get; set; }
}

/// <summary>
/// OKX Spread Trade Leg
/// </summary>
public class OkxSpreadTradeLeg
{
    /// <summary>
    /// Instrument ID, e.g. BTC-USDT-SWAP
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; }
    
    /// <summary>
    /// The price the leg executed
    /// </summary>
    [JsonProperty("px")]
    public decimal Price { get; set; }

    /// <summary>
    /// The size of each leg
    /// </summary>
    [JsonProperty("sz")]
    public decimal Quantity { get; set; }

    /// <summary>
    /// The direction of the leg. Valid value can be buy or sell.
    /// </summary>
    [JsonProperty("side"), JsonConverter(typeof(OkxTradeOrderSideConverter))]
    public OkxTradeOrderSide OrderSide { get; set; }
    
    /// <summary>
    /// Fee. Negative number represents the user transaction fee charged by the platform. Positive number represents rebate.
    /// </summary>
    [JsonProperty("fee")]
    public decimal FeeQuantity { get; set; }

    /// <summary>
    /// Fee currency
    /// </summary>
    [JsonProperty("feeCcy")]
    public string FeeCurrency { get; set; }

    /// <summary>
    /// Traded ID in the OKX orderbook.
    /// </summary>
    [JsonProperty("tradeId")]
    public long TradeId { get; set; }
}