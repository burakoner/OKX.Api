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
    LimitOrder = 1,

    /// <summary>
    /// Market Order
    /// </summary>
    [Map("2")]
    MarketOrder = 2,

    /// <summary>
    /// TradingView Signal
    /// </summary>
    [Map("9")]
    TradingView = 9,
}
