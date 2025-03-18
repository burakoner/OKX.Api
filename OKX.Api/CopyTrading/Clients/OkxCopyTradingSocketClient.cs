namespace OKX.Api.CopyTrading;

/// <summary>
/// OKX WebSocket Api Copy Trading Client
/// </summary>
public class OkxCopyTradingSocketClient(OkxWebSocketApiClient root)
{
    // Internal
    internal OkxWebSocketApiClient _ { get; } = root;

    // TODO: WS / Lead trading notification channel
}