namespace OKX.Api.Funding.Clients;

/// <summary>
/// OKX WebSocket Api Funding Account Client
/// </summary>
public class OKXFundingSocketClient
{
    // Root Client
    internal OKXWebSocketApiClient RootClient { get; }

    internal OKXFundingSocketClient(OKXWebSocketApiClient root)
    {
        RootClient = root;
    }

    // TODO: Deposit info channel
    // TODO: Withdrawal info channel
}