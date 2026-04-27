#pragma warning disable CS1591
using Newtonsoft.Json;

namespace OKX.Api.Spread;

/// <summary>
/// Spread websocket amend order request
/// </summary>
public record OkxSpreadOrderAmendRequest
{
    [JsonProperty("ordId", NullValueHandling = NullValueHandling.Ignore)]
    public long? OrderId { get; set; }

    [JsonProperty("clOrdId", NullValueHandling = NullValueHandling.Ignore)]
    public string? ClientOrderId { get; set; }

    [JsonProperty("reqId", NullValueHandling = NullValueHandling.Ignore)]
    public string? RequestId { get; set; }

    [JsonProperty("newSz", NullValueHandling = NullValueHandling.Ignore)]
    public string? NewQuantity { get; set; }

    [JsonProperty("newPx", NullValueHandling = NullValueHandling.Ignore)]
    public string? NewPrice { get; set; }
}
#pragma warning restore CS1591
