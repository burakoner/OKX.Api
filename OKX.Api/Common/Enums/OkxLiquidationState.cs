namespace OKX.Api.Common;

/// <summary>
/// OKX Liquidation State
/// </summary>
public enum OkxLiquidationState
{
    /// <summary>
    /// Unfilled
    /// </summary>
    [Map("unfilled")]
    Unfilled,

    /// <summary>
    /// Filled
    /// </summary>
    [Map("filled")]
    Filled,
}