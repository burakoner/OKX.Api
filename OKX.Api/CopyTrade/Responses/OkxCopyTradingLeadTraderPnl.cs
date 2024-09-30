namespace OKX.Api.CopyTrade;

/// <summary>
/// OKX Copy Trading Lead Traders Ranks
/// </summary>
public class OkxCopyTradingLeadTraderPnl
{
    /// <summary>
    /// Pnl
    /// </summary>
    [JsonProperty("pnl")]
    public decimal Pnl { get; set; }
    
    /// <summary>
    /// Pnl ratio
    /// </summary>
    [JsonProperty("pnlRatio")]
    public decimal PnlRatio { get; set; }
    
    /// <summary>
    /// Begin timestamp
    /// </summary>
    [JsonProperty("beginTs")]
    public long BeginTimestamp { get; set; }

    /// <summary>
    /// Begin time
    /// </summary>
    [JsonIgnore]
    public DateTime BeginTime => BeginTimestamp.ConvertFromMilliseconds();
}