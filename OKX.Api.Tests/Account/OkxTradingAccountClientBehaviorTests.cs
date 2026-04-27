using Newtonsoft.Json.Linq;
using OKX.Api.Account;
using OKX.Api.Common;
using OKX.Api.Tests.TestInfrastructure;

namespace OKX.Api.Tests.Account;

public class OkxTradingAccountClientBehaviorTests
{
    [Fact]
    public async Task PositionBuilderTrendGraph_SendsMmrConfigObject()
    {
        using var server = CreateServer("/api/v5/account/position-builder-graph", "position-builder-graph-response.json");
        var client = CreateClient(server);

        var result = await client.Account.PositionBuilderAsync(
            importExisting: false,
            simulatedMmr: new OkxAccountSimulatedMmrRequest
            {
                AccountLevel = OkxAccountMode.MultiCurrencyMarginMode,
                Leverage = "1"
            });

        Assert.True(result.Success);

        var payload = ParseBody(server);
        Assert.Equal("mmr", payload["type"]?.Value<string>());
        Assert.Equal("3", payload["mmrConfig"]?["acctLv"]?.Value<string>());
        Assert.Equal("1", payload["mmrConfig"]?["lever"]?.Value<string>());
    }

    [Fact]
    public async Task GetPositionTiersAsync_ReturnsAllRows()
    {
        using var server = CreateServer("/api/v5/account/position-tiers", "get-position-tiers-multi.json");
        var client = CreateClient(server);

        var result = await client.Account.GetPositionTiersAsync(OkxInstrumentType.Swap, "BTC-USDT,ETH-USDT");

        Assert.True(result.Success);
        Assert.Equal(2, result.Data!.Count);

        var request = Assert.Single(server.Requests);
        var query = Uri.UnescapeDataString(request.Query);
        Assert.Contains("instType=SWAP", query);
        Assert.Contains("instFamily=BTC-USDT,ETH-USDT", query);
    }

    [Fact]
    public async Task SetTradingConfigAsync_SendsStrategyTypeBody()
    {
        using var server = CreateServer("/api/v5/account/set-trading-config", "set-trading-config.json");
        var client = CreateClient(server);

        var result = await client.Account.SetTradingConfigAsync(OkxAccountStrategyType.DeltaNeutral);

        Assert.True(result.Success);
        Assert.Equal(OkxAccountStrategyType.DeltaNeutral, result.Data!.StrategyType);

        var payload = ParseBody(server);
        Assert.Equal("stgyType", payload["type"]?.Value<string>());
        Assert.Equal("1", payload["stgyType"]?.Value<string>());
    }

    [Fact]
    public async Task PrecheckSetDeltaNeutralAsync_SendsStrategyTypeQuery()
    {
        using var server = CreateServer("/api/v5/account/precheck-set-delta-neutral", "precheck-set-delta-neutral.json");
        var client = CreateClient(server);

        var result = await client.Account.PrecheckSetDeltaNeutralAsync(OkxAccountStrategyType.DeltaNeutral);

        Assert.True(result.Success);
        Assert.NotNull(result.Data);

        var request = Assert.Single(server.Requests);
        Assert.Contains("stgyType=1", request.Query);
    }

    private static JObject ParseBody(LocalOkxRestServer server)
        => JObject.Parse(Assert.Single(server.Requests).Body);

    private static LocalOkxRestServer CreateServer(string path, string fixtureName)
        => new(new Dictionary<string, string>
        {
            [$"GET {path}"] = FixtureReader.ReadManual("Account", fixtureName),
            [$"POST {path}"] = FixtureReader.ReadManual("Account", fixtureName),
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
