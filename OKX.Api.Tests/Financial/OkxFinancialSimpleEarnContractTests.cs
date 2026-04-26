using ApiSharp.Models;
using Newtonsoft.Json;
using OKX.Api.Base;
using OKX.Api.Financial;
using OKX.Api.Tests.TestInfrastructure;

namespace OKX.Api.Tests.Financial;

public class OkxFinancialSimpleEarnContractTests
{
    [Fact]
    public void PublicBorrowHistoryFixture_ParsesLendingRate()
    {
        var response = Deserialize("Financial", "get-public-borrow-history-with-lending-rate.json");

        var item = Assert.Single(response.Data!);
        Assert.Equal("BTC", item.Currency);
        Assert.Equal(12.5m, item.Amount);
        Assert.Equal(0.0175m, item.Rate);
        Assert.Equal(0.0215m, item.LendingRate);
        Assert.Equal(1711929600000L, item.Timestamp);
    }

    [Fact]
    public void PublicBorrowHistoryFixture_LeavesLendingRateNullWhenMissing()
    {
        var response = Deserialize("Financial", "get-public-borrow-history-without-lending-rate.json");

        var item = Assert.Single(response.Data!);
        Assert.Equal("ETH", item.Currency);
        Assert.Null(item.LendingRate);
    }

    private static OkxRestApiResponse<List<OkxFinancialSimpleEarnSavingsBorrowHistory>> Deserialize(params string[] fixturePath)
    {
        var json = FixtureReader.Read(fixturePath);
        var response = JsonConvert.DeserializeObject<OkxRestApiResponse<List<OkxFinancialSimpleEarnSavingsBorrowHistory>>>(json, SerializerOptions.WithConverters);

        Assert.NotNull(response);
        Assert.Equal(0, response.ErrorCode);
        Assert.NotNull(response.Data);
        return response;
    }
}
