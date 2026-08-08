using Newtonsoft.Json.Linq;
using OKX.Api.Algo;
using OKX.Api.Common;
using OKX.Api.Tests.TestInfrastructure;
using OKX.Api.Trade;

namespace OKX.Api.Tests.Algo;

public class OkxAlgoCurrentClientBehaviorTests
{
    [Fact]
    public async Task PlaceTriggerChase_SendsNestedParametersWithoutStandaloneChaseFields()
    {
        using var server = CreateServer();
        var client = CreateClient(server);

        var result = await client.Algo.PlaceOrderAsync(
            "BTC-USDT-SWAP",
            OkxTradeMode.Cross,
            OkxTradeOrderSide.Buy,
            OkxAlgoOrderType.Trigger,
            size: 2,
            triggerPrice: 90000,
            triggerPriceType: OkxAlgoPriceType.Mark,
            triggerOrderType: OkxAlgoTriggerOrderType.Chase,
            advancedChaseParameters:
            [
                new()
                {
                    ChaseType = OkxAlgoChaseType.Distance,
                    ChaseValue = 25.5m,
                    MaximumChaseType = OkxAlgoChaseType.Ratio,
                    MaximumChaseValue = 0.02m,
                },
            ]);

        Assert.True(result.Success, result.Error?.ToString());
        Assert.Equal("trigger-chase", result.Data!.ClientAlgoOrderId);
        Assert.Equal("risk-entry", result.Data.Tag);

        var body = JObject.Parse(Assert.Single(server.Requests).Body);
        Assert.Equal("trigger", (string?)body["ordType"]);
        Assert.Equal("chase", (string?)body["advanceOrdType"]);
        Assert.Null(body["orderPx"]);
        Assert.Null(body["attachAlgoOrds"]);
        Assert.Null(body["chaseType"]);
        Assert.Null(body["chaseVal"]);
        var chase = Assert.IsType<JObject>(Assert.IsType<JArray>(body["advChaseParams"])[0]);
        Assert.Equal("distance", (string?)chase["chaseType"]);
        Assert.Equal("25.5", (string?)chase["chaseVal"]);
        Assert.Equal("ratio", (string?)chase["maxChaseType"]);
        Assert.Equal("0.02", (string?)chase["maxChaseVal"]);
    }

    [Fact]
    public async Task PlaceStandaloneChase_KeepsExistingRootContract()
    {
        using var server = CreateServer();
        var client = CreateClient(server);

        var result = await client.Algo.PlaceOrderAsync(
            "BTC-USDT-SWAP",
            OkxTradeMode.Cross,
            OkxTradeOrderSide.Buy,
            OkxAlgoOrderType.Chase,
            size: 2,
            chaseType: OkxAlgoChaseType.Distance,
            chaseValue: 10,
            maxChaseType: OkxAlgoChaseType.Ratio,
            maxChaseValue: 0.02m);

        Assert.True(result.Success, result.Error?.ToString());
        var body = JObject.Parse(Assert.Single(server.Requests).Body);
        Assert.Equal("chase", (string?)body["ordType"]);
        Assert.Equal("distance", (string?)body["chaseType"]);
        Assert.Equal("10", (string?)body["chaseVal"]);
        Assert.Null(body["advanceOrdType"]);
        Assert.Null(body["advChaseParams"]);
    }

    [Fact]
    public async Task AmendTriggerChase_SendsOnlyMutableNestedValuesAsStrings()
    {
        using var server = CreateServer();
        var client = CreateClient(server);

        var result = await client.Algo.AmendOrderAsync(
            "BTC-USDT-SWAP",
            algoOrderId: 700000000000000001,
            advancedChaseParameters:
            [
                new()
                {
                    NewChaseValue = 30,
                    NewMaximumChaseValue = 0.03m,
                },
            ]);

        Assert.True(result.Success, result.Error?.ToString());
        var body = JObject.Parse(Assert.Single(server.Requests).Body);
        var chase = Assert.IsType<JObject>(Assert.IsType<JArray>(body["advChaseParams"])[0]);
        Assert.Equal("30", (string?)chase["newChaseVal"]);
        Assert.Equal("0.03", (string?)chase["newMaxChaseVal"]);
        Assert.Null(chase["chaseType"]);
        Assert.Null(chase["maxChaseType"]);
    }

