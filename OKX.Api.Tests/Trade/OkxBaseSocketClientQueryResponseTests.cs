using Newtonsoft.Json.Linq;
using OKX.Api.Base;
using OKX.Api.Tests.TestInfrastructure;
using OKX.Api.Trade;
using System.Reflection;

namespace OKX.Api.Tests.Trade;

public class OkxBaseSocketClientQueryResponseTests
{
    [Fact]
    public void HandleQueryResponse_MatchesBatchAmendOrderRequests()
    {
        var request = new OkxSocketRequest<OkxTradeOrderAmendRequest>("1514", OkxSocketOperation.BatchAmendOrders,
        [
            new OkxTradeOrderAmendRequest
            {
                InstrumentIdCode = 101,
                OrderId = 123456789,
                NewQuantity = "2"
            }
        ]);

        var callResult = InvokeHandleQueryResponse<IEnumerable<OkxTradeOrderAmend>>(request, "Trade", "ws-batch-amend-orders.json");
        var data = Assert.IsAssignableFrom<IEnumerable<OkxTradeOrderAmend>>(((dynamic)callResult).Data);
        var item = Assert.Single(data);
        Assert.Equal(123456789, item.OrderId);
        Assert.Equal("req-batch-01", item.RequestId);
    }

    [Fact]
    public void HandleQueryResponse_MatchesBatchCancelOrderRequests()
    {
        var request = new OkxSocketRequest<OkxTradeOrderCancelRequest>("1515", OkxSocketOperation.BatchCancelOrders,
        [
            new OkxTradeOrderCancelRequest
            {
                InstrumentIdCode = 101,
                OrderId = 123456790
            }
        ]);

        var callResult = InvokeHandleQueryResponse<IEnumerable<OkxTradeOrderCancel>>(request, "Trade", "ws-batch-cancel-orders.json");
        var data = Assert.IsAssignableFrom<IEnumerable<OkxTradeOrderCancel>>(((dynamic)callResult).Data);
        var item = Assert.Single(data);
        Assert.Equal(123456790, item.OrderId);
        Assert.Equal("cancel-batch-01", item.ClientOrderId);
    }

    [Fact]
    public void HandleQueryResponse_PreservesDocumentedCoolOffOrderError()
    {
        var request = new OkxSocketRequest<OkxTradeOrderPlaceRequest>("1516", OkxSocketOperation.Order,
        [
            new OkxTradeOrderPlaceRequest
            {
                InstrumentIdCode = 101,
                TradeMode = OkxTradeMode.Cross,
                OrderSide = OkxTradeOrderSide.Buy,
                PositionSide = OkxTradePositionSide.Net,
                OrderType = OkxTradeOrderType.MarketOrder,
                Size = 1m,
                ReduceOnly = false,
                ClientOrderId = "cool-off-ws-01"
            }
        ]);

        var callResult = InvokeHandleQueryResponse<OkxTradeOrderPlaceResponse>(request, "Trade", "ws-place-order-cool-off-error.json");
        var item = Assert.IsType<OkxTradeOrderPlaceResponse>(((dynamic)callResult).Data);

        Assert.Equal("54094", item.ErrorCode);
        Assert.Equal("Order rejected. The cool-off period is active for the current instId.", item.ErrorMessage);
        Assert.Equal(string.Empty, item.SubCode);
    }

    private static object InvokeHandleQueryResponse<T>(object request, params string[] fixturePath)
    {
        var method = typeof(OkxBaseSocketClient).GetMethod("HandleQueryResponse", BindingFlags.NonPublic | BindingFlags.Instance);
        Assert.NotNull(method);

        var genericMethod = method!.MakeGenericMethod(typeof(T));
        var client = new OkxWebSocketApiClient(new OkxWebSocketApiOptions());
        var data = JObject.Parse(FixtureReader.ReadManual(fixturePath));
        object?[] args = [null, request, data, null];

        var handled = (bool)genericMethod.Invoke(client, args)!;
        Assert.True(handled);
        Assert.NotNull(args[3]);
        return args[3]!;
    }
}
