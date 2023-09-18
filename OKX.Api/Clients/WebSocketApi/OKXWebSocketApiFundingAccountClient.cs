namespace OKX.Api.Clients.WebSocketApi;

/// <summary>
/// OKX WebSocket Api Funding Account Client
/// </summary>
public class OKXWebSocketApiFundingAccountClient
{
    // Root Client
    internal OKXWebSocketApiClient RootClient { get; }

    internal OKXWebSocketApiFundingAccountClient(OKXWebSocketApiClient root)
    {
        this.RootClient = root;
    }

    // TODO: Deposit info channel
    // TODO: Withdrawal info channel
}