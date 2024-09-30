namespace OKX.Api.RecurringBuy;

/// <summary>
/// Recurring Buy Order Details List
/// </summary>
public record OkxRecurringBuyOrderDetailsList
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

    /// <summary>
    /// Accumulated quantity in unit of recurring buy currency
    /// </summary>
    [JsonProperty("totalAmt")]
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Profit in unit of investmentCcy
    /// </summary>
    [JsonProperty("profit")]
    public decimal Profit { get; set; }

    /// <summary>
    /// Average price of recurring buy, quote currency is investmentCcy
    /// </summary>
    [JsonProperty("avgPx")]
    public decimal AveragePrice { get; set; }

    /// <summary>
    /// Current market price, quote currency is investmentCcy
    /// </summary>
    [JsonProperty("px")]
    public decimal Price { get; set; }
}