    [Fact]
    public async Task PlaceSmartIceberg_SendsAllCurrentDocumentedFields()
    {
        using var server = CreateServer();
        var client = CreateClient(server);

        var result = await client.Algo.PlaceOrderAsync(
            "BTC-USDT-SWAP",
            OkxTradeMode.Cross,
            OkxTradeOrderSide.Buy,
            OkxAlgoOrderType.SmartIceberg,
            size: 100,
            sizeLimit: 5,
            priceLimit: 85000,
            limitOrderNumber: 20,
            aggressiveness: OkxAlgoSmartIcebergAggressiveness.Mid,
            smartIcebergTriggerParameters:
            [
                new()
                {
                    TriggerAction = OkxAlgoSmartIcebergTriggerAction.Start,
                    TriggerStrategy = OkxAlgoSmartIcebergTriggerStrategy.RSI,
                    TriggerCondition = OkxAlgoSmartIcebergTriggerCondition.CrossDown,
                    TimeFrame = OkxAlgoSmartIcebergTimeFrame.ThirtyMinutes,
                    Threshold = 30,
                    TimePeriod = 14,
                },
            ]);

        Assert.True(result.Success, result.Error?.ToString());
        var body = JObject.Parse(Assert.Single(server.Requests).Body);
        Assert.Equal("smart_iceberg", (string?)body["ordType"]);
        Assert.Equal("5", (string?)body["szLimit"]);
        Assert.Equal("20", (string?)body["lmtOrderNumber"]);
        Assert.Equal("mid", (string?)body["aggressiveness"]);
        var trigger = Assert.IsType<JObject>(Assert.IsType<JArray>(body["triggerParams"])[0]);
        Assert.Equal("start", (string?)trigger["triggerAction"]);
        Assert.Equal("rsi", (string?)trigger["triggerStrategy"]);
        Assert.Equal("cross_down", (string?)trigger["triggerCond"]);
        Assert.Equal("30m", (string?)trigger["timeframe"]);
        Assert.Equal("30", (string?)trigger["thold"]);
        Assert.Equal("14", (string?)trigger["timePeriod"]);
    }

    [Fact]
    public async Task TriggerChaseAndSmartIceberg_RejectUnsafeContractsBeforeSending()
    {
        using var server = CreateServer();
        var client = CreateClient(server);

        await Assert.ThrowsAsync<ArgumentException>(() => client.Algo.PlaceOrderAsync(
            "BTC-USDT-SWAP", OkxTradeMode.Cross, OkxTradeOrderSide.Buy, OkxAlgoOrderType.Trigger,
            size: 1, triggerPrice: 90000, triggerOrderType: OkxAlgoTriggerOrderType.Chase));
        await Assert.ThrowsAsync<ArgumentException>(() => client.Algo.PlaceOrderAsync(
            "BTC-USDT-SWAP", OkxTradeMode.Cross, OkxTradeOrderSide.Buy, OkxAlgoOrderType.Trigger,
            size: 1, triggerPrice: 90000, orderPrice: 89900, triggerOrderType: OkxAlgoTriggerOrderType.Chase,
            advancedChaseParameters: [new()]));
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => client.Algo.PlaceOrderAsync(
            "BTC-USDT-SWAP", OkxTradeMode.Cross, OkxTradeOrderSide.Buy, OkxAlgoOrderType.Trigger,
            size: 1, triggerPrice: 90000, triggerOrderType: OkxAlgoTriggerOrderType.Chase,
            advancedChaseParameters: [new() { ChaseValue = -1 }]));
        await Assert.ThrowsAsync<ArgumentException>(() => client.Algo.AmendOrderAsync(
            "BTC-USDT-SWAP", algoOrderId: 1,
            advancedChaseParameters: [new()]));
        await Assert.ThrowsAsync<ArgumentException>(() => client.Algo.PlaceOrderAsync(
            "BTC-USDT-SWAP", OkxTradeMode.Cross, OkxTradeOrderSide.Buy, OkxAlgoOrderType.SmartIceberg,
            size: 100, limitOrderNumber: 20, aggressiveness: OkxAlgoSmartIcebergAggressiveness.Mid));

