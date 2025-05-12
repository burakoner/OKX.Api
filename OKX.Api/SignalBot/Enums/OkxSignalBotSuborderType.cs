namespace OKX.Api.SignalBot;

/// <summary>
/// OKX Signal Bot Suborder Type
/// </summary>
public enum OkxSignalBotSuborderType : byte
{
    /// <summary>
    /// Live
    /// </summary>
    [Map("live")]
    Live = 1,

    /// <summary>
    /// Filled
    /// </summary>
    [Map("filled")]
    Filled = 2,
}
