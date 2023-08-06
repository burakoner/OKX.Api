namespace OKX.Api.Clients.WebSocketApi;

/// <summary>
/// OKX WebSocket Api Earn Client
/// </summary>
public class OKXWebSocketApiEarnClient
{
    // Root Client
    internal OKXWebSocketApiClient RootClient { get; }

    internal OKXWebSocketApiEarnClient(OKXWebSocketApiClient root)
    {
        this.RootClient = root;
    }
}