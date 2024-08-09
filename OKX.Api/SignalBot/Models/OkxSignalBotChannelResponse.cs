namespace OKX.Api.SignalBot.Models;

/// <summary>
/// OKX Signal Channel Response
/// </summary>
public class OkxSignalBotChannelResponse
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