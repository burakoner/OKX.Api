using ApiSharp.Models;
using Newtonsoft.Json.Linq;
using OKX.Api;
using OKX.Api.Base;
using OKX.Api.Common;
using OKX.Api.Spread;
using OKX.Api.Tests.TestInfrastructure;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace OKX.Api.Tests.Spread;

public class OkxSpreadSocketContractTests
{
    [Fact]
    public void OrderUpdatesSubscription_UsesAuthenticatedBusinessSocket()
    {
        var method = typeof(OkxSpreadSocketClient).GetMethod("CreateOrderUpdatesSubscription", BindingFlags.NonPublic | BindingFlags.Static);
        Assert.NotNull(method);

        var result = method!.Invoke(null, ["BTC-USDT_BTC-USDT-SWAP"]);
        Assert.NotNull(result);

        var tuple = (ITuple)result!;
        Assert.Equal("Business", tuple[0]?.ToString());
        Assert.True(Assert.IsType<bool>(tuple[1]));

        var request = Assert.IsType<OkxSocketRequest>(tuple[2]);
        var argument = Assert.Single(request.Arguments);
        Assert.Equal("sprd-orders", argument.Channel);
        Assert.Equal("BTC-USDT_BTC-USDT-SWAP", argument.SpreadId);
    }

    [Fact]
    public void PublicOrderBookSubscription_UsesSpreadChannelAndBusinessSocket()
    {
        var method = typeof(OkxSpreadSocketClient).GetMethod("CreateOrderBookSubscription", BindingFlags.NonPublic | BindingFlags.Static);
        Assert.NotNull(method);

        var result = method!.Invoke(null, [new[] { "BTC-USDT_BTC-USDT-SWAP" }, OkxOrderBookType.OrderBook_l2_TBT]);
        Assert.NotNull(result);

        var tuple = (ITuple)result!;
        Assert.Equal("Business", tuple[0]?.ToString());
        Assert.False(Assert.IsType<bool>(tuple[1]));

        var request = Assert.IsType<OkxSocketRequest>(tuple[2]);
        var argument = Assert.Single(request.Arguments);
        Assert.Equal("sprd-books-l2-tbt", argument.Channel);
        Assert.Equal("BTC-USDT_BTC-USDT-SWAP", argument.SpreadId);
    }

    [Fact]
    public void QueryResponse_ReportsTopLevelSocketErrors()
    {
        var method = typeof(OkxBaseSocketClient).GetMethod("HandleQueryResponse", BindingFlags.NonPublic | BindingFlags.Instance);
        Assert.NotNull(method);

        var generic = method!.MakeGenericMethod(typeof(OkxBooleanResponse));
        var client = new OkxWebSocketApiClient(new OkxWebSocketApiOptions());
        var request = new OkxSocketRequest("1515", OkxSocketOperation.MassCancel, [new OkxSocketRequestArgument { Channel = "sprd-orders" }]);
        object?[] args =
        [
            null,
            request,
            JObject.Parse("""
            {
              "id": "1515",
              "op": "sprd-mass-cancel",
              "data": [],
              "code": "60013",
              "msg": "Invalid args"
            }
            """),
            null
        ];

        var handled = (bool)generic.Invoke(client, args)!;
        Assert.True(handled);

        var callResult = Assert.IsType<CallResult<OkxBooleanResponse>>(args[3]);
        Assert.False(callResult.Success);
        Assert.Contains("60013", callResult.Error!.ToString(), StringComparison.Ordinal);
    }
}
