using OKX.Api.Public;
using OKX.Api.Tests.TestInfrastructure;

namespace OKX.Api.Tests.Public;

public class OkxPublicRpiOrderBookContractTests
{
    [Fact]
    public async Task GetRpiOrderBook_SendsCurrentQueryAndParsesConsolidatedRows()
    {
        using var server = new LocalOkxRestServer(new Dictionary<string, string>
        {
            ["GET /api/v5/market/books-rpi"] = FixtureReader.ReadManual("Public", "get-rpi-order-book.json"),
        });
        var client = CreateClient(server);

        var result = await client.Public.GetRpiOrderBookAsync("BTC-USDT-SWAP", 3);

        Assert.True(result.Success, result.Error?.ToString());
        var request = Assert.Single(server.Requests);
        Assert.Equal("GET", request.Method);
        Assert.Equal("/api/v5/market/books-rpi", request.Path);
        Assert.Contains("instId=BTC-USDT-SWAP", request.Query);
        Assert.Contains("sz=3", request.Query);
        Assert.DoesNotContain("depth=", request.Query);
        Assert.False(request.Headers.ContainsKey("OK-ACCESS-KEY"));

        Assert.Equal("BTC-USDT-SWAP", result.Data.InstrumentId);
        Assert.Equal(332042172451L, result.Data.SequenceId);
        Assert.Equal(1785310731002L, result.Data.Timestamp);
        var secondAsk = result.Data.Asks[1];
        Assert.Equal(67856.0m, secondAsk.Price);
        Assert.Equal(1.3m, secondAsk.Quantity);
        Assert.Equal(1.0m, secondAsk.NonRpiQuantity);
        Assert.Equal(4m, secondAsk.OrdersCount);
    }

    [Fact]
    public async Task GetRpiOrderBook_RejectsInvalidRequestBeforeSending()
    {
        using var server = new LocalOkxRestServer(new Dictionary<string, string>());
        var client = CreateClient(server);

        await Assert.ThrowsAsync<ArgumentException>(() => client.Public.GetRpiOrderBookAsync(" "));
        await Assert.ThrowsAnyAsync<ArgumentException>(() => client.Public.GetRpiOrderBookAsync("BTC-USDT", 0));
        await Assert.ThrowsAnyAsync<ArgumentException>(() => client.Public.GetRpiOrderBookAsync("BTC-USDT", 401));

        Assert.Empty(server.Requests);
    }

    private static OkxRestApiClient CreateClient(LocalOkxRestServer server)
        => new(new OkxRestApiOptions(new OkxApiCredentials("key", "secret", "pass"))
        {
            AutoTimestamp = false,
            BaseAddress = server.BaseAddress,
        });
}
