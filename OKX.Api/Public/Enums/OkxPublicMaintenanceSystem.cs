namespace OKX.Api.Public;

/// <summary>
/// OKX Maintenance System
/// </summary>
public enum OkxPublicMaintenanceSystem : byte
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