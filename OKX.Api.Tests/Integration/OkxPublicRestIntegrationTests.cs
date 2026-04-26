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
}
