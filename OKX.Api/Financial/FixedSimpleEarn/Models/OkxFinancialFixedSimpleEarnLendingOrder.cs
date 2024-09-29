namespace OKX.Api.Financial.FixedSimpleEarn;

/// <summary>
/// OKX Financial Fixed Simple Earn Lending Order Details
/// </summary>
public class OkxFinancialFixedSimpleEarnLendingOrder
{
    /// <summary>
    /// Lending order ID
    /// </summary>
    [JsonProperty("ordId")]
    public long OrderId { get; set; }

    /// <summary>
    /// State
    /// </summary>
    [JsonProperty("state"), JsonConverter(typeof(OkxFinancialFixedSimpleEarnLendingOrderStateConverter))]
    public OkxFinancialFixedSimpleEarnLendingOrderState State { get; set; }

    /// <summary>
    /// Lending currency, e.g. BTC
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    /// <summary>
    /// Lending amount
    /// </summary>
    [JsonProperty("amt")]
    public decimal Amount { get; set; }

    /// <summary>
    /// lending APR, in decimal. e.g. 0.01 represent 1%
    /// </summary>
    [JsonProperty("rate")]
    public decimal Rate { get; set; }

    /// <summary>
    /// Fixed term for lending order, e.g. 30D
    /// </summary>
    [JsonProperty("term")]
    public string Term { get; set; }

    /// <summary>
    /// Whether or not auto-renewal when the term is due
    /// </summary>
    [JsonProperty("autoRenewal")]
    public bool AutoRenewal { get; set; }

    /// <summary>
    /// Total interest
    /// </summary>
    [JsonProperty("totalInterest")]
    public decimal TotalInterest { get; set; }

    /// <summary>
    /// Pending amount
    /// </summary>
    [JsonProperty("pendingAmt")]
    public decimal PendingAmount { get; set; }

    /// <summary>
    /// Earning amount
    /// </summary>
    [JsonProperty("earningAmt")]
    public decimal EarningAmount { get; set; }

    /// <summary>
    /// Start earning time, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("startTime")]
    public long? StartTimestamp { get; set; }
    
    /// <summary>
    /// Start earning time
    /// </summary>
    [JsonIgnore]
    public DateTime? StartTime { get { return StartTimestamp?.ConvertFromMilliseconds(); } }

    /// <summary>
    /// Settled time, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("settledTime")]
    public long? SettledTimestamp { get; set; }
    
    /// <summary>
    /// Settled time
    /// </summary>
    [JsonIgnore]
    public DateTime? SettledTime { get { return SettledTimestamp?.ConvertFromMilliseconds(); } }

    /// <summary>
    /// Creation time for lending order, unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("cTime")]
    public long CreateTimestamp { get; set; }
    
    /// <summary>
    /// Creation time for lending order
    /// </summary>
    [JsonIgnore]
    public DateTime CreateTime { get { return CreateTimestamp.ConvertFromMilliseconds(); } }

    /// <summary>
    /// Update time for lending order, unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("uTime")]
    public long? UpdateTimestamp { get; set; }
    
    /// <summary>
    /// Update time for lending order
    /// </summary>
    [JsonIgnore]
    public DateTime? UpdateTime { get { return UpdateTimestamp?.ConvertFromMilliseconds(); } }
}