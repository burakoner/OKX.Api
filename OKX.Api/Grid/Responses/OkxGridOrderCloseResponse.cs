namespace OKX.Api.Grid;

/// <summary>
/// OKX Grid Order Response
/// </summary>
public record OkxGridOrderCloseResponse : OkxRestApiErrorBase
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

    /// <summary>
    /// Close position order ID
    /// If mktClose is true, the parameter will return "".
    /// </summary>
    [JsonProperty("ordId")]
    public long? OrderId { get; set; }
}