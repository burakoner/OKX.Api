namespace OKX.Api.CopyTrading.Models;

/// <summary>
/// OKX Copy Trading Copy Trader
/// </summary>
public class OkxCopyTradingCopyTrader
{
    /// <summary>
    /// Total copy trader profit and loss
    /// </summary>
    [JsonProperty("copyTotalPnl")]
    public decimal copyTotalPnl { get; set; }

    /// <summary>
    /// The currency name of profit and loss
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    /// <summary>
    /// Number change in last 7 days
    /// </summary>
    [JsonProperty("copyTraderNumChg")]
    public int CopyTraderNumberChange { get; set; }

    /// <summary>
    /// Ratio change in last 7 days
    /// </summary>
    [JsonProperty("copyTraderNumChgRatio")]
    public decimal CopyTraderNumberChangeRatio { get; set; }

    /// <summary>
    /// Copy trader information
    /// </summary>
    [JsonProperty("copyTraders")]
    public List<OkxCopyTradingCopyTraderRecord> CopyTraders { get; set; } = [];
}

/// <summary>
/// OKX Copy Trading Copy Trader Record
/// </summary>
public class OkxCopyTradingCopyTraderRecord
{
    /// <summary>
    /// Begin copying time. Unix timestamp format in milliseconds, e.g.1597026383085
    /// </summary>
    [JsonProperty("beginCopyTime")]
    public long BeginTimestamp { get; set; }
    
    /// <summary>
    /// Begin copying time.
    /// </summary>
    [JsonIgnore]
    public DateTime BeginTime { get { return BeginTimestamp.ConvertFromMilliseconds(); } }

    /// <summary>
    /// Nick name
    /// </summary>
    [JsonProperty("nickName")]
    public string NickName { get; set; }

    /// <summary>
    /// Copy trader portrait link
    /// </summary>
    [JsonProperty("portLink")]
    public string PortraitLink { get; set; }

    /// <summary>
    /// Copy trading profit and loss
    /// </summary>
    [JsonProperty("pnl")]
    public decimal Pnl { get; set; }
}