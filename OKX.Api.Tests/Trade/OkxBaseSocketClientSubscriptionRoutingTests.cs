using ApiSharp;
using ApiSharp.Models;
using ApiSharp.WebSocket;
using Newtonsoft.Json.Linq;
using OKX.Api.Base;
using OKX.Api.Common;

namespace OKX.Api.Tests.Trade;

public class OkxBaseSocketClientSubscriptionRoutingTests
{
    [Theory]
    [InlineData(OkxInstrumentType.Any, null, null, "SPOT", null, "BTC-USDT", true)]
    [InlineData(OkxInstrumentType.Any, null, null, "MARGIN", null, "BTC-USDT", true)]
    [InlineData(OkxInstrumentType.Any, null, null, "SWAP", "BTC-USDT", "BTC-USDT-SWAP", true)]
    [InlineData(OkxInstrumentType.Any, null, null, "FUTURES", "BTC-USD", "BTC-USD-261225", true)]
    [InlineData(OkxInstrumentType.Any, null, null, "OPTION", "BTC-USD", "BTC-USD-261225-100000-C", true)]
    [InlineData(OkxInstrumentType.Any, null, null, "EVENTS", null, "TEST-EVENT", true)]
    [InlineData(OkxInstrumentType.Any, null, null, "ANY", null, null, true)]
    [InlineData(OkxInstrumentType.Spot, null, null, "SPOT", null, null, true)]
    [InlineData(OkxInstrumentType.Any, null, null, null, null, "BTC-USDT", false)]
    [InlineData(OkxInstrumentType.Spot, null, null, "SPOT", null, "BTC-USDT", true)]
    [InlineData(OkxInstrumentType.Swap, "BTC-USDT", null, "SWAP", "BTC-USDT", "BTC-USDT-SWAP", true)]
    [InlineData(OkxInstrumentType.Swap, null, "BTC-USDT-SWAP", "SWAP", "BTC-USDT", "BTC-USDT-SWAP", true)]
    [InlineData(OkxInstrumentType.Spot, null, null, "MARGIN", null, "BTC-USDT", false)]
    [InlineData(OkxInstrumentType.Swap, "BTC-USDT", null, "SWAP", "ETH-USDT", "ETH-USDT-SWAP", false)]
    [InlineData(OkxInstrumentType.Spot, null, "BTC-USDT", "SPOT", null, "ETH-USDT", false)]
    [InlineData(OkxInstrumentType.Swap, "BTC-USDT", null, "SWAP", null, "BTC-USDT-SWAP", false)]
    public void OrdersPush_MatchesSubscriptionFiltersInsteadOfRequiringIdenticalArguments(
        OkxInstrumentType requestedType, string? requestedFamily, string? requestedId,
        string? pushType, string? pushFamily, string? pushId, bool expected)
    {
        using var client = new TestableOkxWebSocketApiClient();
        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, new OkxSocketRequestArgument
        {
            Channel = "orders",
            InstrumentType = requestedType,
            InstrumentFamily = requestedFamily,
            InstrumentId = requestedId,
        });
        var message = new JObject
        {
            ["arg"] = new JObject
            {
                ["channel"] = "orders", ["uid"] = "test-user", ["instType"] = pushType,
                ["instFamily"] = pushFamily, ["instId"] = pushId,
            },
            ["data"] = new JArray(new JObject { ["state"] = "canceled" }),
        };

