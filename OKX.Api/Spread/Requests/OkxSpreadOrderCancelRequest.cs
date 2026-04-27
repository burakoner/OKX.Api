namespace OKX.Api.Spread;

/// <summary>
/// Spread websocket cancel-order request.
/// </summary>
public record OkxSpreadOrderCancelRequest
{
    /// <summary>
    /// Order ID.
    /// </summary>
    [JsonProperty("ordId", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(LongAsStringNullableConverter))]
    public long? OrderId { get; set; }

    /// <summary>
    /// Client order ID.
    /// </summary>
    [JsonProperty("clOrdId", NullValueHandling = NullValueHandling.Ignore)]
    public string? ClientOrderId { get; set; }
}
