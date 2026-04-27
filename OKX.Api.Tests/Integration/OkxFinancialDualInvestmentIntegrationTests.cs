using OKX.Api.Tests.TestInfrastructure;
using Xunit;

namespace OKX.Api.Tests.Integration;

public class OkxFinancialDualInvestmentIntegrationTests
{
    [SkippableFact]
    public async Task GetCurrencyPairsAsync_ReturnsDualInvestmentPairs()
    {
        var configuration = TestConfiguration.Load();
        Skip.IfNot(configuration.RunIntegrationTests, "Set OKX_RUN_INTEGRATION_TESTS=true in .env to enable live integration tests.");
        Skip.IfNot(configuration.HasApiCredentials, "Set OKX_API_KEY, OKX_API_SECRET, and OKX_API_PASSPHRASE in .env to enable private integration tests.");
        Skip.If(configuration.UseDemoTrading, "Dual investment integration tests require OKX_DEMO_TRADING=false.");

        var client = OkxRestClientFactory.CreatePrivate(configuration);
        var result = await client.Financial.DualInvestment.GetCurrencyPairsAsync();

        Skip.If(result.Error?.Code == 50011, "OKX rate-limited the live dual investment currency pairs request.");
        Assert.True(result.Success, result.Error?.ToString() ?? "Dual investment currency pairs request should succeed.");
        Assert.NotNull(result.Data);
        Assert.NotEmpty(result.Data);
    }

    [SkippableFact]
    public async Task GetProductsAsync_ReturnsDualInvestmentProducts()
    {
        var configuration = TestConfiguration.Load();
        Skip.IfNot(configuration.RunIntegrationTests, "Set OKX_RUN_INTEGRATION_TESTS=true in .env to enable live integration tests.");
        Skip.IfNot(configuration.HasApiCredentials, "Set OKX_API_KEY, OKX_API_SECRET, and OKX_API_PASSPHRASE in .env to enable private integration tests.");
        Skip.If(configuration.UseDemoTrading, "Dual investment integration tests require OKX_DEMO_TRADING=false.");

        var client = OkxRestClientFactory.CreatePrivate(configuration);
        var products = await client.Financial.DualInvestment.GetProductsAsync("BTC", "USDT", OKX.Api.Common.OkxOptionType.Call);

        Skip.If(products.Error?.Code == 50011, "OKX rate-limited the live dual investment products request.");
        Assert.True(products.Success, products.Error?.ToString() ?? "Dual investment products request should succeed.");
        Assert.NotNull(products.Data);
        Assert.NotEmpty(products.Data);
        Assert.All(products.Data, product =>
        {
            Assert.Equal("BTC", product.BaseCurrency);
            Assert.Equal("USDT", product.QuoteCurrency);
            Assert.Equal(OKX.Api.Common.OkxOptionType.Call, product.OptionType);
        });
    }

    [SkippableFact]
    public async Task GetOrderHistoryAsync_ReturnsValidResponse()
    {
        var configuration = TestConfiguration.Load();
        Skip.IfNot(configuration.RunIntegrationTests, "Set OKX_RUN_INTEGRATION_TESTS=true in .env to enable live integration tests.");
        Skip.IfNot(configuration.HasApiCredentials, "Set OKX_API_KEY, OKX_API_SECRET, and OKX_API_PASSPHRASE in .env to enable private integration tests.");
        Skip.If(configuration.UseDemoTrading, "Dual investment integration tests require OKX_DEMO_TRADING=false.");

        var client = OkxRestClientFactory.CreatePrivate(configuration);
        var result = await client.Financial.DualInvestment.GetOrderHistoryAsync(limit: 1);

        Skip.If(result.Error?.Code == 50011, "OKX rate-limited the live dual investment order history request.");
        Assert.True(result.Success, result.Error?.ToString() ?? "Dual investment order history request should succeed.");
        Assert.NotNull(result.Data);
    }
}
