namespace OKX.Api.Spread;

/// <summary>
/// Spread websocket place-order request.
/// </summary>
public record OkxSpreadOrderPlaceRequest
{
    /// <summary>
    /// spread ID
    /// </summary>
    [JsonProperty("sprdId")]
    public string SpreadId { get; set; } = string.Empty;

    /// <summary>
    /// Client order ID.
    /// </summary>
    [JsonProperty("clOrdId", NullValueHandling = NullValueHandling.Ignore)]
    public string? ClientOrderId { get; set; }

    /// <summary>
    /// Order side.
    /// </summary>
    [JsonProperty("side")]
    public OkxTradeOrderSide OrderSide { get; set; }

    /// <summary>
    /// Order type.
    /// </summary>
    [JsonProperty("ordType")]
    public OkxSpreadOrderType OrderType { get; set; }

    /// <summary>
    /// Order price.
    /// </summary>
    [JsonProperty("px", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? Price { get; set; }

    /// <summary>
    /// Quantity.
    /// </summary>
    [JsonProperty("sz")]
    [JsonConverter(typeof(DecimalAsStringConverter))]
    public decimal Quantity { get; set; }

    /// <summary>
    /// Order tag.
    /// </summary>
    [JsonProperty("tag", NullValueHandling = NullValueHandling.Ignore)]
    public string? Tag { get; set; }
}
