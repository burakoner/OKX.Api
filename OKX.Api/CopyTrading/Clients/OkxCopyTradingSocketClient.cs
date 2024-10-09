namespace OKX.Api.CopyTrading;

/// <summary>
/// OKX WebSocket Api Copy Trading Client
/// </summary>
public class OkxCopyTradingSocketClient(OKXWebSocketApiClient root)
{
    // Internal
    internal OKXWebSocketApiClient _ { get; } = root;

    // WS / Copy trading notification channel
    // WS / Lead trading notification channel
}