namespace OKX.Api.Algo;

/// <summary>
/// OKX Algo Order Response
/// </summary>
public class OkxAlgoOrderResponse : OkxRestApiErrorBase
{
    /// <summary>
    /// Algo Order ID
    /// </summary>
    [JsonProperty("algoId")]
    public string AlgoOrderId { get; set; }

    /// <summary>
    /// Client Order ID as assigned by the client
    /// </summary>
    [Obsolete]
    [JsonProperty("clOrdId")]
    public string ClientOrderId { get; set; }

    /// <summary>
    /// Algo Client Order ID
    /// </summary>
    [JsonProperty("algoClOrdId")]
    public string ClientAlgoOrderId { get; set; }
}
