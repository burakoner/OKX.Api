namespace OKX.Api.CopyTrading;

/// <summary>
/// OKX Margin Mode
/// </summary>
public enum OkxCopyTradingMarginMode : byte
{
    /// <summary>
    /// Cross margin
    /// </summary>
    [Map("cross")]
    Cross = 1,

    /// <summary>
    /// Isolated margin
    /// </summary>
    [Map("isolated")]
    Isolated = 2,

    /// <summary>
    /// Use the same margin mode as lead trader when opening positions
    /// </summary>
    [Map("copy")]
    Copy = 3
}
