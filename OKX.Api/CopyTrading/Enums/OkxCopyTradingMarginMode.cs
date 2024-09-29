namespace OKX.Api.CopyTrading;

/// <summary>
/// OKX Margin Mode
/// </summary>
public enum OkxCopyTradingMarginMode
{
    /// <summary>
    /// Isolated
    /// </summary>
    Isolated,

    /// <summary>
    /// Cross
    /// </summary>
    Cross,

    /// <summary>
    /// Use the same margin mode as lead trader when opening positions
    /// </summary>
    Copy
}