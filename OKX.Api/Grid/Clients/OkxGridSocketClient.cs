namespace OKX.Api.Grid;

/// <summary>
/// OKX WebSocket Api Grid Trading Client
/// </summary>
public class OkxGridSocketClient(OkxWebSocketApiClient root)
{
    // Internal
    internal OkxWebSocketApiClient _ { get; } = root;

    // TODO: WS / Spot grid algo orders channel
    // TODO: WS / Contract grid algo orders channel
    // TODO: WS / Grid positions channel
    // TODO: WS / Grid sub orders channel

}