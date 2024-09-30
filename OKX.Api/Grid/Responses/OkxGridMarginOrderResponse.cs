namespace OKX.Api.Grid;

/// <summary>
/// OKX Grid Margin Order Response
/// </summary>
public record OkxGridMarginOrderResponse
{
    /// <summary>
    /// Algo Order Id
    /// </summary>
    [JsonProperty("algoId")]
    public string AlgoOrderId { get; set; } = string.Empty;

    /// <summary>
    /// Algo Client Order Id
    /// </summary>
    [JsonProperty("algoClOrdId")]
    public string AlgoClientOrderId { get; set; } = string.Empty;
}