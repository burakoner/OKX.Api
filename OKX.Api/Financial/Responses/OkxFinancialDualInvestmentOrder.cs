namespace OKX.Api.Financial;

/// <summary>
/// OKX Dual Investment Order
/// </summary>
public record OkxFinancialDualInvestmentOrder
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
    /// Order state
    /// </summary>
    [JsonProperty("state")]
    public OkxFinancialDualInvestmentOrderState State { get; set; }

    /// <summary>
    /// Product ID
    /// </summary>
    [JsonProperty("productId")]
    public string ProductId { get; set; } = string.Empty;

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
    /// Underlying index, e.g. BTC-USD
    /// </summary>
    [JsonProperty("uly")]
    public string Underlying { get; set; } = string.Empty;

    /// <summary>
    /// Strike price
    /// </summary>
    [JsonProperty("strike")]
    public decimal StrikePrice { get; set; }

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
    /// Yield size
    /// </summary>
    [JsonProperty("yieldSz"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? YieldSize { get; set; }

    /// <summary>
    /// Yield currency
    /// </summary>
    [JsonProperty("yieldCcy")]
    public string YieldCurrency { get; set; } = string.Empty;

    /// <summary>
    /// Settlement size
    /// </summary>
    [JsonProperty("settleSz"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? SettlementSize { get; set; }

    /// <summary>
    /// Settlement currency
    /// </summary>
    [JsonProperty("settleCcy")]
    public string SettlementCurrency { get; set; } = string.Empty;

    /// <summary>
    /// Settlement price
    /// </summary>
    [JsonProperty("settlePx"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? SettlementPrice { get; set; }

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
    /// Settlement timestamp
    /// </summary>
    [JsonProperty("settleTime"), JsonConverter(typeof(LongAsStringNullableConverter))]
    public long? SettlementTimestamp { get; set; }

    /// <summary>
    /// Settlement time
    /// </summary>
    [JsonIgnore]
    public DateTime? SettlementTime => SettlementTimestamp?.ConvertFromMilliseconds();

    /// <summary>
    /// Earliest time to request early redemption
    /// </summary>
    [JsonProperty("redeemStartTime"), JsonConverter(typeof(LongAsStringNullableConverter))]
    public long? RedeemStartTimestamp { get; set; }

    /// <summary>
    /// Earliest time to request early redemption
    /// </summary>
    [JsonIgnore]
    public DateTime? RedeemStartTime => RedeemStartTimestamp?.ConvertFromMilliseconds();

    /// <summary>
    /// Latest time to request early redemption
    /// </summary>
    [JsonProperty("redeemEndTime"), JsonConverter(typeof(LongAsStringNullableConverter))]
    public long? RedeemEndTimestamp { get; set; }

    /// <summary>
    /// Latest time to request early redemption
    /// </summary>
    [JsonIgnore]
    public DateTime? RedeemEndTime => RedeemEndTimestamp?.ConvertFromMilliseconds();

    /// <summary>
    /// Order creation timestamp
    /// </summary>
    [JsonProperty("cTime")]
    public long CreateTimestamp { get; set; }

    /// <summary>
    /// Order creation time
    /// </summary>
    [JsonIgnore]
    public DateTime CreateTime => CreateTimestamp.ConvertFromMilliseconds();

    /// <summary>
    /// Order update timestamp
    /// </summary>
    [JsonProperty("uTime"), JsonConverter(typeof(LongAsStringNullableConverter))]
    public long? UpdateTimestamp { get; set; }

    /// <summary>
    /// Order update time
    /// </summary>
    [JsonIgnore]
    public DateTime? UpdateTime => UpdateTimestamp?.ConvertFromMilliseconds();
}
