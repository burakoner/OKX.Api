namespace OKX.Api.Trade;

/// <summary>
/// OKX One Click Repay Order History V2
/// </summary>
public record OkxTradeOneClickRepayOrderHistoryV2
{
    /// <summary>
    /// Debt currency
    /// </summary>
    [JsonProperty("debtCcy")]
    public string DeptCurrency { get; set; } = string.Empty;

    /// <summary>
    /// Repay currency list, e.g. ["USDC","BTC"]
    /// </summary>
    [JsonProperty("repayCcyList")]
    public List<string> RepayCurrencies { get; set; } = [];

    /// <summary>
    /// Filled amount of debt currency
    /// </summary>
    [JsonProperty("fillDebtSz")]
    public decimal FilledDebtAmount { get; set; }

    /// <summary>
    /// Current status of one-click repay
    /// </summary>
    [JsonProperty("status")]
    public OkxTradeOneClickRepayOrderStatus Status { get; set; }

    /// <summary>
    /// Order info
    /// </summary>
    [JsonProperty("ordIdInfo")]
    public List<OkxTradeOneClickRepayOrderInfoV2> Orders { get; set; } = [];

    /// <summary>
    /// Request time, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Request time
    /// </summary>
    [JsonIgnore]
    public DateTime Time => Timestamp.ConvertFromMilliseconds();
}
