﻿namespace OKX.Api.CopyTrading.Models;

/// <summary>
/// OkxProfitSharingTotalUnrealized
/// </summary>
public class OkxCopyTradingProfitSharingTotalUnrealized
{
    /// <summary>
    /// Total unrealized profit sharing amount
    /// </summary>
    [JsonProperty("totalUnrealizedProfitSharingAmt")]
    public decimal TotalUnrealizedProfitSharingAmt { get; set; }

    /// <summary>
    /// The settlement time for the total unrealized profit sharing amount. Unix timestamp format in milliseconds, e.g.1597026383085
    /// </summary>
    [JsonProperty("profitSharingTs")]
    public long Timestamp { get; set; }

    /// <summary>
    /// The settlement time for the total unrealized profit sharing amount.
    /// </summary>
    [JsonIgnore]
    public DateTime Time { get { return Timestamp.ConvertFromMilliseconds(); } }
}
