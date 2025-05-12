namespace OKX.Api.Trade;

/// <summary>
/// OKX Order Type
/// </summary>
public enum OkxTradeOrderType : byte
{
    /// <summary>
    /// LimitOrder
    /// </summary>
    [Map("limit")]
    LimitOrder = 1,

    /// <summary>
    /// MarketOrder
    /// </summary>
    [Map("market")]
    MarketOrder = 2,

    /// <summary>
    /// PostOnly
    /// </summary>
    [Map("post_only")]
    PostOnly = 3,

    /// <summary>
    /// FillOrKill
    /// </summary>
    [Map("fok")]
    FillOrKill = 4,

    /// <summary>
    /// ImmediateOrCancel
    /// </summary>
    [Map("ioc")]
    ImmediateOrCancel = 5,

    /// <summary>
    /// Market order with immediate-or-cancel order (applicable only to Futures and Perpetual swap).
    /// </summary>
    [Map("optimal_limit_ioc")]
    OptimalLimitOrder = 6,

    /// <summary>
    /// Market Maker Protection (only applicable to Option in Portfolio Margin mode)
    /// </summary>
    [Map("mmp")]
    MarketMakerProtection = 7,

    /// <summary>
    /// Marekt Maker Protection and Post-only order(only applicable to Option in Portfolio Margin mode)
    /// </summary>
    [Map("mmp_and_post_only")]
    MarektMakerProtectionAndPostOnly = 8,

    /// <summary>
    /// Simple options (fok)
    /// </summary>
    [Map("op_fok")]
    SimpleOptionsFillOrKill = 9,
}