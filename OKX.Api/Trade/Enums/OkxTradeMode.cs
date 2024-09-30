namespace OKX.Api.Trade;

/// <summary>
/// OKX Trade Mode
/// </summary>
public enum OkxTradeMode
{
    /// <summary>
    /// Cash
    /// </summary>
    [Map("cash")]
    Cash = 1,

    /// <summary>
    /// Cross
    /// </summary>
    [Map("cross")]
    Cross = 2,

    /// <summary>
    /// Isolated
    /// </summary>
    [Map("isolated")]
    Isolated = 3,

    /// <summary>
    /// Spot Isolated
    /// </summary>
    [Map("spot_isolated")]
    SpotIsolated = 4
}