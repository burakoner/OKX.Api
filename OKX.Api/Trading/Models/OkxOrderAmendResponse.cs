namespace OKX.Api.Trading.Models;

/// <summary>
/// OKX Order Amend Response
/// </summary>
public class OkxOrderAmendResponse : OkxRestApiErrorResponse
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

    /// <summary>
    /// Request Id
    /// </summary>
    [JsonProperty("reqId")]
    public string RequestId { get; set; }
}
