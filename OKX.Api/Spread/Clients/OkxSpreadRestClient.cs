namespace OKX.Api.Spread;

/// <summary>
/// OKX Rest Api Spread Trading Client
/// </summary>
public class OkxSpreadRestClient(OkxRestApiClient root) : OkxBaseRestClient(root)
{
    // Endpoints
    private const string v5SpreadOrder = "api/v5/sprd/order";
    private const string v5SpreadCancelOrder = "api/v5/sprd/cancel-order";
    private const string v5SpreadMassCancel = "api/v5/sprd/mass-cancel";
    private const string v5SpreadAmendOrder = "api/v5/sprd/amend-order";
    private const string v5SpreadOrdersPending = "api/v5/sprd/orders-pending";
    private const string v5SpreadOrdersHistory = "api/v5/sprd/orders-history";
    private const string v5SpreadOrdersHistoryArchive = "api/v5/sprd/orders-history-archive";
    private const string v5SpreadTrades = "api/v5/sprd/trades";
    private const string v5SpreadSpreads = "api/v5/sprd/spreads";
    private const string v5SpreadBooks = "api/v5/sprd/books";
    private const string v5SpreadPublicTrades = "api/v5/sprd/public-trades";
    private const string v5SpreadCancelAllAfter = "api/v5/sprd/cancel-all-after";
    private const string v5MarketSpreadTicker = "api/v5/market/sprd-ticker";
    private const string v5MarketSpreadCandles = "api/v5/market/sprd-candles";
    private const string v5MarketSpreadHistoryCandles = "api/v5/market/sprd-history-candles";

