namespace OKX.Api.Dca;

/// <summary>
/// OKX DCA Order Response
/// </summary>
public record OkxDcaOrderResponse : OkxRestApiErrorBase
{
    /// <summary>
    /// Algo ID
    /// </summary>
    [JsonProperty("algoId"), JsonConverter(typeof(LongAsStringNullableConverter))]
    public long? AlgoOrderId { get; set; }

    /// <summary>
    /// Client-supplied Algo ID
    /// </summary>
    [JsonProperty("algoClOrdId")]
    public string AlgoClientOrderId { get; set; } = string.Empty;

    /// <summary>
    /// Algo order type
    /// </summary>
    [JsonProperty("algoOrdType")]
    public OkxDcaAlgoOrderType? AlgoOrderType { get; set; }

    /// <summary>
    /// Order tag
    /// </summary>
    [JsonProperty("tag")]
    public string Tag { get; set; } = string.Empty;

    /// <summary>
    /// Extra amount transferred from trading account
    /// </summary>
    [JsonProperty("diffAmount"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? DifferenceAmount { get; set; }
}
