namespace OKX.Api.CopyTrading;

/// <summary>
/// OKX Margin Mode
/// </summary>
public enum OkxCopyTradingMarginMode : byte
{
    /// <summary>
    /// Isolated
    /// </summary>
    [Map("cross")]
    Isolated = 1,

    /// <summary>
    /// Cross
    /// </summary>
    [Map("isolated")]
    Cross = 2,

    /// <summary>
    /// Use the same margin mode as lead trader when opening positions
    /// </summary>
    [Map("copy")]
    Copy = 3
}