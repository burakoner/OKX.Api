using OKX.Api.Models;
using OKX.Api.Models.MarketData;

namespace OKX.Api.Clients.RestApi;

/// <summary>
/// OKX Rest Api Market Data Client
/// </summary>
public class OKXRestApiMarketDataClient : OKXRestApiBaseClient
{
    // Endpoints
    private const string v5MarketTickers = "api/v5/market/tickers";
    private const string v5MarketTicker = "api/v5/market/ticker";
    private const string v5MarketBooks = "api/v5/market/books";
    // TODO: api/v5/market/books-lite
    private const string v5MarketCandles = "api/v5/market/candles";
    private const string v5MarketHistoryCandles = "api/v5/market/history-candles";
    private const string v5MarketTrades = "api/v5/market/trades";
    private const string v5MarketTradesHistory = "api/v5/market/history-trades";
    // TODO: api/v5/market/option/instrument-family-trades
    // TODO: api/v5/public/option-trades
    private const string v5MarketPlatform24Volume = "api/v5/market/platform-24-volume";

    internal OKXRestApiMarketDataClient(OKXRestApiClient root) : base(root)
    {
    }

    #region Market Data Methods
    /// <summary>
    /// Retrieve the latest price snapshot, best bid/ask price, and trading volume in the last 24 hours.
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="instrumentFamily">Instrument Family</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<IEnumerable<OkxTicker>>> GetTickersAsync(
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
    public async Task<RestCallResult<OkxTicker>> GetTickerAsync(string instrumentId, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "instId", instrumentId },
        };

        return await SendOKXSingleRequest<OkxTicker>(GetUri(v5MarketTicker), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve a instrument is order book.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="depth">Order book depth per side. Maximum 400, e.g. 400 bids + 400 asks. Default returns to 1 depth data</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<OkxOrderBook>> GetOrderBookAsync(string instrumentId, int depth = 1, CancellationToken ct = default)
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
    public async Task<RestCallResult<IEnumerable<OkxCandlestick>>> GetCandlesticksAsync(
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
    public async Task<RestCallResult<IEnumerable<OkxCandlestick>>> GetCandlesticksHistoryAsync(string instrumentId, OkxPeriod period, long? after = null, long? before = null, int limit = 100, CancellationToken ct = default)
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
    /// Retrieve the recent transactions of an instrument.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<IEnumerable<OkxTrade>>> GetTradesAsync(string instrumentId, int limit = 100, CancellationToken ct = default)
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
    public async Task<RestCallResult<IEnumerable<OkxTrade>>> GetTradesHistoryAsync(
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

    /// <summary>
    /// The 24-hour trading volume is calculated on a rolling basis, using USD as the pricing unit.
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<Okx24HourVolume>> Get24HourVolumeAsync(CancellationToken ct = default)
    {
        return await SendOKXSingleRequest<Okx24HourVolume>(GetUri(v5MarketPlatform24Volume), HttpMethod.Get, ct).ConfigureAwait(false);
    }


































    #endregion

}