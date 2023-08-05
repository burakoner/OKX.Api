namespace OKX.Api.Clients.WebSocketApi;

/// <summary>
/// OKX Trading Statistics WebSocket Api Client
/// </summary>
public class OKXWebSocketTradingStatisticsClient
{
    // Root Client
    internal OKXWebSocketApiClient RootClient { get; }

    internal OKXWebSocketTradingStatisticsClient(OKXWebSocketApiClient root)
    {
        this.RootClient = root;
    }

}