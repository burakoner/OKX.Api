using ApiSharp.Throttling;
using Newtonsoft.Json.Linq;
using OKX.Api.Common;
using OKX.Api.Financial;
using OKX.Api.Tests.TestInfrastructure;

namespace OKX.Api.Tests.Financial;

public class OkxFinancialOkusdClientBehaviorTests
{
    [Fact]
    public async Task OkusdMethods_SendSignedCurrentRequests()
    {
        using var server = CreateServer();
        var client = CreateClient(server);

        var limits = await client.Financial.Okusd.GetLimitsAsync();
        var subscription = await client.Financial.Okusd.SubscribeAsync(1.23000000m, "sub_1-test");
        var redemption = await client.Financial.Okusd.RedeemAsync(2.50000000m, OkxFinancialOkusdRedemptionType.Standard, "redeem_1-test");

        Assert.True(limits.Success, limits.Error?.ToString());
        Assert.True(subscription.Success, subscription.Error?.ToString());
        Assert.True(redemption.Success, redemption.Error?.ToString());
        Assert.Equal(3, server.Requests.Count);
        Assert.All(server.Requests, request =>
        {
            Assert.Equal("key", request.Headers["OK-ACCESS-KEY"]);
            Assert.True(request.Headers.ContainsKey("OK-ACCESS-SIGN"));
            Assert.True(request.Headers.ContainsKey("OK-ACCESS-TIMESTAMP"));
            Assert.True(request.Headers.ContainsKey("OK-ACCESS-PASSPHRASE"));
        });

        var subscribeBody = JObject.Parse(server.Requests[1].Body);
        Assert.Equal("1.23", (string?)subscribeBody["amt"]);
        Assert.Equal("sub_1-test", (string?)subscribeBody["clOrdId"]);

        var redeemBody = JObject.Parse(server.Requests[2].Body);
        Assert.Equal("2.5", (string?)redeemBody["amt"]);
        Assert.Equal("2", (string?)redeemBody["redeemType"]);
        Assert.Equal("redeem_1-test", (string?)redeemBody["clOrdId"]);
    }

    [Fact]
    public async Task OkusdOrderMethods_RejectInvalidAmountsAndIdsBeforeSending()
    {
        using var server = CreateServer();
        var client = CreateClient(server);

        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => client.Financial.Okusd.SubscribeAsync(0.99999999m, "sub-1"));
        await Assert.ThrowsAsync<ArgumentException>(() => client.Financial.Okusd.SubscribeAsync(1.000000001m, "sub-1"));
        await Assert.ThrowsAsync<ArgumentException>(() => client.Financial.Okusd.SubscribeAsync(1m, "bad id"));
        await Assert.ThrowsAsync<ArgumentException>(() => client.Financial.Okusd.SubscribeAsync(1m, new string('a', 33)));
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => client.Financial.Okusd.RedeemAsync(1m, (OkxFinancialOkusdRedemptionType)byte.MaxValue, "redeem-1"));

        Assert.Empty(server.Requests);
    }

    [Fact]
    public async Task OkusdOrderMethods_TrimInsignificantZerosWithoutScientificNotation()
    {
        using var server = CreateServer();
        var client = CreateClient(server);

        var result = await client.Financial.Okusd.SubscribeAsync(1.000000000m, "sub-1");

        Assert.True(result.Success, result.Error?.ToString());
        var body = JObject.Parse(Assert.Single(server.Requests).Body);
        Assert.Equal("1", (string?)body["amt"]);
    }

    [Fact]
    public async Task OkusdMethods_EnforceSeparateDocumentedUserLimits()
    {
        using var server = CreateServer();
        var client = CreateClient(server, RateLimitingBehavior.Fail);

        Assert.True((await client.Financial.Okusd.GetLimitsAsync()).Success);
        Assert.True((await client.Financial.Okusd.GetLimitsAsync()).Success);
        Assert.False((await client.Financial.Okusd.GetLimitsAsync()).Success);

        Assert.True((await client.Financial.Okusd.SubscribeAsync(1m, "sub-1")).Success);
        Assert.False((await client.Financial.Okusd.SubscribeAsync(1m, "sub-2")).Success);

        Assert.True((await client.Financial.Okusd.RedeemAsync(1m, OkxFinancialOkusdRedemptionType.Fast, "redeem-1")).Success);
        Assert.False((await client.Financial.Okusd.RedeemAsync(1m, OkxFinancialOkusdRedemptionType.Fast, "redeem-2")).Success);

        Assert.Equal(4, server.Requests.Count);
    }

    [Fact]
    public async Task OkusdMethods_PreserveDocumentedNumericServerErrors()
    {
        using var server = new LocalOkxRestServer(new Dictionary<string, string>
        {
            ["POST /api/v5/finance/okusd/subscribe"] = "{\"code\":\"51763\",\"msg\":\"Your account does not meet VIP tier requirement\",\"data\":[]}",
        });
        var client = CreateClient(server);

        var result = await client.Financial.Okusd.SubscribeAsync(1m, "sub-1");

        Assert.False(result.Success);
        Assert.Equal(51763, result.Error?.Code);
        Assert.Contains("VIP tier", result.Error?.Message);
    }

    private static LocalOkxRestServer CreateServer()
        => new(new Dictionary<string, string>
        {
            ["GET /api/v5/finance/okusd/limits"] = FixtureReader.ReadManual("Financial", "okusd-limits.json"),
            ["POST /api/v5/finance/okusd/subscribe"] = FixtureReader.ReadManual("Financial", "okusd-subscribe.json"),
            ["POST /api/v5/finance/okusd/redeem"] = FixtureReader.ReadManual("Financial", "okusd-redeem.json"),
        });

    private static OkxRestApiClient CreateClient(LocalOkxRestServer server, RateLimitingBehavior rateLimitingBehavior = RateLimitingBehavior.Wait)
    {
        var options = new OkxRestApiOptions(new OkxApiCredentials("key", "secret", "pass"))
        {
            AutoTimestamp = false,
            BaseAddress = server.BaseAddress,
            RateLimitingBehavior = rateLimitingBehavior,
        };

        return new OkxRestApiClient(options);
    }
}