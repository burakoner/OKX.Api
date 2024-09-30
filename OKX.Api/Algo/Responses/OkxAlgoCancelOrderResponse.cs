namespace OKX.Api.Algo;

/// <summary>
/// OKX Algo Order Cancel Response
/// </summary>
public record OkxAlgoCancelOrderResponse : OkxRestApiErrorBase
{
    /// <summary>
    /// Algo Order ID
    /// </summary>
    [JsonProperty("algoId")]
    public string AlgoOrderId { get; set; } = string.Empty;
}
