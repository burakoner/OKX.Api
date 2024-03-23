namespace OKX.Api.Clients.WebSocketApi;

/// <summary>
/// OKX WebSocket Api RecurringBuy Client
/// </summary>
public class OKXWebSocketApiRecurringBuyClient
{
    // Root Client
    internal OkxSocketApiClient RootClient { get; }

    internal OKXWebSocketApiRecurringBuyClient(OkxSocketApiClient root)
    {
        this.RootClient = root;
    }

    // TODO: WS / Recurring buy orders channel
}