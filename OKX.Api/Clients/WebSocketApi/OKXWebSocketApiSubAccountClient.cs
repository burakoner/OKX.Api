namespace OKX.Api.Clients.WebSocketApi;

/// <summary>
/// OKX WebSocket Api SubAccount Client
/// </summary>
public class OKXWebSocketApiSubAccountClient
{
    // Root Client
    internal OKXWebSocketApiClient RootClient { get; }

    internal OKXWebSocketApiSubAccountClient(OKXWebSocketApiClient root)
    {
        this.RootClient = root;
    }
}