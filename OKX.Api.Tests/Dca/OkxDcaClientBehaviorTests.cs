using Newtonsoft.Json.Linq;
using OKX.Api.Dca;
using OKX.Api.Grid;
using OKX.Api.Tests.TestInfrastructure;
using OKX.Api.Trade;

namespace OKX.Api.Tests.Dca;

public class OkxDcaClientBehaviorTests
{
    [Fact]
    public async Task PlaceOrderAsync_SendsExpectedBody()
    {
        using var server = CreateServer("/api/v5/tradingBot/dca/create", "order-response.json");
        var client = CreateClient(server);

        var result = await client.DCA.PlaceOrderAsync(new OkxDcaCreateOrderRequest
        {
            InstrumentId = "BTC-USDT-SWAP",
            AlgoOrderType = OkxDcaAlgoOrderType.ContractDca,
            Direction = OkxDcaDirection.Long,
            Leverage = 5,
            InitialOrderAmount = 50,
            MaximumSafetyOrders = 2,
            SafetyOrderAmount = 25,
            PriceStepRatio = 0.01m,
            PriceStepMultiplier = 1.2m,
            VolumeMultiplier = 1.5m,
            TakeProfitTarget = 0.05m,
            StopLossTarget = 0.02m,
            StopLossMode = OkxDcaStopLossMode.Market,
            AllowReinvest = true,
            ProfitSharingRatio = 0.1m,
            TrackingMode = OkxDcaTrackingMode.Sync,
            AlgoClientOrderId = "dca-create-01",
            TriggerParameters =
            [
                new OkxDcaTriggerParametersRequest
                {
                    TriggerAction = OkxDcaTriggerAction.Start,
                    TriggerStrategy = OkxDcaTriggerStrategy.RSI,
                    TimeFrame = OkxGridAlgoTimeFrame.ThirtyMinutes,
                    Threshold = "10",
                    TriggerCondition = OkxGridAlgoTriggerCondition.Cross,
                    TimePeriod = "14"
                }
            ]
        });

        Assert.True(result.Success);
        var body = ParseBody(server);
        Assert.Equal("BTC-USDT-SWAP", body["instId"]?.Value<string>());
        Assert.Equal("contract_dca", body["algoOrdType"]?.Value<string>());
        Assert.Equal("long", body["direction"]?.Value<string>());
        Assert.Equal("5", body["lever"]?.Value<string>());
        Assert.Equal("50", body["initOrdAmt"]?.Value<string>());
        Assert.Equal("2", body["maxSafetyOrds"]?.Value<string>());
        Assert.Equal("0.05", body["tpPct"]?.Value<string>());
        Assert.Equal("0.02", body["slPct"]?.Value<string>());
        Assert.Equal("market", body["slMode"]?.Value<string>());
        Assert.True(body["allowReinvest"]?.Value<bool>());
        Assert.Equal("0.1", body["profitSharingRatio"]?.Value<string>());
        Assert.Equal("sync", body["trackingMode"]?.Value<string>());
        Assert.Equal("dca-create-01", body["algoClOrdId"]?.Value<string>());
        Assert.Equal("538a3965e538BCDE", body["tag"]?.Value<string>());

        var triggerParamsToken = body["triggerParams"];
        Assert.NotNull(triggerParamsToken);
        var triggerParams = Assert.IsAssignableFrom<JArray>(triggerParamsToken);
        var trigger = Assert.Single(triggerParams.Values<JObject>());
        Assert.NotNull(trigger);
        Assert.Equal("start", trigger["triggerAction"]?.Value<string>());
        Assert.Equal("rsi", trigger["triggerStrategy"]?.Value<string>());
        Assert.Equal("30m", trigger["timeframe"]?.Value<string>());
        Assert.Equal("10", trigger["thold"]?.Value<string>());
        Assert.Equal("cross", trigger["triggerCond"]?.Value<string>());
        Assert.Equal("14", trigger["timePeriod"]?.Value<string>());
    }

