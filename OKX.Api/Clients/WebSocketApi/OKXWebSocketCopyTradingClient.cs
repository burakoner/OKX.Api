namespace OKX.Api.Clients.WebSocketApi;

/// <summary>
/// OKX OrderBook Trading Copy Trading WebSocket Api Client
/// </summary>
public class OKXWebSocketCopyTradingClient
{
    // Root Client
    internal OKXWebSocketApiClient RootClient { get; }

    internal OKXWebSocketCopyTradingClient(OKXWebSocketApiClient root)
    {
        this.RootClient = root;
    }

}