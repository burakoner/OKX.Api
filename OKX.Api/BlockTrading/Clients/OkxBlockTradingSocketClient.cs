namespace OKX.Api.BlockTrading.Clients;

/// <summary>
/// OKX WebSocket Api Block Trading Client
/// </summary>
public class OkxBlockTradingSocketClient
{
    // Root Client
    internal OKXWebSocketApiClient RootClient { get; }

    internal OkxBlockTradingSocketClient(OKXWebSocketApiClient root)
    {
        RootClient = root;
    }

    // TODO: Rfqs channel
    // TODO: Quotes channel
    // TODO: Structure block trades channel
    // TODO: Public structure block trades channel
    // TODO: Public block trades channel
    // TODO: Block tickers channel

}