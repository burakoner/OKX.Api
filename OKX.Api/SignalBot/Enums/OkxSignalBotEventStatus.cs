namespace OKX.Api.SignalBot;

/// <summary>
/// OKX Signal Bot Event Status
/// </summary>
public enum OkxSignalBotEventStatus
{
    /// <summary>
    /// Success
    /// </summary>
    [Map("success")]
    Success,

    /// <summary>
    /// Failure
    /// </summary>
    [Map("failure")]
    Failure,
}
