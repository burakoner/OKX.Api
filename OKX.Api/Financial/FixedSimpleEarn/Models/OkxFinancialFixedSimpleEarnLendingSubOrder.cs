namespace OKX.Api.Financial.FixedSimpleEarn;

/// <summary>
/// OKX Financial Fixed Simple Earn Lending Sub Order Details
/// </summary>
public class OkxFinancialFixedSimpleEarnLendingSubOrder
{
    /// <summary>
    /// Lending order ID
    /// </summary>
    [JsonProperty("ordId")]
    public long OrderId { get; set; }

    /// <summary>
    /// Sub-order ID
    /// </summary>
    [JsonProperty("subOrdId")]
    public long SubOrderId { get; set; }

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
    /// Sub-order expiration time, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("expiryTime")]
    public long? ExpiryTimestamp { get; set; }

    /// <summary>
    /// Sub-order expiration time
    /// </summary>
    [JsonIgnore]
    public DateTime? ExpiryTime { get { return ExpiryTimestamp?.ConvertFromMilliseconds(); } }

    /// <summary>
    /// Total interest
    /// </summary>
    [JsonProperty("totalInterest")]
    public decimal TotalInterest { get; set; }

    /// <summary>
    /// Sub-order accrued interest
    /// </summary>
    [JsonProperty("accruedInterest")]
    public decimal AccruedInterest { get; set; }

    /// <summary>
    /// Sub-order early terminated penalty
    /// </summary>
    [JsonProperty("earlyTerminatedPenalty")]
    public decimal EarlyTerminatedPenalty { get; set; }

    /// <summary>
    /// Sub-order overdue interest
    /// </summary>
    [JsonProperty("overdueInterest")]
    public decimal OverdueInterest { get; set; }

    /// <summary>
    /// Sub-order final settlement time, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("finalSettlementTime")]
    public long? FinalSettlementTimestamp { get; set; }

    /// <summary>
    /// Sub-order final settlement time
    /// </summary>
    [JsonIgnore]
    public DateTime? FinalSettlementTime { get { return FinalSettlementTimestamp?.ConvertFromMilliseconds(); } }

    /// <summary>
    /// Sub-order actual settlement time, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("settledTime")]
    public long? SettledTimestamp { get; set; }

    /// <summary>
    /// Sub-order actual settlement time
    /// </summary>
    [JsonIgnore]
    public DateTime? SettledTime { get { return SettledTimestamp?.ConvertFromMilliseconds(); } }
    
    /// <summary>
    /// Creation time for sub-order, unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("cTime")]
    public long CreateTimestamp { get; set; }

    /// <summary>
    /// Creation time for sub-order
    /// </summary>
    [JsonIgnore]
    public DateTime CreateTime { get { return CreateTimestamp.ConvertFromMilliseconds(); } }

    /// <summary>
    /// Update time for sub-order, unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("uTime")]
    public long? UpdateTimestamp { get; set; }

    /// <summary>
    /// Update time for sub-order
    /// </summary>
    [JsonIgnore]
    public DateTime? UpdateTime { get { return UpdateTimestamp?.ConvertFromMilliseconds(); } }
}