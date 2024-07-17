namespace OKX.Api.Trading.Models;

/// <summary>
/// OKX Order Cancel Request
/// </summary>
public class OkxOrderCancelRequest
{
    /// <summary>
    /// Instrument Id
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; }

    /// <summary>
    /// Order Id
    /// </summary>
    [JsonProperty("ordId", NullValueHandling = NullValueHandling.Ignore)]
    public long? OrderId { get; set; }

    /// <summary>
    /// Client Order Id
    /// </summary>
    [JsonProperty("clOrdId", NullValueHandling = NullValueHandling.Ignore)]
    public string ClientOrderId { get; set; }
}