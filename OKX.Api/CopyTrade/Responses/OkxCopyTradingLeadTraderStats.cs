namespace OKX.Api.CopyTrade;

/// <summary>
/// OKX Copy Trading Lead Trader Stats
/// </summary>
public class OkxCopyTradingLeadTraderStats
{
    /// <summary>
    /// Win ratio
    /// </summary>
    [JsonProperty("winRatio")]
    public decimal WinRatio { get; set; }

    /// <summary>
    /// Profit days
    /// </summary>
    [JsonProperty("profitDays")]
    public int ProfitDays { get; set; }

    /// <summary>
    /// Loss days
    /// </summary>
    [JsonProperty("lossDays")]
    public int LossDays { get; set; }

    /// <summary>
    /// Current copy trader pnl (USDT)
    /// </summary>
    [JsonProperty("curCopyTraderPnl")]
    public decimal CurrentCopyTraderPnl { get; set; }

    /// <summary>
    /// Average lead position notional (USDT)
    /// </summary>
    [JsonProperty("avgSubPosNotional")]
    public decimal AverageLeadPositionNotional { get; set; }

    /// <summary>
    /// Investment amount (USDT)
    /// </summary>
    [JsonProperty("investAmt")]
    public decimal InvestmentAmount { get; set; }

    /// <summary>
    /// Margin currency
    /// </summary>
    [JsonProperty("ccy")]
    public decimal Currency { get; set; }
}