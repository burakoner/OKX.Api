namespace OKX.Api.Algo;

/// <summary>
/// OKX Algo Order Response
/// </summary>
public record OkxAlgoOrderResponse : OkxRestApiErrorBase
{
    /// <summary>
    /// Algo Order ID
    /// </summary>
    [JsonProperty("algoId")]
    public string AlgoOrderId { get; set; } = string.Empty;

    /// <summary>
    /// Deprecated client order ID returned by OKX.
    /// </summary>
    [JsonProperty("clOrdId")]
    public string ClientOrderId { get; set; } = string.Empty;

    /// <summary>
    /// Algo Client Order ID
    /// </summary>
    [JsonProperty("algoClOrdId")]
    public string ClientAlgoOrderId { get; set; } = string.Empty;

    /// <summary>
    /// Order tag.
    /// </summary>
    [JsonProperty("tag")]
    public string Tag { get; set; } = string.Empty;
}
