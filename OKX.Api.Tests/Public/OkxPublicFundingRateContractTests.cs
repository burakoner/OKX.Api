using ApiSharp.Models;
using Newtonsoft.Json;
using OKX.Api.Base;
using OKX.Api.Common;
using OKX.Api.Public;
using OKX.Api.Tests.TestInfrastructure;

namespace OKX.Api.Tests.Public;

public class OkxPublicFundingRateContractTests
{
    [Fact]
    public void ManualFundingRateFixture_ParsesAnyResponseWithSwapAndXPerpFutures()
    {
        var response = DeserializeRatesManual("Public", "get-funding-rate-any-with-xperp.json");

        Assert.Collection(
            response.Data!,
            item =>
            {
                Assert.Equal(OkxInstrumentType.Swap, item.InstrumentType);
                Assert.Equal(OkxPublicFundingRateMethod.CurrentPeriod, item.Method);
#pragma warning disable CS0612
                Assert.Null(item.NextFundingRate);
#pragma warning restore CS0612
            },
            item =>
            {
                Assert.Equal(OkxInstrumentType.Futures, item.InstrumentType);
                Assert.Equal("BTC-USD_UM_XPERP-310404", item.InstrumentId);
                Assert.Equal(OkxPublicFundingRateMethod.CurrentPeriod, item.Method);
                Assert.Equal(OkxPublicSettlementState.Processing, item.SettlementState);
            });
    }

    [Fact]
    public void ManualFundingRateHistoryFixture_ParsesXPerpFuturesHistory()
    {
        var response = DeserializeHistoryManual("Public", "get-funding-rate-history-xperp.json");

        Assert.NotEmpty(response.Data!);
        Assert.All(response.Data!, item =>
        {
            Assert.Equal(OkxInstrumentType.Futures, item.InstrumentType);
            Assert.Equal("BTC-USD_UM_XPERP-310404", item.InstrumentId);
            Assert.Equal(OkxPublicFundingRateMethod.CurrentPeriod, item.Method);
        });
    }

    [Fact]
    public void LiveProductionFundingRateAnyFixture_ContainsSwapAndXPerpFutures()
    {
        var response = DeserializeRatesLive("Production", "Public", "get-funding-rate-any.json");

        Assert.NotEmpty(response.Data!);
        Assert.Contains(response.Data!, item => item.InstrumentType == OkxInstrumentType.Swap);
        Assert.Contains(response.Data!, item => item.InstrumentType == OkxInstrumentType.Futures);
        Assert.All(response.Data!, item => Assert.Equal(OkxPublicFundingRateMethod.CurrentPeriod, item.Method));
    }

    [Fact]
    public void LiveProductionFundingRateHistoryFixture_ContainsXPerpFuturesHistory()
    {
        var response = DeserializeHistoryLive("Production", "Public", "get-funding-rate-history-current-xperp.json");

        Assert.NotEmpty(response.Data!);
        Assert.All(response.Data!, item =>
        {
            Assert.Equal(OkxInstrumentType.Futures, item.InstrumentType);
            Assert.Equal(OkxPublicFundingRateMethod.CurrentPeriod, item.Method);
        });
    }

    private static OkxRestApiResponse<List<OkxPublicFundingRate>> DeserializeRatesManual(params string[] fixturePath)
        => DeserializeRates(FixtureReader.ReadManual(fixturePath));

    private static OkxRestApiResponse<List<OkxPublicFundingRate>> DeserializeRatesLive(params string[] fixturePath)
        => DeserializeRates(FixtureReader.ReadLive(fixturePath));

    private static OkxRestApiResponse<List<OkxPublicFundingRateHistory>> DeserializeHistoryManual(params string[] fixturePath)
        => DeserializeHistory(FixtureReader.ReadManual(fixturePath));

    private static OkxRestApiResponse<List<OkxPublicFundingRateHistory>> DeserializeHistoryLive(params string[] fixturePath)
        => DeserializeHistory(FixtureReader.ReadLive(fixturePath));

    private static OkxRestApiResponse<List<OkxPublicFundingRate>> DeserializeRates(string json)
    {
        var response = JsonConvert.DeserializeObject<OkxRestApiResponse<List<OkxPublicFundingRate>>>(json, SerializerOptions.WithConverters);

        Assert.NotNull(response);
        Assert.Equal(0, response.ErrorCode);
        Assert.NotNull(response.Data);
        return response;
    }

    private static OkxRestApiResponse<List<OkxPublicFundingRateHistory>> DeserializeHistory(string json)
    {
        var response = JsonConvert.DeserializeObject<OkxRestApiResponse<List<OkxPublicFundingRateHistory>>>(json, SerializerOptions.WithConverters);

        Assert.NotNull(response);
        Assert.Equal(0, response.ErrorCode);
        Assert.NotNull(response.Data);
        return response;
    }
}
