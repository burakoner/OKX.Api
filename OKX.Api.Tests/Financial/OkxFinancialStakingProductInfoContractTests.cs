using ApiSharp.Models;
using Newtonsoft.Json;
using OKX.Api.Base;
using OKX.Api.Financial;
using OKX.Api.Tests.TestInfrastructure;

namespace OKX.Api.Tests.Financial;

public class OkxFinancialStakingProductInfoContractTests
{
    [Fact]
    public void ManualEthProductInfoFixture_ParsesArrayShapeAndNewFields()
    {
        var response = DeserializeEthFixture("eth-staking-product-info.json");

        var productInfo = Assert.Single(response.Data!);
        Assert.Equal(100m, productInfo.FastRedemptionDailyLimit);
        Assert.Equal(2.23m, productInfo.Rate);
        Assert.Equal(8, productInfo.RedemptionDays);
        Assert.Equal(0.001m, productInfo.MinimumAmount);
        Assert.Null(productInfo.FastRedemptionAvailable);
    }

    [Fact]
    public void ManualSolProductInfoFixture_ParsesObjectShapeAndNewFields()
    {
        var response = DeserializeSolFixture("sol-staking-product-info.json");

        Assert.NotNull(response.Data);
        Assert.Equal(240m, response.Data.FastRedemptionAvailable);
        Assert.Equal(240m, response.Data.FastRedemptionDailyLimit);
        Assert.Equal(5.57m, response.Data.Rate);
        Assert.Equal(2, response.Data.RedemptionDays);
        Assert.Equal(0.01m, response.Data.MinimumAmount);
    }

    private static OkxRestApiResponse<List<OkxFinancialProductInfo>> DeserializeEthFixture(params string[] fixturePath)
    {
        var json = FixtureReader.ReadManual(["Financial", .. fixturePath]);
        var response = JsonConvert.DeserializeObject<OkxRestApiResponse<List<OkxFinancialProductInfo>>>(json, SerializerOptions.WithConverters);

        Assert.NotNull(response);
        Assert.Equal(0, response.ErrorCode);
        Assert.NotNull(response.Data);
        return response;
    }

    private static OkxRestApiResponse<OkxFinancialProductInfo> DeserializeSolFixture(params string[] fixturePath)
    {
        var json = FixtureReader.ReadManual(["Financial", .. fixturePath]);
        var response = JsonConvert.DeserializeObject<OkxRestApiResponse<OkxFinancialProductInfo>>(json, SerializerOptions.WithConverters);

        Assert.NotNull(response);
        Assert.Equal(0, response.ErrorCode);
        Assert.NotNull(response.Data);
        return response;
    }
}
