namespace OKX.Api.Algo;

/// <summary>
/// OKX Algo Order Cancel Response
/// </summary>
public class OkxAlgoCancelOrderResponse : OkxRestApiErrorBase
{
    /// <summary>
    /// Algo Order ID
    /// </summary>
    [JsonProperty("algoId")]
    public string AlgoOrderId { get; set; }
}
