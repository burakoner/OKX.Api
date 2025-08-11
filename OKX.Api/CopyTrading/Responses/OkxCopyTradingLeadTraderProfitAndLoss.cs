namespace OKX.Api.CopyTrading;

/// <summary>
/// OKX Copy Trading Lead Traders Ranks
/// </summary>
public record OkxCopyTradingLeadTraderProfitAndLoss
{
    /// <summary>
    /// Pnl
    /// </summary>
    [JsonProperty("pnl")]
    public decimal ProfitAndLoss { get; set; }
    
    /// <summary>
    /// Pnl ratio
    /// </summary>
    [JsonProperty("pnlRatio")]
    public decimal ProfitAndLossRatio { get; set; }
    
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