namespace OKX.Api.RecurringBuy.Models;

/// <summary>
/// Recurring Buy Item
/// </summary>
public class OkxRecurringItem
{
    /// <summary>
    /// Recurring currency, e.g. BTC
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    /// <summary>
    /// Proportion of recurring currency assets, e.g. "0.2" representing 20%
    /// </summary>
    [JsonProperty("ratio")]
    public decimal Ratio { get; set; }
}