namespace OKX.Api.Clients.WebSocketApi;

/// <summary>
/// OKX WebSocket Api Savings Client
/// </summary>
public class OKXWebSocketApiSavingsClient
{
    // Root Client
    internal OkxSocketApiClient RootClient { get; }

    internal OKXWebSocketApiSavingsClient(OkxSocketApiClient root)
    {
        this.RootClient = root;
    }
}