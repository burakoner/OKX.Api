using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OKX.Api.Base;
using OKX.Api.Trade;
using System.Reflection;

namespace OKX.Api.Tests.Trade;

#pragma warning disable CS0618
public class OkxTradeSocketAmendCancelRequestContractTests
{
    [Fact]
    public void SocketAmendOrderRequest_OmitsDeprecatedInstIdAndUsesInstIdCode()
    {
        var originalRequest = CreateAmendRequest(instrumentIdCode: 2021032621966107, instrumentId: "BTC-USDT");
        var socketRequest = InvokeSingleAmend(originalRequest);
        var payload = SerializeSocketPayload(OkxSocketOperation.AmendOrder, [socketRequest]);

        Assert.Equal("amend-order", payload["op"]?.Value<string>());

        var arg = Assert.Single(payload["args"]!.Values<JObject?>());
        Assert.NotNull(arg);
        Assert.Equal(2021032621966107, arg["instIdCode"]!.Value<long>());
        Assert.Null(arg["instId"]);
        Assert.Equal("BTC-USDT", originalRequest.InstrumentId);
        Assert.Null(socketRequest.InstrumentId);
    }

    [Fact]
    public void SocketBatchAmendOrderRequests_OmitDeprecatedInstIdForEveryOrder()
    {
        var payload = SerializeSocketPayload(
            OkxSocketOperation.BatchAmendOrders,
            InvokeBatchAmend(
            [
                CreateAmendRequest(instrumentIdCode: 101, instrumentId: "BTC-USDT"),
                CreateAmendRequest(instrumentIdCode: 202, instrumentId: "ETH-USDT")
            ]));

        Assert.Equal("batch-amend-orders", payload["op"]?.Value<string>());

        var args = payload["args"]!.Values<JObject?>().ToList();
        Assert.Equal(2, args.Count);
        Assert.All(args, item =>
        {
            Assert.NotNull(item);
            Assert.Null(item["instId"]);
            Assert.NotNull(item["instIdCode"]);
        });
    }

    [Fact]
    public void SocketAmendOrderRequest_RequiresInstIdCode()
    {
        var ex = Assert.Throws<TargetInvocationException>(() => InvokeSingleAmend(CreateAmendRequest(instrumentIdCode: null, instrumentId: "BTC-USDT")));

        var inner = Assert.IsType<ArgumentException>(ex.InnerException);
        Assert.Contains("InstrumentIdCode", inner.Message);
    }

    [Fact]
    public void SocketCancelOrderRequest_OmitsDeprecatedInstIdAndUsesInstIdCode()
    {
        var originalRequest = CreateCancelRequest(instrumentIdCode: 2021032621966107, instrumentId: "BTC-USDT");
        var socketRequest = InvokeSingleCancel(originalRequest);
        var payload = SerializeSocketPayload(OkxSocketOperation.CancelOrder, [socketRequest]);

        Assert.Equal("cancel-order", payload["op"]?.Value<string>());

        var arg = Assert.Single(payload["args"]!.Values<JObject?>());
        Assert.NotNull(arg);
        Assert.Equal(2021032621966107, arg["instIdCode"]!.Value<long>());
        Assert.Null(arg["instId"]);
        Assert.Equal("BTC-USDT", originalRequest.InstrumentId);
        Assert.Null(socketRequest.InstrumentId);
    }

    [Fact]
    public void SocketBatchCancelOrderRequests_UseBatchCancelOperationAndOmitDeprecatedInstId()
    {
        var payload = SerializeSocketPayload(
            OkxSocketOperation.BatchCancelOrders,
            InvokeBatchCancel(
            [
                CreateCancelRequest(instrumentIdCode: 101, instrumentId: "BTC-USDT"),
                CreateCancelRequest(instrumentIdCode: 202, instrumentId: "ETH-USDT")
            ]));

        Assert.Equal("batch-cancel-orders", payload["op"]?.Value<string>());

        var args = payload["args"]!.Values<JObject?>().ToList();
        Assert.Equal(2, args.Count);
        Assert.All(args, item =>
        {
            Assert.NotNull(item);
            Assert.Null(item["instId"]);
            Assert.NotNull(item["instIdCode"]);
        });
    }

    [Fact]
    public void SocketCancelOrderRequest_RequiresInstIdCode()
    {
        var ex = Assert.Throws<TargetInvocationException>(() => InvokeSingleCancel(CreateCancelRequest(instrumentIdCode: null, instrumentId: "BTC-USDT")));

        var inner = Assert.IsType<ArgumentException>(ex.InnerException);
        Assert.Contains("InstrumentIdCode", inner.Message);
    }

    private static OkxTradeOrderAmendRequest CreateAmendRequest(long? instrumentIdCode, string? instrumentId)
        => new()
        {
            InstrumentIdCode = instrumentIdCode,
            InstrumentId = instrumentId!,
            OrderId = 123456789,
            NewQuantity = "2"
        };

    private static OkxTradeOrderCancelRequest CreateCancelRequest(long? instrumentIdCode, string? instrumentId)
        => new()
        {
            InstrumentIdCode = instrumentIdCode,
            InstrumentId = instrumentId,
            OrderId = 123456789
        };

    private static OkxTradeOrderAmendRequest InvokeSingleAmend(OkxTradeOrderAmendRequest request)
    {
        var method = typeof(OkxTradeSocketClient).GetMethod("CreateSocketAmendOrderRequest", BindingFlags.NonPublic | BindingFlags.Static);
        Assert.NotNull(method);
        return (OkxTradeOrderAmendRequest)method!.Invoke(null, [request])!;
    }

    private static List<OkxTradeOrderAmendRequest> InvokeBatchAmend(IEnumerable<OkxTradeOrderAmendRequest> requests)
    {
        var method = typeof(OkxTradeSocketClient).GetMethod("CreateSocketAmendOrderRequests", BindingFlags.NonPublic | BindingFlags.Static);
        Assert.NotNull(method);
        return (List<OkxTradeOrderAmendRequest>)method!.Invoke(null, [requests])!;
    }

    private static OkxTradeOrderCancelRequest InvokeSingleCancel(OkxTradeOrderCancelRequest request)
    {
        var method = typeof(OkxTradeSocketClient).GetMethod("CreateSocketCancelOrderRequest", BindingFlags.NonPublic | BindingFlags.Static);
        Assert.NotNull(method);
        return (OkxTradeOrderCancelRequest)method!.Invoke(null, [request])!;
    }

    private static List<OkxTradeOrderCancelRequest> InvokeBatchCancel(IEnumerable<OkxTradeOrderCancelRequest> requests)
    {
        var method = typeof(OkxTradeSocketClient).GetMethod("CreateSocketCancelOrderRequests", BindingFlags.NonPublic | BindingFlags.Static);
        Assert.NotNull(method);
        return (List<OkxTradeOrderCancelRequest>)method!.Invoke(null, [requests])!;
    }

    private static JObject SerializeSocketPayload(OkxSocketOperation operation, IEnumerable<OkxTradeOrderAmendRequest> requests)
    {
        var socketRequest = new OkxSocketRequest<OkxTradeOrderAmendRequest>("req-01", operation, requests);
        return JObject.Parse(JsonConvert.SerializeObject(socketRequest));
    }

    private static JObject SerializeSocketPayload(OkxSocketOperation operation, IEnumerable<OkxTradeOrderCancelRequest> requests)
    {
        var socketRequest = new OkxSocketRequest<OkxTradeOrderCancelRequest>("req-01", operation, requests);
        return JObject.Parse(JsonConvert.SerializeObject(socketRequest));
    }
}
#pragma warning restore CS0618
