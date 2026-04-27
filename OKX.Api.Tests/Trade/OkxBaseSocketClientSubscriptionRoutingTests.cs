using ApiSharp;
using ApiSharp.Models;
using ApiSharp.WebSocket;
using Newtonsoft.Json.Linq;
using OKX.Api.Base;
using OKX.Api.Common;

namespace OKX.Api.Tests.Trade;

public class OkxBaseSocketClientSubscriptionRoutingTests
{
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
