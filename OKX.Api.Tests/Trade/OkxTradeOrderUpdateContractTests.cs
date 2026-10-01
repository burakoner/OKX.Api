using ApiSharp.Models;
using ApiSharp.WebSocket;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OKX.Api.Base;
using OKX.Api.Common;
using OKX.Api.Trade;

namespace OKX.Api.Tests.Trade;

// Synthetic server pushes test routing and the real Trade callback without opening a connection.
// They do not test OKX's matching engine or the timing of live notifications.
public class OkxTradeOrderUpdateContractTests
{
    [Theory]
    [InlineData(OkxInstrumentType.Any, "ANY")]
    [InlineData(OkxInstrumentType.Spot, "SPOT")]
    [InlineData(OkxInstrumentType.Margin, "MARGIN")]
    [InlineData(OkxInstrumentType.Swap, "SWAP")]
    [InlineData(OkxInstrumentType.Futures, "FUTURES")]
    [InlineData(OkxInstrumentType.Option, "OPTION")]
    [InlineData(OkxInstrumentType.Events, "EVENTS")]
    public async Task Subscribe_UsesPrivateAuthenticatedChannelAndHasNoInitialSnapshot(OkxInstrumentType type, string wireType)
    {
        using var client = new RecordingClient();
        using var cancellation = new CancellationTokenSource();
        var received = new List<OkxTradeOrder>();

        await client.Trade.SubscribeToOrderUpdatesAsync(received.Add, type, ct: cancellation.Token);

        Assert.Equal(OkxSocketEndpoint.Private, client.Endpoint);
        Assert.True(client.Authenticated);
        Assert.Equal(cancellation.Token, client.Cancellation);
        Assert.Equal(OkxSocketOperation.Subscribe, client.Request!.Operation);
        var argument = Assert.Single(client.Request.Arguments);
        Assert.Equal("orders", argument.Channel);
        Assert.Equal(type, argument.InstrumentType);
        var json = JObject.FromObject(client.Request, JsonSerializer.Create(SerializerOptions.WithConverters));
        Assert.Equal(wireType, json["args"]![0]!["instType"]!.Value<string>());
        Assert.Null(json["args"]![0]!["instFamily"]);
        Assert.Null(json["args"]![0]!["instId"]);
        Assert.Empty(received);
    }

    [Fact]
    public async Task SubscribeMultipleSymbols_PreservesFamilyAndInstrumentFilters()
    {
        using var client = new RecordingClient();
        var symbols = new[]
        {
            new OkxSocketSymbolRequest(OkxInstrumentType.Swap, "BTC-USDT", null),
            new OkxSocketSymbolRequest(OkxInstrumentType.Option, null, "BTC-USD-261225-100000-C"),
        };
        await client.Trade.SubscribeToOrderUpdatesAsync(_ => { }, symbols);

        Assert.Collection(client.Request!.Arguments,
            argument =>
            {
                Assert.Equal("BTC-USDT", argument.InstrumentFamily);
                Assert.Equal(OkxInstrumentType.Swap, argument.InstrumentType);
                Assert.Null(argument.InstrumentId);
            },
            argument =>
            {
                Assert.Null(argument.InstrumentFamily);
                Assert.Equal(OkxInstrumentType.Option, argument.InstrumentType);
                Assert.Equal("BTC-USD-261225-100000-C", argument.InstrumentId);
            });
        Assert.Equal("BTC-USDT", symbols[0].InstrumentFamily);
        Assert.Null(symbols[0].InstrumentId);
    }

    [Theory]
    [InlineData("post_only", "31")]
    [InlineData("mmp_and_post_only", "31")]
    [InlineData("rpi", "45")]
    [InlineData("elp", "45")]
    public async Task RejectedMakerPlacement_ForwardsCanceledAsFirstAndOnlyUpdate(string orderType, string cancelSource)
    {
        using var client = new RecordingClient();
        var received = new List<OkxTradeOrder>();
        await client.Trade.SubscribeToOrderUpdatesAsync(received.Add, OkxInstrumentType.Any);
        var row = Order(orderType, "canceled");
        row["cancelSource"] = cancelSource;
        row["code"] = "0";

        Assert.True(client.Publish(Push(row)));

        var order = Assert.Single(received);
        Assert.Equal(OkxTradeOrderState.Canceled, order.OrderState);
        Assert.Equal(cancelSource, order.CancelSource);
        Assert.Equal("0", order.Code); // Cancellation source, not channel code, explains this outcome.
        Assert.Equal(0m, order.AccumulatedFillQuantity);
    }

