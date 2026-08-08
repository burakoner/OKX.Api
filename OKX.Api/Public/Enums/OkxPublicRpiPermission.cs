namespace OKX.Api.Public;

/// <summary>
/// RPI maker permission for an instrument.
/// </summary>
public enum OkxPublicRpiPermission : byte
{
    /// <summary>
    /// RPI is not enabled for the instrument.
    /// </summary>
    [Map("0")]
    Disabled = 0,

    /// <summary>
    /// RPI is enabled, but the current user cannot place RPI orders.
    /// </summary>
    [Map("1")]
    NoUserPermission = 1,

    /// <summary>
    /// RPI is enabled and the current user can place RPI orders.
    /// </summary>
    [Map("2")]
    Allowed = 2,
}
