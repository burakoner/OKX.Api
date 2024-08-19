namespace OKX.Api.Spread.Models;

/// <summary>
/// Cancel Order Response
/// </summary>
public class OkxSpreadOrderCancelResponse : OkxRestApiErrorResponse
{
    /// <summary>
    /// Order Id
    /// </summary>
    [JsonProperty("ordId")]
    public long? OrderId { get; set; }

    /// <summary>
    /// Client Order Id
    /// </summary>
    [JsonProperty("clOrdId")]
    public string ClientOrderId { get; set; }
}
