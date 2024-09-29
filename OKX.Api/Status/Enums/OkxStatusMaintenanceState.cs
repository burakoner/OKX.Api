namespace OKX.Api.Status;

/// <summary>
/// OKX Maintenance State
/// </summary>
public enum OkxStatusMaintenanceState
{
    /// <summary>
    /// Scheduled
    /// </summary>
    Scheduled,

    /// <summary>
    /// Ongoing
    /// </summary>
    Ongoing,

    /// <summary>
    /// Pre-Open
    /// </summary>
    PreOpen,

    /// <summary>
    /// Completed
    /// </summary>
    Completed,

    /// <summary>
    /// Canceled
    /// </summary>
    Canceled,
}