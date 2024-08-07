﻿namespace OKX.Api.Public.Models;

/// <summary>
/// OKX Interest Rate
/// </summary>
public class OkxInterestRateLoanQuota
{
    /// <summary>
    /// Basic
    /// </summary>
    [JsonProperty("basic")]
    public List<OkxPublicInterestRateBasic> Basic { get; set; }

    /// <summary>
    /// VIP
    /// </summary>
    [JsonProperty("vip")]
    public List<OkxPublicInterestRateVip> Vip { get; set; }

    /// <summary>
    /// Regular
    /// </summary>
    [JsonProperty("regular")]
    public List<OkxPublicInterestRateRegular> regular { get; set; }

}

/// <summary>
/// OKX Public Interest Rate Basic
/// </summary>
public class OkxPublicInterestRateBasic
{
    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    /// <summary>
    /// Quota
    /// </summary>
    [JsonProperty("quota")]
    public decimal? Quota { get; set; }

    /// <summary>
    /// Rate
    /// </summary>
    [JsonProperty("rate")]
    public decimal? Rate { get; set; }
}

/// <summary>
/// OKX Public Interest Rate VIP
/// </summary>
public class OkxPublicInterestRateVip
{
    /// <summary>
    /// Interest Rate Discount
    /// </summary>
    [JsonProperty("irDiscount")]
    public decimal? InterestRateDiscount { get; set; }

    /// <summary>
    /// Loan Quota Coefficient
    /// </summary>
    [JsonProperty("loanQuotaCoef")]
    public decimal? LoanQuotaCoefficient { get; set; }

    /// <summary>
    /// Level
    /// </summary>
    [JsonProperty("level")]
    public string Level { get; set; }
}

/// <summary>
/// OKX Public Interest Rate Regular
/// </summary>
public class OkxPublicInterestRateRegular
{
    /// <summary>
    /// Interest Rate Discount
    /// </summary>
    [JsonProperty("irDiscount")]
    public decimal? InterestRateDiscount { get; set; }

    /// <summary>
    /// Loan Quota Coefficient
    /// </summary>
    [JsonProperty("loanQuotaCoef")]
    public decimal? LoanQuotaCoefficient { get; set; }

    /// <summary>
    /// Level
    /// </summary>
    [JsonProperty("level")]
    public string Level { get; set; }
}
