using ApiSharp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OKX.Api.Base;
using OKX.Api.Tests.TestInfrastructure;
using OKX.Api.Trade;

namespace OKX.Api.Tests.Trade;

public class OkxTradeRpiMinimumNotionalTests
{
    // Documentation-derived synthetic shapes, not a simulation of OKX's notional calculation.
    private const string Rejection = "RPI order rejected. The order value is below the minimum required for RPI orders (500 USD).";

    [Theory]
    [InlineData(false, 0, true)]
    [InlineData(false, 0, false)]
    [InlineData(true, 0, true)]
    [InlineData(true, 0, false)]
    [InlineData(false, 2, true)]
    [InlineData(false, 2, false)]
    [InlineData(true, 2, true)]
    [InlineData(true, 2, false)]
    public async Task RestBatch_PreservesRejectedAndAcceptedItems(bool amend, int code, bool rejectedFirst)
    {
        var items = new[] { Item(true), Item(false) };
        if (!rejectedFirst) Array.Reverse(items);
        using var server = Server(amend, true, Envelope(code, items).ToString());
        var client = Client(server);

        if (amend)
            AssertBatch(await client.Trade.AmendOrdersAsync([AmendRequest(), AmendRequest()]), code, rejectedFirst);
        else
            AssertBatch(await client.Trade.PlaceOrdersAsync([PlaceRequest(), PlaceRequest()]), code, rejectedFirst);

        Assert.Single(server.Requests); // No automatic retry of an accepted or rejected order.
    }

    [Theory]
    [InlineData(false, 0, true)]
    [InlineData(false, 0, false)]
    [InlineData(true, 0, true)]
    [InlineData(true, 0, false)]
    [InlineData(false, 2, true)]
    [InlineData(false, 2, false)]
    [InlineData(true, 2, true)]
    [InlineData(true, 2, false)]
    public void SocketMixedBatch_PreservesRejectedAndAcceptedItems(bool amend, int code, bool rejectedFirst)
    {
        var items = new[] { Item(true), Item(false) };
        if (!rejectedFirst) Array.Reverse(items);
        using var client = new QueryClient();
        var response = SocketEnvelope(amend, true, code, items);
        if (amend)
        {
            Assert.True(client.Handle<IEnumerable<OkxTradeOrderAmend>>(SocketRequest(amend, true, 2), response, out var result));
            AssertBatch(result!, code, rejectedFirst);
        }
        else
        {
            Assert.True(client.Handle<IEnumerable<OkxTradeOrderPlaceResponse>>(SocketRequest(amend, true, 2), response, out var result));
            AssertBatch(result!, code, rejectedFirst);
        }
    }

    [Theory]
    [InlineData(false, 0, 1)]
    [InlineData(false, 0, 2)]
    [InlineData(true, 0, 1)]
    [InlineData(true, 0, 2)]
    [InlineData(false, 1, 1)]
    [InlineData(false, 1, 2)]
    [InlineData(true, 1, 1)]
    [InlineData(true, 1, 2)]
    public async Task RestAllRejectedBatch_PreservesEveryItemEvenForOneOrder(bool amend, int code, int count)
    {
        using var server = Server(amend, true, Envelope(code, Enumerable.Range(0, count).Select(_ => Item(true)).ToArray()).ToString());
        var client = Client(server);
        if (amend)
            AssertRejectedBatch(await client.Trade.AmendOrdersAsync(Enumerable.Range(0, count).Select(_ => AmendRequest())), code, count);
        else
            AssertRejectedBatch(await client.Trade.PlaceOrdersAsync(Enumerable.Range(0, count).Select(_ => PlaceRequest())), code, count);
        Assert.Single(server.Requests);
    }