    [Theory]
    [InlineData("post_only", false)]
    [InlineData("post_only", true)]
    [InlineData("mmp_and_post_only", false)]
    [InlineData("mmp_and_post_only", true)]
    [InlineData("rpi", false)]
    [InlineData("rpi", true)]
    public async Task SuccessfulMakerPlacement_ForwardsLiveThenActualFillSequence(string orderType, bool partialFill)
    {
        using var client = new RecordingClient();
        var received = new List<OkxTradeOrder>();
        await client.Trade.SubscribeToOrderUpdatesAsync(received.Add, OkxInstrumentType.Any);
        client.Publish(Push(Order(orderType, "live")));
        if (partialFill)
        {
            var partial = Order(orderType, "partially_filled");
            partial["tradeId"] = "201";
            partial["fillSz"] = "0.25";
            partial["accFillSz"] = "0.25";
            client.Publish(Push(partial));
        }
        var filled = Order(orderType, "filled");
        filled["tradeId"] = "202";
        filled["fillSz"] = partialFill ? "0.75" : "1";
        filled["accFillSz"] = "1";
        client.Publish(Push(filled));

        Assert.Equal(partialFill
            ? new[] { OkxTradeOrderState.Live, OkxTradeOrderState.PartiallyFilled, OkxTradeOrderState.Filled }
            : new[] { OkxTradeOrderState.Live, OkxTradeOrderState.Filled }, received.Select(order => order.OrderState));
        Assert.Equal(1m, received.Last().AccumulatedFillQuantity);
        Assert.Equal(202L, received.Last().TradeId);
    }

    [Theory]
    [InlineData("post_only", "4", OkxTradeOrderAmendSource.SystemReduceOnly)]
    [InlineData("rpi", "6", OkxTradeOrderAmendSource.RpiPriceRounding)]
    public async Task InitialSystemAdjustment_ForwardsBothLiveUpdatesIncludingFirstMetadata(
        string orderType, string source, OkxTradeOrderAmendSource expectedSource)
    {
        using var client = new RecordingClient();
        var received = new List<OkxTradeOrder>();
        await client.Trade.SubscribeToOrderUpdatesAsync(received.Add, OkxInstrumentType.Any);
        var adjusted = Order(orderType, "live");
        adjusted["amendSource"] = source;
        adjusted["amendResult"] = "0";
        if (source == "4")
        {
            adjusted["instType"] = "SWAP";
            adjusted["instId"] = "BTC-USDT-SWAP";
            adjusted["reduceOnly"] = "true";
            adjusted["sz"] = "0.5";
        }
        else adjusted["px"] = "100000.5";
        var live = (JObject)adjusted.DeepClone();
        live["amendSource"] = "";
        live["amendResult"] = "";
        client.Publish(Push(adjusted));
        client.Publish(Push(live));

        Assert.Equal(2, received.Count); // Same ordId/state/uTime must not suppress one of the updates.
        Assert.All(received, order => Assert.Equal(OkxTradeOrderState.Live, order.OrderState));
        Assert.Equal(expectedSource, received[0].AmendSource);
        Assert.Equal(OkxTradeOrderAmendResult.Success, received[0].AmendResult);
        Assert.Null(received[1].AmendSource);
        Assert.Null(received[1].AmendResult);
        Assert.Equal(source == "4" ? 0.5m : 1m, received[0].Quantity);
        Assert.Equal(source == "6" ? 100000.5m : 100000m, received[0].Price);
    }

    [Theory]
    [InlineData("limit", "filled", OkxTradeOrderState.Filled)]
    [InlineData("market", "filled", OkxTradeOrderState.Filled)]
    [InlineData("ioc", "canceled", OkxTradeOrderState.Canceled)]
    [InlineData("fok", "canceled", OkxTradeOrderState.Canceled)]
    [InlineData("optimal_limit_ioc", "partially_filled", OkxTradeOrderState.PartiallyFilled)]
    public async Task OtherOrderTypes_AreForwardedWithoutApplyingMakerPlacementRules(string orderType, string state, OkxTradeOrderState expected)
    {
        using var client = new RecordingClient();
        var received = new List<OkxTradeOrder>();
        await client.Trade.SubscribeToOrderUpdatesAsync(received.Add, OkxInstrumentType.Any);
        var row = Order(orderType, state);
        if (orderType == "optimal_limit_ioc")
        {
            row["instType"] = "SWAP";
            row["instId"] = "BTC-USDT-SWAP";
        }
        client.Publish(Push(row));

        Assert.Equal(expected, Assert.Single(received).OrderState);
    }

