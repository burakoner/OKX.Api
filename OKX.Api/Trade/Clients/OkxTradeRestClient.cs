namespace OKX.Api.Trade;

/// <summary>
/// OKX Rest Api Trade Client
/// </summary>
public class OkxTradeRestClient(OkxRestApiClient root) : OkxBaseRestClient(root)
{
    /// <summary>
    /// You can place an order only if you have sufficient funds.
    /// For leading contracts, this endpoint supports placement, but can't close positions.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="tradeMode">Trade mode
    /// Margin mode cross isolated
    /// Non-Margin mode cash
    /// spot_isolated(only applicable to SPOT lead trading, tdMode should be spot_isolated for SPOT lead trading.)
    /// Note: isolated is not available in multi-currency margin mode and portfolio margin mode.</param>
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
    /// <param name="banAmend">Whether to disallow the system from amending the size of the SPOT Market Order.</param>

    /// <param name="selfTradePreventionMode">Self trade prevention mode. Default to cancel maker. cancel_maker,cancel_taker, cancel_both. Cancel both does not support FOK.</param>
    /// 
    /// <param name="priceUsd">Place options orders in USD
    /// Only applicable to options
    /// When placing an option order, one of px/pxUsd/pxVol must be filled in, and only one can be filled in</param>
    /// <param name="priceVolatility">Place options orders based on implied volatility, where 1 represents 100%
    /// Only applicable to options
    /// When placing an option order, one of px/pxUsd/pxVol must be filled in, and only one can be filled in</param>
    /// 
    /// <param name="tradeQuoteCurrency">The quote currency used for trading. Only applicable to SPOT. The default value is the quote currency of the instId, for example: for BTC-USD, the default is USD.</param>
    /// <param name="priceAmendType">Price Amend Type</param>
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
        OkxSelfTradePreventionMode? selfTradePreventionMode = null,
        bool? banAmend = null,
        decimal? priceUsd = null,
        decimal? priceVolatility = null,
        string? tradeQuoteCurrency = null,
        OkxTradePriceAmendType? priceAmendType = null,

        IEnumerable<OkxTradeOrderPlaceAttachedAlgoRequest>? attachedAlgoOrders = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddParameter("instId", instrumentId);
        parameters.AddOptional("ccy", currency);
        parameters.AddOptional("clOrdId", clientOrderId);
        parameters.AddEnum("tdMode", tradeMode);
        parameters.AddEnum("side", orderSide);
        parameters.AddEnum("posSide", positionSide);
        parameters.AddEnum("ordType", orderType);
        parameters.Add("sz", size.ToOkxString());

        parameters.AddOptional("px", price?.ToOkxString());
        parameters.AddOptional("pxUsd", priceUsd);
        parameters.AddOptional("pxVol", priceVolatility);
        parameters.AddOptional("reduceOnly", reduceOnly);
        parameters.AddOptionalEnum("tgtCcy", quantityType);
        parameters.AddOptional("banAmend", banAmend);
        parameters.AddOptional("tradeQuoteCcy", tradeQuoteCurrency);
        parameters.AddOptionalEnum("stpMode", selfTradePreventionMode);
        parameters.AddOptional("attachAlgoOrds", attachedAlgoOrders);

        parameters.AddOptionalEnum("pxAmendType", priceAmendType);

        parameters.AddOptional("tag", OkxConstants.BrokerId);

