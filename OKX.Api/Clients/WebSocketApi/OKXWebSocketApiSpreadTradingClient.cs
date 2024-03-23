namespace OKX.Api.Clients.WebSocketApi;

/// <summary>
/// OKX WebSocket Api Spread Trading Client
/// </summary>
public class OKXWebSocketApiSpreadTradingClient
{
    // Root Client
    internal OkxSocketApiClient RootClient { get; }

    internal OKXWebSocketApiSpreadTradingClient(OkxSocketApiClient root)
    {
        this.RootClient = root;
    }

    // TODO: Order channel
    // TODO: Trades channel
    // TODO: Order book channel
    // TODO: Public Trades channel
    // TODO: Tickers channel

}