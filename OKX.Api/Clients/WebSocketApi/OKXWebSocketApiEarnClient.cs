namespace OKX.Api.Clients.WebSocketApi;

/// <summary>
/// OKX WebSocket Api Earn Client
/// </summary>
public class OKXWebSocketApiEarnClient
{
    // Root Client
    internal OkxSocketApiClient RootClient { get; }

    internal OKXWebSocketApiEarnClient(OkxSocketApiClient root)
    {
        this.RootClient = root;
    }
}