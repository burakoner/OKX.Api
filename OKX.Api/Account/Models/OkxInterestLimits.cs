namespace OKX.Api.Account.Models;

/// <summary>
/// Okx Interest Limits
/// </summary>
public class OkxInterestLimits
{
    /// <summary>
    /// Debt
    /// </summary>
    [JsonProperty("debt")]
    public decimal Debt { get; set; }

    /// <summary>
    /// Market Loan Interest
    /// </summary>
    [JsonProperty("interest")]
    public decimal? MarketLoanInterest { get; set; }

    /// <summary>
    /// Next Discount Time
    /// </summary>
    [JsonProperty("nextDiscountTime")]
    public long NextDiscountTimestamp { get; set; }

    /// <summary>
    /// Next Discount Time
    /// </summary>
    [JsonIgnore]
    public DateTime NextDiscountTime { get { return NextDiscountTimestamp.ConvertFromMilliseconds(); } }

    /// <summary>
    /// Next Interest Time
    /// </summary>
    [JsonProperty("nextInterestTime")]
    public long NextInterestTimestamp { get; set; }

    /// <summary>
    /// Next Interest Time
    /// </summary>
    [JsonIgnore]
    public DateTime NextInterestTime { get { return NextInterestTimestamp.ConvertFromMilliseconds(); } }

    /// <summary>
    /// Loan Allocation
    /// </summary>
    [JsonProperty("loanAlloc")]
    public decimal? LoanAllocation { get; set; }

    /// <summary>
    /// Records
    /// </summary>
    [JsonProperty("records")]
    public List<OkxInterestLimitRecord> Records { get; set; }
}

/// <summary>
/// OKX Interest Limit Record
/// </summary>
public class OkxInterestLimitRecord
{
    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    /// <summary>
    /// Rate
    /// </summary>
    [JsonProperty("rate")]
    public decimal Rate { get; set; }

    /// <summary>
    /// Loan Quota
    /// </summary>
    [JsonProperty("loanQuota")]
    public decimal LoanQuota { get; set; }

    /// <summary>
    /// Surplus Limit
    /// </summary>
    [JsonProperty("surplusLmt")]
    public decimal surplusLimit { get; set; }

    /// <summary>
    /// Surplus Limit Details
    /// </summary>
    [JsonProperty("surplusLmtDetails")]
    public OkxInterestLimitRecordSurplusLimitDetails SurplusLimitDetails { get; set; }

    /// <summary>
    /// Used Limit
    /// </summary>
    [JsonProperty("usedLmt")]
    public decimal UsedLmt { get; set; }

    /// <summary>
    /// Market Loan Interest
    /// </summary>
    [JsonProperty("interest")]
    public decimal? MarketLoanInterest { get; set; }

    /// <summary>
    /// VIP Position Loan
    /// </summary>
    [JsonProperty("posLoan")]
    public decimal? VipPositionLoan { get; set; }

    /// <summary>
    /// VIP Available Loan
    /// </summary>
    [JsonProperty("availLoan")]
    public decimal? VipAvailableLoan { get; set; }

    /// <summary>
    /// VIP Used Loan
    /// </summary>
    [JsonProperty("usedLoan")]
    public decimal? VipUsedLoan { get; set; }

    /// <summary>
    /// VIP Average Rate
    /// </summary>
    [JsonProperty("avgRate")]
    public decimal? VipAverageRate { get; set; }

}

/// <summary>
/// OKX Interest Limit Record Surplus Limit Details
/// </summary>
public class OkxInterestLimitRecordSurplusLimitDetails
{
    /// <summary>
    /// All Accounts Remaining Quota
    /// </summary>
    [JsonProperty("allAcctRemainingQuota")]
    public decimal AllAccountsRemainingQuota { get; set; }

    /// <summary>
    /// Current Account Remaining Quota
    /// </summary>
    [JsonProperty("curAcctRemainingQuota")]
    public decimal CurrentAccountRemainingQuota { get; set; }

    /// <summary>
    /// Platform Remaining Quota
    /// </summary>
    [JsonProperty("platRemainingQuota")]
    public decimal PlatformRemainingQuota { get; set; }
}
