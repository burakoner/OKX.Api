namespace OKX.Api.Models.Trade;

/// <summary>
/// Place Order Response
/// </summary>
public class OkxOrderPlaceResponse : OkxRestApiResponseModel
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
    /// Order tag. Used for Broker Id
    /// </summary>
    [JsonProperty("tag")]
    internal string Tag { get; set; }
}
