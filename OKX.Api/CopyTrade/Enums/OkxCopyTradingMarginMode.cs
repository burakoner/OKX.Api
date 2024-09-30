namespace OKX.Api.CopyTrade;

/// <summary>
/// OKX Margin Mode
/// </summary>
public enum OkxCopyTradingMarginMode
{
    /// <summary>
    /// Isolated
    /// </summary>
    [Map("cross")]
    Isolated,

    /// <summary>
    /// Cross
    /// </summary>
    [Map("isolated")]
    Cross,

    /// <summary>
    /// Use the same margin mode as lead trader when opening positions
    /// </summary>
    [Map("copy")]
    Copy
}