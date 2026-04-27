using OKX.Api.Common;
using OKX.Api.Spread;
using OKX.Api.Tests.TestInfrastructure;
using OKX.Api.Trade;
using OKX.Api;

namespace OKX.Api.Tests.Spread;

public class OkxSpreadClientBehaviorTests
{
    [Fact]
    public async Task CancelOrdersAsync_AllowsOmittingSpreadId()
    {
        using var server = new LocalOkxRestServer(new Dictionary<string, string>
        {
            ["POST /api/v5/sprd/mass-cancel"] = """
            {
              "code": "0",
              "msg": "",
              "data": [
                {
                  "result": true
                }
              ]
            }
            """
        });

        var client = CreatePrivateClient(server);
        var result = await client.Spread.CancelOrdersAsync();

        Assert.True(result.Success);
        var request = Assert.Single(server.Requests);
        Assert.Equal("POST", request.Method);
        Assert.DoesNotContain("sprdId", request.Body, StringComparison.Ordinal);
    }

    [Fact]
    public async Task GetOrderArchiveAsync_SendsInstTypeAndInstFamilyFilters()
    {
        using var server = new LocalOkxRestServer(new Dictionary<string, string>
        {
            ["GET /api/v5/sprd/orders-history-archive"] = """
            {
              "code": "0",
              "msg": "",
              "data": []
            }
            """
        });

        var client = CreatePrivateClient(server);
        var result = await client.Spread.GetOrderArchiveAsync(
            spreadId: "BTC-USDT_BTC-USDT-SWAP",
            limit: 5,
            instrumentType: OkxInstrumentType.Swap,
            instrumentFamily: "BTC-USDT");

        Assert.True(result.Success);
        var request = Assert.Single(server.Requests);
        Assert.Contains("sprdId=BTC-USDT_BTC-USDT-SWAP", request.Query);
        Assert.Contains("instType=SWAP", request.Query);
        Assert.Contains("instFamily=BTC-USDT", request.Query);
    }

    [Fact]
    public async Task GetOpenOrdersAsync_RejectsUnsupportedOrderStates()
    {
        var client = new OkxRestApiClient(new OkxRestApiOptions());

        var ex = await Assert.ThrowsAsync<ArgumentException>(() => client.Spread.GetOpenOrdersAsync(state: OkxTradeOrderState.MmpCanceled));
        Assert.Contains("Spread open-orders", ex.Message, StringComparison.Ordinal);
    }

    [Fact]
    public async Task GetOrderHistoryAsync_RejectsUnsupportedOrderStates()
    {
        var client = new OkxRestApiClient(new OkxRestApiOptions());

        var ex = await Assert.ThrowsAsync<ArgumentException>(() => client.Spread.GetOrderHistoryAsync(state: OkxTradeOrderState.Live));
        Assert.Contains("Spread order-history", ex.Message, StringComparison.Ordinal);
    }

    private static OkxRestApiClient CreatePrivateClient(LocalOkxRestServer server)
    {
        var options = new OkxRestApiOptions(new OkxApiCredentials("key", "secret", "pass"))
        {
            AutoTimestamp = false,
            BaseAddress = server.BaseAddress
        };

        return new OkxRestApiClient(options);
    }
}
