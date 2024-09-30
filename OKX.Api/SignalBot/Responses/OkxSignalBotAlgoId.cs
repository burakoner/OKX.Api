namespace OKX.Api.SignalBot;

/// <summary>
/// OKX Signal Algo Id
/// </summary>
public class OkxSignalBotAlgoId
{
    /// <summary>
    /// Algo ID
    /// </summary>
    [JsonProperty("algoId")]
    public string AlgoId { get; set; } = string.Empty;
}