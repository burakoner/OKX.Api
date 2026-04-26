namespace OKX.Api.Financial;

/// <summary>
/// OKX Dual Investment Product
/// </summary>
public record OkxFinancialDualInvestmentProduct
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
    /// Base currency
    /// </summary>
    [JsonProperty("baseCcy")]
    public string BaseCurrency { get; set; } = string.Empty;

    /// <summary>
    /// Quote currency
    /// </summary>
    [JsonProperty("quoteCcy")]
    public string QuoteCurrency { get; set; } = string.Empty;

    /// <summary>
    /// Investment currency
    /// </summary>
    [JsonProperty("notionalCcy")]
    public string NotionalCurrency { get; set; } = string.Empty;

    /// <summary>
    /// Expiration timestamp
    /// </summary>
    [JsonProperty("expTime")]
    public long ExpiryTimestamp { get; set; }

    /// <summary>
    /// Expiration time
    /// </summary>
    [JsonIgnore]
    public DateTime ExpiryTime => ExpiryTimestamp.ConvertFromMilliseconds();

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
    /// Listing timestamp
    /// </summary>
    [JsonProperty("listTime")]
    public long ListingTimestamp { get; set; }

    /// <summary>
    /// Listing time
    /// </summary>
    [JsonIgnore]
    public DateTime ListingTime => ListingTimestamp.ConvertFromMilliseconds();

    /// <summary>
    /// Minimum investment size
    /// </summary>
    [JsonProperty("minSize")]
    public decimal MinimumSize { get; set; }

    /// <summary>
    /// Maximum investment size
    /// </summary>
    [JsonProperty("maxSize")]
    public decimal MaximumSize { get; set; }

    /// <summary>
    /// Option type
    /// </summary>
    [JsonProperty("optType")]
    public OkxOptionType OptionType { get; set; }

    /// <summary>
    /// Product ID
    /// </summary>
    [JsonProperty("productId")]
    public string ProductId { get; set; } = string.Empty;

    /// <summary>
    /// Quote timestamp
    /// </summary>
    [JsonProperty("quoteTime")]
    public long QuoteTimestamp { get; set; }

    /// <summary>
    /// Quote time
    /// </summary>
    [JsonIgnore]
    public DateTime QuoteTime => QuoteTimestamp.ConvertFromMilliseconds();

    /// <summary>
    /// Earliest time to request early redemption
    /// </summary>
    [JsonProperty("redeemStartTime")]
    public long RedeemStartTimestamp { get; set; }

    /// <summary>
    /// Earliest time to request early redemption
    /// </summary>
    [JsonIgnore]
    public DateTime RedeemStartTime => RedeemStartTimestamp.ConvertFromMilliseconds();

    /// <summary>
    /// Latest time to request early redemption
    /// </summary>
    [JsonProperty("redeemEndTime")]
    public long RedeemEndTimestamp { get; set; }

    /// <summary>
    /// Latest time to request early redemption
    /// </summary>
    [JsonIgnore]
    public DateTime RedeemEndTime => RedeemEndTimestamp.ConvertFromMilliseconds();

    /// <summary>
    /// Investment size step
    /// </summary>
    [JsonProperty("stepSz")]
    public decimal StepSize { get; set; }

    /// <summary>
    /// Last tradable timestamp
    /// </summary>
    [JsonProperty("tradeEndTime")]
    public long TradeEndTimestamp { get; set; }

    /// <summary>
    /// Last tradable time
    /// </summary>
    [JsonIgnore]
    public DateTime TradeEndTime => TradeEndTimestamp.ConvertFromMilliseconds();

    /// <summary>
    /// Underlying index, e.g. BTC-USD
    /// </summary>
    [JsonProperty("uly")]
    public string Underlying { get; set; } = string.Empty;

    /// <summary>
    /// Strike price
    /// </summary>
    [JsonProperty("strike")]
    public decimal StrikePrice { get; set; }
}
