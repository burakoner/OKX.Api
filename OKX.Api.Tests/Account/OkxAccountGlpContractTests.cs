using ApiSharp.Models;
using Newtonsoft.Json;
using OKX.Api.Account;
using OKX.Api.Base;
using OKX.Api.Tests.TestInfrastructure;

namespace OKX.Api.Tests.Account;

public class OkxAccountGlpContractTests
{
    [Fact]
    public void TodayPerformanceFixture_ParsesProgramsMetricsAndNullableExpiryCategories()
    {
        var snapshot = Assert.Single(Deserialize<OkxAccountGlpPerformance>("glp-today-performance.json").Data!);

        Assert.True(snapshot.DataReady);
        Assert.Equal("2026-07-13", snapshot.DataDate);
        Assert.Equal("832545488879789797", snapshot.Account.MasterAccountId);
        Assert.Equal(["832545488879789798"], snapshot.Account.CombinedAccountIds);

        Assert.Equal(2, snapshot.Programs.Count);
        var spot = snapshot.Programs[0];
        Assert.Equal(OkxAccountGlpProgram.Spot, spot.Program);
        Assert.Equal(OkxAccountGlpEnrollmentStatus.Enrolled, spot.EnrollmentStatus);
        Assert.Equal(OkxAccountGlpQualifyingPool.TypeA, spot.QualifyingPool);
        Assert.Equal(1000000m, spot.Daily.Volume.TypeA!.Maker);
        Assert.Equal(0.0009m, spot.Daily.Share.Total.Taker);
        Assert.Equal(OkxAccountGlpMonthToDateStatus.Qualified, spot.MonthToDate.Status);
        Assert.Equal(0.0012m, spot.MonthToDate.QualifyingShare.Maker);

        var expiryAndNitro = snapshot.Programs[1];
        Assert.Equal(OkxAccountGlpProgram.ExpiryAndNitro, expiryAndNitro.Program);
        Assert.Null(expiryAndNitro.Daily.Volume.TypeA);
        Assert.Null(expiryAndNitro.Daily.Volume.TypeBTotal);
        Assert.Null(expiryAndNitro.Daily.Volume.TradFiX2);
        Assert.Null(expiryAndNitro.Daily.Share.TypeBAdjusted);
        Assert.Equal(250000m, expiryAndNitro.Daily.Volume.Total.Maker);
        Assert.Equal(OkxAccountGlpMonthToDateStatus.Upgrade, expiryAndNitro.MonthToDate.Status);
    }

    [Fact]
    public void HistoricalPerformanceFixture_ParsesNewestFirstDailyContract()
    {
        var rows = Deserialize<OkxAccountGlpHistoricalPerformance>("glp-historical-performance.json").Data!;

        Assert.Equal(2, rows.Count);
        Assert.Equal("2026-07-13", rows[0].Date);
        Assert.Equal(2000000m, rows[0].Volume.Total.Maker);
        Assert.Equal(0.0010m, rows[0].Share.Total.Maker);
        Assert.Equal("2026-07-12", rows[1].Date);
        Assert.Equal(1880000m, rows[1].Volume.Total.Taker);
    }

    private static OkxRestApiResponse<List<T>> Deserialize<T>(string fixtureName) where T : class
    {
        var response = JsonConvert.DeserializeObject<OkxRestApiResponse<List<T>>>(
            FixtureReader.ReadManual("Account", fixtureName),
            SerializerOptions.WithConverters);

        Assert.NotNull(response?.Data);
        return response;
    }
}
