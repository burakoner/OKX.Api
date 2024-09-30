namespace OKX.Api.SubAccount;

/// <summary>
/// OKX Sub-Account Interest Limits
/// </summary>
public class OkxSubAccountInterestLimits
{
    /// <summary>
    /// Name of sub account
    /// </summary>
    [JsonProperty("subAcct")]
    public string SubAccountName { get; set; } = string.Empty;

    /// <summary>
    /// Current debt in USDT
    /// </summary>
    [JsonProperty("debt")]
    public decimal Debt { get; set; }

    /// <summary>
    /// Always return ""
    /// </summary>
    [JsonProperty("interest")]
    public decimal? Interest { get; set; }

    /// <summary>
    /// Next deduct time, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("nextDiscountTime")]
    public long NextDiscountTimestamp { get; set; }

    /// <summary>
    /// Next deduct time
    /// </summary>
    [JsonIgnore]
    public DateTime NextDiscountTime => NextDiscountTimestamp.ConvertFromMilliseconds();

    /// <summary>
    /// Next accrual time, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("nextInterestTime")]
    public long NextInterestTimestamp { get; set; }

    /// <summary>
    /// Next accrual time
    /// </summary>
    [JsonIgnore]
    public DateTime NextInterestTime => NextInterestTimestamp.ConvertFromMilliseconds();

    /// <summary>
    /// VIP Loan allocation for the current trading account
    /// 1. The unit is percent(%). Range is [0, 100]. Precision is 0.01%
    /// 2. If master account did not assign anything, then "0"
    /// 3. "" if shared between master and sub-account
    /// </summary>
    [JsonProperty("loanAlloc")]
    public decimal? LoanAllocation { get; set; }

    /// <summary>
    /// Details for currencies
    /// </summary>
    [JsonProperty("records")]
    public List<OkxSubAccountInterestLimitsRecord> Records { get; set; } = [];
}

/// <summary>
/// OKX Sub-Account Interest Limits Record
/// </summary>
public class OkxSubAccountInterestLimitsRecord
{
    /// <summary>
    /// Loan currency, e.g. BTC
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Current daily rate
    /// </summary>
    [JsonProperty("rate")]
    public decimal Rate { get; set; }

    /// <summary>
    /// Borrow limit of master account
    /// If loan allocation has been assigned, then this returns the borrow limit of the current trading account (sub-account)
    /// </summary>
    [JsonProperty("loanQuota")]
    public decimal LoanQuota { get; set; }

    /// <summary>
    /// Available amount of master account and sub-accounts
    /// If loan allocation has been assigned, then it is the available amount to borrow by the current trading account (sub-account)
    /// </summary>
    [JsonProperty("surplusLmt")]
    public decimal SurplusLimit { get; set; }

    /// <summary>
    /// The details of available amount of master account and sub-accounts
    /// The value of surplusLmt is the minimum value within this array. It can help you judge the reason that surplusLmt is not enough.
    /// Only applicable to VIP loans
    /// </summary>
    [JsonProperty("surplusLmtDetails")]
    public OkxSubAccountInterestSurplusLimitDetails? SurplusLimitDetails { get; set; }

    /// <summary>
    /// Borrowed amount of master account and sub-accounts
    /// If loan allocation has been assigned, then it is the borrowed amount by the current trading account (sub-account)
    /// </summary>
    [JsonProperty("usedLmt")]
    public decimal UsedLimit { get; set; }

    /// <summary>
    /// Always return ""
    /// </summary>
    [JsonProperty("interest")]
    public decimal? Interest { get; set; }

    /// <summary>
    /// Frozen amount for current account (sub-account, within the locked quota)
    /// Only applicable to VIP loans
    /// </summary>
    [JsonProperty("posLoan")]
    public decimal FrozenAmount { get; set; }

    /// <summary>
    /// Available amount for current account (sub-account, within the locked quota)
    /// Only applicable to VIP loans
    /// </summary>
    [JsonProperty("availLoan")]
    public decimal AvailableLoan { get; set; }

    /// <summary>
    /// Borrowed amount for current account (sub-account)
    /// Only applicable to VIP loans
    /// </summary>
    [JsonProperty("usedLoan")]
    public decimal UsedLoan { get; set; }

    /// <summary>
    /// Average (hourly) interest of already borrowed coin
    /// only applicable to VIP loans
    /// </summary>
    [JsonProperty("avgRate")]
    public decimal AverageRate { get; set; }
}

/// <summary>
/// OKX Sub-Account Interest Surplus Limit Details
/// </summary>
public class OkxSubAccountInterestSurplusLimitDetails
{
    /// <summary>
    /// Total remaining quota for master account and sub-accounts
    /// </summary>
    [JsonProperty("allAcctRemainingQuota")]
    public decimal AllAccountsRemainingQuota { get; set; }

    /// <summary>
    /// The remaining quota for the current sub-account
    /// Only applicable to the case in which the sub-account is assigned the loan allocation
    /// </summary>
    [JsonProperty("curAcctRemainingQuota")]
    public decimal CurrentAccountRemainingQuota { get; set; }

    /// <summary>
    /// Remaining quota for the platform
    /// The format like "600" will be returned when it is more than curAcctRemainingQuota or allAcctRemainingQuota
    /// </summary>
    [JsonProperty("platRemainingQuota")]
    public decimal PlatformRemainingQuota { get; set; }
}