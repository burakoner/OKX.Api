using OKX.Api.Tests.TestInfrastructure;
using Xunit;

namespace OKX.Api.Tests.Integration;

[Collection(OkxIntegrationTestCollection.Name)]
public class OkxFundingRestIntegrationTests
{
    [SkippableFact]
    public async Task GetCurrenciesAsync_ReturnsFilteredCurrencyData()
    {
        var configuration = TestConfiguration.Load();
        Skip.IfNot(configuration.RunIntegrationTests, "Set OKX_RUN_INTEGRATION_TESTS=true in .env to enable live integration tests.");
        Skip.IfNot(configuration.HasApiCredentials, "Set OKX_API_KEY, OKX_API_SECRET, and OKX_API_PASSPHRASE in .env to enable private integration tests.");

        var client = OkxRestClientFactory.CreatePrivate(configuration);
        var result = await client.Funding.GetCurrenciesAsync("BTC");

        Skip.If(result.Error?.Code == 50011, "OKX rate-limited the live funding currencies request.");
        Assert.True(result.Success, result.Error?.ToString() ?? "Funding currencies request should succeed.");
        Assert.NotNull(result.Data);
        Assert.NotEmpty(result.Data);
        Assert.All(result.Data, item => Assert.Equal("BTC", item.Currency));
    }

    [SkippableFact]
    public async Task GetConvertCurrencyDetailsAsync_ReturnsConvertCurrencyMetadata()
    {
        var configuration = TestConfiguration.Load();
        Skip.IfNot(configuration.RunIntegrationTests, "Set OKX_RUN_INTEGRATION_TESTS=true in .env to enable live integration tests.");
        Skip.IfNot(configuration.HasApiCredentials, "Set OKX_API_KEY, OKX_API_SECRET, and OKX_API_PASSPHRASE in .env to enable private integration tests.");

        var client = OkxRestClientFactory.CreatePrivate(configuration);
        var result = await client.Funding.GetConvertCurrencyDetailsAsync();

        Skip.If(result.Error?.Code == 50011, "OKX rate-limited the live convert currency details request.");
        Assert.True(result.Success, result.Error?.ToString() ?? "Convert currency details request should succeed.");
        Assert.NotNull(result.Data);
        Assert.NotEmpty(result.Data);
        Assert.All(result.Data, item => Assert.False(string.IsNullOrWhiteSpace(item.Currency)));
    }

    [SkippableFact]
    public async Task GetDepositAddressAsync_ReturnsFundingDepositAddresses()
    {
        var configuration = TestConfiguration.Load();
        Skip.IfNot(configuration.RunIntegrationTests, "Set OKX_RUN_INTEGRATION_TESTS=true in .env to enable live integration tests.");
        Skip.IfNot(configuration.HasApiCredentials, "Set OKX_API_KEY, OKX_API_SECRET, and OKX_API_PASSPHRASE in .env to enable private integration tests.");

        var client = OkxRestClientFactory.CreatePrivate(configuration);
        var result = await client.Funding.GetDepositAddressAsync("BTC");

        Skip.If(result.Error?.Code == 50011, "OKX rate-limited the live funding deposit address request.");
        Assert.True(result.Success, result.Error?.ToString() ?? "Funding deposit address request should succeed.");
        Assert.NotNull(result.Data);
        Assert.NotEmpty(result.Data);
        Assert.All(result.Data, item => Assert.Equal("BTC", item.Currency));
    }
}
