using OKX.Api.Common;
using OKX.Api.Tests.TestInfrastructure;
using Xunit;

namespace OKX.Api.Tests.Integration;

public class OkxPublicEventContractIntegrationTests
{
    [SkippableFact]
    public async Task AuthenticatedEventContractEndpoints_ReturnCurrentEventData()
    {
        var configuration = TestConfiguration.Load();
        Skip.IfNot(configuration.RunIntegrationTests, "Set OKX_RUN_INTEGRATION_TESTS=true in .env to enable live integration tests.");
        Skip.IfNot(configuration.HasApiCredentials, "Set OKX_API_KEY, OKX_API_SECRET, and OKX_API_PASSPHRASE in .env to enable authenticated public event-contract integration tests.");

        var client = OkxRestClientFactory.CreatePrivate(configuration);

        var seriesResult = await client.Public.GetEventContractSeriesAsync(configuration.PublicEventsSeriesId);
        Skip.If(seriesResult.Error?.Code == 50011, "OKX rate-limited the live event contract series request.");
        Assert.True(seriesResult.Success, seriesResult.Error?.ToString() ?? "Event contract series request should succeed.");
        Assert.NotNull(seriesResult.Data);
        Assert.NotEmpty(seriesResult.Data);
        Assert.Contains(seriesResult.Data, x => x.SeriesId == configuration.PublicEventsSeriesId);

        var eventsResult = await client.Public.GetEventContractEventsAsync(configuration.PublicEventsSeriesId, limit: 5);
        Skip.If(eventsResult.Error?.Code == 50011, "OKX rate-limited the live event contract events request.");
        Assert.True(eventsResult.Success, eventsResult.Error?.ToString() ?? "Event contract events request should succeed.");
        Assert.NotNull(eventsResult.Data);
        Assert.NotEmpty(eventsResult.Data);
        Assert.All(eventsResult.Data, x => Assert.Equal(configuration.PublicEventsSeriesId, x.SeriesId));

        var marketsResult = await client.Public.GetEventContractMarketsAsync(configuration.PublicEventsSeriesId, limit: 5);
        Skip.If(marketsResult.Error?.Code == 50011, "OKX rate-limited the live event contract markets request.");
        Assert.True(marketsResult.Success, marketsResult.Error?.ToString() ?? "Event contract markets request should succeed.");
        Assert.NotNull(marketsResult.Data);
        Assert.NotEmpty(marketsResult.Data);
        Assert.All(marketsResult.Data, x => Assert.Equal(configuration.PublicEventsSeriesId, x.SeriesId));

        var instrumentsResult = await client.Public.GetInstrumentsAsync(OkxInstrumentType.Events, seriesId: configuration.PublicEventsSeriesId, signed: true);
        Skip.If(instrumentsResult.Error?.Code == 50011, "OKX rate-limited the live authenticated event instruments request.");
        Assert.True(instrumentsResult.Success, instrumentsResult.Error?.ToString() ?? "Authenticated event instruments request should succeed.");
        Assert.NotNull(instrumentsResult.Data);
        Assert.NotEmpty(instrumentsResult.Data);
        Assert.All(instrumentsResult.Data, x => Assert.Equal(OkxInstrumentType.Events, x.InstrumentType));

        var market = marketsResult.Data.FirstOrDefault(x => !string.IsNullOrWhiteSpace(x.InstrumentId));
        Skip.If(market is null, "No live event contract market instrument was returned by OKX.");

        var openInterestResult = await client.Public.GetOpenInterestsAsync(OkxInstrumentType.Events, instrumentId: market.InstrumentId);
        Skip.If(openInterestResult.Error?.Code == 50011, "OKX rate-limited the live event contract open interest request.");
        Assert.True(openInterestResult.Success, openInterestResult.Error?.ToString() ?? "Event contract open interest request should succeed.");
        Assert.NotNull(openInterestResult.Data);

        var markPriceResult = await client.Public.GetMarkPricesAsync(OkxInstrumentType.Events, instrumentId: market.InstrumentId);
        Skip.If(markPriceResult.Error?.Code == 50011, "OKX rate-limited the live event contract mark price request.");
        Assert.True(markPriceResult.Success, markPriceResult.Error?.ToString() ?? "Event contract mark price request should succeed.");
        Assert.NotNull(markPriceResult.Data);
    }
}
