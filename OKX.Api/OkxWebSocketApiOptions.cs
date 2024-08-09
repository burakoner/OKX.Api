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
    public string BrokerId { get; set; } = OkxConstants.BrokerId;

    /// <summary>
    /// Constructor
    /// </summary>
    public OkxWebSocketApiOptions()
    {
        this.BaseAddress = OkxAddress.Default.WebSocketPublicAddress;
    }
}
