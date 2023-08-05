namespace OKX.Api.Clients.WebSocketApi;

/// <summary>
/// OKX Spread Trading WebSocket Api Client
/// </summary>
public class OKXWebSocketSpreadTradingClient
{
    // Root Client
    internal OKXWebSocketApiClient RootClient { get; }

    internal OKXWebSocketSpreadTradingClient(OKXWebSocketApiClient root)
    {
        this.RootClient = root;
    }

    // TODO: Order channel
    // TODO: Trades channel
    // TODO: Order book channel
    // TODO: Public Trades channel
    // TODO: Tickers channel

}