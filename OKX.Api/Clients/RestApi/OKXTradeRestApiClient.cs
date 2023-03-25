namespace OKX.Api.Clients.RestApi;

public class OKXTradeRestApiClient : OKXBaseRestApiClient
{
    // Trade Endpoints
    protected const string Endpoints_V5_Trade_Order = "api/v5/trade/order";
    protected const string Endpoints_V5_Trade_BatchOrders = "api/v5/trade/batch-orders";
    protected const string Endpoints_V5_Trade_CancelOrder = "api/v5/trade/cancel-order";
    protected const string Endpoints_V5_Trade_CancelBatchOrders = "api/v5/trade/cancel-batch-orders";
    protected const string Endpoints_V5_Trade_AmendOrder = "api/v5/trade/amend-order";
    protected const string Endpoints_V5_Trade_AmendBatchOrders = "api/v5/trade/amend-batch-orders";
    protected const string Endpoints_V5_Trade_ClosePosition = "api/v5/trade/close-position";
    protected const string Endpoints_V5_Trade_OrdersPending = "api/v5/trade/orders-pending";
    protected const string Endpoints_V5_Trade_OrdersHistory = "api/v5/trade/orders-history";
    protected const string Endpoints_V5_Trade_OrdersHistoryArchive = "api/v5/trade/orders-history-archive";
    protected const string Endpoints_V5_Trade_Fills = "api/v5/trade/fills";
    protected const string Endpoints_V5_Trade_FillsHistory = "api/v5/trade/fills-history";
    protected const string Endpoints_V5_Trade_OrderAlgo = "api/v5/trade/order-algo";
    protected const string Endpoints_V5_Trade_CancelAlgos = "api/v5/trade/cancel-algos";
    protected const string Endpoints_V5_Trade_CancelAdvanceAlgos = "api/v5/trade/cancel-advance-algos";
    protected const string Endpoints_V5_Trade_OrdersAlgoPending = "api/v5/trade/orders-algo-pending";
    protected const string Endpoints_V5_Trade_OrdersAlgoHistory = "api/v5/trade/orders-algo-history";

    internal OKXTradeRestApiClient(OKXRestApiClient root) : base(root)
    {
    }

