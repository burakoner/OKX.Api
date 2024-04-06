namespace OKX.Api.GridTrading.Clients;

/// <summary>
/// OKX WebSocket Api Grid Trading Client
/// </summary>
public class OkxGridTradingSocketClient(OKXWebSocketApiClient root)
{
    // Internal
    internal OKXWebSocketApiClient Root { get; } = root;

    // TODO: WS / Spot grid algo orders channel
    // TODO: WS / Contract grid algo orders channel
    // TODO: WS / Moon grid algo orders channel
    // TODO: WS / Grid positions channel
    // TODO: WS / Grid sub orders channel

}