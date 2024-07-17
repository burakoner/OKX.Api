using OKX.Api.Common.Models;

namespace OKX.Api.Trading.Models;

/// <summary>
/// Place Order Response
/// </summary>
public class OkxOrderPlaceResponse : OkxRestApiErrorResponse
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
