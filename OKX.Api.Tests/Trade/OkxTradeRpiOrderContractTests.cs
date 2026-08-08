using ApiSharp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OKX.Api.Base;
using OKX.Api.Tests.TestInfrastructure;
using OKX.Api.Trade;

namespace OKX.Api.Tests.Trade;

public class OkxTradeRpiOrderContractTests
{
    [Fact]
    public void PlaceOrderRequest_SerializesCurrentRpiContract()
    {
        var request = new OkxTradeOrderPlaceRequest
        {
            InstrumentId = "BTC-USDT-SWAP",
            TradeMode = OkxTradeMode.Cross,
            OrderSide = OkxTradeOrderSide.Buy,
            PositionSide = OkxTradePositionSide.Net,
            OrderType = OkxTradeOrderType.RetailPriceImprovementOrder,
            Size = 1m,
            Price = 100000m,
            RpiTakerAccess = true,
            RpiPriceRound = true,
        };

        var payload = Serialize(request);

        Assert.Equal("rpi", payload["ordType"]?.Value<string>());
        Assert.True(payload["rpiTakerAccess"]?.Value<bool>());
        Assert.True(payload["rpiPxRound"]?.Value<bool>());
        Assert.Null(payload["isElpTakerAccess"]);
    }

    [Fact]
    public void PlaceOrderRequest_SerializesCurrentSlippageContract()
    {
        var payload = Serialize(CreatePlaceRequest(0.0123m));

        Assert.Equal("market", payload["ordType"]?.Value<string>());
        Assert.Equal("0.0123", payload["slippagePct"]?.Value<string>());
    }

    [Fact]
    public void AmendOrderRequest_SerializesNonInheritedRpiControls()
    {
        var request = new OkxTradeOrderAmendRequest
        {
            InstrumentIdCode = 101,
            OrderId = 123456789,
            NewPrice = 100001m,
            RpiTakerAccess = true,
            RpiPriceRound = true,
        };

        var payload = Serialize(request);

        Assert.True(payload["rpiTakerAccess"]?.Value<bool>());
        Assert.True(payload["rpiPxRound"]?.Value<bool>());
    }

    [Fact]
    public void OrderResponses_ParseCurrentRpiOrderType()
    {
        var order = JsonConvert.DeserializeObject<OkxTradeOrder>("{\"ordType\":\"rpi\"}", SerializerOptions.WithConverters);

        Assert.NotNull(order);
        Assert.Equal(OkxTradeOrderType.RetailPriceImprovementOrder, order.OrderType);
    }

    [Fact]
    public void PublicTradeSource_MapsCurrentRpiNameAndKeepsLegacyAliasObsolete()
    {
        var trade = JsonConvert.DeserializeObject<OKX.Api.Public.OkxPublicTrade>("{\"source\":\"1\"}", SerializerOptions.WithConverters);

        Assert.NotNull(trade);
        Assert.Equal(OkxTradeOrderSource.RetailPriceImprovementOrder, trade.Source);
        Assert.Equal("1", ApiSharp.Converters.MapConverter.GetString(OkxTradeOrderSource.RetailPriceImprovementOrder));
#pragma warning disable CS0618
        Assert.Equal("1", ApiSharp.Converters.MapConverter.GetString(OkxTradeOrderSource.EnhancedLiquidityProgramOrder));
#pragma warning restore CS0618
        Assert.NotNull(typeof(OkxTradeOrderSource).GetField("EnhancedLiquidityProgramOrder")!
            .GetCustomAttributes(typeof(ObsoleteAttribute), false)
            .SingleOrDefault());
    }

    [Fact]
    public void OrdersChannelFixture_ParsesCurrentRpiAmendmentContract()
    {
        var json = FixtureReader.ReadManual("Trade", "ws-orders-rpi-amendment.json");
        var response = JsonConvert.DeserializeObject<OkxSocketUpdateResponse<List<OkxTradeOrder>>>(json, SerializerOptions.WithConverters);

        Assert.NotNull(response);
        var order = Assert.Single(response.Data);
        Assert.Equal("broker", order.Tag);
        Assert.Equal(100000m, order.NotionalUsd);
        Assert.Equal(1.2m, order.FillProfitAndLoss);
        Assert.Equal(-0.01m, order.FillFee);
        Assert.Equal("USDT", order.FillFeeCurrency);
        Assert.Equal(0.51m, order.FillPriceVolatility);
        Assert.Equal(100001m, order.FillPriceUsd);
        Assert.Equal(0.50m, order.FillMarkVolatility);
        Assert.Equal(99990m, order.FillForwardPrice);
        Assert.Equal(100000.5m, order.FillMarkPrice);
        Assert.Equal(OkxTradeOrderRole.Maker, order.ExecutionType);
        Assert.Equal(50000.5m, order.FillNotionalUsd);
        Assert.Equal(OkxTradeOrderAmendSource.RpiPriceRounding, order.AmendSource);
        Assert.Equal("amend-rpi-01", order.ClientRequestId);
        Assert.Equal(OkxTradeOrderAmendResult.Success, order.AmendResult);
        Assert.Equal(100002m, order.LastPrice);
        Assert.Equal("0", order.Code);
        Assert.Equal(string.Empty, order.Message);
    }

