using OKX.Api.Common;
using OKX.Api.Tests.TestInfrastructure;

namespace OKX.Api.Tests.Public;

public class OkxPublicEventContractClientBehaviorTests
{
    [Fact]
    public async Task EventContractRestEndpoints_AreUnsignedPublicRequests()
    {
        using var server = new LocalOkxRestServer(new Dictionary<string, string>
        {
            ["GET /api/v5/public/event-contract/series"] = FixtureReader.ReadManual("Public", "get-event-contract-series.json"),
            ["GET /api/v5/public/event-contract/events"] = FixtureReader.ReadManual("Public", "get-event-contract-events.json"),
            ["GET /api/v5/public/event-contract/markets"] = FixtureReader.ReadManual("Public", "get-event-contract-markets.json"),
        });
        var client = CreatePublicClient(server);

        var series = await client.Public.GetEventContractSeriesAsync();
        var events = await client.Public.GetEventContractEventsAsync("BTC-ABOVE-DAILY");
        var markets = await client.Public.GetEventContractMarketsAsync("BTC-ABOVE-DAILY");

        Assert.True(series.Success, series.Error?.ToString());
        Assert.True(events.Success, events.Error?.ToString());
        Assert.True(markets.Success, markets.Error?.ToString());
        Assert.Equal(3, server.Requests.Count);
        Assert.All(server.Requests, request =>
        {
            Assert.DoesNotContain("OK-ACCESS-KEY", request.Headers.Keys, StringComparer.OrdinalIgnoreCase);
            Assert.DoesNotContain("OK-ACCESS-SIGN", request.Headers.Keys, StringComparer.OrdinalIgnoreCase);
        });
    }

    [Fact]
    public async Task EventContractQueries_RejectUnsupportedStateBeforeRequest()
    {
        using var server = new LocalOkxRestServer(new Dictionary<string, string>());
        var client = CreatePublicClient(server);

        await Assert.ThrowsAsync<ArgumentException>(() => client.Public.GetEventContractMarketsAsync(
            "BTC-ABOVE-DAILY",
            state: OkxInstrumentState.Suspend));

        Assert.Empty(server.Requests);
    }

    [Fact]
    public async Task GetInstrumentTickBandsAsync_SupportsEventsWithoutInstrumentFamily()
    {
        using var server = new LocalOkxRestServer(new Dictionary<string, string>
        {
            ["GET /api/v5/public/instrument-tick-bands"] = FixtureReader.ReadManual("Public", "get-instrument-tick-bands-events.json"),
        });
        var client = CreatePublicClient(server);

        var result = await client.Public.GetInstrumentTickBandsAsync(OkxInstrumentType.Events);

        Assert.True(result.Success, result.Error?.ToString());
        var bands = Assert.Single(result.Data!);
        Assert.Equal(OkxInstrumentType.Events, bands.InstrumentType);
        Assert.Equal(string.Empty, bands.InstrumentFamily);
        Assert.Equal(3, bands.TickBands.Count);
        Assert.Contains("instType=EVENTS", Uri.UnescapeDataString(Assert.Single(server.Requests).Query));
    }

    [Fact]
    public async Task InstrumentsQueries_EnforceCurrentConditionalParameters()
    {
        using var server = new LocalOkxRestServer(new Dictionary<string, string>());
        var client = CreatePublicClient(server);

        await Assert.ThrowsAsync<ArgumentException>(() => client.Public.GetInstrumentsAsync(OkxInstrumentType.Events));
        await Assert.ThrowsAsync<ArgumentException>(() => client.Public.GetInstrumentsAsync(OkxInstrumentType.Option));
        await Assert.ThrowsAsync<ArgumentException>(() => client.Account.GetInstrumentsAsync(OkxInstrumentType.Events));

        Assert.Empty(server.Requests);
    }

    private static OkxRestApiClient CreatePublicClient(LocalOkxRestServer server)
        => new(new OkxRestApiOptions
        {
            AutoTimestamp = false,
            BaseAddress = server.BaseAddress,
        });
}
