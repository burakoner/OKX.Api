using ApiSharp.Models;
using Newtonsoft.Json;
using OKX.Api.Base;
using OKX.Api.Financial;
using OKX.Api.Tests.TestInfrastructure;

namespace OKX.Api.Tests.Financial;

public class OkxFinancialSimpleEarnContractTests
{
    [Fact]
    public void ManualPublicBorrowHistoryFixture_ParsesLendingRate()
    {
        var response = DeserializeManual("Financial", "get-public-borrow-history-with-lending-rate.json");

        var item = Assert.Single(response.Data!);
        Assert.Equal("BTC", item.Currency);
        Assert.Equal(12.5m, item.Amount);
        Assert.Equal(0.0175m, item.Rate);
        Assert.Equal(0.0215m, item.LendingRate);
        Assert.Equal(1711929600000L, item.Timestamp);
    }

    [Fact]
    public void ManualPublicBorrowHistoryFixture_LeavesLendingRateNullWhenMissing()
    {
        var response = DeserializeManual("Financial", "get-public-borrow-history-without-lending-rate.json");

        var item = Assert.Single(response.Data!);
        Assert.Equal("ETH", item.Currency);
        Assert.Null(item.LendingRate);
    }

    [Fact]
    public void LivePublicBorrowHistoryFixture_ParsesCurrentOkxSnapshot()
    {
        var response = DeserializeLive("Production", "Financial", "get-public-borrow-history-btc.json");

        Assert.NotEmpty(response.Data!);
        Assert.All(response.Data!, item => Assert.Equal("BTC", item.Currency));
        Assert.All(response.Data!, item => Assert.NotNull(item.LendingRate));
        Assert.Null(response.Data!.First().Amount);
    }

    [Fact]
    public void DemoPublicBorrowHistoryFixture_ReflectsDemoAvailabilityError()
    {
        var response = DeserializeLiveAllowingError("Demo", "Financial", "get-public-borrow-history-btc.json");

        Assert.Equal(50038, response.ErrorCode);
        Assert.Empty(response.Data!);
    }

    private static OkxRestApiResponse<List<OkxFinancialSimpleEarnSavingsBorrowHistory>> DeserializeManual(params string[] fixturePath)
        => Deserialize(FixtureReader.ReadManual(fixturePath));

    private static OkxRestApiResponse<List<OkxFinancialSimpleEarnSavingsBorrowHistory>> DeserializeLive(params string[] fixturePath)
        => Deserialize(FixtureReader.ReadLive(fixturePath));

    private static OkxRestApiResponse<List<OkxFinancialSimpleEarnSavingsBorrowHistory>> DeserializeLiveAllowingError(params string[] fixturePath)
        => Deserialize(FixtureReader.ReadLive(fixturePath), requireSuccess: false);

    private static OkxRestApiResponse<List<OkxFinancialSimpleEarnSavingsBorrowHistory>> Deserialize(string json, bool requireSuccess = true)
    {
        var response = JsonConvert.DeserializeObject<OkxRestApiResponse<List<OkxFinancialSimpleEarnSavingsBorrowHistory>>>(json, SerializerOptions.WithConverters);

        Assert.NotNull(response);
        if (requireSuccess)
            Assert.Equal(0, response.ErrorCode);
        Assert.NotNull(response.Data);
        return response;
    }
}
