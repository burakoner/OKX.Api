namespace OKX.Api.Funding.Clients;

/// <summary>
/// OKX WebSocket Api Funding Account Client
/// </summary>
public class OkxFundingSocketClient
{
    // Root Client
    internal OKXWebSocketApiClient RootClient { get; }

    internal OkxFundingSocketClient(OKXWebSocketApiClient root)
    {
        RootClient = root;
    }

    // TODO: Deposit info channel
    // TODO: Withdrawal info channel
}