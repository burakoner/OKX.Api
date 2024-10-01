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
    public long? Data { get; set; }
}