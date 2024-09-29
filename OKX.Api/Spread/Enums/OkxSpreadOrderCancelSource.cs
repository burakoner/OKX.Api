namespace OKX.Api.Spread;

/// <summary>
/// OKX Spread Order Cancel Source
/// </summary>
public enum OkxSpreadOrderCancelSource
{
    /// <summary>
    /// Order canceled by system
    /// </summary>
    System = 0,

    /// <summary>
    /// Order canceled by user
    /// </summary>
    User = 1,

    /// <summary>
    /// Order canceled: IOC order was partially canceled due to incompletely filled
    /// </summary>
    PartiallyCanceled = 14,

    /// <summary>
    /// The post-only order will take liquidity in maker orders
    /// </summary>
    PostOnlyOrderTakeLiquidity = 31,

    /// <summary>
    /// Order failed to settle due to insufficient margin
    /// </summary>
    InsufficientMargin = 34,

    /// <summary>
    /// Order cancellation due to insufficient margin from another order
    /// </summary>
    InsufficientMarginFromAnotherOrder = 35
}