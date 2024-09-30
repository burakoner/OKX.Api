namespace OKX.Api.SignalBot;

/// <summary>
/// OKX Signal Bot Signal Order Id
/// </summary>
public class OkxSignalBotSignalOrderId : OkxRestApiErrorBase
{
    /// <summary>
    /// Signal Order Id
    /// </summary>
    [JsonProperty("signalOrdId")]
    public long? SignalOrdId { get; set; }
}