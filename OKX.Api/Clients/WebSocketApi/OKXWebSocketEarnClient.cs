namespace OKX.Api.Clients.WebSocketApi;

/// <summary>
/// OKX WebSocket Earn Client
/// </summary>
public class OKXWebSocketEarnClient
{
    // Root Client
    internal OKXWebSocketApiClient RootClient { get; }

    internal OKXWebSocketEarnClient(OKXWebSocketApiClient root)
    {
        this.RootClient = root;
    }
}