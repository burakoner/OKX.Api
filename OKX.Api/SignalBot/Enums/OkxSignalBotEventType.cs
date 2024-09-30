namespace OKX.Api.SignalBot;

/// <summary>
/// OKX Signal Bot Event Type
/// </summary>
public enum OkxSignalBotEventType
{
    /// <summary>
    /// SystemAction
    /// </summary>
    [Map("system_action")]
    SystemAction,

    /// <summary>
    /// UserAction
    /// </summary>
    [Map("user_action")]
    UserAction,

    /// <summary>
    /// SignalProcessing
    /// </summary>
    [Map("signal_processing")]
    SignalProcessing
}
