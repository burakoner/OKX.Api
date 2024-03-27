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
    /// Broker ID
    /// </summary>
    internal readonly string BrokerId = "538a3965e538BCDE";

    /// <summary>
    /// Constructor
    /// </summary>
    public OkxWebSocketApiOptions()
    {
        this.BaseAddress = OkxAddress.Default.WebSocketPublicAddress;
    }
}
