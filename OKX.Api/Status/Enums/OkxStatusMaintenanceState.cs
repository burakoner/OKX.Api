namespace OKX.Api.Status;

/// <summary>
/// OKX Maintenance State
/// </summary>
public enum OkxStatusMaintenanceState
{
    /// <summary>
    /// Scheduled
    /// </summary>
    [Map("scheduled")]
    Scheduled = 1,

    /// <summary>
    /// Ongoing
    /// </summary>
    [Map("ongoing")]
    Ongoing = 2,

    /// <summary>
    /// Pre-Open
    /// </summary>
    [Map("pre_open")]
    PreOpen = 3,

    /// <summary>
    /// Completed
    /// </summary>
    [Map("completed")]
    Completed = 4,

    /// <summary>
    /// Canceled
    /// </summary>
    [Map("canceled")]
    Canceled = 5,
}