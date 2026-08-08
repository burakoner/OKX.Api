using System.Reflection;
using ApiSharp.Models;
using ApiSharp.WebSocket;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OKX.Api.Base;
using OKX.Api.Tests.TestInfrastructure;

namespace OKX.Api.Tests.Base;

public class OkxSocketServiceUpgradeNoticeContractTests
{
    [Fact]
    public void ManualFixture_ParsesCurrentServiceUpgradeNotice()
    {
        var notice = DeserializeFixture();

        Assert.Equal("notice", notice.Event);
        Assert.Equal("64008", notice.Code);
        Assert.Equal("The connection will soon be closed for a service upgrade. Please reconnect.", notice.Message);
        Assert.Equal("a4d3ae55", notice.ConnectionId);
    }

    [Fact]
    public void GenericHandler_MatchesOnlyServiceUpgradeNotices()
    {
        using var client = new TestableOkxWebSocketApiClient();
        var payload = JObject.Parse(FixtureReader.ReadManual("Base", "ws-service-upgrade-notice.json"));

        Assert.True(client.InvokeMessageMatchesHandler(payload, "service-upgrade-notice"));

        payload["event"] = "error";
        Assert.False(client.InvokeMessageMatchesHandler(payload, "service-upgrade-notice"));

        payload["event"] = "notice";
        payload["code"] = "64009";
        Assert.False(client.InvokeMessageMatchesHandler(payload, "service-upgrade-notice"));
        Assert.False(client.InvokeMessageMatchesHandler(payload, "unknown-handler"));
    }

    [Fact]
    public void ServiceUpgradeNotice_IsRaisedAndSubscriberFailuresAreIsolated()
    {
        using var client = new TestableOkxWebSocketApiClient();
        OkxSocketServiceUpgradeNotice? received = null;
        client.ServiceUpgradeNotice += _ => throw new InvalidOperationException("subscriber failure");
        client.ServiceUpgradeNotice += notice => received = notice;

        var payload = JObject.Parse(FixtureReader.ReadManual("Base", "ws-service-upgrade-notice.json"));
        var message = new WebSocketMessageEvent(null!, payload, payload.ToString(Formatting.None), DateTime.UtcNow);
        var handler = typeof(OkxBaseSocketClient).GetMethod(
            "HandleServiceUpgradeNotice",
            BindingFlags.Instance | BindingFlags.NonPublic);

        Assert.NotNull(handler);
        handler.Invoke(client, [message]);

        Assert.NotNull(received);
        Assert.Equal("64008", received.Code);
        Assert.Equal("a4d3ae55", received.ConnectionId);
    }

    private static OkxSocketServiceUpgradeNotice DeserializeFixture()
    {
        var json = FixtureReader.ReadManual("Base", "ws-service-upgrade-notice.json");
        var notice = JsonConvert.DeserializeObject<OkxSocketServiceUpgradeNotice>(json, SerializerOptions.WithConverters);
        return Assert.IsType<OkxSocketServiceUpgradeNotice>(notice);
    }

    private sealed class TestableOkxWebSocketApiClient : OkxWebSocketApiClient
    {
        public TestableOkxWebSocketApiClient()
            : base(new OkxWebSocketApiOptions())
        {
        }

        public bool InvokeMessageMatchesHandler(JToken message, string identifier)
            => base.MessageMatchesHandler(null!, message, identifier);
    }
}
