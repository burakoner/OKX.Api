using OKX.Api.Common;
using OKX.Api.Tests.TestInfrastructure;
using Xunit;

namespace OKX.Api.Tests.Integration;

[Collection(OkxIntegrationTestCollection.Name)]
public class OkxPrivateRestIntegrationTests
{
    [SkippableFact]
    public async Task GetInstrumentsAsync_ReturnsPrivateInstrumentData()
    {
        var configuration = TestConfiguration.Load();
        Skip.IfNot(configuration.RunIntegrationTests, "Set OKX_RUN_INTEGRATION_TESTS=true in .env to enable live integration tests.");
        Skip.IfNot(configuration.HasApiCredentials, "Set OKX_API_KEY, OKX_API_SECRET, and OKX_API_PASSPHRASE in .env to enable private integration tests.");

        var client = OkxRestClientFactory.CreatePrivate(configuration);
        var result = await client.Account.GetInstrumentsAsync(OkxInstrumentType.Spot);

        Assert.True(result.Success, result.Error?.ToString() ?? "Private instruments request should succeed.");
        Assert.NotNull(result.Data);
        Assert.NotEmpty(result.Data);
    }
}
