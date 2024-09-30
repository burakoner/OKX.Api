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
    Cash,

    /// <summary>
    /// Cross
    /// </summary>
    [Map("cross")]
    Cross,

    /// <summary>
    /// Isolated
    /// </summary>
    [Map("isolated")]
    Isolated,

    /// <summary>
    /// Spot Isolated
    /// </summary>
    [Map("spot_isolated")]
    SpotIsolated
}