using ApiSharp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OKX.Api;
using OKX.Api.Base;
using OKX.Api.Common;
using OKX.Api.Spread;
using OKX.Api.Tests.TestInfrastructure;
using OKX.Api.Trade;
using System.Reflection;

namespace OKX.Api.Tests.Spread;

public class OkxSpreadTradeSocketContractTests
{
    [Fact]
    public void CreateSocketPlaceOrderRequest_AddsBrokerTag()
    {
        var method = typeof(OkxSpreadSocketClient).GetMethod("CreateSocketPlaceOrderRequest", BindingFlags.NonPublic | BindingFlags.Static);
        Assert.NotNull(method);

        var request = new OkxSpreadOrderPlaceRequest
        {
            SpreadId = "BTC-USDT_BTC-USDT-SWAP",
            ClientOrderId = "b15",
            OrderSide = OkxTradeOrderSide.Buy,
            OrderType = OkxSpreadOrderType.LimitOrder,
            Price = 2.15m,
            Quantity = 2m
        };

        var socketRequest = Assert.IsType<OkxSpreadOrderPlaceRequest>(method!.Invoke(null, [request]));
        Assert.False(string.IsNullOrWhiteSpace(socketRequest.Tag));

        var payload = JObject.Parse(JsonConvert.SerializeObject(socketRequest, SerializerOptions.WithConverters));
        Assert.Equal("BTC-USDT_BTC-USDT-SWAP", (string?)payload["sprdId"]);
        Assert.Equal(socketRequest.Tag, (string?)payload["tag"]);
        Assert.Equal("limit", (string?)payload["ordType"]);
    }

    [Fact]
    public void HandleQueryResponse_MatchesSpreadPlaceOrderRequests()
    {
        var request = new OkxSocketRequest<OkxSpreadOrderPlaceRequest>("1512", OkxSocketOperation.SpreadOrder,
        [
            new OkxSpreadOrderPlaceRequest
            {
                SpreadId = "BTC-USDT_BTC-USDT-SWAP",
                OrderSide = OkxTradeOrderSide.Buy,
                OrderType = OkxSpreadOrderType.LimitOrder,
                Quantity = 2m
            }
        ]);

        var callResult = InvokeHandleQueryResponse<OkxSpreadOrderPlace>(request, "Spread", "ws-spread-place-order-response.json");
        Assert.Equal(12345689, ((dynamic)callResult).Data.OrderId);
    }

    [Fact]
    public void HandleQueryResponse_MatchesSpreadAmendOrderRequests()
    {
        var request = new OkxSocketRequest<OkxSpreadOrderAmendRequest>("1513", OkxSocketOperation.SpreadAmendOrder,
        [
            new OkxSpreadOrderAmendRequest
            {
                OrderId = 2510789768709120,
                NewQuantity = "2"
            }
        ]);

        var callResult = InvokeHandleQueryResponse<OkxSpreadOrderAmend>(request, "Spread", "ws-spread-amend-order-response.json");
        Assert.Equal("b12344", ((dynamic)callResult).Data.RequestId);
    }

    [Fact]
    public void HandleQueryResponse_MatchesSpreadCancelOrderRequests()
    {
        var request = new OkxSocketRequest<OkxSpreadOrderCancelRequest>("1514", OkxSocketOperation.SpreadCancelOrder,
        [
            new OkxSpreadOrderCancelRequest
            {
                OrderId = 2510789768709120
            }
        ]);

        var callResult = InvokeHandleQueryResponse<OkxSpreadOrderCancel>(request, "Spread", "ws-spread-cancel-order-response.json");
        Assert.Equal(2510789768709120, ((dynamic)callResult).Data.OrderId);
    }

    [Fact]
    public void HandleQueryResponse_MatchesSpreadMassCancelRequests()
    {
        var request = new OkxSocketRequest<OkxSpreadMassCancelRequest>("1515", OkxSocketOperation.SpreadMassCancel,
        [
            new OkxSpreadMassCancelRequest
            {
                SpreadId = "BTC-USDT_BTC-USDT-SWAP"
            }
        ]);

        var callResult = InvokeHandleQueryResponse<OkxBooleanResponse>(request, "Spread", "ws-spread-mass-cancel-response.json");
        Assert.True(((dynamic)callResult).Data.Result);
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
