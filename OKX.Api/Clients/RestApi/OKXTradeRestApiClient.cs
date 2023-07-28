using System;

namespace OKX.Api.Clients.RestApi;

/// <summary>
/// OKX Trade Rest Api Client
/// </summary>
public class OKXTradeRestApiClient : OKXBaseRestApiClient
{
    // Endpoints
    private const string v5TradeOrder = "api/v5/trade/order";
    private const string v5TradeBatchOrders = "api/v5/trade/batch-orders";
    private const string v5TradeCancelOrder = "api/v5/trade/cancel-order";
    private const string v5TradeCancelBatchOrders = "api/v5/trade/cancel-batch-orders";
    private const string v5TradeAmendOrder = "api/v5/trade/amend-order";
    private const string v5TradeAmendBatchOrders = "api/v5/trade/amend-batch-orders";
    private const string v5TradeClosePosition = "api/v5/trade/close-position";
    private const string v5TradeOrdersPending = "api/v5/trade/orders-pending";
    private const string v5TradeOrdersHistory = "api/v5/trade/orders-history";
    private const string v5TradeOrdersHistoryArchive = "api/v5/trade/orders-history-archive";
    private const string v5TradeFills = "api/v5/trade/fills";
    private const string v5TradeFillsHistory = "api/v5/trade/fills-history";
    private const string v5TradeOrderAlgo = "api/v5/trade/order-algo";
    private const string v5TradeCancelAlgos = "api/v5/trade/cancel-algos";
    private const string v5TradeCancelAdvanceAlgos = "api/v5/trade/cancel-advance-algos";
    private const string v5TradeOrdersAlgoPending = "api/v5/trade/orders-algo-pending";
    private const string v5TradeOrdersAlgoHistory = "api/v5/trade/orders-algo-history";

    internal OKXTradeRestApiClient(OKXRestApiClient root) : base(root)
    {
    }

    #region Trade API Endpoints
    /// <summary>
    /// You can place an order only if you have sufficient funds.
    /// For leading contracts, this endpoint supports placement, but can't close positions.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="tradeMode">Trade Mode</param>
    /// <param name="orderSide">Order Side</param>
    /// <param name="positionSide">Position Side</param>
    /// <param name="orderType">Order Type</param>
    /// <param name="size">Size</param>
    /// <param name="price">Price</param>
    /// <param name="currency">Currency</param>
    /// <param name="clientOrderId">Client Order ID</param>
    /// <param name="reduceOnly">Whether to reduce position only or not, true false, the default is false.</param>
    /// <param name="quantityType">Quantity Type</param>
    /// <param name="quickMgnType">Quick Margin type. Only applicable to Quick Margin Mode of isolated margin. The default value is manual</param>
    /// <param name="banAmend">Whether to disallow the system from amending the size of the SPOT Market Order.</param>
    /// <param name="tpTriggerPxType">Take-profit trigger price type. The Default is last</param>
    /// <param name="tpTriggerPx">Take-profit trigger price. If you fill in this parameter, you should fill in the take-profit order price as well.</param>
    /// <param name="tpOrdPx">Take-profit order price</param>
    /// <param name="slTriggerPxType">Stop-loss trigger price type. The Default is last</param>
    /// <param name="slTriggerPx">Stop-loss trigger price. If you fill in this parameter, you should fill in the stop-loss order price.</param>
    /// <param name="slOrdPx">Stop-loss order price. If you fill in this parameter, you should fill in the stop-loss trigger price. If the price is -1, stop-loss will be executed at the market price.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<OkxOrderPlaceResponse>> PlaceOrderAsync(
        string instrumentId,
        OkxTradeMode tradeMode,
        OkxOrderSide orderSide,
        OkxPositionSide positionSide,
        OkxOrderType orderType,
        decimal size,
        decimal? price = null,
        string currency = null,
        string clientOrderId = null,
        bool? reduceOnly = null,
        OkxQuantityType? quantityType = null,

        OkxQuickMarginType? quickMgnType = null,
        bool? banAmend = null,

