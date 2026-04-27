namespace OKX.Api.RecurringBuy;

/// <summary>
/// Recurring Buy Order
/// </summary>
public record OkxRecurringBuyOrder
{
    /// <summary>
    /// Order Id
    /// </summary>
    [JsonProperty("algoId")]
    public long OrderId { get; set; }

    /// <summary>
    /// Client Order Id
    /// </summary>
    [JsonProperty("algoClOrdId")]
    public string ClientOrderId { get; set; } = string.Empty;

    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonProperty("instType")]
    public OkxInstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Algo order created time, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("cTime")]
    public long CreateTimestamp { get; set; }

    /// <summary>
    /// Algo order created time
    /// </summary>
    [JsonIgnore]
    public DateTime CreateTime => CreateTimestamp.ConvertFromMilliseconds();

    /// <summary>
    /// Algo order updated  time, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("uTime")]
    public long UpdateTimestamp { get; set; }

    /// <summary>
    /// Algo order updated  time
    /// </summary>
    [JsonIgnore]
    public DateTime UpdateTime => UpdateTimestamp.ConvertFromMilliseconds();

    /// <summary>
    /// Algo order type. recurring: recurring buy
    /// </summary>
    [JsonProperty("algoOrdType")]
    public OkxRecurringBuyType Type { get; set; }

    /// <summary>
    /// State
    /// </summary>
    [JsonProperty("state")]
    public OkxRecurringBuyState State { get; set; }

    /// <summary>
    /// Strategy name
    /// </summary>
    [JsonProperty("stgyName")]
    public string StrategyName { get; set; } = string.Empty;

    /// <summary>
    /// Recurring list
    /// </summary>
    [JsonProperty("recurringList")]
    public List<OkxRecurringBuyOrderList> RecurringList { get; set; } = [];

    /// <summary>
    /// Period
    /// </summary>
    [JsonProperty("period")]
    public OkxRecurringBuyPeriod Period { get; set; }

    /// <summary>
    /// Recurring day
    /// </summary>
    [JsonProperty("recurringDay")]
    public int? RecurringDay { get; set; }

    /// <summary>
    /// Recurring hour
    /// </summary>
    [JsonProperty("recurringHour")]
    public int? RecurringHour { get; set; }

    /// <summary>
    /// Recurring time
    /// </summary>
    [JsonProperty("recurringTime")]
    public int? RecurringTime { get; set; }

    /// <summary>
    /// Time zone
    /// </summary>
    [JsonProperty("timeZone")]
    public string Timezone { get; set; } = string.Empty;

    /// <summary>
    /// Amount
    /// </summary>
    [JsonProperty("amt")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Investment amount
    /// </summary>
    [JsonProperty("investmentAmt")]
    public decimal InvestmentAmount { get; set; }

    /// <summary>
    /// Investment currency
    /// </summary>
    [JsonProperty("investmentCcy")]
    public string InvestmentCurrency { get; set; } = string.Empty;

    /// <summary>
    /// Next invest time, Unix timestamp format in milliseconds
    /// </summary>
    [JsonProperty("nextInvestTime"), JsonConverter(typeof(LongAsStringNullableConverter))]
    public long? NextInvestmentTimestamp { get; set; }

    /// <summary>
    /// Next invest time
    /// </summary>
    [JsonIgnore]
    public DateTime? NextInvestmentTime => NextInvestmentTimestamp?.ConvertFromMilliseconds();

    /// <summary>
    /// Order tag
    /// </summary>
    [JsonProperty("tag")]
    public string Tag { get; set; } = string.Empty;

    /// <summary>
    /// Funding source codes
    /// 1: Trading account
    /// 2: Funding account
    /// 3: Simple earn account
    /// </summary>
    [JsonProperty("source")]
    public List<string> SourceCodes { get; set; } = [];

    /// <summary>
    /// Recurring buy time type
    /// </summary>
    [JsonProperty("recurringTimeType")]
    public OkxRecurringBuyTimeType? RecurringTimeType { get; set; }

    /// <summary>
    /// Recurring buy time in minutes
    /// </summary>
    [JsonProperty("recurringTimeMinutes")]
    public int? RecurringTimeMinutes { get; set; }

    /// <summary>
    /// Total PNL
    /// </summary>
    [JsonProperty("totalPnl")]
    public decimal TotalProfitAndLoss { get; set; }

    /// <summary>
    /// Total annual rate
    /// </summary>
    [JsonProperty("totalAnnRate")]
    public decimal TotalAnnualRate { get; set; }

    /// <summary>
    /// PNL ratio
    /// </summary>
    [JsonProperty("pnlRatio")]
    public decimal ProfitAndLossRatio { get; set; }

    /// <summary>
    /// Market cap
    /// </summary>
    [JsonProperty("mktCap")]
    public decimal MarketCap { get; set; }

    /// <summary>
    /// Cycles
    /// </summary>
    [JsonProperty("cycles")]
    public int Cycles { get; set; }

    /// <summary>
    /// Trade quote currency.
    /// </summary>
    [JsonProperty("tradeQuoteCcy")]
    public string? TradeQuoteCurrency { get; set; }
}

/// <summary>
/// Recurring Buy Item Details
/// </summary>
public record OkxRecurringBuyOrderList
{
    /// <summary>
    /// Recurring currency, e.g. BTC
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Proportion of recurring currency assets, e.g. "0.2" representing 20%
    /// </summary>
    [JsonProperty("ratio")]
    public decimal Ratio { get; set; }

    /// <summary>
    /// Minimum price of price range. Null means no limit
    /// </summary>
    [JsonProperty("minPx"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? MinimumPrice { get; set; }

    /// <summary>
    /// Maximum price of price range. Null means no limit
    /// </summary>
    [JsonProperty("maxPx"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? MaximumPrice { get; set; }

    /// <summary>
    /// Accumulated quantity in unit of recurring buy currency
    /// </summary>
    [JsonProperty("totalAmt"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? TotalAmount { get; set; }

    /// <summary>
    /// Profit in unit of investment currency
    /// </summary>
    [JsonProperty("profit"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? Profit { get; set; }

    /// <summary>
    /// Average recurring buy price
    /// </summary>
    [JsonProperty("avgPx"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? AveragePrice { get; set; }

    /// <summary>
    /// Current market price
    /// </summary>
    [JsonProperty("px"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? CurrentPrice { get; set; }
}
