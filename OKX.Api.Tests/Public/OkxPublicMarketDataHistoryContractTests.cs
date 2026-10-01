using ApiSharp.Converters;
using ApiSharp.Models;
using ApiSharp.Throttling;
using Newtonsoft.Json;
using OKX.Api.Base;
using OKX.Api.Common;
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
        Assert.Equal(1.63m, snapshot.TotalSizeMB);

        var detail = Assert.Single(snapshot.Details);
        Assert.Equal("BTC-USDT", detail.InstrumentId);
        Assert.Equal(1760630400000L, detail.DateRangeStartTimestamp);
        Assert.Equal(1760630400000L, detail.DateRangeEndTimestamp);
        Assert.Equal(new DateTime(2025, 10, 17), detail.GetDateRangeStartDate(OkxPublicMarketDataHistoryModule.BorrowingRate));
        Assert.Equal(new DateTime(2025, 10, 17), detail.GetDateRangeEndDate(OkxPublicMarketDataHistoryModule.BorrowingRate));

        var groupDetail = Assert.Single(detail.groupDetails);
        Assert.Equal("BTC-USDT-borrow-rate-2025-10-17.zip", groupDetail.Filename);
        Assert.Equal(1760630400000L, groupDetail.Timestamp);
        Assert.Equal(1.63m, groupDetail.SizeMB);
        Assert.Equal(new DateTime(2025, 10, 17), groupDetail.GetDate(OkxPublicMarketDataHistoryModule.BorrowingRate));
    }

    [Theory]
    [InlineData("dateTs")]
    [InlineData("dataTs")]
    public void OfficialTradeHistoryExample_UsesFileDatesWithoutChangingUtcInstants(string timestampField)
    {
        // Source: https://www.okx.com/docs-v5/en/#public-data-rest-api-get-historical-market-data
        // The example uses dateTs; the field table documents dataTs. Support both.
        var json = FixtureReader.ReadManual("Public", "get-market-data-history-trades.json")
            .Replace("\"dateTs\"", $"\"{timestampField}\"");
        var response = JsonConvert.DeserializeObject<OkxRestApiResponse<List<OkxPublicMarketDataHistory>>>(json, SerializerOptions.WithConverters);
        Assert.NotNull(response);
        var snapshot = Assert.Single(response.Data!);
        var detail = Assert.Single(snapshot.Details);
        var module = OkxPublicMarketDataHistoryModule.TradeHistory;

        Assert.Equal(DateTimeOffset.FromUnixTimeMilliseconds(1756882260390).UtcDateTime, snapshot.Time);
        Assert.Equal(15.64m, snapshot.TotalSizeMB);
        Assert.Equal(new DateTime(2025, 8, 31), detail.GetDateRangeStartDate(module));
        Assert.Equal(new DateTime(2025, 9, 1), detail.GetDateRangeEndDate(module));
        Assert.Equal(new DateTime(2025, 8, 30, 16, 0, 0, DateTimeKind.Utc), detail.DateRangeStartTime);
        Assert.Equal(new DateTime(2025, 8, 31, 16, 0, 0, DateTimeKind.Utc), detail.DateRangeEndTime);
        Assert.Equal(DateTimeKind.Utc, detail.DateRangeStartTime.Kind);
        Assert.Equal(DateTimeKind.Utc, detail.DateRangeEndTime.Kind);
        Assert.Collection(detail.groupDetails,
            file =>
            {
                Assert.Equal("BTC-USDT-SWAP-trades-2025-09-01.zip", file.Filename);
                Assert.Equal(new DateTime(2025, 9, 1), file.GetDate(module));
                Assert.Equal(new DateTime(2025, 8, 31, 16, 0, 0, DateTimeKind.Utc), file.Time);
                Assert.Equal(DateTimeKind.Utc, file.Time.Kind);
            },
            file =>
            {
                Assert.Equal("BTC-USDT-SWAP-trades-2025-08-31.zip", file.Filename);
                Assert.Equal(new DateTime(2025, 8, 31), file.GetDate(module));
            });
    }

    [Theory]
    [InlineData(OkxPublicMarketDataHistoryModule.TradeHistory, 9, 1)]
    [InlineData(OkxPublicMarketDataHistoryModule.OneMinuteCandlestick, 9, 1)]
    [InlineData(OkxPublicMarketDataHistoryModule.FundingRate, 9, 1)]
    [InlineData(OkxPublicMarketDataHistoryModule.BorrowingRate, 9, 1)]
    [InlineData(OkxPublicMarketDataHistoryModule.FourHundredLevelOrderBook, 8, 31)]
    [InlineData(OkxPublicMarketDataHistoryModule.FiveThousandLevelOrderBook, 8, 31)]
    [InlineData(OkxPublicMarketDataHistoryModule.FiftyLevelOrderBook, 8, 31)]
    public void HistoryDateHelpers_UseExplicitModuleTimezoneAndUnspecifiedDateKind(
        OkxPublicMarketDataHistoryModule module, int month, int day)
    {
        var detail = new OkxPublicMarketDataHistoryItem
        {
            DateRangeStartTimestamp = 1756656000000,
            DateRangeEndTimestamp = 1756656000000,
        };
        var file = new OkxPublicMarketDataHistoryItemGroupDetail { Timestamp = 1756656000000 };
        var expected = new DateTime(2025, month, day);

        Assert.Equal(expected, detail.GetDateRangeStartDate(module));
        Assert.Equal(expected, detail.GetDateRangeEndDate(module));
        Assert.Equal(expected, file.GetDate(module));
        Assert.Equal(DateTimeKind.Unspecified, detail.GetDateRangeStartDate(module).Kind);
        Assert.Equal(DateTimeKind.Unspecified, detail.GetDateRangeEndDate(module).Kind);
        Assert.Equal(DateTimeKind.Unspecified, file.GetDate(module).Kind);
    }

    [Fact]
    public void HistoryDateHelpers_RejectUnknownModuleRatherThanGuessingTimezone()
    {
        var detail = new OkxPublicMarketDataHistoryItem();
        var file = new OkxPublicMarketDataHistoryItemGroupDetail();
        var module = (OkxPublicMarketDataHistoryModule)0;

        Assert.Throws<ArgumentOutOfRangeException>(() => detail.GetDateRangeStartDate(module));
        Assert.Throws<ArgumentOutOfRangeException>(() => detail.GetDateRangeEndDate(module));
        Assert.Throws<ArgumentOutOfRangeException>(() => file.GetDate(module));
    }

    [Theory]
    [InlineData(OkxPublicMarketDataHistoryModule.TradeHistory, true)]
    [InlineData(OkxPublicMarketDataHistoryModule.FourHundredLevelOrderBook, false)]
    public async Task MarketDataHistory_ValidatesCalendarWindowAcrossUtc8Midnight(
        OkxPublicMarketDataHistoryModule module, bool expectedValid)
    {
        using var server = CreateServer();
        var client = CreateClient(server);
        var request = new OkxPublicMarketDataHistoryQueryRequest
        {
            Module = module,
            InstrumentType = OkxInstrumentType.Spot,
            DateAggregationType = OkxPublicDateAggregationType.Daily,
            InstrumentIdList = "BTC-USDT",
            // Eleven UTC dates, but ten UTC+8 dates: Jan 2 through Jan 11.
            Begin = new DateTimeOffset(2026, 1, 1, 16, 0, 0, TimeSpan.Zero).ToUnixTimeMilliseconds(),
            End = new DateTimeOffset(2026, 1, 11, 15, 59, 59, TimeSpan.Zero).ToUnixTimeMilliseconds(),
        };

        if (expectedValid)
        {
            Assert.True((await client.Public.GetMarketDataHistoryAsync(request)).Success);
            Assert.Single(server.Requests);
        }
        else
        {
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => client.Public.GetMarketDataHistoryAsync(request));
            Assert.Empty(server.Requests);
        }
    }

    [Fact]
    public async Task MarketDataHistory_AcceptsTenInclusiveDaysAndMonthsAndSendsRequiredFields()
    {
        using var server = CreateServer();
        var client = CreateClient(server);

        var daily = await client.Public.GetMarketDataHistoryAsync(new OkxPublicMarketDataHistoryQueryRequest
        {
            Module = OkxPublicMarketDataHistoryModule.FourHundredLevelOrderBook,
            InstrumentType = OkxInstrumentType.Spot,
            DateAggregationType = OkxPublicDateAggregationType.Daily,
            InstrumentIdList = "BTC-USDT",
            Begin = UtcTimestamp(2026, 1, 1),
            End = UtcTimestamp(2026, 1, 10),
        });
        var monthly = await client.Public.GetMarketDataHistoryAsync(new OkxPublicMarketDataHistoryQueryRequest
        {
            Module = OkxPublicMarketDataHistoryModule.TradeHistory,
            InstrumentType = OkxInstrumentType.Spot,
            DateAggregationType = OkxPublicDateAggregationType.Monthly,
            InstrumentIdList = "BTC-USDT",
            Begin = Utc8Timestamp(2026, 1, 1),
            End = Utc8Timestamp(2026, 10, 31),
        });

        Assert.True(daily.Success, daily.Error?.ToString());
        Assert.True(monthly.Success, monthly.Error?.ToString());
        Assert.Collection(
            server.Requests,
            request =>
            {
                var query = Uri.UnescapeDataString(request.Query);
                Assert.Contains("module=4", query);
                Assert.Contains("instType=SPOT", query);
                Assert.Contains("instIdList=BTC-USDT", query);
                Assert.Contains("dateAggrType=daily", query);
                Assert.Contains($"begin={UtcTimestamp(2026, 1, 1)}", query);
                Assert.Contains($"end={UtcTimestamp(2026, 1, 10)}", query);
            },
            request => Assert.Contains("dateAggrType=monthly", Uri.UnescapeDataString(request.Query)));
    }

    [Fact]
    public async Task MarketDataHistory_RejectsCurrentContractViolationsBeforeSending()
    {
        using var server = CreateServer();
        var client = CreateClient(server);
        var valid = new OkxPublicMarketDataHistoryQueryRequest
        {
            Module = OkxPublicMarketDataHistoryModule.TradeHistory,
            InstrumentType = OkxInstrumentType.Spot,
            DateAggregationType = OkxPublicDateAggregationType.Daily,
            InstrumentIdList = "BTC-USDT",
            Begin = Utc8Timestamp(2026, 1, 1),
            End = Utc8Timestamp(2026, 1, 1),
        };

        await Assert.ThrowsAsync<ArgumentException>(() => client.Public.GetMarketDataHistoryAsync(valid with { Begin = null }));
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => client.Public.GetMarketDataHistoryAsync(valid with { End = Utc8Timestamp(2026, 1, 11) }));
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => client.Public.GetMarketDataHistoryAsync(valid with
        {
            DateAggregationType = OkxPublicDateAggregationType.Monthly,
            End = Utc8Timestamp(2026, 11, 1),
        }));
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => client.Public.GetMarketDataHistoryAsync(valid with { InstrumentType = OkxInstrumentType.Margin }));
        await Assert.ThrowsAsync<ArgumentException>(() => client.Public.GetMarketDataHistoryAsync(valid with { InstrumentIdList = null }));
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => client.Public.GetMarketDataHistoryAsync(valid with
        {
            InstrumentIdList = string.Join(",", Enumerable.Range(1, 11).Select(x => $"ASSET{x}-USDT")),
        }));
        await Assert.ThrowsAsync<ArgumentException>(() => client.Public.GetMarketDataHistoryAsync(valid with
        {
            Module = OkxPublicMarketDataHistoryModule.FourHundredLevelOrderBook,
            InstrumentIdList = "ANY",
        }));
        await Assert.ThrowsAsync<ArgumentException>(() => client.Public.GetMarketDataHistoryAsync(valid with
        {
            Module = OkxPublicMarketDataHistoryModule.FiftyLevelOrderBook,
            InstrumentType = OkxInstrumentType.Option,
            DateAggregationType = OkxPublicDateAggregationType.Monthly,
            InstrumentIdList = null,
            InstrumentFamilyList = "BTC-USD",
        }));

        Assert.Empty(server.Requests);
    }

    [Fact]
    public async Task MarketDataHistory_EnforcesFivePerTwoSecondIpLimit()
    {
        using var server = CreateServer();
        var client = CreateClient(server, RateLimitingBehavior.Fail);
        var request = new OkxPublicMarketDataHistoryQueryRequest
        {
            Module = OkxPublicMarketDataHistoryModule.BorrowingRate,
            InstrumentType = OkxInstrumentType.Spot,
            DateAggregationType = OkxPublicDateAggregationType.Daily,
            InstrumentIdList = "ANY",
            Begin = Utc8Timestamp(2026, 1, 1),
            End = Utc8Timestamp(2026, 1, 1),
        };

        for (var i = 0; i < 5; i++)
            Assert.True((await client.Public.GetMarketDataHistoryAsync(request)).Success);
        Assert.False((await client.Public.GetMarketDataHistoryAsync(request)).Success);
        Assert.Equal(5, server.Requests.Count);
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

    private static LocalOkxRestServer CreateServer()
        => new(new Dictionary<string, string>
        {
            ["GET /api/v5/public/market-data-history"] = "{\"code\":\"0\",\"msg\":\"\",\"data\":[{}]}",
        });

    private static OkxRestApiClient CreateClient(
        LocalOkxRestServer server,
        RateLimitingBehavior rateLimitingBehavior = RateLimitingBehavior.Wait)
        => new(new OkxRestApiOptions
        {
            AutoTimestamp = false,
            BaseAddress = server.BaseAddress,
            RateLimitingBehavior = rateLimitingBehavior,
        });

    private static long UtcTimestamp(int year, int month, int day)
        => new DateTimeOffset(year, month, day, 0, 0, 0, TimeSpan.Zero).ToUnixTimeMilliseconds();

    private static long Utc8Timestamp(int year, int month, int day)
        => new DateTimeOffset(year, month, day, 0, 0, 0, TimeSpan.FromHours(8)).ToUnixTimeMilliseconds();
}
