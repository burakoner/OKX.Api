namespace OKX.Api.Public;

/// <summary>
/// OKX Rest Api Public Data &amp; Market Data Client
/// </summary>
public class OkxPublicRestClient(OkxRestApiClient root) : OkxBaseRestClient(root)
{
    #region Market Data Methods
    /// <summary>
    /// Retrieve the latest price snapshot, best bid/ask price, and trading volume in the last 24 hours.
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="instrumentFamily">Instrument Family</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxPublicTicker>>> GetTickersAsync(
        OkxInstrumentType instrumentType,
        string? instrumentFamily = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddEnum("instType", instrumentType);
        parameters.AddOptional("instFamily", instrumentFamily);

        return ProcessListRequestAsync<OkxPublicTicker>(GetUri("api/v5/market/tickers"), HttpMethod.Get, ct, signed: false, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve the latest price snapshot, best bid/ask price, and trading volume in the last 24 hours.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxPublicTicker>> GetTickerAsync(string instrumentId, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "instId", instrumentId },
        };

        return ProcessOneRequestAsync<OkxPublicTicker>(GetUri("api/v5/market/ticker"), HttpMethod.Get, ct, signed: false, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve a instrument is order book.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="depth">Order book depth per side. Maximum 400, e.g. 400 bids + 400 asks. Default returns to 1 depth data</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<OkxPublicOrderBook>> GetOrderBookAsync(string instrumentId, int? depth = null, CancellationToken ct = default)
    {
        depth?.ValidateIntBetween(nameof(depth), 1, 400);
        var parameters = new ParameterCollection
        {
            { "instId", instrumentId},
        };
        parameters.AddOptional("depth", depth?.ToOkxString());

        var result = await ProcessOneRequestAsync<OkxPublicOrderBook>(GetUri("api/v5/market/books"), HttpMethod.Get, ct, signed: false, queryParameters: parameters);
        if (!result.Success) return result;

        result.Data.InstrumentId = instrumentId;
        return result;
    }

    /// <summary>
    /// Retrieve order book of the instrument.
    /// </summary>
    /// <param name="instrumentId">Instrument ID, e.g. BTC-USDT</param>
    /// <param name="depth">Order book depth per side. Maximum 5000, e.g. 5000 bids + 5000 asks. Default returns to 1 depth data.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<OkxPublicOrderBook>> GetOrderBookFullAsync(string instrumentId, int? depth = null, CancellationToken ct = default)
    {
        depth?.ValidateIntBetween(nameof(depth), 1, 5000);
        var parameters = new ParameterCollection
        {
            { "instId", instrumentId},
        };
        parameters.AddOptional("depth", depth?.ToOkxString());

        var result = await ProcessOneRequestAsync<OkxPublicOrderBook>(GetUri("api/v5/market/books-full"), HttpMethod.Get, ct, signed: false, queryParameters: parameters);
        if (!result.Success) return result;

        result.Data.InstrumentId = instrumentId;
        return result;
    }

    /// <summary>
    /// Retrieve the candlestick charts. This endpoint can retrieve the latest 1,440 data entries. Charts are returned in groups based on the requested bar.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="period">Bar size, the default is 1m
    /// e.g. [1s/1m/3m/5m/15m/30m/1H/2H/4H]
    /// Hong Kong time opening price k-line: [6H/12H/1D/2D/3D/1W/1M/3M]
    /// UTC time opening price k-line: [6Hutc/12Hutc/1Dutc/2Dutc/3Dutc/1Wutc/1Mutc/3Mutc]</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 300; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxPublicCandlestick>>> GetCandlesticksAsync(
        string instrumentId,
        OkxPeriod period,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        return GetCandlesticksAsync(instrumentId, MapConverter.GetString(period)!, after, before, limit, ct);
    }

    /// <summary>
    /// Retrieve the candlestick charts. This endpoint can retrieve the latest 1,440 data entries. Charts are returned in groups based on the requested bar.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="period">Bar size, the default is 1m
    /// e.g. [1s/1m/3m/5m/15m/30m/1H/2H/4H]
    /// Hong Kong time opening price k-line: [6H/12H/1D/2D/3D/1W/1M/3M]
    /// UTC time opening price k-line: [6Hutc/12Hutc/1Dutc/2Dutc/3Dutc/1Wutc/1Mutc/3Mutc]</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 300; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<List<OkxPublicCandlestick>>> GetCandlesticksAsync(
        string instrumentId,
        string period,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 300);
        var parameters = new ParameterCollection
        {
            { "instId", instrumentId },
            { "bar", period },
        };
        parameters.AddOptional("after", after?.ToOkxString());
        parameters.AddOptional("before", before?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

        var result = await ProcessListRequestAsync<OkxPublicCandlestick>(GetUri("api/v5/market/candles"), HttpMethod.Get, ct, signed: false, queryParameters: parameters);
        if (!result.Success) return result;

        foreach (var candle in result.Data) candle.InstrumentId = instrumentId;
        return result;
    }