        return ProcessOneRequestAsync<OkxTradeOrderPlaceResponse>(GetUri("api/v5/trade/order"), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
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

        return ProcessListRequestAsync<OkxTradeOrderPlaceResponse>(GetUri("api/v5/trade/batch-orders"), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
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

        return ProcessOneRequestAsync<OkxTradeOrderCancel>(GetUri("api/v5/trade/cancel-order"), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
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

        return ProcessListRequestAsync<OkxTradeOrderCancel>(GetUri("api/v5/trade/cancel-batch-orders"), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
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
    /// <param name="priceAmendType">Price Amend Type</param>
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
        OkxTradePriceAmendType? priceAmendType = null,
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
        parameters.AddOptionalEnum("pxAmendType", priceAmendType);
        parameters.AddOptional("attachAlgoOrds", attachedAlgoOrders);

        return ProcessOneRequestAsync<OkxTradeOrderAmend>(GetUri("api/v5/trade/amend-order"), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
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

        return ProcessListRequestAsync<OkxTradeOrderAmend>(GetUri("api/v5/trade/amend-batch-orders"), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
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
        parameters.AddOptionalEnum("posSide", positionSide);
        parameters.AddEnum("mgnMode", marginMode);
        parameters.AddOptional("ccy", currency);
        parameters.AddOptional("autoCxl", autoCxl);
        parameters.AddOptional("clOrdId", clientOrderId);
        parameters.AddOptional("tag", OkxConstants.BrokerId);

        return ProcessOneRequestAsync<OkxTradeClosePositionResponse>(GetUri("api/v5/trade/close-position"), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
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

        return ProcessOneRequestAsync<OkxTradeOrder>(GetUri("api/v5/trade/order"), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve all incomplete orders under the current account.
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="instrumentFamily">Instrument family</param>
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
        parameters.AddOptional("instFamily", instrumentFamily);
        parameters.AddOptional("instId", instrumentId);
        parameters.AddOptionalEnum("ordType", orderType);
        parameters.AddOptionalEnum("state", state);
        parameters.AddOptional("after", after?.ToOkxString());
        parameters.AddOptional("before", before?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxTradeOrder>(GetUri("api/v5/trade/orders-pending"), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve the completed order data for the last 7 days, and the incomplete orders that have been cancelled are only reserved for 2 hours.
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="instrumentFamily">Instrument family</param>
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

        return ProcessListRequestAsync<OkxTradeOrder>(GetUri("api/v5/trade/orders-history"), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve the completed order data of the last 3 months, and the incomplete orders that have been canceled are only reserved for 2 hours.
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="instrumentFamily">Instrument family</param>
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

        return ProcessListRequestAsync<OkxTradeOrder>(GetUri("api/v5/trade/orders-history-archive"), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve recently-filled transaction details in the last 3 day.
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="instrumentFamily">Instrument family</param>
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
        parameters.AddOptional("instFamily", instrumentFamily);
        parameters.AddOptional("instId", instrumentId);
        parameters.AddOptional("ordId", orderId?.ToOkxString());
        parameters.AddOptionalEnum("subType", transactionType);
        parameters.AddOptional("after", after?.ToOkxString());
        parameters.AddOptional("before", before?.ToOkxString());
        parameters.AddOptional("begin", begin?.ToOkxString());
        parameters.AddOptional("end", end?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxTradeTransaction>(GetUri("api/v5/trade/fills"), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve recently-filled transaction details in the last 3 months.
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="instrumentFamily">Instrument family</param>
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
        parameters.AddOptional("instFamily", instrumentFamily);
        parameters.AddOptional("instId", instrumentId);
        parameters.AddOptional("ordId", orderId?.ToOkxString());
        parameters.AddOptionalEnum("subType", transactionType);
        parameters.AddOptional("after", after?.ToOkxString());
        parameters.AddOptional("before", before?.ToOkxString());
        parameters.AddOptional("begin", begin?.ToOkxString());
        parameters.AddOptional("end", end?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxTradeTransaction>(GetUri("api/v5/trade/fills-history"), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
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

        return ProcessOneRequestAsync<OkxTradeEasyConvertCurrencyList>(GetUri("api/v5/trade/easy-convert-currency-list"), HttpMethod.Get, ct, signed: true);
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

        return ProcessListRequestAsync<OkxTradeEasyConvertOrder>(GetUri("api/v5/trade/easy-convert"), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
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

        return ProcessListRequestAsync<OkxTradeEasyConvertOrderHistory>(GetUri("api/v5/trade/easy-convert-history"), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
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

        return ProcessListRequestAsync<OkxTradeOneClickRepayCurrencyList>(GetUri("api/v5/trade/one-click-repay-currency-list"), HttpMethod.Get, ct, signed: true, parameters);
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

        return ProcessListRequestAsync<OkxTradeOneClickRepayOrder>(GetUri("api/v5/trade/one-click-repay"), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
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

        return ProcessListRequestAsync<OkxTradeOneClickRepayOrder>(GetUri("api/v5/trade/one-click-repay-history"), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Get list of debt currency data and repay currencies. Only applicable to SPOT mode.
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxTradeOneClickRepayCurrencyListV2>>> GetOneClickRepayCurrenciesV2Async(CancellationToken ct = default)
    {
        return ProcessListRequestAsync<OkxTradeOneClickRepayCurrencyListV2>(GetUri("api/v5/trade/one-click-repay-currency-list-v2"), HttpMethod.Get, ct, signed: true);
    }

    /// <summary>
    /// Trade one-click repay to repay debts. Only applicable to SPOT mode.
    /// </summary>
    /// <param name="debtCcy">Debt currency</param>
    /// <param name="repayCcyList">Repay currency list, e.g. ["USDC","BTC"]. The priority of currency to repay is consistent with the order in the array. (The first item has the highest priority)</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxTradeOneClickRepayOrderV2>>> PlaceOneClickRepayOrderV2Async(string debtCcy, IEnumerable<string> repayCcyList, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            {"debtCcy", debtCcy },
            {"repayCcyList", repayCcyList },
        };

        return ProcessListRequestAsync<OkxTradeOneClickRepayOrderV2>(GetUri("api/v5/trade/one-click-repay-v2"), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Get the history and status of one-click repay trades in the past 7 days. Only applicable to SPOT mode.
    /// </summary>
    /// <param name="after">Pagination of data to return records earlier than the requested time, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested time, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100. The default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxTradeOneClickRepayOrderHistoryV2>>> GetOneClickRepayHistoryV2Async(long? after = null, long? before = null, int limit = 100, CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);

        var parameters = new ParameterCollection();
        parameters.AddOptional("after", after?.ToOkxString());
        parameters.AddOptional("before", before?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxTradeOneClickRepayOrderHistoryV2>(GetUri("api/v5/trade/one-click-repay-history-v2"), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
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
        if (lockInterval.HasValue)
        {
            lockInterval?.ValidateIntBetween(nameof(lockInterval), 1, 10000);
        }

        var parameters = new ParameterCollection {
            { "instFamily", instrumentFamily },
        };
        parameters.AddEnum("instType", instrumentType);
        parameters.AddOptionalString("lockInterval", lockInterval);

        var result = await ProcessOneRequestAsync<OkxBooleanResponse>(GetUri("api/v5/trade/mass-cancel"), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
        if (!result) return new RestCallResult<bool?>(result.Request, result.Response, result.Raw ?? "", result.Error);
        return new RestCallResult<bool?>(result.Request, result.Response, result.Data.Result, result.Raw ?? "", result.Error);
    }

    /// <summary>
    /// Cancel all pending orders after the countdown timeout. Applicable to all trading symbols through order book (except Spread trading)
    /// </summary>
    /// <param name="timeout">The countdown for order cancellation, with second as the unit. Range of value can be 0, [10, 120]. Setting timeOut to 0 disables Cancel All After.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxCancelAllAfter>> CancelAllAfterAsync(int timeout, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            { "timeOut", timeout.ToOkxString() },
        };

        return ProcessOneRequestAsync<OkxCancelAllAfter>(GetUri("api/v5/trade/cancel-all-after"), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
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
        return ProcessOneRequestAsync<OkxTradeAccountRateLimit>(GetUri("api/v5/trade/account-rate-limit"), HttpMethod.Get, ct, signed: true);
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
    public Task<RestCallResult<OkxTradeOrderPrecheck>> OrderPrecheckAsync(
        string instrumentId,
        OkxTradeMode tradeMode,
        OkxTradeOrderSide orderSide,
        OkxTradeOrderType orderType,
        decimal size,
        decimal? price = null,
        bool? reduceOnly = null,
        OkxTradePositionSide? positionSide = null,
        OkxTradeQuantityType? quantityType = null,
        IEnumerable<OkxTradeOrderPlaceAttachedAlgoRequest>? attachedAlgoOrders = null,

        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            {"instId", instrumentId },
            {"sz", size.ToOkxString() },
        };
        parameters.AddEnum("tdMode", tradeMode);
        parameters.AddEnum("side", orderSide);
        parameters.AddOptionalEnum("posSide", positionSide);
        parameters.AddEnum("ordType", orderType);
        parameters.AddOptional("px", price?.ToOkxString());
        parameters.AddOptional("reduceOnly", reduceOnly);
        parameters.AddOptionalEnum("tgtCcy", quantityType);
        parameters.AddOptional("attachAlgoOrds", attachedAlgoOrders);

        return ProcessOneRequestAsync<OkxTradeOrderPrecheck>(GetUri("api/v5/trade/order-precheck"), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }
}