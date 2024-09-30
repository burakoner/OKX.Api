namespace OKX.Api.Trade;

/// <summary>
/// OKX Order Side
/// </summary>
public enum OkxTradeOrderSide
{
    /// <summary>
    /// Buy
    /// </summary>
    [Map("buy")]
    Buy,

    /// <summary>
    /// Sell
    /// </summary>
    [Map("sell")]
    Sell,
}