using ApiSharp;
using ApiSharp.Models;
using ApiSharp.WebSocket;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OKX.Api.Base;
using OKX.Api.Common;
using OKX.Api.Trade;

namespace OKX.Api.Tests.Trade;

public class OkxSocketAcknowledgementSafetyTests
{
    [Theory]
    [InlineData(false, "SubscriptionB")]
    [InlineData(false, null)]
    [InlineData(true, "SubscriptionB")]
    [InlineData(true, null)]
    public void TradingQuery_DoesNotConsumeSubscriptionError(bool amend, string? responseId)
    {
        using var client = new TestableClient();
        var response = Error(responseId);
        var matched = amend
            ? client.Query<OkxTradeOrderAmend>(new OkxSocketRequest<OkxTradeOrderAmendRequest>(
                "TradeRequest123", OkxSocketOperation.AmendOrder, new[] { new OkxTradeOrderAmendRequest { InstrumentIdCode = 101 } }), response)
            : client.Query<OkxTradeOrderPlaceResponse>(new OkxSocketRequest<OkxTradeOrderPlaceRequest>(
                "TradeRequest123", OkxSocketOperation.Order, new[] { new OkxTradeOrderPlaceRequest { InstrumentIdCode = 101 } }), response);
        Assert.False(matched);
    }

    [Theory]
    [InlineData("subscribe")]
    [InlineData("error")]
    public void SubscriptionResponse_DoesNotCompleteDifferentId(string eventName)
    {
        using var client = new TestableClient();
        var request = Request("SubscriptionA", OkxInstrumentType.Any);
        var response = eventName == "subscribe" ? Ack("SubscriptionB", OkxInstrumentType.Any) : Error("SubscriptionB");
        Assert.False(client.SubscribeResponse(request, response, out var result));
        Assert.Null(result);
    }

    [Fact]
    public void StandaloneResponse_DoesNotConfirmWholeMultiArgumentSubscription()
    {
        using var client = new TestableClient();
        Assert.False(client.SubscribeResponse(Request(null, OkxInstrumentType.Spot, OkxInstrumentType.Option),
            Ack(null, OkxInstrumentType.Spot), out var result));
        Assert.Null(result);
    }

