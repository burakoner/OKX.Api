using ApiSharp.Models;
using Newtonsoft.Json;
using OKX.Api.Base;
using OKX.Api.Financial;
using OKX.Api.Tests.TestInfrastructure;

namespace OKX.Api.Tests.Financial;

public class OkxFinancialOnChainEarnContractTests
{
    [Fact]
    public void ManualActiveOrdersFixture_ParsesTagAndEmptySettlementFields()
    {
        var response = Deserialize("onchain-active-orders.json");

        var order = Assert.Single(response.Data!);
        Assert.Equal(OkxFinancialOnChainEarnOrderState.Redeeming, order.State);
        Assert.Equal("desk-01", order.Tag);
        Assert.Equal(1712908001000L, order.PurchasedTimestamp);
        Assert.Null(order.EstimatedSettlementTimestamp);
        Assert.Null(order.CancelRedemptionDeadlineTimestamp);
        Assert.Equal(0.25m, Assert.Single(order.EarningData).Earnings);
        Assert.Null(Assert.Single(order.EarningData).RealizedEarnings);
        Assert.Equal(0.5m, Assert.Single(order.FastRedemptionData).RedeemingAmount);
    }

    [Fact]
    public void ManualOrderHistoryFixture_ParsesCompletedStateAndHistorySpecificFields()
    {
        var response = Deserialize("onchain-order-history.json");

        var order = Assert.Single(response.Data!);
        Assert.Equal(OkxFinancialOnChainEarnOrderState.Completed, order.State);
        Assert.Equal(1712914294000L, order.RedeemedTimestamp);
        Assert.Equal(string.Empty, order.Tag);
        Assert.Null(Assert.Single(order.EarningData).Earnings);
        Assert.Equal(0.031m, Assert.Single(order.EarningData).RealizedEarnings);
    }

    private static OkxRestApiResponse<List<OkxFinancialOnChainEarnOrder>> Deserialize(string fileName)
    {
        var json = FixtureReader.ReadManual("Financial", fileName);
        var response = JsonConvert.DeserializeObject<OkxRestApiResponse<List<OkxFinancialOnChainEarnOrder>>>(json, SerializerOptions.WithConverters);

        Assert.NotNull(response);
        Assert.Equal(0, response.ErrorCode);
        Assert.NotNull(response.Data);
        return response;
    }
}
