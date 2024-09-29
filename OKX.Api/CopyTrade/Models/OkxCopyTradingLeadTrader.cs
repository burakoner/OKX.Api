namespace OKX.Api.CopyTrade;

/// <summary>
/// OKX Copy Trading Lead Trader
/// </summary>
public class OkxCopyTradingLeadTrader
{
    /// <summary>
    /// Portrait link
    /// </summary>
    [JsonProperty("portLink")]
    public string PortraitLink { get; set; } = "";

    /// <summary>
    /// Nick name
    /// </summary>
    [JsonProperty("nickName")]
    public string NickName { get; set; } = "";

    /// <summary>
    /// Margin for copy trading
    /// </summary>
    [JsonProperty("margin")]
    public decimal Margin { get; set; }

    /// <summary>
    /// Copy total amount
    /// </summary>
    [JsonProperty("copyTotalAmt")]
    public decimal CopyTotalAmount { get; set; }

    /// <summary>
    /// Copy total pnl
    /// </summary>
    [JsonProperty("copyTotalPnl")]
    public decimal CopyTotalPnl { get; set; }

    /// <summary>
    /// Lead trader unique code
    /// </summary>
    [JsonProperty("uniqueCode")]
    public string UniqueCode { get; set; } = "";

    /// <summary>
    /// margin currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = "";

    /// <summary>
    /// Profit sharing ratio. 0.1 represents 10%
    /// </summary>
    [JsonProperty("profitSharingRatio")]
    public decimal ProfitSharingRatio { get; set; }
    
    /// <summary>
    /// Begin copying time. Unix timestamp format in milliseconds, e.g.1597026383085
    /// </summary>
    [JsonProperty("beginCopyTime")]
    public long BeginTimestamp { get; set; }

    /// <summary>
    /// Begin copying time.
    /// </summary>
    [JsonIgnore]
    public DateTime BeginTime => BeginTimestamp.ConvertFromMilliseconds();
        
    /// <summary>
    /// Unrealized profit &amp; loss
    /// </summary>
    [JsonProperty("upl")]
    public decimal UnrealizedProfitLoss { get; set; }

    /// <summary>
    /// Today pnl
    /// </summary>
    [JsonProperty("todayPnl")]
    public decimal TodayProfitLoss { get; set; }

    /// <summary>
    /// Lead mode public private
    /// </summary>
    [JsonProperty("leadMode"), JsonConverter(typeof(OkxCopyTradingLeadingModeConverter))]
    public OkxCopyTradingLeadingMode LeadMode { get; set; }
}
