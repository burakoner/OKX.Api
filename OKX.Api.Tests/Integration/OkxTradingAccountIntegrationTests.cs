using OKX.Api.Tests.TestInfrastructure;
using Xunit;

namespace OKX.Api.Tests.Integration;

[Collection(OkxIntegrationTestCollection.Name)]
public class OkxTradingAccountIntegrationTests
{
    [SkippableFact]
    public async Task PrecheckSetDeltaNeutralAsync_ReturnsLivePrecheckPayload()
    {
        var configuration = TestConfiguration.Load();
        Skip.IfNot(configuration.RunIntegrationTests, "Set OKX_RUN_INTEGRATION_TESTS=true in .env to enable live integration tests.");
        Skip.IfNot(configuration.HasApiCredentials, "Set OKX_API_KEY, OKX_API_SECRET, and OKX_API_PASSPHRASE in .env to enable private integration tests.");

        var client = OkxRestClientFactory.CreatePrivate(configuration);
        var accountConfig = await client.Account.GetConfigurationAsync();

        Assert.True(accountConfig.Success, accountConfig.Error?.ToString() ?? "Account configuration request should succeed.");
        Assert.NotNull(accountConfig.Data);

        var result = await client.Account.PrecheckSetDeltaNeutralAsync(accountConfig.Data!.StrategyType);

        Assert.True(result.Success, result.Error?.ToString() ?? "Delta neutral precheck request should succeed.");
        Assert.NotNull(result.Data);
        Assert.NotNull(result.Data.UnmatchedInformationList);
    }
}
