namespace OKX.Api.RecurringBuy.Models;

/// <summary>
/// Recurring Buy Order Response
/// </summary>
public class OkxRecurringBuyOrderResponse : OkxRestApiErrorBase
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
    public string ClientOrderId { get; set; }
}