namespace OKX.Api.Funding;

/// <summary>
/// OKX buy/sell quote
/// </summary>
public record OkxFundingBuySellQuote
{
    /// <summary>
    /// Quote identifier.
    /// </summary>
    [JsonProperty("quoteId")]
    public string QuoteId { get; set; } = string.Empty;

    /// <summary>
    /// Source currency of the quote.
    /// </summary>
    [JsonProperty("fromCcy")]
    public string FromCurrency { get; set; } = string.Empty;

    /// <summary>
    /// Destination currency of the quote.
    /// </summary>
    [JsonProperty("toCcy")]
    public string ToCurrency { get; set; } = string.Empty;

    /// <summary>
    /// Requested quote amount.
    /// </summary>
    [JsonProperty("rfqAmt")]
    public decimal RfqAmount { get; set; }

    /// <summary>
    /// Currency of the requested quote amount.
    /// </summary>
    [JsonProperty("rfqCcy")]
    public string RfqCurrency { get; set; } = string.Empty;

    /// <summary>
    /// Quoted price.
    /// </summary>
    [JsonProperty("quotePx")]
    public decimal QuotePrice { get; set; }

    /// <summary>
    /// Currency in which the quote price is denominated.
    /// </summary>
    [JsonProperty("quoteCcy")]
    public string QuoteCurrency { get; set; } = string.Empty;

    /// <summary>
    /// Quoted amount for the source currency.
    /// </summary>
    [JsonProperty("quoteFromAmt")]
    public decimal QuoteFromAmount { get; set; }

    /// <summary>
    /// Quoted amount for the destination currency.
    /// </summary>
    [JsonProperty("quoteToAmt")]
    public decimal QuoteToAmount { get; set; }

    /// <summary>
    /// Quote timestamp in milliseconds.
    /// </summary>
    [JsonProperty("quoteTime")]
    public long QuoteTimestamp { get; set; }

    /// <summary>
    /// Quote time.
    /// </summary>
    [JsonIgnore]
    public DateTime QuoteTime => QuoteTimestamp.ConvertFromMilliseconds();

    /// <summary>
    /// Quote time-to-live in milliseconds.
    /// </summary>
    [JsonProperty("ttlMs")]
    public long TtlMilliseconds { get; set; }
}
