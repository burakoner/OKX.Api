namespace OKX.Api.Clients.WebSocketApi;

/// <summary>
/// OKX WebSocket Api Copy Trading Client
/// </summary>
public class OKXWebSocketApiCopyTradingClient
{
    // Root Client
    internal OkxSocketApiClient RootClient { get; }

    internal OKXWebSocketApiCopyTradingClient(OkxSocketApiClient root)
    {
        this.RootClient = root;
    }

}