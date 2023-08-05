namespace OKX.Api.Clients.WebSocketApi;

/// <summary>
/// OKX Sub-Account WebSocket Api Client
/// </summary>
public class OKXWebSocketSubAccountClient
{
    // Root Client
    internal OKXWebSocketApiClient RootClient { get; }

    internal OKXWebSocketSubAccountClient(OKXWebSocketApiClient root)
    {
        this.RootClient = root;
    }
}