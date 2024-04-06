namespace OKX.Api.SpreadTrading.Clients;

/// <summary>
/// OKX WebSocket Api Spread Trading Client
/// </summary>
public class OkxSpreadTradingSocketClient(OKXWebSocketApiClient root)
{
    // Internal
    internal OKXWebSocketApiClient Root { get; } = root;

    // TODO: Order channel
    // TODO: Trades channel
    // TODO: Order book channel
    // TODO: Public Trades channel
    // TODO: Tickers channel

}