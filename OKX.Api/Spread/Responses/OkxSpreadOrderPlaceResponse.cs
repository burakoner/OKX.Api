namespace OKX.Api.Spread;

/// <summary>
/// Place Order Response
/// </summary>
public class OkxSpreadOrderPlaceResponse : OkxRestApiErrorBase
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
    public string ClientOrderId { get; set; } = "";
}
