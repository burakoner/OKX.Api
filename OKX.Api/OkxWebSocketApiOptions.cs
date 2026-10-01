namespace OKX.Api;

/// <summary>
/// OKX WebSocket Options
/// </summary>
public class OkxWebSocketApiOptions : WebSocketApiClientOptions
{
    /// <summary>
    /// Optional fail-fast Place/Amend guard. Share one instance with all REST/WS clients of the same User ID/environment.
    /// Register current instruments and account-specific lead status explicitly. Null preserves legacy throttling.
    /// </summary>
    public OkxTradeRateLimiter? TradeRateLimiter { get; set; }

    /// <summary>
    /// Use Demo Trading Service
    /// </summary>
    public bool DemoTradingService { get; set; } = false;
    
    /// <summary>
    /// Constructor
    /// </summary>
    public OkxWebSocketApiOptions()
    {
        this.BaseAddress = OkxAddress.Default.WebSocketPublicAddress;
    }
}
