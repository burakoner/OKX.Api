namespace OKX.Api.Spread;

/// <summary>
/// Spread websocket amend-order request.
/// </summary>
public record OkxSpreadOrderAmendRequest
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

    /// <summary>
    /// Client request ID.
    /// </summary>
    [JsonProperty("reqId", NullValueHandling = NullValueHandling.Ignore)]
    public string? RequestId { get; set; }

    /// <summary>
    /// New quantity.
    /// </summary>
    [JsonProperty("newSz", NullValueHandling = NullValueHandling.Ignore)]
    public string? NewQuantity { get; set; }

    /// <summary>
    /// New price.
    /// </summary>
    [JsonProperty("newPx", NullValueHandling = NullValueHandling.Ignore)]
    public string? NewPrice { get; set; }
}