    [Fact]
    public async Task SubscribeAttempt_DoesNotConfirmPartialAcknowledgement()
    {
        using var client = new TestableClient();
        var request = Request(null, OkxInstrumentType.Spot, OkxInstrumentType.Option);
        var subscription = Subscription(request);
        using var connection = new ScriptedConnection(client, (sent, receive) =>
        {
            Assert.False(receive(Ack(sent.RequestId, OkxInstrumentType.Spot)));
            Assert.False(subscription.Confirmed);
        });
        var result = await client.SubscribeAndWaitAsync(connection, request, subscription);
        Assert.False(result.Success);
        Assert.False(subscription.Confirmed);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task SubscribeAttempt_ConfirmsAllArgumentsInEitherOrder(bool reverse)
    {
        using var client = new TestableClient();
        var request = Request(null, OkxInstrumentType.Spot, OkxInstrumentType.Option);
        var subscription = Subscription(request);
        using var connection = new ScriptedConnection(client, (sent, receive) =>
        {
            Assert.NotNull(sent.RequestId);
            Assert.Matches("^[A-Za-z0-9]{1,32}$", sent.RequestId!);
            var payload = JObject.FromObject(sent, JsonSerializer.Create(SerializerOptions.WithConverters));
            Assert.Equal(new[] { "id", "op", "args" }.OrderBy(name => name), payload.Properties().Select(property => property.Name).OrderBy(name => name));
            Assert.Equal(sent.RequestId, payload["id"]!.Value<string>());
            Assert.Null(payload["args"]![0]!["id"]);
            Assert.False(receive(Ack(sent.RequestId, reverse ? OkxInstrumentType.Option : OkxInstrumentType.Spot)));
            Assert.False(subscription.Confirmed);
            Assert.True(receive(Ack(sent.RequestId, reverse ? OkxInstrumentType.Spot : OkxInstrumentType.Option)));
            Assert.False(receive(Error(sent.RequestId)));
        });
        var result = await client.SubscribeAndWaitAsync(connection, request, subscription);
        Assert.True(result.Success);
        Assert.True(subscription.Confirmed);
        Assert.Null(request.RequestId);
        Assert.Same(request, subscription.Request);
    }

    [Theory]
    [InlineData("login")]
    [InlineData("error")]
    public async Task Authentication_ReturnsNumericLoginRejection(string eventName)
    {
        using var client = new TestableClient();
        client.SetApiCredentials("unit-key", "unit-secret", "unit-passphrase");
        var response = Error(null, "60009");
        response["event"] = eventName;
        using var connection = new AuthenticationConnection(client, response);
        var result = await client.Authenticate(connection);
        Assert.False(result.Success);
        Assert.Equal(60009, result.Error?.Code);
        Assert.False(client.IsAuthendicated);
    }

    [Theory]
    [InlineData("subscribe", null)]
    [InlineData("error", null)]
    [InlineData("subscribe", "SubscriptionB")]
    [InlineData("error", "SubscriptionB")]
    public void SubscriptionResponse_RequiresEchoedIdWhenSupplied(string eventName, string? id)
    {
        using var client = new TestableClient();
        var response = eventName == "subscribe" ? Ack(id, OkxInstrumentType.Any) : Error(id);
        Assert.False(client.SubscribeResponse(Request("SubscriptionA", OkxInstrumentType.Any), response, out var result));
        Assert.Null(result);
    }

    [Fact]
    public void IdlessRequest_DoesNotAcceptForeignIdOrUnidentifiedError()
    {
        using var client = new TestableClient();
        var request = Request(null, OkxInstrumentType.Any);
        Assert.False(client.SubscribeResponse(request, Ack("SubscriptionB", OkxInstrumentType.Any), out _));
        Assert.False(client.SubscribeResponse(request, Error(null), out _));
        Assert.True(client.SubscribeResponse(request, Ack(null, OkxInstrumentType.Any), out var result));
        Assert.True(result!.Success);
    }

    [Fact]
    public async Task SubscribeAttempt_DuplicateWrongMalformedAndForeignRepliesDoNotAdvanceConfirmation()
    {
        using var client = new TestableClient();
        var request = Request("SubscriptionA", OkxInstrumentType.Spot, OkxInstrumentType.Option);
        var subscription = Subscription(request);
        using var connection = new ScriptedConnection(client, (sent, receive) =>
        {
            Assert.Equal("SubscriptionA", sent.RequestId);
            Assert.False(receive(Ack(null, OkxInstrumentType.Spot)));
            Assert.False(receive(Ack("SubscriptionB", OkxInstrumentType.Spot)));
            Assert.False(receive(Error("SubscriptionB")));
            Assert.False(receive(Error(null)));
            Assert.False(receive(new JValue("pong")));
            Assert.False(receive(new JObject { ["id"] = sent.RequestId, ["event"] = "subscribe" }));
            Assert.False(receive(Ack(sent.RequestId, OkxInstrumentType.Any)));
            Assert.False(receive(Ack(sent.RequestId, OkxInstrumentType.Spot, "unsubscribe")));
            Assert.False(receive(Ack(sent.RequestId, OkxInstrumentType.Spot)));
            Assert.False(receive(Ack(sent.RequestId, OkxInstrumentType.Spot)));
            Assert.False(subscription.Confirmed);
            Assert.True(receive(Ack(sent.RequestId, OkxInstrumentType.Option)));
        });
        Assert.True((await client.SubscribeAndWaitAsync(connection, request, subscription)).Success);
        Assert.Equal("SubscriptionA", request.RequestId);
    }

    [Theory]
    [InlineData("60012", 60012)]
    [InlineData("unexpected", null)]
    public async Task SubscribeAttempt_PreservesCorrelatedFailureAfterPartialAck(string code, int? numericCode)
    {
        using var client = new TestableClient();
        var request = Request(null, OkxInstrumentType.Spot, OkxInstrumentType.Option);
        var subscription = Subscription(request);
        subscription.Confirmed = true;
        using var connection = new ScriptedConnection(client, (sent, receive) =>
        {
            Assert.False(subscription.Confirmed);
            Assert.False(receive(Ack(sent.RequestId, OkxInstrumentType.Spot)));
            Assert.True(receive(Error(sent.RequestId, code)));
            Assert.False(receive(Ack(sent.RequestId, OkxInstrumentType.Option)));
        });
        var result = await client.SubscribeAndWaitAsync(connection, request, subscription);
        Assert.False(result.Success);
        Assert.False(subscription.Confirmed);
        Assert.Equal(numericCode, result.Error?.Code);
        Assert.Equal(code, JObject.Parse(result.Raw!)["code"]!.Value<string>());
    }

    [Fact]
    public async Task Resubscription_UsesFreshStateAndGeneratedIdWithoutMutatingStoredRequest()
    {
        using var client = new TestableClient();
        var request = Request(null, OkxInstrumentType.Spot, OkxInstrumentType.Option);
        var subscription = Subscription(request);
        string? firstId = null;
        using var first = new ScriptedConnection(client, (sent, receive) =>
        {
            firstId = sent.RequestId;
            Assert.False(receive(Ack(firstId, OkxInstrumentType.Spot)));
            Assert.True(receive(Ack(firstId, OkxInstrumentType.Option)));
        });
        Assert.True((await client.SubscribeAndWaitAsync(first, request, subscription)).Success);
        using var second = new ScriptedConnection(client, (sent, receive) =>
        {
            Assert.NotEqual(firstId, sent.RequestId);
            Assert.False(subscription.Confirmed);
            Assert.False(receive(Ack(firstId, OkxInstrumentType.Spot)));
            Assert.False(receive(Error(firstId)));
            Assert.False(receive(Ack(sent.RequestId, OkxInstrumentType.Option)));
            Assert.False(receive(Ack(sent.RequestId, OkxInstrumentType.Option)));
        });
        Assert.False((await client.SubscribeAndWaitAsync(second, request, subscription)).Success);
        Assert.False(subscription.Confirmed);
        Assert.Null(request.RequestId);
    }

    [Fact]
    public async Task ConcurrentAttempts_OnSameConnectionKeepIdenticalFiltersIsolatedById()
    {
        using var client = new TestableClient();
        var receivers = new List<(OkxSocketRequest Request, Func<JToken, bool> Receive)>();
        var release = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        using var connection = new ScriptedConnection(client, (sent, receive) =>
        {
            receivers.Add((sent, receive));
            return release.Task;
        });
        var requestA = Request(null, OkxInstrumentType.Any);
        var requestB = Request(null, OkxInstrumentType.Any);
        var subscriptionA = Subscription(requestA);
        var subscriptionB = Subscription(requestB);
        var taskA = client.SubscribeAndWaitAsync(connection, requestA, subscriptionA);
        var taskB = client.SubscribeAndWaitAsync(connection, requestB, subscriptionB);
        try
        {
            Assert.Equal(2, receivers.Count);
            Assert.NotEqual(receivers[0].Request.RequestId, receivers[1].Request.RequestId);
            var responseB = Ack(receivers[1].Request.RequestId, OkxInstrumentType.Any);
            Assert.False(receivers[0].Receive(responseB));
            Assert.True(receivers[1].Receive(responseB));
            Assert.True(receivers[0].Receive(Ack(receivers[0].Request.RequestId, OkxInstrumentType.Any)));
        }
        finally { release.TrySetResult(); }
        Assert.All(await Task.WhenAll(taskA, taskB).WaitAsync(TimeSpan.FromSeconds(3)), result => Assert.True(result.Success));
        Assert.True(subscriptionA.Confirmed);
        Assert.True(subscriptionB.Confirmed);
    }

    [Fact]
    public async Task DuplicateRequestedFilters_RequireAllDistinctArgumentsNotDuplicateAcks()
    {
        using var client = new TestableClient();
        var request = Request(null, OkxInstrumentType.Spot, OkxInstrumentType.Spot, OkxInstrumentType.Option);
        using var connection = new ScriptedConnection(client, (sent, receive) =>
        {
            Assert.False(receive(Ack(sent.RequestId, OkxInstrumentType.Spot)));
            Assert.False(receive(Ack(sent.RequestId, OkxInstrumentType.Spot)));
            Assert.True(receive(Ack(sent.RequestId, OkxInstrumentType.Option)));
        });
        Assert.True((await client.SubscribeAndWaitAsync(connection, request, Subscription(request))).Success);
        Assert.Equal(3, request.Arguments.Count);
    }

    [Fact]
    public async Task EmptySubscription_FailsBeforeSending()
    {
        using var client = new TestableClient();
        var request = Request(null);
        var subscription = Subscription(request);
        using var connection = new ScriptedConnection(client, (_, _) => Assert.Fail("Must not send an empty subscription"));
        Assert.False((await client.SubscribeAndWaitAsync(connection, request, subscription)).Success);
        Assert.False(subscription.Confirmed);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task OrdersUnsubscribe_RequiresAllArgumentsAndCorrelatedId(bool partial)
    {
        using var client = new TestableClient();
        var request = Request("SubscriptionA", OkxInstrumentType.Spot, OkxInstrumentType.Option);
        using var connection = new ScriptedConnection(client, (sent, receive) =>
        {
            Assert.Equal(OkxSocketOperation.Unsubscribe, sent.Operation);
            Assert.NotEqual(request.RequestId, sent.RequestId);
            Assert.False(receive(Ack(request.RequestId, OkxInstrumentType.Spot, "unsubscribe")));
            Assert.False(receive(Ack(null, OkxInstrumentType.Spot, "unsubscribe")));
            Assert.False(receive(Ack(sent.RequestId, OkxInstrumentType.Spot, "subscribe")));
            Assert.False(receive(Ack(sent.RequestId, OkxInstrumentType.Spot, "unsubscribe")));
            Assert.False(receive(Ack(sent.RequestId, OkxInstrumentType.Spot, "unsubscribe")));
            if (!partial) Assert.True(receive(Ack(sent.RequestId, OkxInstrumentType.Option, "unsubscribe")));
        });
        Assert.Equal(!partial, await client.Unsubscribe(connection, Subscription(request)));
        Assert.Equal("SubscriptionA", request.RequestId);
    }

    [Fact]
    public async Task OrdersUnsubscribe_CorrelatedErrorDoesNotReportSuccess()
    {
        using var client = new TestableClient();
        var request = Request(null, OkxInstrumentType.Any);
        using var connection = new ScriptedConnection(client, (sent, receive) => Assert.True(receive(Error(sent.RequestId))));
        Assert.False(await client.Unsubscribe(connection, Subscription(request)));
    }

    [Fact]
    public async Task GeneralUnsubscribe_RetainsIdlessWireAndConfirmsEveryArgument()
    {
        using var client = new TestableClient();
        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe,
            new OkxSocketRequestArgument { Channel = "account", Currency = "BTC" },
            new OkxSocketRequestArgument { Channel = "account", Currency = "ETH" });
        using var connection = new ScriptedConnection(client, (sent, receive) =>
        {
            Assert.Null(sent.RequestId);
            Assert.False(receive(Error(null)));
            Assert.False(receive(JObject.Parse("{\"event\":\"unsubscribe\",\"arg\":{\"channel\":\"account\",\"ccy\":\"BTC\"}}")));
            Assert.True(receive(JObject.Parse("{\"event\":\"unsubscribe\",\"arg\":{\"channel\":\"account\",\"ccy\":\"ETH\"}}")));
        });
        Assert.True(await client.Unsubscribe(connection, Subscription(request)));
    }

    [Theory]
    [InlineData(60004)]
    [InlineData(60005)]
    [InlineData(60006)]
    [InlineData(60007)]
    [InlineData(60009)]
    [InlineData(60023)]
    [InlineData(60024)]
    [InlineData(60026)]
    [InlineData(60031)]
    [InlineData(60032)]
    [InlineData(63999)]
    public async Task Authentication_PreservesDocumentedAuthSpecificErrors(int code)
    {
        using var client = new TestableClient();
        client.SetApiCredentials("unit-key", "unit-secret", "unit-passphrase");
        using var connection = new AuthenticationConnection(client, Error(null, code.ToString()));
        var result = await client.Authenticate(connection);
        Assert.False(result.Success);
        Assert.Equal(code, result.Error?.Code);
        Assert.Equal(code.ToString(), JObject.Parse(result.Raw!)["code"]!.Value<string>());
    }

    [Fact]
    public async Task Authentication_IgnoresSubscriptionErrorsAndPreservesSuccess()
    {
        using var client = new TestableClient();
        client.SetApiCredentials("unit-key", "unit-secret", "unit-passphrase");
        using var connection = new AuthenticationConnection(client, Error("SubscriptionB"), Error(null),
            Error("SubscriptionB", "60009"), new JValue("pong"),
            JObject.Parse("{\"event\":\"login\",\"code\":\"0\",\"msg\":\"\",\"connId\":\"a4d3ae55\"}"));
        Assert.True((await client.Authenticate(connection)).Success);
        Assert.True(client.IsAuthendicated);
        Assert.Equal(new[] { false, false, false, false, true }, connection.Matches);
    }

    [Fact]
    public void Query_LoginAndNonObjectRepliesCannotCompleteTradingRequest()
    {
        using var client = new TestableClient();
        var request = new OkxSocketRequest<OkxTradeOrderPlaceRequest>("TradeRequest123", OkxSocketOperation.Order, Array.Empty<OkxTradeOrderPlaceRequest>());
        Assert.False(client.Query<OkxTradeOrderPlaceResponse>(request, JObject.Parse("{\"event\":\"login\",\"code\":\"0\"}")));
        Assert.False(client.Query<OkxTradeOrderPlaceResponse>(request, new JValue("pong")));
        Assert.False(client.Query<OkxTradeOrderPlaceResponse>(request, Error("TradeRequest123")));
        Assert.True(client.Query<string>("ping", new JValue("pong")));
        Assert.True(client.Query<OkxSocketResponse>(new OkxSocketAuthRequest(OkxSocketOperation.Login),
            JObject.Parse("{\"event\":\"login\",\"code\":\"0\"}")));
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void CorrelatedError_OptionalCodeOrMessageCannotTurnErrorIntoSuccess(bool hasCode)
    {
        using var client = new TestableClient();
        var response = new JObject { ["id"] = "SubscriptionA", ["event"] = "error" };
        if (hasCode) response["code"] = "60012";
        else response["msg"] = "Subscription rejected";
        Assert.True(client.SubscribeResponse(Request("SubscriptionA", OkxInstrumentType.Any), response, out var result));
        Assert.False(result!.Success);
        Assert.Equal(hasCode ? 60012 : (int?)null, result.Error?.Code);
        Assert.True(JToken.DeepEquals(response, JObject.Parse(result.Raw!)));
    }

    [Fact]
    public void NonzeroCodeOnAcknowledgement_DoesNotConfirmSubscription()
    {
        using var client = new TestableClient();
        var response = Ack("SubscriptionA", OkxInstrumentType.Any);
        response["code"] = "60012";
        Assert.True(client.SubscribeResponse(Request("SubscriptionA", OkxInstrumentType.Any), response, out var result));
        Assert.False(result!.Success);
        Assert.Equal(60012, result.Error?.Code);
    }

    [Fact]
    public void MalformedIdentityOrOperation_IsNotConsumed()
    {
        using var client = new TestableClient();
        var request = Request("SubscriptionA", OkxInstrumentType.Any);
        var response = Ack("SubscriptionA", OkxInstrumentType.Any);
        response["id"] = new JArray();
        Assert.False(client.SubscribeResponse(request, response, out _));
        response["id"] = "SubscriptionA";
        response["op"] = "order";
        Assert.False(client.SubscribeResponse(request, response, out _));
        response["op"] = new JObject();
        Assert.False(client.SubscribeResponse(request, response, out _));
        response.Remove("op");
        response["event"] = new JObject();
        Assert.False(client.SubscribeResponse(request, response, out _));
    }

    [Theory]
    [InlineData(false, 2)]
    [InlineData(true, 1)]
    public async Task MissingAcknowledgements_ReportUncertaintyNotSuccess(bool partial, int missing)
    {
        using var client = new TestableClient();
        var request = Request(null, OkxInstrumentType.Spot, OkxInstrumentType.Option);
        using var connection = new ScriptedConnection(client, (sent, receive) =>
        {
            if (partial) Assert.False(receive(Ack(sent.RequestId, OkxInstrumentType.Spot)));
        });
        var result = await client.SubscribeAndWaitAsync(connection, request, Subscription(request));
        Assert.False(result.Success);
        Assert.Contains($"{missing} argument(s)", result.Error!.Message);
        Assert.Contains("uncertain", result.Error.Message);
    }

    [Fact]
    public async Task FailedReauthentication_ResetsPreviousAuthenticatedFlag()
    {
        using var client = new TestableClient();
        client.SetApiCredentials("unit-key", "unit-secret", "unit-passphrase");
        using var first = new AuthenticationConnection(client, JObject.Parse("{\"event\":\"login\",\"code\":\"0\",\"msg\":\"\"}"));
        Assert.True((await client.Authenticate(first)).Success);
        using var second = new AuthenticationConnection(client, Error(null, "60024"));
        Assert.False((await client.Authenticate(second)).Success);
        Assert.False(client.IsAuthendicated);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task CompletedOrTimedOutAttempt_DoesNotConsumeLateReplies(bool confirmed)
    {
        using var client = new TestableClient();
        var request = Request("SubscriptionA", OkxInstrumentType.Spot, OkxInstrumentType.Option);
        Func<JToken, bool>? oldReceive = null;
        using var connection = new ScriptedConnection(client, (sent, receive) =>
        {
            oldReceive = receive;
            Assert.False(receive(Ack(sent.RequestId, OkxInstrumentType.Spot)));
            if (confirmed) Assert.True(receive(Ack(sent.RequestId, OkxInstrumentType.Option)));
        });
        var subscription = Subscription(request);
        Assert.Equal(confirmed, (await client.SubscribeAndWaitAsync(connection, request, subscription)).Success);
        Assert.False(oldReceive!(Ack(request.RequestId, OkxInstrumentType.Option)));
        Assert.False(oldReceive(Error(request.RequestId)));
        Assert.Equal(confirmed, subscription.Confirmed);
    }

    [Theory]
    [InlineData("subscribe", true)]
    [InlineData("unsubscribe", true)]
    [InlineData("error", false)]
    [InlineData("login", false)]
    [InlineData("channel-conn-count-error", false)]
    public void IntermediateAckRouting_DoesNotTreatErrorsAsSuccessfulAcknowledgements(string eventName, bool expected)
    {
        using var client = new TestableClient();
        var response = Ack("SubscriptionA", OkxInstrumentType.Spot, eventName);
        Assert.Equal(expected, client.GenericResponse(response));
        response["code"] = "60012";
        Assert.False(client.GenericResponse(response));
        response.Remove("code");
        response.Remove("arg");
        Assert.False(client.GenericResponse(response));
    }

    private static OkxSocketRequest Request(string? id, params OkxInstrumentType[] types)
        => new(OkxSocketOperation.Subscribe, types.Select(type => new OkxSocketRequestArgument
        {
            Channel = "orders", InstrumentType = type,
        })) { RequestId = id };

    private static WebSocketSubscription Subscription(OkxSocketRequest request)
        => WebSocketSubscription.CreateForRequest(1, request, true, false, _ => { });

    private static JObject Ack(string? id, OkxInstrumentType type, string eventName = "subscribe")
    {
        var response = new JObject
        {
            ["event"] = eventName,
            ["arg"] = new JObject { ["channel"] = "orders", ["instType"] = type == OkxInstrumentType.Any ? "ANY" : type == OkxInstrumentType.Spot ? "SPOT" : "OPTION" },
            ["connId"] = "a4d3ae55",
        };
        if (id is not null) response["id"] = id;
        return response;
    }

    private static JObject Error(string? id, string code = "60012")
    {
        var response = new JObject { ["event"] = "error", ["code"] = code, ["msg"] = "Invalid request", ["connId"] = "a4d3ae55" };
        if (id is not null) response["id"] = id;
        return response;
    }

    private sealed class TestableClient : OkxWebSocketApiClient
    {
        public bool Query<T>(object request, JToken response) => base.HandleQueryResponse<T>(null!, request, response, out _);
        public bool SubscribeResponse(OkxSocketRequest request, JToken response, out CallResult<object>? result)
            => base.HandleSubscriptionResponse(null!, Subscription(request), request, response, out result);
        public Task<bool> Unsubscribe(WebSocketConnection connection, WebSocketSubscription subscription)
            => base.UnsubscribeAsync(connection, subscription);
        public Task<CallResult<bool>> Authenticate(WebSocketConnection connection)
            => base.AuthenticateAsync(connection);
        public bool GenericResponse(JToken response)
            => base.MessageMatchesHandler(null!, response, "subscription-acknowledgement");
    }

    private sealed class ScriptedConnection : WebSocketConnection, IDisposable
    {
        private readonly Func<OkxSocketRequest, Func<JToken, bool>, Task> _script;
        public OkxSocketRequest? SentRequest { get; private set; }

        public ScriptedConnection(WebSocketApiClient client, Action<OkxSocketRequest, Func<JToken, bool>> script)
            : this(client, (request, receive) => { script(request, receive); return Task.CompletedTask; }) { }

        public ScriptedConnection(WebSocketApiClient client, Func<OkxSocketRequest, Func<JToken, bool>, Task> script)
            : base(BaseClient.LoggerFactory.CreateLogger("OKX.Api.Tests"), client,
                new WebSocketClient(BaseClient.LoggerFactory.CreateLogger("OKX.Api.Tests"),
                    new WebSocketParameters(new Uri("wss://localhost"), false)), "wss://localhost")
            => _script = script;

        public override Task SendAndWaitAsync<T>(T request, TimeSpan timeout, Func<JToken, bool> handler)
        {
            SentRequest = Assert.IsType<OkxSocketRequest>(request);
            return _script(SentRequest, handler);
        }
    }

    private sealed class AuthenticationConnection : WebSocketConnection, IDisposable
    {
        private readonly JToken[] _responses;
        public List<bool> Matches { get; } = [];

        public AuthenticationConnection(WebSocketApiClient client, params JToken[] responses)
            : base(BaseClient.LoggerFactory.CreateLogger("OKX.Api.Tests"), client,
                new WebSocketClient(BaseClient.LoggerFactory.CreateLogger("OKX.Api.Tests"),
                    new WebSocketParameters(new Uri("wss://localhost"), false)), "wss://localhost")
            => _responses = responses;

        public override Task SendAndWaitAsync<T>(T request, TimeSpan timeout, Func<JToken, bool> handler)
        {
            Assert.IsType<OkxSocketAuthRequest>(request);
            foreach (var response in _responses)
            {
                var matched = handler(response);
                Matches.Add(matched);
                if (matched) break;
            }
            return Task.CompletedTask;
        }
    }
}
