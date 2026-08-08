using ApiSharp.Models;
using Newtonsoft.Json;
using OKX.Api.Algo;
using OKX.Api.Tests.TestInfrastructure;

namespace OKX.Api.Tests.Algo;

public class OkxAlgoCurrentContractTests
{
    [Fact]
    public void TriggerChaseFixture_SeparatesParentAndSpawnedAlgoIdentifiers()
    {
        var orders = Deserialize("trigger-chase-orders.json");

        Assert.Equal(2, orders.Count);
        var pending = orders[0];
        Assert.Equal(OkxAlgoOrderType.Trigger, pending.OrderType);
        Assert.Equal(OkxAlgoTriggerOrderType.Chase, pending.TriggerOrderType);
        Assert.Empty(pending.OrderIdList);
        Assert.Empty(pending.SubAlgoIdList);
        var chase = Assert.Single(pending.AdvancedChaseParameters);
        Assert.Equal(OkxAlgoChaseType.Distance, chase.ChaseType);
        Assert.Equal(25.5m, chase.ChaseValue);
        Assert.Equal(OkxAlgoChaseType.Ratio, chase.MaximumChaseType);
        Assert.Equal(0.02m, chase.MaximumChaseValue);
        Assert.Null(pending.ChaseType);
        Assert.Null(pending.ChaseValue);

        var triggered = orders[1];
        Assert.Empty(triggered.OrderIdList);
        Assert.Equal([700000000000000099L], triggered.SubAlgoIdList);
        Assert.Equal(180000m, triggered.NotionalUsd);
        Assert.Equal("amend-risk-entry-1", triggered.ClientRequestId);
        Assert.Equal(OkxAlgoAmendResult.Success, triggered.AmendResult);
    }

    [Fact]
    public void SmartIcebergFixture_ParsesCurrentListAndHistoryFields()
    {
        var order = Assert.Single(Deserialize("smart-iceberg-orders.json"));

        Assert.Equal(OkxAlgoOrderType.SmartIceberg, order.OrderType);
        Assert.Equal(5m, order.AverageQuantity);
        Assert.Equal(20, order.LimitOrderNumber);
        Assert.Equal(OkxAlgoSmartIcebergAggressiveness.Mid, order.Aggressiveness);
        var trigger = Assert.Single(order.SmartIcebergTriggerParameters);
        Assert.Equal(OkxAlgoSmartIcebergTriggerAction.Start, trigger.TriggerAction);
        Assert.Equal(OkxAlgoSmartIcebergTriggerStrategy.RSI, trigger.TriggerStrategy);
        Assert.Equal(OkxAlgoSmartIcebergTriggerCondition.CrossDown, trigger.TriggerCondition);
        Assert.Equal(OkxAlgoSmartIcebergTimeFrame.ThirtyMinutes, trigger.TimeFrame);
        Assert.Equal(30, trigger.Threshold);
        Assert.Equal(14, trigger.TimePeriod);
    }

    private static List<OkxAlgoOrder> Deserialize(string fixtureName)
    {
        var orders = JsonConvert.DeserializeObject<List<OkxAlgoOrder>>(
            FixtureReader.ReadManual("Algo", fixtureName),
            SerializerOptions.WithConverters);

        Assert.NotNull(orders);
        return orders;
    }
}
