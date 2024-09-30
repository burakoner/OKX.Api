namespace OKX.Api.Funding;

/// <summary>
/// OKX Convert Order
/// </summary>
public record OkxFundingConvertOrder
{
    /// <summary>
    /// Trade ID
    /// </summary>
    [JsonProperty("tradeId")]
    public string TradeId { get; set; } = string.Empty;

    /// <summary>
    /// Quote ID
    /// </summary>
    [JsonProperty("quoteId")]
    public string QuoteId { get; set; } = string.Empty;

    /// <summary>
    /// Client Order ID as assigned by the client
    /// </summary>
    [JsonProperty("clTReqId")]
    public string ClientRequestId { get; set; } = string.Empty;

    /// <summary>
    /// Trade state
    /// fullyFilled : success
    /// rejected : failed
    /// </summary>
    [JsonProperty("state")]
    public OkxFundingConvertOrderState State { get; set; }

    /// <summary>
    /// Currency pair, e.g. BTC-USDT
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; } = string.Empty;

    /// <summary>
    /// Base currency, e.g. BTC in BTC-USDT
    /// </summary>
    [JsonProperty("baseCcy")]
    public string BaseCurrency { get; set; } = string.Empty;

    /// <summary>
    /// Quote currency, e.g. USDT in BTC-USDT
    /// </summary>
    [JsonProperty("quoteCcy")]
    public string QuoteCurrency { get; set; } = string.Empty;

    /// <summary>
    /// Trade side based on baseCcy
    /// </summary>
    [JsonProperty("side")]
    public OkxTradeOrderSide Side { get; set; }

    /// <summary>
    /// Filled price based on quote currency
    /// </summary>
    [JsonProperty("fillPx")]
    public decimal FilledPrice { get; set; }

    /// <summary>
    /// Filled amount for base currency
    /// </summary>
    [JsonProperty("fillBaseSz")]
    public decimal FilledBaseAmount { get; set; }

    /// <summary>
    /// Filled amount for quote currency
    /// </summary>
    [JsonProperty("fillQuoteSz")]
    public decimal FilledQuoteAmount { get; set; }

    /// <summary>
    /// Convert trade time, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Convert trade time
    /// </summary>
    [JsonIgnore]
    public DateTime Time => Timestamp.ConvertFromMilliseconds();
}
