namespace OKX.Api.Clients.WebSocketApi;

/// <summary>
/// OKX WebSocket Api Trading Statistics Client
/// </summary>
public class OKXWebSocketApiTradingStatisticsClient
{
    // Root Client
    internal OkxSocketApiClient RootClient { get; }

    internal OKXWebSocketApiTradingStatisticsClient(OkxSocketApiClient root)
    {
        this.RootClient = root;
    }

}