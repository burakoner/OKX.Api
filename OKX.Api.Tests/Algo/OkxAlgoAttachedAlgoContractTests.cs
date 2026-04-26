using ApiSharp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OKX.Api.Algo;
using OKX.Api.Tests.TestInfrastructure;

namespace OKX.Api.Tests.Algo;

public class OkxAlgoAttachedAlgoContractTests
{
    [Fact]
    public void PlaceAttachedAlgoRequests_SerializeTrailingStopFields()
    {
        var payload = Serialize(
        [
            new OkxAlgoAttachedAlgoPlaceRequest
            {
                AttachedAlgoClientOrderId = "trail-ratio",
                CallbackRatio = "0.05",
                ActivePrice = "90500"
            },
            new OkxAlgoAttachedAlgoPlaceRequest
            {
                AttachedAlgoClientOrderId = "trail-spread",
                CallbackSpread = "120.5"
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
    public void AmendAttachedAlgoRequests_SerializeCurrentNestedFields()
    {
        var payload = Serialize(
        [
            new OkxAlgoAttachedAlgoAmendRequest
            {
                NewTakeProfitTriggerRatio = "0.25",
                NewStopLossTriggerRatio = "0.15",
                NewCallbackRatio = "0.05",
                NewActivePrice = "91000"
            },
            new OkxAlgoAttachedAlgoAmendRequest
            {
                NewCallbackSpread = "80"
            }
        ]);

        var ratioOrder = payload[0]!;
        Assert.Equal("0.25", ratioOrder["newTpTriggerRatio"]?.Value<string>());
        Assert.Equal("0.15", ratioOrder["newSlTriggerRatio"]?.Value<string>());
        Assert.Equal("0.05", ratioOrder["newCallbackRatio"]?.Value<string>());
        Assert.Equal("91000", ratioOrder["newActivePx"]?.Value<string>());

        var spreadOrder = payload[1]!;
        Assert.Null(spreadOrder["newCallbackRatio"]);
        Assert.Equal("80", spreadOrder["newCallbackSpread"]?.Value<string>());
        Assert.Null(spreadOrder["newActivePx"]);
    }

    [Fact]
    public void AttachedAlgoResponseFixture_ParsesRatiosAndTrailingStopFields()
    {
        var json = FixtureReader.ReadManual("Algo", "attached-algo-trailing-stop-orders.json");
        var response = JsonConvert.DeserializeObject<List<OkxAlgoAttachedAlgoOrder>>(json, SerializerOptions.WithConverters);

        Assert.NotNull(response);
        Assert.Equal(2, response!.Count);

        Assert.Equal("0.30", response[0].TakeProfitTriggerRatio);
        Assert.Equal("0.15", response[0].StopLossTriggerRatio);
        Assert.Equal("0.05", response[0].CallbackRatio);
        Assert.Equal(string.Empty, response[0].CallbackSpread);
        Assert.Equal("90500", response[0].ActivePrice);

        Assert.Equal(string.Empty, response[1].TakeProfitTriggerRatio);
        Assert.Equal(string.Empty, response[1].StopLossTriggerRatio);
        Assert.Equal(string.Empty, response[1].CallbackRatio);
        Assert.Equal("120.5", response[1].CallbackSpread);
        Assert.Equal(string.Empty, response[1].ActivePrice);
    }

    private static JArray Serialize(IEnumerable<OkxAlgoAttachedAlgoPlaceRequest> requests)
        => JArray.Parse(JsonConvert.SerializeObject(requests, SerializerOptions.WithConverters));

    private static JArray Serialize(IEnumerable<OkxAlgoAttachedAlgoAmendRequest> requests)
        => JArray.Parse(JsonConvert.SerializeObject(requests, SerializerOptions.WithConverters));
}
