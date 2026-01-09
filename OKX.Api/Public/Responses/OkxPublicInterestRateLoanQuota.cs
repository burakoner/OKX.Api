namespace OKX.Api.Public;

/// <summary>
/// OKX Interest Rate
/// </summary>
public record OkxPublicInterestRateLoanQuota
{
    /// <summary>
    /// Basic
    /// </summary>
    [JsonProperty("basic")]
    public List<OkxPublicInterestRateBasic> Basic { get; set; } = [];

    /// <summary>
    /// VIP
    /// </summary>
    [JsonProperty("vip")]
    public List<OkxPublicInterestRateVip> VIP { get; set; } = [];

    /// <summary>
    /// Regular
    /// </summary>
    [JsonProperty("regular")]
    public List<OkxPublicInterestRateRegular> Regular { get; set; } = [];

    /// <summary>
    /// Currencies that have loan quota configured using customized absolute value.
    /// Users should refer to config to get the loan quota of a currency which is listed in configCcyList, instead of getting it from basic/vip/regular.
    /// </summary>
    [JsonProperty("configCcyList")]
    public List<OkxPublicInterestRateCurrency> Currencies { get; set; } = [];

    /// <summary>
    /// The currency details of loan quota configured using customized absolute value
    /// </summary>
    [JsonProperty("config")]
    public List<OkxPublicInterestRateConfiguration> Configurations { get; set; } = [];

}

/// <summary>
/// OKX Public Interest Rate Basic
/// </summary>
public record OkxPublicInterestRateBasic
{
    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Quota
    /// </summary>
    [JsonProperty("quota")]
    public decimal Quota { get; set; }

    /// <summary>
    /// Rate
    /// </summary>
    [JsonProperty("rate")]
    public decimal Rate { get; set; }
}

/// <summary>
/// OKX Public Interest Rate VIP
/// </summary>
public record OkxPublicInterestRateVip
{
    /// <summary>
    /// Level
    /// </summary>
    [JsonProperty("level")]
    public string Level { get; set; } = string.Empty;

    /// <summary>
    /// Loan Quota Coefficient
    /// </summary>
    [JsonProperty("loanQuotaCoef")]
    public decimal LoanQuotaCoefficient { get; set; }
}

/// <summary>
/// OKX Public Interest Rate Regular
/// </summary>
public record OkxPublicInterestRateRegular
{
    /// <summary>
    /// Level
    /// </summary>
    [JsonProperty("level")]
    public string Level { get; set; } = string.Empty;

    /// <summary>
    /// Loan Quota Coefficient
    /// </summary>
    [JsonProperty("loanQuotaCoef")]
    public decimal LoanQuotaCoefficient { get; set; }
}

/// <summary>
/// OKX Public Interest Rate Currency
/// </summary>
public record OkxPublicInterestRateCurrency
{
    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Daily rate
    /// </summary>
    [JsonProperty("rate")]
    public decimal? DailyRate { get; set; }
}

/// <summary>
/// OKX Public Interest Rate Configuration
/// </summary>
public record OkxPublicInterestRateConfiguration
{
    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Strategy type
    /// 0: general strategy
    /// 1: delta neutral strategy
    /// If only 0 is returned for a currency, it means the loan quota is shared between accounts in general strategy and accounts in delta neutral strategy; if both 0/1 are returned for a currency, it means accounts in delta neutral strategy have separate loan quotas.
    /// </summary>
    [JsonProperty("ccy")]
    public OkxAccountStrategyType? StrategyType { get; set; }

    /// <summary>
    /// Loan quota in absolute value
    /// </summary>
    [JsonProperty("quota")]
    public string LoanQuota { get; set; } = "";

    /// <summary>
    /// VIP level
    /// </summary>
    [JsonProperty("level")]
    public string VipLevel { get; set; } = "";
}
