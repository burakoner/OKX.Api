namespace OKX.Api.RecurringBuy;

/// <summary>
/// Recurring Buy Item
/// </summary>
public record OkxRecurringBuyItem
{
    /// <summary>
    /// Recurring currency, e.g. BTC
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Proportion of recurring currency assets, e.g. "0.2" representing 20%
    /// </summary>
    [JsonProperty("ratio")]
    public string Ratio { get; set; } = string.Empty;
}