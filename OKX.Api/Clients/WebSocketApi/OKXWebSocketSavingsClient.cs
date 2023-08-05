namespace OKX.Api.Clients.WebSocketApi;

/// <summary>
/// OKX WebSocket Savings Client
/// </summary>
public class OKXWebSocketSavingsClient
{
    // Root Client
    internal OKXWebSocketApiClient RootClient { get; }

    internal OKXWebSocketSavingsClient(OKXWebSocketApiClient root)
    {
        this.RootClient = root;
    }
}