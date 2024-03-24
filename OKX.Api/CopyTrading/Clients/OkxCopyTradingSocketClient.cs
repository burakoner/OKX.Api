namespace OKX.Api.CopyTrading.Clients;

/// <summary>
/// OKX WebSocket Api Copy Trading Client
/// </summary>
public class OkxCopyTradingSocketClient
{
    // Root Client
    internal OkxSocketApiClient RootClient { get; }

    internal OkxCopyTradingSocketClient(OkxSocketApiClient root)
    {
        RootClient = root;
    }

}