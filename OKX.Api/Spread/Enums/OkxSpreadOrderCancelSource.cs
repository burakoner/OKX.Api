namespace OKX.Api.Spread;

/// <summary>
/// OKX Spread Order Cancel Source
/// </summary>
public enum OkxSpreadOrderCancelSource
{
    /// <summary>
    /// Order canceled by system
    /// </summary>
    [Map("0")]
    System = 0,

    /// <summary>
    /// Order canceled by user
    /// </summary>
    [Map("1")]
    User = 1,

    /// <summary>
    /// Order canceled: IOC order was partially canceled due to incompletely filled
    /// </summary>
    [Map("14")]
    PartiallyCanceled = 14,

    /// <summary>
    /// Order canceled: The order price is beyond the limit
    /// </summary>
    [Map("15")]
    PriceIsBeyondTheLimit = 15,

    /// <summary>
    /// Cancel all after triggered
    /// </summary>
    [Map("20")]
    CancelAllAfterTriggered = 20,

    /// <summary>
    /// The post-only order will take liquidity in maker orders
    /// </summary>
    [Map("31")]
    PostOnlyOrderTakeLiquidity = 31,

    /// <summary>
    /// Self trade prevention
    /// </summary>
    [Map("32")]
    SelfTradePrevention = 32,

    /// <summary>
    /// Order failed to settle due to insufficient margin
    /// </summary>
    [Map("34")]
    InsufficientMargin = 34,

    /// <summary>
    /// Order cancellation due to insufficient margin from another order
    /// </summary>
    [Map("35")]
    InsufficientMarginFromAnotherOrder = 35
}