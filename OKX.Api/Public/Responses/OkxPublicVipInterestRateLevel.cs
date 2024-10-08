﻿namespace OKX.Api.Public;

/// <summary>
/// OKX VIP Interest Rate Level
/// </summary>
public record OkxPublicVipInterestRateLevel
{
    /// <summary>
    /// Level
    /// </summary>
    [JsonProperty("level")]
    public string Level { get; set; } = string.Empty;

    /// <summary>
    /// Loan Quota
    /// </summary>
    [JsonProperty("loanQuota")]
    public decimal LoanQuota { get; set; }
}
