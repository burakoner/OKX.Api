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
    Scheduled,

    /// <summary>
    /// Ongoing
    /// </summary>
    [Map("ongoing")]
    Ongoing,

    /// <summary>
    /// Pre-Open
    /// </summary>
    [Map("pre_open")]
    PreOpen,

    /// <summary>
    /// Completed
    /// </summary>
    [Map("completed")]
    Completed,

    /// <summary>
    /// Canceled
    /// </summary>
    [Map("canceled")]
    Canceled,
}