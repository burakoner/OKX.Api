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
    /// Algo Client Order ID
    /// </summary>
    [JsonProperty("algoClOrdId")]
    public string ClientAlgoOrderId { get; set; } = string.Empty;
}
