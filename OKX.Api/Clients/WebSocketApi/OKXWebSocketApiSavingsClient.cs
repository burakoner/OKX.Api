namespace OKX.Api.Clients.WebSocketApi;

/// <summary>
/// OKX WebSocket Api Savings Client
/// </summary>
public class OKXWebSocketApiSavingsClient
{
    // Root Client
    internal OKXWebSocketApiClient RootClient { get; }

    internal OKXWebSocketApiSavingsClient(OKXWebSocketApiClient root)
    {
        this.RootClient = root;
    }
}