    [Theory]
    [InlineData(false, 1)]
    [InlineData(false, 2)]
    [InlineData(true, 1)]
    [InlineData(true, 2)]
    public void SocketAllRejectedBatch_PreservesEveryItem(bool amend, int count)
    {
        using var client = new QueryClient();
        var response = SocketEnvelope(amend, true, 1, Enumerable.Range(0, count).Select(_ => Item(true)).ToArray());
        if (amend)
        {
            Assert.True(client.Handle<IEnumerable<OkxTradeOrderAmend>>(SocketRequest(amend, true, count), response, out var result));
            AssertRejectedBatch(result!, 1, count);
        }
        else
        {
            Assert.True(client.Handle<IEnumerable<OkxTradeOrderPlaceResponse>>(SocketRequest(amend, true, count), response, out var result));
            AssertRejectedBatch(result!, 1, count);
        }
    }

    [Theory]
    [InlineData(false, 0, 500)]
    [InlineData(false, 1, 2000)]
    [InlineData(false, 1, 5000)]
    [InlineData(true, 0, 500)]
    [InlineData(true, 1, 2000)]
    [InlineData(true, 1, 5000)]
    public async Task RestSingle_ExposesNumeric54051AndOriginalServerMessage(bool amend, int code, int minimum)
    {
        var item = Item(true);
        var message = Rejection.Replace("500 USD", $"{minimum} USD");
        item["sMsg"] = message;
        item["subCode"] = "example-subcode";
        using var server = Server(amend, false, Envelope(code, item).ToString());
        var client = Client(server);
        CallResult result;
        if (amend)
        {
            var response = await client.Trade.AmendOrderAsync("BTC-USDT", orderId: 590909145319051111, newQuantity: 0.001m);
            Assert.Equal("54051", Assert.IsType<OkxTradeOrderAmend>(response.Data).ErrorCode);
            result = response;
        }
        else
        {
            var response = await client.Trade.PlaceOrderAsync(PlaceRequest());
            Assert.Equal("54051", Assert.IsType<OkxTradeOrderPlaceResponse>(response.Data).ErrorCode);
            result = response;
        }

        Assert.False(result.Success);
        Assert.Equal(54051, result.Error?.Code);
        Assert.Equal($"{message} (subCode: example-subcode)", result.Error?.Message);
        Assert.Equal("example-subcode", result.Error?.Data);
        Assert.Single(server.Requests);
    }

    [Theory]
    [InlineData(false, 0)]
    [InlineData(false, 1)]
    [InlineData(true, 0)]
    [InlineData(true, 1)]
    public void SocketSingle_PreservesItemAndDocumentedAggregateError(bool amend, int code)
    {
        using var client = new QueryClient();
        var item = Item(true);
        item["subCode"] = "example-subcode";
        var response = SocketEnvelope(amend, false, code, item);
        if (amend)
        {
            Assert.True(client.Handle<OkxTradeOrderAmend>(SocketRequest(amend, false), response, out var result));
            AssertSingle(result!, code);
        }
        else
        {
            Assert.True(client.Handle<OkxTradeOrderPlaceResponse>(SocketRequest(amend, false), response, out var result));
            AssertSingle(result!, code);
        }
    }

    [Theory]
    [InlineData(false, false)]
    [InlineData(false, true)]
    [InlineData(true, false)]
    [InlineData(true, true)]
    public void SocketGatewayError_RemainsNumericAndHasNoOrderData(bool amend, bool batch)
    {
        using var client = new QueryClient();
        var response = SocketEnvelope(amend, batch, 60013);
        response["msg"] = "Invalid args";
        Assert.True(client.Handle<object>(SocketRequest(amend, batch), response, out var result));
        Assert.False(result!.Success);
        Assert.Equal(60013, result.Error?.Code);
        Assert.Null(result.Data);
    }

    [Theory]
    [InlineData("id", "other-request", 1)]
    [InlineData("id", "other-request", 2)]
    [InlineData("id", "other-request", 60013)]
    [InlineData("op", "batch-amend-orders", 2)]
    [InlineData("op", "amend-order", 60013)]
    [InlineData("op", "order", 1)]
    [InlineData("op", "order", 2)]
    [InlineData("op", "order", 60013)]
    public void SocketError_NeverConsumesAnotherRequestOrOperation(string field, string value, int code)
    {
        using var client = new QueryClient();
        var response = SocketEnvelope(false, true, code, Item(true));
        response[field] = value;
        Assert.False(client.Handle<IEnumerable<OkxTradeOrderPlaceResponse>>(SocketRequest(false, true), response, out var result));
        Assert.Null(result);
    }

