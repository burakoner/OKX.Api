namespace OKX.Api;

public class OKXApiAddresses
{
    public string RestApiAddress { get; set; }
    public string WebSocketPublicAddress { get; set; }
    public string WebSocketPrivateAddress { get; set; }

    public static OKXApiAddresses Default = new()
    {
        RestApiAddress = "https://www.okx.com",
        WebSocketPublicAddress = "wss://ws.okx.com:8443/ws/v5/public",
        WebSocketPrivateAddress = "wss://ws.okx.com:8443/ws/v5/private",
    };

    public static OKXApiAddresses AWS = new()
    {
        RestApiAddress = "https://aws.okx.com",
        WebSocketPublicAddress = "wss://wsaws.okx.com:8443/ws/v5/public",
        WebSocketPrivateAddress = "wss://wsaws.okx.com:8443/ws/v5/private",
    };

    public static OKXApiAddresses Demo = new()
    {
        RestApiAddress = "https://www.okx.com",
        WebSocketPublicAddress = "wss://wspap.okx.com:8443/ws/v5/public?brokerId=9999",
        WebSocketPrivateAddress = "wss://wspap.okx.com:8443/ws/v5/private?brokerId=9999",
    };
}
