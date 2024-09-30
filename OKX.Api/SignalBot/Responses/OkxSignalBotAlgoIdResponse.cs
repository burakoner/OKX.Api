namespace OKX.Api.SignalBot;

/// <summary>
/// OKX Signal Algo Response
/// </summary>
internal record OkxSignalBotAlgoIdResponse : OkxRestApiErrorBase
{
    /// <summary>
    /// Algo ID
    /// </summary>
    [JsonProperty("algoId")]
    public long Data { get; set; }
}