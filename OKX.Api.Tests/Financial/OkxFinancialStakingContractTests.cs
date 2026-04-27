using ApiSharp.Models;
using Newtonsoft.Json;
using OKX.Api.Base;
using OKX.Api.Financial;
using OKX.Api.Tests.TestInfrastructure;

namespace OKX.Api.Tests.Financial;

public class OkxFinancialStakingContractTests
{
    [Fact]
    public void ManualStakingHistoryFixture_ParsesEmptyStringFieldsAsNull()
    {
        var json = FixtureReader.ReadManual("Financial", "staking-history-empty-fields.json");
        var response = JsonConvert.DeserializeObject<OkxRestApiResponse<List<OkxFinancialStakingHistory>>>(json, SerializerOptions.WithConverters);

        Assert.NotNull(response);
        Assert.Equal(0, response.ErrorCode);

        var item = Assert.Single(response.Data!);
        Assert.Equal("123456789", item.OrderId);
        Assert.Equal(0.62666630m, item.Amount);
        Assert.Null(item.RedeemingAmount);
        Assert.Equal(1683413171000L, item.CompletedTimestamp);
        Assert.Null(item.EstimatedCompletedTimestamp);
    }

    [Fact]
    public void ManualSimpleEarnBalanceFixture_ParsesDeprecatedRedemptionAmount()
    {
        var json = FixtureReader.ReadManual("Financial", "simple-earn-savings-balance.json");
        var response = JsonConvert.DeserializeObject<OkxRestApiResponse<List<OkxFinancialSimpleEarnSavingsBalance>>>(json, SerializerOptions.WithConverters);

        Assert.NotNull(response);
        Assert.Equal(0, response.ErrorCode);

        var item = Assert.Single(response.Data!);
        Assert.Equal("BTC", item.Currency);
        Assert.Equal(0.05m, item.RedemptionAmount);
    }
}
