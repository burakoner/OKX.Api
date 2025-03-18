namespace OKX.Api.Trade;

/// <summary>
/// OKX Rest Api Trade Client
/// </summary>
public class OkxTradeRestClient(OkxRestApiClient root) : OkxBaseRestClient(root)
{
    // Endpoints
    private const string v5TradeOrder = "api/v5/trade/order";
    private const string v5TradeBatchOrders = "api/v5/trade/batch-orders";
    private const string v5TradeCancelOrder = "api/v5/trade/cancel-order";
    private const string v5TradeCancelBatchOrders = "api/v5/trade/cancel-batch-orders";
    private const string v5TradeAmendOrder = "api/v5/trade/amend-order";
    private const string v5TradeAmendBatchOrders = "api/v5/trade/amend-batch-orders";
    private const string v5TradeClosePosition = "api/v5/trade/close-position";
    private const string v5TradeOrderGet = "api/v5/trade/order";
    private const string v5TradeOrdersPending = "api/v5/trade/orders-pending";
    private const string v5TradeOrdersHistory = "api/v5/trade/orders-history";
    private const string v5TradeOrdersHistoryArchive = "api/v5/trade/orders-history-archive";
    private const string v5TradeFills = "api/v5/trade/fills";
    private const string v5TradeFillsHistory = "api/v5/trade/fills-history";
    private const string v5TradeEasyConvertCurrencyList = "api/v5/trade/easy-convert-currency-list";
    private const string v5TradeEasyConvert = "api/v5/trade/easy-convert";
    private const string v5TradeEasyConvertHistory = "api/v5/trade/easy-convert-history";
    private const string v5TradeOneClickRepayCurrencyList = "api/v5/trade/one-click-repay-currency-list";
    private const string v5TradeOneClickRepay = "api/v5/trade/one-click-repay";
    private const string v5TradeOneClickRepayHistory = "api/v5/trade/one-click-repay-history";
    private const string v5TradeMassCancel = "api/v5/trade/mass-cancel";
    private const string v5TradeCancelAllAfter = "api/v5/trade/cancel-all-after";
    private const string v5TradeAccountRateLimit = "api/v5/trade/account-rate-limit";
    private const string v5TradeOrderPrecheck = "api/v5/trade/order-precheck";

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
    /// 
    /// <param name="quickMarginType">Quick Margin type. Only applicable to Quick Margin Mode of isolated margin. The default value is manual</param>
    /// <param name="banAmend">Whether to disallow the system from amending the size of the SPOT Market Order.</param>
    /// 
    /// <param name="tpTriggerPriceType">Take-profit trigger price type. The Default is last</param>
    /// <param name="tpTriggerPrice">Take-profit trigger price. If you fill in this parameter, you should fill in the take-profit order price as well.</param>
    /// <param name="tpOrdPrice">Take-profit order price</param>
    /// 
    /// <param name="slTriggerPriceType">Stop-loss trigger price type. The Default is last</param>
    /// <param name="slTriggerPrice">Stop-loss trigger price. If you fill in this parameter, you should fill in the stop-loss order price.</param>
    /// <param name="slOrderPrice">Stop-loss order price. If you fill in this parameter, you should fill in the stop-loss trigger price. If the price is -1, stop-loss will be executed at the market price.</param>
    /// 
    /// <param name="attachAlgoClientOrderOrderId">Client-supplied Algo ID when plaing order attaching TP/SL. A combination of case-sensitive alphanumerics, all numbers, or all letters of up to 32 characters. It will be posted to algoClOrdId when placing TP/SL order once the general order is filled completely.</param>
    /// 
    /// <param name="selfTradePreventionId">Self trade prevention ID. Orders from the same master account with the same ID will be prevented from self trade.</param>
    /// <param name="selfTradePreventionMode">Self trade prevention mode. Default to cancel maker. cancel_maker,cancel_taker, cancel_both. Cancel both does not support FOK.</param>
    /// 
    /// <param name="priceUsd">Place options orders in USD
    /// Only applicable to options
    /// When placing an option order, one of px/pxUsd/pxVol must be filled in, and only one can be filled in</param>
    /// <param name="priceVolatility">Place options orders based on implied volatility, where 1 represents 100%
    /// Only applicable to options
    /// When placing an option order, one of px/pxUsd/pxVol must be filled in, and only one can be filled in</param>
    /// 
    /// <param name="attachedAlgoOrders">TP/SL information attached when placing order</param>
    /// 
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxTradeOrderPlaceResponse>> PlaceOrderAsync(
        string instrumentId,
        OkxTradeMode tradeMode,
        OkxTradeOrderSide orderSide,
        OkxTradePositionSide positionSide,
        OkxTradeOrderType orderType,
        decimal size,
        decimal? price = null,
        string? currency = null,
        string? clientOrderId = null,
        bool? reduceOnly = null,
        OkxTradeQuantityType? quantityType = null,

        OkxQuickMarginType? quickMarginType = null,
        bool? banAmend = null,

        OkxAlgoPriceType? tpTriggerPriceType = null,
        decimal? tpTriggerPrice = null,
        decimal? tpOrdPrice = null,

        OkxAlgoPriceType? slTriggerPriceType = null,
        decimal? slTriggerPrice = null,
        decimal? slOrderPrice = null,

        string? attachAlgoClientOrderOrderId = null,

        long? selfTradePreventionId = null,
        OkxSelfTradePreventionMode? selfTradePreventionMode = null,

        decimal? priceUsd = null,
        decimal? priceVolatility = null,

        IEnumerable<OkxTradeOrderPlaceAttachedAlgoRequest>? attachedAlgoOrders = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddEnum("tdMode", tradeMode);
        parameters.AddEnum("side", orderSide);
        parameters.AddEnum("posSide", positionSide);
        parameters.AddEnum("ordType", orderType);
        parameters.Add("instId", instrumentId);
        parameters.Add("sz", size.ToOkxString());

        parameters.AddOptional("px", price?.ToOkxString());
        parameters.AddOptional("ccy", currency);
        parameters.AddOptional("clOrdId", clientOrderId);
        parameters.AddOptional("reduceOnly", reduceOnly);
        parameters.AddOptionalEnum("tgtCcy", quantityType);

        parameters.AddOptionalEnum("quickMgnType", quickMarginType);
        parameters.AddOptional("banAmend", banAmend);

        parameters.AddOptionalEnum("tpTriggerPxType", tpTriggerPriceType);
        parameters.AddOptional("tpTriggerPx", tpTriggerPrice?.ToOkxString());
        parameters.AddOptional("tpOrdPx", tpOrdPrice?.ToOkxString());

        parameters.AddOptionalEnum("slTriggerPxType", slTriggerPriceType);
        parameters.AddOptional("slTriggerPx", slTriggerPrice?.ToOkxString());
        parameters.AddOptional("slOrdPx", slOrderPrice?.ToOkxString());

        parameters.AddOptional("attachAlgoClOrdId", attachAlgoClientOrderOrderId);

        parameters.AddOptionalEnum("stpMode", selfTradePreventionMode);
        parameters.AddOptional("stpId", selfTradePreventionId?.ToOkxString());

        parameters.AddOptional("pxUsd", priceUsd);
        parameters.AddOptional("pxVol", priceVolatility);

        parameters.AddOptional("attachAlgoOrds", attachedAlgoOrders);

        parameters.AddOptional("tag", OkxConstants.BrokerId);

        return ProcessOneRequestAsync<OkxTradeOrderPlaceResponse>(GetUri(v5TradeOrder), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Place orders in batches. Maximum 20 orders can be placed at a time. Request parameters should be passed in the form of an array.
    /// </summary>
    /// <param name="orders">Orders</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxTradeOrderPlaceResponse>>> PlaceOrdersAsync(IEnumerable<OkxTradeOrderPlaceRequest> orders, CancellationToken ct = default)
    {
        foreach (var order in orders) order.Tag = OkxConstants.BrokerId;
        var parameters = new ParameterCollection();
        parameters.SetBody(orders);

        return ProcessListRequestAsync<OkxTradeOrderPlaceResponse>(GetUri(v5TradeBatchOrders), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Cancel an incomplete order.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="orderId">Order ID</param>
    /// <param name="clientOrderId">Client Order ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxTradeOrderCancel>> CancelOrderAsync(string instrumentId, long? orderId = null, string? clientOrderId = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            {"instId", instrumentId },
        };
        parameters.AddOptional("ordId", orderId?.ToOkxString());
        parameters.AddOptional("clOrdId", clientOrderId);

        return ProcessOneRequestAsync<OkxTradeOrderCancel>(GetUri(v5TradeCancelOrder), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Cancel incomplete orders in batches. Maximum 20 orders can be canceled at a time. Request parameters should be passed in the form of an array.
    /// </summary>
    /// <param name="orders">Orders</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxTradeOrderCancel>>> CancelOrdersAsync(IEnumerable<OkxTradeOrderCancelRequest> orders, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.SetBody(orders);

        return ProcessListRequestAsync<OkxTradeOrderCancel>(GetUri(v5TradeCancelBatchOrders), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
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
    /// <param name="newPriceUsd">Modify options orders using USD prices
    /// Only applicable to options.
    /// When modifying options orders, users can only fill in one of the following: newPx, newPxUsd, or newPxVol.</param>
    /// <param name="newPriceVolatility">Modify options orders based on implied volatility, where 1 represents 100%
    /// Only applicable to options.
    /// When modifying options orders, users can only fill in one of the following: newPx, newPxUsd, or newPxVol.</param>
    /// <param name="attachedAlgoOrders">TP/SL information attached when placing order</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxTradeOrderAmend>> AmendOrderAsync(
        string instrumentId,
        long? orderId = null,
        string? clientOrderId = null,
        string? requestId = null,
        bool? cancelOnFail = null,
        decimal? newQuantity = null,
        decimal? newPrice = null,
        decimal? newPriceUsd = null,
        decimal? newPriceVolatility = null,
        IEnumerable<OkxTradeOrderAmendAttachedAlgoRequest>? attachedAlgoOrders = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "instId", instrumentId },
        };
        parameters.AddOptional("cxlOnFail", cancelOnFail);
        parameters.AddOptional("ordId", orderId?.ToOkxString());
        parameters.AddOptional("clOrdId", clientOrderId);
        parameters.AddOptional("reqId", requestId);
        parameters.AddOptional("newSz", newQuantity?.ToOkxString());
        parameters.AddOptional("newPx", newPrice?.ToOkxString());
        parameters.AddOptional("newPxUsd", newPriceUsd?.ToOkxString());
        parameters.AddOptional("newPxVol", newPriceVolatility?.ToOkxString());
        parameters.AddOptional("attachAlgoOrds", attachedAlgoOrders);

        return ProcessOneRequestAsync<OkxTradeOrderAmend>(GetUri(v5TradeAmendOrder), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Amend incomplete orders in batches. Maximum 20 orders can be amended at a time. Request parameters should be passed in the form of an array.
    /// </summary>
    /// <param name="orders">Orders</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxTradeOrderAmend>>> AmendOrdersAsync(IEnumerable<OkxTradeOrderAmendRequest> orders, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.SetBody(orders);

        return ProcessListRequestAsync<OkxTradeOrderAmend>(GetUri(v5TradeAmendBatchOrders), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
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
    public Task<RestCallResult<OkxTradeClosePositionResponse>> ClosePositionAsync(
        string instrumentId,
        OkxAccountMarginMode marginMode,
        OkxTradePositionSide? positionSide = null,
        string? currency = null,
        bool? autoCxl = null,
        string? clientOrderId = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            {"instId", instrumentId },
        };
        parameters.AddEnum("mgnMode", marginMode);
        parameters.AddOptionalEnum("posSide", positionSide);
        parameters.AddOptional("ccy", currency);
        parameters.AddOptional("autoCxl", autoCxl);
        parameters.AddOptional("clOrdId", clientOrderId);
        parameters.AddOptional("tag", OkxConstants.BrokerId);

        return ProcessOneRequestAsync<OkxTradeClosePositionResponse>(GetUri(v5TradeClosePosition), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Retrieve order details.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="orderId">Order ID</param>
    /// <param name="clientOrderId">Client Order ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxTradeOrder>> GetOrderAsync(
        string instrumentId,
        long? orderId = null,
        string? clientOrderId = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            {"instId", instrumentId },
        };
        parameters.AddOptional("ordId", orderId?.ToOkxString());
        parameters.AddOptional("clOrdId", clientOrderId);

        return ProcessOneRequestAsync<OkxTradeOrder>(GetUri(v5TradeOrderGet), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve all incomplete orders under the current account.
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="instrumentFamily">Instrument family</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="orderType">Order Type</param>
    /// <param name="state">State</param>
    /// <param name="after">Pagination of data to return records later than the requested ordId</param>
    /// <param name="before">Pagination of data to return records earlier than the requested ordId</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxTradeOrder>>> GetOpenOrdersAsync(
        OkxInstrumentType? instrumentType = null,
        string? instrumentId = null,
        string? instrumentFamily = null,
        string? underlying = null,
        OkxTradeOrderType? orderType = null,
        OkxTradeOrderState? state = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new ParameterCollection();
        parameters.AddOptionalEnum("instType", instrumentType);
        parameters.AddOptionalEnum("ordType", orderType);
        parameters.AddOptionalEnum("state", state);
        parameters.AddOptional("uly", underlying);
        parameters.AddOptional("instFamily", instrumentFamily);
        parameters.AddOptional("instId", instrumentId);
        parameters.AddOptional("after", after?.ToOkxString());
        parameters.AddOptional("before", before?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxTradeOrder>(GetUri(v5TradeOrdersPending), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve the completed order data for the last 7 days, and the incomplete orders that have been cancelled are only reserved for 2 hours.
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="instrumentFamily">Instrument family</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="orderType">Order Type</param>
    /// <param name="state">State</param>
    /// <param name="category">Category</param>
    /// <param name="after">Pagination of data to return records later than the requested order id</param>
    /// <param name="before">Pagination of data to return records earlier than the requested order id</param>
    /// <param name="begin">Filter with a begin timestamp. Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="end">Filter with a begin timestamp. Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxTradeOrder>>> GetOrderHistoryAsync(
        OkxInstrumentType instrumentType,
        string? instrumentId = null,
        string? instrumentFamily = null,
        string? underlying = null,
        OkxTradeOrderType? orderType = null,
        OkxTradeOrderState? state = null,
        OkxOrderCategory? category = null,
        long? after = null,
        long? before = null,
        long? begin = null,
        long? end = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new ParameterCollection();
        parameters.AddEnum("instType", instrumentType);
        parameters.AddOptional("uly", underlying);
        parameters.AddOptional("instFamily", instrumentFamily);
        parameters.AddOptional("instId", instrumentId);
        parameters.AddOptionalEnum("ordType", orderType);
        parameters.AddOptionalEnum("state", state);
        parameters.AddOptionalEnum("category", category);
        parameters.AddOptional("after", after?.ToOkxString());
        parameters.AddOptional("before", before?.ToOkxString());
        parameters.AddOptional("begin", begin?.ToOkxString());
        parameters.AddOptional("end", end?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxTradeOrder>(GetUri(v5TradeOrdersHistory), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve the completed order data of the last 3 months, and the incomplete orders that have been canceled are only reserved for 2 hours.
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="instrumentFamily">Instrument family</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="orderType">Order Type</param>
    /// <param name="state">State</param>
    /// <param name="category">Category</param>
    /// <param name="after">Pagination of data to return records later than the requested order id</param>
    /// <param name="before">Pagination of data to return records earlier than the requested order id</param>
    /// <param name="begin">Filter with a begin timestamp. Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="end">Filter with a begin timestamp. Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxTradeOrder>>> GetOrderArchiveAsync(
        OkxInstrumentType instrumentType,
        string? instrumentId = null,
        string? instrumentFamily = null,
        string? underlying = null,
        OkxTradeOrderType? orderType = null,
        OkxTradeOrderState? state = null,
        OkxOrderCategory? category = null,
        long? after = null,
        long? before = null,
        long? begin = null,
        long? end = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new ParameterCollection();
        parameters.AddEnum("instType", instrumentType);
        parameters.AddOptional("uly", underlying);
        parameters.AddOptional("instFamily", instrumentFamily);
        parameters.AddOptional("instId", instrumentId);
        parameters.AddOptionalEnum("ordType", orderType);
        parameters.AddOptionalEnum("state", state);
        parameters.AddOptionalEnum("category", category);
        parameters.AddOptional("after", after?.ToOkxString());
        parameters.AddOptional("before", before?.ToOkxString());
        parameters.AddOptional("begin", begin?.ToOkxString());
        parameters.AddOptional("end", end?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxTradeOrder>(GetUri(v5TradeOrdersHistoryArchive), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve recently-filled transaction details in the last 3 day.
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="instrumentFamily">Instrument family</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="orderId">Order ID</param>
    /// <param name="transactionType">Transaction type</param>
    /// <param name="after">Pagination of data to return records later than the requested <see cref="OkxTradeTransaction.BillId"/></param>
    /// <param name="before">Pagination of data to return records earlier than the requested <see cref="OkxTradeTransaction.BillId"/></param>
    /// <param name="begin">Filter with a begin timestamp. Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="end">Filter with a begin timestamp. Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxTradeTransaction>>> GetTradesAsync(
        OkxInstrumentType? instrumentType = null,
        string? instrumentId = null,
        string? instrumentFamily = null,
        string? underlying = null,
        long? orderId = null,
        OkxAccountBillSubType? transactionType = null,
        long? after = null,
        long? before = null,
        long? begin = null,
        long? end = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new ParameterCollection();
        parameters.AddOptionalEnum("instType", instrumentType);
        parameters.AddOptionalEnum("subType", transactionType);
        parameters.AddOptional("uly", underlying);
        parameters.AddOptional("instFamily", instrumentFamily);
        parameters.AddOptional("instId", instrumentId);
        parameters.AddOptional("ordId", orderId?.ToOkxString());
        parameters.AddOptional("after", after?.ToOkxString());
        parameters.AddOptional("before", before?.ToOkxString());
        parameters.AddOptional("begin", begin?.ToOkxString());
        parameters.AddOptional("end", end?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxTradeTransaction>(GetUri(v5TradeFills), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve recently-filled transaction details in the last 3 months.
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="instrumentFamily">Instrument family</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="orderId">Order ID</param>
    /// <param name="transactionType">Transaction type</param>
    /// <param name="after">Pagination of data to return records later than the requested <see cref="OkxTradeTransaction.BillId"/></param>
    /// <param name="before">Pagination of data to return records earlier than the requested <see cref="OkxTradeTransaction.BillId"/></param>
    /// <param name="begin">Filter with a begin timestamp. Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="end">Filter with a begin timestamp. Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns>RestCallResult containing enumerable OkxTransaction list</returns>
    public Task<RestCallResult<List<OkxTradeTransaction>>> GetTradesHistoryAsync(
        OkxInstrumentType instrumentType,
        string? instrumentId = null,
        string? instrumentFamily = null,
        string? underlying = null,
        long? orderId = null,
        OkxAccountBillSubType? transactionType = null,
        long? after = null,
        long? before = null,
        long? begin = null,
        long? end = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new ParameterCollection();
        parameters.AddOptionalEnum("instType", instrumentType);
        parameters.AddOptionalEnum("subType", transactionType);
        parameters.AddOptional("uly", underlying);
        parameters.AddOptional("instFamily", instrumentFamily);
        parameters.AddOptional("instId", instrumentId);
        parameters.AddOptional("ordId", orderId?.ToOkxString());
        parameters.AddOptional("after", after?.ToOkxString());
        parameters.AddOptional("before", before?.ToOkxString());
        parameters.AddOptional("begin", begin?.ToOkxString());
        parameters.AddOptional("end", end?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxTradeTransaction>(GetUri(v5TradeFillsHistory), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Get list of small convertibles and mainstream currencies. Only applicable to the crypto balance less than $10.
    /// </summary>
    /// <param name="source">Funding source</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxTradeEasyConvertCurrencyList>> GetEasyConvertCurrenciesAsync(
        OkxTradeEasyConvertSource? source = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptionalEnum("source", source);

        return ProcessOneRequestAsync<OkxTradeEasyConvertCurrencyList>(GetUri(v5TradeEasyConvertCurrencyList), HttpMethod.Get, ct, signed: true);
    }

    /// <summary>
    /// Convert small currencies to mainstream currencies.
    /// </summary>
    /// <param name="fromCcy">Type of small payment currency convert from. Maximum 5 currencies can be selected in one order</param>
    /// <param name="toCcy">Type of mainstream currency convert to. Only one receiving currency type can be selected in one order and cannot be the same as the small payment currencies.</param>
    /// <param name="source">Funding source</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxTradeEasyConvertOrder>>> PlaceEasyConvertOrderAsync(
        IEnumerable<string> fromCcy, 
        string toCcy, 
        OkxTradeEasyConvertSource? source = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            {"fromCcy", fromCcy },
            {"toCcy", toCcy },
        };
        parameters.AddOptionalEnum("source", source);

        return ProcessListRequestAsync<OkxTradeEasyConvertOrder>(GetUri(v5TradeEasyConvert), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Get the history and status of easy convert trades.
    /// </summary>
    /// <param name="after">Pagination of data to return records earlier than the requested time, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested time, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100. The default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxTradeEasyConvertOrderHistory>>> GetEasyConvertHistoryAsync(long? after = null, long? before = null, int limit = 100, CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);

        var parameters = new ParameterCollection();
        parameters.AddOptional("after", after?.ToOkxString());
        parameters.AddOptional("before", before?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxTradeEasyConvertOrderHistory>(GetUri(v5TradeEasyConvertHistory), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Get list of debt currency data and repay currencies. Debt currencies include both cross and isolated debts.
    /// </summary>
    /// <param name="debtType">Debt type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxTradeOneClickRepayCurrencyList>>> GetOneClickRepayCurrenciesAsync(OkxTradeDebtType debtType, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptionalEnum("debtType", debtType);

        return ProcessListRequestAsync<OkxTradeOneClickRepayCurrencyList>(GetUri(v5TradeOneClickRepayCurrencyList), HttpMethod.Get, ct, signed: true, parameters);
    }

    /// <summary>
    /// Trade one-click repay to repay cross debts. Isolated debts are not applicable. The maximum repayment amount is based on the remaining available balance of funding and trading accounts.
    /// </summary>
    /// <param name="debtCcy">Debt currency type. Maximum 5 currencies can be selected in one order.</param>
    /// <param name="repayCcy">Repay currency type. Only one receiving currency type can be selected in one order and cannot be the same as the small payment currencies.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxTradeOneClickRepayOrder>>> PlaceOneClickRepayOrderAsync(IEnumerable<string> debtCcy, string repayCcy, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            {"debtCcy", debtCcy },
            {"repayCcy", repayCcy },
        };

        return ProcessListRequestAsync<OkxTradeOneClickRepayOrder>(GetUri(v5TradeOneClickRepay), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Get the history and status of one-click repay trades.
    /// </summary>
    /// <param name="after">Pagination of data to return records earlier than the requested time, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested time, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100. The default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxTradeOneClickRepayOrder>>> GetOneClickRepayHistoryAsync(long? after = null, long? before = null, int limit = 100, CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);

        var parameters = new ParameterCollection();
        parameters.AddOptional("after", after?.ToOkxString());
        parameters.AddOptional("before", before?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxTradeOneClickRepayOrder>(GetUri(v5TradeOneClickRepayHistory), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Cancel all the MMP pending orders of an instrument family.
    /// Only applicable to Option in Portfolio Margin mode, and MMP privilege is required.
    /// </summary>
    /// <param name="instrumentType">Instrument type</param>
    /// <param name="instrumentFamily">Instrument family</param>
    /// <param name="lockInterval">Lock interval(ms)
    /// The range should be [0, 10 000]
    /// The default is 0. You can set it as "0" if you want to unlock it immediately.
    /// Error 54008 will be thorwn when placing order duiring lock interval, it is different from 51034 which is thrown when MMP is triggered</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<bool?>> MassCancelAsync(
        OkxInstrumentType instrumentType, 
        string instrumentFamily,
        int? lockInterval = null,
        CancellationToken ct = default)
    {
        if(lockInterval.HasValue)
        {
            lockInterval?.ValidateIntBetween(nameof(lockInterval), 1, 10000);
        }

        var parameters = new ParameterCollection {
            { "instFamily", instrumentFamily },
        };
        parameters.AddEnum("instType", instrumentType);
        parameters.AddOptionalString("lockInterval", lockInterval);

        var result = await ProcessOneRequestAsync<OkxBooleanResponse>(GetUri(v5TradeMassCancel), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
        if (!result) return new RestCallResult<bool?>(result.Request, result.Response, result.Raw, result.Error);
        return new RestCallResult<bool?>(result.Request, result.Response, result.Data.Result, result.Raw, result.Error);
    }

    /// <summary>
    /// Cancel all pending orders after the countdown timeout. Applicable to all trading symbols through order book (except Spread trading)
    /// </summary>
    /// <param name="timeout">The countdown for order cancellation, with second as the unit. Range of value can be 0, [10, 120]. Setting timeOut to 0 disables Cancel All After.</param>
    /// <param name="tag">CAA order tag. A combination of case-sensitive alphanumerics, all numbers, or all letters of up to 16 characters.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxCancelAllAfter>> CancelAllAfterAsync(int timeout, string tag, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            { "timeOut", timeout.ToOkxString() },
        };
        parameters.AddOptional("tag", tag);

        return ProcessOneRequestAsync<OkxCancelAllAfter>(GetUri(v5TradeCancelAllAfter), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Get account rate limit related information.
    /// Only new order requests and amendment order requests will be counted towards this limit. For batch order requests consisting of multiple orders, each order will be counted individually.
    /// For details, please refer to Fill ratio based sub-account rate limit
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxTradeAccountRateLimit>> GetAccountRateLimitAsync(CancellationToken ct = default)
    {
        return ProcessOneRequestAsync<OkxTradeAccountRateLimit>(GetUri(v5TradeAccountRateLimit), HttpMethod.Get, ct, signed: true);
    }

    /// <summary>
    /// This endpoint is used to precheck the account information before and after placing the order.
    /// Only applicable to Multi-currency margin mode, and Portfolio margin mode.
    /// </summary>
    /// <param name="instrumentId">Instrument ID, e.g. BTC-USDT</param>
    /// <param name="tradeMode">Trade mode.
    /// Margin mode cross isolated
    /// Non-Margin mode cash
    /// spot_isolated (only applicable to SPOT lead trading, tdMode should be spot_isolated for SPOT lead trading.)</param>
    /// <param name="orderSide">Order side, buy sell</param>
    /// <param name="positionSide">Position side</param>
    /// <param name="orderType">Order type</param>
    /// <param name="size">Quantity to buy or sell</param>
    /// <param name="price">Order price. Only applicable to limit,post_only,fok,ioc,mmp,mmp_and_post_only order.</param>
    /// <param name="reduceOnly">Whether orders can only reduce in position size.
    /// Valid options: true or false. The default value is false.
    /// Only applicable to MARGIN orders, and FUTURES/SWAP orders in net mode
    /// Only applicable to Spot and futures mode and Multi-currency margin</param>
    /// <param name="quantityType">Whether the target currency uses the quote or base currency.
    /// base_ccy: Base currency ,quote_ccy: Quote currency
    /// Only applicable to SPOT Market Orders
    /// Default is quote_ccy for buy, base_ccy for sell</param>
    /// <param name="attachedAlgoOrders">TP/SL information attached when placing order</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxTradeOrderCheck>> CheckOrderAsync(
        string instrumentId,
        OkxTradeMode tradeMode,
        OkxTradeOrderSide orderSide,
        OkxTradePositionSide positionSide,
        OkxTradeOrderType orderType,
        decimal size,
        decimal? price = null,
        bool? reduceOnly = null,
        OkxTradeQuantityType? quantityType = null,
        IEnumerable<OkxTradeOrderPlaceAttachedAlgoRequest>? attachedAlgoOrders = null,

        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            {"instId", instrumentId },
            {"sz", size.ToOkxString() },
        };
        parameters.AddOptionalEnum("tdMode", tradeMode);
        parameters.AddOptionalEnum("side", orderSide);
        parameters.AddOptionalEnum("posSide", positionSide);
        parameters.AddOptionalEnum("ordType", orderType);
        parameters.AddOptionalEnum("tgtCcy", quantityType);
        parameters.AddOptional("px", price?.ToOkxString());
        parameters.AddOptional("reduceOnly", reduceOnly);
        parameters.AddOptional("attachAlgoOrds", attachedAlgoOrders);

        return ProcessOneRequestAsync<OkxTradeOrderCheck>(GetUri(v5TradeOrderPrecheck), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }
}