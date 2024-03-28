namespace OKX.Api.RecurringBuy.Models;

/// <summary>
/// Recurring Buy Order Response
/// </summary>
public class OkxRecurringOrderResponse
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

    /// <summary>
    /// The code of the event execution result, 0 means success
    /// </summary>
    [JsonProperty("sCode")]
    public string ErrorCode { get; set; }

    /// <summary>
    /// Rejection message if the request is unsuccessful
    /// </summary>
    [JsonProperty("sMsg")]
    public string ErrorMessage { get; set; }
}