namespace OKX.Api.Clients.WebSocketApi;

/// <summary>
/// OKX Financial Product Base WebSocket Api Client
/// </summary>
public class OKXWebSocketFinancialProductClient
{
    /// <summary>
    /// Earn Client
    /// </summary>
    public OKXWebSocketEarnClient Earn { get; }

    /// <summary>
    /// Savings Client
    /// </summary>
    public OKXWebSocketSavingsClient Savings { get; }

    // Root Client
    internal OKXWebSocketApiClient RootClient { get; }

    internal OKXWebSocketFinancialProductClient(OKXWebSocketApiClient root)
    {
        this.RootClient = root;

        Earn = new OKXWebSocketEarnClient(root);
        Savings = new OKXWebSocketSavingsClient(root);
    }
}