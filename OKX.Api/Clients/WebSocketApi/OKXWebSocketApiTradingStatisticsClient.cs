namespace OKX.Api.Clients.WebSocketApi;

/// <summary>
/// OKX WebSocket Api Trading Statistics Client
/// </summary>
public class OKXWebSocketApiTradingStatisticsClient
{
    // Root Client
    internal OKXWebSocketApiClient RootClient { get; }

    internal OKXWebSocketApiTradingStatisticsClient(OKXWebSocketApiClient root)
    {
        this.RootClient = root;
    }

}