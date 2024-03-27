namespace OKX.Api.GridTrading.Clients;

/// <summary>
/// OKX WebSocket Api Grid Trading Client
/// </summary>
public class OkxGridTradingSocketClient
{
    // Root Client
    internal OKXWebSocketApiClient RootClient { get; }

    internal OkxGridTradingSocketClient(OKXWebSocketApiClient root)
    {
        RootClient = root;
    }

    // TODO: WS / Spot grid algo orders channel
    // TODO: WS / Contract grid algo orders channel
    // TODO: WS / Moon grid algo orders channel
    // TODO: WS / Grid positions channel
    // TODO: WS / Grid sub orders channel

}