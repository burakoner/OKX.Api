namespace OKX.Api.Clients.RestApi;

/// <summary>
/// OKX Market Data Rest Api Client
/// </summary>
public class OKXMarketDataRestApiClient : OKXBaseRestApiClient
{
    // Endpoints
    private const string v5MarketTickers = "api/v5/market/tickers";
    private const string v5MarketTicker = "api/v5/market/ticker";
    private const string v5MarketIndexTickers = "api/v5/market/index-tickers";
    private const string v5MarketBooks = "api/v5/market/books";
    private const string v5MarketCandles = "api/v5/market/candles";
    private const string v5MarketHistoryCandles = "api/v5/market/history-candles";
    private const string v5MarketIndexCandles = "api/v5/market/index-candles";
    private const string v5MarketMarkPriceCandles = "api/v5/market/mark-price-candles";
    private const string v5MarketTrades = "api/v5/market/trades";
    private const string v5MarketTradesHistory = "api/v5/market/history-trades";
    private const string v5MarketPlatform24Volume = "api/v5/market/platform-24-volume";
    private const string v5MarketOpenOracle = "api/v5/market/open-oracle";
    private const string v5MarketExchangeRate = "api/v5/market/exchange-rate";
    private const string v5MarketIndexComponents = "api/v5/market/index-components";
    private const string v5MarketBlockTickers = "api/v5/market/block-tickers";
    private const string v5MarketBlockTicker = "api/v5/market/block-ticker";
    private const string v5MarketBlockTrades = "api/v5/market/block-trades";

    internal OKXMarketDataRestApiClient(OKXRestApiClient root) : base(root)
    {
    }