    [Fact]
    public void OrdersChannel_ParsesUnavailableCurrentFieldsAsNull()
    {
        const string json = "{\"notionalUsd\":\"\",\"fillPnl\":\"\",\"fillFee\":\"\",\"fillPxVol\":\"\",\"fillPxUsd\":\"\",\"fillMarkVol\":\"\",\"fillFwdPx\":\"\",\"fillMarkPx\":\"\",\"execType\":\"\",\"fillNotionalUsd\":\"\",\"amendSource\":\"\",\"amendResult\":\"\",\"lastPx\":\"\"}";

        var order = JsonConvert.DeserializeObject<OkxTradeOrder>(json, SerializerOptions.WithConverters);

        Assert.NotNull(order);
        Assert.Null(order.NotionalUsd);
        Assert.Null(order.FillProfitAndLoss);
        Assert.Null(order.FillFee);
        Assert.Null(order.FillPriceVolatility);
        Assert.Null(order.FillPriceUsd);
        Assert.Null(order.FillMarkVolatility);
        Assert.Null(order.FillForwardPrice);
        Assert.Null(order.FillMarkPrice);
        Assert.Null(order.ExecutionType);
        Assert.Null(order.FillNotionalUsd);
        Assert.Null(order.AmendSource);
        Assert.Null(order.AmendResult);
        Assert.Null(order.LastPrice);
    }

    [Fact]
    public async Task PlaceOrderClients_RejectInvalidValuesBeforeSending()
    {
        using var server = new LocalOkxRestServer(new Dictionary<string, string>());
        var restClient = new OkxRestApiClient(new OkxRestApiOptions(new OkxApiCredentials("key", "secret", "pass"))
        {
            AutoTimestamp = false,
            BaseAddress = server.BaseAddress,
        });

        foreach (var invalidSlippage in new[] { -0.0001m, 0.0501m, 0.01234m })
        {
            var request = CreatePlaceRequest(invalidSlippage);
            await Assert.ThrowsAnyAsync<ArgumentException>(() => restClient.Trade.PlaceOrderAsync(request));
        }

        var invalidPrices = CreatePlaceRequest(0.01m) with { Price = 1m, PriceUsd = 2m };
        await Assert.ThrowsAsync<ArgumentException>(() => restClient.Trade.PlaceOrderAsync(invalidPrices));
        await Assert.ThrowsAsync<ArgumentException>(() => restClient.Trade.PlaceOrderAsync(
            "BTC-USD-260828-100000-C",
            OkxTradeMode.Isolated,
            OkxTradeOrderSide.Buy,
            OkxTradePositionSide.Net,
            OkxTradeOrderType.LimitOrder,
            1m,
            price: 1m,
            priceUsd: 2m));

        using var socketClient = new OkxWebSocketApiClient();
        var socketRequest = CreatePlaceRequest(0.01234m) with { InstrumentIdCode = 101 };
        await Assert.ThrowsAnyAsync<ArgumentException>(() => socketClient.Trade.PlaceOrderAsync(socketRequest));

        Assert.Empty(server.Requests);
    }

