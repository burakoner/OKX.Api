namespace OKX.Api.Grid.Models;

/// <summary>
/// OKX Grid Order Response
/// </summary>
public class OkxGridOrderResponse : OkxRestApiErrorBase
{
    /// <summary>
    /// Algo Client Order Id
    /// </summary>
    [JsonProperty("algoClOrdId")]
    public string AlgoClientOrderId { get; set; }

    /// <summary>
    /// Algo Order Id
    /// </summary>
    [JsonProperty("algoId")]
    public string AlgoOrderId { get; set; }

    /// <summary>
    /// Order Id
    /// </summary>
    [JsonProperty("ordId")]
    public string OrderId { get; set; }
}