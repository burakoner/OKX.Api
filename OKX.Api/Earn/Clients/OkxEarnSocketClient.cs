namespace OKX.Api.Earn.Clients;

/// <summary>
/// OKX WebSocket Api Earn Client
/// </summary>
public class OkxEarnSocketClient
{
    // Root Client
    internal OkxSocketApiClient RootClient { get; }

    internal OkxEarnSocketClient(OkxSocketApiClient root)
    {
        RootClient = root;
    }
}