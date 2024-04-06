namespace OKX.Api.Funding.Clients;

/// <summary>
/// OKX WebSocket Api Funding Account Client
/// </summary>
public class OkxFundingSocketClient(OKXWebSocketApiClient root)
{
    // Internal
    internal OKXWebSocketApiClient Root { get; } = root;

    // TODO: Deposit info channel
    // TODO: Withdrawal info channel
}