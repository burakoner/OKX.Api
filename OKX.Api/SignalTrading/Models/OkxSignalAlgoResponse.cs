namespace OKX.Api.SignalTrading.Models;

/// <summary>
/// OKX Signal Algo Response
/// </summary>
public class OkxSignalAlgoResponse : OkxRestApiErrorResponse
{
    /// <summary>
    /// Algo ID
    /// </summary>
    [JsonProperty("algoId")]
    public long AlgoId { get; set; }
}