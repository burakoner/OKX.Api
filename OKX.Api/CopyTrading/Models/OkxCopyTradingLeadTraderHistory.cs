using OKX.Api.CopyTrading.Converters;
using OKX.Api.CopyTrading.Enums;

namespace OKX.Api.CopyTrading.Models;

/// <summary>
/// OKX Copy Trading Lead Trader History
/// </summary>
public class OkxCopyTradingLeadTraderHistory
{
    /// <summary>
    /// Portrait link
    /// </summary>
    [JsonProperty("portLink")]
    public string PortraitLink { get; set; }

    /// <summary>
    /// Nick name
    /// </summary>
    [JsonProperty("nickName")]
    public string NickName { get; set; }
    
    /// <summary>
    /// Lead trader unique code
    /// </summary>
    [JsonProperty("uniqueCode")]
    public string UniqueCode { get; set; }

    /// <summary>
    /// Number of times to copy order
    /// </summary>
    [JsonProperty("copyNum")]
    public string CopyNumber { get; set; }

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
    /// Copy amount per order in USDT
    /// </summary>
    [JsonProperty("copyAmt")]
    public decimal CopyAmount { get; set; }
    
    /// <summary>
    /// Copy mode
    /// </summary>
    [JsonProperty("copyMode"), JsonConverter(typeof(OkxCopyTradingCopyModeConverter))]
    public OkxCopyTradingCopyMode CopyMode { get; set; }
    
    /// <summary>
    /// Copy ratio per order
    /// </summary>
    [JsonProperty("copyRatio")]
    public decimal CopyRatio { get; set; }

    /// <summary>
    /// margin currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; }
    
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
    public DateTime BeginTime { get { return BeginTimestamp.ConvertFromMilliseconds(); } }
        
    /// <summary>
    /// Stop copying time. Unix timestamp format in milliseconds, e.g.1597026383085
    /// </summary>
    [JsonProperty("endCopyTime")]
    public long? EndTimestamp { get; set; }

    /// <summary>
    /// Begin copying time.
    /// </summary>
    [JsonIgnore]
    public DateTime? EndTime { get { return EndTimestamp?.ConvertFromMilliseconds(); } }
    
    /// <summary>
    /// Copy relation ID
    /// </summary>
    [JsonProperty("copyRelId")]
    public long CopyRelationId { get; set; }
    
    /// <summary>
    /// Current copy state
    /// </summary>
    [JsonProperty("copyState"), JsonConverter(typeof(OkxCopyTradingStateConverter))]
    public OkxCopyTradingState CopyState { get; set; }
    
    /// <summary>
    /// Lead mode public private
    /// </summary>
    [JsonProperty("leadMode"), JsonConverter(typeof(OkxCopyTradingLeadingModeConverter))]
    public OkxCopyTradingLeadingMode LeadMode { get; set; }
}
