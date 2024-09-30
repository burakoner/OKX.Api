namespace OKX.Api.SignalBot;

/// <summary>
/// OKX Signal Algo Id
/// </summary>
internal record OkxSignalBotAlgoId
{
    /// <summary>
    /// Algo ID
    /// </summary>
    [JsonProperty("algoId")]
    public string Data { get; set; } = string.Empty;
}