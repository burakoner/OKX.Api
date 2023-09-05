namespace OKX.Api.Clients.WebSocketApi;

/// <summary>
/// OKX WebSocket Api RecurringBuy Client
/// </summary>
public class OKXWebSocketApiRecurringBuyClient
{
    // Root Client
    internal OKXWebSocketApiClient RootClient { get; }

    internal OKXWebSocketApiRecurringBuyClient(OKXWebSocketApiClient root)
    {
        this.RootClient = root;
    }

    // TODO: WS / Recurring buy orders channel
}