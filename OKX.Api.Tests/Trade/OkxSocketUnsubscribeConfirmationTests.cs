using System.Net;
using System.Net.Sockets;
using ApiSharp;
using ApiSharp.Models;
using ApiSharp.WebSocket;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OKX.Api.Base;
using OKX.Api.Common;

namespace OKX.Api.Tests.Trade;

// Native SDK CloseAsync/public unsubscribe on loopback sockets; ACKs are scripted, never from OKX.
public class OkxSocketUnsubscribeConfirmationTests(OkxSocketUnsubscribeConfirmationTests.SocketServer server)
    : IClassFixture<OkxSocketUnsubscribeConfirmationTests.SocketServer>
{
    [Theory]
    [InlineData("complete", true)]
    [InlineData("reversedDuplicate", true)]
    [InlineData("partial", false)]
    [InlineData("rejected", false)]
    [InlineData("partialRejected", false)]
    [InlineData("foreignId", false)]
    [InlineData("noResponse", false)]
    [InlineData("wrongOperation", false)]
    public async Task ConfirmedClose_PreservesServerOutcomeAndClosesLocallyOnce(string replies, bool confirmed)
    {
        using var client = new TestClient();
        JObject? terminal = null;
        using var socket = await server.Open(client, (request, receive) =>
        {
            Assert.Equal(OkxSocketOperation.Unsubscribe, request.Operation);
            Assert.Matches("^[A-Za-z0-9]{32}$", request.RequestId!);
            var spot = Ack(request, OkxInstrumentType.Spot);
            var option = Ack(request, OkxInstrumentType.Option);
            switch (replies)
            {
                case "complete":
                    Assert.False(receive(spot));
                    Assert.True(receive(terminal = option));
                    break;
                case "reversedDuplicate":
                    Assert.False(receive(option));
                    Assert.False(receive(option));
                    Assert.True(receive(terminal = spot));
                    break;
                case "partial": Assert.False(receive(spot)); break;
                case "rejected": Assert.True(receive(terminal = Error(request))); break;
                case "partialRejected":
                    Assert.False(receive(spot));
                    Assert.True(receive(terminal = Error(request)));
                    break;
                case "foreignId":
                    spot["id"] = "ForeignRequest";
                    Assert.False(receive(spot));
                    break;
                case "wrongOperation":
                    spot["event"] = "subscribe";
                    Assert.False(receive(spot));
                    break;
            }
            return Task.CompletedTask; // Unanswered wait ends, equivalent to timeout/closed-connection completion.
        });
        var target = Add(socket.Connection, 801, OkxInstrumentType.Spot, OkxInstrumentType.Option);
        Add(socket.Connection, 802, OkxInstrumentType.Margin); // Preserve a shared open connection.
        var original = JsonConvert.SerializeObject(target.Request, SerializerOptions.WithConverters);

        var result = await client.UnsubscribeWithConfirmationAsync(Update(socket.Connection, target));

        Assert.Equal(confirmed, result.Success);
        Assert.Equal(801, result.Data.SubscriptionId);
        Assert.True(result.Data.LocalClosed);
        Assert.Equal(confirmed, result.Data.ServerConfirmed);
        Assert.Null(socket.Connection.GetSubscription(target.Id));
        Assert.True(socket.Connection.Connected);
        Assert.Single(socket.Connection.Sent);
        Assert.Equal(original, JsonConvert.SerializeObject(target.Request, SerializerOptions.WithConverters));
        Assert.Equal(terminal?.ToString(), result.Raw);
        if (replies is "rejected" or "partialRejected") Assert.Equal(60012, result.Error?.Code);
        else if (!confirmed) Assert.Contains("uncertain", result.Error!.Message);
        // A retained handler must neither consume late replies nor mutate the returned snapshot.
        Assert.False(socket.Connection.Receivers[0](Ack(socket.Connection.Sent[0], OkxInstrumentType.Option)));
        Assert.Equal(confirmed, result.Data.ServerConfirmed);
    }

    [Fact]
    public async Task GeneralChannel_UsesIdlessExactAllArgumentConfirmation()
    {
        using var client = new TestClient();
        using var socket = await server.Open(client, (request, receive) =>
        {
            Assert.Null(request.RequestId);
            Assert.Equal(2, request.Arguments.Count);
            foreach (var id in new[] { "BTC-USDT", "ETH-USDT" })
                Assert.Equal(id == "ETH-USDT", receive(new JObject
                {
                    ["event"] = "unsubscribe", ["arg"] = new JObject { ["channel"] = "tickers", ["instId"] = id },
                    ["connId"] = "unit-connection",
                }));
            return Task.CompletedTask;
        });
        var target = WebSocketSubscription.CreateForRequest(803, new OkxSocketRequest(OkxSocketOperation.Subscribe,
            new[] { "BTC-USDT", "ETH-USDT" }.Select(id => new OkxSocketRequestArgument { Channel = "tickers", InstrumentId = id })),
            true, false, _ => { });
        target.Confirmed = true;
        Assert.True(socket.Connection.AddSubscription(target));
        Add(socket.Connection, 802, OkxInstrumentType.Margin);

        var result = await client.UnsubscribeWithConfirmationAsync(Update(socket.Connection, target));

        Assert.True(result.Success);
        Assert.True(result.Data.LocalClosed);
        Assert.True(result.Data.ServerConfirmed);
        Assert.Single(socket.Connection.Sent);
    }

    [Theory]
    [InlineData(false, false)]
    [InlineData(false, true)]
    [InlineData(true, false)]
    [InlineData(true, true)]
    public async Task LegacyPublicClose_RetainsLocalOnlySemantics(bool byId, bool rejected)
    {
        using var client = new TestClient();
        using var socket = await server.Open(client, (request, receive) =>
        {
            Assert.Equal(rejected, receive(rejected ? Error(request) : Ack(request, OkxInstrumentType.Spot)));
            return Task.CompletedTask;
        });
        var target = Add(socket.Connection, 801, OkxInstrumentType.Spot, OkxInstrumentType.Option);
        Add(socket.Connection, 802, OkxInstrumentType.Margin);

        if (byId) Assert.True(await client.UnsubscribeAsync(target.Id));
        else await client.UnsubscribeAsync(Update(socket.Connection, target));

        Assert.True(target.Closed);
        Assert.Null(socket.Connection.GetSubscription(target.Id));
        Assert.True(socket.Connection.Connected);
        Assert.Single(socket.Connection.Sent);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task NoServerWait_DoesNotInventConfirmation(bool connected)
    {
        using var client = new TestClient();
        using var socket = connected ? await server.Open(client, Complete) : server.Unconnected(client, Complete);
        var target = Add(socket.Connection, 801, OkxInstrumentType.Spot);
        target.Confirmed = !connected; // Open but unconfirmed, or previously confirmed on an unopened/closed transport.
        Add(socket.Connection, 802, OkxInstrumentType.Margin);

        var result = await client.UnsubscribeWithConfirmationAsync(Update(socket.Connection, target));

        Assert.False(result.Success);
        Assert.True(result.Data.LocalClosed);
        Assert.False(result.Data.ServerConfirmed);
        Assert.Null(result.Raw);
        Assert.Contains("uncertain", result.Error!.Message);
        Assert.Empty(socket.Connection.Sent);
    }

    [Fact]
    public async Task ForeignClientAndInternalSubscription_AreNotClosed()
    {
        using var owner = new TestClient();
        using var other = new TestClient();
        using var socket = await server.Open(owner, Complete);
        var target = Add(socket.Connection, 801, OkxInstrumentType.Spot);
        var foreign = await other.UnsubscribeWithConfirmationAsync(Update(socket.Connection, target));
        Assert.False(foreign.Success);
        Assert.False(foreign.Data.LocalClosed);
        Assert.False(foreign.Data.ServerConfirmed);
        Assert.IsType<InvalidOperationError>(foreign.Error);
        Assert.Same(target, socket.Connection.GetSubscription(target.Id));
        target.UserSubscription = false;
        var internalResult = await owner.UnsubscribeWithConfirmationAsync(Update(socket.Connection, target));
        Assert.False(internalResult.Success);
        Assert.False(target.Closed);
        Assert.Empty(socket.Connection.Sent);
    }

    [Fact]
    public async Task RepeatedClose_DoesNotResendOrReusePreviousConfirmation()
    {
        using var client = new TestClient();
        using var socket = await server.Open(client, Complete);
        var target = Add(socket.Connection, 801, OkxInstrumentType.Spot);
        Add(socket.Connection, 802, OkxInstrumentType.Margin);
        var update = Update(socket.Connection, target);

        var first = await client.UnsubscribeWithConfirmationAsync(update);
        var second = await client.UnsubscribeWithConfirmationAsync(update);

        Assert.True(first.Success);
        Assert.False(second.Success);
        Assert.True(second.Data.LocalClosed);
        Assert.False(second.Data.ServerConfirmed);
        Assert.Null(second.Raw);
        Assert.True(first.Data.ServerConfirmed);
        Assert.Single(socket.Connection.Sent);
    }

    [Fact]
    public async Task ConcurrentDifferentSubscriptions_HaveIndependentNumericRawResults()
    {
        using var client = new TestClient();
        var gates = new[] { new TaskCompletionSource(), new TaskCompletionSource() };
        using var socket = await server.Open(client, async (request, receive) =>
        {
            var spot = request.Arguments.Single().InstrumentType == OkxInstrumentType.Spot;
            await gates[spot ? 0 : 1].Task;
            Assert.True(receive(spot ? Ack(request, OkxInstrumentType.Spot) : Error(request)));
        });
        var spot = Add(socket.Connection, 801, OkxInstrumentType.Spot);
        var option = Add(socket.Connection, 803, OkxInstrumentType.Option);
        Add(socket.Connection, 802, OkxInstrumentType.Margin);
        var first = client.UnsubscribeWithConfirmationAsync(Update(socket.Connection, spot));
        var second = client.UnsubscribeWithConfirmationAsync(Update(socket.Connection, option));

        gates[1].SetResult();
        var rejected = await second.WaitAsync(TimeSpan.FromSeconds(3));
        Assert.False(first.IsCompleted);
        gates[0].SetResult();
        var accepted = await first.WaitAsync(TimeSpan.FromSeconds(3));

        Assert.True(accepted.Success);
        Assert.Equal(801, accepted.Data.SubscriptionId);
        Assert.True(accepted.Data.LocalClosed);
        Assert.True(accepted.Data.ServerConfirmed);
        Assert.False(rejected.Success);
        Assert.Equal(803, rejected.Data.SubscriptionId);
        Assert.True(rejected.Data.LocalClosed);
        Assert.False(rejected.Data.ServerConfirmed);
        Assert.Equal(60012, rejected.Error?.Code);
        Assert.NotEqual(JObject.Parse(accepted.Raw!)["id"]!.Value<string>(), JObject.Parse(rejected.Raw!)["id"]!.Value<string>());
        Assert.Equal(2, socket.Connection.Sent.Count);
    }

    [Fact]
    public async Task ConcurrentSameSubscription_RejectsSecondCallWithoutAnotherCloseOrSend()
    {
        using var client = new TestClient();
        var gate = new TaskCompletionSource();
        using var socket = await server.Open(client, async (request, receive) =>
        {
            await gate.Task;
            Assert.True(receive(Ack(request, OkxInstrumentType.Spot)));
        });
        var target = Add(socket.Connection, 801, OkxInstrumentType.Spot);
        Add(socket.Connection, 802, OkxInstrumentType.Margin);
        var update = Update(socket.Connection, target);
        var pending = client.UnsubscribeWithConfirmationAsync(update);

        var duplicate = await client.UnsubscribeWithConfirmationAsync(update);
        Assert.False(duplicate.Success);
        Assert.IsType<InvalidOperationError>(duplicate.Error);
        Assert.False(duplicate.Data.ServerConfirmed);
        Assert.False(duplicate.Data.LocalClosed); // Closed flag alone precedes removal while the first wait is pending.
        Assert.Same(target, socket.Connection.GetSubscription(target.Id));
        Assert.Single(socket.Connection.Sent);
        gate.SetResult();
        Assert.True((await pending.WaitAsync(TimeSpan.FromSeconds(3))).Success);
    }

    [Fact]
    public async Task ThrowingSend_PropagatesAndReleasesPerCallCapture()
    {
        using var client = new TestClient();
        var calls = 0;
        using var socket = await server.Open(client, (request, receive) =>
        {
            if (++calls == 1) throw new InvalidOperationException("Synthetic send failure");
            return Complete(request, receive);
        });
        var target = Add(socket.Connection, 801, OkxInstrumentType.Spot);
        Add(socket.Connection, 802, OkxInstrumentType.Margin);
        var update = Update(socket.Connection, target);
        await Assert.ThrowsAsync<InvalidOperationException>(() => client.UnsubscribeWithConfirmationAsync(update));
        Assert.True(target.Closed);
        Assert.Same(target, socket.Connection.GetSubscription(target.Id)); // Existing SDK exception path, not normal removal.

        var markedOnly = await client.UnsubscribeWithConfirmationAsync(update);
        Assert.False(markedOnly.Success);
        Assert.False(markedOnly.Data.LocalClosed);
        Assert.False(markedOnly.Data.ServerConfirmed);
        Assert.Single(socket.Connection.Sent);

        target.Closed = false; // Restore only the local test fixture, not an automatic wrapper retry/repair.
        var next = await client.UnsubscribeWithConfirmationAsync(update);
        Assert.True(next.Success);
        Assert.Equal(2, calls); // An unreleased in-progress guard would prevent this explicit second operation.
    }

    [Fact]
    public async Task NullSubscription_IsRejectedBeforeSdkAccess()
    {
        using var client = new TestClient();
        await Assert.ThrowsAsync<ArgumentNullException>(() => client.UnsubscribeWithConfirmationAsync(null!));
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task NativePendingDispatch_PreservesAcknowledgementsAndNumericErrors(bool accepted)
    {
        using var client = new TestClient();
        using var socket = await server.Open(client, Complete, nativeWait: true);
        var target = Add(socket.Connection, 801, OkxInstrumentType.Spot, OkxInstrumentType.Option);
        Add(socket.Connection, 802, OkxInstrumentType.Margin);
        var pending = client.UnsubscribeWithConfirmationAsync(Update(socket.Connection, target));
        var wire = await socket.Receive();
        var request = Assert.Single(socket.Connection.Sent);
        Assert.Equal("unsubscribe", wire["op"]!.Value<string>());
        Assert.Equal(request.RequestId, wire["id"]!.Value<string>());
        Assert.Equal(2, ((JArray)wire["args"]!).Count);

        JObject terminal;
        if (accepted)
        {
            var foreign = Error(request);
            foreign["id"] = "ForeignRequest";
            await socket.Send(foreign);
            await socket.Send(Ack(request, OkxInstrumentType.Option));
            await socket.Send(Ack(request, OkxInstrumentType.Option));
            terminal = Ack(request, OkxInstrumentType.Spot);
        }
        else
        {
            await socket.Send(Ack(request, OkxInstrumentType.Spot));
            terminal = Error(request);
        }
        await socket.Send(terminal);
        var result = await pending.WaitAsync(TimeSpan.FromSeconds(3));

        Assert.Equal(accepted, result.Success);
        Assert.True(result.Data.LocalClosed);
        Assert.Equal(accepted, result.Data.ServerConfirmed);
        Assert.Equal(terminal.ToString(), result.Raw);
        if (!accepted) Assert.Equal(60012, result.Error?.Code);
        Assert.Single(socket.Connection.Sent);
        Assert.True(socket.Connection.Connected);
        Assert.Null(socket.Connection.GetSubscription(target.Id));
    }

    [Fact]
    public async Task UnsupportedStoredRequest_DoesNotInventServerConfirmation()
    {
        using var client = new TestClient();
        using var socket = await server.Open(client, Complete);
        var target = Add(socket.Connection, 801, OkxInstrumentType.Spot);
        target.Request = new object();
        Add(socket.Connection, 802, OkxInstrumentType.Margin);

        var result = await client.UnsubscribeWithConfirmationAsync(Update(socket.Connection, target));

        Assert.False(result.Success);
        Assert.True(result.Data.LocalClosed);
        Assert.False(result.Data.ServerConfirmed);
        Assert.Empty(socket.Connection.Sent);
    }

    [Fact]
    public async Task ServerAckWithoutCompletedLocalRemoval_IsNotAggregateSuccess()
    {
        using var client = new TestClient();
        ScriptedConnection? connection = null;
        using var socket = await server.Open(client, (request, receive) =>
        {
            Complete(request, receive);
            // Inject the SDK's closing-state race after ACK: native CloseAsync then skips registry removal.
            typeof(WebSocketConnection).GetProperty(nameof(WebSocketConnection.Status))!
                .SetValue(connection, WebSocketStatus.Closing);
            return Task.CompletedTask;
        });
        connection = socket.Connection;
        var target = Add(connection, 801, OkxInstrumentType.Spot);
        Add(connection, 802, OkxInstrumentType.Margin);

        var result = await client.UnsubscribeWithConfirmationAsync(Update(connection, target));

        Assert.False(result.Success);
        Assert.False(result.Data.LocalClosed);
        Assert.True(result.Data.ServerConfirmed);
        Assert.True(target.Closed);
        Assert.Same(target, connection.GetSubscription(target.Id));
        Assert.IsType<InvalidOperationError>(result.Error);
        Assert.Equal("unsubscribe", JObject.Parse(result.Raw!)["event"]!.Value<string>());
        Assert.Single(connection.Sent);
    }

    [Fact]
    public async Task LastSubscription_ClosesTransportWithoutLosingNativeServerAck()
    {
        using var client = new TestClient();
        using var socket = await server.Open(client, Complete, nativeWait: true);
        var target = Add(socket.Connection, 801, OkxInstrumentType.Spot);
        var pending = client.UnsubscribeWithConfirmationAsync(Update(socket.Connection, target));
        await socket.Receive();
        var request = Assert.Single(socket.Connection.Sent);
        var acknowledgement = Ack(request, OkxInstrumentType.Spot);
        await socket.Send(acknowledgement);

        var result = await pending.WaitAsync(TimeSpan.FromSeconds(3));

        Assert.True(result.Success);
        Assert.True(result.Data.LocalClosed);
        Assert.True(result.Data.ServerConfirmed);
        Assert.Equal(acknowledgement.ToString(), result.Raw);
        Assert.Null(socket.Connection.GetSubscription(target.Id));
        Assert.False(socket.Connection.Connected);
        Assert.Single(socket.Connection.Sent);
    }

    private static Task Complete(OkxSocketRequest request, Func<JToken, bool> receive)
    {
        Assert.True(receive(Ack(request, request.Arguments.Single().InstrumentType!.Value)));
        return Task.CompletedTask;
    }

    private static JObject Ack(OkxSocketRequest request, OkxInstrumentType type) => new()
    {
        ["id"] = request.RequestId, ["event"] = "unsubscribe", ["connId"] = "unit-connection",
        ["arg"] = new JObject { ["channel"] = "orders", ["instType"] = type == OkxInstrumentType.Spot ? "SPOT" : "OPTION" },
    };

    private static JObject Error(OkxSocketRequest request) => new()
    {
        ["id"] = request.RequestId, ["event"] = "error", ["code"] = "60012", ["msg"] = "Synthetic rejection",
        ["connId"] = "unit-connection",
    };

    private static WebSocketSubscription Add(ScriptedConnection connection, int id, params OkxInstrumentType[] types)
    {
        var target = WebSocketSubscription.CreateForRequest(id, new OkxSocketRequest(OkxSocketOperation.Subscribe,
            types.Select(type => new OkxSocketRequestArgument { Channel = "orders", InstrumentType = type })), true, false, _ => { });
        target.Confirmed = true;
        Assert.True(connection.AddSubscription(target));
        return target;
    }

    private static WebSocketUpdateSubscription Update(WebSocketConnection connection, WebSocketSubscription target) => new(connection, target);

    public sealed class SocketServer : IDisposable
    {
        private readonly HttpListener _listener = new();
        private readonly string _url;

        public SocketServer()
        {
            using var probe = new TcpListener(IPAddress.Loopback, 0);
            probe.Start();
            var port = ((IPEndPoint)probe.LocalEndpoint).Port;
            probe.Stop();
            _url = $"ws://127.0.0.1:{port}/";
            _listener.Prefixes.Add($"http://127.0.0.1:{port}/");
            try { _listener.Start(); }
            catch { _listener.Close(); throw; }
        }

        internal async Task<SocketScope> Open(TestClient client, Func<OkxSocketRequest, Func<JToken, bool>, Task> script, bool nativeWait = false)
        {
            var accepted = Accept();
            var connection = new ScriptedConnection(client, _url, script) { NativeWait = nativeWait };
            try
            {
                Assert.True(await connection.ConnectAsync().WaitAsync(TimeSpan.FromSeconds(3)));
                var peer = await accepted.WaitAsync(TimeSpan.FromSeconds(3));
                client.Attach(connection);
                return new SocketScope(connection, peer);
            }
            catch { connection.Dispose(); throw; }
        }

        internal SocketScope Unconnected(TestClient client, Func<OkxSocketRequest, Func<JToken, bool>, Task> script)
        {
            var connection = new ScriptedConnection(client, _url, script);
            client.Attach(connection);
            return new SocketScope(connection, null);
        }

        private async Task<System.Net.WebSockets.WebSocket> Accept()
        {
            var context = await _listener.GetContextAsync();
            return (await context.AcceptWebSocketAsync(null)).WebSocket;
        }

        public void Dispose() => _listener.Close();
    }

    internal sealed class SocketScope(ScriptedConnection connection, System.Net.WebSockets.WebSocket? peer) : IDisposable
    {
        public ScriptedConnection Connection { get; } = connection;
        public Task Send(JObject response)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(response.ToString());
            return peer!.SendAsync(new ArraySegment<byte>(bytes), System.Net.WebSockets.WebSocketMessageType.Text, true, CancellationToken.None);
        }
        public async Task<JObject> Receive()
        {
            var bytes = new byte[8192];
            var frame = await peer!.ReceiveAsync(new ArraySegment<byte>(bytes), CancellationToken.None).WaitAsync(TimeSpan.FromSeconds(3));
            Assert.True(frame.EndOfMessage);
            Assert.Equal(System.Net.WebSockets.WebSocketMessageType.Text, frame.MessageType);
            return JObject.Parse(System.Text.Encoding.UTF8.GetString(bytes, 0, frame.Count));
        }
        public void Dispose() { Connection.Dispose(); peer?.Dispose(); }
    }

    internal sealed class TestClient : OkxWebSocketApiClient
    {
        public void Attach(WebSocketConnection connection) => Assert.True(WebSocketConnections.TryAdd(connection.Id, connection));
    }

    internal sealed class ScriptedConnection(WebSocketApiClient client, string url, Func<OkxSocketRequest, Func<JToken, bool>, Task> script)
        : WebSocketConnection(BaseClient.LoggerFactory.CreateLogger("OKX.Api.Tests"), client,
            new WebSocketClient(BaseClient.LoggerFactory.CreateLogger("OKX.Api.Tests"),
                new WebSocketParameters(new Uri(url), false)), url), IDisposable
    {
        public List<OkxSocketRequest> Sent { get; } = [];
        public List<Func<JToken, bool>> Receivers { get; } = [];
        public bool NativeWait { get; init; }

        public override Task SendAndWaitAsync<T>(T request, TimeSpan timeout, Func<JToken, bool> handler)
        {
            Assert.Equal(TimeSpan.FromSeconds(10), timeout);
            var command = Assert.IsType<OkxSocketRequest>(request);
            Sent.Add(command);
            Receivers.Add(handler);
            return NativeWait ? base.SendAndWaitAsync(request, timeout, handler) : script(command, handler);
        }
    }
}
