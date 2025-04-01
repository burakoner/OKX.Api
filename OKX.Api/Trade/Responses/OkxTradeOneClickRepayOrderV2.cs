namespace OKX.Api.Trade;

/// <summary>
/// OKX One Click Repay Order V2
/// </summary>
public record OkxTradeOneClickRepayOrderV2
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
