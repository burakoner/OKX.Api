namespace OKX.Api.CopyTrading.Clients;

/// <summary>
/// OKX WebSocket Api Copy Trading Client
/// </summary>
public class OkxCopyTradingSocketClient
{
    // Root Client
    internal OKXWebSocketApiClient RootClient { get; }

    internal OkxCopyTradingSocketClient(OKXWebSocketApiClient root)
    {
        RootClient = root;
    }

    // WS / Copy trading notification channel
    // WS / Lead trading notification channel
}