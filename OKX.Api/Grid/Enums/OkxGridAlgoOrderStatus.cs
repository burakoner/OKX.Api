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
    /// NoClosePosition
    /// </summary>
    [Map("no_close_position")]
    NoClosePosition
}