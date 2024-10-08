﻿namespace OKX.Api.Financial;

/// <summary>
/// OKX Financial Fixed Simple Earn Lending APY History
/// </summary>
public record OkxFinancialFixedSimpleEarnLendingApyHistory
{
    /// <summary>
    /// Currency type, e.g. BTC
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Latest lending APY, in decimal.
    /// e.g. 0.01 represent 1%
    /// </summary>
    [JsonProperty("rate")]
    public decimal Rate { get; set; }

    /// <summary>
    /// Timestamp for lending, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Timestamp for lending
    /// </summary>
    [JsonIgnore]
    public DateTime Time => Timestamp.ConvertFromMilliseconds();

}