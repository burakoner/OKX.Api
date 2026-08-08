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
    public async Task PlaceOrderClients_RejectInvalidSlippageBeforeSending()
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

        using var socketClient = new OkxWebSocketApiClient();
        var socketRequest = CreatePlaceRequest(0.01234m) with { InstrumentIdCode = 101 };
        await Assert.ThrowsAnyAsync<ArgumentException>(() => socketClient.Trade.PlaceOrderAsync(socketRequest));

        Assert.Empty(server.Requests);
    }

    [Fact]
    public async Task PositionalPlaceOrder_SerializesCurrentRpiAndSlippageFields()
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

        Assert.True(rpiResult.Success, rpiResult.Error?.ToString());
        Assert.True(slippageResult.Success, slippageResult.Error?.ToString());
        Assert.Equal(2, server.Requests.Count);

        var rpiPayload = JObject.Parse(server.Requests[0].Body);
        Assert.Equal("rpi", rpiPayload["ordType"]?.Value<string>());
        Assert.True(rpiPayload["rpiTakerAccess"]?.Value<bool>());
        Assert.True(rpiPayload["rpiPxRound"]?.Value<bool>());
        Assert.Null(rpiPayload["slippagePct"]);

        var slippagePayload = JObject.Parse(server.Requests[1].Body);
        Assert.Equal("market", slippagePayload["ordType"]?.Value<string>());
        Assert.Equal("0.0123", slippagePayload["slippagePct"]?.Value<string>());
        Assert.Null(slippagePayload["rpiTakerAccess"]);
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
