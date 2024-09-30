namespace OKX.Api.Algo;

/// <summary>
/// OKX Algo Order Request
/// </summary>
public record OkxAlgoCancelOrderRequest
{
    /// <summary>
    /// Algo Client Order ID
    /// </summary>
    [JsonProperty("algoId")]
    public string? AlgoOrderId { get; set; }

    /// <summary>
    /// Instrument ID
    /// </summary>
    [JsonProperty("instId")]
    public string? InstrumentId { get; set; }
}
