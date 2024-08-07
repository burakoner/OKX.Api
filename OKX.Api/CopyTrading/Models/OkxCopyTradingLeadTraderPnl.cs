﻿namespace OKX.Api.CopyTrading.Models;

/// <summary>
/// OKX Copy Trading Lead Traders Ranks
/// </summary>
public class OkxCopyTradingLeadTraderPnl
{
    /// <summary>
    /// Begin timestamp
    /// </summary>
    [JsonProperty("beginTs")]
    public long BeginTimestamp { get; set; }

    /// <summary>
    /// Begin time
    /// </summary>
    [JsonIgnore]
    public DateTime BeginTime { get { return BeginTimestamp.ConvertFromMilliseconds(); } }
    
    /// <summary>
    /// Pnl ratio
    /// </summary>
    [JsonProperty("pnlRatio")]
    public decimal PnlRatio { get; set; }

    /// <summary>
    /// Pnl
    /// </summary>
    [JsonProperty("pnl")]
    public decimal Pnl { get; set; }
}