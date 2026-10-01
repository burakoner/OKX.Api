using ApiSharp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OKX.Api.Base;
using OKX.Api.Common;
using OKX.Api.Tests.TestInfrastructure;
using OKX.Api.Trade;

namespace OKX.Api.Tests.Trade;

public class OkxTradeRateLimiterClientTests
{
    private const string Ack = "{\"code\":\"0\",\"msg\":\"\",\"data\":[{\"ordId\":\"590909145319051111\",\"clOrdId\":\"\",\"ts\":\"1695190491421\",\"sCode\":\"0\",\"sMsg\":\"\"}]}";

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task RestAndSocketSingleAndOneOrderBatch_ShareTheSameOperationBudget(bool amend)
    {
        var limiter = CreateLimiter(lead: true);
        using var server = CreateServer();
        var rest = CreateRest(server, limiter);
        var anotherRest = CreateRest(server, limiter); // Same User ID can have multiple client instances/API keys.
        using var socket = new RecordingSocketClient(limiter);

        if (amend)
        {
            Assert.True((await rest.Trade.AmendOrderAsync("BTC-USDT-SWAP", orderId: 1, newPrice: 2)).Success);
            Assert.True((await anotherRest.Trade.AmendOrderAsync("BTC-USDT-SWAP", orderId: 1, newPrice: 2)).Success);
            Assert.True((await socket.Trade.AmendOrderAsync(Amend())).Success);
            Assert.True((await rest.Trade.AmendOrdersAsync([Amend()])).Success);
            Assert.IsType<ClientRateLimitError>((await socket.Trade.AmendOrdersAsync([Amend()], 1790841600123)).Error);
            Assert.IsType<ClientRateLimitError>((await rest.Trade.AmendOrderAsync("BTC-USDT-SWAP", orderId: 1, newPrice: 2)).Error);
        }
        else
        {
            Assert.True((await rest.Trade.PlaceOrderAsync("BTC-USDT-SWAP", OkxTradeMode.Cross, OkxTradeOrderSide.Buy,
                OkxTradePositionSide.Net, OkxTradeOrderType.LimitOrder, 1, price: 2)).Success);
            Assert.True((await anotherRest.Trade.PlaceOrderAsync(Place())).Success);
            Assert.True((await socket.Trade.PlaceOrderAsync(Place())).Success);
            Assert.True((await rest.Trade.PlaceOrdersAsync([Place()])).Success);
            Assert.IsType<ClientRateLimitError>((await socket.Trade.PlaceOrdersAsync([Place()], 1790841600123)).Error);
            Assert.IsType<ClientRateLimitError>((await rest.Trade.PlaceOrderAsync(Place())).Error);
        }

        Assert.Equal(3, server.Requests.Count);
        var command = SerializeCommand(Assert.Single(socket.Commands));
        Assert.Equal(amend ? "amend-order" : "order", command["op"]?.Value<string>());
        Assert.Null(command["expTime"]);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task RestAndSocketBatches_WeightOrdersAndPreservePayloadAndDeadlines(bool amend)
    {
        var limiter = CreateLimiter(lead: true);
        using var server = CreateServer();
        var rest = CreateRest(server, limiter);
        using var socket = new RecordingSocketClient(limiter);
        if (amend)
        {
            Assert.True((await rest.Trade.AmendOrdersAsync([Amend(), Amend()])).Success);
            Assert.True((await socket.Trade.AmendOrdersAsync([Amend(), Amend()], 1790841600123)).Success);
            Assert.IsType<ClientRateLimitError>((await rest.Trade.AmendOrdersAsync([Amend(), Amend()])).Error);
            Assert.IsType<ClientRateLimitError>((await socket.Trade.AmendOrdersAsync([Amend(), Amend()])).Error);
        }
        else
        {
            Assert.True((await rest.Trade.PlaceOrdersAsync([Place(), Place()])).Success);
            Assert.True((await socket.Trade.PlaceOrdersAsync([Place(), Place()], 1790841600123)).Success);
            Assert.IsType<ClientRateLimitError>((await rest.Trade.PlaceOrdersAsync([Place(), Place()])).Error);
            Assert.IsType<ClientRateLimitError>((await socket.Trade.PlaceOrdersAsync([Place(), Place()])).Error);
        }
        var body = JArray.Parse(Assert.Single(server.Requests).Body);
        Assert.Equal(2, body.Count);
        Assert.All(body.Values<JObject>(), row => Assert.Null(Assert.IsType<JObject>(row)["instIdCode"]));
        var command = SerializeCommand(Assert.Single(socket.Commands));
        Assert.Equal("1790841600123", command["expTime"]?.Value<string>());
        Assert.Equal(2, command["args"]!.Count());
    }

    [Fact]
    public async Task PendingSocketCommand_HoldsAccountBudgetAcrossTransportDelayAndCompletionWindow()
    {
        var elapsed = TimeSpan.Zero;
        var limiter = CreateLimiter(() => elapsed, accountLimit: 1);
        using var server = CreateServer();
        var rest = CreateRest(server, limiter);
        using var socket = new RecordingSocketClient(limiter) { Hold = new(TaskCreationOptions.RunContinuationsAsynchronously) };
        var pending = socket.Trade.PlaceOrderAsync(Place(), 1790841600123);
        Assert.Single(socket.Commands);
        elapsed = TimeSpan.FromMinutes(10);
        Assert.IsType<ClientRateLimitError>((await rest.Trade.AmendOrderAsync("BTC-USDT-SWAP", orderId: 1, newPrice: 2)).Error);
        socket.Hold.SetResult(true);
        Assert.True((await pending).Success);
        elapsed += TimeSpan.FromMilliseconds(1999);
        Assert.IsType<ClientRateLimitError>((await rest.Trade.PlaceOrderAsync(Place())).Error);
        elapsed += TimeSpan.FromMilliseconds(1);
        Assert.True((await rest.Trade.PlaceOrderAsync(Place())).Success);
        Assert.Single(server.Requests);
    }

    [Fact]
    public async Task TransportException_DoesNotRefundAnUncertainSubmission()
    {
        var elapsed = TimeSpan.Zero;
        var limiter = CreateLimiter(() => elapsed, accountLimit: 1);
        using var server = CreateServer();
        var rest = CreateRest(server, limiter);
        using var socket = new RecordingSocketClient(limiter) { Throw = true };
        await Assert.ThrowsAsync<IOException>(() => socket.Trade.PlaceOrderAsync(Place()));
        Assert.IsType<ClientRateLimitError>((await rest.Trade.PlaceOrderAsync(Place())).Error);
        elapsed = TimeSpan.FromSeconds(2);
        Assert.True((await rest.Trade.PlaceOrderAsync(Place())).Success);
        Assert.Single(server.Requests);
    }

    [Fact]
    public async Task MissingMetadataAndPreCancelledOrInvalidCommands_NeverSendOrConsumeBudget()
    {
        var limiter = CreateLimiter(accountLimit: 1);
        using var server = CreateServer();
        var rest = CreateRest(server, limiter);
        using var socket = new RecordingSocketClient(limiter);
        await Assert.ThrowsAsync<InvalidOperationException>(() => rest.Trade.PlaceOrderAsync(Place() with { InstrumentId = "UNKNOWN" }));
        await Assert.ThrowsAsync<InvalidOperationException>(() => socket.Trade.PlaceOrderAsync(Place() with { InstrumentIdCode = 999 }));
        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => rest.Trade.PlaceOrderAsync(Place(), new CancellationToken(true)));
        await Assert.ThrowsAsync<ArgumentException>(() => socket.Trade.AmendOrderAsync(Amend() with { NewPriceUsd = 1 }));
        Assert.Empty(server.Requests);
        Assert.Empty(socket.Commands);
        Assert.True((await socket.Trade.PlaceOrderAsync(Place())).Success);
        Assert.IsType<ClientRateLimitError>((await rest.Trade.PlaceOrderAsync(Place())).Error);
    }

