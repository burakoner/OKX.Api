namespace OKX.Api.Savings.Clients;

/// <summary>
/// OKX WebSocket Api Savings Client
/// </summary>
public class OkxSavingsSocketClient
{
    // Root Client
    internal OkxSocketApiClient RootClient { get; }

    internal OkxSavingsSocketClient(OkxSocketApiClient root)
    {
        RootClient = root;
    }
}