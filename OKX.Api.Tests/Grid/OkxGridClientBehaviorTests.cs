using Newtonsoft.Json.Linq;
using OKX.Api.Grid;
using OKX.Api.Tests.TestInfrastructure;

namespace OKX.Api.Tests.Grid;

public class OkxGridClientBehaviorTests
{
    [Fact]
    public async Task ClosePositionAsync_SendsMktCloseField()
    {
        using var server = CreateServer("/api/v5/tradingBot/grid/close-position", GridCloseResponse);
        var client = CreateClient(server);

        var result = await client.Grid.ClosePositionAsync(123, true, size: 2, price: 45000m);

        Assert.True(result.Success);
        var body = ParseBody(server);
        Assert.Equal("123", body["algoId"]?.Value<string>());
        Assert.True(body["mktClose"]!.Value<bool>());
        Assert.Null(body["instId"]);
        Assert.Equal("2", body["sz"]?.Value<string>());
        Assert.Equal("45000", body["px"]?.Value<string>());
    }

    [Fact]
    public async Task AmendOrderAsync_SerializesTriggerParamsAsArray()
    {
        using var server = CreateServer("/api/v5/tradingBot/grid/amend-order-algo", GridAmendResponse);
        var client = CreateClient(server);

        var result = await client.Grid.AmendOrderAsync(
            123,
            "BTC-USDT-SWAP",
            triggerParameters:
            [
                new OkxGridOrderAmendRequestTriggerParameters
                {
                    TriggerAction = OkxGridAlgoTriggerAction.Start,
                    TriggerStrategy = OkxGridTriggerStrategy.Price,
                    TriggerPrice = "45000"
                }
            ]);

        Assert.True(result.Success);
        var body = ParseBody(server);
        var triggerParamsToken = body["triggerParams"];
        Assert.NotNull(triggerParamsToken);
        var triggerParams = Assert.IsAssignableFrom<JArray>(triggerParamsToken);
        var trigger = Assert.Single(triggerParams.Values<JObject>());
        Assert.NotNull(trigger);
        Assert.Equal("start", trigger["triggerAction"]?.Value<string>());
        Assert.Equal("price", trigger["triggerStrategy"]?.Value<string>());
        Assert.Equal("45000", trigger["triggerPx"]?.Value<string>());
    }

    [Fact]
    public async Task AmendBasicParametersAsync_SendsExpectedBody()
    {
        using var server = CreateServer("/api/v5/tradingBot/grid/amend-algo-basic-param", GridBasicAmendResponse);
        var client = CreateClient(server);

        var result = await client.Grid.AmendBasicParametersAsync(123, 40000m, 50000m, 20, topupAmount: 50m);

        Assert.True(result.Success);
        var body = ParseBody(server);
        Assert.Equal("123", body["algoId"]?.Value<string>());
        Assert.Equal("40000", body["minPx"]?.Value<string>());
        Assert.Equal("50000", body["maxPx"]?.Value<string>());
        Assert.Equal("20", body["gridNum"]?.Value<string>());
        Assert.Equal("50", body["topupAmount"]?.Value<string>());
    }

    [Fact]
    public async Task CopyOrderAsync_SendsExpectedBody()
    {
        using var server = CreateServer("/api/v5/tradingBot/grid/copy-order-algo", GridPlaceResponse);
        var client = CreateClient(server);

        var result = await client.Grid.CopyOrderAsync(
            "BTC-USDT-SWAP",
            OkxGridAlgoOrderType.ContractGrid,
            789,
            leverage: 3,
            autoReserve: false,
            actualMarginSize: 150m,
            extraMarginSize: 25m,
            algoClientOrderId: "grid-copy-01");

        Assert.True(result.Success);
        var body = ParseBody(server);
        Assert.Equal("BTC-USDT-SWAP", body["instId"]?.Value<string>());
        Assert.Equal("contract_grid", body["algoOrdType"]?.Value<string>());
        Assert.Equal("789", body["sourceAlgoId"]?.Value<string>());
        Assert.Equal("3", body["lever"]?.Value<string>());
        Assert.False(body["autoReserve"]!.Value<bool>());
        Assert.Equal("150", body["actualMarginSz"]?.Value<string>());
        Assert.Equal("25", body["extraMarginSz"]?.Value<string>());
        Assert.Equal("grid-copy-01", body["algoClOrdId"]?.Value<string>());
        Assert.Equal("538a3965e538BCDE", body["tag"]?.Value<string>());
    }

    private static JObject ParseBody(LocalOkxRestServer server)
        => JObject.Parse(Assert.Single(server.Requests).Body);

    private static LocalOkxRestServer CreateServer(string path, string payload)
        => new(new Dictionary<string, string>
        {
            [$"POST {path}"] = payload,
        });

    private static OkxRestApiClient CreateClient(LocalOkxRestServer server)
    {
        var options = new OkxRestApiOptions(new OkxApiCredentials("key", "secret", "pass"))
        {
            AutoTimestamp = false,
            BaseAddress = server.BaseAddress,
        };

        return new OkxRestApiClient(options);
    }

    private const string GridCloseResponse = "{\"code\":\"0\",\"msg\":\"\",\"data\":[{\"algoId\":\"123\",\"algoClOrdId\":\"\",\"ordId\":\"456\",\"sCode\":\"0\",\"sMsg\":\"\"}]}";
    private const string GridAmendResponse = "{\"code\":\"0\",\"msg\":\"\",\"data\":[{\"algoId\":\"123\",\"algoClOrdId\":\"grid-amend-01\",\"sCode\":\"0\",\"sMsg\":\"\"}]}";
    private const string GridBasicAmendResponse = "{\"code\":\"0\",\"msg\":\"\",\"data\":[{\"algoId\":\"123\",\"maxTopupAmount\":\"100\",\"requiredTopupAmount\":\"50\",\"sCode\":\"0\",\"sMsg\":\"\"}]}";
    private const string GridPlaceResponse = "{\"code\":\"0\",\"msg\":\"\",\"data\":[{\"algoId\":\"789\",\"algoClOrdId\":\"grid-copy-01\",\"tag\":\"538a3965e538BCDE\",\"sCode\":\"0\",\"sMsg\":\"\"}]}";
}
