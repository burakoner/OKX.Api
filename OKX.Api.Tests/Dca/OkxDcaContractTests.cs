using ApiSharp.Models;
using Newtonsoft.Json;
using OKX.Api.Base;
using OKX.Api.Dca;
using OKX.Api.Tests.TestInfrastructure;

namespace OKX.Api.Tests.Dca;

public class OkxDcaContractTests
{
    [Fact]
    public void OrderResponseFixture_ParsesGenericOrderReferences()
    {
        var response = DeserializeSingle<OkxDcaOrderResponse>("order-response.json");

        Assert.Equal(532177187189760000, response.AlgoOrderId);
        Assert.Equal("dca-create-01", response.AlgoClientOrderId);
        Assert.Equal(OkxDcaAlgoOrderType.ContractDca, response.AlgoOrderType);
    }

    [Fact]
    public void ManualBuyResponseFixture_ParsesDiffAmountAndTag()
    {
        var response = DeserializeSingle<OkxDcaOrderResponse>("manual-buy-response.json");

        Assert.Equal(15.5m, response.DifferenceAmount);
        Assert.Equal("manual-buy-tag", response.Tag);
    }

    [Fact]
    public void OngoingOrdersFixture_ParsesFundingFeeAliasAndNullableProfitSharingRatio()
    {
        var response = DeserializeList<OkxDcaOrder>("get-ongoing-orders.json");

        var order = Assert.Single(response.Data!);
        Assert.Equal(OkxDcaAlgoOrderType.ContractDca, order.AlgoOrderType);
        Assert.Equal(1.25m, order.FundingFee);
        Assert.Null(order.ProfitSharingRatio);
        Assert.Equal(OkxDcaTrackingMode.Sync, order.TrackingMode);
        Assert.Equal(OkxDcaTriggerStrategy.Price, Assert.Single(order.TriggerParameters).TriggerStrategy);
    }

    [Fact]
    public void OrderHistoryFixture_ParsesWebhookTriggerStrategyAndFundingFee()
    {
        var response = DeserializeList<OkxDcaOrder>("get-order-history.json");

        var order = Assert.Single(response.Data!);
        Assert.Equal(0.85m, order.FundingFee);
        Assert.Equal(0.1m, order.ProfitSharingRatio);
        Assert.Equal(OkxDcaTriggerStrategy.Webhook, Assert.Single(order.TriggerParameters).TriggerStrategy);
    }

    [Fact]
    public void SubOrdersFixture_ParsesRecurringSpecificOrderTypes()
    {
        var response = DeserializeList<OkxDcaSubOrder>("get-sub-orders.json");

        var order = Assert.Single(response.Data!);
        Assert.Equal("manual_close_position", order.OrderType);
        Assert.Equal("filled", order.State);
        Assert.Equal(42050m, order.AverageFillPrice);
        Assert.Equal(1714210000000, order.FillTimestamp);
    }

    [Fact]
    public void PositionDetailsFixture_ParsesCurrentCycleIdTypoAndBlankSpotBalances()
    {
        var response = DeserializeSingle<OkxDcaPositionDetails>("get-position-details.json");

        Assert.Equal(88, response.CurrentCycleId);
        Assert.Equal(2, response.FilledManualOrders);
        Assert.Null(response.BaseSize);
        Assert.Equal(120m, response.QuoteSize);
        Assert.Null(response.LiquidationPrice);
    }

    [Fact]
    public void CycleListFixture_ParsesNullableEndTime()
    {
        var response = DeserializeList<OkxDcaCycle>("get-cycle-list.json");

        var cycle = Assert.Single(response.Data!);
        Assert.True(cycle.IsCurrentCycle);
        Assert.Null(cycle.EndTimestamp);
        Assert.Equal(0.45m, cycle.RealizedProfitAndLoss);
    }

    private static OkxRestApiResponse<List<T>> DeserializeList<T>(string fixtureName) where T : class
    {
        var response = JsonConvert.DeserializeObject<OkxRestApiResponse<List<T>>>(FixtureReader.ReadManual("Dca", fixtureName), SerializerOptions.WithConverters);

        Assert.NotNull(response);
        Assert.Equal(0, response!.ErrorCode);
        Assert.NotNull(response.Data);
        return response;
    }

    private static T DeserializeSingle<T>(string fixtureName) where T : class
        => Assert.Single(DeserializeList<T>(fixtureName).Data!);
}
