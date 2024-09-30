namespace OKX.Api.Spread;

/// <summary>
/// OKX Spread Order Type
/// </summary>
public enum OkxSpreadOrderType
{
    /// <summary>
    /// Limit Order
    /// </summary>
    [Map("limit")]
    LimitOrder,

    /// <summary>
    /// Post Only Order
    /// </summary>
    [Map("post_only")]
    PostOnly,

    /// <summary>
    /// Immediate-Or-Cancel Order
    /// </summary>
    [Map("ioc")]
    ImmediateOrCancel,
}