    [Fact]
    public async Task MixedRowsAndRepeatedPushes_AreForwardedWithoutWrapperDeduplication()
    {
        using var client = new RecordingClient();
        var received = new List<OkxTradeOrder>();
        await client.Trade.SubscribeToOrderUpdatesAsync(received.Add, OkxInstrumentType.Any);
        var canceled = Order("post_only", "canceled");
        var filled = Order("limit", "filled");
        filled["ordId"] = "102";
        filled["tradeId"] = "201";
        var mmpCanceled = Order("mmp_and_post_only", "mmp_canceled");
        mmpCanceled["ordId"] = "103";
        mmpCanceled["cancelSource"] = "39";
        var message = Push(canceled, filled);
        var optionMessage = Push(mmpCanceled);
        client.Publish(message);
        client.Publish(optionMessage);
        message["data"]![0]!["uTime"] = "1787220000001";
        client.Publish(message);
        client.Publish(optionMessage);

        Assert.Equal(new long[] { 101, 102, 103, 101, 102, 103 }, received.Select(order => order.OrderId));
        Assert.Equal(OkxTradeOrderState.MmpCanceled, received[2].OrderState);
        Assert.Equal("39", received[2].CancelSource);
        Assert.False(client.Publish(new JObject { ["arg"] = message["arg"]!.DeepClone(), ["data"] = new JArray() }));
        Assert.Equal(6, received.Count);
    }

    [Fact]
    public async Task FilledMarketOrder_WithZeroLastFillQuantity_IsNotSuppressed()
    {
        using var client = new RecordingClient();
        var received = new List<OkxTradeOrder>();
        await client.Trade.SubscribeToOrderUpdatesAsync(received.Add, OkxInstrumentType.Spot);
        var row = Order("market", "filled");
        row["fillSz"] = "0";
        row["accFillSz"] = "1";
        row["tradeId"] = "";
        client.Publish(Push(row));

        var order = Assert.Single(received);
        Assert.Equal(OkxTradeOrderState.Filled, order.OrderState);
        Assert.Equal(0m, order.FillQuantity);
        Assert.Equal(1m, order.AccumulatedFillQuantity);
        Assert.Null(order.TradeId);
    }

    [Theory]
    [InlineData(null, "")]
    [InlineData("", "")]
    [InlineData("opaque-future-server-value", "opaque-future-server-value")]
    public void RiskBypassResult_IsPreservedWithoutInventingAnEnum(string? wireValue, string expected)
    {
        var row = Order("limit", "live");
        if (wireValue is not null) row["riskBypassResult"] = wireValue;
        var order = row.ToObject<OkxTradeOrder>(JsonSerializer.Create(SerializerOptions.WithConverters))!;

        Assert.Equal(expected, order.RiskBypassResult);
        Assert.Equal(0m, order.AveragePrice);
    }

    [Theory]
    [InlineData("-0.1", "0.0001")]
    [InlineData("0.1", "-0.0001")]
    [InlineData("", "")]
    public void MakerSellFeeAndRebate_PreserveSignsAndQuoteBaseCurrencies(string fee, string rebate)
    {
        var row = Order("limit", "filled");
        row["side"] = "sell";
        row["execType"] = "M";
        row["fee"] = fee;
        row["feeCcy"] = "USDT";
        row["rebate"] = rebate;
        row["rebateCcy"] = "BTC";
        var order = row.ToObject<OkxTradeOrder>(JsonSerializer.Create(SerializerOptions.WithConverters))!;

        Assert.Equal(fee.Length == 0 ? (decimal?)null : decimal.Parse(fee, System.Globalization.CultureInfo.InvariantCulture), order.Fee);
        Assert.Equal(rebate.Length == 0 ? (decimal?)null : decimal.Parse(rebate, System.Globalization.CultureInfo.InvariantCulture), order.Rebate);
        Assert.Equal("USDT", order.FeeCurrency);
        Assert.Equal("BTC", order.RebateCurrency);
    }

    [Theory]
    [InlineData("normal", OkxOrderCategory.Normal, 1)]
    [InlineData("twap", OkxOrderCategory.TWAP, 2)]
    [InlineData("adl", OkxOrderCategory.ADL, 3)]
    [InlineData("full_liquidation", OkxOrderCategory.FullLiquidation, 4)]
    [InlineData("partial_liquidation", OkxOrderCategory.PartialLiquidation, 5)]
    [InlineData("delivery", OkxOrderCategory.Delivery, 6)]
    [InlineData("ddh", OkxOrderCategory.DDH, 7)]
    [InlineData("auto_conversion", OkxOrderCategory.AutoConversion, 8)]
    public void Category_MapsEveryCurrentValueAndPreservesExistingEnumNumbers(string wireValue, OkxOrderCategory expected, byte number)
    {
        var order = JsonConvert.DeserializeObject<OkxTradeOrder>($"{{\"category\":\"{wireValue}\"}}", SerializerOptions.WithConverters)!;
        Assert.Equal(expected, order.Category);
        Assert.Equal(number, (byte)expected);
        Assert.Equal(wireValue, JObject.FromObject(order, JsonSerializer.Create(SerializerOptions.WithConverters))["category"]!.Value<string>());
    }

