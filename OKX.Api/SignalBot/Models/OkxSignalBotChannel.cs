namespace OKX.Api.SignalBot;

/// <summary>
/// OKX SignalBot Signal
/// </summary>
public class OkxSignalBotChannel
{
    /// <summary>
    /// Signal Channel Id
    /// </summary>
    [JsonProperty("signalChanId")]
    public long SignalChannelId { get; set; }

    /// <summary>
    /// User identify when placing orders via signal
    /// </summary>
    [JsonProperty("signalChanToken")]
    public string SignalChannelToken { get; set; }
}