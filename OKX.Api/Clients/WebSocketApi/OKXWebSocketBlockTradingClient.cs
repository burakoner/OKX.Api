namespace OKX.Api.Clients.WebSocketApi;

/// <summary>
/// OKX Block Trading WebSocket Api Client
/// </summary>
public class OKXWebSocketBlockTradingClient
{
    // Root Client
    internal OKXWebSocketApiClient RootClient { get; }

    internal OKXWebSocketBlockTradingClient(OKXWebSocketApiClient root)
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