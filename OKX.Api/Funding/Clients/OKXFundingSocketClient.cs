namespace OKX.Api.Funding.Clients;

/// <summary>
/// OKX WebSocket Api Funding Account Client
/// </summary>
public class OkxFundingSocketClient
{
    // Root Client
    internal OkxSocketApiClient RootClient { get; }

    internal OkxFundingSocketClient(OkxSocketApiClient root)
    {
        RootClient = root;
    }

    // TODO: Deposit info channel
    // TODO: Withdrawal info channel
}