namespace OKX.Api.SignalBot;

/// <summary>
/// Signal order type
/// </summary>
public enum OkxSignalBotOrderType
{
    /// <summary>
    /// Limit Order
    /// </summary>
    [Map("1")]
    LimitOrder,

    /// <summary>
    /// Market Order
    /// </summary>
    [Map("2")]
    MarketOrder,

    /// <summary>
    /// TradingView Signal
    /// </summary>
    [Map("9")]
    TradingViewSignal,
}
