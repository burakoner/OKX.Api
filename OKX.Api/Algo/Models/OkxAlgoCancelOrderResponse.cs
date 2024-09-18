namespace OKX.Api.Algo.Models;

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
