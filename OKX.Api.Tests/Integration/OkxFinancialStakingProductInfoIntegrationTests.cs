using OKX.Api.Tests.TestInfrastructure;
using Xunit;

namespace OKX.Api.Tests.Integration;

public class OkxFinancialStakingProductInfoIntegrationTests
{
    [SkippableFact]
    public async Task GetEthProductInfoAsync_ReturnsCurrentStakingMetadata()
    {
        var configuration = TestConfiguration.Load();
        Skip.IfNot(configuration.RunIntegrationTests, "Set OKX_RUN_INTEGRATION_TESTS=true in .env to enable live integration tests.");
        Skip.IfNot(configuration.HasApiCredentials, "Set OKX_API_KEY, OKX_API_SECRET, and OKX_API_PASSPHRASE in .env to enable private integration tests.");
        Skip.If(configuration.UseDemoTrading, "ETH staking integration coverage runs against live production.");

        var client = OkxRestClientFactory.CreatePrivate(configuration);
        var result = await client.Financial.EthStaking.GetProductInfoAsync();

        Assert.True(result.Success, result.Error?.ToString() ?? "ETH staking product info request should succeed.");
        Assert.NotNull(result.Data);
        Assert.NotNull(result.Data.Rate);
        Assert.NotNull(result.Data.RedemptionDays);
        Assert.NotNull(result.Data.MinimumAmount);
    }

    [SkippableFact]
    public async Task GetSolProductInfoAsync_ReturnsCurrentStakingMetadata()
    {
        var configuration = TestConfiguration.Load();
        Skip.IfNot(configuration.RunIntegrationTests, "Set OKX_RUN_INTEGRATION_TESTS=true in .env to enable live integration tests.");
        Skip.IfNot(configuration.HasApiCredentials, "Set OKX_API_KEY, OKX_API_SECRET, and OKX_API_PASSPHRASE in .env to enable private integration tests.");
        Skip.If(configuration.UseDemoTrading, "SOL staking integration coverage runs against live production.");

        var client = OkxRestClientFactory.CreatePrivate(configuration);
        var result = await client.Financial.SolStaking.GetProductInfoAsync();

        Assert.True(result.Success, result.Error?.ToString() ?? "SOL staking product info request should succeed.");
        Assert.NotNull(result.Data);
        Assert.NotNull(result.Data.Rate);
        Assert.NotNull(result.Data.RedemptionDays);
        Assert.NotNull(result.Data.MinimumAmount);
    }
}
