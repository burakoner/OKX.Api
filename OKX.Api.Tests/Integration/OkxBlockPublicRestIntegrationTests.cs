using OKX.Api.Tests.TestInfrastructure;
using Xunit;

namespace OKX.Api.Tests.Integration;

public class OkxBlockPublicRestIntegrationTests
{
    [SkippableFact]
    public async Task GetPublicExecutedTradesAsync_WorksWithoutCredentials()
    {
        var configuration = TestConfiguration.Load();
        Skip.IfNot(configuration.RunIntegrationTests, "Set OKX_RUN_INTEGRATION_TESTS=true in .env to enable live integration tests.");

        var client = OkxRestClientFactory.CreatePublic(configuration);
        var result = await client.Block.GetPublicExecutedTradesAsync(limit: 5);

        Assert.True(result.Success, result.Error?.ToString() ?? "Public block executed trades request should succeed without credentials.");
        Assert.NotNull(result.Data);
    }
}
