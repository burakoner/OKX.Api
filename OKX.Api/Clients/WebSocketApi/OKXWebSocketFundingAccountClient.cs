namespace OKX.Api.Clients.WebSocketApi;

/// <summary>
/// OKX Funding Account WebSocket Api Client
/// </summary>
public class OKXWebSocketFundingAccountClient
{
    // Root Client
    internal OKXWebSocketApiClient RootClient { get; }

    internal OKXWebSocketFundingAccountClient(OKXWebSocketApiClient root)
    {
        this.RootClient = root;
    }

    // TODO: Deposit info channel

}