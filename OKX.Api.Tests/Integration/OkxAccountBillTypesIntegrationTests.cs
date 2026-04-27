using OKX.Api.Account;
using OKX.Api.Tests.TestInfrastructure;
using Xunit;

namespace OKX.Api.Tests.Integration;

[Collection(OkxIntegrationTestCollection.Name)]
public class OkxAccountBillTypesIntegrationTests
{
    [SkippableFact]
    public async Task GetBillTypesAsync_ReturnsFilteredMappings()
    {
        var configuration = TestConfiguration.Load();
        Skip.IfNot(configuration.RunIntegrationTests, "Set OKX_RUN_INTEGRATION_TESTS=true in .env to enable live integration tests.");
        Skip.IfNot(configuration.HasApiCredentials, "Set OKX_API_KEY, OKX_API_SECRET, and OKX_API_PASSPHRASE in .env to enable private integration tests.");

        var client = OkxRestClientFactory.CreatePrivate(configuration);
        var result = await client.Account.GetBillTypesAsync([OkxAccountBillType.Transfer, OkxAccountBillType.Trade]);

        Assert.True(result.Success, result.Error?.ToString() ?? "Bill types request should succeed.");
        Assert.NotNull(result.Data);
        Assert.NotEmpty(result.Data);
        Assert.All(result.Data, item => Assert.Contains(item.TypeId, new[] { "1", "2" }));
        Assert.Contains(result.Data, item => item.SubTypeDetails.Count > 0);
    }
}
