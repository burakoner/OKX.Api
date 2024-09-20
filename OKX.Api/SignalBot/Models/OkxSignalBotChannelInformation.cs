using OKX.Api.SignalBot.Converters;
using OKX.Api.SignalBot.Enums;

namespace OKX.Api.SignalBot.Models;

/// <summary>
/// OKX SignalBot Signal Details
/// </summary>
public class OkxSignalBotChannelInformation
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
    public string SignalChannelName { get; set; }

    /// <summary>
    /// Signal channel description
    /// </summary>
    [JsonProperty("signalChanDesc")]
    public string SignalChannelDescription { get; set; }
    
    /// <summary>
    /// User identify when placing orders via signal
    /// </summary>
    [JsonProperty("signalChanToken")]
    public string SignalChannelToken { get; set; }

    /// <summary>
    /// Signal source type
    /// </summary>
    [JsonProperty("signalSourceType"), JsonConverter(typeof(OkxSignalBotSourceTypeConverter))]
    public OkxSignalBotSourceType SignalSourceType { get; set; }
}