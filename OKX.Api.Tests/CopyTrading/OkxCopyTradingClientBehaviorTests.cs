using Newtonsoft.Json.Linq;
using OKX.Api.CopyTrading;
using OKX.Api.Tests.TestInfrastructure;

namespace OKX.Api.Tests.CopyTrading;

public class OkxCopyTradingClientBehaviorTests
{
    private const string UniqueCode16 = "213E8C92DC61EFAC";
    private const string UniqueCode18 = "381749205163847291";

    [Fact]
    public async Task FirstCopySettingsAsync_Accepts18CharacterCodeAndSerializesCurrentEnums()
    {
        using var server = CreateServer("POST", "/api/v5/copytrading/first-copy-settings", "success.json");
        var client = CreateClient(server);

        var result = await client.CopyTrading.FirstCopySettingsAsync(
            UniqueCode18,
            OkxCopyTradingMarginMode.Cross,
            OkxCopyTradingInstrumentIdType.Copy,
            500m,
            OkxCopyTradingPositionCloseType.CopyClose,
            copyMode: OkxCopyTradingMode.RatioCopy,
            copyRatio: 1m);

        Assert.True(result.Success, result.Error?.ToString());
        Assert.True(result.Data!.Result);

        var body = JObject.Parse(Assert.Single(server.Requests).Body);
        Assert.Equal(UniqueCode18, (string?)body["uniqueCode"]);
        Assert.Equal("cross", (string?)body["copyMgnMode"]);
        Assert.Equal("ratio_copy", (string?)body["copyMode"]);
        Assert.Equal("1", (string?)body["copyRatio"]);
    }

    [Fact]
    public async Task Analytics_RejectsUnsupportedUniqueCodeLengthBeforeRequest()
    {
        using var server = CreateServer("GET", "/api/v5/copytrading/public-weekly-pnl", "stats.json");
        var client = CreateClient(server);

        await Assert.ThrowsAsync<ArgumentException>(() => client.CopyTrading.GetLeadTraderWeeklyPnlAsync("38174920516384729"));

        Assert.Empty(server.Requests);
    }

    [Fact]
    public async Task FirstCopySettingsAsync_RequiresInstrumentIdsForCustomSelection()
    {
        using var server = CreateServer("POST", "/api/v5/copytrading/first-copy-settings", "success.json");
        var client = CreateClient(server);

        await Assert.ThrowsAsync<ArgumentException>(() => client.CopyTrading.FirstCopySettingsAsync(
            UniqueCode16,
            OkxCopyTradingMarginMode.Isolated,
            OkxCopyTradingInstrumentIdType.Custom,
            500m,
            OkxCopyTradingPositionCloseType.CopyClose,
            copyMode: OkxCopyTradingMode.FixedAmount,
            copyAmount: 10m));

        Assert.Empty(server.Requests);
    }

    [Fact]
    public async Task StopCopyingAsync_AllowsDocumentedDefaultInstrumentType()
    {
        using var server = CreateServer("POST", "/api/v5/copytrading/stop-copy-trading", "success.json");
        var client = CreateClient(server);

        var result = await client.CopyTrading.StopCopyingAsync(
            UniqueCode18,
            OkxCopyTradingPositionCloseType.CopyClose);

        Assert.True(result.Success, result.Error?.ToString());
        var body = JObject.Parse(Assert.Single(server.Requests).Body);
        Assert.Null(body["instType"]);
    }

    [Fact]
    public async Task GetLeadTraderStatsAsync_ParsesCurrencyAndSendsTypedPeriod()
    {
        using var server = CreateServer("GET", "/api/v5/copytrading/public-stats", "stats.json");
        var client = CreateClient(server);

        var result = await client.CopyTrading.GetLeadTraderStatsAsync(
            UniqueCode18,
            OkxCopyTradingPerformancePeriod.Last365Days);

        Assert.True(result.Success, result.Error?.ToString());
        Assert.Equal("USDT", Assert.Single(result.Data!).Currency);

        var query = Uri.UnescapeDataString(Assert.Single(server.Requests).Query);
        Assert.Contains($"uniqueCode={UniqueCode18}", query);
        Assert.Contains("lastDays=4", query);
    }

    [Fact]
    public async Task GetCopySettingsAsync_ParsesTagAndCorrectedMarginMode()
    {
        using var server = CreateServer("GET", "/api/v5/copytrading/copy-settings", "copy-settings.json");
        var client = CreateClient(server);

        var result = await client.CopyTrading.GetCopySettingsAsync(UniqueCode16);

        Assert.True(result.Success, result.Error?.ToString());
        Assert.Equal("affiliate-tag", result.Data!.Tag);
        Assert.Equal(OkxCopyTradingMarginMode.Cross, result.Data.MarginMode);
    }

    private static LocalOkxRestServer CreateServer(string method, string path, string fixtureName)
        => new(new Dictionary<string, string>
        {
            [$"{method} {path}"] = FixtureReader.ReadManual("CopyTrading", fixtureName),
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
