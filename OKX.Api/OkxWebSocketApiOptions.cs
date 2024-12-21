namespace OKX.Api;

/// <summary>
/// OKX WebSocket Options
/// </summary>
public class OkxWebSocketApiOptions : WebSocketApiClientOptions
{
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
