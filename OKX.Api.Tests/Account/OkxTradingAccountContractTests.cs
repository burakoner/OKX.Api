using ApiSharp.Models;
using Newtonsoft.Json;
using OKX.Api.Account;
using OKX.Api.Base;
using OKX.Api.Tests.TestInfrastructure;
using OKX.Api.Trade;

namespace OKX.Api.Tests.Account;

public class OkxTradingAccountContractTests
{
    [Fact]
    public void ManualPositionTiersFixture_ParsesMultipleRowsAndEmptyPositionType()
    {
        var response = DeserializeRest<OkxAccountPositionTiers>("Account", "get-position-tiers-multi.json");

        Assert.Equal(2, response.Data!.Count);

        var btc = response.Data[0];
        Assert.Equal("BTC-USDT", btc.InstrumentFamily);
        Assert.Null(btc.PositionType);

        var eth = response.Data[1];
        Assert.Equal(OkxTradePositionType.ContractsOfPendingOrdersAndOpenPositionsForAllDerivativesInstruments, eth.PositionType);
    }

    [Fact]
    public void ManualSetTradingConfigFixture_ParsesStrategyType()
    {
        var response = DeserializeRest<OkxAccountTradingConfig>("Account", "set-trading-config.json");

        var data = Assert.Single(response.Data!);
        Assert.Equal("stgyType", data.Type);
        Assert.Equal(OkxAccountStrategyType.DeltaNeutral, data.StrategyType);
    }

    [Fact]
    public void ManualPrecheckSetDeltaNeutralFixture_ParsesDeltaLeverAndOrderLists()
    {
        var response = DeserializeRest<OkxAccountPrecheckSetDeltaNeutral>("Account", "precheck-set-delta-neutral.json");

        var data = Assert.Single(response.Data!);
        Assert.NotNull(data.UnmatchedInformationList);

        var deltaRisk = Assert.Single(data.UnmatchedInformationList!, x => x.Type == "delta_risk");
        Assert.Equal(1.25m, deltaRisk.DeltaLeverage);

        var pendingOrders = Assert.Single(data.UnmatchedInformationList, x => x.Type == "isolated_pending_orders");
        Assert.Equal(2, pendingOrders.OrderList!.Count);

        var isolatedMargin = Assert.Single(data.UnmatchedInformationList, x => x.Type == "isolated_margin");
        Assert.Equal("998877", Assert.Single(isolatedMargin.PositionList!));
    }

    private static OkxRestApiResponse<List<T>> DeserializeRest<T>(params string[] fixturePath) where T : class
    {
        var json = FixtureReader.ReadManual(fixturePath);
        var response = JsonConvert.DeserializeObject<OkxRestApiResponse<List<T>>>(json, SerializerOptions.WithConverters);

        Assert.NotNull(response);
        Assert.Equal(0, response.ErrorCode);
        Assert.NotNull(response.Data);
        return response;
    }
}
