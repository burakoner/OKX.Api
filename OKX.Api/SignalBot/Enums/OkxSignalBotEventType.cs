namespace OKX.Api.SignalBot;

/// <summary>
/// OKX Signal Bot Event Type
/// </summary>
public enum OkxSignalBotEventType : byte
{
    /// <summary>
    /// SystemAction
    /// </summary>
    [Map("system_action")]
    SystemAction = 1,

    /// <summary>
    /// UserAction
    /// </summary>
    [Map("user_action")]
    UserAction = 2,

    /// <summary>
    /// SignalProcessing
    /// </summary>
    [Map("signal_processing")]
    SignalProcessing = 3
}
