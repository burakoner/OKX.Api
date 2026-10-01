using System.Reflection;
using ApiSharp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OKX.Api.Base;
using OKX.Api.Tests.TestInfrastructure;
using OKX.Api.Trade;

namespace OKX.Api.Tests.Trade;

public class OkxTradeAmendContractTests
{
    private const string Acknowledgement = "{\"code\":\"0\",\"msg\":\"\",\"data\":[{\"ordId\":\"590909145319051111\",\"clOrdId\":\"\",\"reqId\":\"amend-01\",\"ts\":\"1695190491421\",\"sCode\":\"0\",\"sMsg\":\"\",\"subCode\":\"\"}]}";

    [Theory]
    [InlineData("newPx")]
    [InlineData("newPxUsd")]
    [InlineData("newPxVol")]
    public async Task RestSingleAndBatchAmend_SendStringPricesAndPreserveRpiControls(string priceField)
    {
        using var server = new LocalOkxRestServer(new Dictionary<string, string>
        {
            ["POST /api/v5/trade/amend-order"] = Acknowledgement,
            ["POST /api/v5/trade/amend-batch-orders"] = Acknowledgement,
        });
        var client = CreateClient(server);
        var request = CreateRequest(priceField);

        var single = await client.Trade.AmendOrderAsync("BTC-USD-260828-100000-C",
            orderId: request.OrderId, newPrice: request.NewPrice, newPriceUsd: request.NewPriceUsd,
            newPriceVolatility: request.NewPriceVolatility, rpiTakerAccess: true, rpiPriceRound: true);
        var batch = await client.Trade.AmendOrdersAsync([request]);

        Assert.True(single.Success, single.Error?.ToString());
        Assert.True(batch.Success, batch.Error?.ToString());
        Assert.Equal(1695190491421L, single.Data.Timestamp);
        Assert.Equal(1695190491421L, Assert.Single(batch.Data).Timestamp);
        Assert.Equal(DateTimeKind.Utc, single.Data.Time.Kind);
        Assert.Equal(DateTimeOffset.FromUnixTimeMilliseconds(1695190491421).UtcDateTime, single.Data.Time);
        Assert.Equal(2, server.Requests.Count);
        AssertAmendPayload(JObject.Parse(server.Requests[0].Body), priceField);
        AssertAmendPayload(Assert.IsType<JObject>(Assert.Single(JArray.Parse(server.Requests[1].Body))), priceField);
        Assert.Equal(101L, request.InstrumentIdCode); // REST payload cloning must not mutate the caller's request.
    }