    [Fact]
    public async Task AmendOrderAsync_SendsExpectedBody()
    {
        using var server = CreateServer("/api/v5/tradingBot/dca/amend-order-algo", "order-response.json");
        var client = CreateClient(server);

        var result = await client.DCA.AmendOrderAsync(new OkxDcaAmendOrderRequest
        {
            AlgoOrderId = 123,
            PriceStepRatio = 0.02m,
            PriceStepMultiplier = 1.5m,
            VolumeMultiplier = 2.1m,
            TakeProfitTarget = 0.06m,
            StopLossTarget = 0.03m,
            InitialOrderAmount = 100,
            SafetyOrderAmount = 50,
            MaximumSafetyOrders = 3,
            ReserveFunds = true,
            TriggerParameters =
            [
                new OkxDcaTriggerParametersRequest
                {
                    TriggerAction = OkxDcaTriggerAction.Start,
                    TriggerStrategy = OkxDcaTriggerStrategy.Instant
                }
            ]
        });

        Assert.True(result.Success);
        var body = ParseBody(server);
        Assert.Equal("123", body["algoId"]?.Value<string>());
        Assert.Equal("0.02", body["pxSteps"]?.Value<string>());
        Assert.Equal("1.5", body["pxStepsMult"]?.Value<string>());
        Assert.Equal("2.1", body["volMult"]?.Value<string>());
        Assert.Equal("0.06", body["tpPct"]?.Value<string>());
        Assert.Equal("0.03", body["slPct"]?.Value<string>());
        Assert.Equal("100", body["initOrdAmt"]?.Value<string>());
        Assert.Equal("50", body["safetyOrdAmt"]?.Value<string>());
        Assert.Equal("3", body["maxSafetyOrds"]?.Value<string>());
        Assert.True(body["reserveFunds"]!.Value<bool>());
        var triggerParamsToken = body["triggerParams"];
        Assert.NotNull(triggerParamsToken);
        var triggerParams = Assert.IsAssignableFrom<JArray>(triggerParamsToken);
        var trigger = Assert.Single(triggerParams.Values<JObject>());
        Assert.NotNull(trigger);
        Assert.Equal("instant", trigger["triggerStrategy"]?.Value<string>());
    }

    [Fact]
    public async Task StopOrderAsync_SendsExpectedBody()
    {
        using var server = CreateServer("/api/v5/tradingBot/dca/stop", "order-response.json");
        var client = CreateClient(server);

        var result = await client.DCA.StopOrderAsync(321, OkxDcaAlgoOrderType.ContractDca, OkxDcaStopType.Keep);

        Assert.True(result.Success);
        var body = ParseBody(server);
        Assert.Equal("321", body["algoId"]?.Value<string>());
        Assert.Equal("contract_dca", body["algoOrdType"]?.Value<string>());
        Assert.Equal("2", body["stopType"]?.Value<string>());
    }

    [Fact]
    public async Task GetOrderAsync_SendsExpectedQuery()
    {
        using var server = CreateServer("/api/v5/tradingBot/dca/ongoing-list", "get-ongoing-orders.json");
        var client = CreateClient(server);

        var result = await client.DCA.GetOrderAsync(123, OkxDcaAlgoOrderType.ContractDca);

        Assert.True(result.Success);
        var request = Assert.Single(server.Requests);
        Assert.Contains("algoId=123", request.Query);
        Assert.Contains("algoOrdType=contract_dca", request.Query);
    }

    [Fact]
    public async Task GetOrderHistoryAsync_SendsExpectedQuery()
    {
        using var server = CreateServer("/api/v5/tradingBot/dca/history-list", "get-order-history.json");
        var client = CreateClient(server);

        var result = await client.DCA.GetOrderHistoryAsync(OkxDcaAlgoOrderType.ContractDca, 321, after: 100, before: 50, limit: 10);

        Assert.True(result.Success);
        var request = Assert.Single(server.Requests);
        Assert.Contains("algoOrdType=contract_dca", request.Query);
        Assert.Contains("algoId=321", request.Query);
        Assert.Contains("after=100", request.Query);
        Assert.Contains("before=50", request.Query);
        Assert.Contains("limit=10", request.Query);
    }

    [Fact]
    public async Task GetSubOrdersAsync_SendsExpectedQuery()
    {
        using var server = CreateServer("/api/v5/tradingBot/dca/orders", "get-sub-orders.json");
        var client = CreateClient(server);

        var result = await client.DCA.GetSubOrdersAsync(123, OkxDcaAlgoOrderType.ContractDca, cycleId: 88, after: 10, before: 5, limit: 20);

        Assert.True(result.Success);
        var request = Assert.Single(server.Requests);
        Assert.Contains("algoId=123", request.Query);
        Assert.Contains("algoOrdType=contract_dca", request.Query);
        Assert.Contains("cycleId=88", request.Query);
        Assert.Contains("after=10", request.Query);
        Assert.Contains("before=5", request.Query);
        Assert.Contains("limit=20", request.Query);
    }

