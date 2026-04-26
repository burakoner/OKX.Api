using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OKX.Api.Base;
using OKX.Api.Trade;
using System.Reflection;

namespace OKX.Api.Tests.Trade;

public class OkxTradeSocketPlaceOrderRequestContractTests
{
    [Fact]
    public void SocketPlaceOrderRequest_OmitsDelistedInstIdAndUsesInstIdCode()
    {
        var originalRequest = CreateRequest(instrumentIdCode: 2021032621966107, instrumentId: "BTC-USDT");
        var socketRequest = InvokeSingle(originalRequest);
        var payload = SerializeSocketPayload(OkxSocketOperation.Order, [socketRequest]);

        Assert.Equal("order", payload["op"]?.Value<string>());

        var arg = Assert.Single(payload["args"]!.Values<JObject?>());
        Assert.NotNull(arg);
        var instIdCode = arg["instIdCode"];
        Assert.NotNull(instIdCode);
        Assert.Equal(2021032621966107, instIdCode!.Value<long>());
        Assert.Null(arg["instId"]);
        Assert.Equal("BTC-USDT", originalRequest.InstrumentId);
        Assert.Null(socketRequest.InstrumentId);
    }

    [Fact]
    public void SocketBatchPlaceOrderRequests_OmitDelistedInstIdForEveryOrder()
    {
        var socketRequests = InvokeBatch(
        [
            CreateRequest(instrumentIdCode: 101, instrumentId: "BTC-USDT"),
            CreateRequest(instrumentIdCode: 202, instrumentId: "ETH-USDT")
        ]);

        var payload = SerializeSocketPayload(OkxSocketOperation.BatchOrders, socketRequests);
        Assert.Equal("batch-orders", payload["op"]?.Value<string>());

        var args = payload["args"]!.Values<JObject?>().ToList();
        Assert.Equal(2, args.Count);
        var firstArg = args[0];
        var secondArg = args[1];

        Assert.NotNull(firstArg);
        Assert.NotNull(secondArg);

        Assert.All(args, item =>
        {
            Assert.NotNull(item);
            Assert.Null(item["instId"]);
        });

        Assert.NotNull(firstArg["instIdCode"]);
        Assert.NotNull(secondArg["instIdCode"]);
        Assert.Equal(101, firstArg["instIdCode"]!.Value<long>());
        Assert.Equal(202, secondArg["instIdCode"]!.Value<long>());
    }

    [Fact]
    public void SocketPlaceOrderRequest_RequiresInstIdCode()
    {
        var ex = Assert.Throws<TargetInvocationException>(() => InvokeSingle(CreateRequest(instrumentIdCode: null, instrumentId: "BTC-USDT")));

        var inner = Assert.IsType<ArgumentException>(ex.InnerException);
        Assert.Contains("InstrumentIdCode", inner.Message);
    }

    private static OkxTradeOrderPlaceRequest CreateRequest(long? instrumentIdCode, string? instrumentId)
        => new()
        {
            InstrumentIdCode = instrumentIdCode,
            InstrumentId = instrumentId,
            TradeMode = OkxTradeMode.Cash,
            OrderSide = OkxTradeOrderSide.Buy,
            PositionSide = OkxTradePositionSide.Net,
            OrderType = OkxTradeOrderType.MarketOrder,
            Size = 1
        };

    private static OkxTradeOrderPlaceRequest InvokeSingle(OkxTradeOrderPlaceRequest request)
    {
        var method = typeof(OkxTradeSocketClient).GetMethod("CreateSocketPlaceOrderRequest", BindingFlags.NonPublic | BindingFlags.Static);
        Assert.NotNull(method);
        return (OkxTradeOrderPlaceRequest)method!.Invoke(null, [request])!;
    }

    private static List<OkxTradeOrderPlaceRequest> InvokeBatch(IEnumerable<OkxTradeOrderPlaceRequest> requests)
    {
        var method = typeof(OkxTradeSocketClient).GetMethod("CreateSocketPlaceOrderRequests", BindingFlags.NonPublic | BindingFlags.Static);
        Assert.NotNull(method);
        return (List<OkxTradeOrderPlaceRequest>)method!.Invoke(null, [requests])!;
    }

    private static JObject SerializeSocketPayload(OkxSocketOperation operation, IEnumerable<OkxTradeOrderPlaceRequest> requests)
    {
        var socketRequest = new OkxSocketRequest<OkxTradeOrderPlaceRequest>("req-01", operation, requests);
        return JObject.Parse(JsonConvert.SerializeObject(socketRequest));
    }
}
