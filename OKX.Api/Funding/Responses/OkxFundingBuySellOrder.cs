namespace OKX.Api.Funding;

/// <summary>
/// OKX buy/sell order
/// </summary>
public record OkxFundingBuySellOrder
{
    /// <summary>
    /// Buy/sell order identifier.
    /// </summary>
    [JsonProperty("ordId")]
    public string OrderId { get; set; } = string.Empty;

    /// <summary>
    /// Client supplied order identifier.
    /// </summary>
    [JsonProperty("clOrdId")]
    public string ClientOrderId { get; set; } = string.Empty;

    /// <summary>
    /// Quote identifier used for the trade.
    /// </summary>
    [JsonProperty("quoteId")]
    public string QuoteId { get; set; } = string.Empty;

    /// <summary>
    /// Order state reported by OKX.
    /// </summary>
    [JsonProperty("state")]
    public string State { get; set; } = string.Empty;

    /// <summary>
    /// Executed trade side.
    /// </summary>
    [JsonProperty("side")]
    public OkxTradeOrderSide Side { get; set; }

    /// <summary>
    /// Source currency of the trade.
    /// </summary>
    [JsonProperty("fromCcy")]
    public string FromCurrency { get; set; } = string.Empty;

    /// <summary>
    /// Destination currency of the trade.
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
    /// Filled execution price.
    /// </summary>
    [JsonProperty("fillPx")]
    public decimal FilledPrice { get; set; }

    /// <summary>
    /// Currency in which the fill price is denominated.
    /// </summary>
    [JsonProperty("fillQuoteCcy")]
    public string FillQuoteCurrency { get; set; } = string.Empty;

    /// <summary>
    /// Filled amount for the source currency.
    /// </summary>
    [JsonProperty("fillFromAmt")]
    public decimal FilledFromAmount { get; set; }

    /// <summary>
    /// Filled amount for the destination currency.
    /// </summary>
    [JsonProperty("fillToAmt")]
    public decimal FilledToAmount { get; set; }

    /// <summary>
    /// Order creation timestamp in milliseconds.
    /// </summary>
    [JsonProperty("cTime")]
    public long CreateTimestamp { get; set; }

    /// <summary>
    /// Order creation time.
    /// </summary>
    [JsonIgnore]
    public DateTime CreateTime => CreateTimestamp.ConvertFromMilliseconds();

    /// <summary>
    /// Last update timestamp in milliseconds.
    /// </summary>
    [JsonProperty("uTime")]
    [JsonConverter(typeof(LongAsStringNullableConverter))]
    public long? UpdateTimestamp { get; set; }

    /// <summary>
    /// Last update time.
    /// </summary>
    [JsonIgnore]
    public DateTime? UpdateTime => UpdateTimestamp?.ConvertFromMilliseconds();
}
