﻿namespace OKX.Api.CopyTrading.Models;

/// <summary>
/// OKX Copy Trading Lead Trader Currency Preference
/// </summary>
public class OkxCopyTradingLeadTraderCurrencyPreference
{
    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    /// <summary>
    /// Ratio
    /// </summary>
    [JsonProperty("ratio")]
    public decimal Ratio { get; set; }
}