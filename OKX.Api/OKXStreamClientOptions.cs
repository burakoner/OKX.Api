namespace OKX.Api;

/// <summary>
/// OKX WebSocket Client Options
/// </summary>
public class OKXStreamClientOptions : StreamApiClientOptions
{
    /// <summary>
    /// Use Demo Trading Service
    /// </summary>
    public bool DemoTradingService { get; set; } = false;

    /// <summary>
    /// OKXStreamClientOptions Constructor
    /// </summary>
    public OKXStreamClientOptions()
    {
        this.BaseAddress = OKXApiAddresses.Default.WebSocketPublicAddress;
    }
}
