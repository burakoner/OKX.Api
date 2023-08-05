namespace OKX.Api.Clients.WebSocketApi;

/// <summary>
/// OKX OrderBook Trading Grid Trading WebSocket Api Client
/// </summary>
public class OKXWebSocketGridTradingClient
{
    // Root Client
    internal OKXWebSocketApiClient RootClient { get; }

    internal OKXWebSocketGridTradingClient(OKXWebSocketApiClient root)
    {
        this.RootClient = root;
    }

    // TODO: WS / Spot grid algo orders channel
    // TODO: WS / Contract grid algo orders channel
    // TODO: WS / Moon grid algo orders channel
    // TODO: WS / Grid positions channel
    // TODO: WS / Grid sub orders channel

}