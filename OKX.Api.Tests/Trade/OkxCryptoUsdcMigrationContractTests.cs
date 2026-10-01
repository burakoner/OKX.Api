using ApiSharp.Models;
using ApiSharp.Throttling;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OKX.Api.Base;
using OKX.Api.Common;
using OKX.Api.Tests.TestInfrastructure;
using OKX.Api.Trade;

namespace OKX.Api.Tests.Trade;

// Local/captured requests only. No order is sent to OKX and no account feature is activated.
public class OkxCryptoUsdcMigrationContractTests
{
    private const string ActivationRequired = "Synthetic activation-required rejection";

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task Instruments_ForwardActualIdsCodesAndAccountQuoteChoices(bool account)
    {
        var row = JObject.Parse(FixtureReader.ReadManual("Public", "ws-instruments-incremental.json"))["data"]![0]!;
        if (account) row["tradeQuoteCcyList"] = new JArray("USD", "USDC", "NEW-QUOTE");
        var endpoint = account ? "/api/v5/account/instruments" : "/api/v5/public/instruments";
        using var server = new LocalOkxRestServer(new Dictionary<string, string>
        {
            [$"GET {endpoint}"] = new JObject { ["code"] = "0", ["msg"] = "", ["data"] = new JArray(row) }.ToString(),
        });
        var client = Client(server);
        var result = account
            ? await client.Account.GetInstrumentsAsync(OkxInstrumentType.Spot, instrumentId: "BTC-USDC")
            : await client.Public.GetInstrumentsAsync(OkxInstrumentType.Spot, instrumentId: "BTC-USDC");

        Assert.True(result.Success, result.Error?.ToString());
        var instrument = Assert.Single(result.Data);
        Assert.Equal("BTC-USDC", instrument.InstrumentId);
        Assert.Equal(9000000001L, instrument.InstrumentIdCode);
        Assert.Equal("USDC", instrument.QuoteCurrency);
        Assert.Equal(3, instrument.UpcomingChanges!.Count);
        if (account) Assert.Equal(new[] { "USD", "USDC", "NEW-QUOTE" }, instrument.TradeQuoteCurrencyList);
        var request = Assert.Single(server.Requests);
        Assert.Equal("GET", request.Method);
        Assert.Equal(endpoint, request.Path);
        Assert.Equal("?instId=BTC-USDC&instType=SPOT", request.Query);
        Assert.Empty(request.Body);
        Assert.Equal(account, request.Headers.ContainsKey("OK-ACCESS-KEY"));
        Assert.Equal(account, request.Headers.ContainsKey("OK-ACCESS-SIGN"));
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task Instruments_OldIdCanReturnNoDataWithoutGeneratingANewInstrument(bool account)
    {
        var endpoint = account ? "/api/v5/account/instruments" : "/api/v5/public/instruments";
        using var server = new LocalOkxRestServer(new Dictionary<string, string>
        {
            [$"GET {endpoint}"] = "{\"code\":\"0\",\"msg\":\"\",\"data\":[]}",
        });
        var client = Client(server);
        var result = account
            ? await client.Account.GetInstrumentsAsync(OkxInstrumentType.Spot, instrumentId: "BTC-USD")
            : await client.Public.GetInstrumentsAsync(OkxInstrumentType.Spot, instrumentId: "BTC-USD");
        Assert.True(result.Success);
        Assert.Empty(result.Data);
        Assert.Contains("instId=BTC-USD&", Assert.Single(server.Requests).Query);
    }

    [Fact]
    public async Task Instruments_RetainConservativeCombinedBudgetRatherThanClaimPerTypeParity()
    {
        using var server = new LocalOkxRestServer(new Dictionary<string, string>
        {
            ["GET /api/v5/account/instruments"] = "{\"code\":\"0\",\"msg\":\"\",\"data\":[]}",
            ["GET /api/v5/public/instruments"] = "{\"code\":\"0\",\"msg\":\"\",\"data\":[]}",
        });
        var client = Client(server);
        client.Options.RateLimitingBehavior = RateLimitingBehavior.Fail;
        for (var i = 0; i < 10; i++)
        {
            Assert.True((await client.Public.GetInstrumentsAsync(OkxInstrumentType.Spot)).Success);
            Assert.True((await client.Account.GetInstrumentsAsync(OkxInstrumentType.Swap)).Success);
        }
        Assert.IsType<ClientRateLimitError>((await client.Public.GetInstrumentsAsync(OkxInstrumentType.Margin)).Error);
        Assert.IsType<ClientRateLimitError>((await client.Account.GetInstrumentsAsync(OkxInstrumentType.Futures)).Error);
        Assert.Equal(20, server.Requests.Count);
    }

    [Theory]
    [InlineData("model", null)]
    [InlineData("model", "USD")]
    [InlineData("model", "USDC")]
    [InlineData("positional", null)]
    [InlineData("positional", "USD")]
    [InlineData("positional", "USDC")]
    [InlineData("batch", null)]
    [InlineData("batch", "USD")]
    [InlineData("batch", "USDC")]
    public async Task RestPlacement_ForwardsSelectedOrOmittedQuoteWithoutInferringADefault(string overload, string? quote)
    {
        var batch = overload == "batch";
        using var server = Server(batch, Envelope(0, batch ? new[] { Item(false), Item(false) } : new[] { Item(false) }));
        var client = Client(server);
        var original = Order(quote);
        var before = JsonConvert.SerializeObject(original, SerializerOptions.WithConverters);
        if (batch)
            Assert.True((await client.Trade.PlaceOrdersAsync(new[] { original, Order("USDC", "ETH-USDC", 9000000002) })).Success);
        else if (overload == "positional")
            Assert.True((await client.Trade.PlaceOrderAsync("BTC-USDC", OkxTradeMode.Cash, OkxTradeOrderSide.Buy,
                OkxTradePositionSide.Net, OkxTradeOrderType.LimitOrder, 0.001m, price: 100m, tradeQuoteCurrency: quote)).Success);
        else
            Assert.True((await client.Trade.PlaceOrderAsync(original)).Success);

        var request = Assert.Single(server.Requests);
        Assert.Equal(batch ? "/api/v5/trade/batch-orders" : "/api/v5/trade/order", request.Path);
        Assert.Equal("POST", request.Method);
        Assert.True(request.Headers.ContainsKey("OK-ACCESS-SIGN"));
        var body = JToken.Parse(request.Body);
        var first = batch ? body[0]! : body;
        Assert.Equal("BTC-USDC", first["instId"]?.Value<string>());
        Assert.Null(first["instIdCode"]);
        AssertQuote(first, quote);
        if (batch)
        {
            Assert.Equal("ETH-USDC", body[1]!["instId"]?.Value<string>());
            AssertQuote(body[1]!, "USDC");
        }
        Assert.Equal(before, JsonConvert.SerializeObject(original, SerializerOptions.WithConverters));
    }

    [Theory]
    [InlineData(false, null)]
    [InlineData(false, "USD")]
    [InlineData(false, "USDC")]
    [InlineData(true, null)]
    [InlineData(true, "USD")]
    [InlineData(true, "USDC")]
    public async Task SocketPlacement_UsesExactNewIntegerCodeAndPreservesPerOrderQuote(bool batch, string? quote)
    {
        using var client = new QueryClient { Response = Envelope(0, batch ? new[] { Item(false), Item(false) } : new[] { Item(false) }) };
        var original = Order(quote);
        var before = JsonConvert.SerializeObject(original, SerializerOptions.WithConverters);
        if (batch)
            Assert.True((await client.Trade.PlaceOrdersAsync(new[] { original, Order("USDC", "ETH-USDC", 9000000002) })).Success);
        else
            Assert.True((await client.Trade.PlaceOrderAsync(original)).Success);

        Assert.Equal(1, client.QueryCount);
        Assert.Equal(OkxSocketEndpoint.Private, client.Endpoint);
        Assert.True(client.Authenticated);
        var command = Assert.IsType<JObject>(client.LastCommand);
        Assert.Equal(batch ? "batch-orders" : "order", command["op"]?.Value<string>());
        var args = Assert.IsType<JArray>(command["args"]);
        Assert.Equal(batch ? 2 : 1, args.Count);
        Assert.Equal(JTokenType.Integer, args[0]["instIdCode"]?.Type);
        Assert.Equal(9000000001L, args[0]["instIdCode"]?.Value<long>());
        Assert.Null(args[0]["instId"]);
        AssertQuote(args[0], quote);
        if (batch)
        {
            Assert.Equal(9000000002L, args[1]["instIdCode"]?.Value<long>());
            AssertQuote(args[1], "USDC");
        }
        Assert.Equal(before, JsonConvert.SerializeObject(original, SerializerOptions.WithConverters));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public async Task RestSingle54109_PreservesItemErrorWithoutActivationOrReplay(int code)
    {
        using var server = Server(false, Envelope(code, Item(true)));
        var result = await Client(server).Trade.PlaceOrderAsync(Order("USD"));
        Assert.False(result.Success);
        Assert.Equal(54109, result.Error?.Code);
        Assert.Equal(ActivationRequired, result.Error?.Message);
        Assert.Equal("54109", result.Data.ErrorCode);
        Assert.Single(server.Requests);
        Assert.Equal("/api/v5/trade/order", server.Requests[0].Path);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public async Task SocketSingle54109_PreservesItemOutcomeWithoutActivationOrReplay(int code)
    {
        using var client = new QueryClient { Response = Envelope(code, Item(true)) };
        var result = await client.Trade.PlaceOrderAsync(Order("USD"));
        Assert.Equal(code == 0, result.Success); // Existing code-0 WS convention still requires inspecting sCode.
        Assert.Equal("54109", result.Data.ErrorCode);
        Assert.Equal(ActivationRequired, result.Data.ErrorMessage);
        if (code != 0) Assert.Equal(54109, result.Error?.Code);
        Assert.Equal(1, client.QueryCount);
    }

    [Theory]
    [InlineData(false, false)]
    [InlineData(false, true)]
    [InlineData(true, false)]
    [InlineData(true, true)]
    public async Task MixedBatch54109_PreservesAcceptedOrderRegardlessOfItemOrderWithoutReplay(bool socket, bool rejectedFirst)
    {
        var rows = rejectedFirst ? new[] { Item(true), Item(false) } : new[] { Item(false), Item(true) };
        var orders = new[] { Order("USD"), Order("USDC", "ETH-USDC", 9000000002) };
        CallResult<IEnumerable<OkxTradeOrderPlaceResponse>> result;
        if (socket)
        {
            using var client = new QueryClient { Response = Envelope(2, rows) };
            result = await client.Trade.PlaceOrdersAsync(orders);
            Assert.Equal(1, client.QueryCount);
        }
        else
        {
            using var server = Server(true, Envelope(2, rows));
            var rest = await Client(server).Trade.PlaceOrdersAsync(orders);
            result = rest.As<IEnumerable<OkxTradeOrderPlaceResponse>>(rest.Data);
            Assert.Single(server.Requests);
            Assert.Equal("/api/v5/trade/batch-orders", server.Requests[0].Path);
        }
        Assert.False(result.Success);
        Assert.Equal(2, result.Error?.Code);
        var outcomes = result.Data.ToArray();
        Assert.Equal(2, outcomes.Length);
        Assert.Equal("54109", outcomes[rejectedFirst ? 0 : 1].ErrorCode);
        Assert.Equal(ActivationRequired, outcomes[rejectedFirst ? 0 : 1].ErrorMessage);
        Assert.Equal("0", outcomes[rejectedFirst ? 1 : 0].ErrorCode);
        Assert.Equal(101L, outcomes[rejectedFirst ? 1 : 0].OrderId);
    }

    [Fact]
    public async Task LegacyRestId_IsNotSilentlyConvertedOrRetriedOnServerRejection()
    {
        using var server = Server(false, Envelope(1, Item(true)));
        var result = await Client(server).Trade.PlaceOrderAsync(Order("USD", "BTC-USD", 123456));
        Assert.False(result.Success);
        Assert.Equal("BTC-USD", JObject.Parse(Assert.Single(server.Requests).Body)["instId"]?.Value<string>());
    }

    [Fact]
    public async Task LegacySocketCode_IsNotSilentlyConvertedOrRetriedOnServerRejection()
    {
        using var client = new QueryClient { Response = Envelope(1, Item(true)) };
        var result = await client.Trade.PlaceOrderAsync(Order("USD", "BTC-USD", 123456));
        Assert.False(result.Success);
        Assert.Equal(123456, client.LastCommand!["args"]![0]!["instIdCode"]?.Value<long>());
        Assert.Equal(1, client.QueryCount);
    }

    private static void AssertQuote(JToken row, string? quote)
    {
        if (quote is null) Assert.Null(row["tradeQuoteCcy"]);
        else
        {
            Assert.Equal(JTokenType.String, row["tradeQuoteCcy"]?.Type);
            Assert.Equal(quote, row["tradeQuoteCcy"]?.Value<string>());
        }
    }

    private static OkxTradeOrderPlaceRequest Order(string? quote, string id = "BTC-USDC", long code = 9000000001) => new()
    {
        InstrumentId = id, InstrumentIdCode = code, TradeMode = OkxTradeMode.Cash,
        OrderSide = OkxTradeOrderSide.Buy, OrderType = OkxTradeOrderType.LimitOrder,
        Size = 0.001m, Price = 100m, TradeQuoteCurrency = quote,
    };

    private static JObject Item(bool rejected) => new()
    {
        ["clOrdId"] = rejected ? "rejected" : "accepted", ["ordId"] = rejected ? "" : "101",
        ["sCode"] = rejected ? "54109" : "0", ["sMsg"] = rejected ? ActivationRequired : "",
        ["subCode"] = "", ["ts"] = "1790812800000", ["tag"] = "",
    };

    private static JObject Envelope(int code, params JObject[] items) => new()
    {
        ["code"] = code.ToString(), ["msg"] = "", ["data"] = new JArray(items),
        ["inTime"] = "1790812800000000", ["outTime"] = "1790812800000100",
    };

    private static LocalOkxRestServer Server(bool batch, JObject response) => new(new Dictionary<string, string>
    {
        [$"POST /api/v5/trade/{(batch ? "batch-orders" : "order")}"] = response.ToString(),
    });

    private static OkxRestApiClient Client(LocalOkxRestServer server) => new(new OkxRestApiOptions(new OkxApiCredentials("key", "secret", "pass"))
    {
        BaseAddress = server.BaseAddress, AutoTimestamp = false, RawResponse = true,
    });

    private sealed class QueryClient : OkxWebSocketApiClient
    {
        public JObject? Response { get; init; }
        public JObject? LastCommand { get; private set; }
        public int QueryCount { get; private set; }
        public OkxSocketEndpoint Endpoint { get; private set; }
        public bool Authenticated { get; private set; }

        internal override Task<CallResult<T>> RootQueryAsync<T>(OkxSocketEndpoint endpoint, object request, bool authenticated)
        {
            QueryCount++;
            Endpoint = endpoint;
            Authenticated = authenticated;
            LastCommand = JObject.Parse(JsonConvert.SerializeObject(request, SerializerOptions.WithConverters));
            var response = Assert.IsType<JObject>(Response!.DeepClone());
            response["id"] = LastCommand["id"]!.DeepClone();
            response["op"] = LastCommand["op"]!.DeepClone();
            Assert.True(HandleQueryResponse<T>(null!, request, response, out var result));
            return Task.FromResult(result!);
        }
    }
}
