namespace OKX.Api.Spread;

/// <summary>
/// Amend Order Response
/// </summary>
public class OkxSpreadOrderAmendResponse : OkxRestApiErrorBase
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
    public string ClientOrderId { get; set; } = string.Empty;

    /// <summary>
    /// Request Id
    /// </summary>
    [JsonProperty("reqId")]
    public string RequestId { get; set; } = string.Empty;
}
