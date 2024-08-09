namespace OKX.Api.SignalBot.Models;

/// <summary>
/// OKX Signal Order Response
/// </summary>
public class OkxSignalBotOrderResponse : OkxRestApiErrorResponse
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