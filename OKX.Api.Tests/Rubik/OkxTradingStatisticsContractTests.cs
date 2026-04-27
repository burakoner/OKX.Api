using ApiSharp.Models;
using Newtonsoft.Json;
using OKX.Api.Base;
using OKX.Api.Rubik;
using OKX.Api.Tests.TestInfrastructure;

namespace OKX.Api.Tests.Rubik;

public class OkxTradingStatisticsContractTests
{
    [Fact]
    public void ManualSupportCoinFixture_ParsesDocsShape()
    {
        var response = DeserializeObjectResponse<OkxRubikSupportCoins>(FixtureReader.ReadManual("Rubik", "get-support-coin.json"));

        Assert.Contains("BTC", response.Data!.Contract);
        Assert.Contains("BTC", response.Data.Option);
        Assert.Contains("ADA", response.Data.Spot);
    }

    [Fact]
    public void ManualContractOpenInterestHistoryFixture_ParsesDocsShape()
    {
        var response = DeserializeListResponse<OkxRubikContractOpenInterestHistory>(FixtureReader.ReadManual("Rubik", "get-contract-open-interest-history.json"));

        var first = response.Data!.First();
        Assert.Equal(1701417600000L, first.Timestamp);
        Assert.Equal(731377.57500501m, first.OpenInterestInTheUnitOfContracts);
        Assert.Equal(111m, first.OpenInterestInTheUnitOfCrypto);
        Assert.Equal(8888888m, first.OpenInterestInTheUnitOfUsd);
    }

    [Fact]
    public void ManualTakerVolumeFixtures_ParseDocsShape()
    {
        var spotResponse = DeserializeListResponse<OkxRubikTakerVolume>(FixtureReader.ReadManual("Rubik", "get-taker-volume-spot.json"));
        var contractResponse = DeserializeListResponse<OkxRubikContractTakerVolume>(FixtureReader.ReadManual("Rubik", "get-contract-taker-volume.json"));

        var spot = spotResponse.Data!.First();
        Assert.Equal(7596.2651m, spot.SellVolume);
        Assert.Equal(7149.4855m, spot.BuyVolume);

        var contract = contractResponse.Data!.First();
        Assert.Equal(2354.12m, contract.SellVolume);
        Assert.Equal(3123.45m, contract.BuyVolume);
    }

    [Fact]
    public void ManualRatioFixtures_ParseDocsShape()
    {
        var marginLending = DeserializeListResponse<OkxRubikRatio>(FixtureReader.ReadManual("Rubik", "get-margin-lending-ratio.json"));
        var topTraderAccounts = DeserializeListResponse<OkxRubikRatio>(FixtureReader.ReadManual("Rubik", "get-top-traders-contract-long-short-account-ratio.json"));
        var topTraderPositions = DeserializeListResponse<OkxRubikRatio>(FixtureReader.ReadManual("Rubik", "get-top-traders-contract-long-short-position-ratio.json"));
        var contractRatio = DeserializeListResponse<OkxRubikRatio>(FixtureReader.ReadManual("Rubik", "get-contract-long-short-ratio.json"));
        var longShortRatio = DeserializeListResponse<OkxRubikRatio>(FixtureReader.ReadManual("Rubik", "get-long-short-ratio.json"));

        Assert.Equal(0.45m, marginLending.Data!.First().Ratio);
        Assert.Equal(1.21m, topTraderAccounts.Data!.First().Ratio);
        Assert.Equal(1.34m, topTraderPositions.Data!.First().Ratio);
        Assert.Equal(0.98m, contractRatio.Data!.First().Ratio);
        Assert.Equal(1.06m, longShortRatio.Data!.First().Ratio);
    }

    [Fact]
    public void ManualInterestVolumeFixtures_ParseDocsShape()
    {
        var contractSummary = DeserializeListResponse<OkxRubikInterestVolume>(FixtureReader.ReadManual("Rubik", "get-contract-summary.json"));
        var optionsSummary = DeserializeListResponse<OkxRubikInterestVolume>(FixtureReader.ReadManual("Rubik", "get-options-summary.json"));
        var contractSummaryData = contractSummary.Data;
        var optionsSummaryData = optionsSummary.Data;
        Assert.NotNull(contractSummaryData);
        Assert.NotNull(optionsSummaryData);

        Assert.Equal(5412.23m, contractSummaryData!.First().OpenInterest);
        Assert.Equal(12876.11m, contractSummaryData.First().Volume);
        Assert.Equal(24.8m, optionsSummaryData!.First().OpenInterest);
        Assert.Equal(11.1m, optionsSummaryData.First().Volume);
    }

