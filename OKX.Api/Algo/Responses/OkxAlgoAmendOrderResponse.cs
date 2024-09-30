namespace OKX.Api.Algo;

/// <summary>
/// OKX Algo Order Amend Response
/// </summary>
public record OkxAlgoAmendOrderResponse : OkxRestApiErrorBase
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
    public string AlgoClientOrderId { get; set; } = string.Empty;

    /// <summary>
    /// Client Request ID
    /// </summary>
    [JsonProperty("reqId")]
    public string ClientRequestId { get; set; } = string.Empty;
}
