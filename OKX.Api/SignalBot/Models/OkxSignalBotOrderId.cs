namespace OKX.Api.SignalBot;

/// <summary>
/// OKX Signal Order Response
/// </summary>
public class OkxSignalBotOrderId : OkxRestApiErrorBase
{
    /// <summary>
    /// Algo ID
    /// </summary>
    [JsonProperty("algoId")]
    public long? AlgoId { get; set; }

    /// <summary>
    /// Client-supplied Algo ID
    /// </summary>
    [JsonProperty("algoClOrdId")]
    public string AlgoClientOrderId { get; set; }
}