    [Fact]
    public void ManualPutCallRatioFixture_ParsesDocsShape()
    {
        var response = DeserializeListResponse<OkxRubikPutCallRatio>(FixtureReader.ReadManual("Rubik", "get-put-call-ratio.json"));
        var first = response.Data!.First();

        Assert.Equal(1.25m, first.OpenInterestRatio);
        Assert.Equal(0.88m, first.VolumeRatio);
    }

    [Fact]
    public void ManualInterestVolumeExpiryFixture_ParsesDocsShapeAndExpiryDate()
    {
        var response = DeserializeListResponse<OkxRubikInterestVolumeExpiry>(FixtureReader.ReadManual("Rubik", "get-interest-volume-expiry.json"));
        var first = response.Data!.First();

        Assert.Equal(20210902L, first.ExpiryTimestamp);
        Assert.Equal("20210902", first.ExpiryDate);
        Assert.Equal(new DateTime(2021, 9, 2, 0, 0, 0, DateTimeKind.Utc), first.ExpiryTime);
        Assert.Equal(6.4m, first.CallOpenInterest);
        Assert.Equal(18.4m, first.PutOpenInterest);
    }

    [Fact]
    public void ManualInterestVolumeStrikeFixture_ParsesDocsShape()
    {
        var response = DeserializeListResponse<OkxRubikInterestVolumeStrike>(FixtureReader.ReadManual("Rubik", "get-interest-volume-strike.json"));
        var first = response.Data!.First();

        Assert.Equal("10000", first.Strike);
        Assert.Equal(0m, first.CallOpenInterest);
        Assert.Equal(0.5m, first.PutOpenInterest);
    }

    [Fact]
    public void ManualTakerFlowFixture_ParsesDocsShape()
    {
        var response = DeserializeArrayModelResponse<OkxRubikTakerFlow>(FixtureReader.ReadManual("Rubik", "get-taker-flow.json"));

        Assert.Equal(1630512000000L, response.Data!.Timestamp);
        Assert.Equal("8.55", response.Data.CallOptionBuyVolume);
        Assert.Equal("67.3", response.Data.CallOptionSellVolume);
        Assert.Equal("16.05", response.Data.PutOptionBuyVolume);
        Assert.Equal("16.3", response.Data.PutOptionSellVolume);
        Assert.Equal(126.4m, response.Data.CallBlockVolume);
        Assert.Equal(40.7m, response.Data.PutBlockVolume);
    }

    [Fact]
    public void LiveSupportCoinFixture_ParsesCurrentSnapshot()
    {
        var response = DeserializeObjectResponse<OkxRubikSupportCoins>(FixtureReader.ReadLive("Production", "Rubik", "get-support-coin.json"));

        Assert.Contains("BTC", response.Data!.Contract);
        Assert.Contains("BTC", response.Data.Option);
        Assert.Contains("BTC", response.Data.Spot);
    }

    [Fact]
    public void LiveContractOpenInterestHistoryFixture_ParsesCurrentSnapshot()
    {
        var response = DeserializeListResponse<OkxRubikContractOpenInterestHistory>(FixtureReader.ReadLive("Production", "Rubik", "get-contract-open-interest-history-btc-usdt-swap.json"));

        Assert.NotEmpty(response.Data!);
        Assert.All(response.Data!, x => Assert.True(x.OpenInterestInTheUnitOfUsd >= 0m));
    }

    [Fact]
    public void LiveTakerVolumeFixtures_ParseCurrentSnapshots()
    {
        var spotResponse = DeserializeListResponse<OkxRubikTakerVolume>(FixtureReader.ReadLive("Production", "Rubik", "get-taker-volume-btc-spot.json"));
        var contractsResponse = DeserializeListResponse<OkxRubikTakerVolume>(FixtureReader.ReadLive("Production", "Rubik", "get-taker-volume-btc-contracts.json"));
        var contractResponse = DeserializeListResponse<OkxRubikContractTakerVolume>(FixtureReader.ReadLive("Production", "Rubik", "get-contract-taker-volume-btc-usdt-swap.json"));

        Assert.NotEmpty(spotResponse.Data!);
        Assert.NotEmpty(contractsResponse.Data!);
        Assert.NotEmpty(contractResponse.Data!);
    }

