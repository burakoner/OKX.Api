namespace OKX.Api.Trade;

/// <summary>
/// OKX Order Source
/// </summary>
public enum OkxTradeOrderSource : byte
{
    /// <summary>
    /// Normal order
    /// </summary>
    [Map("0")]
    NormalOrder = 0,

    /// <summary>
    /// Retail Price Improvement order
    /// </summary>
    [Map("1")]
    RetailPriceImprovementOrder = 1,

    /// <summary>
    /// Deprecated name for <see cref="RetailPriceImprovementOrder"/>.
    /// </summary>
    [Obsolete("OKX renamed ELP to RPI. Use RetailPriceImprovementOrder.")]
    EnhancedLiquidityProgramOrder = RetailPriceImprovementOrder,
}
