using OKX.Api.Common;
using OKX.Api.Tests.TestInfrastructure;
using Xunit;

namespace OKX.Api.Tests.Integration;

[Collection(OkxIntegrationTestCollection.Name)]
public class OkxPublicDataIntegrationTests
{
    [SkippableFact]
    public async Task GetOpenInterestsAsync_ReturnsCurrentSwapOpenInterest()
    {
        var configuration = TestConfiguration.Load();
        Skip.IfNot(configuration.RunIntegrationTests, "Set OKX_RUN_INTEGRATION_TESTS=true in .env to enable live integration tests.");

        var client = OkxRestClientFactory.CreatePublic(configuration);
        var result = await client.Public.GetOpenInterestsAsync(OkxInstrumentType.Swap, instrumentId: "BTC-USDT-SWAP");
        SkipIfRateLimited(result.Error?.ToString());

        Assert.True(result.Success, result.Error?.ToString() ?? "Public open interest request should succeed.");
        var item = Assert.Single(result.Data!);
        Assert.True(item.OpenInterestCount > 0m);
    }

    [SkippableFact]
    public async Task GetInsuranceFundsAsync_ReturnsCurrentSecurityFundRows()
    {
        var configuration = TestConfiguration.Load();
        Skip.IfNot(configuration.RunIntegrationTests, "Set OKX_RUN_INTEGRATION_TESTS=true in .env to enable live integration tests.");

        var client = OkxRestClientFactory.CreatePublic(configuration);
        var result = await client.Public.GetInsuranceFundsAsync(OkxInstrumentType.Swap, instrumentFamily: "BTC-USD", limit: 5);
        SkipIfRateLimited(result.Error?.ToString());

        Assert.True(result.Success, result.Error?.ToString() ?? "Public insurance fund request should succeed.");
        Assert.NotNull(result.Data);
        Assert.NotEmpty(result.Data);
        Assert.All(result.Data, x => Assert.NotEmpty(x.Details));
    }

    [SkippableFact]
    public async Task GetEconomicCalendarDataAsync_ReturnsAuthenticatedProductionData()
    {
        var configuration = TestConfiguration.Load();
        Skip.IfNot(configuration.RunIntegrationTests, "Set OKX_RUN_INTEGRATION_TESTS=true in .env to enable live integration tests.");
        Skip.If(configuration.UseDemoTrading, "Economic calendar coverage is only supported in production.");
        Skip.IfNot(configuration.HasApiCredentials, "Economic calendar coverage requires API credentials.");

        var client = OkxRestClientFactory.CreatePrivate(configuration);
        var result = await client.Public.GetEconomicCalendarDataAsync("united_states", limit: 5);
        SkipIfRateLimited(result.Error?.ToString());

        Assert.True(result.Success, result.Error?.ToString() ?? "Economic calendar request should succeed.");
        Assert.NotNull(result.Data);
        Assert.NotEmpty(result.Data);
        Assert.All(result.Data, x => Assert.Equal("United States", x.Region));
    }

    private static void SkipIfRateLimited(string? errorText)
    {
        if (!string.IsNullOrEmpty(errorText) && errorText.Contains("50011", StringComparison.Ordinal))
            Skip.If(true, "OKX rate-limited the live public-data request (50011).");
    }
}
