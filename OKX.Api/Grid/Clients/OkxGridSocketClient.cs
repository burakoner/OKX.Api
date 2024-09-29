namespace OKX.Api.Grid;

/// <summary>
/// OKX WebSocket Api Grid Trading Client
/// </summary>
public class OkxGridSocketClient(OKXWebSocketApiClient root)
{
    // Internal
    internal OKXWebSocketApiClient Root { get; } = root;

    // TODO: WS / Spot grid algo orders channel
    // TODO: WS / Contract grid algo orders channel
    // TODO: WS / Moon grid algo orders channel
    // TODO: WS / Grid positions channel
    // TODO: WS / Grid sub orders channel

}