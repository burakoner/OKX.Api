using OKX.Api.Tests.TestInfrastructure;

namespace OKX.Api.Tests.Integration;

[Collection(OkxIntegrationTestCollection.Name)]
public class OkxSubAccountIntegrationTests
{
    [SkippableFact]
    public async Task ReadOnlySubAccountEndpoints_ReturnConsistentShapesForFirstAvailableSubAccount()
    {
        var configuration = TestConfiguration.Load();
        Skip.IfNot(configuration.RunIntegrationTests, "Set OKX_RUN_INTEGRATION_TESTS=true in .env to enable live integration tests.");
        Skip.IfNot(configuration.HasApiCredentials, "Set OKX_API_KEY, OKX_API_SECRET, and OKX_API_PASSPHRASE in .env to enable private integration tests.");

        var client = OkxRestClientFactory.CreatePrivate(configuration);
        var subAccountsResult = await client.SubAccount.GetSubAccountsAsync(limit: 1);

        Skip.If(subAccountsResult.Error?.Code == 50011, "OKX rate-limited the live sub-account list request.");
        Assert.True(subAccountsResult.Success, subAccountsResult.Error?.ToString() ?? "Sub-account list request should succeed.");
        Skip.If(subAccountsResult.Data is null || subAccountsResult.Data.Count == 0, "No sub-accounts available for live Sub Account integration tests.");

        var subAccountName = subAccountsResult.Data[0].SubAccountName;

        var tradingBalanceResult = await client.SubAccount.GetSubAccountTradingBalancesAsync(subAccountName);
        Skip.If(tradingBalanceResult.Error?.Code == 50011, "OKX rate-limited the live sub-account trading balance request.");
        Assert.True(tradingBalanceResult.Success, tradingBalanceResult.Error?.ToString() ?? "Sub-account trading balance request should succeed.");
        Assert.NotNull(tradingBalanceResult.Data);
        Assert.NotNull(tradingBalanceResult.Data!.Details);

        var fundingBalanceResult = await client.SubAccount.GetSubAccountFundingBalancesAsync(subAccountName);
        Skip.If(fundingBalanceResult.Error?.Code == 50011, "OKX rate-limited the live sub-account funding balance request.");
        Assert.True(fundingBalanceResult.Success, fundingBalanceResult.Error?.ToString() ?? "Sub-account funding balance request should succeed.");
        Assert.NotNull(fundingBalanceResult.Data);
        Assert.All(fundingBalanceResult.Data!, item => Assert.False(string.IsNullOrWhiteSpace(item.Currency)));

        var maximumWithdrawalResult = await client.SubAccount.GetSubAccountMaximumWithdrawalsAsync(subAccountName);
        Skip.If(maximumWithdrawalResult.Error?.Code == 50011, "OKX rate-limited the live sub-account maximum withdrawal request.");
        Assert.True(maximumWithdrawalResult.Success, maximumWithdrawalResult.Error?.ToString() ?? "Sub-account maximum withdrawal request should succeed.");
        Assert.NotNull(maximumWithdrawalResult.Data);
    }
}
