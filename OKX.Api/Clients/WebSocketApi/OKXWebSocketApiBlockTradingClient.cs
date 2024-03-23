namespace OKX.Api.Clients.WebSocketApi;

/// <summary>
/// OKX WebSocket Api Block Trading Client
/// </summary>
public class OKXWebSocketApiBlockTradingClient
{
    // Root Client
    internal OkxSocketApiClient RootClient { get; }

    internal OKXWebSocketApiBlockTradingClient(OkxSocketApiClient root)
    {
        this.RootClient = root;
    }

    // TODO: Rfqs channel
    // TODO: Quotes channel
    // TODO: Structure block trades channel
    // TODO: Public structure block trades channel
    // TODO: Public block trades channel
    // TODO: Block tickers channel

}