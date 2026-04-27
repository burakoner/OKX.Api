#pragma warning disable CS1591
using Newtonsoft.Json;

namespace OKX.Api.Spread;

/// <summary>
/// Spread websocket place order request
/// </summary>
public record OkxSpreadOrderPlaceRequest
{
    [JsonProperty("sprdId")]
    public string SpreadId { get; set; } = string.Empty;

    [JsonProperty("clOrdId", NullValueHandling = NullValueHandling.Ignore)]
    public string? ClientOrderId { get; set; }

    [JsonProperty("side")]
    public OkxTradeOrderSide OrderSide { get; set; }

    [JsonProperty("ordType")]
    public OkxSpreadOrderType OrderType { get; set; }

    [JsonProperty("px", NullValueHandling = NullValueHandling.Ignore)]
    public decimal? Price { get; set; }

    [JsonProperty("sz")]
    public decimal Quantity { get; set; }

    [JsonProperty("tag", NullValueHandling = NullValueHandling.Ignore)]
    public string? Tag { get; set; }
}
#pragma warning restore CS1591