    [Fact]
    public async Task ManualBuyAsync_SendsExpectedBody()
    {
        using var server = CreateServer("/api/v5/tradingBot/dca/orders/manual-buy", "manual-buy-response.json");
        var client = CreateClient(server);

        var result = await client.DCA.ManualBuyAsync(456, OkxDcaAlgoOrderType.SpotDca, 42000m, 100m, OkxTradeOrderType.MarketOrder, "USDT");

        Assert.True(result.Success);
        var body = ParseBody(server);
        Assert.Equal("456", body["algoId"]?.Value<string>());
        Assert.Equal("spot_dca", body["algoOrdType"]?.Value<string>());
        Assert.Equal("42000", body["price"]?.Value<string>());
        Assert.Equal("100", body["amt"]?.Value<string>());
        Assert.Equal("market", body["ordType"]?.Value<string>());
        Assert.Equal("USDT", body["tradeQuoteCcy"]?.Value<string>());
        Assert.Equal("538a3965e538BCDE", body["tag"]?.Value<string>());
    }

    [Fact]
    public async Task AmendReinvestmentAsync_SendsExpectedBody()
    {
        using var server = CreateServer("/api/v5/tradingBot/dca/settings/reinvestment", "order-response.json");
        var client = CreateClient(server);

        var result = await client.DCA.AmendReinvestmentAsync(654, OkxDcaAlgoOrderType.ContractDca, true);

        Assert.True(result.Success);
        var body = ParseBody(server);
        Assert.Equal("654", body["algoId"]?.Value<string>());
        Assert.Equal("contract_dca", body["algoOrdType"]?.Value<string>());
        Assert.True(body["allowReinvest"]!.Value<bool>());
    }

    [Fact]
    public async Task AmendTakeProfitAsync_SendsExpectedBody()
    {
        using var server = CreateServer("/api/v5/tradingBot/dca/settings/take-profit", "order-response.json");
        var client = CreateClient(server);

        var result = await client.DCA.AmendTakeProfitAsync(654, OkxDcaAlgoOrderType.ContractDca, 52000m);

        Assert.True(result.Success);
        var body = ParseBody(server);
        Assert.Equal("654", body["algoId"]?.Value<string>());
        Assert.Equal("contract_dca", body["algoOrdType"]?.Value<string>());
        Assert.Equal("52000", body["tpPrice"]?.Value<string>());
    }

    [Fact]
    public async Task GetPositionDetailsAsync_SendsExpectedQuery()
    {
        using var server = CreateServer("/api/v5/tradingBot/dca/position-details", "get-position-details.json");
        var client = CreateClient(server);

        var result = await client.DCA.GetPositionDetailsAsync(987, OkxDcaAlgoOrderType.ContractDca);

        Assert.True(result.Success);
        var request = Assert.Single(server.Requests);
        Assert.Contains("algoId=987", request.Query);
        Assert.Contains("algoOrdType=contract_dca", request.Query);
    }

    [Fact]
    public async Task GetCyclesAsync_SendsExpectedQuery()
    {
        using var server = CreateServer("/api/v5/tradingBot/dca/cycle-list", "get-cycle-list.json");
        var client = CreateClient(server);

        var result = await client.DCA.GetCyclesAsync(777, OkxDcaAlgoOrderType.ContractDca, "BTC-USDT-SWAP", after: 9, before: 3, limit: 25);

        Assert.True(result.Success);
        var request = Assert.Single(server.Requests);
        Assert.Contains("algoId=777", request.Query);
        Assert.Contains("algoOrdType=contract_dca", request.Query);
        Assert.Contains("instId=BTC-USDT-SWAP", request.Query);
        Assert.Contains("after=9", request.Query);
        Assert.Contains("before=3", request.Query);
        Assert.Contains("limit=25", request.Query);
    }

    [Fact]
    public async Task AddMarginAsync_SendsExpectedBody()
    {
        using var server = CreateServer("/api/v5/tradingBot/dca/margin/add", "order-response.json");
        var client = CreateClient(server);

        var result = await client.DCA.AddMarginAsync(888, 25m);

        Assert.True(result.Success);
        var body = ParseBody(server);
        Assert.Equal("888", body["algoId"]?.Value<string>());
        Assert.Equal("25", body["amt"]?.Value<string>());
    }

    [Fact]
    public async Task ReduceMarginAsync_SendsExpectedBody()
    {
        using var server = CreateServer("/api/v5/tradingBot/dca/margin/reduce", "order-response.json");
        var client = CreateClient(server);

        var result = await client.DCA.ReduceMarginAsync(999, 10m);

        Assert.True(result.Success);
        var body = ParseBody(server);
        Assert.Equal("999", body["algoId"]?.Value<string>());
        Assert.Equal("10", body["amt"]?.Value<string>());
    }

    private static JObject ParseBody(LocalOkxRestServer server)
        => JObject.Parse(Assert.Single(server.Requests).Body);

    private static LocalOkxRestServer CreateServer(string path, string fixtureName)
        => new(new Dictionary<string, string>
        {
            [$"GET {path}"] = FixtureReader.ReadManual("Dca", fixtureName),
            [$"POST {path}"] = FixtureReader.ReadManual("Dca", fixtureName),
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
