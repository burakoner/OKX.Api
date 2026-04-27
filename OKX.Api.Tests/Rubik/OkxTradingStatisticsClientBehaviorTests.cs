using Newtonsoft.Json.Linq;
using OKX.Api.Common;
using OKX.Api.Tests.TestInfrastructure;

namespace OKX.Api.Tests.Rubik;

public class OkxTradingStatisticsClientBehaviorTests
{
    [Fact]
    public void TradingStatisticsAlias_ReturnsRubikClientInstance()
    {
        var client = new OkxRestApiClient();
        Assert.Same(client.Rubik, client.TradingStatistics);
    }

    [Fact]
    public async Task GetSupportCoinAsync_UsesExpectedPathAndParsesObjectResponse()
    {
        using var server = CreateServer();
        var client = CreateClient(server);

        var result = await client.TradingStatistics.GetSupportCoinAsync();

        Assert.True(result.Success, result.Error?.ToString() ?? "Support coin request should succeed.");
        Assert.Contains("BTC", result.Data!.Contract);

        var request = Assert.Single(server.Requests);
        Assert.Equal("GET", request.Method);
        Assert.Equal("/api/v5/rubik/stat/trading-data/support-coin", request.Path);
    }

    [Fact]
    public async Task GetTakerVolumeAsync_WithContracts_SendsContractsQueryValue()
    {
        using var server = CreateServer();
        var client = CreateClient(server);

        var result = await client.TradingStatistics.GetTakerVolumeAsync("BTC", OkxInstrumentType.Contracts, period: "1D");

        Assert.True(result.Success, result.Error?.ToString() ?? "Taker volume request should succeed.");

        var request = server.Requests.Single(x => x.Path == "/api/v5/rubik/stat/taker-volume");
        var query = Uri.UnescapeDataString(request.Query);
        Assert.Contains("ccy=BTC", query);
        Assert.Contains("instType=CONTRACTS", query);
        Assert.Contains("period=1D", query);
    }

    [Fact]
    public async Task GetInterestVolumeStrikeAsync_SendsExpiryDateQuery()
    {
        using var server = CreateServer();
        var client = CreateClient(server);

        var result = await client.TradingStatistics.GetInterestVolumeStrikeAsync("BTC", "20210901", "1D");

        Assert.True(result.Success, result.Error?.ToString() ?? "Strike statistics request should succeed.");

        var request = server.Requests.Single(x => x.Path == "/api/v5/rubik/stat/option/open-interest-volume-strike");
        var query = Uri.UnescapeDataString(request.Query);
        Assert.Contains("ccy=BTC", query);
        Assert.Contains("expTime=20210901", query);
        Assert.Contains("period=1D", query);
    }

    [Fact]
    public async Task GetTakerVolumeAsync_WithUnsupportedInstrumentType_Throws()
    {
        var client = new OkxRestApiClient();

        var exception = await Assert.ThrowsAsync<ArgumentException>(() => client.TradingStatistics.GetTakerVolumeAsync("BTC", OkxInstrumentType.Margin));
        Assert.Contains("SPOT or CONTRACTS", exception.Message, StringComparison.Ordinal);
    }

    [Fact]
    public async Task SimplePeriodEndpoints_RejectUnsupportedPeriods()
    {
        var client = new OkxRestApiClient();

        await Assert.ThrowsAsync<ArgumentException>(() => client.TradingStatistics.GetTakerVolumeAsync("BTC", OkxInstrumentType.Spot, period: "8H"));
        await Assert.ThrowsAsync<ArgumentException>(() => client.TradingStatistics.GetLongShortRatioAsync("BTC", period: "15m"));
        await Assert.ThrowsAsync<ArgumentException>(() => client.TradingStatistics.GetContractSummaryAsync("BTC", period: "8H"));
    }

    [Fact]
    public async Task OptionPeriodEndpoints_RejectUnsupportedPeriods()
    {
        var client = new OkxRestApiClient();

        await Assert.ThrowsAsync<ArgumentException>(() => client.TradingStatistics.GetOptionsSummaryAsync("BTC", period: "5m"));
        await Assert.ThrowsAsync<ArgumentException>(() => client.TradingStatistics.GetPutCallRatioAsync("BTC", period: "5m"));
        await Assert.ThrowsAsync<ArgumentException>(() => client.TradingStatistics.GetInterestVolumeExpiryAsync("BTC", period: "5m"));
        await Assert.ThrowsAsync<ArgumentException>(() => client.TradingStatistics.GetInterestVolumeStrikeAsync("BTC", "20210901", period: "5m"));
        await Assert.ThrowsAsync<ArgumentException>(() => client.TradingStatistics.GetTakerFlowAsync("BTC", period: "5m"));
    }

    [Fact]
    public async Task ContractPeriodEndpoints_RejectUnsupportedPeriods()
    {
        var client = new OkxRestApiClient();

        await Assert.ThrowsAsync<ArgumentException>(() => client.TradingStatistics.GetContractOpenInterestHistoryAsync("BTC-USDT-SWAP", period: "8H"));
        await Assert.ThrowsAsync<ArgumentException>(() => client.TradingStatistics.GetContractTakerVolumeAsync("BTC-USDT-SWAP", period: "8H"));
        await Assert.ThrowsAsync<ArgumentException>(() => client.TradingStatistics.GetMarginLendingRatioAsync("BTC", period: "8H"));
        await Assert.ThrowsAsync<ArgumentException>(() => client.TradingStatistics.GetTopTradersContractLongShortRatioAsync("BTC-USDT-SWAP", period: "8H"));
        await Assert.ThrowsAsync<ArgumentException>(() => client.TradingStatistics.GetTopTradersContractLongShortRatioByPositionAsync("BTC-USDT-SWAP", period: "8H"));
        await Assert.ThrowsAsync<ArgumentException>(() => client.TradingStatistics.GetContractLongShortRatioAsync("BTC-USDT-SWAP", period: "8H"));
    }

    private static LocalOkxRestServer CreateServer()
        => new(new Dictionary<string, string>
        {
            ["GET /api/v5/rubik/stat/trading-data/support-coin"] = FixtureReader.ReadManual("Rubik", "get-support-coin.json"),
            ["GET /api/v5/rubik/stat/taker-volume"] = FixtureReader.ReadManual("Rubik", "get-taker-volume-spot.json"),
            ["GET /api/v5/rubik/stat/option/open-interest-volume-strike"] = FixtureReader.ReadManual("Rubik", "get-interest-volume-strike.json"),
        });

    private static OkxRestApiClient CreateClient(LocalOkxRestServer server)
    {
        var options = new OkxRestApiOptions
        {
            AutoTimestamp = false,
            BaseAddress = server.BaseAddress,
        };

        return new OkxRestApiClient(options);
    }
}
