namespace OKX.Api.Account;

/// <summary>
/// OKX Fixed Loan Borrow Order Details
/// </summary>
public record OkxAccountFixedLoanBorrowingOrderDetails
{
    /// <summary>
    /// Borrowing currency, e.g. BTC
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Borrowing order ID
    /// </summary>
    [JsonProperty("ordId")]
    public long OrderId { get; set; }

    /// <summary>
    /// Fixed term for borrowing order. 30D: 30 days
    /// </summary>
    [JsonProperty("term")]
    public string Term { get; set; } = string.Empty;

    /// <summary>
    /// State
    /// </summary>
    [JsonProperty("state")]
    public OkxAccountFixedLoanBorrowingOrderState State { get; set; }

    /// <summary>
    /// Borrowing failed reason
    /// 1: There are currently no matching orders
    /// 2: Unable to pay prepaid interest
    /// -1: Other reason
    /// </summary>
    [JsonProperty("failedReason")]
    public string FailedReason { get; set; } = string.Empty;

    /// <summary>
    /// Settle reason
    /// 1: Order at maturity
    /// 2: Extension in advance
    /// 3: Early repayment
    /// </summary>
    [JsonProperty("settleReason")]
    public string SettleReason { get; set; } = string.Empty;

    /// <summary>
    /// Borrowing annual rate of current order
    /// </summary>
    [JsonProperty("curRate")]
    public decimal CurrentRate { get; set; }

    /// <summary>
    /// Accrued interest
    /// </summary>
    [JsonProperty("accruedInterest")]
    public decimal? AccruedInterest { get; set; }

    /// <summary>
    /// Requested borrowing mount
    /// </summary>
    [JsonProperty("reqBorrowAmt")]
    public decimal? RequestedBorrowAmount { get; set; }

    /// <summary>
    /// Actual borrowed mount
    /// </summary>
    [JsonProperty("actualBorrowAmt")]
    public decimal? ActualBorrowAmount { get; set; }

    /// <summary>
    /// Whether or not auto-renew when the term is due
    /// true: auto-renew
    /// false: not auto-renew
    /// </summary>
    [JsonProperty("reborrow")]
    public bool Reborrow { get; set; }

    /// <summary>
    /// Auto-renew borrowing rate, in decimal. e.g. 0.01 represents 1%
    /// </summary>
    [JsonProperty("reborrowRate")]
    public decimal? ReborrowRate { get; set; }

    /// <summary>
    /// Expiry time, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("expiryTime")]
    public long? ExpiryTimestamp { get; set; }

    /// <summary>
    /// Expiry time, Unix timestamp format in milliseconds
    /// </summary>
    [JsonIgnore]
    public DateTime? ExpiryTime => ExpiryTimestamp?.ConvertFromMilliseconds();

    /// <summary>
    /// Force repayment time, unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("forceRepayTime")]
    public long? ForceRepayTimestamp { get; set; }

    /// <summary>
    /// Force repayment time, unix timestamp format in milliseconds
    /// </summary>
    [JsonIgnore]
    public DateTime? ForceRepayTime => ForceRepayTimestamp?.ConvertFromMilliseconds();

    /// <summary>
    /// Deadline of penalty interest for early repayment, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("deadlinePenaltyInterest")]
    public long? PenaltyInterestDeadlineTimestamp { get; set; }

    /// <summary>
    /// Deadline of penalty interest for early repayment, Unix timestamp format in milliseconds
    /// </summary>
    [JsonIgnore]
    public DateTime? PenaltyInterestDeadlineTime => PenaltyInterestDeadlineTimestamp?.ConvertFromMilliseconds();

    /// <summary>
    /// Potential penalty Interest for early repayment
    /// </summary>
    [JsonProperty("potentialPenaltyInterest")]
    public decimal? PotentialPenaltyInterest { get; set; }

    /// <summary>
    /// Overdue penalty interest
    /// </summary>
    [JsonProperty("overduePenaltyInterest")]
    public decimal? OverduePenaltyInterest { get; set; }

    /// <summary>
    /// Early repay penalty interest
    /// </summary>
    [JsonProperty("earlyRepayPenaltyInterest")]
    public decimal? EarlyRepayPenaltyInterest { get; set; }

    /// <summary>
    /// Creation time for borrowing order, unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("cTime")]
    public long CreateTimestamp { get; set; }

    /// <summary>
    /// Creation time for borrowing order, unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonIgnore]
    public DateTime CreateTime => CreateTimestamp.ConvertFromMilliseconds();

    /// <summary>
    /// Update time for borrowing order, unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("uTime")]
    public long? UpdateTimestamp { get; set; }

    /// <summary>
    /// Update time for borrowing order, unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonIgnore]
    public DateTime? UpdateTime => UpdateTimestamp?.ConvertFromMilliseconds();
}
