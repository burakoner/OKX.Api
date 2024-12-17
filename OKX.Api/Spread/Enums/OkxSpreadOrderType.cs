namespace OKX.Api.Spread;

/// <summary>
/// OKX Spread Order Type
/// </summary>
public enum OkxSpreadOrderType
{
    /// <summary>
    /// Market order
    /// </summary>
    [Map("market")]
    MarketOrder = 4,

    /// <summary>
    /// Limit Order
    /// </summary>
    [Map("limit")]
    LimitOrder = 1,

    /// <summary>
    /// Post Only Order
    /// </summary>
    [Map("post_only")]
    PostOnly = 2,

    /// <summary>
    /// Immediate-Or-Cancel Order
    /// </summary>
    [Map("ioc")]
    ImmediateOrCancel = 3,
}