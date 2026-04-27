using Newtonsoft.Json.Linq;
using OKX.Api.Account;
using OKX.Api.Common;
using OKX.Api.Trade;
using OKX.Api.Tests.TestInfrastructure;

namespace OKX.Api.Tests.Trade;

public class OkxTradeRequestOverloadClientBehaviorTests
{
    [Fact]
    public async Task ClosePositionRequestOverload_MatchesLegacyBody()
    {
        using var server = CreateServer("/api/v5/trade/close-position", "{\"code\":\"0\",\"msg\":\"\",\"data\":[{}]}");
        var client = CreateClient(server);

        await client.Trade.ClosePositionAsync("BTC-USDT-SWAP", OkxAccountMarginMode.Cross, OkxTradePositionSide.Long, "BTC", true, "legacy");
        await client.Trade.ClosePositionAsync(new OkxTradeClosePositionRequest
        {
            InstrumentId = "BTC-USDT-SWAP",
            MarginMode = OkxAccountMarginMode.Cross,
            PositionSide = OkxTradePositionSide.Long,
            Currency = "BTC",
            AutoCancel = true,
            ClientOrderId = "legacy"
        });

        AssertRequestBodiesEqual(server);
    }

    [Fact]
    public async Task GetOpenOrdersRequestOverload_MatchesLegacyQuery()
    {
        using var server = CreateServer("/api/v5/trade/orders-pending", "{\"code\":\"0\",\"msg\":\"\",\"data\":[{}]}");
        var client = CreateClient(server);

        await client.Trade.GetOpenOrdersAsync(OkxInstrumentType.Swap, "BTC-USDT-SWAP", "BTC-USDT", OkxTradeOrderType.LimitOrder, OkxTradeOrderState.Live, 1, 2, 3);
        await client.Trade.GetOpenOrdersAsync(new OkxTradeOpenOrdersRequest
        {
            InstrumentType = OkxInstrumentType.Swap,
            InstrumentId = "BTC-USDT-SWAP",
            InstrumentFamily = "BTC-USDT",
            OrderType = OkxTradeOrderType.LimitOrder,
            State = OkxTradeOrderState.Live,
            After = 1,
            Before = 2,
            Limit = 3
        });

        AssertRequestQueriesEqual(server);
    }

    [Fact]
    public async Task GetOrderHistoryRequestOverload_MatchesLegacyQuery()
    {
        using var server = CreateServer("/api/v5/trade/orders-history", "{\"code\":\"0\",\"msg\":\"\",\"data\":[{}]}");
        var client = CreateClient(server);

        await client.Trade.GetOrderHistoryAsync(OkxInstrumentType.Swap, "BTC-USDT-SWAP", "BTC-USDT", OkxTradeOrderType.LimitOrder, OkxTradeOrderState.Filled, OkxOrderCategory.TWAP, 1, 2, 3, 4, 5);
        await client.Trade.GetOrderHistoryAsync(new OkxTradeOrderQueryRequest
        {
            InstrumentType = OkxInstrumentType.Swap,
            InstrumentId = "BTC-USDT-SWAP",
            InstrumentFamily = "BTC-USDT",
            OrderType = OkxTradeOrderType.LimitOrder,
            State = OkxTradeOrderState.Filled,
            Category = OkxOrderCategory.TWAP,
            After = 1,
            Before = 2,
            Begin = 3,
            End = 4,
            Limit = 5
        });

        AssertRequestQueriesEqual(server);
    }

    [Fact]
    public async Task GetTradesHistoryRequestOverload_MatchesLegacyQuery()
    {
        using var server = CreateServer("/api/v5/trade/fills-history", "{\"code\":\"0\",\"msg\":\"\",\"data\":[{}]}");
        var client = CreateClient(server);

        await client.Trade.GetTradesHistoryAsync(OkxInstrumentType.Swap, "BTC-USDT-SWAP", "BTC-USDT", 1001, OkxAccountBillSubType.OpenLong, 1, 2, 3, 4, 5);
        await client.Trade.GetTradesHistoryAsync(new OkxTradeTransactionQueryRequest
        {
            InstrumentType = OkxInstrumentType.Swap,
            InstrumentId = "BTC-USDT-SWAP",
            InstrumentFamily = "BTC-USDT",
            OrderId = 1001,
            TransactionType = OkxAccountBillSubType.OpenLong,
            After = 1,
            Before = 2,
            Begin = 3,
            End = 4,
            Limit = 5
        });

        AssertRequestQueriesEqual(server);
    }

    [Fact]
    public async Task OrderPrecheckRequestOverload_MatchesLegacyBody()
    {
        using var server = CreateServer("/api/v5/trade/order-precheck", "{\"code\":\"0\",\"msg\":\"\",\"data\":[{}]}");
        var client = CreateClient(server);

        var attached = new[]
        {
            new OkxTradeOrderPlaceRequestAttachedAlgo
            {
                TakeProfitTriggerPrice = 100m,
                TakeProfitOrderPrice = -1m
            }
        };

        await client.Trade.OrderPrecheckAsync("BTC-USDT", OkxTradeMode.Cash, OkxTradeOrderSide.Buy, OkxTradeOrderType.LimitOrder, 1m, 2m, false, OkxTradePositionSide.Net, OkxTradeQuantityType.QuoteCurrency, null, attached);
        await client.Trade.OrderPrecheckAsync(new OkxTradeOrderPrecheckRequest
        {
            InstrumentId = "BTC-USDT",
            TradeMode = OkxTradeMode.Cash,
            OrderSide = OkxTradeOrderSide.Buy,
            OrderType = OkxTradeOrderType.LimitOrder,
            Size = 1m,
            Price = 2m,
            ReduceOnly = false,
            PositionSide = OkxTradePositionSide.Net,
            QuantityType = OkxTradeQuantityType.QuoteCurrency,
            AttachedAlgoOrders = attached
        });

        AssertRequestBodiesEqual(server);
    }

    private static void AssertRequestBodiesEqual(LocalOkxRestServer server)
    {
        Assert.Equal(2, server.Requests.Count);
        Assert.Equal(server.Requests[0].Path, server.Requests[1].Path);
        Assert.True(JToken.DeepEquals(JToken.Parse(server.Requests[0].Body), JToken.Parse(server.Requests[1].Body)));
    }

    private static void AssertRequestQueriesEqual(LocalOkxRestServer server)
    {
        Assert.Equal(2, server.Requests.Count);
        Assert.Equal(server.Requests[0].Path, server.Requests[1].Path);
        Assert.Equal(server.Requests[0].Query, server.Requests[1].Query);
    }

    private static LocalOkxRestServer CreateServer(string path, string response)
        => new(new Dictionary<string, string>
        {
            [$"GET {path}"] = response,
            [$"POST {path}"] = response,
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
