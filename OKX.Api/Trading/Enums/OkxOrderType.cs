namespace OKX.Api.Trade.Enums;

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
    /// Market order with immediate-or-cancel order (applicable only to Futures and Perpetual swap).
    /// </summary>
    OptimalLimitOrder,

    /// <summary>
    /// Market Maker Protection (only applicable to Option in Portfolio Margin mode)
    /// </summary>
    MarketMakerProtection,

    /// <summary>
    /// Marekt Maker Protection and Post-only order(only applicable to Option in Portfolio Margin mode)
    /// </summary>
    MarektMakerProtectionAndPostOnly
}