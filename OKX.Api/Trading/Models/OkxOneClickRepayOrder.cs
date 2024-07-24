using OKX.Api.Trade.Converters;
using OKX.Api.Trade.Enums;

namespace OKX.Api.Trade.Models;

/// <summary>
/// OKX One Click Repay Order
/// </summary>
public class OkxOneClickRepayOrder
{
    /// <summary>
    /// Debt currency type
    /// </summary>
    [JsonProperty("debtCcy")]
    public string DeptCurrency { get; set; }

    /// <summary>
    /// Repay currency type
    /// </summary>
    [JsonProperty("repayCcy")]
    public string RepayCurrency { get; set; }

    /// <summary>
    /// Filled amount of debt currency
    /// </summary>
    [JsonProperty("fillDebtSz")]
    public decimal FilledDebtAmount { get; set; }

    /// <summary>
    /// Filled amount of repay currency
    /// </summary>
    [JsonProperty("fillRepaySz")]
    public decimal FilledRepayAmount { get; set; }
    
    /// <summary>
    /// Current status of one-click repay
    /// </summary>
    [JsonProperty("status"), JsonConverter(typeof(OkxOneClickRepayOrderStatusConverter))]
    public OkxOneClickRepayOrderStatus Status { get; set; }

    /// <summary>
    /// Trade time, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("uTime")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Trade time
    /// </summary>
    [JsonIgnore]
    public DateTime Time { get { return Timestamp.ConvertFromMilliseconds(); } }
}