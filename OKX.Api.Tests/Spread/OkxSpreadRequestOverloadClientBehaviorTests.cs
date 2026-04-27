using Newtonsoft.Json.Linq;
using OKX.Api.Common;
using OKX.Api.Spread;
using OKX.Api.Tests.TestInfrastructure;
using OKX.Api.Trade;

namespace OKX.Api.Tests.Spread;

public class OkxSpreadRequestOverloadClientBehaviorTests
{
    [Fact]
    public async Task PlaceOrderRequestOverload_MatchesLegacyBody()
    {
        using var server = CreateServer("/api/v5/sprd/order", "{\"code\":\"0\",\"msg\":\"\",\"data\":[{}]}");
        var client = CreateClient(server);

        await client.Spread.PlaceOrderAsync("BTC-USDT_BTC-USD-SWAP", OkxTradeOrderSide.Buy, OkxSpreadOrderType.LimitOrder, 2m, 3m, "cid");
        await client.Spread.PlaceOrderAsync(new OkxSpreadRestOrderPlaceRequest
        {
            SpreadId = "BTC-USDT_BTC-USD-SWAP",
            Side = OkxTradeOrderSide.Buy,
            Type = OkxSpreadOrderType.LimitOrder,
            Size = 2m,
            Price = 3m,
            ClientOrderId = "cid"
        });

        AssertRequestBodiesEqual(server);
    }

    [Fact]
    public async Task AmendOrderRequestOverload_MatchesLegacyBody()
    {
        using var server = CreateServer("/api/v5/sprd/amend-order", "{\"code\":\"0\",\"msg\":\"\",\"data\":[{}]}");
        var client = CreateClient(server);

        await client.Spread.AmendOrderAsync(123, "cid", "req-1", 4m, 5m);
        await client.Spread.AmendOrderAsync(new OkxSpreadRestOrderAmendRequest
        {
            OrderId = 123,
            ClientOrderId = "cid",
            RequestId = "req-1",
            NewQuantity = 4m,
            NewPrice = 5m
        });

        AssertRequestBodiesEqual(server);
    }

    [Fact]
    public async Task GetOrderArchiveRequestOverload_MatchesLegacyQuery()
    {
        using var server = CreateServer("/api/v5/sprd/orders-history-archive", "{\"code\":\"0\",\"msg\":\"\",\"data\":[{}]}");
        var client = CreateClient(server);

        await client.Spread.GetOrderArchiveAsync("spread", OkxTradeOrderType.LimitOrder, OkxTradeOrderState.Filled, 1, 2, 3, 4, 5, default, OkxInstrumentType.Swap, "BTC-USDT");
        await client.Spread.GetOrderArchiveAsync(new OkxSpreadOrderQueryRequest
        {
            SpreadId = "spread",
            Type = OkxTradeOrderType.LimitOrder,
            State = OkxTradeOrderState.Filled,
            BeginId = 1,
            EndId = 2,
            Begin = 3,
            End = 4,
            Limit = 5,
            InstrumentType = OkxInstrumentType.Swap,
            InstrumentFamily = "BTC-USDT"
        });

        AssertRequestQueriesEqual(server);
    }

    [Fact]
    public async Task GetTradesRequestOverload_MatchesLegacyQuery()
    {
        using var server = CreateServer("/api/v5/sprd/trades", "{\"code\":\"0\",\"msg\":\"\",\"data\":[{}]}");
        var client = CreateClient(server);

        await client.Spread.GetTradesAsync("spread", 11, 12, 13, 14, 15, 16, 17);
        await client.Spread.GetTradesAsync(new OkxSpreadTradeQueryRequest
        {
            SpreadId = "spread",
            TradeId = 11,
            OrderId = 12,
            BeginId = 13,
            EndId = 14,
            Begin = 15,
            End = 16,
            Limit = 17
        });

        AssertRequestQueriesEqual(server);
    }

    [Fact]
    public async Task GetCandlestickHistoryRequestOverload_MatchesLegacyQuery()
    {
        using var server = CreateServer("/api/v5/market/sprd-history-candles", "{\"code\":\"0\",\"msg\":\"\",\"data\":[[]]}");
        var client = CreateClient(server);

        await client.Spread.GetCandlesticksHistoryAsync("spread", "1H", 1, 2, 3);
        await client.Spread.GetCandlesticksHistoryAsync(new OkxSpreadCandlestickRequest
        {
            SpreadId = "spread",
            Period = "1H",
            After = 1,
            Before = 2,
            Limit = 3
        });

        AssertRequestQueriesEqual(server);
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
