namespace OKX.Api.CopyTrading.Clients;

/// <summary>
/// OKX WebSocket Api Copy Trading Client
/// </summary>
public class OkxCopyTradingSocketClient(OKXWebSocketApiClient root)
{
    // Internal
    internal OKXWebSocketApiClient Root { get; } = root;

    // WS / Copy trading notification channel
    // WS / Lead trading notification channel
}