    #region Trade API Endpoints
    /// <summary>
    /// You can place an order only if you have sufficient funds.
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
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            {"tag", ClientOptions.BrokerId },
            {"instId", instrumentId },
            {"tdMode", JsonConvert.SerializeObject(tradeMode, new TradeModeConverter(false)) },
            {"side", JsonConvert.SerializeObject(orderSide, new OrderSideConverter(false)) },
            {"posSide", JsonConvert.SerializeObject(positionSide, new PositionSideConverter(false)) },
            {"ordType", JsonConvert.SerializeObject(orderType, new OrderTypeConverter(false)) },
            {"sz", size.ToString(OkxGlobals.OkxCultureInfo) },
        };
        parameters.AddOptionalParameter("px", price?.ToString(OkxGlobals.OkxCultureInfo));
        parameters.AddOptionalParameter("ccy", currency);
        parameters.AddOptionalParameter("clOrdId", clientOrderId);
        parameters.AddOptionalParameter("reduceOnly", reduceOnly);
        parameters.AddOptionalParameter("tgtCcy", JsonConvert.SerializeObject(quantityType, new QuantityTypeConverter(false)));

        return await SendOKXSingleRequest<OkxOrderPlaceResponse>(RootClient.GetUri(Endpoints_V5_Trade_Order), HttpMethod.Post, ct, signed: true, bodyParameters: parameters).ConfigureAwait(false);
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

        return await SendOKXRequest<IEnumerable<OkxOrderPlaceResponse>>(RootClient.GetUri(Endpoints_V5_Trade_BatchOrders), HttpMethod.Post, ct, signed: true, bodyParameters: parameters).ConfigureAwait(false);
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
        parameters.AddOptionalParameter("ordId", orderId?.ToString(OkxGlobals.OkxCultureInfo));
        parameters.AddOptionalParameter("clOrdId", clientOrderId);

        return await SendOKXSingleRequest<OkxOrderCancelResponse>(RootClient.GetUri(Endpoints_V5_Trade_CancelOrder), HttpMethod.Post, ct, signed: true, bodyParameters: parameters).ConfigureAwait(false);
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

        return await SendOKXRequest<IEnumerable<OkxOrderCancelResponse>>(RootClient.GetUri(Endpoints_V5_Trade_CancelBatchOrders), HttpMethod.Post, ct, signed: true, bodyParameters: parameters).ConfigureAwait(false);
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
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            //{ "tag", BROKER_ID },
            { "instId", instrumentId },
        };
        parameters.AddOptionalParameter("ordId", orderId?.ToString(OkxGlobals.OkxCultureInfo));
        parameters.AddOptionalParameter("clOrdId", clientOrderId);
        parameters.AddOptionalParameter("cxlOnFail", cancelOnFail);
        parameters.AddOptionalParameter("reqId", requestId);
        parameters.AddOptionalParameter("newSz", newQuantity?.ToString(OkxGlobals.OkxCultureInfo));
        parameters.AddOptionalParameter("newPx", newPrice?.ToString(OkxGlobals.OkxCultureInfo));

        return await SendOKXSingleRequest<OkxOrderAmendResponse>(RootClient.GetUri(Endpoints_V5_Trade_AmendOrder), HttpMethod.Post, ct, signed: true, bodyParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Amend incomplete orders in batches. Maximum 20 orders can be amended at a time. Request parameters should be passed in the form of an array.
    /// </summary>
    /// <param name="orders">Orders</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<OkxOrderAmendResponse>>> AmendMultipleOrdersAsync(IEnumerable<OkxOrderAmendRequest> orders, CancellationToken ct = default)
    {
        // foreach (var order in orders) order.Tag = BROKER_ID;
        var parameters = new Dictionary<string, object> {
            { ClientOptions.RequestBodyParameterKey, orders },
        };

        return await SendOKXRequest<IEnumerable<OkxOrderAmendResponse>>(RootClient.GetUri(Endpoints_V5_Trade_AmendBatchOrders), HttpMethod.Post, ct, signed: true, bodyParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Close all positions of an instrument via a market order.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="marginMode">Margin Mode</param>
    /// <param name="positionSide">Position Side</param>
    /// <param name="currency">Currency</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<OkxClosePositionResponse>> ClosePositionAsync(
        string instrumentId,
        OkxMarginMode marginMode,
        OkxPositionSide? positionSide = null,
        string currency = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            {"instId", instrumentId },
            {"mgnMode", JsonConvert.SerializeObject(marginMode, new MarginModeConverter(false)) },
        };
        parameters.AddOptionalParameter("posSide", JsonConvert.SerializeObject(positionSide, new PositionSideConverter(false)));
        parameters.AddOptionalParameter("ccy", currency);

        return await SendOKXSingleRequest<OkxClosePositionResponse>(RootClient.GetUri(Endpoints_V5_Trade_ClosePosition), HttpMethod.Post, ct, signed: true, bodyParameters: parameters).ConfigureAwait(false);
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
        parameters.AddOptionalParameter("ordId", orderId?.ToString());
        parameters.AddOptionalParameter("clOrdId", clientOrderId);

        return await SendOKXSingleRequest<OkxOrder>(RootClient.GetUri(Endpoints_V5_Trade_Order), HttpMethod.Get, ct, signed: true, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve all incomplete orders under the current account.
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="instrumentId">Instrument ID</param>
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
        parameters.AddOptionalParameter("instId", instrumentId);
        parameters.AddOptionalParameter("uly", underlying);
        parameters.AddOptionalParameter("after", after?.ToString());
        parameters.AddOptionalParameter("before", before?.ToString());
        parameters.AddOptionalParameter("limit", limit.ToString());
        parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)));
        parameters.AddOptionalParameter("ordType", JsonConvert.SerializeObject(orderType, new OrderTypeConverter(false)));
        parameters.AddOptionalParameter("state", JsonConvert.SerializeObject(state, new OrderStateConverter(false)));

        return await SendOKXRequest<IEnumerable<OkxOrder>>(RootClient.GetUri(Endpoints_V5_Trade_OrdersPending), HttpMethod.Get, ct, signed: true, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve the completed order data for the last 7 days, and the incomplete orders that have been cancelled are only reserved for 2 hours.
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="orderType">Order Type</param>
    /// <param name="state">State</param>
    /// <param name="category">Category</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<OkxOrder>>> GetOrderHistoryAsync(
        OkxInstrumentType instrumentType,
        string instrumentId = null,
        string underlying = null,
        OkxOrderType? orderType = null,
        OkxOrderState? state = null,
        OkxOrderCategory? category = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>
        {
            {"instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false))}
        };
        parameters.AddOptionalParameter("instId", instrumentId);
        parameters.AddOptionalParameter("uly", underlying);
        parameters.AddOptionalParameter("after", after?.ToString());
        parameters.AddOptionalParameter("before", before?.ToString());
        parameters.AddOptionalParameter("limit", limit.ToString());
        parameters.AddOptionalParameter("ordType", JsonConvert.SerializeObject(orderType, new OrderTypeConverter(false)));
        parameters.AddOptionalParameter("state", JsonConvert.SerializeObject(state, new OrderStateConverter(false)));
        parameters.AddOptionalParameter("category", JsonConvert.SerializeObject(category, new OrderCategoryConverter(false)));

        return await SendOKXRequest<IEnumerable<OkxOrder>>(RootClient.GetUri(Endpoints_V5_Trade_OrdersHistory), HttpMethod.Get, ct, signed: true, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve the completed order data of the last 3 months, and the incomplete orders that have been canceled are only reserved for 2 hours.
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="orderType">Order Type</param>
    /// <param name="state">State</param>
    /// <param name="category">Category</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<OkxOrder>>> GetOrderArchiveAsync(
        OkxInstrumentType instrumentType,
        string instrumentId = null,
        string underlying = null,
        OkxOrderType? orderType = null,
        OkxOrderState? state = null,
        OkxOrderCategory? category = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>
        {
            {"instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false))}
        };
        parameters.AddOptionalParameter("instId", instrumentId);
        parameters.AddOptionalParameter("uly", underlying);
        parameters.AddOptionalParameter("after", after?.ToString());
        parameters.AddOptionalParameter("before", before?.ToString());
        parameters.AddOptionalParameter("limit", limit.ToString());
        parameters.AddOptionalParameter("ordType", JsonConvert.SerializeObject(orderType, new OrderTypeConverter(false)));
        parameters.AddOptionalParameter("state", JsonConvert.SerializeObject(state, new OrderStateConverter(false)));
        parameters.AddOptionalParameter("category", JsonConvert.SerializeObject(category, new OrderCategoryConverter(false)));

        return await SendOKXRequest<IEnumerable<OkxOrder>>(RootClient.GetUri(Endpoints_V5_Trade_OrdersHistoryArchive), HttpMethod.Get, ct, signed: true, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve recently-filled transaction details in the last 3 day.
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="orderId">Order ID</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<OkxTransaction>>> GetTransactionHistoryAsync(
        OkxInstrumentType? instrumentType = null,
        string instrumentId = null,
        string underlying = null,
        long? orderId = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)));
        parameters.AddOptionalParameter("instId", instrumentId);
        parameters.AddOptionalParameter("uly", underlying);
        parameters.AddOptionalParameter("ordId", orderId?.ToString());
        parameters.AddOptionalParameter("after", after?.ToString());
        parameters.AddOptionalParameter("before", before?.ToString());
        parameters.AddOptionalParameter("limit", limit.ToString());

        return await SendOKXRequest<IEnumerable<OkxTransaction>>(RootClient.GetUri(Endpoints_V5_Trade_Fills), HttpMethod.Get, ct, signed: true, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve recently-filled transaction details in the last 3 months.
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="orderId">Order ID</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<OkxTransaction>>> GetTransactionArchiveAsync(
        OkxInstrumentType instrumentType,
        string instrumentId = null,
        string underlying = null,
        long? orderId = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)));
        parameters.AddOptionalParameter("instId", instrumentId);
        parameters.AddOptionalParameter("uly", underlying);
        parameters.AddOptionalParameter("ordId", orderId?.ToString());
        parameters.AddOptionalParameter("after", after?.ToString());
        parameters.AddOptionalParameter("before", before?.ToString());
        parameters.AddOptionalParameter("limit", limit.ToString());

        return await SendOKXRequest<IEnumerable<OkxTransaction>>(RootClient.GetUri(Endpoints_V5_Trade_FillsHistory), HttpMethod.Get, ct, signed: true, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// The algo order includes trigger order, oco order, conditional order,iceberg order and twap order.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="tradeMode">Trade Mode</param>
    /// <param name="orderSide">Order Side</param>
    /// <param name="algoOrderType">Algo Order Type</param>
    /// <param name="size">Size</param>
    /// <param name="currency">Currency</param>
    /// <param name="reduceOnly">Reduce Only</param>
    /// <param name="positionSide">Position Side</param>
    /// <param name="quantityType">Quantity Type</param>
    /// <param name="tpTriggerPxType">Take-profit trigger price type</param>
    /// <param name="tpTriggerPrice">Take Profit Trigger Price</param>
    /// <param name="tpOrderPrice">Take Profit Order Price</param>
    /// <param name="slTriggerPxType">Stop-loss trigger price. If you fill in this parameter, you should fill in the stop-loss order price.</param>
    /// <param name="slTriggerPrice">Stop Loss Trigger Price</param>
    /// <param name="slOrderPrice">Stop Loss Order Price</param>
    /// <param name="triggerPrice">Trigger Price</param>
    /// <param name="orderPrice">Order Price</param>
    /// <param name="pxVar">Price Variance</param>
    /// <param name="priceRatio">Price Ratio</param>
    /// <param name="sizeLimit">Size Limit</param>
    /// <param name="priceLimit">Price Limit</param>
    /// <param name="timeInterval">Time Interval</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<OkxAlgoOrderResponse>> PlaceAlgoOrderAsync(
        //* Common */
        string instrumentId,
        OkxTradeMode tradeMode,
        OkxOrderSide orderSide,
        OkxAlgoOrderType algoOrderType,
        decimal size,
        string currency = null,
        bool? reduceOnly = null,
        OkxPositionSide? positionSide = null,
        OkxQuantityType? quantityType = null,

        /* Stop Order */
        OkxAlgoPriceType? tpTriggerPxType = null,
        decimal? tpTriggerPrice = null,
        decimal? tpOrderPrice = null,
        OkxAlgoPriceType? slTriggerPxType = null,
        decimal? slTriggerPrice = null,
        decimal? slOrderPrice = null,

        /* Trigger Order */
        decimal? triggerPrice = null,
        decimal? orderPrice = null,

        /* Iceberg Order */
        /* TWAP Order */
        OkxPriceVariance? pxVar = null,
        decimal? priceRatio = null,
        decimal? sizeLimit = null,
        decimal? priceLimit = null,

        /* TWAP Order */
        long? timeInterval = null,

        /* Cancellation Token */
        CancellationToken ct = default)
    {
        /* Common */
        var parameters = new Dictionary<string, object> {
            {"tag", ClientOptions.BrokerId },
            {"instId", instrumentId },
            {"tdMode", JsonConvert.SerializeObject(tradeMode, new TradeModeConverter(false)) },
            {"side", JsonConvert.SerializeObject(orderSide, new OrderSideConverter(false)) },
            {"ordType", JsonConvert.SerializeObject(algoOrderType, new AlgoOrderTypeConverter(false)) },
            {"sz", size.ToString(OkxGlobals.OkxCultureInfo) },
        };
        parameters.AddOptionalParameter("ccy", currency);
        parameters.AddOptionalParameter("reduceOnly", reduceOnly);
        parameters.AddOptionalParameter("posSide", JsonConvert.SerializeObject(positionSide, new PositionSideConverter(false)));
        parameters.AddOptionalParameter("tgtCcy", JsonConvert.SerializeObject(quantityType, new QuantityTypeConverter(false)));

        /* Stop Order */
        parameters.AddOptionalParameter("tpTriggerPxType", JsonConvert.SerializeObject(tpTriggerPxType, new AlgoPriceTypeConverter(false)));
        parameters.AddOptionalParameter("tpTriggerPx", tpTriggerPrice?.ToString(OkxGlobals.OkxCultureInfo));
        parameters.AddOptionalParameter("tpOrdPx", tpOrderPrice?.ToString(OkxGlobals.OkxCultureInfo));
        parameters.AddOptionalParameter("slTriggerPxType", JsonConvert.SerializeObject(slTriggerPxType, new AlgoPriceTypeConverter(false)));
        parameters.AddOptionalParameter("slTriggerPx", slTriggerPrice?.ToString(OkxGlobals.OkxCultureInfo));
        parameters.AddOptionalParameter("slOrdPx", slOrderPrice?.ToString(OkxGlobals.OkxCultureInfo));

        /* Trigger Order */
        parameters.AddOptionalParameter("triggerPx", triggerPrice?.ToString(OkxGlobals.OkxCultureInfo));
        parameters.AddOptionalParameter("orderPx", orderPrice?.ToString(OkxGlobals.OkxCultureInfo));

        /* Iceberg Order */
        /* TWAP Order */
        parameters.AddOptionalParameter("pxVar", JsonConvert.SerializeObject(pxVar, new PriceVarianceConverter(false)));
        parameters.AddOptionalParameter("pxSpread", priceRatio?.ToString(OkxGlobals.OkxCultureInfo));
        parameters.AddOptionalParameter("szLimit", sizeLimit?.ToString(OkxGlobals.OkxCultureInfo));
        parameters.AddOptionalParameter("pxLimit", priceLimit?.ToString(OkxGlobals.OkxCultureInfo));

        /* TWAP Order */
        parameters.AddOptionalParameter("timeInterval", timeInterval?.ToString(OkxGlobals.OkxCultureInfo));

        return await SendOKXSingleRequest<OkxAlgoOrderResponse>(RootClient.GetUri(Endpoints_V5_Trade_OrderAlgo), HttpMethod.Post, ct, signed: true, bodyParameters: parameters).ConfigureAwait(false);
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

        return await SendOKXSingleRequest<OkxAlgoOrderResponse>(RootClient.GetUri(Endpoints_V5_Trade_CancelAlgos), HttpMethod.Post, ct, signed: true, bodyParameters: parameters).ConfigureAwait(false);
    }

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

        return await SendOKXSingleRequest<OkxAlgoOrderResponse>(RootClient.GetUri(Endpoints_V5_Trade_CancelAdvanceAlgos), HttpMethod.Post, ct, signed: true, bodyParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Cancel unfilled algo orders(iceberg order and twap order). A maximum of 10 orders can be canceled at a time. Request parameters should be passed in the form of an array.
    /// </summary>
    /// <param name="algoOrderType">Algo Order Type</param>
    /// <param name="algoId">Algo ID</param>
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
        parameters.AddOptionalParameter("algoId", algoId?.ToString(OkxGlobals.OkxCultureInfo));
        parameters.AddOptionalParameter("instId", instrumentId);
        parameters.AddOptionalParameter("after", after?.ToString());
        parameters.AddOptionalParameter("before", before?.ToString());
        parameters.AddOptionalParameter("limit", limit.ToString());
        parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)));

        return await SendOKXRequest<IEnumerable<OkxAlgoOrder>>(RootClient.GetUri(Endpoints_V5_Trade_OrdersAlgoPending), HttpMethod.Get, ct, signed: true, queryParameters: parameters).ConfigureAwait(false);
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
        parameters.AddOptionalParameter("algoId", algoId?.ToString(OkxGlobals.OkxCultureInfo));
        parameters.AddOptionalParameter("instId", instrumentId);
        parameters.AddOptionalParameter("after", after?.ToString());
        parameters.AddOptionalParameter("before", before?.ToString());
        parameters.AddOptionalParameter("limit", limit.ToString());
        parameters.AddOptionalParameter("state", JsonConvert.SerializeObject(algoOrderState, new AlgoOrderStateConverter(false)));
        parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)));

        return await SendOKXRequest<IEnumerable<OkxAlgoOrder>>(RootClient.GetUri(Endpoints_V5_Trade_OrdersAlgoHistory), HttpMethod.Get, ct, signed: true, queryParameters: parameters).ConfigureAwait(false);
    }

    #endregion

}