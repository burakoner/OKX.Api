namespace OKX.Api.Clients.WebSocketApi;

/// <summary>
/// OKX WebSocket Api Grid Trading Client
/// </summary>
public class OKXWebSocketApiGridTradingClient
{
    // Root Client
    internal OkxSocketApiClient RootClient { get; }

    internal OKXWebSocketApiGridTradingClient(OkxSocketApiClient root)
    {
        this.RootClient = root;
    }

    // TODO: WS / Spot grid algo orders channel
    // TODO: WS / Contract grid algo orders channel
    // TODO: WS / Moon grid algo orders channel
    // TODO: WS / Grid positions channel
    // TODO: WS / Grid sub orders channel

}