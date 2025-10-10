namespace OKX.Api.Algo;

/// <summary>
/// OKX Algo Order Request
/// </summary>
public record OkxAlgoCancelOrderRequest
{
    /// <summary>
    /// Instrument ID, e.g. BTC-USDT
    /// </summary>
    [JsonProperty("instId")]
    public string? InstrumentId { get; set; }

    /// <summary>
    /// Algo ID
    /// Either algoId or algoClOrdId is required. If both are passed, algoId will be used.
    /// </summary>
    [JsonProperty("algoId")]
    public string? AlgoOrderId { get; set; }

    /// <summary>
    /// Client-supplied Algo ID
    /// Either algoId or algoClOrdId is required. If both are passed, algoId will be used.
    /// </summary>
    [JsonProperty("algoClOrdId")]
    public string? AlgoClientOrderId { get; set; }
}
