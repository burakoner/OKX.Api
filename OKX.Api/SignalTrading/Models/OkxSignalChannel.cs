using OKX.Api.SignalTrading.Converters;

namespace OKX.Api.SignalTrading.Models;

/// <summary>
/// OKX Signal Channel
/// </summary>
public class OkxSignalChannel
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
    [JsonProperty("signalSourceType"), JsonConverter(typeof(OkxSignalSourceTypeConverter))]
    public string SignalSourceType { get; set; }
}