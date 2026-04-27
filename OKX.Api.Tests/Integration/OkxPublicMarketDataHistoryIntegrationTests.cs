using OKX.Api.Common;
using OKX.Api.Public;
using OKX.Api.Tests.TestInfrastructure;
using Xunit;

namespace OKX.Api.Tests.Integration;

[Collection(OkxIntegrationTestCollection.Name)]
public class OkxPublicMarketDataHistoryIntegrationTests
{
    [SkippableFact]
    public async Task GetMarketDataHistoryAsync_ReturnsBorrowingRateSnapshots()
    {
        var configuration = TestConfiguration.Load();
        Skip.IfNot(configuration.RunIntegrationTests, "Set OKX_RUN_INTEGRATION_TESTS=true in .env to enable live integration tests.");
        Skip.If(configuration.UseDemoTrading, "Historical market data integration coverage runs against live production.");

        var begin = DateTimeOffset.UtcNow.AddDays(-7).ToUnixTimeMilliseconds();
        var end = DateTimeOffset.UtcNow.AddDays(-6).ToUnixTimeMilliseconds();

        var client = OkxRestClientFactory.CreatePublic(configuration);
        var result = await client.Public.GetMarketDataHistoryAsync(
            OkxPublicMarketDataHistoryModule.BorrowingRate,
            OkxInstrumentType.Spot,
            OkxPublicDateAggregationType.Daily,
            instrumentIdList: "ANY",
            begin: begin,
            end: end);

        Assert.True(result.Success, result.Error?.ToString() ?? "Borrowing-rate market data history request should succeed.");
        Assert.NotNull(result.Data);
        Assert.NotEmpty(result.Data);
        Assert.All(result.Data, snapshot => Assert.Equal(OkxPublicDateAggregationType.Daily, snapshot.DateAggregationType));
        Assert.All(result.Data.SelectMany(snapshot => snapshot.Details), detail => Assert.NotEmpty(detail.groupDetails));
    }
}
