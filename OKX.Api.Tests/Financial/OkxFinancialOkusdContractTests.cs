using ApiSharp.Models;
using Newtonsoft.Json;
using OKX.Api.Base;
using OKX.Api.Financial;
using OKX.Api.Tests.TestInfrastructure;

namespace OKX.Api.Tests.Financial;

public class OkxFinancialOkusdContractTests
{
    [Fact]
    public void LimitsFixture_ParsesCurrentNestedQuotaContract()
    {
        var limits = Assert.Single(Deserialize<OkxFinancialOkusdLimits>("okusd-limits.json").Data!);

        Assert.Equal(45000000m, limits.SubscriptionLimit.MaximumSubscriptionAmount);
        Assert.Equal(500000m, limits.SubscriptionLimit.PersonalUsedAmount);
        Assert.Equal(0.001m, limits.FastRedemptionLimit.FeeRate);
        Assert.Equal(0.00025m, limits.StandardRedemptionLimit.FeeRate);
        Assert.Equal(1718500000000L, limits.Timestamp);
    }

    [Fact]
    public void SubscribeFixture_ParsesCurrentOrderContract()
    {
        var subscription = Assert.Single(Deserialize<OkxFinancialOkusdSubscription>("okusd-subscribe.json").Data!);

        Assert.Equal("680012345678901234", subscription.OrderId);
        Assert.Equal("my-sub-001", subscription.ClientOrderId);
        Assert.Equal("USDT", subscription.Currency);
        Assert.Equal(1000m, subscription.Amount);
        Assert.Equal(1000m, subscription.OkusdAmount);
        Assert.Equal(OkxFinancialOkusdSubscriptionState.Success, subscription.State);
    }

    [Fact]
    public void RedeemFixture_ParsesFastAndStandardContracts()
    {
        var redemptions = Deserialize<OkxFinancialOkusdRedemption>("okusd-redeem.json").Data!;

        Assert.Equal(2, redemptions.Count);
        Assert.Equal(OkxFinancialOkusdRedemptionType.Fast, redemptions[0].RedemptionType);
        Assert.Equal(OkxFinancialOkusdRedemptionState.Success, redemptions[0].State);
        Assert.Equal(999m, redemptions[0].UsdtAmount);
        Assert.Equal(OkxFinancialOkusdRedemptionType.Standard, redemptions[1].RedemptionType);
        Assert.Equal(OkxFinancialOkusdRedemptionState.Processing, redemptions[1].State);
        Assert.Equal(1718932800000L, redemptions[1].EstimatedSettlementTimestamp);
    }

    private static OkxRestApiResponse<List<T>> Deserialize<T>(string fixtureName) where T : class
    {
        var response = JsonConvert.DeserializeObject<OkxRestApiResponse<List<T>>>(
            FixtureReader.ReadManual("Financial", fixtureName),
            SerializerOptions.WithConverters);

        Assert.NotNull(response?.Data);
        return response;
    }
}