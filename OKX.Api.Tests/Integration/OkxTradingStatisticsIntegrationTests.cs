using OKX.Api.Common;
using OKX.Api.Tests.TestInfrastructure;
using Xunit;

namespace OKX.Api.Tests.Integration;

public class OkxTradingStatisticsIntegrationTests
{
    [SkippableFact]
    public async Task GetSupportCoinAsync_ReturnsCurrentCollections()
    {
        var configuration = TestConfiguration.Load();
        Skip.IfNot(configuration.RunIntegrationTests, "Set OKX_RUN_INTEGRATION_TESTS=true in .env to enable live integration tests.");

        var client = OkxRestClientFactory.CreatePublic(configuration);
        var result = await client.TradingStatistics.GetSupportCoinAsync();
        SkipIfRateLimited(result.Error?.ToString());

        Assert.True(result.Success, result.Error?.ToString() ?? "Support coin request should succeed.");
        Assert.Contains("BTC", result.Data!.Contract);
    }

    [SkippableFact]
    public async Task ContractEndpoints_ReturnCurrentData()
    {
        var configuration = TestConfiguration.Load();
        Skip.IfNot(configuration.RunIntegrationTests, "Set OKX_RUN_INTEGRATION_TESTS=true in .env to enable live integration tests.");

        var client = OkxRestClientFactory.CreatePublic(configuration);

        var openInterestHistory = await client.TradingStatistics.GetContractOpenInterestHistoryAsync("BTC-USDT-SWAP", period: "1D", limit: 5);
        SkipIfRateLimited(openInterestHistory.Error?.ToString());
        Assert.True(openInterestHistory.Success, openInterestHistory.Error?.ToString() ?? "Contract open interest history should succeed.");
        Assert.NotEmpty(openInterestHistory.Data!);

        await Task.Delay(250);

        var takerVolume = await client.TradingStatistics.GetTakerVolumeAsync("BTC", OkxInstrumentType.Contracts, period: "1D");
        SkipIfRateLimited(takerVolume.Error?.ToString());
        Assert.True(takerVolume.Success, takerVolume.Error?.ToString() ?? "Contracts taker volume should succeed.");
        Assert.NotEmpty(takerVolume.Data!);

        await Task.Delay(250);

        var contractTakerVolume = await client.TradingStatistics.GetContractTakerVolumeAsync("BTC-USDT-SWAP", period: "1D", limit: 5);
        SkipIfRateLimited(contractTakerVolume.Error?.ToString());
        Assert.True(contractTakerVolume.Success, contractTakerVolume.Error?.ToString() ?? "Contract taker volume should succeed.");
        Assert.NotEmpty(contractTakerVolume.Data!);
    }

    [SkippableFact]
    public async Task RatioEndpoints_ReturnCurrentData()
    {
        var configuration = TestConfiguration.Load();
        Skip.IfNot(configuration.RunIntegrationTests, "Set OKX_RUN_INTEGRATION_TESTS=true in .env to enable live integration tests.");

        var client = OkxRestClientFactory.CreatePublic(configuration);

        var marginLending = await client.TradingStatistics.GetMarginLendingRatioAsync("BTC", period: "1D");
        SkipIfRateLimited(marginLending.Error?.ToString());
        Assert.True(marginLending.Success, marginLending.Error?.ToString() ?? "Margin lending ratio should succeed.");
        Assert.NotEmpty(marginLending.Data!);

        await Task.Delay(250);

        var longShort = await client.TradingStatistics.GetLongShortRatioAsync("BTC", period: "1D");
        SkipIfRateLimited(longShort.Error?.ToString());
        Assert.True(longShort.Success, longShort.Error?.ToString() ?? "Long/short ratio should succeed.");
        Assert.NotEmpty(longShort.Data!);

        await Task.Delay(250);

        var contractSummary = await client.TradingStatistics.GetContractSummaryAsync("BTC", period: "1D");
        SkipIfRateLimited(contractSummary.Error?.ToString());
        Assert.True(contractSummary.Success, contractSummary.Error?.ToString() ?? "Contract summary should succeed.");
        Assert.NotEmpty(contractSummary.Data!);
    }

    [SkippableFact]
    public async Task OptionEndpoints_ReturnCurrentData()
    {
        var configuration = TestConfiguration.Load();
        Skip.IfNot(configuration.RunIntegrationTests, "Set OKX_RUN_INTEGRATION_TESTS=true in .env to enable live integration tests.");

        var client = OkxRestClientFactory.CreatePublic(configuration);

        var optionsSummary = await client.TradingStatistics.GetOptionsSummaryAsync("BTC", period: "1D");
        SkipIfRateLimited(optionsSummary.Error?.ToString());
        Assert.True(optionsSummary.Success, optionsSummary.Error?.ToString() ?? "Options summary should succeed.");
        Assert.NotEmpty(optionsSummary.Data!);

        await Task.Delay(250);

        var putCallRatio = await client.TradingStatistics.GetPutCallRatioAsync("BTC", period: "1D");
        SkipIfRateLimited(putCallRatio.Error?.ToString());
        Assert.True(putCallRatio.Success, putCallRatio.Error?.ToString() ?? "Put/call ratio should succeed.");
        Assert.NotEmpty(putCallRatio.Data!);

        await Task.Delay(250);

        var expiry = await client.TradingStatistics.GetInterestVolumeExpiryAsync("BTC", period: "1D");
        SkipIfRateLimited(expiry.Error?.ToString());
        Assert.True(expiry.Success, expiry.Error?.ToString() ?? "Interest/volume by expiry should succeed.");
        Assert.NotNull(expiry.Data);
        var currentExpiry = expiry.Data.FirstOrDefault()?.ExpiryDate;
        Skip.If(string.IsNullOrWhiteSpace(currentExpiry), "No current expiry was returned by OKX.");

        await Task.Delay(250);

        var strike = await client.TradingStatistics.GetInterestVolumeStrikeAsync("BTC", currentExpiry!, period: "1D");
        SkipIfRateLimited(strike.Error?.ToString());
        Assert.True(strike.Success, strike.Error?.ToString() ?? "Interest/volume by strike should succeed.");
        Assert.NotEmpty(strike.Data!);

        await Task.Delay(250);

        var takerFlow = await client.TradingStatistics.GetTakerFlowAsync("BTC", period: "1D");
        SkipIfRateLimited(takerFlow.Error?.ToString());
        Assert.True(takerFlow.Success, takerFlow.Error?.ToString() ?? "Taker flow should succeed.");
        Assert.NotNull(takerFlow.Data);
        Assert.True(takerFlow.Data.Timestamp > 0);
    }

    private static void SkipIfRateLimited(string? errorText)
    {
        if (!string.IsNullOrEmpty(errorText) && errorText.Contains("50011", StringComparison.Ordinal))
            Skip.If(true, "OKX rate-limited the live Trading Statistics request (50011).");
    }
}
