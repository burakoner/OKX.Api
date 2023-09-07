namespace OKX.Api.Clients.WebSocketApi;

/// <summary>
/// OKX WebSocket Api Copy Trading Client
/// </summary>
public class OKXWebSocketApiCopyTradingClient
{
    // Root Client
    internal OKXWebSocketApiClient RootClient { get; }

    internal OKXWebSocketApiCopyTradingClient(OKXWebSocketApiClient root)
    {
        this.RootClient = root;
    }

}