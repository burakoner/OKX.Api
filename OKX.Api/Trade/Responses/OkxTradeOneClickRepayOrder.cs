namespace OKX.Api.Trade;

/// <summary>
/// OKX One Click Repay Order
/// </summary>
public class OkxTradeOneClickRepayOrder
{
    /// <summary>
    /// Current status of one-click repay
    /// </summary>
    [JsonProperty("status")]
    public OkxTradeOneClickRepayOrderStatus Status { get; set; }

    /// <summary>
    /// Debt currency type
    /// </summary>
    [JsonProperty("debtCcy")]
    public string DeptCurrency { get; set; } = string.Empty;

    /// <summary>
    /// Repay currency type
    /// </summary>
    [JsonProperty("repayCcy")]
    public string RepayCurrency { get; set; } = string.Empty;

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
    /// Trade time, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("uTime")]
    public long UpdateTimestamp { get; set; }

    /// <summary>
    /// Trade time
    /// </summary>
    [JsonIgnore]
    public DateTime UpdateTime => UpdateTimestamp.ConvertFromMilliseconds();
}