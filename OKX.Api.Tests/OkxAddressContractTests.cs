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
    public void DemoTrading_UsesDedicatedRestDomainWithoutChangingDemoWebSockets()
    {
        var options = new OkxRestApiOptions
        {
            DemoTradingService = true,
        };

        Assert.Equal("https://openapi.okx.com", options.BaseAddress);
        Assert.Equal("wss://wspap.okx.com:8443/ws/v5/public", OkxAddress.Demo.WebSocketPublicAddress);
        Assert.Equal("wss://wspap.okx.com:8443/ws/v5/private", OkxAddress.Demo.WebSocketPrivateAddress);
        Assert.Equal("wss://wspap.okx.com:8443/ws/v5/business", OkxAddress.Demo.WebSocketBusinessAddress);
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
