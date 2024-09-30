namespace OKX.Api.Trade;

/// <summary>
/// OKX Order Type
/// </summary>
public enum OkxTradeOrderType
{
    /// <summary>
    /// LimitOrder
    /// </summary>
    [Map("limit")]
    LimitOrder,

    /// <summary>
    /// MarketOrder
    /// </summary>
    [Map("market")]
    MarketOrder,

    /// <summary>
    /// PostOnly
    /// </summary>
    [Map("post_only")]
    PostOnly,

    /// <summary>
    /// FillOrKill
    /// </summary>
    [Map("fok")]
    FillOrKill,

    /// <summary>
    /// ImmediateOrCancel
    /// </summary>
    [Map("ioc")]
    ImmediateOrCancel,

    /// <summary>
    /// Market order with immediate-or-cancel order (applicable only to Futures and Perpetual swap).
    /// </summary>
    [Map("optimal_limit_ioc")]
    OptimalLimitOrder,

    /// <summary>
    /// Market Maker Protection (only applicable to Option in Portfolio Margin mode)
    /// </summary>
    [Map("mmp")]
    MarketMakerProtection,

    /// <summary>
    /// Marekt Maker Protection and Post-only order(only applicable to Option in Portfolio Margin mode)
    /// </summary>
    [Map("mmp_and_post_only")]
    MarektMakerProtectionAndPostOnly,

    /// <summary>
    /// Simple options (fok)
    /// </summary>
    [Map("op_fok")]
    SimpleOptionsFillOrKill,
}