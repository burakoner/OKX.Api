namespace OKX.Api.Funding;

/// <summary>
/// OKX Convert Estimate Quote
/// </summary>
public record OkxFundingConvertEstimateQuote
{
    /// <summary>
    /// Quotation generation time, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("quoteTime")]
    public long QuoteTimestamp { get; set; }

    /// <summary>
    /// Quotation generation time
    /// </summary>
    [JsonIgnore]
    public DateTime QuoteTime => QuoteTimestamp.ConvertFromMilliseconds();
    
    /// <summary>
    /// Validity period of quotation in milliseconds
    /// </summary>
    [JsonProperty("ttlMs")]
    public long TtlMilliseconds { get; set; }
    
    /// <summary>
    /// Client Order ID as assigned by the client
    /// </summary>
    [JsonProperty("clQReqId")]
    public string ClientRequestId { get; set; } = string.Empty;
    
    /// <summary>
    /// Quote ID
    /// </summary>
    [JsonProperty("quoteId")]
    public string QuoteId { get; set; } = string.Empty;
    
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
    /// Original RFQ amount
    /// </summary>
    [JsonProperty("origRfqSz")]
    public decimal OriginalRfqAmount { get; set; }
    
    /// <summary>
    /// Real RFQ amount
    /// </summary>
    [JsonProperty("rfqSz")]
    public decimal RfqAmount { get; set; }
    
    /// <summary>
    /// RFQ currency
    /// </summary>
    [JsonProperty("rfqSzCcy")]
    public decimal RfqAmountCurrency { get; set; }
    
    /// <summary>
    /// Convert price based on quote currency
    /// </summary>
    [JsonProperty("cnvtPx")]
    public decimal ConvertPrice { get; set; }
    
    /// <summary>
    /// Convert amount of base currency
    /// </summary>
    [JsonProperty("baseSz")]
    public decimal BaseAmount { get; set; }
    
    /// <summary>
    /// Convert amount of quote currency
    /// </summary>
    [JsonProperty("quoteSz")]
    public decimal QuoteAmount { get; set; }
}
