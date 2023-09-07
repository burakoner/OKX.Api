﻿namespace OKX.Api;

/// <summary>
/// OKX Api Addresses
/// </summary>
public class OKXApiAddresses
{
    /// <summary>
    /// Rest Api Address
    /// </summary>
    public string RestApiAddress { get; set; }

    /// <summary>
    /// WebSocket Public Address
    /// </summary>
    public string WebSocketPublicAddress { get; set; }

    /// <summary>
    /// WebSocket Private Address
    /// </summary>
    public string WebSocketPrivateAddress { get; set; }

    /// <summary>
    /// Default Environment Endpoints
    /// </summary>
    public static OKXApiAddresses Default = new()
    {
        RestApiAddress = "https://www.okx.com",
        WebSocketPublicAddress = "wss://ws.okx.com:8443/ws/v5/public",
        WebSocketPrivateAddress = "wss://ws.okx.com:8443/ws/v5/private",
    };

    /// <summary>
    /// AWS Environment Endpoints
    /// </summary>
    public static OKXApiAddresses AWS = new()
    {
        RestApiAddress = "https://aws.okx.com",
        WebSocketPublicAddress = "wss://wsaws.okx.com:8443/ws/v5/public",
        WebSocketPrivateAddress = "wss://wsaws.okx.com:8443/ws/v5/private",
    };

    /// <summary>
    /// Demo Environment Endpoints
    /// </summary>
    public static OKXApiAddresses Demo = new()
    {
        RestApiAddress = "https://www.okx.com",
        WebSocketPublicAddress = "wss://wspap.okx.com:8443/ws/v5/public?brokerId=9999",
        WebSocketPrivateAddress = "wss://wspap.okx.com:8443/ws/v5/private?brokerId=9999",
    };
}