    [Theory]
    [InlineData(false, 0)]
    [InlineData(false, 1)]
    [InlineData(false, 2)]
    [InlineData(false, 60013)]
    [InlineData(true, 0)]
    [InlineData(true, 1)]
    [InlineData(true, 2)]
    [InlineData(true, 60013)]
    public async Task RestEmptyBatchResponse_NeverInventsOrderOutcomes(bool amend, int code)
    {
        var response = Envelope(code);
        response["msg"] = "Invalid args";
        using var server = Server(amend, true, response.ToString());
        var client = Client(server);
        if (amend)
            AssertEmpty(await client.Trade.AmendOrdersAsync([AmendRequest()]), code);
        else
            AssertEmpty(await client.Trade.PlaceOrdersAsync([PlaceRequest()]), code);
    }

    [Theory]
    [InlineData(false, false)]
    [InlineData(false, true)]
    [InlineData(true, false)]
    [InlineData(true, true)]
    public async Task RestGatewayError_IsNotReplacedByAnOrderItem(bool amend, bool batch)
    {
        var response = Envelope(60013, Item(true));
        response["msg"] = "Invalid args";
        using var server = Server(amend, batch, response.ToString());
        var client = Client(server);
        CallResult result;
        if (amend)
            result = batch ? await client.Trade.AmendOrdersAsync([AmendRequest()])
                : await client.Trade.AmendOrderAsync("BTC-USDT", orderId: 590909145319051111, newQuantity: 0.001m);
        else
            result = batch ? await client.Trade.PlaceOrdersAsync([PlaceRequest()]) : await client.Trade.PlaceOrderAsync(PlaceRequest());
        Assert.False(result.Success);
        Assert.Equal(60013, result.Error?.Code);
        Assert.Equal("Invalid args", result.Error?.Message);
        Assert.Single(server.Requests);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(60013)]
    public void SocketError_EmptyOrNonAggregatePayloadNeverReturnsInventedOrderData(int code)
    {
        using var client = new QueryClient();
        var response = SocketEnvelope(false, true, code, code == 60013 ? [Item(true)] : []);
        Assert.True(client.Handle<IEnumerable<OkxTradeOrderPlaceResponse>>(SocketRequest(false, true), response, out var result));
        Assert.False(result!.Success);
        Assert.Equal(code, result.Error?.Code);
        Assert.Null(result.Data);
    }

    [Theory]
    [InlineData(null)]
    [InlineData(false)]
    [InlineData(true)]
    public async Task RestAmend_PriceOnlyAndCancelOnFailAreForwardedWithoutInventedNotionalRules(bool? cancelOnFail)
    {
        using var server = Server(true, false, Envelope(0, Item(false)).ToString());
        var client = Client(server);
        var result = await client.Trade.AmendOrderAsync("BTC-USDT", orderId: 590909145319051111,
            newPrice: 0.01m, cancelOnFail: cancelOnFail);
        Assert.True(result.Success);
        var payload = JObject.Parse(Assert.Single(server.Requests).Body);
        Assert.Null(payload["newSz"]);
        Assert.Equal("0.01", payload["newPx"]?.Value<string>());
        Assert.Equal(cancelOnFail, payload["cxlOnFail"]?.Value<bool>());
    }

    [Theory]
    [InlineData("rpi")]
    [InlineData("elp")]
    [InlineData("limit")]
    public async Task SocketPlacement_ForwardsMakerAliasesAndNonMakerTakerAccessWithoutLocalNotionalCheck(string orderType)
    {
        using var client = new QueryClient { Response = SocketEnvelope(false, false, 0, Item(false)) };
#pragma warning disable CS0618 // Verify the officially accepted transition alias.
        var type = orderType == "rpi" ? OkxTradeOrderType.RetailPriceImprovementOrder
            : orderType == "elp" ? OkxTradeOrderType.EnhancedLiquidityProgramOrder : OkxTradeOrderType.LimitOrder;
#pragma warning restore CS0618
        var original = PlaceRequest() with { OrderType = type, RpiTakerAccess = orderType == "limit" };
        var result = await client.Trade.PlaceOrderAsync(original);
        Assert.True(result.Success);
        Assert.Equal(1, client.QueryCount);
        var payload = Assert.IsType<JObject>(Assert.Single(Assert.IsType<JArray>(client.LastCommand!["args"])));
        Assert.Equal(orderType, payload["ordType"]?.Value<string>());
        Assert.Equal("0.001", payload["sz"]?.Value<string>());
        Assert.Equal("100", payload["px"]?.Value<string>());
        Assert.Equal(orderType == "limit", payload["rpiTakerAccess"]?.Value<bool>());
        Assert.Equal(0.001m, original.Size);
        Assert.Equal(100m, original.Price);
    }

