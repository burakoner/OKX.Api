using OKX.Api.Tests.TestInfrastructure;
using Xunit;

namespace OKX.Api.Tests.Integration;

public class OkxTradeAccountRateLimitIntegrationTests
{
    [SkippableFact]
    public async Task GetAccountRateLimitAsync_ReturnsTradeRateLimitData()
    {
        var configuration = TestConfiguration.Load();
        Skip.IfNot(configuration.RunIntegrationTests, "Set OKX_RUN_INTEGRATION_TESTS=true in .env to enable live integration tests.");
        Skip.IfNot(configuration.HasApiCredentials, "Set OKX_API_KEY, OKX_API_SECRET, and OKX_API_PASSPHRASE in .env to enable private integration tests.");

        var client = OkxRestClientFactory.CreatePrivate(configuration);
        var result = await client.Trade.GetAccountRateLimitAsync();

        Assert.True(result.Success, result.Error?.ToString() ?? "Account rate limit request should succeed.");
        Assert.NotNull(result.Data);
        Assert.True(result.Data.AccountRateLimit > 0);
    }
}
