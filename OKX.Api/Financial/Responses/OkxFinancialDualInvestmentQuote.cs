namespace OKX.Api.Financial;

/// <summary>
/// OKX Dual Investment Quote
/// </summary>
public record OkxFinancialDualInvestmentQuote
{
    /// <summary>
    /// Absolute yield
    /// </summary>
    [JsonProperty("absYield")]
    public decimal AbsoluteYield { get; set; }

    /// <summary>
    /// Annualized yield
    /// </summary>
    [JsonProperty("annualizedYield")]
    public decimal AnnualizedYield { get; set; }

    /// <summary>
    /// Interest accrual timestamp
    /// </summary>
    [JsonProperty("interestAccrualTime")]
    public long InterestAccrualTimestamp { get; set; }

    /// <summary>
    /// Interest accrual time
    /// </summary>
    [JsonIgnore]
    public DateTime InterestAccrualTime => InterestAccrualTimestamp.ConvertFromMilliseconds();

    /// <summary>
    /// Investment size
    /// </summary>
    [JsonProperty("notionalSz")]
    public decimal NotionalSize { get; set; }

    /// <summary>
    /// Investment currency
    /// </summary>
    [JsonProperty("notionalCcy")]
    public string NotionalCurrency { get; set; } = string.Empty;

    /// <summary>
    /// Product ID
    /// </summary>
    [JsonProperty("productId")]
    public string ProductId { get; set; } = string.Empty;

    /// <summary>
    /// Quote ID
    /// </summary>
    [JsonProperty("quoteId")]
    public string QuoteId { get; set; } = string.Empty;

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

    /// <summary>
    /// Index price
    /// </summary>
    [JsonProperty("idxPx")]
    public decimal IndexPrice { get; set; }
}
