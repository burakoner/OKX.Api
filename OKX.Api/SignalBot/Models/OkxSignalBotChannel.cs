using OKX.Api.SignalBot.Converters;

namespace OKX.Api.SignalBot.Models;

/// <summary>
/// OKX Signal Channel
/// </summary>
public class OkxSignalBotChannel
{
    /// <summary>
    /// Signal Channel Id
    /// </summary>
    [JsonProperty("signalChanId")]
    public long SignalChannelId { get; set; }
    
    /// <summary>
    /// Signal channel name
    /// </summary>
    [JsonProperty("signalChanName")]
    public string SignalChanName { get; set; }

    /// <summary>
    /// Signal channel description
    /// </summary>
    [JsonProperty("signalChanDesc")]
    public string signalChanDesc { get; set; }
    
    /// <summary>
    /// User identify when placing orders via signal
    /// </summary>
    [JsonProperty("signalChanToken")]
    public string SignalChannelToken { get; set; }

    /// <summary>
    /// Signal source type
    /// </summary>
    [JsonProperty("signalSourceType"), JsonConverter(typeof(OkxSignalBotSourceTypeConverter))]
    public string SignalSourceType { get; set; }
}