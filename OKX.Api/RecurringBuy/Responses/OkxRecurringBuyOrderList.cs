namespace OKX.Api.RecurringBuy;

/// <summary>
/// Recurring Buy Item Details
/// </summary>
public record OkxRecurringBuyOrderList
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
    public decimal Ratio { get; set; }
}