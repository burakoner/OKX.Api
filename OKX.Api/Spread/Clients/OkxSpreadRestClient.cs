using OKX.Api.Spread.Converters;
using OKX.Api.Spread.Enums;
using OKX.Api.Spread.Models;
using OKX.Api.Trade.Converters;
using OKX.Api.Trade.Enums;
using OKX.Api.Trade.Models;

namespace OKX.Api.Spread.Clients;

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

    public Task<RestCallResult<OkxSpreadOrderPlaceResponse>> PlaceOrderAsync(
        string spreadId,
        OkxTradeOrderSide side,
        OkxSpreadOrderType type,
        decimal size,
        decimal? price = null,
        string clientOrderId = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            {"sprdId", spreadId },
            {"side", JsonConvert.SerializeObject(side, new OkxTradeOrderSideConverter(false)) },
            {"ordType", JsonConvert.SerializeObject(type, new OkxSpreadOrderTypeConverter(false)) },
            {"sz", size.ToOkxString() },
        };
        parameters.AddOptionalParameter("px", price?.ToOkxString());
        parameters.AddOptionalParameter("clOrdId", clientOrderId);
        parameters.AddOptionalParameter("tag", Options.BrokerId);

        return ProcessOneRequestAsync<OkxSpreadOrderPlaceResponse>(GetUri(v5SpreadOrder), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    public Task<RestCallResult<OkxSpreadOrderCancelResponse>> CancelOrderAsync(long? orderId = null, string clientOrderId = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("ordId", orderId?.ToOkxString());
        parameters.AddOptionalParameter("clOrdId", clientOrderId);

        return ProcessOneRequestAsync<OkxSpreadOrderCancelResponse>(GetUri(v5SpreadCancelOrder), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    public Task<RestCallResult<List<OkxBooleanResponse>>> CancelOrdersAsync(string spreadId, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            {"sprdId", spreadId },
        };

        return ProcessListRequestAsync<OkxBooleanResponse>(GetUri(v5SpreadMassCancel), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    public Task<RestCallResult<OkxSpreadOrderAmendResponse>> AmendOrderAsync(
        string spreadId,
        long? orderId = null,
        string clientOrderId = null,
        string requestId = null,
        decimal? newQuantity = null,
        decimal? newPrice = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("ordId", orderId?.ToOkxString());
        parameters.AddOptionalParameter("clOrdId", clientOrderId);
        parameters.AddOptionalParameter("reqId", requestId);
        parameters.AddOptionalParameter("newSz", newQuantity?.ToOkxString());
        parameters.AddOptionalParameter("newPx", newPrice?.ToOkxString());

        return ProcessOneRequestAsync<OkxSpreadOrderAmendResponse>(GetUri(v5SpreadAmendOrder), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    public Task<RestCallResult<OkxSpreadOrder>> GetOrderAsync(
        long? orderId = null,
        string clientOrderId = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("ordId", orderId?.ToOkxString());
        parameters.AddOptionalParameter("clOrdId", clientOrderId);

        return ProcessOneRequestAsync<OkxSpreadOrder>(GetUri(v5SpreadOrder), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    public Task<RestCallResult<List<OkxSpreadOrder>>> GetOpenOrdersAsync(
        string spreadId = null,
        OkxTradeOrderType? type = null,
        OkxTradeOrderState? state = null,
        long? beginId = null,
        long? endId = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("sprdId", spreadId);
        parameters.AddOptionalParameter("ordType", JsonConvert.SerializeObject(type, new OkxTradeOrderTypeConverter(false)));
        parameters.AddOptionalParameter("state", JsonConvert.SerializeObject(state, new OkxTradeOrderStateConverter(false)));
        parameters.AddOptionalParameter("beginId", beginId?.ToOkxString());
        parameters.AddOptionalParameter("endId", endId?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxSpreadOrder>(GetUri(v5SpreadOrdersPending), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    public Task<RestCallResult<List<OkxSpreadOrder>>> GetOrderHistoryAsync(
        string spreadId = null,
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
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("sprdId", spreadId);
        parameters.AddOptionalParameter("ordType", JsonConvert.SerializeObject(type, new OkxTradeOrderTypeConverter(false)));
        parameters.AddOptionalParameter("state", JsonConvert.SerializeObject(state, new OkxTradeOrderStateConverter(false)));
        parameters.AddOptionalParameter("beginId", beginId?.ToOkxString());
        parameters.AddOptionalParameter("endId", endId?.ToOkxString());
        parameters.AddOptionalParameter("begin", begin?.ToOkxString());
        parameters.AddOptionalParameter("end", end?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxSpreadOrder>(GetUri(v5SpreadOrdersHistory), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    public Task<RestCallResult<List<OkxSpreadOrder>>> GetOrderArchiveAsync(
        string spreadId = null,
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
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("sprdId", spreadId);
        parameters.AddOptionalParameter("ordType", JsonConvert.SerializeObject(type, new OkxTradeOrderTypeConverter(false)));
        parameters.AddOptionalParameter("state", JsonConvert.SerializeObject(state, new OkxTradeOrderStateConverter(false)));
        parameters.AddOptionalParameter("beginId", beginId?.ToOkxString());
        parameters.AddOptionalParameter("endId", endId?.ToOkxString());
        parameters.AddOptionalParameter("begin", begin?.ToOkxString());
        parameters.AddOptionalParameter("end", end?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxSpreadOrder>(GetUri(v5SpreadOrdersHistoryArchive), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    public Task<RestCallResult<List<OkxSpreadTrade>>> GetTradesAsync(
        string spreadId = null,
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
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("sprdId", spreadId);
        parameters.AddOptionalParameter("tradeId", tradeId?.ToOkxString());
        parameters.AddOptionalParameter("ordId", orderId?.ToOkxString());
        parameters.AddOptionalParameter("beginId", beginId?.ToOkxString());
        parameters.AddOptionalParameter("endId", endId?.ToOkxString());
        parameters.AddOptionalParameter("begin", begin?.ToOkxString());
        parameters.AddOptionalParameter("end", end?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxSpreadTrade>(GetUri(v5SpreadTrades), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    public Task<RestCallResult<List<OkxSpreadInstrument>>> GetSpreadsAsync(
        string baseCurrency = null,
        string instrumentId = null,
        string spreadId = null,
        OkxSpreadInstrumentState? state = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("baseCcy", baseCurrency);
        parameters.AddOptionalParameter("instId", instrumentId);
        parameters.AddOptionalParameter("sprdId", spreadId);
        parameters.AddOptionalParameter("state", JsonConvert.SerializeObject(state, new OkxSpreadInstrumentStateConverter(false)));

        return ProcessListRequestAsync<OkxSpreadInstrument>(GetUri(v5SpreadSpreads), HttpMethod.Get, ct, signed: false, queryParameters: parameters);
    }

    public Task<RestCallResult<OkxSpreadOrderBook>> GetOrderBookAsync(string spreadId, int depth = 5, CancellationToken ct = default)
    {
        depth.ValidateIntBetween(nameof(depth), 1, 400);
        var parameters = new Dictionary<string, object>
        {
            { "sprdId", spreadId},
            { "sz", depth},
        };

        return ProcessOneRequestAsync<OkxSpreadOrderBook>(GetUri(v5SpreadBooks), HttpMethod.Get, ct, signed: false, queryParameters: parameters);
    }

    public Task<RestCallResult<OkxSpreadTicker>> GetTickerAsync(string spreadId, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "sprdId", spreadId},
        };

        return ProcessOneRequestAsync<OkxSpreadTicker>(GetUri(v5MarketSpreadTicker), HttpMethod.Get, ct, signed: false, queryParameters: parameters);
    }

    public Task<RestCallResult<List<OkxSpreadPublicTrade>>> GetTradesAsync(string spreadId, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "sprdId", spreadId},
        };

        return ProcessListRequestAsync<OkxSpreadPublicTrade>(GetUri(v5SpreadPublicTrades), HttpMethod.Get, ct, signed: false, queryParameters: parameters);
    }
    public Task<RestCallResult<List<OkxSpreadCandlestick>>> GetCandlesticksAsync(
        string spreadId,
        OkxPeriod period,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 300);
        var parameters = new Dictionary<string, object>
        {
            { "sprdId", spreadId },
            { "bar", JsonConvert.SerializeObject(period, new OkxPeriodConverter(false)) },
        };
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxSpreadCandlestick>(GetUri(v5MarketSpreadCandles), HttpMethod.Get, ct, signed: false, queryParameters: parameters);
    }

    public Task<RestCallResult<List<OkxSpreadCandlestick>>> GetCandlesticksHistoryAsync(string spreadId, OkxPeriod period, long? after = null, long? before = null, int limit = 100, CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>
        {
            { "sprdId", spreadId },
            { "bar", JsonConvert.SerializeObject(period, new OkxPeriodConverter(false)) },
        };
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxSpreadCandlestick>(GetUri(v5MarketSpreadHistoryCandles), HttpMethod.Get, ct, signed: false, queryParameters: parameters);
    }
    
    public Task<RestCallResult<OkxSpreadCancelAllAfter>> CancelAllAfterAsync(int timeout, string tag, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            {"timeOut", timeout.ToOkxString() },
        };
        parameters.AddOptionalParameter("tag", tag);

        return ProcessOneRequestAsync<OkxSpreadCancelAllAfter>(GetUri(v5SpreadCancelAllAfter), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

}
