using System.Reflection;
using ApiSharp.Models;
using ApiSharp.WebSocket;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OKX.Api.Base;

namespace OKX.Api.Tests.Base;

public class OkxSocketChannelConnectionCountContractTests
{
    [Theory]
    [InlineData("channel-conn-count", "2", false)]
    [InlineData("channel-conn-count-error", "30", true)]
    public void OfficialExamples_ParseAndRaiseEventWithSubscriberFailureIsolation(string eventName, string count, bool error)
    {
        var payload = new JObject
        {
            ["event"] = eventName, ["channel"] = "orders", ["connCount"] = count, ["connId"] = "a4d3ae55",
        };
        var notice = payload.ToObject<OkxSocketChannelConnectionCount>(JsonSerializer.Create(SerializerOptions.WithConverters))!;
        Assert.Equal(int.Parse(count), notice.ConnectionCount);
        Assert.Equal(error, notice.IsLimitError);
        Assert.Equal("orders", notice.Channel);
        Assert.Equal("a4d3ae55", notice.ConnectionId);

        using var client = new TestableClient();
        OkxSocketChannelConnectionCount? received = null;
        client.ChannelConnectionCount += _ => throw new InvalidOperationException("subscriber failure");
        client.ChannelConnectionCount += value => received = value;
        var handler = typeof(OkxBaseSocketClient).GetMethod("HandleChannelConnectionCount", BindingFlags.Instance | BindingFlags.NonPublic)!;
        handler.Invoke(client, [new WebSocketMessageEvent(null!, payload, payload.ToString(Formatting.None), DateTime.UnixEpoch)]);

        Assert.NotNull(received);
        Assert.Equal(notice, received);
    }

    [Theory]
    [InlineData("channel-conn-count", true)]
    [InlineData("channel-conn-count-error", true)]
    [InlineData("subscribe", false)]
    [InlineData("unsubscribe", false)]
    [InlineData("error", false)]
    [InlineData("notice", false)]
    [InlineData("channel-conn-count-future", false)]
    public void GenericRouting_MatchesOnlyTheTwoConnectionCountEvents(string eventName, bool expected)
    {
        using var client = new TestableClient();
        var payload = new JObject { ["event"] = eventName, ["channel"] = "orders", ["code"] = "64008" };
        Assert.Equal(expected, client.Matches(payload, "channel-connection-count"));
        Assert.False(client.Matches(payload, "unknown-handler"));
        if (expected) Assert.False(client.Matches(payload, "service-upgrade-notice"));
        Assert.False(client.Matches(new JArray(payload), "channel-connection-count"));
    }

    [Fact]
    public void MissingCount_IsNotFabricatedAndMalformedCountDoesNotRaiseEvent()
    {
        var missing = JsonConvert.DeserializeObject<OkxSocketChannelConnectionCount>("{\"event\":\"channel-conn-count\"}", SerializerOptions.WithConverters)!;
        Assert.Null(missing.ConnectionCount);
        using var client = new TestableClient();
        var raised = false;
        client.ChannelConnectionCount += _ => raised = true;
        var payload = JObject.Parse("{\"event\":\"channel-conn-count\",\"connCount\":\"not-a-number\"}");
        var handler = typeof(OkxBaseSocketClient).GetMethod("HandleChannelConnectionCount", BindingFlags.Instance | BindingFlags.NonPublic)!;
        handler.Invoke(client, [new WebSocketMessageEvent(null!, payload, payload.ToString(Formatting.None), DateTime.UnixEpoch)]);
        Assert.False(raised);
    }

    private sealed class TestableClient : OkxWebSocketApiClient
    {
        public bool Matches(JToken message, string identifier) => base.MessageMatchesHandler(null!, message, identifier);
    }
}
