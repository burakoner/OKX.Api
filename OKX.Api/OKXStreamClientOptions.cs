namespace OKX.Api;

public class OKXStreamClientOptions : StreamApiClientOptions
{
    // Demo
    public bool DemoTradingService { get; set; } = false;

    public OKXStreamClientOptions()
    {
        this.BaseAddress = OKXApiAddresses.Default.WebSocketPublicAddress;
    }
}
