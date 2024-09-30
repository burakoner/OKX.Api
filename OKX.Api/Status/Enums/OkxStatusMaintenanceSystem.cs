namespace OKX.Api.Status;

/// <summary>
/// OKX Maintenance System
/// </summary>
public enum OkxStatusMaintenanceSystem
{
    /// <summary>
    /// Classic
    /// </summary>
    [Map("classic")]
    Classic = 1,

    /// <summary>
    /// Unified
    /// </summary>
    [Map("unified")]
    Unified = 2,
}