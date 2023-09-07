namespace OKX.Api.Clients.WebSocketApi;

/// <summary>
/// OKX WebSocket Api Financial Product Client
/// </summary>
public class OKXWebSocketApiFinancialProductClient
{
    /// <summary>
    /// Earn Client
    /// </summary>
    public OKXWebSocketApiEarnClient Earn { get; }

    /// <summary>
    /// Savings Client
    /// </summary>
    public OKXWebSocketApiSavingsClient Savings { get; }

    // Root Client
    internal OKXWebSocketApiClient RootClient { get; }

    internal OKXWebSocketApiFinancialProductClient(OKXWebSocketApiClient root)
    {
        this.RootClient = root;

        Earn = new OKXWebSocketApiEarnClient(root);
        Savings = new OKXWebSocketApiSavingsClient(root);
    }
}