    [Fact]
    public void LiveRatioFixtures_ParseCurrentSnapshots()
    {
        var marginLending = DeserializeListResponse<OkxRubikRatio>(FixtureReader.ReadLive("Production", "Rubik", "get-margin-lending-ratio-btc.json"));
        var topTraderAccounts = DeserializeListResponse<OkxRubikRatio>(FixtureReader.ReadLive("Production", "Rubik", "get-top-traders-contract-long-short-account-ratio-btc-usdt-swap.json"));
        var topTraderPositions = DeserializeListResponse<OkxRubikRatio>(FixtureReader.ReadLive("Production", "Rubik", "get-top-traders-contract-long-short-position-ratio-btc-usdt-swap.json"));
        var contractRatio = DeserializeListResponse<OkxRubikRatio>(FixtureReader.ReadLive("Production", "Rubik", "get-contract-long-short-ratio-btc-usdt-swap.json"));
        var longShortRatio = DeserializeListResponse<OkxRubikRatio>(FixtureReader.ReadLive("Production", "Rubik", "get-long-short-ratio-btc.json"));

        Assert.NotEmpty(marginLending.Data!);
        Assert.NotEmpty(topTraderAccounts.Data!);
        Assert.NotEmpty(topTraderPositions.Data!);
        Assert.NotEmpty(contractRatio.Data!);
        Assert.NotEmpty(longShortRatio.Data!);
    }

    [Fact]
    public void LiveInterestVolumeFixtures_ParseCurrentSnapshots()
    {
        var contractSummary = DeserializeListResponse<OkxRubikInterestVolume>(FixtureReader.ReadLive("Production", "Rubik", "get-contract-summary-btc.json"));
        var optionsSummary = DeserializeListResponse<OkxRubikInterestVolume>(FixtureReader.ReadLive("Production", "Rubik", "get-options-summary-btc.json"));

        Assert.NotEmpty(contractSummary.Data!);
        Assert.NotEmpty(optionsSummary.Data!);
    }

    [Fact]
    public void LivePutCallRatioFixture_ParsesCurrentSnapshot()
    {
        var response = DeserializeListResponse<OkxRubikPutCallRatio>(FixtureReader.ReadLive("Production", "Rubik", "get-put-call-ratio-btc.json"));

        Assert.NotEmpty(response.Data!);
    }

    [Fact]
    public void LiveInterestVolumeExpiryFixture_ParsesCurrentSnapshotAndCurrentExpiryDates()
    {
        var response = DeserializeListResponse<OkxRubikInterestVolumeExpiry>(FixtureReader.ReadLive("Production", "Rubik", "get-interest-volume-expiry-btc.json"));

        Assert.NotEmpty(response.Data!);
        Assert.All(response.Data!, x =>
        {
            Assert.Equal(8, x.ExpiryDate.Length);
            Assert.NotEqual(DateTime.MinValue, x.ExpiryTime);
        });
    }

    [Fact]
    public void LiveInterestVolumeStrikeFixture_ParsesCurrentSnapshot()
    {
        var response = DeserializeListResponse<OkxRubikInterestVolumeStrike>(FixtureReader.ReadLive("Production", "Rubik", "get-interest-volume-strike-btc-current.json"));

        Assert.NotEmpty(response.Data!);
        Assert.All(response.Data!, x => Assert.False(string.IsNullOrWhiteSpace(x.Strike)));
    }

    [Fact]
    public void LiveTakerFlowFixture_ParsesCurrentSnapshot()
    {
        var response = DeserializeArrayModelResponse<OkxRubikTakerFlow>(FixtureReader.ReadLive("Production", "Rubik", "get-taker-flow-btc.json"));

        Assert.NotNull(response.Data);
        Assert.True(response.Data.Timestamp > 0);
    }

    private static OkxRestApiResponse<T> DeserializeObjectResponse<T>(string json) where T : class
    {
        var response = JsonConvert.DeserializeObject<OkxRestApiResponse<T>>(json, SerializerOptions.WithConverters);

        Assert.NotNull(response);
        Assert.Equal(0, response.ErrorCode);
        Assert.NotNull(response.Data);
        return response;
    }

    private static OkxRestApiResponse<List<T>> DeserializeListResponse<T>(string json) where T : class
    {
        var response = JsonConvert.DeserializeObject<OkxRestApiResponse<List<T>>>(json, SerializerOptions.WithConverters);

        Assert.NotNull(response);
        Assert.Equal(0, response.ErrorCode);
        Assert.NotNull(response.Data);
        return response;
    }

    private static OkxRestApiResponse<T> DeserializeArrayModelResponse<T>(string json) where T : class
    {
        var response = JsonConvert.DeserializeObject<OkxRestApiResponse<T>>(json, SerializerOptions.WithConverters);

        Assert.NotNull(response);
        Assert.Equal(0, response.ErrorCode);
        Assert.NotNull(response.Data);
        return response;
    }
}
