namespace OKX.Api.SignalBot;

/// <summary>
/// Signal order state
/// </summary>
public enum OkxSignalBotOrderState
{
    /// <summary>
    /// Starting
    /// </summary>
    [Map("starting")]
    Starting,

    /// <summary>
    /// Running
    /// </summary>
    [Map("running")]
    Running,

    /// <summary>
    /// Stopping
    /// </summary>
    [Map("stopping")]
    Stopping,

    /// <summary>
    /// Stopped
    /// </summary>
    [Map("stopped")]
    Stopped,
}