    [Fact]
    public void ChannelEnvelopes_PreserveIdentityAndOptionalAcknowledgementMetadata()
    {
        const string json = """
        {"id":"1512","event":"subscribe","connId":"a4d3ae55",
         "arg":{"channel":"orders","instType":"SWAP","instFamily":"BTC-USDT","instId":"BTC-USDT-SWAP","uid":"test-user"}}
        """;
        var response = JsonConvert.DeserializeObject<OkxSocketUpdateResponse<List<OkxTradeOrder>>>(json, SerializerOptions.WithConverters)!;
        Assert.Equal("1512", response.RequestId);
        Assert.Equal("a4d3ae55", response.ConnectionId);
        Assert.Equal(OkxInstrumentType.Swap, response.Arguments!.InstrumentType);
        Assert.Equal("BTC-USDT", response.Arguments.InstrumentFamily);
        Assert.Equal("BTC-USDT-SWAP", response.Arguments.InstrumentId);
        Assert.Equal("test-user", response.Arguments.UserId);
        var otherChannel = JsonConvert.DeserializeObject<OkxSocketUpdateResponse<object>>("{\"arg\":{\"channel\":\"account\"}}", SerializerOptions.WithConverters)!;
        Assert.Null(otherChannel.RequestId);
        Assert.Null(otherChannel.ConnectionId);
        Assert.Null(otherChannel.Arguments!.InstrumentType);
        Assert.Null(otherChannel.Arguments.InstrumentFamily);
    }

    private static JObject Order(string orderType, string state)
    {
        var option = orderType == "mmp_and_post_only";
        var price = option ? "0.02" : "100000";
        var filledQuantity = state == "filled" ? "1" : state == "partially_filled" ? "0.25" : "0";
        return new JObject
        {
            ["ordId"] = "101", ["instType"] = option ? "OPTION" : "SPOT",
            ["instId"] = option ? "BTC-USD-261225-100000-C" : "BTC-USDT",
            ["ordType"] = orderType, ["state"] = state, ["sz"] = "1", ["px"] = price,
            ["avgPx"] = filledQuantity == "0" ? "0" : price,
            ["accFillSz"] = filledQuantity, ["fillSz"] = filledQuantity,
            ["tradeId"] = filledQuantity == "0" ? "" : "200",
            ["amendSource"] = "", ["amendResult"] = "", ["cancelSource"] = "",
            ["cTime"] = "1787220000000", ["uTime"] = "1787220000000",
        };
    }

    private static JObject Push(params JObject[] rows) => new()
    {
        ["arg"] = new JObject
        {
            ["channel"] = "orders", ["instType"] = rows[0]["instType"]!.DeepClone(),
            ["instId"] = rows[0]["instId"]!.DeepClone(), ["uid"] = "test-user",
        },
        ["data"] = new JArray(rows),
    };

    private sealed class RecordingClient : OkxWebSocketApiClient
    {
        private Action<JObject>? _publish;
        public OkxSocketRequest? Request { get; private set; }
        public OkxSocketEndpoint Endpoint { get; private set; }
        public bool Authenticated { get; private set; }
        public CancellationToken Cancellation { get; private set; }

        internal override Task<CallResult<WebSocketUpdateSubscription>> RootSubscribeAsync<T>(
            OkxSocketEndpoint endpoint, object request, string? identifier, bool authenticated,
            Action<WebSocketDataEvent<T>> dataHandler, CancellationToken ct)
        {
            Endpoint = endpoint;
            Request = Assert.IsType<OkxSocketRequest>(request);
            Authenticated = authenticated;
            Cancellation = ct;
            _publish = message => dataHandler(new WebSocketDataEvent<T>(
                message.ToObject<T>(JsonSerializer.Create(SerializerOptions.WithConverters))!, DateTime.UnixEpoch));
            return Task.FromResult(new CallResult<WebSocketUpdateSubscription>(new ServerError("Test-only subscription; no network")));
        }

        public bool Publish(JObject message)
        {
            if (!base.MessageMatchesHandler(null!, message, Request!)) return false;
            _publish!(message);
            return true;
        }
    }
}
