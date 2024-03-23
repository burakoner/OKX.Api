namespace OKX.Api.Account.Models;

/// <summary>
/// Okx Interest Limits
/// </summary>
public class OkxInterestLimits
{
    [JsonProperty("debt")]
    public decimal Debt { get; set; }

    [JsonProperty("interest")]
    public decimal? MarketLoanInterest { get; set; }

    [JsonProperty("nextDiscountTime")]
    public long NextDiscountTimestamp { get; set; }

    [JsonIgnore]
    public DateTime NextDiscountTime { get { return NextDiscountTimestamp.ConvertFromMilliseconds(); } }

    [JsonProperty("nextInterestTime")]
    public long NextInterestTimestamp { get; set; }

    [JsonIgnore]
    public DateTime NextInterestTime { get { return NextInterestTimestamp.ConvertFromMilliseconds(); } }

    [JsonProperty("loanAlloc")]
    public decimal? LoanAllocation { get; set; }

    [JsonProperty("records")]
    public List<OkxInterestLimitRecord> Records { get; set; }
}

public class OkxInterestLimitRecord
{
    [JsonProperty("ccy")]
    public string Currency { get; set; }
    
    [JsonProperty("rate")]
    public decimal Rate { get; set; }

    [JsonProperty("loanQuota")]
    public decimal LoanQuota { get; set; }

    [JsonProperty("surplusLmt")]
    public decimal surplusLimit { get; set; }

    [JsonProperty("surplusLmtDetails")]
    public OkxInterestLimitRecordSurplusLimitDetails SurplusLimitDetails { get; set; }

    [JsonProperty("usedLmt")]
    public decimal UsedLmt { get; set; }
    
    [JsonProperty("interest")]
    public decimal? MarketLoanInterest { get; set; }

    [JsonProperty("posLoan")]
    public decimal? VipPositionLoan { get; set; }

    [JsonProperty("availLoan")]
    public decimal? VipAvailableLoan { get; set; }

    [JsonProperty("usedLoan")]
    public decimal? VipUsedLoan { get; set; }

    [JsonProperty("avgRate")]
    public decimal? VipAverageRate { get; set; }

}

public class OkxInterestLimitRecordSurplusLimitDetails
{
    [JsonProperty("allAcctRemainingQuota")]
    public decimal AllAccountsRemainingQuota { get; set; }
    
    [JsonProperty("curAcctRemainingQuota")]
    public decimal CurrentAccountRemainingQuota { get; set; }

    [JsonProperty("platRemainingQuota")]
    public decimal PlatformRemainingQuota { get; set; }
}