    [Fact]
    public async Task ExhaustedPlaceAmendBudget_DoesNotBlockCancellation()
    {
        var limiter = CreateLimiter(accountLimit: 1);
        using var server = CreateServer();
        var rest = CreateRest(server, limiter);
        using var socket = new RecordingSocketClient(limiter);
        Assert.True((await rest.Trade.PlaceOrderAsync(Place())).Success);
        Assert.IsType<ClientRateLimitError>((await socket.Trade.AmendOrderAsync(Amend())).Error);
        Assert.True((await rest.Trade.CancelOrderAsync("BTC-USDT-SWAP", orderId: 1)).Success);
        Assert.True((await socket.Trade.CancelOrderAsync(new() { InstrumentIdCode = 101, OrderId = 1 })).Success);
        Assert.Equal(2, server.Requests.Count);
        Assert.Equal("cancel-order", SerializeCommand(Assert.Single(socket.Commands))["op"]?.Value<string>());
    }

    [Fact]
    public async Task KnownMmpPlacement_UsesNoAccountSlot_ButAmendmentRemainsConservative()
    {
        var limiter = CreateLimiter(accountLimit: 1);
        limiter.RegisterInstrument(OkxTradeRateLimiterTests.Instrument("BTC-USD-OPTION", 201, OkxInstrumentType.Option, "BTC-USD"), false);
        using var server = CreateServer();
        var rest = CreateRest(server, limiter);
        using var socket = new RecordingSocketClient(limiter);
        Assert.True((await rest.Trade.PlaceOrderAsync(Place())).Success); // Exhaust account budget.
        Assert.True((await socket.Trade.PlaceOrderAsync(Place() with
        {
            InstrumentId = "BTC-USD-OPTION", InstrumentIdCode = 201, OrderType = OkxTradeOrderType.MarektMakerProtectionAndPostOnly
        })).Success);
        Assert.True((await rest.Trade.PlaceOrderAsync(Place() with
        {
            InstrumentId = "BTC-USD-OPTION", InstrumentIdCode = 201, OrderType = OkxTradeOrderType.MarketMakerProtection
        })).Success);
        Assert.IsType<ClientRateLimitError>((await socket.Trade.AmendOrderAsync(Amend() with { InstrumentIdCode = 201 })).Error);
        Assert.Single(socket.Commands);
        Assert.Equal(2, server.Requests.Count);
    }

