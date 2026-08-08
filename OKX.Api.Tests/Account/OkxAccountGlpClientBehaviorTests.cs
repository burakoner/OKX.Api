using ApiSharp.Throttling;
using OKX.Api.Account;
using OKX.Api.Tests.TestInfrastructure;

namespace OKX.Api.Tests.Account;

public class OkxAccountGlpClientBehaviorTests
{
    [Fact]
    public async Task GlpMethods_SendSignedCurrentRequestsAndSerializeHistoryFilters()
    {
        using var server = CreateServer();
        var client = CreateClient(server);

        var today = await client.Account.GetGlpTodayPerformanceAsync();
        var history = await client.Account.GetGlpHistoricalPerformanceAsync(
            OkxAccountGlpProgram.ExpiryAndNitro,
            begin: 1751299200000,
            end: 1753804800000,
            limit: 60);

        Assert.True(today.Success, today.Error?.ToString());
        Assert.True(history.Success, history.Error?.ToString());
        Assert.Equal(2, server.Requests.Count);
        Assert.All(server.Requests, request =>
        {
            Assert.Equal("GET", request.Method);
            Assert.Equal("key", request.Headers["OK-ACCESS-KEY"]);
            Assert.True(request.Headers.ContainsKey("OK-ACCESS-SIGN"));
            Assert.True(request.Headers.ContainsKey("OK-ACCESS-TIMESTAMP"));
            Assert.True(request.Headers.ContainsKey("OK-ACCESS-PASSPHRASE"));
        });

        Assert.Equal("/api/v5/users/glp/today-performance", server.Requests[0].Path);
        Assert.Equal("/api/v5/users/glp/historical-performance", server.Requests[1].Path);
        var query = Uri.UnescapeDataString(server.Requests[1].Query);
        Assert.Contains("program=FUT_NTO", query);
        Assert.Contains("begin=1751299200000", query);
        Assert.Contains("end=1753804800000", query);
        Assert.Contains("limit=60", query);
    }

    [Fact]
    public async Task GlpHistory_RejectsInvalidFiltersBeforeSending()
    {
        using var server = CreateServer();
        var client = CreateClient(server);

        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
            client.Account.GetGlpHistoricalPerformanceAsync((OkxAccountGlpProgram)byte.MaxValue));
        await Assert.ThrowsAsync<ArgumentException>(() =>
            client.Account.GetGlpHistoricalPerformanceAsync(OkxAccountGlpProgram.Spot, begin: 2, end: 1));
        await Assert.ThrowsAsync<ArgumentException>(() =>
            client.Account.GetGlpHistoricalPerformanceAsync(OkxAccountGlpProgram.Spot, limit: 101));

        Assert.Empty(server.Requests);
    }

    [Fact]
    public async Task GlpMethods_EnforceSeparateFivePerTwoSecondUserLimits()
    {
        using var server = CreateServer();
        var client = CreateClient(server, RateLimitingBehavior.Fail);

        for (var i = 0; i < 5; i++)
            Assert.True((await client.Account.GetGlpTodayPerformanceAsync()).Success);
        Assert.False((await client.Account.GetGlpTodayPerformanceAsync()).Success);

        for (var i = 0; i < 5; i++)
            Assert.True((await client.Account.GetGlpHistoricalPerformanceAsync(OkxAccountGlpProgram.Spot)).Success);
        Assert.False((await client.Account.GetGlpHistoricalPerformanceAsync(OkxAccountGlpProgram.Spot)).Success);

        Assert.Equal(10, server.Requests.Count);
    }

    private static LocalOkxRestServer CreateServer()
        => new(new Dictionary<string, string>
        {
            ["GET /api/v5/users/glp/today-performance"] = FixtureReader.ReadManual("Account", "glp-today-performance.json"),
            ["GET /api/v5/users/glp/historical-performance"] = FixtureReader.ReadManual("Account", "glp-historical-performance.json"),
        });

    private static OkxRestApiClient CreateClient(
        LocalOkxRestServer server,
        RateLimitingBehavior rateLimitingBehavior = RateLimitingBehavior.Wait)
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
