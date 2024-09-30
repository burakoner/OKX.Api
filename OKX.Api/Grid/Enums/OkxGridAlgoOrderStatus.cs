namespace OKX.Api.Grid;

/// <summary>
/// OKX Grid Algo Order Status
/// </summary>
public enum OkxGridAlgoOrderStatus
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
    /// NoClosePosition
    /// </summary>
    [Map("no_close_position")]
    NoClosePosition = 4
}