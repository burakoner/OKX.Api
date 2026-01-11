namespace OKX.Api.Trade;

/// <summary>
/// OKX Order Source
/// </summary>
public enum OkxTradeOrderSource : byte
{
    /// <summary>
    /// Buy
    /// </summary>
    [Map("0")]
    NormalOrder = 0,

    /// <summary>
    /// Enhanced Liquidity Program order
    /// </summary>
    [Map("1")]
    EnhancedLiquidityProgramOrder = 1,
}