    [Fact]
    public async Task PositionalPlaceOrder_SerializesCurrentPlaceFields()
    {
        using var server = new LocalOkxRestServer(new Dictionary<string, string>
        {
            ["POST /api/v5/trade/order"] = "{\"code\":\"0\",\"msg\":\"\",\"data\":[{\"ordId\":\"1\",\"clOrdId\":\"\",\"ts\":\"1783425600000\",\"sCode\":\"0\",\"sMsg\":\"\",\"subCode\":\"\"}]}",
        });
        var client = new OkxRestApiClient(new OkxRestApiOptions(new OkxApiCredentials("key", "secret", "pass"))
        {
            AutoTimestamp = false,
            BaseAddress = server.BaseAddress,
        });

        var rpiResult = await client.Trade.PlaceOrderAsync(
            "BTC-USDT-SWAP",
            OkxTradeMode.Cross,
            OkxTradeOrderSide.Buy,
            OkxTradePositionSide.Net,
            OkxTradeOrderType.RetailPriceImprovementOrder,
            1m,
            price: 100000m,
            rpiTakerAccess: true,
            rpiPriceRound: true);

        var slippageResult = await client.Trade.PlaceOrderAsync(
            "BTC-USDT",
            OkxTradeMode.Cash,
            OkxTradeOrderSide.Buy,
            OkxTradePositionSide.Net,
            OkxTradeOrderType.MarketOrder,
            1m,
            slippagePercentage: 0.0123m);

        var usdPriceResult = await client.Trade.PlaceOrderAsync(
            "BTC-USD-260828-100000-C",
            OkxTradeMode.Isolated,
            OkxTradeOrderSide.Buy,
            OkxTradePositionSide.Net,
            OkxTradeOrderType.LimitOrder,
            1m,
            priceUsd: 1234.56m);

        var volatilityPriceResult = await client.Trade.PlaceOrderAsync(
            "BTC-USD-260828-100000-C",
            OkxTradeMode.Isolated,
            OkxTradeOrderSide.Buy,
            OkxTradePositionSide.Net,
            OkxTradeOrderType.LimitOrder,
            1m,
            priceVolatility: 0.1234m);

        Assert.True(rpiResult.Success, rpiResult.Error?.ToString());
        Assert.True(slippageResult.Success, slippageResult.Error?.ToString());
        Assert.True(usdPriceResult.Success, usdPriceResult.Error?.ToString());
        Assert.True(volatilityPriceResult.Success, volatilityPriceResult.Error?.ToString());
        Assert.Equal(4, server.Requests.Count);

        var rpiPayload = JObject.Parse(server.Requests[0].Body);
        Assert.Equal("rpi", rpiPayload["ordType"]?.Value<string>());
        Assert.True(rpiPayload["rpiTakerAccess"]?.Value<bool>());
        Assert.True(rpiPayload["rpiPxRound"]?.Value<bool>());
        Assert.Null(rpiPayload["slippagePct"]);

        var slippagePayload = JObject.Parse(server.Requests[1].Body);
        Assert.Equal("market", slippagePayload["ordType"]?.Value<string>());
        Assert.Equal("0.0123", slippagePayload["slippagePct"]?.Value<string>());
        Assert.Null(slippagePayload["rpiTakerAccess"]);
        Assert.Null(slippagePayload["posSide"]);
        Assert.Null(slippagePayload["speedBump"]);

        var usdPricePayload = JObject.Parse(server.Requests[2].Body);
        Assert.Equal(JTokenType.String, usdPricePayload["pxUsd"]?.Type);
        Assert.Equal("1234.56", usdPricePayload["pxUsd"]?.Value<string>());

        var volatilityPricePayload = JObject.Parse(server.Requests[3].Body);
        Assert.Equal(JTokenType.String, volatilityPricePayload["pxVol"]?.Type);
        Assert.Equal("0.1234", volatilityPricePayload["pxVol"]?.Value<string>());
    }

    [Fact]
    public async Task BatchPlaceAndAmendClients_EnforceDocumentedOrderCount()
    {
        using var server = new LocalOkxRestServer(new Dictionary<string, string>());
        var restClient = new OkxRestApiClient(new OkxRestApiOptions(new OkxApiCredentials("key", "secret", "pass"))
        {
            AutoTimestamp = false,
            BaseAddress = server.BaseAddress,
        });

        await Assert.ThrowsAsync<ArgumentException>(() => restClient.Trade.PlaceOrdersAsync([]));
        await Assert.ThrowsAsync<ArgumentException>(() => restClient.Trade.PlaceOrdersAsync(
            Enumerable.Range(0, 21).Select(_ => CreatePlaceRequest(0.01m))));
        await Assert.ThrowsAsync<ArgumentException>(() => restClient.Trade.AmendOrdersAsync([]));
        await Assert.ThrowsAsync<ArgumentException>(() => restClient.Trade.AmendOrdersAsync(
            Enumerable.Range(0, 21).Select(_ => new OkxTradeOrderAmendRequest())));

        using var socketClient = new OkxWebSocketApiClient();
        await Assert.ThrowsAsync<ArgumentException>(() => socketClient.Trade.PlaceOrdersAsync([]));
        await Assert.ThrowsAsync<ArgumentException>(() => socketClient.Trade.AmendOrdersAsync([]));

        Assert.Empty(server.Requests);
    }

    private static OkxTradeOrderPlaceRequest CreatePlaceRequest(decimal slippagePercentage)
        => new()
        {
            InstrumentId = "BTC-USDT",
            TradeMode = OkxTradeMode.Cash,
            OrderSide = OkxTradeOrderSide.Buy,
            PositionSide = OkxTradePositionSide.Net,
            OrderType = OkxTradeOrderType.MarketOrder,
            Size = 1m,
            SlippagePercentage = slippagePercentage,
        };

    private static JObject Serialize(object request)
        => JObject.Parse(JsonConvert.SerializeObject(request, SerializerOptions.WithConverters));
}
