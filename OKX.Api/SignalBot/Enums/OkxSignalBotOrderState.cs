namespace OKX.Api.SignalBot;

/// <summary>
/// Signal order state
/// </summary>
public enum OkxSignalBotOrderState : byte
{
    /// <summary>
    /// Starting
    /// </summary>
    [Map("starting")]
    Starting = 1,

    /// <summary>
    /// Running
    /// </summary>
    [Map("running")]
    Running = 2,

    /// <summary>
    /// Stopping
    /// </summary>
    [Map("stopping")]
    Stopping = 3,

    /// <summary>
    /// Stopped
    /// </summary>
    [Map("stopped")]
    Stopped = 4,
}
