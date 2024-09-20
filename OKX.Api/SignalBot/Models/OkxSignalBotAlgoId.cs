namespace OKX.Api.SignalBot.Models;

/// <summary>
/// OKX Signal Algo Id
/// </summary>
public class OkxSignalBotAlgoId
{
    /// <summary>
    /// Algo ID
    /// </summary>
    [JsonProperty("algoId")]
    public string AlgoId { get; set; }
}