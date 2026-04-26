using OKX.Api.Common;
using OKX.Api.Tests.TestInfrastructure;
using Xunit;

namespace OKX.Api.Tests.Integration;

public class OkxPublicRestIntegrationTests
{
    [SkippableFact]
    public async Task GetInstrumentsAsync_ReturnsPublicInstrumentData()
    {
        var configuration = TestConfiguration.Load();
        Skip.IfNot(configuration.RunIntegrationTests, "Set OKX_RUN_INTEGRATION_TESTS=true in .env to enable live integration tests.");

        var client = OkxRestClientFactory.CreatePublic(configuration);
        var result = await client.Public.GetInstrumentsAsync(OkxInstrumentType.Spot);

        Assert.True(result.Success, result.Error?.ToString() ?? "Public instruments request should succeed.");
        Assert.NotNull(result.Data);
        Assert.NotEmpty(result.Data);

        if (configuration.UseDemoTrading)
            Assert.Contains(result.Data, x => x.InstrumentIdCode > int.MaxValue);
    }

    [SkippableFact]
    public async Task GetPublicBorrowHistoryAsync_ReturnsBorrowRateData()
    {
        var configuration = TestConfiguration.Load();
        Skip.IfNot(configuration.RunIntegrationTests, "Set OKX_RUN_INTEGRATION_TESTS=true in .env to enable live integration tests.");

        var client = OkxRestClientFactory.CreatePublic(configuration);
        var result = await client.Financial.SimpleEarn.GetPublicBorrowHistoryAsync(configuration.PublicBorrowCurrency, limit: 5);

        if (configuration.UseDemoTrading)
        {
            Assert.False(result.Success);
            Assert.Contains("50038", result.Error?.ToString() ?? string.Empty);
            return;
        }

        Assert.True(result.Success, result.Error?.ToString() ?? "Public borrow history request should succeed.");
        Assert.NotNull(result.Data);
    }

    [SkippableFact]
    public async Task GetFundingRatesAsync_ReturnsSwapAndXPerpFuturesData()
    {
        var configuration = TestConfiguration.Load();
        Skip.IfNot(configuration.RunIntegrationTests, "Set OKX_RUN_INTEGRATION_TESTS=true in .env to enable live integration tests.");
        Skip.If(configuration.UseDemoTrading, "Funding rate integration coverage runs against live production snapshots.");

        var client = OkxRestClientFactory.CreatePublic(configuration);
        var result = await client.Public.GetFundingRatesAsync();

        Assert.True(result.Success, result.Error?.ToString() ?? "Funding rates request should succeed.");
        Assert.NotNull(result.Data);
        Assert.NotEmpty(result.Data);
        Assert.Contains(result.Data, x => x.InstrumentType == OkxInstrumentType.Swap);
        Assert.Contains(result.Data, x => x.InstrumentType == OkxInstrumentType.Futures);
    }

    [SkippableFact]
    public async Task GetFundingRateHistoryAsync_ReturnsXPerpFuturesHistory()
    {
        var configuration = TestConfiguration.Load();
        Skip.IfNot(configuration.RunIntegrationTests, "Set OKX_RUN_INTEGRATION_TESTS=true in .env to enable live integration tests.");
        Skip.If(configuration.UseDemoTrading, "X-Perps funding rate history coverage runs against live production snapshots.");

        var client = OkxRestClientFactory.CreatePublic(configuration);
        var instrumentsResult = await client.Public.GetInstrumentsAsync(OkxInstrumentType.Futures);

        Assert.True(instrumentsResult.Success, instrumentsResult.Error?.ToString() ?? "Futures instruments request should succeed.");
        Assert.NotNull(instrumentsResult.Data);

        var instrument = instrumentsResult.Data
            .FirstOrDefault(x => x.RuleType == OkxInstrumentRuleType.XPerp && !string.IsNullOrWhiteSpace(x.InstrumentId));

        Skip.If(instrument is null, "No live X-Perps futures instrument was returned by OKX.");

        var result = await client.Public.GetFundingRateHistoryAsync(instrument.InstrumentId, limit: 3);

        Assert.True(result.Success, result.Error?.ToString() ?? "Funding rate history request should succeed.");
        Assert.NotNull(result.Data);
        Assert.NotEmpty(result.Data);
        Assert.All(result.Data, x => Assert.Equal(OkxInstrumentType.Futures, x.InstrumentType));
    }
}
