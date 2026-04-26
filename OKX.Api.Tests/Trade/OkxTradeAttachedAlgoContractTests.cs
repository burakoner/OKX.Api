using ApiSharp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OKX.Api.Tests.TestInfrastructure;
using OKX.Api.Trade;

namespace OKX.Api.Tests.Trade;

public class OkxTradeAttachedAlgoContractTests
{
    [Fact]
    public void PlaceAttachedAlgoRequests_SerializeTrailingStopFields()
    {
        var payload = Serialize(
        [
            new OkxTradeOrderPlaceRequestAttachedAlgo
            {
                AttachedAlgoClientOrderId = "trail-ratio",
                CallbackRatio = 0.05m,
                ActivePrice = 90500m
            },
            new OkxTradeOrderPlaceRequestAttachedAlgo
            {
                AttachedAlgoClientOrderId = "trail-spread",
                CallbackSpread = 120.5m
            }
        ]);

        var ratioOrder = payload[0]!;
        Assert.Equal("trail-ratio", ratioOrder["attachAlgoClOrdId"]?.Value<string>());
        Assert.Equal("0.05", ratioOrder["callbackRatio"]?.Value<string>());
        Assert.Null(ratioOrder["callbackSpread"]);
        Assert.Equal("90500", ratioOrder["activePx"]?.Value<string>());

        var spreadOrder = payload[1]!;
        Assert.Equal("trail-spread", spreadOrder["attachAlgoClOrdId"]?.Value<string>());
        Assert.Null(spreadOrder["callbackRatio"]);
        Assert.Equal("120.5", spreadOrder["callbackSpread"]?.Value<string>());
        Assert.Null(spreadOrder["activePx"]);
    }

    [Fact]
    public void PlaceAttachedAlgoRequests_SerializeExistingStopLossFields()
    {
        var payload = Serialize(
        [
            new OkxTradeOrderPlaceRequestAttachedAlgo
            {
                StopLossTriggerPrice = 85000m,
                StopLossOrderPrice = "-1",
                AmendPriceOnTriggerType = true
            }
        ]);

        var order = payload[0]!;
        Assert.Equal("85000", order["slTriggerPx"]?.Value<string>());
        Assert.Equal("-1", order["slOrdPx"]?.Value<string>());
        Assert.Equal("1", order["amendPxOnTriggerType"]?.Value<string>());
    }

    [Fact]
    public void AmendAttachedAlgoRequests_SerializeTrailingStopFields()
    {
        var payload = Serialize(
        [
            new OkxTradeOrderAmendRequestAttachedAlgo
            {
                AttachedAlgoId = "123",
                NewCallbackRatio = "0.05",
                NewActivePrice = "91000"
            },
            new OkxTradeOrderAmendRequestAttachedAlgo
            {
                AttachedAlgoClientOrderId = "trail-spread",
                NewCallbackSpread = "80"
            }
        ]);

        var ratioOrder = payload[0]!;
        Assert.Equal("123", ratioOrder["attachAlgoId"]?.Value<string>());
        Assert.Equal("0.05", ratioOrder["newCallbackRatio"]?.Value<string>());
        Assert.Null(ratioOrder["newCallbackSpread"]);
        Assert.Equal("91000", ratioOrder["newActivePx"]?.Value<string>());

        var spreadOrder = payload[1]!;
        Assert.Equal("trail-spread", spreadOrder["attachAlgoClOrdId"]?.Value<string>());
        Assert.Null(spreadOrder["newCallbackRatio"]);
        Assert.Equal("80", spreadOrder["newCallbackSpread"]?.Value<string>());
        Assert.Null(spreadOrder["newActivePx"]);
    }

    [Fact]
    public void AttachedAlgoResponseFixture_ParsesTrailingStopFields()
    {
        var json = FixtureReader.ReadManual("Trade", "attached-algo-trailing-stop-orders.json");
        var response = JsonConvert.DeserializeObject<List<OkxTradeOrderAttachedAlgoOrder>>(json, SerializerOptions.WithConverters);

        Assert.NotNull(response);
        Assert.Equal(2, response!.Count);

        Assert.Equal("0.05", response[0].CallbackRatio);
        Assert.Equal(string.Empty, response[0].CallbackSpread);
        Assert.Equal("90500", response[0].ActivePrice);
        Assert.True(response[0].AmendPriceOnTriggerType);

        Assert.Equal(string.Empty, response[1].CallbackRatio);
        Assert.Equal("120.5", response[1].CallbackSpread);
        Assert.Equal(string.Empty, response[1].ActivePrice);
        Assert.False(response[1].AmendPriceOnTriggerType);
    }

    private static JArray Serialize(IEnumerable<OkxTradeOrderPlaceRequestAttachedAlgo> requests)
        => JArray.Parse(JsonConvert.SerializeObject(requests, SerializerOptions.WithConverters));

    private static JArray Serialize(IEnumerable<OkxTradeOrderAmendRequestAttachedAlgo> requests)
        => JArray.Parse(JsonConvert.SerializeObject(requests, SerializerOptions.WithConverters));
}