    [Theory]
    [InlineData("newPx")]
    [InlineData("newPxUsd")]
    [InlineData("newPxVol")]
    public void SocketSingleAndBatchAmend_UseStringIdsAndPricesWithoutChangingRequestedPrice(string priceField)
    {
        var original = CreateRequest(priceField);
        var method = typeof(OkxTradeSocketClient).GetMethod("CreateSocketAmendOrderRequest", BindingFlags.NonPublic | BindingFlags.Static);
        Assert.NotNull(method);
        var request = Assert.IsType<OkxTradeOrderAmendRequest>(method!.Invoke(null, [original]));

        foreach (var operation in new[] { OkxSocketOperation.AmendOrder, OkxSocketOperation.BatchAmendOrders })
        {
            var command = SerializeCommand(operation, new[] { request }, null);
            var payload = Assert.IsType<JObject>(Assert.Single(command["args"]!.Values<JObject>()));
            AssertAmendPayload(payload, priceField, isSocket: true);
            Assert.Null(command["expTime"]);
        }

        Assert.Equal(original.NewPrice, request.NewPrice);
        Assert.Equal(original.NewPriceUsd, request.NewPriceUsd);
        Assert.Equal(original.NewPriceVolatility, request.NewPriceVolatility);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    public async Task AmendClients_RejectEveryConflictingPricePairBeforeSending(int pair)
    {
        using var server = new LocalOkxRestServer(new Dictionary<string, string>());
        var client = CreateClient(server);
        using var socket = new OkxWebSocketApiClient();
        var request = CreateRequest("newPx") with
        {
            NewPrice = pair == 2 ? null : 1m,
            NewPriceUsd = pair == 1 ? null : 2m,
            NewPriceVolatility = pair == 0 ? null : 0.5m,
        };

        await Assert.ThrowsAsync<ArgumentException>(() => client.Trade.AmendOrderAsync("BTC-USDT", orderId: 1,
            newPrice: request.NewPrice, newPriceUsd: request.NewPriceUsd, newPriceVolatility: request.NewPriceVolatility));
        await Assert.ThrowsAsync<ArgumentException>(() => client.Trade.AmendOrdersAsync([request]));
        await Assert.ThrowsAsync<ArgumentException>(() => socket.Trade.AmendOrderAsync(request));
        await Assert.ThrowsAsync<ArgumentException>(() => socket.Trade.AmendOrdersAsync([request]));
        Assert.Empty(server.Requests);
    }

    [Fact]
    public async Task BatchAmend_RejectsNullEntriesBeforeSending()
    {
        using var server = new LocalOkxRestServer(new Dictionary<string, string>());
        var client = CreateClient(server);
        await Assert.ThrowsAsync<ArgumentException>(() => client.Trade.AmendOrdersAsync([null!]));
        Assert.Empty(server.Requests);
    }

    [Theory]
    [InlineData(OkxSocketOperation.Order)]
    [InlineData(OkxSocketOperation.BatchOrders)]
    [InlineData(OkxSocketOperation.AmendOrder)]
    [InlineData(OkxSocketOperation.BatchAmendOrders)]
    public void SocketTradeDeadlines_AreOptionalStringFieldsAtCommandRoot(OkxSocketOperation operation)
    {
        var place = operation is OkxSocketOperation.Order or OkxSocketOperation.BatchOrders;
        var command = place
            ? SerializeCommand(operation, new[] { new OkxTradeOrderPlaceRequest { InstrumentIdCode = 101 } }, 1790841600123)
            : SerializeCommand(operation, new[] { CreateRequest("newPx") }, 1790841600123);
        var noDeadline = place
            ? SerializeCommand(operation, new[] { new OkxTradeOrderPlaceRequest { InstrumentIdCode = 101 } }, null)
            : SerializeCommand(operation, new[] { CreateRequest("newPx") }, null);

        Assert.Equal(JTokenType.String, command["expTime"]?.Type);
        Assert.Equal("1790841600123", command["expTime"]?.Value<string>());
        Assert.All(command["args"]!.Values<JObject>(), argument => Assert.Null(Assert.IsType<JObject>(argument)["expTime"]));
        Assert.Null(noDeadline["expTime"]);
    }

    private static JObject SerializeCommand<T>(OkxSocketOperation operation, IEnumerable<T> requests, long? expiryTimestamp)
    {
        var factory = typeof(OkxTradeSocketClient).GetMethod("CreateTradeRequest", BindingFlags.NonPublic | BindingFlags.Static);
        Assert.NotNull(factory);
        var command = factory!.MakeGenericMethod(typeof(T)).Invoke(null, ["req-01", operation, requests, expiryTimestamp]);
        return JObject.Parse(JsonConvert.SerializeObject(command, SerializerOptions.WithConverters));
    }

    private static void AssertAmendPayload(JObject payload, string priceField, bool isSocket = false)
    {
        Assert.Equal(JTokenType.String, payload["ordId"]?.Type);
        Assert.Equal("590909145319051111", payload["ordId"]?.Value<string>());
        Assert.Equal(JTokenType.String, payload[priceField]?.Type);
        Assert.Equal(priceField == "newPxVol" ? "0.1234" : "1234.5678", payload[priceField]?.Value<string>());
        Assert.True(payload["rpiTakerAccess"]?.Value<bool>());
        Assert.True(payload["rpiPxRound"]?.Value<bool>());
        if (isSocket)
        {
            Assert.Equal(101L, payload["instIdCode"]?.Value<long>());
            Assert.Null(payload["instId"]);
        }
        else
        {
            Assert.Equal("BTC-USD-260828-100000-C", payload["instId"]?.Value<string>());
            Assert.Null(payload["instIdCode"]);
        }
        foreach (var other in new[] { "newPx", "newPxUsd", "newPxVol" }.Where(field => field != priceField))
            Assert.Null(payload[other]);
    }

    private static OkxTradeOrderAmendRequest CreateRequest(string priceField)
        => new()
        {
#pragma warning disable CS0618 // InstrumentId remains required for REST batch amendment.
            InstrumentId = "BTC-USD-260828-100000-C",
#pragma warning restore CS0618
            InstrumentIdCode = 101,
            OrderId = 590909145319051111,
            NewPrice = priceField == "newPx" ? 1234.5678m : null,
            NewPriceUsd = priceField == "newPxUsd" ? 1234.5678m : null,
            NewPriceVolatility = priceField == "newPxVol" ? 0.1234m : null,
            RpiTakerAccess = true,
            RpiPriceRound = true,
        };

    private static OkxRestApiClient CreateClient(LocalOkxRestServer server)
        => new(new OkxRestApiOptions(new OkxApiCredentials("key", "secret", "pass"))
        {
            AutoTimestamp = false,
            BaseAddress = server.BaseAddress,
        });
}
