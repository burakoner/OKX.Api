namespace OKX.Api.Grid;

/// <summary>
/// OKX Grid Order Response
/// </summary>
public record OkxGridOrderAmendResponse : OkxRestApiErrorBase
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