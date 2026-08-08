using Newtonsoft.Json.Linq;
using OKX.Api.Account;
using OKX.Api.Common;
using OKX.Api.Tests.TestInfrastructure;
using System.Globalization;

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

    [Fact]
    public async Task AdjustDemoAccountBalanceAsync_SendsDocumentedBodyAndParsesResponse()
    {
        using var server = CreateServer("/api/v5/account/demo-adjust-balance", "demo-adjust-balance.json");
        var client = CreateClient(server, demoTrading: true);

        var result = await client.Account.AdjustDemoAccountBalanceAsync(CreateAdjustmentRequest(
            OkxAccountDemoBalanceAdjustmentType.Increase,
            ("BTC", 0.5m),
            ("USDT", 3000m)));

        Assert.True(result.Success, result.Error?.ToString());
        Assert.Equal(2, result.Data!.RemainingIncreaseCount);
        Assert.Equal(3, result.Data.TotalIncreaseCount);
        Assert.Collection(
            result.Data.Details,
            detail =>
            {
                Assert.Equal("BTC", detail.Currency);
                Assert.Equal(0.5m, detail.Amount);
                Assert.Equal(1.5m, detail.Balance);
            },
            detail =>
            {
                Assert.Equal("USDT", detail.Currency);
                Assert.Equal(3000m, detail.Amount);
                Assert.Equal(13000m, detail.Balance);
            });

        var capturedRequest = Assert.Single(server.Requests);
        Assert.Equal("POST", capturedRequest.Method);
        Assert.Equal("1", capturedRequest.Headers["x-simulated-trading"]);

        var payload = ParseBody(server);
        Assert.Equal("increase", payload["type"]?.Value<string>());
        var adjustmentArray = Assert.IsType<JArray>(payload["adjustments"]);
        var adjustments = adjustmentArray.Select(Assert.IsType<JObject>).ToList();
        Assert.Equal("BTC", adjustments[0]["ccy"]?.Value<string>());
        Assert.Equal(JTokenType.String, adjustments[0]["amt"]?.Type);
        Assert.Equal("0.5", adjustments[0]["amt"]?.Value<string>());
        Assert.Equal("USDT", adjustments[1]["ccy"]?.Value<string>());
        Assert.Equal("3000", adjustments[1]["amt"]?.Value<string>());
    }

    [Fact]
    public async Task AdjustDemoAccountBalanceAsync_ReduceDoesNotApplyIncreaseLimit()
    {
        using var server = CreateServer("/api/v5/account/demo-adjust-balance", "demo-adjust-balance.json");
        var client = CreateClient(server, demoTrading: true);

        var result = await client.Account.AdjustDemoAccountBalanceAsync(CreateAdjustmentRequest(
            OkxAccountDemoBalanceAdjustmentType.Reduce,
            ("USDT", 6000m)));

        Assert.True(result.Success, result.Error?.ToString());
        var payload = ParseBody(server);
        Assert.Equal("reduce", payload["type"]?.Value<string>());
        Assert.Equal("6000", payload["adjustments"]?[0]?["amt"]?.Value<string>());
    }

    [Fact]
    public async Task AdjustDemoAccountBalanceAsync_ReturnsDocumentedTopLevelError()
    {
        using var server = CreateServer("/api/v5/account/demo-adjust-balance", "demo-adjust-balance-error.json");
        var client = CreateClient(server, demoTrading: true);

        var result = await client.Account.AdjustDemoAccountBalanceAsync(CreateAdjustmentRequest(
            OkxAccountDemoBalanceAdjustmentType.Reduce,
            ("USDT", 1m)));

        Assert.False(result.Success);
        Assert.Equal(59693, result.Error?.Code);
        Assert.Contains("transferable balance insufficient", result.Error?.Message);
    }

    [Fact]
    public async Task AdjustDemoAccountBalanceAsync_RejectsProductionEnvironment()
    {
        using var server = CreateServer("/api/v5/account/demo-adjust-balance", "demo-adjust-balance.json");
        var client = CreateClient(server);

        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
            client.Account.AdjustDemoAccountBalanceAsync(CreateAdjustmentRequest(
                OkxAccountDemoBalanceAdjustmentType.Increase,
                ("BTC", 0.1m))));

        Assert.Contains("DemoTradingService", exception.Message);
        Assert.Empty(server.Requests);
    }

    [Fact]
    public async Task AdjustDemoAccountBalanceAsync_RejectsEmptyAdjustments()
    {
        using var server = CreateServer("/api/v5/account/demo-adjust-balance", "demo-adjust-balance.json");
        var client = CreateClient(server, demoTrading: true);

        await Assert.ThrowsAsync<ArgumentException>(() =>
            client.Account.AdjustDemoAccountBalanceAsync(CreateAdjustmentRequest(OkxAccountDemoBalanceAdjustmentType.Increase)));

        Assert.Empty(server.Requests);
    }

    [Fact]
    public async Task AdjustDemoAccountBalanceAsync_RejectsDuplicateCurrencies()
    {
        using var server = CreateServer("/api/v5/account/demo-adjust-balance", "demo-adjust-balance.json");
        var client = CreateClient(server, demoTrading: true);

        var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
            client.Account.AdjustDemoAccountBalanceAsync(CreateAdjustmentRequest(
                OkxAccountDemoBalanceAdjustmentType.Increase,
                ("BTC", 0.1m),
                ("BTC", 0.2m))));

        Assert.Contains("Duplicate", exception.Message);
        Assert.Empty(server.Requests);
    }

    [Fact]
    public async Task AdjustDemoAccountBalanceAsync_RejectsUnsupportedCurrency()
    {
        using var server = CreateServer("/api/v5/account/demo-adjust-balance", "demo-adjust-balance.json");
        var client = CreateClient(server, demoTrading: true);

        var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
            client.Account.AdjustDemoAccountBalanceAsync(CreateAdjustmentRequest(
                OkxAccountDemoBalanceAdjustmentType.Increase,
                ("SOL", 0.1m))));

        Assert.Contains("BTC, ETH, USDT, and OKB", exception.Message);
        Assert.Empty(server.Requests);
    }

    [Fact]
    public async Task AdjustDemoAccountBalanceAsync_RejectsNegativeAmount()
    {
        using var server = CreateServer("/api/v5/account/demo-adjust-balance", "demo-adjust-balance.json");
        var client = CreateClient(server, demoTrading: true);

        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
            client.Account.AdjustDemoAccountBalanceAsync(CreateAdjustmentRequest(
                OkxAccountDemoBalanceAdjustmentType.Reduce,
                ("BTC", -0.1m))));

        Assert.Empty(server.Requests);
    }

    [Theory]
    [InlineData("BTC", "1.00000001")]
    [InlineData("ETH", "1.00000001")]
    [InlineData("USDT", "5000.01")]
    [InlineData("OKB", "100.01")]
    public async Task AdjustDemoAccountBalanceAsync_RejectsIncreaseAbovePerRequestLimit(string currency, string amount)
    {
        using var server = CreateServer("/api/v5/account/demo-adjust-balance", "demo-adjust-balance.json");
        var client = CreateClient(server, demoTrading: true);

        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
            client.Account.AdjustDemoAccountBalanceAsync(CreateAdjustmentRequest(
                OkxAccountDemoBalanceAdjustmentType.Increase,
                (currency, decimal.Parse(amount, CultureInfo.InvariantCulture)))));

        Assert.Empty(server.Requests);
    }

    private static JObject ParseBody(LocalOkxRestServer server)
        => JObject.Parse(Assert.Single(server.Requests).Body);

    private static LocalOkxRestServer CreateServer(string path, string fixtureName)
        => new(new Dictionary<string, string>
        {
            [$"GET {path}"] = FixtureReader.ReadManual("Account", fixtureName),
            [$"POST {path}"] = FixtureReader.ReadManual("Account", fixtureName),
        });

    private static OkxAccountDemoBalanceAdjustmentRequest CreateAdjustmentRequest(
        OkxAccountDemoBalanceAdjustmentType type,
        params (string Currency, decimal Amount)[] adjustments)
        => new()
        {
            Type = type,
            Adjustments = adjustments.Select(x => new OkxAccountDemoBalanceAdjustmentItemRequest
            {
                Currency = x.Currency,
                Amount = x.Amount,
            }),
        };

    private static OkxRestApiClient CreateClient(LocalOkxRestServer server, bool demoTrading = false)
    {
        var options = new OkxRestApiOptions(new OkxApiCredentials("key", "secret", "pass"))
        {
            AutoTimestamp = false,
            DemoTradingService = demoTrading,
            BaseAddress = server.BaseAddress,
        };

        return new OkxRestApiClient(options);
    }
}
