namespace OKX.Api.SignalBot;

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
    public string SignalChannelName { get; set; } = string.Empty;

    /// <summary>
    /// Signal channel description
    /// </summary>
    [JsonProperty("signalChanDesc")]
    public string SignalChannelDescription { get; set; } = string.Empty;
    
    /// <summary>
    /// User identify when placing orders via signal
    /// </summary>
    [JsonProperty("signalChanToken")]
    public string SignalChannelToken { get; set; } = string.Empty;

    /// <summary>
    /// Signal source type
    /// </summary>
    [JsonProperty("signalSourceType"), JsonConverter(typeof(OkxSignalBotSourceTypeConverter))]
    public OkxSignalBotSourceType SignalSourceType { get; set; }
}