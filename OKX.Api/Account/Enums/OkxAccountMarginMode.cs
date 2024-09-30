namespace OKX.Api.Account;

/// <summary>
/// OKX Margin Mode
/// </summary>
public enum OkxAccountMarginMode
{
    /// <summary>
    /// Isolated
    /// </summary>
    [Map("isolated")]
    Isolated = 1,

    /// <summary>
    /// Cross
    /// </summary>
    [Map("cross")]
    Cross = 2,
}