namespace OKX.Api.SignalBotTrading.Models;

/// <summary>
/// OKX Signal Order Response
/// </summary>
public class OkxSignalOrderResponse : OkxRestApiErrorResponse
{
    /// <summary>
    /// Algo ID
    /// </summary>
    [JsonProperty("algoId")]
    public long AlgoId { get; set; }

    /// <summary>
    /// Client-supplied Algo ID
    /// </summary>
    [JsonProperty("algoClOrdId")]
    public string AlgoClientOrderId { get; set; }
}