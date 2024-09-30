namespace OKX.Api.SignalBot;

/// <summary>
/// OKX Signal Algo Response
/// </summary>
public record OkxSignalBotAlgoIdResponse : OkxRestApiErrorBase
{
    /// <summary>
    /// Algo ID
    /// </summary>
    [JsonProperty("algoId")]
    public long AlgoId { get; set; }
}