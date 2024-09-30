namespace OKX.Api.RecurringBuy;

/// <summary>
/// Recurring Buy Item
/// </summary>
public class OkxRecurringBuyItem
{
    /// <summary>
    /// Recurring currency, e.g. BTC
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = "";

    /// <summary>
    /// Proportion of recurring currency assets, e.g. "0.2" representing 20%
    /// </summary>
    [JsonProperty("ratio")]
    public string Ratio { get; set; } = "";
}