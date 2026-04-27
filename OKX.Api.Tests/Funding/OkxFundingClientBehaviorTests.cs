using OKX.Api.Funding;
using OKX.Api.Trade;
using OKX.Api.Tests.TestInfrastructure;

namespace OKX.Api.Tests.Funding;

public class OkxFundingClientBehaviorTests
{
    [Fact]
    public async Task GetCurrenciesAsync_SendsCurrencyFilterAsQueryParameter()
    {
        using var server = CreateServer("/api/v5/asset/currencies", "get-currencies-btc.json");
        var client = CreateClient(server);

        var result = await client.Funding.GetCurrenciesAsync("BTC");

        Assert.True(result.Success);
        var request = Assert.Single(server.Requests);
        Assert.Equal("/api/v5/asset/currencies", request.Path);
        Assert.Contains("ccy=BTC", request.Query);
    }

    [Fact]
    public async Task GetDepositAddressAsync_SendsRequiredCurrencyParameter()
    {
        using var server = CreateServer("/api/v5/asset/deposit-address", "get-deposit-address-with-attachment.json");
        var client = CreateClient(server);

        var result = await client.Funding.GetDepositAddressAsync("TONCOIN");

        Assert.True(result.Success);
        var request = Assert.Single(server.Requests);
        Assert.Contains("ccy=TONCOIN", request.Query);
    }

    [Fact]
    public async Task GetConvertCurrenciesAsync_ProjectsDetailedResponseToCurrencyCodes()
    {
        using var server = CreateServer("/api/v5/asset/convert/currencies", "get-convert-currency-details.json");
        var client = CreateClient(server);

        var result = await client.Funding.GetConvertCurrenciesAsync();

        Assert.True(result.Success);
        Assert.Equal(["BTC", "ETH"], result.Data);
    }

    [Fact]
    public async Task CreateWithdrawalOrderAsync_SerializesExpectedBody()
    {
        using var server = CreateServer("/api/v5/fiat/create-withdrawal", "fiat-order.json");
        var client = CreateClient(server);

        var result = await client.Funding.CreateWithdrawalOrderAsync("412323", "TRY", 10000m, "TR_BANKS", "194a6975e98246538faeb0fab0d502df");

        Assert.True(result.Success);
        var request = Assert.Single(server.Requests);
        Assert.Equal("/api/v5/fiat/create-withdrawal", request.Path);
        Assert.Contains("\"paymentAcctId\":\"412323\"", request.Body);
        Assert.Contains("\"paymentMethod\":\"TR_BANKS\"", request.Body);
    }

    [Fact]
    public async Task PlaceBuySellTradeAsync_SerializesExpectedBody()
    {
        using var server = CreateServer("/api/v5/fiat/buy-sell/trade", "buy-sell-order.json");
        var client = CreateClient(server);

        var result = await client.Funding.PlaceBuySellTradeAsync(
            "quoterBTC-USD16461885104612381",
            OkxTradeOrderSide.Sell,
            "USD",
            "BTC",
            30m,
            "USD",
            "balance",
            "123456");

        Assert.True(result.Success);
        var request = Assert.Single(server.Requests);
        Assert.Contains("\"quoteId\":\"quoterBTC-USD16461885104612381\"", request.Body);
        Assert.Contains("\"paymentMethod\":\"balance\"", request.Body);
        Assert.Contains("\"clOrdId\":\"123456\"", request.Body);
    }

    private static LocalOkxRestServer CreateServer(string path, string fixtureName)
        => new(new Dictionary<string, string>
        {
            [$"GET {path}"] = FixtureReader.ReadManual("Funding", fixtureName),
            [$"POST {path}"] = FixtureReader.ReadManual("Funding", fixtureName),
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
