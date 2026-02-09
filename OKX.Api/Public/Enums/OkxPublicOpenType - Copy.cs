namespace OKX.Api.Public;

/// <summary>
/// Okx Public ELP Permission
/// </summary>
public enum OkxPublicElpPermission : byte
{
    /// <summary>
    /// ELP is not enabled for this symbol
    /// </summary>
    [Map("0")]
    Disabled = 0,

    /// <summary>
    /// ELP is enabled for this symbol, but current users don't have permission to place ELP orders for it.
    /// </summary>
    [Map("1")]
    NoUserPermission = 1,

    /// <summary>
    /// ELP is enabled for this symbol, and current users have permission to place ELP orders for it.
    /// </summary>
    [Map("2")]
    Allowed = 2,
}