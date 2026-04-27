namespace OKX.Api.Spread;

/// <summary>
/// Place Order Response
/// </summary>
public record OkxSpreadOrderPlace : OkxRestApiErrorBase
{
    /// <summary>
    /// Order Id
    /// </summary>
    [JsonProperty("ordId")]
    [JsonConverter(typeof(LongAsStringNullableConverter))]
    public long? OrderId { get; set; }

    /// <summary>
    /// Client Order Id
    /// </summary>
    [JsonProperty("clOrdId")]
    public string ClientOrderId { get; set; } = string.Empty;

    /// <summary>
    /// Order tag.
    /// </summary>
    [JsonProperty("tag")]
    public string Tag { get; set; } = string.Empty;
}
