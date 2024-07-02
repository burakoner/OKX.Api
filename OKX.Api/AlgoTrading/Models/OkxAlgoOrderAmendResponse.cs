namespace OKX.Api.AlgoTrading.Models;

/// <summary>
/// OKX Algo Order Amend Response
/// </summary>
public class OkxAlgoOrderAmendResponse : OkxRestApiErrorResponse
{
    /// <summary>
    /// Algo Client Order ID
    /// </summary>
    [JsonProperty("algoClOrdId")]
    public string AlgoClientOrderId { get; set; }

    /// <summary>
    /// Algo Order ID
    /// </summary>
    [JsonProperty("algoId")]
    public long? AlgoOrderId { get; set; }

    /// <summary>
    /// Client Request ID
    /// </summary>
    [JsonProperty("reqId")]
    public string ClientRequestId { get; set; }
}
