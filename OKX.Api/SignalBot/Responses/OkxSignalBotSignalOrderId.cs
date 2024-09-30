namespace OKX.Api.SignalBot;

/// <summary>
/// OKX Signal Bot Signal Order Id
/// </summary>
internal record OkxSignalBotSignalOrderId : OkxRestApiErrorBase
{
    /// <summary>
    /// Signal Order Id
    /// </summary>
    [JsonProperty("signalOrdId")]
    public long? Data { get; set; }
}