namespace OKX.Api.Spread;

/// <summary>
/// OKX WebSocket Api Spread Trading Client
/// </summary>
public class OkxSpreadSocketClient(OkxWebSocketApiClient root)
{
    // Internal
    internal OkxWebSocketApiClient _ { get; } = root;

    // TODO: WS / Place order
    // TODO: WS / Amend order
    // TODO: WS / Cancel order
    // TODO: WS / Cancel all orders

    // TODO: Order channel
    // TODO: Trades channel

    // TODO: Order book channel
    // TODO: Public Trades channel
    // TODO: Tickers channel
    // TODO: Candlesticks channel

}