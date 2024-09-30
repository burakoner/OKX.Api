namespace OKX.Api.Spread;

/// <summary>
/// OKX Spread Trade
/// </summary>
public record OkxSpreadTrade
{
    /// <summary>
    /// Spread ID
    /// </summary>
    [JsonProperty("sprdId")]
    public string SpreadId { get; set; } = string.Empty;

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
    public string ClientOrderId { get; set; } = string.Empty;

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
    [JsonProperty("side")]
    public OkxTradeOrderSide OrderSide { get; set; }

    /// <summary>
    /// Trade state.
    /// Valid values are filled and rejected
    /// </summary>
    [JsonProperty("state")]
    public OkxSpreadTradeState State { get; set; }

    /// <summary>
    /// Liquidity taker or maker, T: taker M: maker
    /// </summary>
    [JsonProperty("execType")]
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
    public string ErrorCode { get; set; } = string.Empty;

    /// <summary>
    /// Error Message
    /// </summary>
    [JsonProperty("msg")]
    public string ErrorMessage { get; set; } = string.Empty;
}