        Assert.Empty(server.Requests);
    }

    [Fact]
    public async Task AlgoQueries_SerializeCurrentCombinedTypeAndHistoryContracts()
    {
        using var server = CreateServer();
        var client = CreateClient(server);

        var pending = await client.Algo.GetOpenOrdersAsync(
            [OkxAlgoOrderType.Conditional, OkxAlgoOrderType.OCO],
            instrumentType: OkxInstrumentType.Swap,
            after: 700000000000000100,
            limit: 25);
        var history = await client.Algo.GetOrderHistoryAsync(
            OkxAlgoOrderType.SmartIceberg,
            algoOrderState: OkxAlgoOrderState.Canceled,
            instrumentType: OkxInstrumentType.Futures,
            limit: 10);

        Assert.True(pending.Success, pending.Error?.ToString());
        Assert.True(history.Success, history.Error?.ToString());
        Assert.Equal(2, server.Requests.Count);

        var pendingQuery = Uri.UnescapeDataString(server.Requests[0].Query);
        Assert.Contains("ordType=conditional,oco", pendingQuery);
        Assert.Contains("instType=SWAP", pendingQuery);
        Assert.Contains("after=700000000000000100", pendingQuery);
        Assert.Contains("limit=25", pendingQuery);

        var historyQuery = Uri.UnescapeDataString(server.Requests[1].Query);
        Assert.Contains("ordType=smart_iceberg", historyQuery);
        Assert.Contains("state=canceled", historyQuery);
        Assert.Contains("instType=FUTURES", historyQuery);
        Assert.Contains("limit=10", historyQuery);
    }

    [Fact]
    public async Task AlgoQueries_RejectCurrentInvalidShapesBeforeSending()
    {
        using var server = CreateServer();
        var client = CreateClient(server);

        await Assert.ThrowsAsync<ArgumentException>(() => client.Algo.GetOrderAsync());
        await Assert.ThrowsAsync<ArgumentException>(() => client.Algo.GetOrderHistoryAsync(OkxAlgoOrderType.Trigger));
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => client.Algo.GetOrderHistoryAsync(
            OkxAlgoOrderType.Trigger,
            algoOrderState: OkxAlgoOrderState.Live));
        await Assert.ThrowsAsync<ArgumentException>(() => client.Algo.GetOpenOrdersAsync(
            [OkxAlgoOrderType.Trigger, OkxAlgoOrderType.TWAP]));
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => client.Algo.GetOpenOrdersAsync(
            OkxAlgoOrderType.Trigger,
            instrumentType: OkxInstrumentType.Option));

        Assert.Empty(server.Requests);
    }

    [Fact]
    public async Task AlgoOrdersChannel_RejectsCurrentInvalidSubscriptionShapesBeforeConnecting()
    {
        using var client = new OkxWebSocketApiClient();

        await Assert.ThrowsAsync<ArgumentNullException>(() =>
            client.Algo.SubscribeToAlgoOrderUpdatesAsync(null!, OkxInstrumentType.Swap));
        await Assert.ThrowsAsync<ArgumentException>(() =>
            client.Algo.SubscribeToAlgoOrderUpdatesAsync(_ => { }, []));
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
            client.Algo.SubscribeToAlgoOrderUpdatesAsync(_ => { }, OkxInstrumentType.Option));
        await Assert.ThrowsAsync<ArgumentException>(() =>
            client.Algo.SubscribeToAlgoOrderUpdatesAsync(_ => { }, OkxInstrumentType.Spot, instrumentFamily: "BTC-USDT"));
        await Assert.ThrowsAsync<ArgumentException>(() =>
            client.Algo.SubscribeToAlgoOrderUpdatesAsync(_ => { }, OkxInstrumentType.Swap, instrumentId: " "));
    }

    private static LocalOkxRestServer CreateServer()
        => new(new Dictionary<string, string>
        {
            ["POST /api/v5/trade/order-algo"] = "{\"code\":\"0\",\"msg\":\"\",\"data\":[{\"algoId\":\"700000000000000001\",\"clOrdId\":\"\",\"algoClOrdId\":\"trigger-chase\",\"sCode\":\"0\",\"sMsg\":\"\",\"tag\":\"risk-entry\"}]}",
            ["POST /api/v5/trade/amend-algos"] = "{\"code\":\"0\",\"msg\":\"\",\"data\":[{\"algoId\":\"700000000000000001\",\"algoClOrdId\":\"trigger-chase\",\"reqId\":\"amend-1\",\"sCode\":\"0\",\"sMsg\":\"\"}]}",
            ["GET /api/v5/trade/orders-algo-pending"] = "{\"code\":\"0\",\"msg\":\"\",\"data\":[]}",
            ["GET /api/v5/trade/orders-algo-history"] = "{\"code\":\"0\",\"msg\":\"\",\"data\":[]}",
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
