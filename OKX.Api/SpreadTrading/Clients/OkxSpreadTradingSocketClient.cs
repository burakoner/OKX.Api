namespace OKX.Api.SpreadTrading.Clients;

/// <summary>
/// OKX WebSocket Api Spread Trading Client
/// </summary>
public class OkxSpreadTradingSocketClient
{
    // Root Client
    internal OkxSocketApiClient RootClient { get; }

    internal OkxSpreadTradingSocketClient(OkxSocketApiClient root)
    {
        RootClient = root;
    }

    // TODO: Order channel
    // TODO: Trades channel
    // TODO: Order book channel
    // TODO: Public Trades channel
    // TODO: Tickers channel

}