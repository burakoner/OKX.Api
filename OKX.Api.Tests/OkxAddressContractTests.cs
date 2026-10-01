namespace OKX.Api.Tests;

public class OkxAddressContractTests
{
    [Fact]
    public void RestOptions_DefaultToDedicatedGlobalDomain()
    {
        var options = new OkxRestApiOptions();

        Assert.Equal("https://openapi.okx.com", options.BaseAddress);
        Assert.Equal("https://openapi.okx.com", OkxAddress.Default.RestApiAddress);
    }

    [Fact]
    public void WebSocketOptions_DefaultToPort443OnAllGlobalPaths()
    {
        var options = new OkxWebSocketApiOptions();

        Assert.Equal("wss://ws.okx.com/ws/v5/public", options.BaseAddress);
        Assert.Equal(options.BaseAddress, OkxAddress.Default.WebSocketPublicAddress);
        Assert.Equal("wss://ws.okx.com/ws/v5/private", OkxAddress.Default.WebSocketPrivateAddress);
        Assert.Equal("wss://ws.okx.com/ws/v5/business", OkxAddress.Default.WebSocketBusinessAddress);
        Assert.Equal(443, new Uri(OkxAddress.Default.WebSocketPublicAddress).Port);
        Assert.Equal(443, new Uri(OkxAddress.Default.WebSocketPrivateAddress).Port);
        Assert.Equal(443, new Uri(OkxAddress.Default.WebSocketBusinessAddress).Port);
    }

    [Fact]
    public void DemoTrading_UsesDedicatedRestDomainAndPort443WebSockets()
    {
        var options = new OkxRestApiOptions
        {
            DemoTradingService = true,
        };

        Assert.Equal("https://openapi.okx.com", options.BaseAddress);
        Assert.Equal("wss://wspap.okx.com/ws/v5/public", OkxAddress.Demo.WebSocketPublicAddress);
        Assert.Equal("wss://wspap.okx.com/ws/v5/private", OkxAddress.Demo.WebSocketPrivateAddress);
        Assert.Equal("wss://wspap.okx.com/ws/v5/business", OkxAddress.Demo.WebSocketBusinessAddress);
        Assert.Equal(443, new Uri(OkxAddress.Demo.WebSocketPublicAddress).Port);
        Assert.Equal(443, new Uri(OkxAddress.Demo.WebSocketPrivateAddress).Port);
        Assert.Equal(443, new Uri(OkxAddress.Demo.WebSocketBusinessAddress).Port);
    }

    [Fact]
    public void LegacyAwsAddress_ResolvesToActiveGlobalEndpoints()
    {
#pragma warning disable CS0618 // Compatibility entry is intentionally verified.
        var address = OkxAddress.AWS;
#pragma warning restore CS0618

        Assert.Equal(OkxAddress.Default.RestApiAddress, address.RestApiAddress);
        Assert.Equal(OkxAddress.Default.WebSocketPublicAddress, address.WebSocketPublicAddress);
        Assert.Equal(OkxAddress.Default.WebSocketPrivateAddress, address.WebSocketPrivateAddress);
        Assert.Equal(OkxAddress.Default.WebSocketBusinessAddress, address.WebSocketBusinessAddress);
    }
}