    /// <summary>
    /// Place a new order
    /// </summary>
    /// <param name="spreadId">spread ID, e.g. BTC-USDT_BTC-USD-SWAP</param>
    /// <param name="side">	Order side, buy sell</param>
    /// <param name="type">Order type</param>
    /// <param name="size">Quantity to buy or sell. The unit is USD for inverse spreads, and the corresponding baseCcy for linear and hybrid spreads.</param>
    /// <param name="price">Order price. Only applicable to limit, post_only, ioc</param>
    /// <param name="clientOrderId">Client Order ID as assigned by the client. A combination of case-sensitive alphanumerics, all numbers, or all letters of up to 32 characters.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxSpreadOrderPlace>> PlaceOrderAsync(
        string spreadId,
        OkxTradeOrderSide side,
        OkxSpreadOrderType type,
        decimal size,
        decimal? price = null,
        string? clientOrderId = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            {"sprdId", spreadId },
            {"sz", size.ToOkxString() },
        };
        parameters.AddEnum("side", side);
        parameters.AddEnum("ordType", type);
        parameters.AddOptional("px", price?.ToOkxString());
        parameters.AddOptional("clOrdId", clientOrderId);
        parameters.AddOptional("tag", OkxConstants.BrokerId);

        return ProcessOneRequestAsync<OkxSpreadOrderPlace>(GetUri(v5SpreadOrder), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Cancel an incomplete order.
    /// </summary>
    /// <param name="orderId">Order ID. Either ordId or clOrdId is required. If both are passed, ordId will be used.</param>
    /// <param name="clientOrderId">Client Order ID as assigned by the client</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxSpreadOrderCancel>> CancelOrderAsync(long? orderId = null, string? clientOrderId = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("ordId", orderId?.ToOkxString());
        parameters.AddOptional("clOrdId", clientOrderId);

        return ProcessOneRequestAsync<OkxSpreadOrderCancel>(GetUri(v5SpreadCancelOrder), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Cancel all pending orders.
    /// </summary>
    /// <param name="spreadId">spread ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<bool?>> CancelOrdersAsync(string spreadId, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            {"sprdId", spreadId },
        };

        var result = await  ProcessOneRequestAsync<OkxBooleanResponse>(GetUri(v5SpreadMassCancel), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
        if (!result) return new RestCallResult<bool?>(result.Request, result.Response, result.Raw, result.Error);
        return new RestCallResult<bool?>(result.Request, result.Response, result.Data.Result, result.Raw, result.Error);
    }

    /// <summary>
    /// Amend an incomplete order.
    /// </summary>
    /// <param name="orderId">Order ID. Either ordId or clOrdId is required. If both are passed, ordId will be used.</param>
    /// <param name="clientOrderId">Client Order ID as assigned by the client</param>
    /// <param name="requestId">Client Request ID as assigned by the client for order amendment.
    /// A combination of case-sensitive alphanumerics, all numbers, or all letters of up to 32 characters.
    /// The response will include the corresponding reqId to help you identify the request if you provide it in the request.</param>
    /// <param name="newQuantity">New quantity after amendment
    /// Either newSz or newPx is required.
    /// When amending a partially-filled order, the newSz should include the amount that has been filled.</param>
    /// <param name="newPrice">	New price after amendment
    /// Either newSz or newPx is required.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxSpreadOrderAmend>> AmendOrderAsync(
        long? orderId = null,
        string? clientOrderId = null,
        string? requestId = null,
        decimal? newQuantity = null,
        decimal? newPrice = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("ordId", orderId?.ToOkxString());
        parameters.AddOptional("clOrdId", clientOrderId);
        parameters.AddOptional("reqId", requestId);
        parameters.AddOptional("newSz", newQuantity?.ToOkxString());
        parameters.AddOptional("newPx", newPrice?.ToOkxString());

        return ProcessOneRequestAsync<OkxSpreadOrderAmend>(GetUri(v5SpreadAmendOrder), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Retrieve order details.
    /// </summary>
    /// <param name="orderId">Order ID. Either ordId or clOrdId is required, if both are passed, ordId will be used</param>
    /// <param name="clientOrderId">Client Order ID as assigned by the client. The latest order will be returned.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxSpreadOrder>> GetOrderAsync(
        long? orderId = null,
        string? clientOrderId = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("ordId", orderId?.ToOkxString());
        parameters.AddOptional("clOrdId", clientOrderId);

        return ProcessOneRequestAsync<OkxSpreadOrder>(GetUri(v5SpreadOrder), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve all incomplete orders under the current account.
    /// </summary>
    /// <param name="spreadId">spread ID, e.g.</param>
    /// <param name="type">Order type</param>
    /// <param name="state">State</param>
    /// <param name="beginId">Start order ID the request to begin with. Pagination of data to return records newer than the requested order Id, not including beginId</param>
    /// <param name="endId">End order ID the request to end with. Pagination of data to return records earlier than the requested order Id, not including endId</param>
    /// <param name="limit">Number of results per request. The maximum is 100. The default is 100</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxSpreadOrder>>> GetOpenOrdersAsync(
        string? spreadId = null,
        OkxTradeOrderType? type = null,
        OkxTradeOrderState? state = null,
        long? beginId = null,
        long? endId = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);

        var parameters = new ParameterCollection();
        parameters.AddOptionalEnum("ordType", type);
        parameters.AddOptionalEnum("state", state);
        parameters.AddOptional("sprdId", spreadId);
        parameters.AddOptional("beginId", beginId?.ToOkxString());
        parameters.AddOptional("endId", endId?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxSpreadOrder>(GetUri(v5SpreadOrdersPending), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve the completed order data for the last 21 days, and the incomplete orders (filledSz =0 &amp; state = canceled) that have been canceled are only reserved for 2 hours. Results are returned in counter chronological order of orders creation.
    /// </summary>
    /// <param name="spreadId">spread ID, e.g.</param>
    /// <param name="type">Order type</param>
    /// <param name="state">State</param>
    /// <param name="beginId">Start order ID the request to begin with. Pagination of data to return records newer than the requested order Id, not including beginId</param>
    /// <param name="endId">End order ID the request to end with. Pagination of data to return records earlier than the requested order Id, not including endId</param>
    /// <param name="begin">Filter with a begin timestamp. Unix timestamp format in milliseconds, e.g. 1597026383085. Date older than 7 days will be truncated.</param>
    /// <param name="end">Filter with an end timestamp. Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100. The default is 100</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxSpreadOrder>>> GetOrderHistoryAsync(
        string? spreadId = null,
        OkxTradeOrderType? type = null,
        OkxTradeOrderState? state = null,
        long? beginId = null,
        long? endId = null,
        long? begin = null,
        long? end = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);

        var parameters = new ParameterCollection();
        parameters.AddOptionalEnum("ordType", type);
        parameters.AddOptionalEnum("state", state);
        parameters.AddOptional("sprdId", spreadId);
        parameters.AddOptional("beginId", beginId?.ToOkxString());
        parameters.AddOptional("endId", endId?.ToOkxString());
        parameters.AddOptional("begin", begin?.ToOkxString());
        parameters.AddOptional("end", end?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxSpreadOrder>(GetUri(v5SpreadOrdersHistory), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve the completed order data for the last 3 months, including those placed 3 months ago but completed in the last 3 months. Results are returned in counter chronological order.
    /// </summary>
    /// <param name="spreadId">spread ID, e.g.</param>
    /// <param name="type">Order type</param>
    /// <param name="state">State</param>
    /// <param name="beginId">Start order ID the request to begin with. Pagination of data to return records newer than the requested order Id, not including beginId</param>
    /// <param name="endId">End order ID the request to end with. Pagination of data to return records earlier than the requested order Id, not including endId</param>
    /// <param name="begin">Filter with a begin timestamp. Unix timestamp format in milliseconds, e.g. 1597026383085. Date older than 7 days will be truncated.</param>
    /// <param name="end">Filter with an end timestamp. Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100. The default is 100</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxSpreadOrder>>> GetOrderArchiveAsync(
        string? spreadId = null,
        OkxTradeOrderType? type = null,
        OkxTradeOrderState? state = null,
        long? beginId = null,
        long? endId = null,
        long? begin = null,
        long? end = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);

        var parameters = new ParameterCollection();
        parameters.AddOptionalEnum("ordType", type);
        parameters.AddOptionalEnum("state", state);
        parameters.AddOptional("sprdId", spreadId);
        parameters.AddOptional("beginId", beginId?.ToOkxString());
        parameters.AddOptional("endId", endId?.ToOkxString());
        parameters.AddOptional("begin", begin?.ToOkxString());
        parameters.AddOptional("end", end?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxSpreadOrder>(GetUri(v5SpreadOrdersHistoryArchive), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve historical transaction details for the last 7 days. Results are returned in counter chronological order.
    /// </summary>
    /// <param name="spreadId">spread ID, e.g.</param>
    /// <param name="tradeId">Trade ID</param>
    /// <param name="orderId">Order ID</param>
    /// <param name="beginId">Start trade ID the request to begin with. Pagination of data to return records newer than the requested tradeId, not including beginId</param>
    /// <param name="endId">End trade ID the request to end with. Pagination of data to return records earlier than the requested tradeId, not including endId</param>
    /// <param name="begin">Filter with a begin timestamp. Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="end">Filter with an end timestamp. Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100. The default is 100</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxSpreadTrade>>> GetTradesAsync(
        string? spreadId = null,
        long? tradeId = null,
        long? orderId = null,
        long? beginId = null,
        long? endId = null,
        long? begin = null,
        long? end = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);

        var parameters = new ParameterCollection();
        parameters.AddOptional("sprdId", spreadId);
        parameters.AddOptional("tradeId", tradeId?.ToOkxString());
        parameters.AddOptional("ordId", orderId?.ToOkxString());
        parameters.AddOptional("beginId", beginId?.ToOkxString());
        parameters.AddOptional("endId", endId?.ToOkxString());
        parameters.AddOptional("begin", begin?.ToOkxString());
        parameters.AddOptional("end", end?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxSpreadTrade>(GetUri(v5SpreadTrades), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve all available spreads based on the request parameters.
    /// </summary>
    /// <param name="baseCurrency">Currency instrument is based in, e.g. BTC, ETH</param>
    /// <param name="instrumentId">The instrument ID to be included in the spread.</param>
    /// <param name="spreadId">The spread ID</param>
    /// <param name="state">Spreads which are available to trade, suspened or expired. Valid values include live, suspend and expired.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxSpreadInstrument>>> GetSpreadsAsync(
        string? baseCurrency = null,
        string? instrumentId = null,
        string? spreadId = null,
        OkxSpreadInstrumentState? state = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("baseCcy", baseCurrency);
        parameters.AddOptional("instId", instrumentId);
        parameters.AddOptional("sprdId", spreadId);
        parameters.AddOptionalEnum("state", state);

        return ProcessListRequestAsync<OkxSpreadInstrument>(GetUri(v5SpreadSpreads), HttpMethod.Get, ct, signed: false, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve the order book of the spread.
    /// </summary>
    /// <param name="spreadId">spread ID, e.g. BTC-USDT_BTC-USDT-SWAP</param>
    /// <param name="depth">Order book depth per side. Maximum value is 400. Default value is 5.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxSpreadOrderBook>> GetOrderBookAsync(string spreadId, int depth = 5, CancellationToken ct = default)
    {
        depth.ValidateIntBetween(nameof(depth), 1, 400);
        var parameters = new ParameterCollection
        {
            { "sprdId", spreadId},
            { "sz", depth},
        };

        return ProcessOneRequestAsync<OkxSpreadOrderBook>(GetUri(v5SpreadBooks), HttpMethod.Get, ct, signed: false, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve the latest price snapshot, best bid/ask price and quantity.
    /// </summary>
    /// <param name="spreadId">spread ID, e.g. BTC-USDT_BTC-USDT-SWAP</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxSpreadTicker>> GetTickerAsync(string spreadId, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "sprdId", spreadId},
        };

        return ProcessOneRequestAsync<OkxSpreadTicker>(GetUri(v5MarketSpreadTicker), HttpMethod.Get, ct, signed: false, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve the recent transactions of an instrument (at most 500 records per request). Results are returned in counter chronological order.
    /// </summary>
    /// <param name="spreadId">spread ID, e.g. BTC-USDT_BTC-USDT-SWAP</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxSpreadPublicTrade>>> GetTradesAsync(string spreadId, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "sprdId", spreadId},
        };

        return ProcessListRequestAsync<OkxSpreadPublicTrade>(GetUri(v5SpreadPublicTrades), HttpMethod.Get, ct, signed: false, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve the candlestick charts. This endpoint can retrieve the latest 1,440 data entries. Charts are returned in groups based on the requested bar.
    /// </summary>
    /// <param name="spreadId">Spread ID</param>
    /// <param name="period">Bar size, the default is 1m, e.g. [1m/3m/5m/15m/30m/1H/2H/4H]
    /// Hong Kong time opening price k-line:[6H/12H/1D/2D/3D/1W/1M/3M]
    /// UTC time opening price k-line:[/6Hutc/12Hutc/1Dutc/2Dutc/3Dutc/1Wutc/1Mutc/3Mutc]</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts. The latest data will be returned when using before individually</param>
    /// <param name="limit">Number of results per request. The maximum is 300. The default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxSpreadCandlestick>>> GetCandlesticksAsync(
        string spreadId,
        OkxPeriod period,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        return GetCandlesticksAsync(spreadId, MapConverter.GetString(period), after, before, limit, ct);
    }

    /// <summary>
    /// Retrieve the candlestick charts. This endpoint can retrieve the latest 1,440 data entries. Charts are returned in groups based on the requested bar.
    /// </summary>
    /// <param name="spreadId">Spread ID</param>
    /// <param name="period">Bar size, the default is 1m, e.g. [1m/3m/5m/15m/30m/1H/2H/4H]
    /// Hong Kong time opening price k-line:[6H/12H/1D/2D/3D/1W/1M/3M]
    /// UTC time opening price k-line:[/6Hutc/12Hutc/1Dutc/2Dutc/3Dutc/1Wutc/1Mutc/3Mutc]</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts. The latest data will be returned when using before individually</param>
    /// <param name="limit">Number of results per request. The maximum is 300. The default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxSpreadCandlestick>>> GetCandlesticksAsync(
        string spreadId,
        string period,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 300);

        var parameters = new ParameterCollection
        {
            { "sprdId", spreadId },
            { "bar", period },
        };
        parameters.AddOptional("after", after?.ToOkxString());
        parameters.AddOptional("before", before?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxSpreadCandlestick>(GetUri(v5MarketSpreadCandles), HttpMethod.Get, ct, signed: false, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve history candlestick charts from recent years.
    /// </summary>
    /// <param name="spreadId">Spread ID</param>
    /// <param name="period">Bar size, the default is 1m, e.g. [1m/3m/5m/15m/30m/1H/2H/4H]
    /// Hong Kong time opening price k-line:[6H/12H/1D/2D/3D/1W/1M/3M]
    /// UTC time opening price k-line:[/6Hutc/12Hutc/1Dutc/2Dutc/3Dutc/1Wutc/1Mutc/3Mutc]</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts. The latest data will be returned when using before individually</param>
    /// <param name="limit">Number of results per request. The maximum is 300. The default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxSpreadCandlestick>>> GetCandlesticksHistoryAsync(
        string spreadId,
        OkxPeriod period,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        return GetCandlesticksHistoryAsync(spreadId, MapConverter.GetString(period), after, before, limit, ct);
    }

    /// <summary>
    /// Retrieve history candlestick charts from recent years.
    /// </summary>
    /// <param name="spreadId">Spread ID</param>
    /// <param name="period">Bar size, the default is 1m, e.g. [1m/3m/5m/15m/30m/1H/2H/4H]
    /// Hong Kong time opening price k-line:[6H/12H/1D/2D/3D/1W/1M/3M]
    /// UTC time opening price k-line:[/6Hutc/12Hutc/1Dutc/2Dutc/3Dutc/1Wutc/1Mutc/3Mutc]</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts. The latest data will be returned when using before individually</param>
    /// <param name="limit">Number of results per request. The maximum is 300. The default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxSpreadCandlestick>>> GetCandlesticksHistoryAsync(string spreadId, string period, long? after = null, long? before = null, int limit = 100, CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new ParameterCollection
        {
            { "sprdId", spreadId },
            { "bar", period },
        };
        parameters.AddOptional("after", after?.ToOkxString());
        parameters.AddOptional("before", before?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxSpreadCandlestick>(GetUri(v5MarketSpreadHistoryCandles), HttpMethod.Get, ct, signed: false, queryParameters: parameters);
    }

    /// <summary>
    /// Cancel all pending orders after the countdown timeout. Only applicable to spread trading.
    /// </summary>
    /// <param name="timeout">	The countdown for order cancellation, with second as the unit.
    /// Range of value can be 0, [10, 120].
    /// Setting timeOut to 0 disables Cancel All After.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxCancelAllAfter>> CancelAllAfterAsync(int timeout, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            {"timeOut", timeout.ToOkxString() },
        };

        return ProcessOneRequestAsync<OkxCancelAllAfter>(GetUri(v5SpreadCancelAllAfter), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

}
