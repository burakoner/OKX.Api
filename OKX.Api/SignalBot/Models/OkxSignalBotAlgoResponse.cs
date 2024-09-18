namespace OKX.Api.SignalBot.Models;

/// <summary>
/// OKX Signal Algo Response
/// </summary>
public class OkxSignalBotAlgoResponse : OkxRestApiErrorBase
{
    /// <summary>
    /// Algo ID
    /// </summary>
    [JsonProperty("algoId")]
    public long AlgoId { get; set; }
}