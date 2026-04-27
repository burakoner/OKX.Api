using OKX.Api.Common;
using OKX.Api.Spread;
using OKX.Api.Tests.TestInfrastructure;
using Xunit;

namespace OKX.Api.Tests.Integration;

public class OkxSpreadPublicIntegrationTests
{
    [SkippableFact]
    public async Task PublicSpreadEndpoints_ReturnCurrentLiveData()
    {
        var configuration = TestConfiguration.Load();
        Skip.IfNot(configuration.RunIntegrationTests, "Set OKX_RUN_INTEGRATION_TESTS=true in .env to enable live integration tests.");

        var client = OkxRestClientFactory.CreatePublic(configuration);

        var spreadsResult = await client.Spread.GetSpreadsAsync(baseCurrency: "BTC");
        Assert.True(spreadsResult.Success, spreadsResult.Error?.ToString() ?? "Spread list request should succeed.");
        Assert.NotNull(spreadsResult.Data);
        Assert.NotEmpty(spreadsResult.Data);

        var spreadId = ResolveSpreadId(spreadsResult.Data);

        var orderBookResult = await client.Spread.GetOrderBookAsync(spreadId, depth: 5);
        Assert.True(orderBookResult.Success, orderBookResult.Error?.ToString() ?? "Spread order book request should succeed.");
        Assert.NotNull(orderBookResult.Data);

        var tickerResult = await client.Spread.GetTickerAsync(spreadId);
        Assert.True(tickerResult.Success, tickerResult.Error?.ToString() ?? "Spread ticker request should succeed.");
        Assert.NotNull(tickerResult.Data);
        Assert.Equal(spreadId, tickerResult.Data.SpreadId);

        var publicTradesResult = await client.Spread.GetTradesAsync(spreadId, CancellationToken.None);
        Assert.True(publicTradesResult.Success, publicTradesResult.Error?.ToString() ?? "Spread public trades request should succeed.");
        Assert.NotNull(publicTradesResult.Data);

        var candlesResult = await client.Spread.GetCandlesticksAsync(spreadId, OkxPeriod.OneDay, limit: 2);
        Assert.True(candlesResult.Success, candlesResult.Error?.ToString() ?? "Spread candlesticks request should succeed.");
        Assert.NotNull(candlesResult.Data);
        Assert.NotEmpty(candlesResult.Data);

        var historyResult = await client.Spread.GetCandlesticksHistoryAsync(spreadId, OkxPeriod.OneDay, limit: 2);
        Assert.True(historyResult.Success, historyResult.Error?.ToString() ?? "Spread candlesticks history request should succeed.");
        Assert.NotNull(historyResult.Data);
        Assert.NotEmpty(historyResult.Data);
    }

    private static string ResolveSpreadId(IEnumerable<OkxSpreadInstrument> spreads)
    {
        var preferred = spreads.FirstOrDefault(x => x.SpreadId == "BTC-USDT_BTC-USDT-SWAP");
        if (preferred is not null)
            return preferred.SpreadId;

        var live = spreads.FirstOrDefault(x => x.State == OkxSpreadInstrumentState.Live);
        if (live is not null)
            return live.SpreadId;

        return spreads.First().SpreadId;
    }
}
