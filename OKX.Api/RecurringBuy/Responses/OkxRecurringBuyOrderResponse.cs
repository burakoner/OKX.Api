namespace OKX.Api.RecurringBuy;

/// <summary>
/// Recurring Buy Order Response
/// </summary>
public record OkxRecurringBuyOrderResponse : OkxRestApiErrorBase
{
    /// <summary>
    /// Algo ID
    /// </summary>
    [JsonProperty("algoId")]
    public long OrderId { get; set; }

    /// <summary>
    /// Client-supplied Algo ID
    /// </summary>
    [JsonProperty("algoClOrdId")]
    public string ClientOrderId { get; set; } = string.Empty;
}