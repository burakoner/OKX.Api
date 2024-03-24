namespace OKX.Api.Common.Clients.WebSocketApi;

/// <summary>
/// OKX WebSocket Api Savings Client
/// </summary>
public class OKXWebSocketApiSavingsClient
{
    // Root Client
    internal OkxSocketApiClient RootClient { get; }

    internal OKXWebSocketApiSavingsClient(OkxSocketApiClient root)
    {
        RootClient = root;
    }
}