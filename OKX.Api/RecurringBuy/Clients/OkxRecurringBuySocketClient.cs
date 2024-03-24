namespace OKX.Api.RecurringBuy.Clients;

/// <summary>
/// OKX WebSocket Api RecurringBuy Client
/// </summary>
public class OkxRecurringBuySocketClient
{
    // Root Client
    internal OkxSocketApiClient RootClient { get; }

    internal OkxRecurringBuySocketClient(OkxSocketApiClient root)
    {
        RootClient = root;
    }

    // TODO: WS / Recurring buy orders channel
}