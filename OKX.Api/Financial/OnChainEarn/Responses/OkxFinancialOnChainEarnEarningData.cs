﻿namespace OKX.Api.Financial.OnChainEarn;

/// <summary>
/// OKX Financial On Chain Earn Earning Data
/// </summary>
public class OkxFinancialOnChainEarnEarningData
{
    /// <summary>
    /// Currency type, e.g. BTC
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = "";

    /// <summary>
    /// Earning type
    /// </summary>
    [JsonProperty("earningType"), JsonConverter(typeof(OkxFinancialOnChainEarnEarningTypeConverter))]
    public OkxFinancialOnChainEarnEarningType EarningType { get; set; }

    /// <summary>
    /// Earning amount
    /// </summary>
    [JsonProperty("earnings")]
    public string Earnings { get; set; } = "";
}