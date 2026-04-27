using ApiSharp.Models;
using Newtonsoft.Json;
using OKX.Api.Base;
using OKX.Api.RecurringBuy;
using OKX.Api.Tests.TestInfrastructure;
using OKX.Api.Trade;

namespace OKX.Api.Tests.RecurringBuy;

public class OkxRecurringBuyContractTests
{
    [Fact]
    public void OrderDetailsFixture_ParsesExtendedRecurringFields()
    {
        var response = DeserializeOrders("get-order-details.json");

        var order = Assert.Single(response.Data!);
        Assert.Equal(OkxRecurringBuyState.Running, order.State);
        Assert.Equal("broker-tag", order.Tag);
        Assert.Equal(["1", "3"], order.SourceCodes);
        Assert.Equal(OkxRecurringBuyTimeType.Custom, order.RecurringTimeType);
        Assert.Equal(45, order.RecurringTimeMinutes);
        Assert.Equal(1714286400000, order.NextInvestmentTimestamp);
        Assert.Equal(0.12m, order.RecurringList[0].TotalAmount);
        Assert.Null(order.RecurringList[1].MinimumPrice);
        Assert.Null(order.RecurringList[1].MaximumPrice);
    }

    [Fact]
    public void SubOrdersFixture_ParsesManualAddOrderWithoutBreakingNormalOrderMappings()
    {
        var response = DeserializeSubOrders("get-sub-orders.json");

        Assert.Equal(2, response.Data!.Count);
        Assert.Equal(OkxTradeOrderType.MarketOrder, response.Data[0].OrderType);
        Assert.Null(response.Data[1].OrderType);
        Assert.Equal("manual_add_order", response.Data[1].OrderTypeLabel);
        Assert.Null(response.Data[0].TradeMode);
        Assert.Equal("USDT", response.Data[1].TradeQuoteCurrency);
    }

    private static OkxRestApiResponse<List<OkxRecurringBuyOrder>> DeserializeOrders(string fixtureName)
    {
        var response = JsonConvert.DeserializeObject<OkxRestApiResponse<List<OkxRecurringBuyOrder>>>(FixtureReader.ReadManual("RecurringBuy", fixtureName), SerializerOptions.WithConverters);

        Assert.NotNull(response);
        Assert.Equal(0, response!.ErrorCode);
        Assert.NotNull(response.Data);
        return response;
    }

    private static OkxRestApiResponse<List<OkxRecurringBuySubOrder>> DeserializeSubOrders(string fixtureName)
    {
        var response = JsonConvert.DeserializeObject<OkxRestApiResponse<List<OkxRecurringBuySubOrder>>>(FixtureReader.ReadManual("RecurringBuy", fixtureName), SerializerOptions.WithConverters);

        Assert.NotNull(response);
        Assert.Equal(0, response!.ErrorCode);
        Assert.NotNull(response.Data);
        return response;
    }
}