        Assert.Equal(expected, client.InvokeMessageMatchesHandler(request, message));
        message["arg"]!["channel"] = "fills";
        Assert.False(client.InvokeMessageMatchesHandler(request, message));
    }

    [Theory]
    [InlineData("ANY", true)]
    [InlineData("SPOT", false)]
    public void OrdersSubscribeAck_StillRequiresExactSubscriptionArguments(string responseType, bool expected)
    {
        using var client = new TestableOkxWebSocketApiClient();
        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, new OkxSocketRequestArgument
        {
            Channel = "orders", InstrumentType = OkxInstrumentType.Any,
        });
        var acknowledgement = new JObject
        {
            ["event"] = "subscribe",
            ["arg"] = new JObject { ["channel"] = "orders", ["instType"] = responseType },
            ["connId"] = "test-connection",
        };

        Assert.Equal(expected, client.InvokeHandleSubscriptionResponse(request, acknowledgement, out var result));
        Assert.Equal(expected, result?.Success ?? false);
        Assert.False(client.InvokeMessageMatchesHandler(request, acknowledgement));
    }

    [Theory]
    [InlineData("ANY", true)]
    [InlineData("SPOT", false)]
    public async Task OrdersUnsubscribeAck_StillRequiresExactSubscriptionArguments(string responseType, bool expected)
    {
        using var client = new TestableOkxWebSocketApiClient();
        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, new OkxSocketRequestArgument
        {
            Channel = "orders", InstrumentType = OkxInstrumentType.Any,
        });
        var subscription = WebSocketSubscription.CreateForRequest(1, request, true, false, _ => { });
        var connection = new StubWebSocketConnection(client, new JObject
        {
            ["event"] = "unsubscribe",
            ["arg"] = new JObject { ["channel"] = "orders", ["instType"] = responseType },
        });

        Assert.Equal(expected, await client.InvokeUnsubscribeAsync(connection, subscription));
    }

    [Fact]
    public void HandleSubscriptionResponse_MatchesSubscribeAckByFullArguments()
    {
        var client = new TestableOkxWebSocketApiClient();
        var request = new OkxSocketRequest(
            OkxSocketOperation.Subscribe,
            new OkxSocketRequestArgument
            {
                Channel = "account",
                Currency = "BTC",
                ExtraParameters = new Dictionary<string, string> { ["updateInterval"] = "0" }
            });

        var handled = client.InvokeHandleSubscriptionResponse(
            request,
            JObject.Parse("""
            {
              "event": "subscribe",
                "arg": {
                "channel": "account",
                "ccy": "BTC",
                "extraParams": "{\"updateInterval\":\"0\"}"
              }
            }
            """),
            out var callResult);

        Assert.True(handled);
        Assert.NotNull(callResult);
        Assert.True(callResult!.Success);
    }

    [Fact]
    public void HandleSubscriptionResponse_DoesNotMatchSubscribeAckWithDifferentCurrency()
    {
        var client = new TestableOkxWebSocketApiClient();
        var request = new OkxSocketRequest(
            OkxSocketOperation.Subscribe,
            new OkxSocketRequestArgument
            {
                Channel = "account",
                Currency = "BTC",
                ExtraParameters = new Dictionary<string, string> { ["updateInterval"] = "0" }
            });

        var handled = client.InvokeHandleSubscriptionResponse(
            request,
            JObject.Parse("""
            {
              "event": "subscribe",
                "arg": {
                "channel": "account",
                "ccy": "ETH",
                "extraParams": "{\"updateInterval\":\"0\"}"
              }
            }
            """),
            out var callResult);

        Assert.False(handled);
        Assert.Null(callResult);
    }

    [Fact]
    public void MessageMatchesHandler_DistinguishesSubscriptionsByCurrencyAndExtraParameters()
    {
        var client = new TestableOkxWebSocketApiClient();
        var request = new OkxSocketRequest(
            OkxSocketOperation.Subscribe,
            new OkxSocketRequestArgument
            {
                Channel = "account",
                Currency = "BTC",
                ExtraParameters = new Dictionary<string, string> { ["updateInterval"] = "0" }
            });

        var matchingMessage = JObject.Parse("""
        {
          "arg": {
            "channel": "account",
            "ccy": "BTC",
            "extraParams": "{\"updateInterval\":\"0\"}"
          },
          "data": [
            {
              "details": []
            }
          ]
        }
        """);

        var mismatchedMessage = JObject.Parse("""
        {
          "arg": {
            "channel": "account",
            "ccy": "BTC",
            "extraParams": "{\"updateInterval\":\"1000\"}"
          },
          "data": [
            {
              "details": []
            }
          ]
        }
        """);

        Assert.True(client.InvokeMessageMatchesHandler(request, matchingMessage));
        Assert.False(client.InvokeMessageMatchesHandler(request, mismatchedMessage));
    }

    [Fact]
    public void MessageMatchesHandler_DistinguishesSubscriptionsByAlgoId()
    {
        var client = new TestableOkxWebSocketApiClient();
        var request = new OkxSocketRequest(
            OkxSocketOperation.Subscribe,
            new OkxSocketRequestArgument
            {
                Channel = "algo-recurring-buy",
                InstrumentType = OkxInstrumentType.Spot,
                AlgoOrderId = "123"
            });

        var mismatchedMessage = JObject.Parse("""
        {
          "arg": {
            "channel": "algo-recurring-buy",
            "instType": "SPOT",
            "algoId": "999"
          },
          "data": [
            {
              "algoId": "999"
            }
          ]
        }
        """);

        Assert.False(client.InvokeMessageMatchesHandler(request, mismatchedMessage));
    }

    [Fact]
    public async Task UnsubscribeAsync_ReturnsTrueOnlyWhenAckMatchesFullArguments()
    {
        var client = new TestableOkxWebSocketApiClient();
        var request = new OkxSocketRequest(
            OkxSocketOperation.Subscribe,
            new OkxSocketRequestArgument
            {
                Channel = "account",
                Currency = "BTC",
                ExtraParameters = new Dictionary<string, string> { ["updateInterval"] = "0" }
            });

        var subscription = WebSocketSubscription.CreateForRequest(1, request, true, false, _ => { });

        var matchingConnection = new StubWebSocketConnection(
            client,
            JObject.Parse("""
            {
              "event": "unsubscribe",
                "arg": {
                "channel": "account",
                "ccy": "BTC",
                "extraParams": "{\"updateInterval\":\"0\"}"
              }
            }
            """));

        var mismatchedConnection = new StubWebSocketConnection(
            client,
            JObject.Parse("""
            {
              "event": "unsubscribe",
                "arg": {
                "channel": "account",
                "ccy": "ETH",
                "extraParams": "{\"updateInterval\":\"0\"}"
              }
            }
            """));

        Assert.True(await client.InvokeUnsubscribeAsync(matchingConnection, subscription));
        Assert.False(await client.InvokeUnsubscribeAsync(mismatchedConnection, subscription));

        var sentRequest = Assert.IsType<OkxSocketRequest>(matchingConnection.LastRequest);
        Assert.Equal(OkxSocketOperation.Unsubscribe, sentRequest.Operation);
        Assert.Single(sentRequest.Arguments);
        Assert.Equal("account", sentRequest.Arguments[0].Channel);
    }

    private sealed class TestableOkxWebSocketApiClient : OkxWebSocketApiClient
    {
        public TestableOkxWebSocketApiClient()
            : base(new OkxWebSocketApiOptions())
        {
        }

        public bool InvokeHandleSubscriptionResponse(object request, JToken data, out CallResult<object>? callResult)
        {
            var subscription = WebSocketSubscription.CreateForRequest(1, request, true, false, _ => { });
            return base.HandleSubscriptionResponse(null!, subscription, request, data, out callResult);
        }

        public bool InvokeMessageMatchesHandler(object request, JToken message)
            => base.MessageMatchesHandler(null!, message, request);

        public Task<bool> InvokeUnsubscribeAsync(WebSocketConnection connection, WebSocketSubscription subscription)
            => base.UnsubscribeAsync(connection, subscription);
    }

    private sealed class StubWebSocketConnection : WebSocketConnection
    {
        private readonly JToken _response;

        public object? LastRequest { get; private set; }

        public StubWebSocketConnection(WebSocketApiClient apiClient, JToken response)
            : base(
                BaseClient.LoggerFactory.CreateLogger("OKX.Api.Tests"),
                apiClient,
                new WebSocketClient(
                    BaseClient.LoggerFactory.CreateLogger("OKX.Api.Tests"),
                    new WebSocketParameters(new Uri("wss://localhost"), false)),
                "wss://localhost")
        {
            _response = response;
        }

        public override Task SendAndWaitAsync<T>(T obj, TimeSpan timeout, Func<JToken, bool> handler)
        {
            LastRequest = obj;
            handler(_response);
            return Task.CompletedTask;
        }
    }
}