    private static void AssertEmpty<T>(RestCallResult<List<T>> result, int code)
    {
        Assert.Equal(code == 0, result.Success);
        if (code == 0) Assert.Empty(result.Data);
        else
        {
            Assert.Equal(code, result.Error?.Code);
            Assert.Null(result.Data);
        }
    }

    private static void AssertSingle<T>(CallResult<T> result, int code) where T : OkxRestApiErrorBase
    {
        // Retain legacy code=0 acknowledgement semantics: callers must always inspect sCode.
        Assert.Equal(code == 0, result.Success);
        Assert.Equal("54051", Assert.IsAssignableFrom<T>(result.Data).ErrorCode);
        Assert.Equal("example-subcode", result.Data.SubCode);
        AssertEnvelopeMetadata(result.Raw, code);
        if (code != 0)
        {
            Assert.Equal(54051, result.Error?.Code);
            Assert.Equal("example-subcode", result.Error?.Data);
            Assert.Equal($"{Rejection} (subCode: example-subcode)", result.Error?.Message);
        }
    }

    private static void AssertRejectedBatch<T>(CallResult<IEnumerable<T>> result, int code, int count) where T : OkxRestApiErrorBase
    {
        Assert.Equal(code == 0, result.Success);
        Assert.Equal(code == 0 ? null : (int?)code, result.Error?.Code);
        var items = Assert.IsAssignableFrom<IEnumerable<T>>(result.Data).ToArray();
        Assert.Equal(count, items.Length);
        Assert.All(items, item => Assert.Equal("54051", item.ErrorCode));
        AssertEnvelopeMetadata(result.Raw, code);
    }

    private static void AssertRejectedBatch<T>(RestCallResult<List<T>> result, int code, int count) where T : OkxRestApiErrorBase
        => AssertRejectedBatch(result.As<IEnumerable<T>>(result.Data!), code, count);

    private static void AssertBatch<T>(CallResult<IEnumerable<T>> result, int code, bool rejectedFirst) where T : OkxRestApiErrorBase
    {
        Assert.Equal(code == 0, result.Success);
        Assert.Equal(code == 0 ? null : (int?)code, result.Error?.Code);
        var items = Assert.IsAssignableFrom<IEnumerable<T>>(result.Data).ToArray();
        Assert.Equal(2, items.Length);
        Assert.Equal("54051", items[rejectedFirst ? 0 : 1].ErrorCode);
        Assert.Equal(Rejection, items[rejectedFirst ? 0 : 1].ErrorMessage);
        Assert.Equal("0", items[rejectedFirst ? 1 : 0].ErrorCode);
        var accepted = items[rejectedFirst ? 1 : 0];
        Assert.Equal(590909145319051111L, accepted switch
        {
            OkxTradeOrderPlaceResponse place => place.OrderId,
            OkxTradeOrderAmend amendment => amendment.OrderId,
            _ => throw new InvalidOperationException("Unexpected acknowledgement type"),
        });
        AssertEnvelopeMetadata(result.Raw, code);
    }

    private static void AssertBatch<T>(RestCallResult<List<T>> result, int code, bool rejectedFirst) where T : OkxRestApiErrorBase
        => AssertBatch(result.As<IEnumerable<T>>(result.Data!), code, rejectedFirst);

