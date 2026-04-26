using ApiSharp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OKX.Api.Base;
using OKX.Api.Common;
using OKX.Api.Public;
using OKX.Api.Trade;
using OKX.Api.Tests.TestInfrastructure;
using System.Reflection;

namespace OKX.Api.Tests.Trade;

public class OkxTradeEventContractContractTests
{
    [Fact]
    public void PlaceOrderRequest_SerializesEventOutcomeAndSpeedBump()
    {
        var request = new OkxTradeOrderPlaceRequest
        {
            InstrumentId = "BTC-ABOVE-DAILY-260224-1600-65000",
            TradeMode = OkxTradeMode.Isolated,
            OrderSide = OkxTradeOrderSide.Buy,
            PositionSide = OkxTradePositionSide.Net,
            OrderType = OkxTradeOrderType.LimitOrder,
            Size = 1,
            Price = 0.42m,
            SpeedBump = OkxTradeEventSpeedBump.Enabled,
            Outcome = OkxTradeEventOutcome.No,
        };

        var payload = JObject.Parse(JsonConvert.SerializeObject(request, SerializerOptions.WithConverters));

        Assert.Equal("1", payload["speedBump"]?.Value<string>());
        Assert.Equal("no", payload["outcome"]?.Value<string>());
    }

    [Fact]
    public void AmendOrderRequest_SerializesEventSpeedBump()
    {
#pragma warning disable CS0618
        var request = new OkxTradeOrderAmendRequest
        {
            InstrumentId = "BTC-ABOVE-DAILY-260224-1600-65000",
            OrderId = 123456789,
            SpeedBump = OkxTradeEventSpeedBump.Enabled,
        };
#pragma warning restore CS0618

        var payload = JObject.Parse(JsonConvert.SerializeObject(request, SerializerOptions.WithConverters));

        Assert.Equal("1", payload["speedBump"]?.Value<string>());
    }

    [Fact]
    public void ManualEventOrderDetailsFixture_ParsesOutcome()
    {
        var response = DeserializeRest<OkxTradeOrder>("Trade", "event-order-details.json");

        var item = Assert.Single(response.Data!);
        Assert.Equal(OkxInstrumentType.Events, item.InstrumentType);
        Assert.Equal(OkxTradeEventOutcome.No, item.Outcome);
        Assert.Equal(OkxOrderCategory.Delivery, item.Category);
    }

    [Fact]
    public void EstimatedPriceSocketArguments_RequireInstIdForEventContracts()
    {
        var method = typeof(OkxPublicSocketClient).GetMethod("CreateEstimatedPriceSubscriptionArguments", BindingFlags.NonPublic | BindingFlags.Static);
        Assert.NotNull(method);

        var ex = Assert.Throws<TargetInvocationException>(() => method!.Invoke(null, new object[] { new[] { new OkxSocketSymbolRequest(OkxInstrumentType.Events, null, null) } }));

        var inner = Assert.IsType<ArgumentException>(ex.InnerException);
        Assert.Contains("instId", inner.Message, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void EstimatedPriceSocketArguments_AllowInstIdForEventContracts()
    {
        var method = typeof(OkxPublicSocketClient).GetMethod("CreateEstimatedPriceSubscriptionArguments", BindingFlags.NonPublic | BindingFlags.Static);
        Assert.NotNull(method);

        var args = (List<OkxSocketRequestArgument>)method!.Invoke(null, new object[] { new[] { new OkxSocketSymbolRequest(OkxInstrumentType.Events, null, "BTC-ABOVE-DAILY-260224-1600-65000") } })!;
        var arg = Assert.Single(args);

        Assert.Equal("estimated-price", arg.Channel);
        Assert.Equal(OkxInstrumentType.Events, arg.InstrumentType);
        Assert.Equal("BTC-ABOVE-DAILY-260224-1600-65000", arg.InstrumentId);
    }

    private static OkxRestApiResponse<List<T>> DeserializeRest<T>(params string[] fixturePath) where T : class
    {
        var json = FixtureReader.ReadManual(fixturePath);
        var response = JsonConvert.DeserializeObject<OkxRestApiResponse<List<T>>>(json, SerializerOptions.WithConverters);

        Assert.NotNull(response);
        Assert.Equal(0, response.ErrorCode);
        Assert.NotNull(response.Data);
        return response;
    }
}
