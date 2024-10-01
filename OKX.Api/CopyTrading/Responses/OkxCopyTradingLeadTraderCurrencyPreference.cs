namespace OKX.Api.CopyTrading;

/// <summary>
/// OKX Copy Trading Lead Trader Currency Preference
/// </summary>
public record OkxCopyTradingLeadTraderCurrencyPreference
{
    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Ratio
    /// </summary>
    [JsonProperty("ratio")]
    public decimal Ratio { get; set; }
}