    [Fact]
    public async Task NoConfiguredGuard_PreservesExistingCallPathsWithoutRegistration()
    {
        using var server = CreateServer();
        var rest = CreateRest(server, null);
        using var socket = new RecordingSocketClient(null);
        Assert.True((await rest.Trade.PlaceOrderAsync(Place())).Success);
        Assert.True((await socket.Trade.PlaceOrdersAsync([Place(), Place()], 1790841600123)).Success);
        Assert.True((await rest.Trade.AmendOrdersAsync([Amend()])).Success);
        Assert.True((await socket.Trade.AmendOrderAsync(Amend())).Success);
        Assert.Equal(2, server.Requests.Count);
        Assert.Equal(2, socket.Commands.Count);
    }

    private static OkxTradeRateLimiter CreateLimiter(Func<TimeSpan>? clock = null, int accountLimit = 1000, bool lead = false)
    {
        var limiter = new OkxTradeRateLimiter(accountLimit, clock ?? (() => TimeSpan.Zero));
        limiter.RegisterInstrument(OkxTradeRateLimiterTests.Instrument("BTC-USDT-SWAP", 101), lead);
        return limiter;
    }

    private static LocalOkxRestServer CreateServer() => new(new Dictionary<string, string>
    {
        ["POST /api/v5/trade/order"] = Ack,
        ["POST /api/v5/trade/batch-orders"] = Ack,
        ["POST /api/v5/trade/amend-order"] = Ack,
        ["POST /api/v5/trade/amend-batch-orders"] = Ack,
        ["POST /api/v5/trade/cancel-order"] = Ack,
    });

    private static OkxRestApiClient CreateRest(LocalOkxRestServer server, OkxTradeRateLimiter? limiter)
        => new(new OkxRestApiOptions(new OkxApiCredentials("key", "secret", "pass"))
        {
            AutoTimestamp = false, BaseAddress = server.BaseAddress, TradeRateLimiter = limiter,
#pragma warning disable CS0612 // Isolate the shared guard; production transport guards are unchanged.
            RateLimiters = [],
#pragma warning restore CS0612
        });

    private static JObject SerializeCommand(object command)
        => JObject.Parse(JsonConvert.SerializeObject(command, SerializerOptions.WithConverters));

    private static OkxTradeOrderPlaceRequest Place() => new()
    {
        InstrumentId = "BTC-USDT-SWAP", InstrumentIdCode = 101, TradeMode = OkxTradeMode.Cross,
        OrderSide = OkxTradeOrderSide.Buy, OrderType = OkxTradeOrderType.LimitOrder, Size = 1, Price = 2,
    };

    private static OkxTradeOrderAmendRequest Amend() => new()
    {
#pragma warning disable CS0618 // REST batch requires instId; WS strips it from its cloned payload.
        InstrumentId = "BTC-USDT-SWAP",
#pragma warning restore CS0618
        InstrumentIdCode = 101, OrderId = 1, NewPrice = 2,
    };

    private sealed class RecordingSocketClient(OkxTradeRateLimiter? limiter)
        : OkxWebSocketApiClient(new OkxWebSocketApiOptions { TradeRateLimiter = limiter })
    {
        internal List<object> Commands { get; } = [];
        internal TaskCompletionSource<bool>? Hold { get; init; }
        internal bool Throw { get; init; }

        internal override async Task<CallResult<T>> RootQueryAsync<T>(OkxSocketEndpoint endpoint, object request, bool authenticated)
        {
            Assert.Equal(OkxSocketEndpoint.Private, endpoint);
            Assert.True(authenticated);
            Commands.Add(request);
            if (Hold is not null) await Hold.Task;
            if (Throw) throw new IOException("Synthetic uncertain submission failure");
            return new CallResult<T>(default(T)!, null);
        }
    }
}