        OkxAlgoPriceType? tpTriggerPxType = null,
        decimal? tpTriggerPx = null,
        decimal? tpOrdPx = null,

        OkxAlgoPriceType? slTriggerPxType = null,
        decimal? slTriggerPx = null,
        decimal? slOrdPx = null,

        string attachAlgoClientOrderOrderId = null,

        long? selfTradePreventionId = null,
        OkxSelfTradePreventionMode? selfTradePreventionMode = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            {"tag", ClientOptions.BrokerId },
            {"instId", instrumentId },
            {"tdMode", JsonConvert.SerializeObject(tradeMode, new TradeModeConverter(false)) },
            {"side", JsonConvert.SerializeObject(orderSide, new OrderSideConverter(false)) },
            {"posSide", JsonConvert.SerializeObject(positionSide, new PositionSideConverter(false)) },
            {"ordType", JsonConvert.SerializeObject(orderType, new OrderTypeConverter(false)) },
            {"sz", size.ToOkxString() },
        };
        parameters.AddOptionalParameter("px", price?.ToOkxString());
        parameters.AddOptionalParameter("ccy", currency);
        parameters.AddOptionalParameter("clOrdId", clientOrderId);
        parameters.AddOptionalParameter("reduceOnly", reduceOnly);
        parameters.AddOptionalParameter("tgtCcy", JsonConvert.SerializeObject(quantityType, new QuantityTypeConverter(false)));

        parameters.AddOptionalParameter("quickMgnType", JsonConvert.SerializeObject(quickMgnType, new QuickMarginTypeConverter(false)));
        parameters.AddOptionalParameter("banAmend", banAmend);

        parameters.AddOptionalParameter("tpTriggerPxType", JsonConvert.SerializeObject(tpTriggerPxType, new AlgoPriceTypeConverter(false)));
        parameters.AddOptionalParameter("tpTriggerPx", tpTriggerPx?.ToOkxString());
        parameters.AddOptionalParameter("tpOrdPx", tpOrdPx?.ToOkxString());

        parameters.AddOptionalParameter("slTriggerPxType", JsonConvert.SerializeObject(slTriggerPxType, new AlgoPriceTypeConverter(false)));
        parameters.AddOptionalParameter("slTriggerPx", slTriggerPx?.ToOkxString());
        parameters.AddOptionalParameter("slOrdPx", slOrdPx?.ToOkxString());

        parameters.AddOptionalParameter("attachAlgoClOrdId", attachAlgoClientOrderOrderId);

        parameters.AddOptionalParameter("stpId", selfTradePreventionId?.ToOkxString());
        parameters.AddOptionalParameter("stpMode", JsonConvert.SerializeObject(selfTradePreventionMode, new SelfTradePreventionModeConverter(false)));

        return await SendOKXSingleRequest<OkxOrderPlaceResponse>(GetUri(v5TradeOrder), HttpMethod.Post, ct, signed: true, bodyParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Place orders in batches. Maximum 20 orders can be placed at a time. Request parameters should be passed in the form of an array.
    /// </summary>
    /// <param name="orders">Orders</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<OkxOrderPlaceResponse>>> PlaceMultipleOrdersAsync(IEnumerable<OkxOrderPlaceRequest> orders, CancellationToken ct = default)
    {
        foreach (var order in orders) order.Tag = ClientOptions.BrokerId;
        var parameters = new Dictionary<string, object>
        {
            { ClientOptions.RequestBodyParameterKey, orders },
        };

        return await SendOKXRequest<IEnumerable<OkxOrderPlaceResponse>>(GetUri(v5TradeBatchOrders), HttpMethod.Post, ct, signed: true, bodyParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Cancel an incomplete order.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="orderId">Order ID</param>
    /// <param name="clientOrderId">Client Order ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<OkxOrderCancelResponse>> CancelOrderAsync(string instrumentId, long? orderId = null, string clientOrderId = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            {"instId", instrumentId },
        };
        parameters.AddOptionalParameter("ordId", orderId?.ToOkxString());
        parameters.AddOptionalParameter("clOrdId", clientOrderId);

        return await SendOKXSingleRequest<OkxOrderCancelResponse>(GetUri(v5TradeCancelOrder), HttpMethod.Post, ct, signed: true, bodyParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Cancel incomplete orders in batches. Maximum 20 orders can be canceled at a time. Request parameters should be passed in the form of an array.
    /// </summary>
    /// <param name="orders">Orders</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<OkxOrderCancelResponse>>> CancelMultipleOrdersAsync(IEnumerable<OkxOrderCancelRequest> orders, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { ClientOptions.RequestBodyParameterKey, orders },
        };

        return await SendOKXRequest<IEnumerable<OkxOrderCancelResponse>>(GetUri(v5TradeCancelBatchOrders), HttpMethod.Post, ct, signed: true, bodyParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Amend an incomplete order.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="orderId">Order ID</param>
    /// <param name="clientOrderId">Client Order ID</param>
    /// <param name="requestId">Request ID</param>
    /// <param name="cancelOnFail">Cancel On Fail</param>
    /// <param name="newQuantity">New Quantity</param>
    /// <param name="newPrice">New Price</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<OkxOrderAmendResponse>> AmendOrderAsync(
        string instrumentId,
        long? orderId = null,
        string clientOrderId = null,
        string requestId = null,
        bool? cancelOnFail = null,
        decimal? newQuantity = null,
        decimal? newPrice = null,

        OkxAlgoPriceType? newTpTriggerPxType = null,
        decimal? newTpTriggerPx = null,
        decimal? newTpOrdPx = null,

        OkxAlgoPriceType? newSlTriggerPxType = null,
        decimal? newSlTriggerPx = null,
        decimal? newSlOrdPx = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "instId", instrumentId },
        };
        parameters.AddOptionalParameter("ordId", orderId?.ToOkxString());
        parameters.AddOptionalParameter("clOrdId", clientOrderId);
        parameters.AddOptionalParameter("cxlOnFail", cancelOnFail);
        parameters.AddOptionalParameter("reqId", requestId);
        parameters.AddOptionalParameter("newSz", newQuantity?.ToOkxString());
        parameters.AddOptionalParameter("newPx", newPrice?.ToOkxString());

        parameters.AddOptionalParameter("newTpTriggerPxType", JsonConvert.SerializeObject(newTpTriggerPxType, new AlgoPriceTypeConverter(false)));
        parameters.AddOptionalParameter("newTpTriggerPx", newTpTriggerPx?.ToOkxString());
        parameters.AddOptionalParameter("newTpOrdPx", newTpOrdPx?.ToOkxString());

        parameters.AddOptionalParameter("newSlTriggerPxType", JsonConvert.SerializeObject(newSlTriggerPxType, new AlgoPriceTypeConverter(false)));
        parameters.AddOptionalParameter("newSlTriggerPx", newSlTriggerPx?.ToOkxString());
        parameters.AddOptionalParameter("newSlOrdPx", newSlOrdPx?.ToOkxString());

        return await SendOKXSingleRequest<OkxOrderAmendResponse>(GetUri(v5TradeAmendOrder), HttpMethod.Post, ct, signed: true, bodyParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Amend incomplete orders in batches. Maximum 20 orders can be amended at a time. Request parameters should be passed in the form of an array.
    /// </summary>
    /// <param name="orders">Orders</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<OkxOrderAmendResponse>>> AmendMultipleOrdersAsync(IEnumerable<OkxOrderAmendRequest> orders, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { ClientOptions.RequestBodyParameterKey, orders },
        };

        return await SendOKXRequest<IEnumerable<OkxOrderAmendResponse>>(GetUri(v5TradeAmendBatchOrders), HttpMethod.Post, ct, signed: true, bodyParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Close all positions of an instrument via a market order.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="marginMode">Margin Mode</param>
    /// <param name="positionSide">Position Side</param>
    /// <param name="currency">Currency</param>
    /// <param name="autoCxl">Whether any pending orders for closing out needs to be automatically canceled when close position via a market order.</param>
    /// <param name="clientOrderId">Client-supplied ID. A combination of case-sensitive alphanumerics, all numbers, or all letters of up to 32 characters.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<OkxClosePositionResponse>> ClosePositionAsync(
        string instrumentId,
        OkxMarginMode marginMode,
        OkxPositionSide? positionSide = null,
        string currency = null,
        bool? autoCxl = null,
        string clientOrderId = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            {"tag", ClientOptions.BrokerId },
            {"instId", instrumentId },
            {"mgnMode", JsonConvert.SerializeObject(marginMode, new MarginModeConverter(false)) },
        };
        parameters.AddOptionalParameter("posSide", JsonConvert.SerializeObject(positionSide, new PositionSideConverter(false)));
        parameters.AddOptionalParameter("ccy", currency);
        parameters.AddOptionalParameter("autoCxl", autoCxl);
        parameters.AddOptionalParameter("clOrdId", clientOrderId);

        return await SendOKXSingleRequest<OkxClosePositionResponse>(GetUri(v5TradeClosePosition), HttpMethod.Post, ct, signed: true, bodyParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve order details.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="orderId">Order ID</param>
    /// <param name="clientOrderId">Client Order ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<OkxOrder>> GetOrderDetailsAsync(
        string instrumentId,
        long? orderId = null,
        string clientOrderId = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            {"instId", instrumentId },
        };
        parameters.AddOptionalParameter("ordId", orderId?.ToOkxString());
        parameters.AddOptionalParameter("clOrdId", clientOrderId);

        return await SendOKXSingleRequest<OkxOrder>(GetUri(v5TradeOrder), HttpMethod.Get, ct, signed: true, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve all incomplete orders under the current account.
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="instFamily">Instrument family</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="orderType">Order Type</param>
    /// <param name="state">State</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<OkxOrder>>> GetOrderListAsync(
        OkxInstrumentType? instrumentType = null,
        string instrumentId = null,
        string instFamily = null,
        string underlying = null,
        OkxOrderType? orderType = null,
        OkxOrderState? state = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)));
        parameters.AddOptionalParameter("uly", underlying);
        parameters.AddOptionalParameter("instFamily", instFamily);
        parameters.AddOptionalParameter("instId", instrumentId);
        parameters.AddOptionalParameter("ordType", JsonConvert.SerializeObject(orderType, new OrderTypeConverter(false)));
        parameters.AddOptionalParameter("state", JsonConvert.SerializeObject(state, new OrderStateConverter(false)));
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return await SendOKXRequest<IEnumerable<OkxOrder>>(GetUri(v5TradeOrdersPending), HttpMethod.Get, ct, signed: true, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve the completed order data for the last 7 days, and the incomplete orders that have been cancelled are only reserved for 2 hours.
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="instFamily">Instrument family</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="orderType">Order Type</param>
    /// <param name="state">State</param>
    /// <param name="category">Category</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="begin">Filter with a begin timestamp. Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="end">Filter with a begin timestamp. Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<OkxOrder>>> GetOrderHistoryAsync(
        OkxInstrumentType instrumentType,
        string instrumentId = null,
        string instFamily = null,
        string underlying = null,
        OkxOrderType? orderType = null,
        OkxOrderState? state = null,
        OkxOrderCategory? category = null,
        long? after = null,
        long? before = null,
        long? begin = null,
        long? end = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>
        {
            {"instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false))}
        };
        parameters.AddOptionalParameter("uly", underlying);
        parameters.AddOptionalParameter("instFamily", instFamily);
        parameters.AddOptionalParameter("instId", instrumentId);
        parameters.AddOptionalParameter("ordType", JsonConvert.SerializeObject(orderType, new OrderTypeConverter(false)));
        parameters.AddOptionalParameter("state", JsonConvert.SerializeObject(state, new OrderStateConverter(false)));
        parameters.AddOptionalParameter("category", JsonConvert.SerializeObject(category, new OrderCategoryConverter(false)));
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("begin", begin?.ToOkxString());
        parameters.AddOptionalParameter("end", end?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return await SendOKXRequest<IEnumerable<OkxOrder>>(GetUri(v5TradeOrdersHistory), HttpMethod.Get, ct, signed: true, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve the completed order data of the last 3 months, and the incomplete orders that have been canceled are only reserved for 2 hours.
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="instFamily">Instrument family</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="orderType">Order Type</param>
    /// <param name="state">State</param>
    /// <param name="category">Category</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="begin">Filter with a begin timestamp. Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="end">Filter with a begin timestamp. Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<OkxOrder>>> GetOrderArchiveAsync(
        OkxInstrumentType instrumentType,
        string instrumentId = null,
        string instFamily = null,
        string underlying = null,
        OkxOrderType? orderType = null,
        OkxOrderState? state = null,
        OkxOrderCategory? category = null,
        long? after = null,
        long? before = null,
        long? begin = null,
        long? end = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>
        {
            {"instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false))}
        };
        parameters.AddOptionalParameter("uly", underlying);
        parameters.AddOptionalParameter("instFamily", instFamily);
        parameters.AddOptionalParameter("instId", instrumentId);
        parameters.AddOptionalParameter("ordType", JsonConvert.SerializeObject(orderType, new OrderTypeConverter(false)));
        parameters.AddOptionalParameter("state", JsonConvert.SerializeObject(state, new OrderStateConverter(false)));
        parameters.AddOptionalParameter("category", JsonConvert.SerializeObject(category, new OrderCategoryConverter(false)));
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("begin", begin?.ToOkxString());
        parameters.AddOptionalParameter("end", end?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return await SendOKXRequest<IEnumerable<OkxOrder>>(GetUri(v5TradeOrdersHistoryArchive), HttpMethod.Get, ct, signed: true, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve recently-filled transaction details in the last 3 day.
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="instFamily">Instrument family</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="orderId">Order ID</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="begin">Filter with a begin timestamp. Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="end">Filter with a begin timestamp. Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<OkxTransaction>>> GetTransactionHistoryAsync(
        OkxInstrumentType? instrumentType = null,
        string instrumentId = null,
        string instFamily = null,
        string underlying = null,
        long? orderId = null,
        long? after = null,
        long? before = null,
        long? begin = null,
        long? end = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)));
        parameters.AddOptionalParameter("uly", underlying);
        parameters.AddOptionalParameter("instFamily", instFamily);
        parameters.AddOptionalParameter("instId", instrumentId);
        parameters.AddOptionalParameter("ordId", orderId?.ToOkxString());
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("begin", begin?.ToOkxString());
        parameters.AddOptionalParameter("end", end?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return await SendOKXRequest<IEnumerable<OkxTransaction>>(GetUri(v5TradeFills), HttpMethod.Get, ct, signed: true, queryParameters: parameters).ConfigureAwait(false);
    }


    /// <summary>
    /// Retrieve recently-filled transaction details in the last 3 months.
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="instFamily">Instrument family</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="orderId">Order ID</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="begin">Filter with a begin timestamp. Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="end">Filter with a begin timestamp. Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<OkxTransaction>>> GetTransactionArchiveAsync(
        OkxInstrumentType instrumentType,
        string instrumentId = null,
        string instFamily = null,
        string underlying = null,
        long? orderId = null,
        long? after = null,
        long? before = null,
        long? begin = null,
        long? end = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)));
        parameters.AddOptionalParameter("uly", underlying);
        parameters.AddOptionalParameter("instFamily", instFamily);
        parameters.AddOptionalParameter("instId", instrumentId);
        parameters.AddOptionalParameter("ordId", orderId?.ToOkxString());
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("begin", begin?.ToOkxString());
        parameters.AddOptionalParameter("end", end?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return await SendOKXRequest<IEnumerable<OkxTransaction>>(GetUri(v5TradeFillsHistory), HttpMethod.Get, ct, signed: true, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// The algo order includes trigger order, oco order, conditional order,iceberg order and twap order.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="tradeMode">Trade Mode</param>
    /// <param name="orderSide">Order Side</param>
    /// <param name="algoOrderType">Algo Order Type</param>
    /// <param name="currency">Currency</param>
    /// <param name="positionSide">Position Side</param>
    /// <param name="size">Size</param>
    /// <param name="quantityType">Quantity Type</param>
    /// <param name="clientOrderId">Client-supplied Algo ID. A combination of case-sensitive alphanumerics, all numbers, or all letters of up to 32 characters.</param>
    /// <param name="closeFraction">Fraction of position to be closed when the algo order is triggered.
    /// 
    /// <param name="tpTriggerPrice">Take Profit Trigger Price</param>
    /// <param name="tpTriggerPriceType">Take-profit trigger price type</param>
    /// <param name="tpOrderPrice">Take Profit Order Price</param>
    /// <param name="slTriggerPrice">Stop Loss Trigger Price</param>
    /// <param name="slTriggerPriceType">Stop-loss trigger price. If you fill in this parameter, you should fill in the stop-loss order price.</param>
    /// <param name="slOrderPrice">Stop Loss Order Price</param>
    /// <param name="cxlOnClosePosition">Whether the TP/SL order placed by the user is associated with the corresponding position of the instrument. If it is associated, the TP/SL order will be cancelled when the position is fully closed; if it is not, the TP/SL order will not be affected when the position is fully closed.
    /// Valid values:
    /// true: Place a TP/SL order associated with the position
    /// false: Place a TP/SL order that is not associated with the position
    /// The default value is false. If true is passed in, users must pass reduceOnly = true as well, indicating that when placing a TP/SL order associated with a position, it must be a reduceOnly order.
    /// Only applicable to Single-currency margin and Multi-currency margin.</param>
    /// <param name="reduceOnly">Reduce Only</param>
    /// <param name="quickMarginType">	Quick Margin type. Only applicable to Quick Margin Mode of isolated margin
    /// manual, auto_borrow, auto_repay
    /// The default value is manual</param>
    /// 
    /// <param name="triggerPrice">Trigger Price</param>
    /// <param name="orderPrice">Order Price</param>
    /// <param name="triggerPriceType">Trigger Price Type</param>
    /// 
    /// <param name="callbackRatio">Callback price ratio , e.g. 0.01</param>
    /// <param name="callbackSpread">Callback price variance</param>
    /// <param name="activePrice">Active price</param>
    /// 
    /// 
    /// <param name="priceVariance">Price Variance</param>
    /// <param name="priceRatio">Price Ratio</param>
    /// <param name="sizeLimit">Size Limit</param>
    /// <param name="priceLimit">Price Limit</param>
    /// 
    /// <param name="timeInterval">Time Interval</param>
    /// Currently the system supports fully closing the position only so the only accepted value is 1.
    /// This is only applicable to FUTURES or SWAP instruments.
    /// This is only applicable if posSide is net.
    /// This is only applicable if reduceOnly is true.
    /// This is only applicable if ordType is conditional or oco.
    /// This is only applicable if the stop loss and take profit order is executed as market order
    /// Either sz or closeFraction is required.</param>
    /// 
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<OkxAlgoOrderResponse>> PlaceAlgoOrderAsync(

        // Common
        string instrumentId,
        OkxTradeMode tradeMode,
        OkxOrderSide orderSide,
        OkxAlgoOrderType algoOrderType,
        string currency = null,
        OkxPositionSide? positionSide = null,
        decimal? size = null,
        OkxQuantityType? quantityType = null,
        string clientOrderId = null,
        decimal? closeFraction = null,

        // Take Profit / Stop Loss
        decimal? tpTriggerPrice = null,
        OkxAlgoPriceType? tpTriggerPriceType = null,
        decimal? tpOrderPrice = null,
        decimal? slTriggerPrice = null,
        OkxAlgoPriceType? slTriggerPriceType = null,
        decimal? slOrderPrice = null,
        bool? cxlOnClosePosition = null,
        bool? reduceOnly = null,
        OkxQuickMarginType? quickMarginType = null,

        // Trigger Order
        decimal? triggerPrice = null,
        decimal? orderPrice = null,
        OkxAlgoPriceType? triggerPriceType = null,

        // Trailing Stop Order
        decimal? callbackRatio = null,
        decimal? callbackSpread = null,
        decimal? activePrice = null,

        // Iceberg Order
        // TWAP Order
        OkxPriceVariance? priceVariance = null,
        decimal? priceRatio = null,
        decimal? sizeLimit = null,
        decimal? priceLimit = null,

        // TWAP Order
        long? timeInterval = null,

        // Cancellation Token
        CancellationToken ct = default)
    {
        // Common
        var parameters = new Dictionary<string, object> {
            {"tag", ClientOptions.BrokerId },
            {"instId", instrumentId },
            {"tdMode", JsonConvert.SerializeObject(tradeMode, new TradeModeConverter(false)) },
            {"side", JsonConvert.SerializeObject(orderSide, new OrderSideConverter(false)) },
            {"ordType", JsonConvert.SerializeObject(algoOrderType, new AlgoOrderTypeConverter(false)) },
        };
        parameters.AddOptionalParameter("ccy", currency);
        parameters.AddOptionalParameter("posSide", JsonConvert.SerializeObject(positionSide, new PositionSideConverter(false)));
        parameters.AddOptionalParameter("sz", size?.ToOkxString());
        parameters.AddOptionalParameter("tgtCcy", JsonConvert.SerializeObject(quantityType, new QuantityTypeConverter(false)));
        parameters.AddOptionalParameter("algoClOrdId", clientOrderId);
        parameters.AddOptionalParameter("closeFraction", closeFraction?.ToOkxString());

        // Take Profit / Stop Loss
        parameters.AddOptionalParameter("tpTriggerPx", tpTriggerPrice?.ToOkxString());
        parameters.AddOptionalParameter("tpTriggerPxType", JsonConvert.SerializeObject(tpTriggerPriceType, new AlgoPriceTypeConverter(false)));
        parameters.AddOptionalParameter("tpOrdPx", tpOrderPrice?.ToOkxString());
        parameters.AddOptionalParameter("slTriggerPx", slTriggerPrice?.ToOkxString());
        parameters.AddOptionalParameter("slTriggerPxType", JsonConvert.SerializeObject(slTriggerPriceType, new AlgoPriceTypeConverter(false)));
        parameters.AddOptionalParameter("slOrdPx", slOrderPrice?.ToOkxString());
        parameters.AddOptionalParameter("cxlOnClosePos", cxlOnClosePosition);
        parameters.AddOptionalParameter("reduceOnly", reduceOnly);

        // Take Profit / Stop Loss
        // Trigger Order
        // Trailing Stop Order
        parameters.AddOptionalParameter("quickMgnType", JsonConvert.SerializeObject(quickMarginType, new QuickMarginTypeConverter(false)));

        // Trigger Order
        parameters.AddOptionalParameter("triggerPx", triggerPrice?.ToOkxString());
        parameters.AddOptionalParameter("orderPx", orderPrice?.ToOkxString());
        parameters.AddOptionalParameter("triggerPxType", JsonConvert.SerializeObject(triggerPriceType, new AlgoPriceTypeConverter(false)));

        // Trailing Stop Order
        parameters.AddOptionalParameter("callbackRatio", callbackRatio?.ToOkxString());
        parameters.AddOptionalParameter("callbackSpread", callbackSpread?.ToOkxString());
        parameters.AddOptionalParameter("activePx", activePrice?.ToOkxString());
        
        // Iceberg Order
        // TWAP Order
        parameters.AddOptionalParameter("pxVar", JsonConvert.SerializeObject(priceVariance, new PriceVarianceConverter(false)));
        parameters.AddOptionalParameter("pxSpread", priceRatio?.ToOkxString());
        parameters.AddOptionalParameter("szLimit", sizeLimit?.ToOkxString());
        parameters.AddOptionalParameter("pxLimit", priceLimit?.ToOkxString());

        // TWAP Order
        parameters.AddOptionalParameter("timeInterval", timeInterval?.ToOkxString());

        // Reequest
        return await SendOKXSingleRequest<OkxAlgoOrderResponse>(GetUri(v5TradeOrderAlgo), HttpMethod.Post, ct, signed: true, bodyParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Cancel unfilled algo orders(trigger order, oco order, conditional order). A maximum of 10 orders can be canceled at a time. Request parameters should be passed in the form of an array.
    /// </summary>
    /// <param name="orders">Orders</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<OkxAlgoOrderResponse>> CancelAlgoOrderAsync(IEnumerable<OkxAlgoOrderRequest> orders, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { ClientOptions.RequestBodyParameterKey, orders },
        };

        return await SendOKXSingleRequest<OkxAlgoOrderResponse>(GetUri(v5TradeCancelAlgos), HttpMethod.Post, ct, signed: true, bodyParameters: parameters).ConfigureAwait(false);
    }

    // TODO: Amend algo order

    /// <summary>
    /// Cancel unfilled algo orders(iceberg order and twap order). A maximum of 10 orders can be canceled at a time. Request parameters should be passed in the form of an array.
    /// </summary>
    /// <param name="orders">Orders</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<OkxAlgoOrderResponse>> CancelAdvanceAlgoOrderAsync(IEnumerable<OkxAlgoOrderRequest> orders, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { ClientOptions.RequestBodyParameterKey, orders },
        };

        return await SendOKXSingleRequest<OkxAlgoOrderResponse>(GetUri(v5TradeCancelAdvanceAlgos), HttpMethod.Post, ct, signed: true, bodyParameters: parameters).ConfigureAwait(false);
    }

    // TODO: Get algo order details

    /// <summary>
    /// Retrieve a list of untriggered Algo orders under the current account.
    /// </summary>
    /// <param name="algoOrderType">Algo Order Type</param>
    /// <param name="algoId">Algo ID</param>
    /// <param name="algoClientOrderId">Client-supplied Algo ID. A combination of case-sensitive alphanumerics, all numbers, or all letters of up to 32 characters.</param>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<OkxAlgoOrder>>> GetAlgoOrderListAsync(
        OkxAlgoOrderType algoOrderType,
        long? algoId = null,
        string algoClientOrderId = null,
        OkxInstrumentType? instrumentType = null,
        string instrumentId = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>
        {
            { "ordType", JsonConvert.SerializeObject(algoOrderType, new AlgoOrderTypeConverter(false)) }
        };
        parameters.AddOptionalParameter("algoId", algoId?.ToOkxString());
        parameters.AddOptionalParameter("algoClOrdId", algoClientOrderId);
        parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)));
        parameters.AddOptionalParameter("instId", instrumentId);
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return await SendOKXRequest<IEnumerable<OkxAlgoOrder>>(GetUri(v5TradeOrdersAlgoPending), HttpMethod.Get, ct, signed: true, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve a list of untriggered Algo orders under the current account.
    /// </summary>
    /// <param name="algoOrderType">Algo Order Type</param>
    /// <param name="algoOrderState">Algo Order State</param>
    /// <param name="algoId">Algo ID</param>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<OkxAlgoOrder>>> GetAlgoOrderHistoryAsync(
        OkxAlgoOrderType algoOrderType,
        OkxAlgoOrderState? algoOrderState = null,
        long? algoId = null,
        OkxInstrumentType? instrumentType = null,
        string instrumentId = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>
        {
            {"ordType",   JsonConvert.SerializeObject(algoOrderType, new AlgoOrderTypeConverter(false))}
        };
        parameters.AddOptionalParameter("algoId", algoId?.ToOkxString());
        parameters.AddOptionalParameter("instId", instrumentId);
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());
        parameters.AddOptionalParameter("state", JsonConvert.SerializeObject(algoOrderState, new AlgoOrderStateConverter(false)));
        parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)));

        return await SendOKXRequest<IEnumerable<OkxAlgoOrder>>(GetUri(v5TradeOrdersAlgoHistory), HttpMethod.Get, ct, signed: true, queryParameters: parameters).ConfigureAwait(false);
    }

    // TODO: Get easy convert currency list
    // TODO: Place easy convert
    // TODO: Get easy convert history
    // TODO: Get one-click repay currency list
    // TODO: Trade one-click repay
    // TODO: Get one-click repay history

    #endregion

}
