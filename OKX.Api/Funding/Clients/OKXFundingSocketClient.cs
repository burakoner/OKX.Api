namespace OKX.Api.Funding;

/// <summary>
/// OKX WebSocket Api Funding Account Client
/// </summary>
public class OkxFundingSocketClient(OkxWebSocketApiClient root)
{
    // Internal
    internal OkxWebSocketApiClient _ { get; } = root;

    // TODO: Deposit info channel
    // TODO: Withdrawal info channel
}