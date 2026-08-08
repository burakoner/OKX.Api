using ApiSharp.Throttling;
using Newtonsoft.Json.Linq;
using OKX.Api.Account;
using OKX.Api.Common;
using OKX.Api.Tests.TestInfrastructure;

namespace OKX.Api.Tests.Account;

public class OkxAccountBillArchiveClientBehaviorTests
{
    [Fact]
    public async Task ApplyAndGetBillDataAsync_UseCurrentContractsAndSeparateRateLimits()
    {
        using var server = CreateServer();
        var client = CreateClient(server, RateLimitingBehavior.Fail);

        var apply = await client.Account.ApplyBillDataAsync(
            2023,
            OkxQuarter.Quarter1,
            [OkxAccountBillType.Transfer, OkxAccountBillType.Trade, (OkxAccountBillType)999, OkxAccountBillType.Transfer]);

        Assert.True(apply.Success, apply.Error?.ToString());
        Assert.True(apply.Data!.Result);
        Assert.Equal(1646892328000L, apply.Data.Timestamp);

        var applyRequest = Assert.Single(server.Requests);
        var body = JObject.Parse(applyRequest.Body);
        Assert.Equal("2023", (string?)body["year"]);
        Assert.Equal("Q1", (string?)body["quarter"]);
        Assert.Equal("1,2,999", (string?)body["type"]);

        var get = await client.Account.GetBillDataAsync(
            2023,
            OkxQuarter.Quarter4,
            [OkxAccountBillType.Transfer, OkxAccountBillType.Trade]);

        Assert.True(get.Success, get.Error?.ToString());
        Assert.Equal("https://example.test/bills.csv.zip", get.Data!.DownloadLink);
        Assert.Equal(OkxDownloadLinkState.Finished, get.Data.State);
        Assert.Equal(1646892328000L, get.Data.Timestamp);

        Assert.Equal(2, server.Requests.Count);
        var getRequest = server.Requests[1];
        var query = Uri.UnescapeDataString(getRequest.Query);
        Assert.Contains("year=2023", query);
        Assert.Contains("quarter=Q4", query);
        Assert.Contains("type=1,2", query);

        for (var i = 0; i < 9; i++)
        {
            var allowedGet = await client.Account.GetBillDataAsync(2023, OkxQuarter.Quarter4);
            Assert.True(allowedGet.Success, allowedGet.Error?.ToString());
        }

        var limitedGet = await client.Account.GetBillDataAsync(2023, OkxQuarter.Quarter4);
        Assert.False(limitedGet.Success);
        Assert.Equal(11, server.Requests.Count);

        var limited = await client.Account.ApplyBillDataAsync(2023, OkxQuarter.Quarter1);

        Assert.False(limited.Success);
        Assert.Equal(11, server.Requests.Count);
    }

    [Theory]
    [InlineData(999)]
    [InlineData(10000)]
    public async Task BillDataMethods_RejectNonFourDigitYears(int year)
    {
        using var server = CreateServer();
        var client = CreateClient(server);

        await Assert.ThrowsAsync<ArgumentException>(() => client.Account.ApplyBillDataAsync(year, OkxQuarter.Quarter1));
        await Assert.ThrowsAsync<ArgumentException>(() => client.Account.GetBillDataAsync(year, OkxQuarter.Quarter1));
        Assert.Empty(server.Requests);
    }

    [Fact]
    public async Task BillDataMethods_RejectUnknownQuarter()
    {
        using var server = CreateServer();
        var client = CreateClient(server);

        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => client.Account.ApplyBillDataAsync(2023, (OkxQuarter)0));
        Assert.Empty(server.Requests);
    }

    private static LocalOkxRestServer CreateServer()
        => new(new Dictionary<string, string>
        {
            ["POST /api/v5/account/bills-history-archive"] = FixtureReader.ReadManual("Account", "apply-bills-history-archive.json"),
            ["GET /api/v5/account/bills-history-archive"] = FixtureReader.ReadManual("Account", "get-bills-history-archive.json"),
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
