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
    Unfilled = 1,

    /// <summary>
    /// Filled
    /// </summary>
    [Map("filled")]
    Filled = 2,
}