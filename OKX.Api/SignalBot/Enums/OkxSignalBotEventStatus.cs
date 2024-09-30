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
    Success = 1,

    /// <summary>
    /// Failure
    /// </summary>
    [Map("failure")]
    Failure = 2,
}
