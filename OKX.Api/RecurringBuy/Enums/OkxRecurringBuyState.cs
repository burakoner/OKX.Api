namespace OKX.Api.RecurringBuy;

/// <summary>
/// OKX Recurring Buy State
/// </summary>
public enum OkxRecurringBuyState : byte
{
    /// <summary>
    /// Running
    /// </summary>
    [Map("running")]
    Running = 1,

    /// <summary>
    /// Stopping
    /// </summary>
    [Map("stopping")]
    Stopping = 2,

    /// <summary>
    /// Stopped
    /// </summary>
    [Map("stopped")]
    Stopped = 3,

    /// <summary>
    /// Paused
    /// </summary>
    [Map("pause")]
    Paused = 4
}