    /// <summary>
    /// Retrieve history candlestick charts from recent years.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="period">Bar size, the default is 1m
    /// e.g. [1s/1m/3m/5m/15m/30m/1H/2H/4H]
    /// Hong Kong time opening price k-line: [6H/12H/1D/2D/3D/1W/1M/3M]
    /// UTC time opening price k-line: [6Hutc/12Hutc/1Dutc/2Dutc/3Dutc/1Wutc/1Mutc/3Mutc]</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<List<OkxPublicCandlestick>>> GetCandlestickHistoryAsync(string instrumentId, OkxPeriod period, long? after = null, long? before = null, int limit = 100, CancellationToken ct = default)
    {
        return await GetCandlestickHistoryAsync(instrumentId, MapConverter.GetString(period)!, after, before, limit, ct);
    }

    /// <summary>
    /// Retrieve history candlestick charts from recent years.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="period">Bar size, the default is 1m
    /// e.g. [1s/1m/3m/5m/15m/30m/1H/2H/4H]
    /// Hong Kong time opening price k-line: [6H/12H/1D/2D/3D/1W/1M/3M]
    /// UTC time opening price k-line: [6Hutc/12Hutc/1Dutc/2Dutc/3Dutc/1Wutc/1Mutc/3Mutc]</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<List<OkxPublicCandlestick>>> GetCandlestickHistoryAsync(string instrumentId, string period, long? after = null, long? before = null, int limit = 100, CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new ParameterCollection
        {
            { "instId", instrumentId },
            { "bar", period },
        };
        parameters.AddOptional("after", after?.ToOkxString());
        parameters.AddOptional("before", before?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

        var result = await ProcessListRequestAsync<OkxPublicCandlestick>(GetUri("api/v5/market/history-candles"), HttpMethod.Get, ct, signed: false, queryParameters: parameters);
        if (!result.Success) return result;

        foreach (var candle in result.Data) candle.InstrumentId = instrumentId;
        return result;
    }

    /// <summary>
    /// Retrieve the recent transactions of an instrument.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxPublicTrade>>> GetTradesAsync(string instrumentId, int limit = 100, CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 500);
        var parameters = new ParameterCollection
        {
            { "instId", instrumentId },
        };
        parameters.AddOptional("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxPublicTrade>(GetUri("api/v5/market/trades"), HttpMethod.Get, ct, signed: false, queryParameters: parameters);
    }

    /// <summary>
    /// Get trades history
    /// Retrieve the recent transactions of an instrument from the last 3 months with pagination.
    /// Rate Limit: 10 requests per 2 seconds
    /// </summary>
    /// <param name="instrumentId">Instrument ID, e.g. BTC-USDT</param>
    /// <param name="type">Pagination Type</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxPublicTrade>>> GetTradeHistoryAsync(
        string instrumentId,
        OkxPublicTradeHistoryPaginationType? type = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new ParameterCollection
        {
            { "instId", instrumentId },
        };

        parameters.AddOptionalEnum("type", type);
        parameters.AddOptional("after", after?.ToOkxString());
        parameters.AddOptional("before", before?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxPublicTrade>(GetUri("api/v5/market/history-trades"), HttpMethod.Get, ct, signed: false, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve the recent transactions of an instrument under same instFamily. The maximum is 100.
    /// </summary>
    /// <param name="instrumentFamily">Instrument family, e.g. BTC-USD. Applicable to OPTION</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxPublicOptionTradeByInstrumentFamily>>> GetOptionTradesByInstrumentFamilyAsync(
        string instrumentFamily,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "instFamily", instrumentFamily },
        };

