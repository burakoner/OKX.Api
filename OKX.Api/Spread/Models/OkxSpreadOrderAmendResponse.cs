namespace OKX.Api.Spread.Models;

/// <summary>
/// Amend Order Response
/// </summary>
public class OkxSpreadOrderAmendResponse : OkxRestApiErrorResponse
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