    private static JObject Item(bool rejected) => new()
    {
        ["ordId"] = rejected ? "" : "590909145319051111",
        ["clOrdId"] = rejected ? "rejected" : "accepted",
        ["reqId"] = "amend-01",
        ["ts"] = "1695190491421",
        ["sCode"] = rejected ? "54051" : "0",
        ["sMsg"] = rejected ? Rejection : "",
        ["subCode"] = "",
    };

    private static JObject Envelope(int code, params JObject[] items) => new()
    {
        ["code"] = code.ToString(), ["msg"] = "", ["data"] = new JArray(items),
        ["inTime"] = "1695190491421339", ["outTime"] = "1695190491423240",
    };

    private static void AssertEnvelopeMetadata(string? raw, int code)
    {
        var envelope = JObject.Parse(Assert.IsType<string>(raw));
        Assert.Equal(code.ToString(), envelope["code"]?.Value<string>());
        Assert.Equal("1695190491421339", envelope["inTime"]?.Value<string>());
        Assert.Equal("1695190491423240", envelope["outTime"]?.Value<string>());
    }

    private static JObject SocketEnvelope(bool amend, bool batch, int code, params JObject[] items)
    {
        var result = Envelope(code, items);
        result["id"] = "rpi-01";
        result["op"] = amend ? (batch ? "batch-amend-orders" : "amend-order") : (batch ? "batch-orders" : "order");
        return result;
    }

    private static object SocketRequest(bool amend, bool batch, int count = 1) => amend
        ? new OkxSocketRequest<OkxTradeOrderAmendRequest>("rpi-01", batch ? OkxSocketOperation.BatchAmendOrders : OkxSocketOperation.AmendOrder, Enumerable.Range(0, count).Select(_ => AmendRequest()))
        : new OkxSocketRequest<OkxTradeOrderPlaceRequest>("rpi-01", batch ? OkxSocketOperation.BatchOrders : OkxSocketOperation.Order, Enumerable.Range(0, count).Select(_ => PlaceRequest()));

    private static OkxTradeOrderPlaceRequest PlaceRequest() => new()
    {
        InstrumentId = "BTC-USDT", InstrumentIdCode = 101,
        TradeMode = OkxTradeMode.Cash, OrderSide = OkxTradeOrderSide.Buy,
        OrderType = OkxTradeOrderType.RetailPriceImprovementOrder, Size = 0.001m, Price = 100m,
    };

    private static OkxTradeOrderAmendRequest AmendRequest() => new()
    {
#pragma warning disable CS0618 // Still required by the REST endpoint.
        InstrumentId = "BTC-USDT",
#pragma warning restore CS0618
        InstrumentIdCode = 101, OrderId = 590909145319051111, NewQuantity = "0.001",
    };

    private static LocalOkxRestServer Server(bool amend, bool batch, string response)
        => new(new Dictionary<string, string>
        {
            [$"POST /api/v5/trade/{(amend ? (batch ? "amend-batch-orders" : "amend-order") : (batch ? "batch-orders" : "order"))}"] = response,
        });

    private static OkxRestApiClient Client(LocalOkxRestServer server) => new(new OkxRestApiOptions(new OkxApiCredentials("key", "secret", "pass"))
    {
        AutoTimestamp = false, BaseAddress = server.BaseAddress, RawResponse = true,
    });

    private sealed class QueryClient : OkxWebSocketApiClient
    {
        public JObject? Response { get; init; }
        public JObject? LastCommand { get; private set; }
        public int QueryCount { get; private set; }

        public bool Handle<T>(object request, JObject response, out CallResult<T>? result)
            => HandleQueryResponse(null!, request, response, out result);

        internal override Task<CallResult<T>> RootQueryAsync<T>(OkxSocketEndpoint endpoint, object request, bool authenticated)
        {
            QueryCount++;
            LastCommand = JObject.Parse(JsonConvert.SerializeObject(request, SerializerOptions.WithConverters));
            var response = Assert.IsType<JObject>(Response!.DeepClone());
            response["id"] = LastCommand["id"]!.Value<string>();
            response["op"] = LastCommand["op"]!.Value<string>();
            Assert.True(Handle<T>(request, response, out var result));
            return Task.FromResult(result!);
        }
    }
}
