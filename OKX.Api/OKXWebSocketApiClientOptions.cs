namespace OKX.Api;

/// <summary>
/// OKX WebSocket Client Options
/// </summary>
public class OKXWebSocketApiClientOptions : WebSocketApiClientOptions
{
    /// <summary>
    /// Use Demo Trading Service
    /// </summary>
    public bool DemoTradingService { get; set; } = false;

    /// <summary>
    /// OKXWebSocketClientOptions Constructor
    /// </summary>
    public OKXWebSocketApiClientOptions()
    {
        this.BaseAddress = OKXApiAddresses.Default.WebSocketPublicAddress;
    }
}
