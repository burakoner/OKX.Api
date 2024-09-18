namespace OKX.Api.Public.Models;

/// <summary>
/// OKX VIP Interest Rate
/// </summary>
public class OkxVipInterestRate
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
    /// Quota
    /// </summary>
    [Obsolete]
    [JsonProperty("quota")]
    public decimal? Quota { get; set; }

    /// <summary>
    /// Level List
    /// </summary>
    [JsonProperty("levelList")]
    public List<OkxVipInterestRateLevel> LevelList { get; set; }
}

/// <summary>
/// OKX VIP Interest Rate Level
/// </summary>
public class OkxVipInterestRateLevel
{
    /// <summary>
    /// Level
    /// </summary>
    [JsonProperty("level")]
    public string Level { get; set; }

    /// <summary>
    /// Loan Quota
    /// </summary>
    [JsonProperty("loanQuota")]
    public decimal LoanQuota { get; set; }
}
