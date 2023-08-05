namespace OKX.Api.Clients.WebSocketApi;

/// <summary>
/// OKX OrderBook Trading Recurring Buy WebSocket Api Client
/// </summary>
public class OKXWebSocketRecurringBuyClient
{
    // Root Client
    internal OKXWebSocketApiClient RootClient { get; }

    internal OKXWebSocketRecurringBuyClient(OKXWebSocketApiClient root)
    {
        this.RootClient = root;
    }

    // TODO: WS / Recurring buy orders channel
}