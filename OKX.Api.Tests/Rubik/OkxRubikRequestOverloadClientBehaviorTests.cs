using OKX.Api.Common;
using OKX.Api.Rubik;
using OKX.Api.Tests.TestInfrastructure;

namespace OKX.Api.Tests.Rubik;

public class OkxRubikRequestOverloadClientBehaviorTests
{
    [Fact]
    public async Task GetContractOpenInterestHistoryRequestOverload_MatchesLegacyQuery()
    {
        using var server = CreateServer("/api/v5/rubik/stat/contracts/open-interest-history");
        var client = CreateClient(server);

        await client.TradingStatistics.GetContractOpenInterestHistoryAsync("BTC-USDT-SWAP", "1H", 1, 2, 3);
        await client.TradingStatistics.GetContractOpenInterestHistoryAsync(new OkxRubikInstrumentPeriodRangeRequest
        {
            InstrumentId = "BTC-USDT-SWAP",
            Period = "1H",
            Begin = 1,
            End = 2,
            Limit = 3
        });

        AssertRequestQueriesEqual(server);
    }

    [Fact]
    public async Task GetTakerVolumeRequestOverload_MatchesLegacyQuery()
    {
        using var server = CreateServer("/api/v5/rubik/stat/taker-volume");
        var client = CreateClient(server);

        await client.TradingStatistics.GetTakerVolumeAsync("BTC", OkxInstrumentType.Spot, "1H", 1, 2);
        await client.TradingStatistics.GetTakerVolumeAsync(new OkxRubikTakerVolumeRequest
        {
            Currency = "BTC",
            InstrumentType = OkxInstrumentType.Spot,
            Period = "1H",
            Begin = 1,
            End = 2
        });

        AssertRequestQueriesEqual(server);
    }

    [Fact]
    public async Task GetTopTradersRequestOverload_MatchesLegacyQuery()
    {
        using var server = CreateServer("/api/v5/rubik/stat/contracts/long-short-account-ratio-contract-top-trader");
        var client = CreateClient(server);

        await client.TradingStatistics.GetTopTradersContractLongShortRatioAsync("BTC-USDT-SWAP", "1H", 1, 2, 3);
        await client.TradingStatistics.GetTopTradersContractLongShortRatioAsync(new OkxRubikInstrumentPeriodRangeRequest
        {
            InstrumentId = "BTC-USDT-SWAP",
            Period = "1H",
            Begin = 1,
            End = 2,
            Limit = 3
        });

        AssertRequestQueriesEqual(server);
    }

    [Fact]
    public async Task GetOptionsSummaryRequestOverload_MatchesLegacyQuery()
    {
        using var server = CreateServer("/api/v5/rubik/stat/option/open-interest-volume");
        var client = CreateClient(server);

        await client.TradingStatistics.GetOptionsSummaryAsync("BTC", "8H");
        await client.TradingStatistics.GetOptionsSummaryAsync(new OkxRubikOptionPeriodRequest
        {
            Currency = "BTC",
            Period = "8H"
        });

        AssertRequestQueriesEqual(server);
    }

    [Fact]
    public async Task GetInterestVolumeStrikeRequestOverload_MatchesLegacyQuery()
    {
        using var server = CreateServer("/api/v5/rubik/stat/option/open-interest-volume-strike");
        var client = CreateClient(server);

        await client.TradingStatistics.GetInterestVolumeStrikeAsync("BTC", "20260626", "8H");
        await client.TradingStatistics.GetInterestVolumeStrikeAsync(new OkxRubikOptionStrikeRequest
        {
            Currency = "BTC",
            ExpiryTime = "20260626",
            Period = "8H"
        });

        AssertRequestQueriesEqual(server);
    }

    private static void AssertRequestQueriesEqual(LocalOkxRestServer server)
    {
        Assert.Equal(2, server.Requests.Count);
        Assert.Equal(server.Requests[0].Path, server.Requests[1].Path);
        Assert.Equal(server.Requests[0].Query, server.Requests[1].Query);
    }

    private static LocalOkxRestServer CreateServer(string path)
        => new(new Dictionary<string, string>
        {
            [$"GET {path}"] = "{\"code\":\"0\",\"msg\":\"\",\"data\":[{}]}",
            [$"POST {path}"] = "{\"code\":\"0\",\"msg\":\"\",\"data\":[{}]}",
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
