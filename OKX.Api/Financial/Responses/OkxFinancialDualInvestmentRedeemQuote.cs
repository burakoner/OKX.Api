namespace OKX.Api.Financial;

/// <summary>
/// OKX Dual Investment Redeem Quote
/// </summary>
public record OkxFinancialDualInvestmentRedeemQuote
{
    /// <summary>
    /// Order ID
    /// </summary>
    [JsonProperty("ordId")]
    public string OrderId { get; set; } = string.Empty;

    /// <summary>
    /// Quote ID
    /// </summary>
    [JsonProperty("quoteId")]
    public string QuoteId { get; set; } = string.Empty;

    /// <summary>
    /// Redemption size
    /// </summary>
    [JsonProperty("redeemSz")]
    public decimal RedeemSize { get; set; }

    /// <summary>
    /// Redemption currency
    /// </summary>
    [JsonProperty("redeemCcy")]
    public string RedeemCurrency { get; set; } = string.Empty;

    /// <summary>
    /// Term rate
    /// </summary>
    [JsonProperty("termRate")]
    public decimal TermRate { get; set; }

    /// <summary>
    /// Quote validity timestamp
    /// </summary>
    [JsonProperty("validUntil")]
    public long ValidUntilTimestamp { get; set; }

    /// <summary>
    /// Quote validity time
    /// </summary>
    [JsonIgnore]
    public DateTime ValidUntilTime => ValidUntilTimestamp.ConvertFromMilliseconds();
}
