namespace OKX.Api.Enums;

/// <summary>
/// OKX Order Type
/// </summary>
public enum OkxOrderType
{
    /// <summary>
    /// MarketOrder
    /// </summary>
    MarketOrder,

    /// <summary>
    /// LimitOrder
    /// </summary>
    LimitOrder,

    /// <summary>
    /// PostOnly
    /// </summary>
    PostOnly,

    /// <summary>
    /// FillOrKill
    /// </summary>
    FillOrKill,

    /// <summary>
    /// ImmediateOrCancel
    /// </summary>
    ImmediateOrCancel,

    /// <summary>
    /// OptimalLimitOrder
    /// </summary>
    OptimalLimitOrder,
}