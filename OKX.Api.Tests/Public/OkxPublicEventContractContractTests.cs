using ApiSharp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OKX.Api.Base;
using OKX.Api.Common;
using OKX.Api.Public;
using OKX.Api.Tests.TestInfrastructure;

namespace OKX.Api.Tests.Public;

public class OkxPublicEventContractContractTests
{
    [Fact]
    public void ManualEventContractSeriesFixture_ParsesSeriesResponse()
    {
        var response = DeserializeRest<OkxPublicEventContractSeries>("Public", "get-event-contract-series.json");

        Assert.Equal(2, response.Data!.Count);

        var daily = response.Data.Single(x => x.SeriesId == "BTC-ABOVE-DAILY");
        Assert.Equal(OkxPublicEventContractFrequency.Daily, daily.Frequency);
        Assert.Equal("BTC price above 150k", daily.Title);
        Assert.Equal("Crypto", daily.Category);
        Assert.Equal(OkxPublicEventContractSettlementMethod.PriceAbove, daily.Settlement.Method);
        Assert.False(daily.Settlement.CloseEarly);
        Assert.Equal("okx_index", daily.Settlement.SourceName);
        Assert.Equal("BTC-USDT", daily.Settlement.Underlying);

        var fifteenMinute = response.Data.Single(x => x.SeriesId == "BTC-UPDOWN-FIFTEEN-MIN");
        Assert.Equal(OkxPublicEventContractFrequency.FifteenMinute, fifteenMinute.Frequency);
        Assert.Equal(OkxPublicEventContractSettlementMethod.PriceUpDown, fifteenMinute.Settlement.Method);
        Assert.True(fifteenMinute.Settlement.CloseEarly);
    }

    [Fact]
    public void ManualEventContractEventsFixture_ParsesEventStatesAndOptionalFixTimes()
    {
        var response = DeserializeRest<OkxPublicEventContractEvent>("Public", "get-event-contract-events.json");

        Assert.Equal(2, response.Data!.Count);

        var settling = response.Data.Single(x => x.EventId == "BTC-ABOVE-DAILY-260224-1600");
        Assert.Equal(OkxInstrumentState.Settling, settling.State);
        Assert.Null(settling.FixingTimestamp);
        Assert.Equal(1769697132335L, settling.ExpiryTimestamp);

        var expired = response.Data.Single(x => x.EventId == "BTC-UPDOWN-FIFTEEN-MIN-260224-1615");
        Assert.Equal(OkxInstrumentState.Expired, expired.State);
        Assert.Equal(1769697000000L, expired.FixingTimestamp);
    }

    [Fact]
    public void ManualEventContractMarketsFixture_ParsesOutcomeAndSettlementValues()
    {
        var response = DeserializeRest<OkxPublicEventContractMarket>("Public", "get-event-contract-markets.json");

        Assert.Equal(2, response.Data!.Count);

        var live = response.Data.Single(x => x.InstrumentId == "BTC-ABOVE-DAILY-260224-1600-65000");
        Assert.Equal(OkxInstrumentState.Live, live.State);
        Assert.Equal(OkxPublicEventContractMarketOutcome.NotAvailable, live.Outcome);
        Assert.Equal(65000m, live.FloorStrike);
        Assert.Null(live.SettlementValue);
        Assert.False(live.IsDisputed);

        var expired = response.Data.Single(x => x.InstrumentId == "BTC-UPDOWN-FIFTEEN-MIN-260224-1615-120000");
        Assert.Equal(OkxInstrumentState.Expired, expired.State);
        Assert.Equal(OkxPublicEventContractMarketOutcome.No, expired.Outcome);
        Assert.Equal(118500.5m, expired.SettlementValue);
        Assert.True(expired.IsDisputed);
    }

