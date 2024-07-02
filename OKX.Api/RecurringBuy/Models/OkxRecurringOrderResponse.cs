namespace OKX.Api.RecurringBuy.Models;

/// <summary>
/// Recurring Buy Order Response
/// </summary>
public class OkxRecurringOrderResponse : OkxRestApiErrorResponse
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