using ApiSharp.Converters;
using ApiSharp.Models;
using Newtonsoft.Json;
using OKX.Api.Base;
using OKX.Api.Public;
using OKX.Api.Tests.TestInfrastructure;

namespace OKX.Api.Tests.Public;

public class OkxPublicMarketDataHistoryContractTests
{
    [Fact]
    public void MarketDataHistoryModules_MapToCurrentEndpointValues()
    {
        Assert.Equal("4", MapConverter.GetString(OkxPublicMarketDataHistoryModule.FourHundredLevelOrderBook));
        Assert.Equal("5", MapConverter.GetString(OkxPublicMarketDataHistoryModule.FiveThousandLevelOrderBook));
        Assert.Equal("6", MapConverter.GetString(OkxPublicMarketDataHistoryModule.FiftyLevelOrderBook));
        Assert.Equal("11", MapConverter.GetString(OkxPublicMarketDataHistoryModule.BorrowingRate));
    }

    [Fact]
    public void ManualHistoricalMarketDataFixture_ParsesBorrowingRateSnapshotAndDateTsAlias()
    {
        var response = DeserializeManual("Public", "get-market-data-history-borrowing-rate.json");

        var snapshot = Assert.Single(response.Data!);
        Assert.Equal(OkxPublicDateAggregationType.Daily, snapshot.DateAggregationType);
        Assert.Equal(3.27m, snapshot.TotalSizeMB);

        var detail = Assert.Single(snapshot.Details);
        Assert.Equal("BTC-USDT", detail.InstrumentId);
        Assert.Equal(1760659200000L, detail.DateRangeStartTimestamp);
        Assert.Equal(1760745600000L, detail.DateRangeEndTimestamp);

        var groupDetail = Assert.Single(detail.groupDetails);
        Assert.Equal("BTC-USDT-borrow-rate-2025-10-17.zip", groupDetail.Filename);
        Assert.Equal(1760745600000L, groupDetail.Timestamp);
        Assert.Equal(1.63m, groupDetail.SizeMB);
    }

    private static OkxRestApiResponse<List<OkxPublicMarketDataHistory>> DeserializeManual(params string[] fixturePath)
    {
        var json = FixtureReader.ReadManual(fixturePath);
        var response = JsonConvert.DeserializeObject<OkxRestApiResponse<List<OkxPublicMarketDataHistory>>>(json, SerializerOptions.WithConverters);

        Assert.NotNull(response);
        Assert.Equal(0, response.ErrorCode);
        Assert.NotNull(response.Data);
        return response;
    }
}