    [Fact]
    public void ManualEventContractMarketsSocketFixture_ParsesPushData()
    {
        var items = DeserializeSocketData<OkxPublicEventContractMarket>("Public", "ws-event-contract-markets.json");

        var market = Assert.Single(items);
        Assert.Equal("BTC-ABOVE-DAILY", market.SeriesId);
        Assert.Equal(OkxPublicEventContractMarketOutcome.NotAvailable, market.Outcome);
        Assert.Equal(65000m, market.FloorStrike);
    }

    [Fact]
    public void ManualEventInstrumentsFixture_ParsesSeriesIdAndSettlingState()
    {
        var response = DeserializeRest<OkxPublicInstrument>("Public", "get-instruments-events-seriesid.json");

        Assert.Equal(2, response.Data!.Count);

        var live = response.Data.Single(x => x.InstrumentId == "BTC-ABOVE-DAILY-260224-1600-65000");
        Assert.Equal("BTC-ABOVE-DAILY", live.SeriesId);
        Assert.Equal(OkxInstrumentState.Live, live.State);

        var settling = response.Data.Single(x => x.InstrumentId == "BTC-ABOVE-DAILY-260224-1600-70000");
        Assert.Equal("BTC-ABOVE-DAILY", settling.SeriesId);
        Assert.Equal(OkxInstrumentState.Settling, settling.State);
    }

    [Fact]
    public void LiveProductionEventContractSeriesFixture_ParsesCurrentOkxSnapshot()
    {
        var response = DeserializeRest<OkxPublicEventContractSeries>(FixtureReader.ReadLive("Production", "Public", "get-event-contract-series-btc-above-daily.json"));

        Assert.NotEmpty(response.Data!);
        Assert.Contains(response.Data!, x => x.SeriesId == "BTC-ABOVE-DAILY");
    }

    [Fact]
    public void LiveProductionEventContractEventsFixture_ParsesCurrentOkxSnapshot()
    {
        var response = DeserializeRest<OkxPublicEventContractEvent>(FixtureReader.ReadLive("Production", "Public", "get-event-contract-events-btc-above-daily.json"));

        Assert.NotEmpty(response.Data!);
        Assert.All(response.Data!, x => Assert.Equal("BTC-ABOVE-DAILY", x.SeriesId));
    }

    [Fact]
    public void LiveProductionEventContractMarketsFixture_ParsesCurrentOkxSnapshot()
    {
        var response = DeserializeRest<OkxPublicEventContractMarket>(FixtureReader.ReadLive("Production", "Public", "get-event-contract-markets-btc-above-daily.json"));

        Assert.NotEmpty(response.Data!);
        Assert.All(response.Data!, x => Assert.Equal("BTC-ABOVE-DAILY", x.SeriesId));
        Assert.Contains(response.Data!, x => x.Outcome == OkxPublicEventContractMarketOutcome.NotAvailable || x.Outcome == OkxPublicEventContractMarketOutcome.Yes || x.Outcome == OkxPublicEventContractMarketOutcome.No);
    }

    private static OkxRestApiResponse<List<T>> DeserializeRest<T>(params string[] fixturePath) where T : class
        => DeserializeRest<T>(FixtureReader.ReadManual(fixturePath));

    private static OkxRestApiResponse<List<T>> DeserializeRest<T>(string json) where T : class
    {
        var response = JsonConvert.DeserializeObject<OkxRestApiResponse<List<T>>>(json, SerializerOptions.WithConverters);

        Assert.NotNull(response);
        Assert.Equal(0, response.ErrorCode);
        Assert.NotNull(response.Data);
        return response;
    }

    private static List<T> DeserializeSocketData<T>(params string[] fixturePath)
    {
        var json = FixtureReader.ReadManual(fixturePath);
        var token = JObject.Parse(json)["data"];

        Assert.NotNull(token);

        var response = token!.ToObject<List<T>>(JsonSerializer.Create(SerializerOptions.WithConverters));
        Assert.NotNull(response);
        return response!;
    }
}
