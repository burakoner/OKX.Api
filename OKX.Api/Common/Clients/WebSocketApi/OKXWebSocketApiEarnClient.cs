namespace OKX.Api.Common.Clients.WebSocketApi;

/// <summary>
/// OKX WebSocket Api Earn Client
/// </summary>
public class OKXWebSocketApiEarnClient
{
    // Root Client
    internal OkxSocketApiClient RootClient { get; }

    internal OKXWebSocketApiEarnClient(OkxSocketApiClient root)
    {
        RootClient = root;
    }
}