    #region Market Data Methods
    /// <summary>
    /// Retrieve the latest price snapshot, best bid/ask price, and trading volume in the last 24 hours.
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<OkxTicker>>> GetTickersAsync(
        OkxInstrumentType instrumentType, 
        string instrumentFamily = null,
        string underlying = null, 
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)) },
        };
        parameters.AddOptionalParameter("instFamily", instrumentFamily);
        parameters.AddOptionalParameter("uly", underlying);

        return await SendOKXRequest<IEnumerable<OkxTicker>>(GetUri(v5MarketTickers), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve the latest price snapshot, best bid/ask price, and trading volume in the last 24 hours.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<OkxTicker>> GetTickerAsync(string instrumentId, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "instId", instrumentId },
        };

        return await SendOKXSingleRequest<OkxTicker>(GetUri(v5MarketTicker), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve index tickers.
    /// </summary>
    /// <param name="quoteCurrency">Quote currency. Currently there is only an index with USD/USDT/BTC as the quote currency.</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<OkxIndexTicker>>> GetIndexTickersAsync(string quoteCurrency = null, string instrumentId = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("quoteCcy", quoteCurrency);
        parameters.AddOptionalParameter("instId", instrumentId);

        return await SendOKXRequest<IEnumerable<OkxIndexTicker>>(GetUri(v5MarketIndexTickers), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve a instrument is order book.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="depth">Order book depth per side. Maximum 400, e.g. 400 bids + 400 asks. Default returns to 1 depth data</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<OkxOrderBook>> GetOrderBookAsync(string instrumentId, int depth = 1, CancellationToken ct = default)
    {
        depth.ValidateIntBetween(nameof(depth), 1, 400);
        var parameters = new Dictionary<string, object>
        {
            { "instId", instrumentId},
            { "sz", depth},
        };

        var result = await SendOKXRequest<IEnumerable<OkxOrderBook>>(GetUri(v5MarketBooks), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
        if (!result.Success || result.Data.Count() == 0) return result.AsError<OkxOrderBook>(new OkxRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Error != null && result.Error.Code > 0) return result.AsError<OkxOrderBook>(new OkxRestApiError(result.Error.Code, result.Error.Message, null));

        var orderbook = result.Data.FirstOrDefault();
        orderbook.Instrument = instrumentId;
        return result.As(orderbook);
    }

    // TODO: Get order lite book

    /// <summary>
    /// Retrieve the candlestick charts. This endpoint can retrieve the latest 1,440 data entries. Charts are returned in groups based on the requested bar.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="period">Bar size, the default is 1m</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 300; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<OkxCandlestick>>> GetCandlesticksAsync(
        string instrumentId, 
        OkxPeriod period, 
        long? after = null, 
        long? before = null, 
        int limit = 100, 
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 300);
        var parameters = new Dictionary<string, object>
        {
            { "instId", instrumentId },
            { "bar", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
        };
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        var result = await SendOKXRequest<IEnumerable<OkxCandlestick>>(GetUri(v5MarketCandles), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
        if (!result.Success) return result;

        foreach (var candle in result.Data) candle.Instrument = instrumentId;
        return result;
    }

    /// <summary>
    /// Retrieve history candlestick charts from recent years.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="period">Bar size, the default is 1m</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<OkxCandlestick>>> GetCandlesticksHistoryAsync(string instrumentId, OkxPeriod period, long? after = null, long? before = null, int limit = 100, CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>
        {
            { "instId", instrumentId },
            { "bar", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
        };
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        var result = await SendOKXRequest<IEnumerable<OkxCandlestick>>(GetUri(v5MarketHistoryCandles), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
        if (!result.Success) return result;

        foreach (var candle in result.Data) candle.Instrument = instrumentId;
        return result;
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
    public virtual async Task<RestCallResult<IEnumerable<OkxIndexCandlestick>>> GetIndexCandlesticksAsync(string instrumentId, OkxPeriod period, long? after = null, long? before = null, int limit = 100, CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>
        {
            { "instId", instrumentId },
            { "bar", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
        };
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        var result = await SendOKXRequest<IEnumerable<OkxIndexCandlestick>>(GetUri(v5MarketIndexCandles), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
        if (!result.Success) return result;

        foreach (var candle in result.Data) candle.Instrument = instrumentId;
        return result;
    }

    // TODO: Get index candlesticks history

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
    public virtual async Task<RestCallResult<IEnumerable<OkxMarkCandlestick>>> GetMarkPriceCandlesticksAsync(string instrumentId, OkxPeriod period, long? after = null, long? before = null, int limit = 100, CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>
        {
            { "instId", instrumentId },
            { "bar", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
        };
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        var result = await SendOKXRequest<IEnumerable<OkxMarkCandlestick>>(GetUri(v5MarketMarkPriceCandles), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
        if (!result.Success) return result;

        foreach (var candle in result.Data) candle.Instrument = instrumentId;
        return result;
    }

    // TODO: Get mark price candlesticks history

    /// <summary>
    /// Retrieve the recent transactions of an instrument.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<OkxTrade>>> GetTradesAsync(string instrumentId, int limit = 100, CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 500);
        var parameters = new Dictionary<string, object>
        {
            { "instId", instrumentId },
        };
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return await SendOKXRequest<IEnumerable<OkxTrade>>(GetUri(v5MarketTrades), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
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
    public virtual async Task<RestCallResult<IEnumerable<OkxTrade>>> GetTradesHistoryAsync(
        string instrumentId, 
        OkxTradeHistoryPaginationType? type = null,
        long? after = null, 
        long? before = null, 
        int limit = 100, 
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>
        {
            { "instId", instrumentId },
        };

        parameters.AddOptionalParameter("type", JsonConvert.SerializeObject(type, new TradeHistoryPaginationTypeConverter(false)));
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return await SendOKXRequest<IEnumerable<OkxTrade>>(GetUri(v5MarketTradesHistory), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
    }

    // TODO: Get option trades

    /// <summary>
    /// The 24-hour trading volume is calculated on a rolling basis, using USD as the pricing unit.
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<Okx24HourVolume>> Get24HourVolumeAsync(CancellationToken ct = default)
    {
        return await SendOKXSingleRequest<Okx24HourVolume>(GetUri(v5MarketPlatform24Volume), HttpMethod.Get, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Get the crypto price of signing using Open Oracle smart contract.
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<OkxOracle>> GetOracleAsync(CancellationToken ct = default)
    {
        return await SendOKXSingleRequest<OkxOracle>(GetUri(v5MarketOpenOracle), HttpMethod.Get, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// This interface provides the average exchange rate data for 2 weeks
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<OkxExchangeRate>>> GetExchangeRatesAsync(CancellationToken ct = default)
    {
        return await SendOKXRequest<IEnumerable<OkxExchangeRate>>(GetUri(v5MarketExchangeRate), HttpMethod.Get, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Get the index component information data on the market
    /// </summary>
    /// <param name="index"></param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<OkxIndexComponents>> GetIndexComponentsAsync(string index, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "index", index },
        };

        return await SendOKXRequest<OkxIndexComponents>(GetUri(v5MarketIndexComponents), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Get block tickers
    /// Retrieve the latest block trading volume in the last 24 hours.
    /// Rate Limit: 20 requests per 2 seconds
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<OkxBlockTicker>>> GetBlockTickersAsync(
        OkxInstrumentType instrumentType, 
        string instrumentFamily = null, 
        string underlying = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)) },
        };
        parameters.AddOptionalParameter("uly", underlying);
        parameters.AddOptionalParameter("instFamily", instrumentFamily);

        return await SendOKXRequest<IEnumerable<OkxBlockTicker>>(GetUri(v5MarketBlockTickers), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Get block ticker
    /// Retrieve the latest block trading volume in the last 24 hours.
    /// Rate Limit: 20 requests per 2 seconds
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<OkxBlockTicker>> GetBlockTickerAsync(string instrumentId, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "instId", instrumentId },
        };

        return await SendOKXSingleRequest<OkxBlockTicker>(GetUri(v5MarketBlockTicker), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Get block trades
    /// Retrieve the recent block trading transactions of an instrument. Descending order by tradeId.
    /// Rate Limit: 20 requests per 2 seconds
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<OkxTrade>>> GetBlockTradesAsync(string instrumentId, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "instId", instrumentId },
        };

        return await SendOKXRequest<IEnumerable<OkxTrade>>(GetUri(v5MarketBlockTrades), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

}