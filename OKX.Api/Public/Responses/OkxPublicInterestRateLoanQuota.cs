﻿namespace OKX.Api.Public;

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
