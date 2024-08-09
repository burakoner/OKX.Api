namespace OKX.Api.Algo.Models;

/// <summary>
/// OKX Algo Order Response
/// </summary>
public class OkxAlgoOrderResponse : OkxRestApiErrorResponse
{
    /// <summary>
    /// Algo Order ID
    /// </summary>
    [JsonProperty("algoId")]
    public long? AlgoOrderId { get; set; }

    /// <summary>
    /// Client Order ID as assigned by the client
    /// </summary>
    [JsonProperty("clOrdId")]
    public string ClientOrderId { get; set; }

    /// <summary>
    /// Algo Client Order ID
    /// </summary>
    [JsonProperty("algoClOrdId")]
    public string ClientAlgoOrderId { get; set; }
}
