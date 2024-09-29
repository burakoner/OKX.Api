namespace OKX.Api.Algo;

/// <summary>
/// OKX Algo Order Amend Response
/// </summary>
public class OkxAlgoAmendOrderResponse : OkxRestApiErrorBase
{
    /// <summary>
    /// Algo Order ID
    /// </summary>
    [JsonProperty("algoId")]
    public string AlgoOrderId { get; set; }

    /// <summary>
    /// Algo Client Order ID
    /// </summary>
    [JsonProperty("algoClOrdId")]
    public string AlgoClientOrderId { get; set; }

    /// <summary>
    /// Client Request ID
    /// </summary>
    [JsonProperty("reqId")]
    public string ClientRequestId { get; set; }
}
