using OKX.Api.SignalBot;
using OKX.Api.Tests.TestInfrastructure;

namespace OKX.Api.Tests.SignalBot;

public class OkxSignalBotClientBehaviorTests
{
    [Fact]
    public async Task GetSignalBotOrderAsync_UsesQueryParameters()
    {
        using var server = new LocalOkxRestServer(new Dictionary<string, string>
        {
            ["GET /api/v5/tradingBot/signal/orders-algo-details"] =
                "{\"code\":\"0\",\"msg\":\"\",\"data\":[{\"algoId\":\"123\",\"instType\":\"SWAP\",\"instIds\":[\"BTC-USDT-SWAP\"],\"cTime\":\"1714200000000\",\"uTime\":\"1714203600000\",\"algoOrdType\":\"contract\",\"state\":\"running\",\"cancelType\":\"\",\"subOrdType\":\"1\",\"signalSourceType\":\"1\"}]}",
        });
        var client = CreateClient(server);

        var result = await client.SignalBot.GetSignalBotOrderAsync(123);

        Assert.True(result.Success);
        var request = Assert.Single(server.Requests);
        Assert.Equal("/api/v5/tradingBot/signal/orders-algo-details", request.Path);
        Assert.Contains("algoId=123", request.Query);
        Assert.Contains("algoOrdType=contract", request.Query);
        Assert.Equal(string.Empty, request.Body);
    }

    private static OkxRestApiClient CreateClient(LocalOkxRestServer server)
    {
        var options = new OkxRestApiOptions(new OkxApiCredentials("key", "secret", "pass"))
        {
            AutoTimestamp = false,
            BaseAddress = server.BaseAddress,
        };

        return new OkxRestApiClient(options);
    }
}
