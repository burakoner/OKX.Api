namespace OKX.Api.Public;

/// <summary>
/// OKX VIP Interest Rate Level
/// </summary>
public class OkxPublicVipInterestRateLevel
{
    /// <summary>
    /// Level
    /// </summary>
    [JsonProperty("level")]
    public string Level { get; set; } = "";

    /// <summary>
    /// Loan Quota
    /// </summary>
    [JsonProperty("loanQuota")]
    public decimal LoanQuota { get; set; }
}
