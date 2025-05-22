namespace OKX.Api.Account;

/// <summary>
/// OKX Margin Mode
/// </summary>
public enum OkxAccountMarginMode : byte
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