namespace OKX.Api.SignalBot;

/// <summary>
/// OKX Signal Bot Suborder Type
/// </summary>
public enum OkxSignalBotSuborderType
{
    /// <summary>
    /// Live
    /// </summary>
    [Map("live")]
    Live,

    /// <summary>
    /// Filled
    /// </summary>
    [Map("filled")]
    Filled,
}
