using Newtonsoft.Json.Linq;
using OKX.Api.Account;
using OKX.Api.Tests.TestInfrastructure;
using OKX.Api.Trade;

namespace OKX.Api.Tests.Account;

public class OkxAccountMovePositionClientBehaviorTests
{
    [Fact]
    public async Task MovePositionsAsync_SerializesCurrentContractAndParsesResponse()
    {
        using var server = CreateServer();
        var client = CreateClient(server);

        var request = CreateRequest();
        request.Legs.Single().To.Currency = null;
        var result = await client.Account.MovePositionsAsync(request);

        Assert.True(result.Success, result.Error?.ToString());
        Assert.Equal(123456789, result.Data!.BlockTradeId);
        Assert.Equal(OkxAccountMovePositionState.Filled, result.Data.State);
        Assert.Single(result.Data.Legs);

        var body = JObject.Parse(Assert.Single(server.Requests).Body);
        Assert.Equal("0", (string?)body["fromAcct"]);
        Assert.Equal("copydesk", (string?)body["toAcct"]);
        Assert.Equal("move15", (string?)body["clientId"]);
        Assert.Equal("987654321", (string?)body["legs"]![0]!["from"]!["posId"]);
        Assert.Equal("cross", (string?)body["legs"]![0]!["to"]!["tdMode"]);
        Assert.Null(body["legs"]![0]!["to"]!["ccy"]);
    }

    [Fact]
    public async Task MovePositionsAsync_RejectsSameAccountBeforeRequest()
    {
        using var server = CreateServer();
        var client = CreateClient(server);
        var request = CreateRequest();
        request.ToAccount = request.FromAccount;

        await Assert.ThrowsAsync<ArgumentException>(() => client.Account.MovePositionsAsync(request));

        Assert.Empty(server.Requests);
    }

    [Fact]
    public async Task MovePositionsAsync_RejectsMoreThanThirtyLegsBeforeRequest()
    {
        using var server = CreateServer();
        var client = CreateClient(server);
        var request = CreateRequest();
        request.Legs = Enumerable.Range(0, 31).Select(_ => CreateLeg()).ToList();

        await Assert.ThrowsAsync<ArgumentException>(() => client.Account.MovePositionsAsync(request));

        Assert.Empty(server.Requests);
    }

    [Fact]
    public async Task MovePositionsAsync_RejectsInvalidClientIdBeforeRequest()
    {
        using var server = CreateServer();
        var client = CreateClient(server);
        var request = CreateRequest();
        request.ClientOrderId = "move-15";

        await Assert.ThrowsAsync<ArgumentException>(() => client.Account.MovePositionsAsync(request));

        Assert.Empty(server.Requests);
    }

    private static OkxAccountMovePositionRequest CreateRequest()
        => new()
        {
            FromAccount = "0",
            ToAccount = "copydesk",
            ClientOrderId = "move15",
            Legs = [CreateLeg()],
        };

    private static OkxAccountMovePositionLegRequest CreateLeg()
        => new()
        {
            From = new OkxAccountMovePositionLegFromRequest
            {
                PositionId = "987654321",
                Quantity = "2",
                Side = OkxTradeOrderSide.Sell,
            },
            To = new OkxAccountMovePositionLegToRequest
            {
                TradeMode = OkxTradeMode.Cross,
                PositionSide = OkxTradePositionSide.Net,
                Currency = "USDT",
            },
        };

    private static LocalOkxRestServer CreateServer()
        => new(new Dictionary<string, string>
        {
            ["POST /api/v5/account/move-positions"] = FixtureReader.ReadManual("Account", "move-positions.json"),
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
}
