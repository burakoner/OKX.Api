namespace OKX.Api;

/// <summary>
/// OKX Api Addresses
/// </summary>
public class OkxAddress
{
    /// <summary>
    /// Rest Api Address
    /// </summary>
    public string RestApiAddress { get; set; } = string.Empty;

    /// <summary>
    /// WebSocket Public Address
    /// </summary>
    public string WebSocketPublicAddress { get; set; } = string.Empty;

    /// <summary>
    /// WebSocket Private Address
    /// </summary>
    public string WebSocketPrivateAddress { get; set; } = string.Empty;

    /// <summary>
    /// WebSocket Business Address
    /// </summary>
    public string WebSocketBusinessAddress { get; set; } = string.Empty;

    /// <summary>
    /// Default Environment Endpoints
    /// </summary>
    public static OkxAddress Default = new()
    {
        RestApiAddress = "https://openapi.okx.com",
        WebSocketPublicAddress = "wss://ws.okx.com:8443/ws/v5/public",
        WebSocketPrivateAddress = "wss://ws.okx.com:8443/ws/v5/private",
        WebSocketBusinessAddress = "wss://ws.okx.com:8443/ws/v5/business",
    };

    /// <summary>
    /// Legacy AWS Environment Endpoints.
    /// OKX ceased the AWS domains; this compatibility entry now resolves to the global endpoints.
    /// </summary>
    [Obsolete("OKX AWS domains have ceased service. Use OkxAddress.Default.")]
    public static OkxAddress AWS = new()
    {
        RestApiAddress = "https://openapi.okx.com",
        WebSocketPublicAddress = "wss://ws.okx.com:8443/ws/v5/public",
        WebSocketPrivateAddress = "wss://ws.okx.com:8443/ws/v5/private",
        WebSocketBusinessAddress = "wss://ws.okx.com:8443/ws/v5/business",
    };

    /// <summary>
    /// Demo Environment Endpoints
    /// </summary>
    public static OkxAddress Demo = new()
    {
        RestApiAddress = "https://openapi.okx.com",
        WebSocketPublicAddress = "wss://wspap.okx.com:8443/ws/v5/public",
        WebSocketPrivateAddress = "wss://wspap.okx.com:8443/ws/v5/private",
        WebSocketBusinessAddress = "wss://wspap.okx.com:8443/ws/v5/business",
    };
}