        return ProcessListRequestAsync<OkxPublicOptionTradeByInstrumentFamily>(GetUri("api/v5/market/option/instrument-family-trades"), HttpMethod.Get, ct, signed: false, queryParameters: parameters);
    }

    /// <summary>
    /// The maximum is 100.
    /// </summary>
    /// <param name="instrumentId">Instrument ID, e.g. BTC-USD-221230-4000-C, Either instId or instFamily is required. If both are passed, instId will be used.</param>
    /// <param name="instrumentFamily">Instrument family, e.g. BTC-USD</param>
    /// <param name="optionType">Option type, C: Call P: put</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxPublicOptionTrade>>> GetOptionTradesAsync(
    string? instrumentId = null,
    string? instrumentFamily = null,
    OkxOptionType? optionType = null,
    CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("instId", instrumentId);
        parameters.AddOptional("instFamily", instrumentFamily);
        parameters.AddOptionalEnum("optType", optionType);

        return ProcessListRequestAsync<OkxPublicOptionTrade>(GetUri("api/v5/public/option-trades"), HttpMethod.Get, ct, signed: false, queryParameters: parameters);
    }

    /// <summary>
    /// The 24-hour trading volume is calculated on a rolling basis, using USD as the pricing unit.
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxPublicVolume>> Get24HourVolumeAsync(CancellationToken ct = default)
    {
        return ProcessOneRequestAsync<OkxPublicVolume>(GetUri("api/v5/market/platform-24-volume"), HttpMethod.Get, ct);
    }

    /// <summary>
    /// Retrieve call auction details.
    /// </summary>
    /// <param name="instrumentId">Instrument Id</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxPublicCallAuction>> GetCallAuctionDetailsAsync(string instrumentId, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddParameter("instId", instrumentId);

        return ProcessOneRequestAsync<OkxPublicCallAuction>(GetUri("api/v5/market/call-auction-details"), HttpMethod.Get, ct);
    }
    #endregion

    #region Public Data Methods
    /// <summary>
    /// Retrieve a list of instruments with open contracts.
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="instrumentFamily">Instrument family. Only applicable to FUTURES/SWAP/OPTION.If instType is OPTION, either uly or instFamily is required.</param>
    /// <param name="signed">Sign Request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxPublicInstrument>>> GetInstrumentsAsync(
        OkxInstrumentType instrumentType,
        string? instrumentId = null,
        string? instrumentFamily = null,
        bool signed = false,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddEnum("instType", instrumentType);
        parameters.AddOptional("instId", instrumentId);
        parameters.AddOptional("instFamily", instrumentFamily);

        return ProcessListRequestAsync<OkxPublicInstrument>(GetUri("api/v5/public/instruments"), HttpMethod.Get, ct, signed: signed, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve the estimated delivery price which will only have a return value one hour before the delivery/exercise.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxPublicEstimatedPrice>> GetEstimatedPriceAsync(string instrumentId, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "instId", instrumentId },
        };

        return ProcessOneRequestAsync<OkxPublicEstimatedPrice>(GetUri("api/v5/public/estimated-price"), HttpMethod.Get, ct, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve the estimated delivery price, which will only have a return value one hour before the delivery/exercise.
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="instrumentFamily">Instrument Family</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxPublicDeliveryExerciseHistory>>> GetDeliveryExerciseHistoryAsync(
        OkxInstrumentType instrumentType,
        string? instrumentFamily = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        if (instrumentType.IsNotIn(OkxInstrumentType.Futures, OkxInstrumentType.Option))
            throw new ArgumentException("Instrument Type can be only Futures or Option.");

        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new ParameterCollection();
        parameters.AddEnum("instType", instrumentType);
        parameters.AddOptional("instFamily", instrumentFamily);
        parameters.AddOptional("after", after?.ToOkxString());
        parameters.AddOptional("before", before?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxPublicDeliveryExerciseHistory>(GetUri("api/v5/public/delivery-exercise-history"), HttpMethod.Get, ct, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve the estimated settlement price which will only have a return value one hour before the settlement.
    /// </summary>
    /// <param name="instrumentId">Instrument ID, e.g. XRP-USDT-250307. only applicable to FUTURES</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxPublicEstimatedSettlementInfo>> GetEstimatedSettlementInfoAsync(string instrumentId, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddParameter("instId", instrumentId);

        return ProcessOneRequestAsync<OkxPublicEstimatedSettlementInfo>(GetUri("api/v5/public/estimated-settlement-info"), HttpMethod.Get, ct, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve the settlement history of the instrument family.
    /// </summary>
    /// <param name="instrumentFamily">Instrument family</param>
    /// <param name="after">Pagination of data to return records earlier than (not include) the requested ts</param>
    /// <param name="before">Pagination of data to return records newer than (not include) the requested ts</param>
    /// <param name="limit">Number of results per request. The maximum is 100. The default is 100</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxPublicSettlementHistory>>> GetSettlementHistoryAsync(
        string instrumentFamily,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new ParameterCollection();
        parameters.AddParameter("instFamily", instrumentFamily);
        parameters.AddOptional("after", after?.ToOkxString());
        parameters.AddOptional("before", before?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxPublicSettlementHistory>(GetUri("api/v5/public/settlement-history"), HttpMethod.Get, ct, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve funding rate.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxPublicFundingRate>>> GetFundingRatesAsync(string? instrumentId = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        if (string.IsNullOrEmpty(instrumentId)) parameters.Add("instId", "ANY");
        else parameters.AddOptionalParameter("instId", instrumentId);

        return ProcessListRequestAsync<OkxPublicFundingRate>(GetUri("api/v5/public/funding-rate"), HttpMethod.Get, ct, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve funding rate history. This endpoint can retrieve data from the last 3 months.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxPublicFundingRateHistory>>> GetFundingRateHistoryAsync(string instrumentId, long? after = null, long? before = null, int limit = 100, CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new ParameterCollection
        {
            { "instId", instrumentId },
        };
        parameters.AddOptional("after", after?.ToOkxString());
        parameters.AddOptional("before", before?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxPublicFundingRateHistory>(GetUri("api/v5/public/funding-rate-history"), HttpMethod.Get, ct, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve the total open interest for contracts on OKX
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="instrumentFamily">Instrument Family</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxPublicOpenInterest>>> GetOpenInterestsAsync(
        OkxInstrumentType instrumentType,
        string? instrumentId = null,
        string? instrumentFamily = null,
        CancellationToken ct = default)
    {
        if (instrumentType.IsNotIn(OkxInstrumentType.Futures, OkxInstrumentType.Option, OkxInstrumentType.Swap))
            throw new ArgumentException("Instrument Type can be only Futures, Option or Swap.");

        var parameters = new ParameterCollection();
        parameters.AddEnum("instType", instrumentType);
        parameters.AddOptional("instId", instrumentId);
        parameters.AddOptional("instFamily", instrumentFamily);

        return ProcessListRequestAsync<OkxPublicOpenInterest>(GetUri("api/v5/public/open-interest"), HttpMethod.Get, ct, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve the highest buy limit and lowest sell limit of the instrument.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxPublicLimitPrice>> GetLimitPriceAsync(string instrumentId, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "instId", instrumentId },
        };

        return ProcessOneRequestAsync<OkxPublicLimitPrice>(GetUri("api/v5/public/price-limit"), HttpMethod.Get, ct, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve option market data.
    /// </summary>
    /// <param name="instrumentFamily">Instrument family, only applicable to OPTION. Either uly or instFamily is required. If both are passed, instFamily will be used.</param>
    /// <param name="expiryDate">Contract expiry date, the format is "YYMMDD", e.g. "200527"</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxPublicOptionSummary>>> GetOptionMarketDataAsync(
        string instrumentFamily,
        DateTime? expiryDate = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("instFamily", instrumentFamily);
        parameters.AddOptional("expTime", expiryDate?.ToString("yyMMdd"));

        return ProcessListRequestAsync<OkxPublicOptionSummary>(GetUri("api/v5/public/opt-summary"), HttpMethod.Get, ct, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve discount rate level and interest-free quota.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxPublicDiscountInfo>>> GetDiscountInfoAsync(string? currency = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("ccy", currency);

        return ProcessListRequestAsync<OkxPublicDiscountInfo>(GetUri("api/v5/public/discount-rate-interest-free-quota"), HttpMethod.Get, ct, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve API server time.
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default)
    {
        var result = await ProcessOneRequestAsync<OkxPublicTimeContainer>(GetUri("api/v5/public/time"), HttpMethod.Get, ct);
        return result.As(result.Data?.Time ?? default);
    }

    /// <summary>
    /// Retrieve mark price.
    /// We set the mark price based on the SPOT index and at a reasonable basis to prevent individual users from manipulating the market and causing the contract price to fluctuate.
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="instrumentFamily">Instrument Family</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxPublicMarkPrice>>> GetMarkPricesAsync(
        OkxInstrumentType instrumentType,
        string? instrumentId = null,
        string? instrumentFamily = null,
        CancellationToken ct = default)
    {
        if (instrumentType.IsNotIn(OkxInstrumentType.Margin, OkxInstrumentType.Futures, OkxInstrumentType.Option, OkxInstrumentType.Swap))
            throw new ArgumentException("Instrument Type can be only Margin, Futures, Option or Swap.");

        var parameters = new ParameterCollection();
        parameters.AddEnum("instType", instrumentType);
        parameters.AddOptional("instId", instrumentId);
        parameters.AddOptional("instFamily", instrumentFamily);

        return ProcessListRequestAsync<OkxPublicMarkPrice>(GetUri("api/v5/public/mark-price"), HttpMethod.Get, ct, queryParameters: parameters);
    }

    /// <summary>
    /// Position information，Maximum leverage depends on your borrowings and margin ratio.
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="marginMode">Margin Mode</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="instrumentFamily">Instrument Family</param>
    /// <param name="currency">Currency</param>
    /// <param name="tier"></param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxPublicPositionTier>>> GetPositionTiersAsync(
        OkxInstrumentType instrumentType,
        OkxAccountMarginMode marginMode,
        string? instrumentId = null,
        string? instrumentFamily = null,
        string? currency = null,
        string? tier = null,
        CancellationToken ct = default)
    {
        if (instrumentType.IsNotIn(OkxInstrumentType.Margin, OkxInstrumentType.Futures, OkxInstrumentType.Option, OkxInstrumentType.Swap))
            throw new ArgumentException("Instrument Type can be only Margin, Futures, Option or Swap.");

        var parameters = new ParameterCollection();
        parameters.AddEnum("instType", instrumentType);
        parameters.AddEnum("tdMode", marginMode);
        parameters.AddOptional("instFamily", instrumentFamily);
        parameters.AddOptional("instId", instrumentId);
        parameters.AddOptional("ccy", currency);
        parameters.AddOptional("tier", tier);

        return ProcessListRequestAsync<OkxPublicPositionTier>(GetUri("api/v5/public/position-tiers"), HttpMethod.Get, ct, queryParameters: parameters);
    }

    /// <summary>
    /// Get margin interest rate
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxPublicInterestRateLoanQuota>> GetInterestRatesAsync(CancellationToken ct = default)
    {
        return ProcessOneRequestAsync<OkxPublicInterestRateLoanQuota>(GetUri("api/v5/public/interest-rate-loan-quota"), HttpMethod.Get, ct);
    }

    /// <summary>
    /// Get Underlying
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<string>>> GetUnderlyingAsync(OkxInstrumentType instrumentType, CancellationToken ct = default)
    {
        if (instrumentType.IsNotIn(OkxInstrumentType.Futures, OkxInstrumentType.Option, OkxInstrumentType.Swap))
            throw new ArgumentException("Instrument Type can be only Futures, Option or Swap.");

        var parameters = new ParameterCollection();
        parameters.AddEnum("instType", instrumentType);

        return ProcessOneRequestAsync<List<string>>(GetUri("api/v5/public/underlying"), HttpMethod.Get, ct, queryParameters: parameters);
    }

    /// <summary>
    /// Get insurance fund
    /// Get insurance fund balance information
    /// Rate Limit: 10 requests per 2 seconds
    /// </summary>
    /// <param name="instrumentType"></param>
    /// <param name="type"></param>
    /// <param name="instrumentFamily">Instrument Family</param>
    /// <param name="currency"></param>
    /// <param name="after"></param>
    /// <param name="before"></param>
    /// <param name="limit"></param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxPublicInsuranceFund>> GetInsuranceFundAsync(
        OkxInstrumentType instrumentType,
        OkxPublicInsuranceType? type = null,
        string? instrumentFamily = null,
        string? currency = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        if (instrumentType.IsNotIn(OkxInstrumentType.Margin, OkxInstrumentType.Swap, OkxInstrumentType.Futures, OkxInstrumentType.Option))
            throw new ArgumentException("Instrument Type can be only Margin, Swap, Futures or Option.");

        var parameters = new ParameterCollection();
        parameters.AddEnum("instType", instrumentType);
        parameters.AddOptionalEnum("type", type);
        parameters.AddOptional("instFamily", instrumentFamily);
        parameters.AddOptional("ccy", currency);
        parameters.AddOptional("after", after?.ToOkxString());
        parameters.AddOptional("before", before?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

        return ProcessOneRequestAsync<OkxPublicInsuranceFund>(GetUri("api/v5/public/insurance-fund"), HttpMethod.Get, ct, queryParameters: parameters);
    }

    /// <summary>
    /// Convert the crypto value to the number of contracts, or vice versa
    /// </summary>
    /// <param name="instrumentId">Instrument ID, only applicable to FUTURES/SWAP/OPTION</param>
    /// <param name="size">Quantity to buy or sell
    /// It is quantity of currency while converting currency to contract;
    /// It is quantity of contract while contract to currency.Quantity of coin needs to be positive integer</param>
    /// <param name="price">Order price
    /// For crypto-margined contracts, it is necessary while converting;
    /// For USDT-margined contracts, it is necessary while converting between usdt and contract, it is optional while converting between coin and contract.
    /// For OPTION, it is optional.</param>
    /// <param name="type">Convert type
    /// 1: Convert currency to contract.It will be rounded up when the value of contract is decimal
    /// 2: Convert contract to currency
    /// The defautl is 1</param>
    /// <param name="unit">The unit of currency. coin usds: usdt or usdc, only applicable to USDⓈ-margined contracts from FUTURES/SWAP</param>
    /// <param name="operationType">Order type
    /// open: round down sz when opening positions
    /// close: round sz to the nearest when closing positions
    /// The default is close
    /// Applicable to FUTURES SWAP</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxPublicUnitConvert>> GetUnitConvertAsync(
        string instrumentId,
        decimal size,
        decimal? price = null,
        OkxPublicConvertType? type = null,
        OkxPublicConvertUnit? unit = null,
        OkxPublicConvertOperation? operationType = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptionalEnum("type", type);
        parameters.AddOptional("instId", instrumentId);
        parameters.AddOptional("sz", size.ToOkxString());
        parameters.AddOptional("px", price?.ToOkxString());
        parameters.AddOptionalEnum("unit", unit);
        parameters.AddOptionalEnum("opType", operationType);

        return ProcessOneRequestAsync<OkxPublicUnitConvert>(GetUri("api/v5/public/convert-contract-coin"), HttpMethod.Get, ct, queryParameters: parameters);
    }

    /// <summary>
    /// Get option tick bands information
    /// </summary>
    /// <param name="instrumentFamily">Instrument family. Only applicable to OPTION</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxPublicOptionTickBands>>> GetOptionTickBandsAsync(
    string instrumentFamily = "",
    CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "instType", "OPTION" },
        };

        parameters.AddOptional("instFamily", instrumentFamily);
        return ProcessListRequestAsync<OkxPublicOptionTickBands>(GetUri("api/v5/public/instrument-tick-bands"), HttpMethod.Get, ct, queryParameters: parameters);
    }

    /// <summary>
    /// It will return premium data in the past 6 months.
    /// </summary>
    /// <param name="instrumentId">Instrument ID, e.g.</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts(not included)</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts(not included)</param>
    /// <param name="limit">Number of results per request. The maximum is 100. The default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxPublicPremiumHistory>>> GetPremiumHistoryAsync(
        string instrumentId,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new ParameterCollection
        {
            { "instId", instrumentId },
        };
        parameters.AddOptional("after", after?.ToOkxString());
        parameters.AddOptional("before", before?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxPublicPremiumHistory>(GetUri("api/v5/public/premium-history"), HttpMethod.Get, ct, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve index tickers.
    /// </summary>
    /// <param name="quoteCurrency">Quote currency. Currently there is only an index with USD/USDT/BTC as the quote currency.</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxPublicIndexTicker>>> GetIndexTickersAsync(string? quoteCurrency = null, string? instrumentId = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("quoteCcy", quoteCurrency);
        parameters.AddOptional("instId", instrumentId);

        return ProcessListRequestAsync<OkxPublicIndexTicker>(GetUri("api/v5/market/index-tickers"), HttpMethod.Get, ct, signed: false, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve the candlestick charts of the index. This endpoint can retrieve the latest 1,440 data entries. Charts are returned in groups based on the requested bar.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="period">Bar size, the default is 1m</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxPublicIndexCandlestick>>> GetIndexCandlesticksAsync(string instrumentId, OkxPeriod period, long? after = null, long? before = null, int limit = 100, CancellationToken ct = default)
    {
        return GetIndexCandlesticksAsync(instrumentId, MapConverter.GetString(period)!, after, before, limit, ct);
    }

    /// <summary>
    /// Retrieve the candlestick charts of the index. This endpoint can retrieve the latest 1,440 data entries. Charts are returned in groups based on the requested bar.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="period">Bar size, the default is 1m</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<List<OkxPublicIndexCandlestick>>> GetIndexCandlesticksAsync(string instrumentId, string period, long? after = null, long? before = null, int limit = 100, CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new ParameterCollection
        {
            { "instId", instrumentId },
            { "bar", period },
        };
        parameters.AddOptional("after", after?.ToOkxString());
        parameters.AddOptional("before", before?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

        var result = await ProcessListRequestAsync<OkxPublicIndexCandlestick>(GetUri("api/v5/market/index-candles"), HttpMethod.Get, ct, signed: false, queryParameters: parameters);
        if (!result.Success) return result;

        foreach (var candle in result.Data) candle.InstrumentId = instrumentId;
        return result;
    }

    /// <summary>
    /// Retrieve the candlestick charts of the index from recent years.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="period">Bar size, the default is 1m</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxPublicIndexCandlestick>>> GetIndexCandlesticksHistoryAsync(string instrumentId, OkxPeriod period, long? after = null, long? before = null, int limit = 100, CancellationToken ct = default)
    {
        return GetIndexCandlesticksHistoryAsync(instrumentId, MapConverter.GetString(period)!, after, before, limit, ct);
    }

    /// <summary>
    /// Retrieve the candlestick charts of the index from recent years.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="period">Bar size, the default is 1m</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<List<OkxPublicIndexCandlestick>>> GetIndexCandlesticksHistoryAsync(string instrumentId, string period, long? after = null, long? before = null, int limit = 100, CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new ParameterCollection
        {
            { "instId", instrumentId },
            { "bar", period },
        };
        parameters.AddOptional("after", after?.ToOkxString());
        parameters.AddOptional("before", before?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

        var result = await ProcessListRequestAsync<OkxPublicIndexCandlestick>(GetUri("api/v5/market/history-index-candles"), HttpMethod.Get, ct, signed: false, queryParameters: parameters);
        if (!result.Success) return result;

        foreach (var candle in result.Data) candle.InstrumentId = instrumentId;
        return result;
    }

    /// <summary>
    /// Retrieve the candlestick charts of mark price. This endpoint can retrieve the latest 1,440 data entries. Charts are returned in groups based on the requested bar.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="period">Bar size, the default is 1m</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxPublicMarkCandlestick>>> GetMarkPriceCandlesticksAsync(string instrumentId, OkxPeriod period, long? after = null, long? before = null, int limit = 100, CancellationToken ct = default)
    {
        return GetMarkPriceCandlesticksAsync(instrumentId, MapConverter.GetString(period)!, after, before, limit, ct);
    }

    /// <summary>
    /// Retrieve the candlestick charts of mark price. This endpoint can retrieve the latest 1,440 data entries. Charts are returned in groups based on the requested bar.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="period">Bar size, the default is 1m</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<List<OkxPublicMarkCandlestick>>> GetMarkPriceCandlesticksAsync(string instrumentId, string period, long? after = null, long? before = null, int limit = 100, CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new ParameterCollection
        {
            { "instId", instrumentId },
            { "bar", period },
        };
        parameters.AddOptional("after", after?.ToOkxString());
        parameters.AddOptional("before", before?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

        var result = await ProcessListRequestAsync<OkxPublicMarkCandlestick>(GetUri("api/v5/market/mark-price-candles"), HttpMethod.Get, ct, signed: false, queryParameters: parameters);
        if (!result.Success) return result;

        foreach (var candle in result.Data) candle.InstrumentId = instrumentId;
        return result;
    }

    /// <summary>
    /// Retrieve the candlestick charts of mark price from recent years.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="period">Bar size, the default is 1m</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxPublicMarkCandlestick>>> GetMarkPriceCandlesticksHistoryAsync(string instrumentId, OkxPeriod period, long? after = null, long? before = null, int limit = 100, CancellationToken ct = default)
    {
        return GetMarkPriceCandlesticksHistoryAsync(instrumentId, MapConverter.GetString(period)!, after, before, limit, ct);
    }

    /// <summary>
    /// Retrieve the candlestick charts of mark price from recent years.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="period">Bar size, the default is 1m</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<List<OkxPublicMarkCandlestick>>> GetMarkPriceCandlesticksHistoryAsync(string instrumentId, string period, long? after = null, long? before = null, int limit = 100, CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new ParameterCollection
        {
            { "instId", instrumentId },
            { "bar", period },
        };
        parameters.AddOptional("after", after?.ToOkxString());
        parameters.AddOptional("before", before?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

        var result = await ProcessListRequestAsync<OkxPublicMarkCandlestick>(GetUri("api/v5/market/history-mark-price-candles"), HttpMethod.Get, ct, signed: false, queryParameters: parameters);
        if (!result.Success) return result;

        foreach (var candle in result.Data) candle.InstrumentId = instrumentId;
        return result;
    }

    /// <summary>
    /// This interface provides the average exchange rate data for 2 weeks
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<decimal>> GetExchangeRateAsync(CancellationToken ct = default)
    {
        var result = await ProcessOneRequestAsync<OkxPublicExchangeRateContainer>(GetUri("api/v5/market/exchange-rate"), HttpMethod.Get, ct);
        if (!result) return new RestCallResult<decimal>(result.Request, result.Response, result.Raw ?? "", result.Error);
        return new RestCallResult<decimal>(result.Request, result.Response, result.Data.Payload, result.Raw ?? "", result.Error);
    }

    /// <summary>
    /// Get the index component information data on the market
    /// </summary>
    /// <param name="index"></param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxPublicIndexComponents>> GetIndexComponentsAsync(string index, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "index", index },
        };

        return ProcessModelRequestAsync<OkxPublicIndexComponents>(GetUri("api/v5/market/index-components"), HttpMethod.Get, ct, signed: false, queryParameters: parameters);
    }

    /// <summary>
    /// Get the macro-economic calendar data within 3 months. Historical data from 3 months ago is only available to users with trading fee tier VIP1 and above.
    /// </summary>
    /// <param name="region">Country, region or entity</param>
    /// <param name="importance">Level of importance</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts based on the date parameter. Unix timestamp format in milliseconds. The default is the timestamp of the request moment.</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts based on the date parameter. Unix timestamp format in milliseconds.</param>
    /// <param name="limit">Number of results per request. The maximum is 100. The default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxPublicEconomicCalendarEvent>>> GetEconomicCalendarDataAsync(
        string? region = null,
        OkxPublicEventImportance? importance = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new ParameterCollection();
        parameters.AddOptionalEnum("importance", importance);
        parameters.AddOptional("region", region);
        parameters.AddOptional("after", after?.ToOkxString());
        parameters.AddOptional("before", before?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxPublicEconomicCalendarEvent>(GetUri("api/v5/public/economic-calendar"), HttpMethod.Get, ct, signed: false, queryParameters: parameters);
    }
    #endregion

    #region System Status Methods
    /// <summary>
    /// Get event status of system upgrade.
    /// Planned system maintenance that may result in short interruption (lasting less than 5 seconds) or websocket disconnection (users can immediately reconnect) will not be announced. 
    /// The maintenance will only be performed during times of low market volatility.
    /// </summary>
    /// <param name="state">System maintenance status</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxPublicMaintenance>>> GetSystemUpgradeStatusAsync(
        OkxPublicMaintenanceState? state = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptionalEnum("state", state);

        return ProcessListRequestAsync<OkxPublicMaintenance>(GetUri("api/v5/system/status"), HttpMethod.Get, ct, signed: false, queryParameters: parameters);
    }
    #endregion

    #region Announcement Methods
    /// <summary>
    /// Authentication is required for this private endpoint.
    /// Get announcements, the response is sorted by pTime with the most recent first. The sort will not be affected if the announcement is updated. Every page has 20 records
    /// </summary>
    /// <param name="type">Announcement type. Delivering the annType from "GET / Announcement types"
    /// Returning all when it is not posted</param>
    /// <param name="page">Page for pagination. The default is 1</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxPublicAnnouncements>> GetAnnouncementsAsync(string? type = null, int? page = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("annType", type);
        parameters.AddOptional("page", page?.ToOkxString());

        return ProcessOneRequestAsync<OkxPublicAnnouncements>(GetUri("api/v5/support/announcements"), HttpMethod.Get, ct, signed: false, queryParameters: parameters);
    }

    /// <summary>
    /// Authentication is not required for this public endpoint. Get announcements types
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxPublicAnnouncementType>>> GetAnnouncementTypesAsync(CancellationToken ct = default)
    {
        return ProcessListRequestAsync<OkxPublicAnnouncementType>(GetUri("api/v5/support/announcement-types"), HttpMethod.Get, ct, signed: false);
    }
    #endregion

}
