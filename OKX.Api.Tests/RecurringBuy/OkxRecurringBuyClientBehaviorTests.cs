using Newtonsoft.Json.Linq;
using OKX.Api.RecurringBuy;
using OKX.Api.Tests.TestInfrastructure;

namespace OKX.Api.Tests.RecurringBuy;

public class OkxRecurringBuyClientBehaviorTests
{
    [Fact]
    public async Task AmendRecurringTimeAsync_SendsExpectedBody()
    {
        using var server = CreateServer("/api/v5/tradingBot/recurring/amend-recurring-time");
        var client = CreateClient(server);

        var result = await client.RecurringBuy.AmendRecurringTimeAsync(
            123,
            OkxRecurringBuyTimeType.Custom,
            OkxRecurringBuyPeriod.Weekly,
            "3",
            recurringDay: 2,
            recurringTime: 11);

        Assert.True(result.Success);
        var body = ParseBody(server);
        Assert.Equal("123", body["algoId"]?.Value<string>());
        Assert.Equal("1", body["recurringTimeType"]?.Value<string>());
        Assert.Equal("weekly", body["period"]?.Value<string>());
        Assert.Equal("3", body["timeZone"]?.Value<string>());
        Assert.Equal("2", body["recurringDay"]?.Value<string>());
        Assert.Equal("11", body["recurringTime"]?.Value<string>());
    }

    [Fact]
    public async Task AmendRecurringAmountAsync_SendsExpectedBody()
    {
        using var server = CreateServer("/api/v5/tradingBot/recurring/amend-recurring-amount");
        var client = CreateClient(server);

        var result = await client.RecurringBuy.AmendRecurringAmountAsync(123, 20m);

        Assert.True(result.Success);
        var body = ParseBody(server);
        Assert.Equal("123", body["algoId"]?.Value<string>());
        Assert.Equal("20", body["amount"]?.Value<string>());
    }

    [Fact]
    public async Task AddInvestmentAsync_SendsExpectedBody()
    {
        using var server = CreateServer("/api/v5/tradingBot/recurring/add-investment");
        var client = CreateClient(server);

        var result = await client.RecurringBuy.AddInvestmentAsync(123, 30m);

        Assert.True(result.Success);
        var body = ParseBody(server);
        Assert.Equal("123", body["algoId"]?.Value<string>());
        Assert.Equal("30", body["amount"]?.Value<string>());
    }

    [Fact]
    public async Task PauseAndRestartAsync_SendExpectedBody()
    {
        using var pauseServer = CreateServer("/api/v5/tradingBot/recurring/pause");
        using var restartServer = CreateServer("/api/v5/tradingBot/recurring/restart");
        var pauseClient = CreateClient(pauseServer);
        var restartClient = CreateClient(restartServer);

        var pauseResult = await pauseClient.RecurringBuy.PauseAsync(123);
        var restartResult = await restartClient.RecurringBuy.RestartAsync(456);

        Assert.True(pauseResult.Success);
        Assert.True(restartResult.Success);
        Assert.Equal("123", ParseBody(pauseServer)["algoId"]?.Value<string>());
        Assert.Equal("456", ParseBody(restartServer)["algoId"]?.Value<string>());
    }

    [Fact]
    public async Task AmendPriceRangeAsync_SendsBlankBounds()
    {
        using var server = CreateServer("/api/v5/tradingBot/recurring/amend-price-range");
        var client = CreateClient(server);

        var result = await client.RecurringBuy.AmendPriceRangeAsync(
            789,
            [
                new OkxRecurringBuyPriceRange
                {
                    Currency = "BTC",
                    MinimumPrice = "30000",
                    MaximumPrice = "60000"
                },
                new OkxRecurringBuyPriceRange
                {
                    Currency = "ETH",
                    MinimumPrice = "",
                    MaximumPrice = ""
                }
            ]);

        Assert.True(result.Success);
        var body = ParseBody(server);
        Assert.Equal("789", body["algoId"]?.Value<string>());
        var recurringList = Assert.IsAssignableFrom<JArray>(body["recurringList"]).Values<JObject>().ToList();
        Assert.Equal(2, recurringList.Count);
        Assert.Equal("", recurringList[1]?["minPx"]?.Value<string>());
        Assert.Equal("", recurringList[1]?["maxPx"]?.Value<string>());
    }

    private static JObject ParseBody(LocalOkxRestServer server)
        => JObject.Parse(Assert.Single(server.Requests).Body);

    private static LocalOkxRestServer CreateServer(string path)
        => new(new Dictionary<string, string>
        {
            [$"POST {path}"] = "{\"code\":\"0\",\"msg\":\"\",\"data\":[{\"algoId\":\"123\",\"sCode\":\"0\",\"sMsg\":\"\"}]}",
        });

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
