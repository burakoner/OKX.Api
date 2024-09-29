namespace OKX.Api.RecurringBuy;

/// <summary>
/// Recurring Buy Order
/// </summary>
public class OkxRecurringBuyOrder
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
    public string ClientOrderId { get; set; }

    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonProperty("instType"), JsonConverter(typeof(OkxInstrumentTypeConverter))]
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
    public DateTime CreateTime { get { return CreateTimestamp.ConvertFromMilliseconds(); } }

    /// <summary>
    /// Algo order updated  time, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("uTime")]
    public long UpdateTimestamp { get; set; }

    /// <summary>
    /// Algo order updated  time
    /// </summary>
    [JsonIgnore]
    public DateTime UpdateTime { get { return UpdateTimestamp.ConvertFromMilliseconds(); } }

    /// <summary>
    /// Algo order type. recurring: recurring buy
    /// </summary>
    [JsonProperty("algoOrdType"), JsonConverter(typeof(OkxRecurringBuyTypeConverter))]
    public OkxRecurringBuyType Type { get; set; }

    /// <summary>
    /// State
    /// </summary>
    [JsonProperty("state"), JsonConverter(typeof(OkxRecurringBuyStateConverter))]
    public OkxRecurringBuyState State { get; set; }

    /// <summary>
    /// Strategy name
    /// </summary>
    [JsonProperty("stgyName")]
    public string StrategyName { get; set; }

    /// <summary>
    /// Recurring list
    /// </summary>
    [JsonProperty("recurringList")]
    public List<OkxRecurringBuyOrderList> RecurringList { get; set; } = [];

    /// <summary>
    /// Period
    /// </summary>
    [JsonProperty("period"), JsonConverter(typeof(OkxRecurringBuyPeriodConverter))]
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
    public int RecurringTime { get; set; }

    /// <summary>
    /// Time zone
    /// </summary>
    [JsonProperty("timeZone")]
    public string Timezone { get; set; }

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
    public string InvestmentCurrency { get; set; }

    /// <summary>
    /// Total PNL
    /// </summary>
    [JsonProperty("totalPnl")]
    public decimal TotalPnl { get; set; }

    /// <summary>
    /// Total annual rate
    /// </summary>
    [JsonProperty("totalAnnRate")]
    public decimal TotalAnnualRate { get; set; }

    /// <summary>
    /// PNL ratio
    /// </summary>
    [JsonProperty("pnlRatio")]
    public decimal PnlRatio { get; set; }

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
}
