namespace OKX.Api.Spread;

/// <summary>
/// Cancel Order Response
/// </summary>
public class OkxSpreadOrderCancelResponse : OkxRestApiErrorBase
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
