using Newtonsoft.Json.Linq;
using OKX.Api.Common;
using OKX.Api.Public;
using OKX.Api.Tests.TestInfrastructure;

namespace OKX.Api.Tests.Public;

public class OkxPublicRequestOverloadClientBehaviorTests
{
    [Fact]
    public async Task GetCandlesticksRequestOverload_MatchesLegacyQuery()
    {
        using var server = CreateServer("/api/v5/market/candles", "{\"code\":\"0\",\"msg\":\"\",\"data\":[[]]}");
        var client = CreateClient(server);

        await client.Public.GetCandlesticksAsync("BTC-USDT", "1H", 1, 2, 3);
        await client.Public.GetCandlesticksAsync(new OkxPublicCandlestickRequest
        {
            InstrumentId = "BTC-USDT",
            Period = "1H",
            After = 1,
            Before = 2,
            Limit = 3
        });

        AssertRequestQueriesEqual(server);
    }

    [Fact]
    public async Task GetInstrumentsRequestOverload_MatchesLegacyQuery()
    {
        using var server = CreateServer("/api/v5/public/instruments", "{\"code\":\"0\",\"msg\":\"\",\"data\":[{}]}");
        var client = CreateClient(server);

        await client.Public.GetInstrumentsAsync(OkxInstrumentType.Events, "EVENT-1", "BTC-USD", "series-1", true);
        await client.Public.GetInstrumentsAsync(new OkxPublicInstrumentQueryRequest
        {
            InstrumentType = OkxInstrumentType.Events,
            InstrumentId = "EVENT-1",
            InstrumentFamily = "BTC-USD",
            SeriesId = "series-1",
            Signed = true
        });

        AssertRequestQueriesEqual(server);
    }

    [Fact]
    public async Task GetEventContractMarketsRequestOverload_MatchesLegacyQuery()
    {
        using var server = CreateServer("/api/v5/public/event-contract/markets", "{\"code\":\"0\",\"msg\":\"\",\"data\":[{}]}");
        var client = CreateClient(server);

        await client.Public.GetEventContractMarketsAsync("series-1", "event-1", "EVENT-1", OkxInstrumentState.Live, 5, 6, 7);
        await client.Public.GetEventContractMarketsAsync(new OkxPublicEventContractMarketsRequest
        {
            SeriesId = "series-1",
            EventId = "event-1",
            InstrumentId = "EVENT-1",
            State = OkxInstrumentState.Live,
            Limit = 5,
            Before = 6,
            After = 7
        });

        AssertRequestQueriesEqual(server);
    }

    [Fact]
    public async Task GetInsuranceFundsRequestOverload_MatchesLegacyQuery()
    {
        using var server = CreateServer("/api/v5/public/insurance-fund", "{\"code\":\"0\",\"msg\":\"\",\"data\":[{}]}");
        var client = CreateClient(server);

        await client.Public.GetInsuranceFundsAsync(OkxInstrumentType.Swap, OkxPublicInsuranceType.All, "BTC-USD", "BTC", 1, 2, 3);
        await client.Public.GetInsuranceFundsAsync(new OkxPublicInsuranceFundQueryRequest
        {
            InstrumentType = OkxInstrumentType.Swap,
            Type = OkxPublicInsuranceType.All,
            InstrumentFamily = "BTC-USD",
            Currency = "BTC",
            After = 1,
            Before = 2,
            Limit = 3
        });

        AssertRequestQueriesEqual(server);
    }

    [Fact]
    public async Task GetUnitConvertRequestOverload_MatchesLegacyQuery()
    {
        using var server = CreateServer("/api/v5/public/convert-contract-coin", "{\"code\":\"0\",\"msg\":\"\",\"data\":[{}]}");
        var client = CreateClient(server);

        await client.Public.GetUnitConvertAsync("BTC-USDT-SWAP", 2m, 3m, OkxPublicConvertType.CurrencyToContract, OkxPublicConvertUnit.Coin, OkxPublicConvertOperation.Close);
        await client.Public.GetUnitConvertAsync(new OkxPublicUnitConvertRequest
        {
            InstrumentId = "BTC-USDT-SWAP",
            Size = 2m,
            Price = 3m,
            Type = OkxPublicConvertType.CurrencyToContract,
            Unit = OkxPublicConvertUnit.Coin,
            OperationType = OkxPublicConvertOperation.Close
        });

        AssertRequestQueriesEqual(server);
    }

    [Fact]
    public async Task GetMarketDataHistoryRequestOverload_MatchesLegacyQuery()
    {
        using var server = CreateServer("/api/v5/public/market-data-history", "{\"code\":\"0\",\"msg\":\"\",\"data\":[{}]}");
        var client = CreateClient(server);

        await client.Public.GetMarketDataHistoryAsync(OkxPublicMarketDataHistoryModule.FourHundredLevelOrderBook, OkxInstrumentType.Spot, OkxPublicDateAggregationType.Daily, "BTC-USDT", null, 1, 2);
        await client.Public.GetMarketDataHistoryAsync(new OkxPublicMarketDataHistoryQueryRequest
        {
            Module = OkxPublicMarketDataHistoryModule.FourHundredLevelOrderBook,
            InstrumentType = OkxInstrumentType.Spot,
            DateAggregationType = OkxPublicDateAggregationType.Daily,
            InstrumentIdList = "BTC-USDT",
            Begin = 1,
            End = 2
        });

        AssertRequestQueriesEqual(server);
    }

    private static void AssertRequestQueriesEqual(LocalOkxRestServer server)
    {
        Assert.Equal(2, server.Requests.Count);
        Assert.Equal(server.Requests[0].Path, server.Requests[1].Path);
        Assert.Equal(server.Requests[0].Query, server.Requests[1].Query);
    }

    private static LocalOkxRestServer CreateServer(string path, string response)
        => new(new Dictionary<string, string>
        {
            [$"GET {path}"] = response,
            [$"POST {path}"] = response,
        });

    private static OkxRestApiClient CreateClient(LocalOkxRestServer server)
    {
        var options = new OkxRestApiOptions(new OkxApiCredentials("key", "secret", "pass"))
        {
            AutoTimestamp = false,
            BaseAddress = server.BaseAddress,
        };

        return new OkxRestApiClient(options);
    }
}
