namespace OKX.Api.RecurringBuy;

/// <summary>
/// Recurring Buy Price Range
/// </summary>
public record OkxRecurringBuyPriceRange
{
    /// <summary>
    /// Recurring buy currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Minimum price of price range. Empty string means no limit.
    /// </summary>
    [JsonProperty("minPx")]
    public string MinimumPrice { get; set; } = string.Empty;

    /// <summary>
    /// Maximum price of price range. Empty string means no limit.
    /// </summary>
    [JsonProperty("maxPx")]
    public string MaximumPrice { get; set; } = string.Empty;
}
