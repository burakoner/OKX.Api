using OKX.Api.Account;
using OKX.Api.Common;
using OKX.Api.Tests.TestInfrastructure;

namespace OKX.Api.Tests.Account;

public class OkxAccountFeeRateClientBehaviorTests
{
    [Fact]
    public async Task GetFeeRatesAsync_ParsesCurrentAndTransitionalFeeFields()
    {
        using var server = CreateServer();
        var client = CreateClient(server);

        var result = await client.Account.GetFeeRatesAsync(
            OkxInstrumentType.Spot,
            instrumentId: "BTC-USDT");

        Assert.True(result.Success, result.Error?.ToString());
        var feeRate = Assert.IsType<OkxAccountFeeRate>(result.Data);
        Assert.Equal("Lv1", feeRate.Level);
        Assert.Equal(OkxInstrumentType.Spot, feeRate.InstrumentType);
        Assert.Null(feeRate.Delivery);
        Assert.Null(feeRate.Exercise);
        Assert.Null(feeRate.Settlement);
        Assert.Equal(-0.001m, feeRate.DeprecatedTaker);
        Assert.Equal(-0.0008m, feeRate.DeprecatedMaker);
        Assert.Null(feeRate.DeprecatedUsdtTaker);
        Assert.Null(feeRate.DeprecatedUsdtMaker);
        Assert.Equal(-0.0009m, feeRate.DeprecatedUsdcTaker);
        Assert.Equal(-0.0007m, feeRate.DeprecatedUsdcMaker);
        Assert.Equal(OkxInstrumentRuleType.Normal, feeRate.DeprecatedRuleType);
        Assert.Equal("1", feeRate.DeprecatedCategory);

        Assert.Collection(
            feeRate.FeeGroup,
            current =>
            {
                Assert.Equal(0.0002m, current.RpiMaker);
                Assert.Null(current.ElpMaker);
                Assert.Equal(0.0002m, current.EffectiveRpiMaker);
            },
            transitional =>
            {
                Assert.Null(transitional.RpiMaker);
                Assert.Equal(0.0001m, transitional.ElpMaker);
                Assert.Equal(0.0001m, transitional.EffectiveRpiMaker);
            });

        var fiat = Assert.Single(feeRate.DeprecatedFiatFeeRates);
        Assert.Equal("EUR", fiat.Currency);
        Assert.Equal(-0.0015m, fiat.Taker);
        Assert.Equal(-0.001m, fiat.Maker);

        var request = Assert.Single(server.Requests);
        var query = Uri.UnescapeDataString(request.Query);
        Assert.Contains("instType=SPOT", query);
        Assert.Contains("instId=BTC-USDT", query);
    }

    [Fact]
    public async Task GetFeeRatesAsync_RejectsGroupIdCombinedWithInstrumentSelector()
    {
        using var server = CreateServer();
        var client = CreateClient(server);

        await Assert.ThrowsAsync<ArgumentException>(() => client.Account.GetFeeRatesAsync(
            OkxInstrumentType.Spot,
            instrumentId: "BTC-USDT",
            groupId: "3"));

        Assert.Empty(server.Requests);
    }

    [Theory]
    [InlineData(OkxInstrumentType.Futures, "BTC-USDT", null)]
    [InlineData(OkxInstrumentType.Spot, null, "BTC-USDT")]
    [InlineData(OkxInstrumentType.Contracts, null, null)]
    public async Task GetFeeRatesAsync_RejectsUnsupportedParameterCombinations(
        OkxInstrumentType instrumentType,
        string? instrumentId,
        string? instrumentFamily)
    {
        using var server = CreateServer();
        var client = CreateClient(server);

        await Assert.ThrowsAsync<ArgumentException>(() => client.Account.GetFeeRatesAsync(
            instrumentType,
            instrumentId,
            instrumentFamily));

        Assert.Empty(server.Requests);
    }

    private static LocalOkxRestServer CreateServer()
        => new(new Dictionary<string, string>
        {
            ["GET /api/v5/account/trade-fee"] = FixtureReader.ReadManual("Account", "get-fee-rates-current.json"),
        });

    private static OkxRestApiClient CreateClient(LocalOkxRestServer server)
        => new(new OkxRestApiOptions(new OkxApiCredentials("key", "secret", "pass"))
        {
            AutoTimestamp = false,
            BaseAddress = server.BaseAddress,
        });
}
