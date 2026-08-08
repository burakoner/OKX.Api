namespace OKX.Api.Algo;

/// <summary>
/// OKX Rest Api Algo Trading Client
/// </summary>
public class OkxAlgoRestClient(OkxRestApiClient root) : OkxBaseRestClient(root)
{
    /// <summary>
    /// Place a conditional, OCO, standalone chase, trigger, trailing stop, TWAP, iceberg, or smart iceberg algo order.
    /// </summary>
    /// 
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="tradeMode">Trade Mode</param>
    /// <param name="orderSide">Order Side</param>
    /// <param name="algoOrderType">Algo Order Type</param>
    /// <param name="currency">Currency</param>
    /// <param name="positionSide">Position Side</param>
    /// <param name="size">Size</param>
    /// <param name="quantityType">Quantity Type</param>
    /// <param name="clientOrderId">Client-supplied Algo ID. A combination of case-sensitive alphanumerics, all numbers, or all letters of up to 32 characters.</param>
    /// <param name="closeFraction">Fraction of position to be closed when the algo order is triggered. The current contract accepts 1 only and requires either size or close fraction.</param>
    /// 
    /// <param name="tpTriggerPrice">Take Profit Trigger Price</param>
    /// <param name="tpTriggerPriceType">Take-profit trigger price type</param>
    /// <param name="tpOrderPrice">Take Profit Order Price</param>
    /// <param name="tpOrderKind">Take Profit Order Kind</param>
    /// <param name="slTriggerPrice">Stop Loss Trigger Price</param>
    /// <param name="slTriggerPriceType">Stop-loss trigger price. If you fill in this parameter, you should fill in the stop-loss order price.</param>
    /// <param name="slOrderPrice">Stop Loss Order Price</param>
    /// <param name="cancelOnClosePosition">Whether the TP/SL order placed by the user is associated with the corresponding position of the instrument. If it is associated, the TP/SL order will be cancelled when the position is fully closed; if it is not, the TP/SL order will not be affected when the position is fully closed.
    /// Valid values:
    /// true: Place a TP/SL order associated with the position
    /// false: Place a TP/SL order that is not associated with the position
    /// The default value is false. If true is passed in, users must pass reduceOnly = true as well, indicating that when placing a TP/SL order associated with a position, it must be a reduceOnly order.
    /// Only applicable to Single-currency margin and Multi-currency margin.</param>
    /// <param name="reduceOnly">Whether the order can only reduce the position size. The default is false.</param>
    /// 
    /// <param name="triggerPrice">Trigger Price</param>
    /// <param name="orderPrice">Order Price</param>
    /// <param name="triggerPriceType">Trigger Price Type</param>
    /// <param name="triggerOrderType">Trigger order type</param>
    /// <param name="attachedAlgoOrders">Attached TP/SL or trailing stop order info. Applicable to Spot and Futures mode/Multi-currency margin/Portfolio margin</param>
    /// <param name="advancedChaseParameters">Chase parameters required when a FUTURES or SWAP trigger order uses <see cref="OkxAlgoTriggerOrderType.Chase"/>.</param>
    /// 
    /// <param name="callbackRatio">Callback price ratio , e.g. 0.01</param>
    /// <param name="callbackSpread">Callback price variance</param>
    /// <param name="activePrice">Active price</param>
    /// 
    /// <param name="priceVariance">Price Variance</param>
    /// <param name="priceSpread">Price Spread</param>
    /// <param name="sizeLimit">Average TWAP amount or minimum size per smart iceberg execution.</param>
    /// <param name="priceLimit">TWAP or smart iceberg price limit.</param>
    /// 
    /// <param name="timeInterval">TWAP execution interval in seconds.</param>
    /// <param name="limitOrderNumber">Number of limit-order splits. Required for smart iceberg orders.</param>
    /// <param name="aggressiveness">Execution aggressiveness. Required for smart iceberg orders.</param>
    /// <param name="smartIcebergTriggerParameters">Optional smart iceberg start triggers. An empty list starts the order immediately.</param>
    /// 
    /// <param name="chaseType">Chase type</param>
    /// <param name="chaseValue">Chase value.
    /// It represents distance from best bid/ask price when chaseType is distance.
    /// For USDT-margined contract, the unit is USDT.
    /// For USDC-margined contract, the unit is USDC.
    /// For Crypto-margined contract, the unit is USD.
    /// It represents ratio when chaseType is ratio. 0.1 represents 10%.
    /// The default value is 0.</param>
    /// <param name="maxChaseType">Maximum chase type. maxChaseTyep and maxChaseVal need to be used together or none of them.</param>
    /// <param name="maxChaseValue">Maximum chase value.
    /// It represents maximum distance when maxChaseType is distance.
    /// It represents ratio when maxChaseType is ratio. 0.1 represents 10%.</param>
    /// 
    /// <param name="tradeQuoteCurrency">The quote currency used for trading. Only applicable to SPOT. The default value is the quote currency of the instId, for example: for BTC-USD, the default is USD.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxAlgoOrderResponse>> PlaceOrderAsync(

        // Common
        string instrumentId,
        OkxTradeMode tradeMode,
        OkxTradeOrderSide orderSide,
        OkxAlgoOrderType algoOrderType,
        string? currency = null,
        OkxTradePositionSide? positionSide = null,
        decimal? size = null,
        OkxTradeQuantityType? quantityType = null,
        string? clientOrderId = null,
        decimal? closeFraction = null,
        string? tradeQuoteCurrency = null,

        // Take Profit / Stop Loss
        decimal? tpTriggerPrice = null,
        OkxAlgoPriceType? tpTriggerPriceType = null,
        decimal? tpOrderPrice = null,
        OkxAlgoOrderKind? tpOrderKind = null,
        decimal? slTriggerPrice = null,
        OkxAlgoPriceType? slTriggerPriceType = null,
        decimal? slOrderPrice = null,
        bool? cancelOnClosePosition = null,
        bool? reduceOnly = null,

        // Chase Order
        OkxAlgoChaseType? chaseType = null,
        decimal? chaseValue = null,
        OkxAlgoChaseType? maxChaseType = null,
        decimal? maxChaseValue = null,

        // Trigger Order
        decimal? triggerPrice = null,
        decimal? orderPrice = null,
        OkxAlgoPriceType? triggerPriceType = null,
        OkxAlgoTriggerOrderType? triggerOrderType = null,
        IEnumerable<OkxAlgoAttachedAlgoPlaceRequest>? attachedAlgoOrders = null,

        // Trailing Stop Order
        decimal? callbackRatio = null,
        decimal? callbackSpread = null,
        decimal? activePrice = null,

        // TWAP Order
        OkxPriceVariance? priceVariance = null,
        decimal? priceSpread = null,
        decimal? sizeLimit = null,
        decimal? priceLimit = null,
        long? timeInterval = null,

        // Cancellation Token
        CancellationToken ct = default,

        // Advanced Chase Trigger Order
        IEnumerable<OkxAlgoAdvancedChaseParameters>? advancedChaseParameters = null,

        // Smart Iceberg Order
        int? limitOrderNumber = null,
        OkxAlgoSmartIcebergAggressiveness? aggressiveness = null,
        IEnumerable<OkxAlgoSmartIcebergTriggerParameters>? smartIcebergTriggerParameters = null)
    {
        var attachedAlgoOrderList = attachedAlgoOrders?.ToList();
        var advancedChaseParameterList = advancedChaseParameters?.ToList();
        var smartIcebergTriggerParameterList = smartIcebergTriggerParameters?.ToList();

        ValidateStandaloneChaseParameters(algoOrderType, chaseType, chaseValue, maxChaseType, maxChaseValue);
        ValidateAdvancedChaseParameters(
            algoOrderType,
            triggerOrderType,
            orderPrice,
            attachedAlgoOrderList,
            advancedChaseParameterList);
        ValidateSmartIcebergParameters(
            algoOrderType,
            sizeLimit,
            limitOrderNumber,
            aggressiveness,
            smartIcebergTriggerParameterList);

        // Common
        var parameters = new ParameterCollection();
        parameters.AddOptional("instId", instrumentId);
        parameters.AddEnum("tdMode", tradeMode);
        parameters.AddEnum("side", orderSide);
        parameters.AddEnum("ordType", algoOrderType);

        // Optional
        parameters.AddOptional("ccy", currency);
        parameters.AddOptionalEnum("posSide", positionSide);
        parameters.AddOptional("sz", size?.ToOkxString());
        parameters.AddOptionalEnum("tgtCcy", quantityType);
        parameters.AddOptional("algoClOrdId", clientOrderId);
        parameters.AddOptional("closeFraction", closeFraction?.ToOkxString());
        parameters.AddOptional("tradeQuoteCcy", tradeQuoteCurrency);

        // Take Profit / Stop Loss
        parameters.AddOptional("tpTriggerPx", tpTriggerPrice?.ToOkxString());
        parameters.AddOptionalEnum("tpTriggerPxType", tpTriggerPriceType);
        parameters.AddOptional("tpOrdPx", tpOrderPrice?.ToOkxString());
        parameters.AddOptionalEnum("tpOrdKind", tpOrderKind);
        parameters.AddOptional("slTriggerPx", slTriggerPrice?.ToOkxString());
        parameters.AddOptionalEnum("slTriggerPxType", slTriggerPriceType);
        parameters.AddOptional("slOrdPx", slOrderPrice?.ToOkxString());
        parameters.AddOptional("cxlOnClosePos", cancelOnClosePosition);
        parameters.AddOptional("reduceOnly", reduceOnly);

        // Chase Order
        parameters.AddOptionalEnum("chaseType", chaseType);
        parameters.AddOptional("chaseVal", chaseValue?.ToOkxString());
        parameters.AddOptionalEnum("maxChaseType", maxChaseType);
        parameters.AddOptional("maxChaseVal", maxChaseValue?.ToOkxString());
        // parameters.AddOptional("reduceOnly", reduceOnly);

        // Trigger Order
        parameters.AddOptional("triggerPx", triggerPrice?.ToOkxString());
        parameters.AddOptional("orderPx", orderPrice?.ToOkxString());
        parameters.AddOptionalEnum("advanceOrdType", triggerOrderType);
        parameters.AddOptionalEnum("triggerPxType", triggerPriceType);
        parameters.AddOptional("attachAlgoOrds", attachedAlgoOrderList is { Count: > 0 } ? attachedAlgoOrderList : null);
        parameters.AddOptional("advChaseParams", advancedChaseParameterList is { Count: > 0 } ? advancedChaseParameterList : null);

        // Trailing Stop Order
        parameters.AddOptional("callbackRatio", callbackRatio?.ToOkxString());
        parameters.AddOptional("callbackSpread", callbackSpread?.ToOkxString());
        parameters.AddOptional("activePx", activePrice?.ToOkxString());
        // parameters.AddOptional("reduceOnly", reduceOnly);

        // TWAP Order
        parameters.AddOptionalEnum("pxVar", priceVariance);
        parameters.AddOptional("pxSpread", priceSpread?.ToOkxString());
        parameters.AddOptional("szLimit", sizeLimit?.ToOkxString());
        parameters.AddOptional("pxLimit", priceLimit?.ToOkxString());
        parameters.AddOptional("timeInterval", timeInterval?.ToOkxString());

        // Smart Iceberg Order
        parameters.AddOptional("lmtOrderNumber", limitOrderNumber?.ToOkxString());
        parameters.AddOptionalEnum("aggressiveness", aggressiveness);
        parameters.AddOptional("triggerParams", smartIcebergTriggerParameterList is { Count: > 0 } ? smartIcebergTriggerParameterList : null);

        // Broker Id
        parameters.AddOptional("tag", OkxConstants.BrokerId);

        // Reequest
        return ProcessOneRequestAsync<OkxAlgoOrderResponse>(GetUri("api/v5/trade/order-algo"), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Cancel unfilled algo orders(trigger order, oco order, conditional order). A maximum of 10 orders can be canceled at a time. Request parameters should be passed in the form of an array.
    /// </summary>
    /// <param name="algoOrderId">Algo ID</param>
    /// <param name="instrumentId">	Instrument ID, e.g. BTC-USDT</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxAlgoCancelOrderResponse>>> CancelOrderAsync(long algoOrderId, string instrumentId, CancellationToken ct = default)
        => CancelOrdersAsync([new() { AlgoOrderId = algoOrderId.ToOkxString(), InstrumentId = instrumentId }], ct);
    
    /// <summary>
    /// Cancel unfilled algo orders(trigger order, oco order, conditional order). A maximum of 10 orders can be canceled at a time. Request parameters should be passed in the form of an array.
    /// </summary>
    /// <param name="orders">Orders to Cancel</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxAlgoCancelOrderResponse>>> CancelOrdersAsync(IEnumerable<OkxAlgoCancelOrderRequest> orders, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.SetBody(orders);

        return ProcessListRequestAsync<OkxAlgoCancelOrderResponse>(GetUri("api/v5/trade/cancel-algos"), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Amend an unfilled stop order or the mutable chase values of a pending FUTURES/SWAP trigger-to-chase order.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="algoOrderId">Algo ID. Either algoId or algoClOrdId is required. If both are passed, algoId will be used.</param>
    /// <param name="algoClientOrderId">Client-supplied Algo ID. Either algoId or algoClOrdId is required. If both are passed, algoId will be used.</param>
    /// <param name="cancelOnFail">Whether the order needs to be automatically canceled when the order amendment fails. Valid options: false or true, the default is false.</param>
    /// <param name="clientRequestId">Client Request ID as assigned by the client for order amendment. A combination of case-sensitive alphanumerics, all numbers, or all letters of up to 32 characters.The response will include the corresponding reqId to help you identify the request if you provide it in the request.</param>
    /// <param name="newSize">New quantity after amendment.</param>
    /// <param name="newTakeProfitTriggerPriceType">Take-profit trigger price type</param>
    /// <param name="newTakeProfitTriggerPrice">Take-profit trigger price. Either the take-profit trigger price or order price is 0, it means that the take-profit is deleted</param>
    /// <param name="newTakeProfitOrderPrice">Take-profit order price. If the price is -1, take-profit will be executed at the market price.</param>
    /// <param name="newStopLossTriggerPriceType">Stop-loss trigger price type</param>
    /// <param name="newStopLossTriggerPrice">Stop-loss trigger price. Either the stop-loss trigger price or order price is 0, it means that the stop-loss is deleted</param>
    /// <param name="newStopLossOrderPrice">Stop-loss order price. If the price is -1, stop-loss will be executed at the market price.</param>
    /// <param name="newTriggerPrice">New trigger price after amendment</param>
    /// <param name="newOrderPrice">New order price after amendment. If the price is -1, the order will be executed at the market price.</param>
    /// <param name="newTriggerPriceType">New trigger price type after amendment</param>
    /// <param name="attachedAlgoOrders">Attached TP/SL or trailing stop order info. Applicable to Spot and Futures mode/Multi-currency margin/Portfolio margin</param>
    /// <param name="advancedChaseParameters">Chase values to amend before a trigger order with <c>advanceOrdType=chase</c> fires.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxAlgoAmendOrderResponse>> AmendOrderAsync(

        // Common
        string instrumentId,
        long? algoOrderId = null,
        string? algoClientOrderId = null,
        bool? cancelOnFail = null,

        string? clientRequestId = null,
        decimal? newSize = null,

        // Take Profit
        OkxAlgoPriceType? newTakeProfitTriggerPriceType = null,
        decimal? newTakeProfitTriggerPrice = null,
        decimal? newTakeProfitOrderPrice = null,

        // Stop Loss
        OkxAlgoPriceType? newStopLossTriggerPriceType = null,
        decimal? newStopLossTriggerPrice = null,
        decimal? newStopLossOrderPrice = null,

        // Trigger Order
        OkxAlgoPriceType? newTriggerPriceType = null,
        decimal? newTriggerPrice = null,
        decimal? newOrderPrice = null,
        IEnumerable<OkxAlgoAttachedAlgoAmendRequest>? attachedAlgoOrders = null,

        // Cancellation Token
        CancellationToken ct = default,

        // Advanced Chase Trigger Order
        IEnumerable<OkxAlgoAdvancedChaseAmendParameters>? advancedChaseParameters = null)
    {
        if (algoOrderId is null && string.IsNullOrWhiteSpace(algoClientOrderId))
            throw new ArgumentException("Either algo order ID or algo client order ID is required.", nameof(algoOrderId));
        if (newSize.HasValue && newSize.Value <= 0)
            throw new ArgumentOutOfRangeException(nameof(newSize), "New size must be greater than zero.");

        var attachedAlgoOrderList = attachedAlgoOrders?.ToList();
        var advancedChaseParameterList = advancedChaseParameters?.ToList();
        ValidateAdvancedChaseAmendParameters(attachedAlgoOrderList, advancedChaseParameterList);

        // Common
        var parameters = new ParameterCollection {
            {"instId", instrumentId },
        };
        parameters.AddOptional("algoId", algoOrderId);
        parameters.AddOptional("algoClOrdId", algoClientOrderId);
        parameters.AddOptional("cxlOnFail", cancelOnFail);
        parameters.AddOptional("reqId", clientRequestId);
        parameters.AddOptional("newSz", newSize?.ToOkxString());

        // Take Profit
        parameters.AddOptionalEnum("newTpTriggerPxType", newTakeProfitTriggerPriceType);
        parameters.AddOptional("newTpTriggerPx", newTakeProfitTriggerPrice?.ToOkxString());
        parameters.AddOptional("newTpOrdPx", newTakeProfitOrderPrice?.ToOkxString());

        // Stop Loss
        parameters.AddOptionalEnum("newSlTriggerPxType", newStopLossTriggerPriceType);
        parameters.AddOptional("newSlTriggerPx", newStopLossTriggerPrice?.ToOkxString());
        parameters.AddOptional("newSlOrdPx", newStopLossOrderPrice?.ToOkxString());
        
        // Trigger Order
        parameters.AddOptionalEnum("newTriggerPxType", newTriggerPriceType);
        parameters.AddOptional("newTriggerPx", newTriggerPrice?.ToOkxString());
        parameters.AddOptional("newOrdPx", newOrderPrice?.ToOkxString());
        parameters.AddOptional("attachAlgoOrds", attachedAlgoOrderList is { Count: > 0 } ? attachedAlgoOrderList : null);
        parameters.AddOptional("advChaseParams", advancedChaseParameterList is { Count: > 0 } ? advancedChaseParameterList : null);

        // Reequest
        return ProcessOneRequestAsync<OkxAlgoAmendOrderResponse>(GetUri("api/v5/trade/amend-algos"), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Get Algo order details
    /// </summary>
    /// <param name="algoOrderId">Algo ID. Either algoId or algoClOrdId is required.If both are passed, algoId will be used.</param>
    /// <param name="algoClientOrderId">Client-supplied Algo ID. A combination of case-sensitive alphanumerics, all numbers, or all letters of up to 32 characters.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxAlgoOrder>> GetOrderAsync(long? algoOrderId = null, string? algoClientOrderId = null, CancellationToken ct = default)
    {
        if (algoOrderId is null && string.IsNullOrWhiteSpace(algoClientOrderId))
            throw new ArgumentException("Either algo order ID or algo client order ID is required.", nameof(algoOrderId));

        var parameters = new ParameterCollection();
        parameters.AddOptional("algoId", algoOrderId?.ToOkxString());
        parameters.AddOptional("algoClOrdId", algoClientOrderId);

        return ProcessOneRequestAsync<OkxAlgoOrder>(GetUri("api/v5/trade/order-algo"), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve a list of untriggered Algo orders under the current account.
    /// </summary>
    /// <param name="algoOrderType">Algo Order Type</param>
    /// <param name="algoId">Algo ID</param>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="after">Pagination of data to return records earlier than the requested algo ID</param>
    /// <param name="before">Pagination of data to return records newer than the requested algo ID</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxAlgoOrder>>> GetOpenOrdersAsync(
        OkxAlgoOrderType algoOrderType,
        long? algoId = null,
        OkxInstrumentType? instrumentType = null,
        string? instrumentId = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
        => GetOpenOrdersAsync([algoOrderType], algoId, instrumentType, instrumentId, after, before, limit, ct);

    /// <summary>
    /// Retrieve untriggered algo orders. The current contract permits the conditional and OCO types to be queried together.
    /// </summary>
    /// <param name="algoOrderTypes">One order type, or conditional and OCO together</param>
    /// <param name="algoId">Algo ID</param>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="after">Pagination of data to return records earlier than the requested algo ID</param>
    /// <param name="before">Pagination of data to return records newer than the requested algo ID</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxAlgoOrder>>> GetOpenOrdersAsync(
        IEnumerable<OkxAlgoOrderType> algoOrderTypes,
        long? algoId = null,
        OkxInstrumentType? instrumentType = null,
        string? instrumentId = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        ValidateAlgoOrderInstrumentType(instrumentType);
        var parameters = new ParameterCollection();
        parameters.AddOptional("ordType", FormatAlgoOrderTypes(algoOrderTypes));
        parameters.AddOptional("algoId", algoId?.ToOkxString());
        parameters.AddOptionalEnum("instType", instrumentType);
        parameters.AddOptional("instId", instrumentId);
        parameters.AddOptional("after", after?.ToOkxString());
        parameters.AddOptional("before", before?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxAlgoOrder>(GetUri("api/v5/trade/orders-algo-pending"), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve algo order history under the current account.
    /// </summary>
    /// <param name="algoOrderType">Algo Order Type</param>
    /// <param name="algoOrderState">Algo Order State</param>
    /// <param name="algoId">Algo ID</param>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="after">Pagination of data to return records earlier than the requested algo ID</param>
    /// <param name="before">Pagination of data to return records newer than the requested algo ID</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxAlgoOrder>>> GetOrderHistoryAsync(
        OkxAlgoOrderType algoOrderType,
        OkxAlgoOrderState? algoOrderState = null,
        long? algoId = null,
        OkxInstrumentType? instrumentType = null,
        string? instrumentId = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
        => GetOrderHistoryAsync([algoOrderType], algoOrderState, algoId, instrumentType, instrumentId, after, before, limit, ct);

    /// <summary>
    /// Retrieve algo order history. The current contract permits the conditional and OCO types to be queried together.
    /// </summary>
    /// <param name="algoOrderTypes">One order type, or conditional and OCO together</param>
    /// <param name="algoOrderState">Effective, canceled, or failed state. Either state or algo ID is required.</param>
    /// <param name="algoId">Algo ID. Either state or algo ID is required.</param>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="after">Pagination of data to return records earlier than the requested algo ID</param>
    /// <param name="before">Pagination of data to return records newer than the requested algo ID</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxAlgoOrder>>> GetOrderHistoryAsync(
        IEnumerable<OkxAlgoOrderType> algoOrderTypes,
        OkxAlgoOrderState? algoOrderState = null,
        long? algoId = null,
        OkxInstrumentType? instrumentType = null,
        string? instrumentId = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        if (algoOrderState is null && algoId is null)
            throw new ArgumentException("Either algo order state or algo ID is required.", nameof(algoOrderState));
        if (algoOrderState.HasValue && algoOrderState.Value.IsNotIn(
            OkxAlgoOrderState.Effective,
            OkxAlgoOrderState.Canceled,
            OkxAlgoOrderState.Failed))
            throw new ArgumentOutOfRangeException(nameof(algoOrderState), "History state must be effective, canceled, or order_failed.");
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        ValidateAlgoOrderInstrumentType(instrumentType);
        var parameters = new ParameterCollection();
        parameters.AddOptional("ordType", FormatAlgoOrderTypes(algoOrderTypes));
        parameters.AddOptionalEnum("state", algoOrderState);
        parameters.AddOptional("algoId", algoId?.ToOkxString());
        parameters.AddOptionalEnum("instType", instrumentType);
        parameters.AddOptional("instId", instrumentId);
        parameters.AddOptional("after", after?.ToOkxString());
        parameters.AddOptional("before", before?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxAlgoOrder>(GetUri("api/v5/trade/orders-algo-history"), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    private static void ValidateStandaloneChaseParameters(
        OkxAlgoOrderType algoOrderType,
        OkxAlgoChaseType? chaseType,
        decimal? chaseValue,
        OkxAlgoChaseType? maximumChaseType,
        decimal? maximumChaseValue)
    {
        var hasStandaloneChaseParameters = chaseType.HasValue || chaseValue.HasValue || maximumChaseType.HasValue || maximumChaseValue.HasValue;
        if (algoOrderType != OkxAlgoOrderType.Chase && hasStandaloneChaseParameters)
            throw new ArgumentException("Root chase parameters are only applicable to standalone chase orders.", nameof(chaseType));
        if (maximumChaseType.HasValue != maximumChaseValue.HasValue)
            throw new ArgumentException("Maximum chase type and maximum chase value must be supplied together.", nameof(maximumChaseType));
    }

    private static void ValidateAdvancedChaseParameters(
        OkxAlgoOrderType algoOrderType,
        OkxAlgoTriggerOrderType? triggerOrderType,
        decimal? orderPrice,
        IReadOnlyCollection<OkxAlgoAttachedAlgoPlaceRequest>? attachedAlgoOrders,
        IReadOnlyCollection<OkxAlgoAdvancedChaseParameters>? advancedChaseParameters)
    {
        if (triggerOrderType.HasValue && triggerOrderType.Value.IsNotIn(
            OkxAlgoTriggerOrderType.FillOrKill,
            OkxAlgoTriggerOrderType.ImmediateOrCancelOrder,
            OkxAlgoTriggerOrderType.Chase))
            throw new ArgumentOutOfRangeException(nameof(triggerOrderType));
        var usesAdvancedChase = triggerOrderType == OkxAlgoTriggerOrderType.Chase;
        if (triggerOrderType.HasValue && algoOrderType != OkxAlgoOrderType.Trigger)
            throw new ArgumentException("Advanced order type is only applicable to trigger orders.", nameof(triggerOrderType));
        if (usesAdvancedChase && advancedChaseParameters is not { Count: > 0 })
            throw new ArgumentException("Advanced chase parameters are required for a trigger chase order.", nameof(advancedChaseParameters));
        if (!usesAdvancedChase && advancedChaseParameters is { Count: > 0 })
            throw new ArgumentException("Advanced chase parameters require advanceOrdType=chase.", nameof(advancedChaseParameters));
        if (usesAdvancedChase && orderPrice.HasValue)
            throw new ArgumentException("Order price is not applicable to a trigger chase order.", nameof(orderPrice));
        if (usesAdvancedChase && attachedAlgoOrders is { Count: > 0 })
            throw new ArgumentException("Attached TP/SL orders are not applicable to a trigger chase order.", nameof(attachedAlgoOrders));

        if (advancedChaseParameters is null)
            return;

        foreach (var parameters in advancedChaseParameters)
        {
            if (parameters is null)
                throw new ArgumentException("Advanced chase parameters cannot contain null items.", nameof(advancedChaseParameters));
            if (parameters.ChaseType.HasValue && parameters.ChaseType.Value.IsNotIn(OkxAlgoChaseType.Distance, OkxAlgoChaseType.Ratio))
                throw new ArgumentOutOfRangeException(nameof(advancedChaseParameters), "Unsupported chase type.");
            if (parameters.ChaseValue.HasValue && parameters.ChaseValue.Value < 0)
                throw new ArgumentOutOfRangeException(nameof(advancedChaseParameters), "Chase value cannot be negative.");
            if (parameters.MaximumChaseType.HasValue != parameters.MaximumChaseValue.HasValue)
                throw new ArgumentException("Maximum chase type and maximum chase value must be supplied together.", nameof(advancedChaseParameters));
            if (parameters.MaximumChaseType.HasValue && parameters.MaximumChaseType.Value.IsNotIn(OkxAlgoChaseType.Distance, OkxAlgoChaseType.Ratio))
                throw new ArgumentOutOfRangeException(nameof(advancedChaseParameters), "Unsupported maximum chase type.");
            if (parameters.MaximumChaseValue.HasValue && parameters.MaximumChaseValue.Value <= 0)
                throw new ArgumentOutOfRangeException(nameof(advancedChaseParameters), "Maximum chase value must be greater than zero.");
        }
    }

    private static void ValidateAdvancedChaseAmendParameters(
        IReadOnlyCollection<OkxAlgoAttachedAlgoAmendRequest>? attachedAlgoOrders,
        IReadOnlyCollection<OkxAlgoAdvancedChaseAmendParameters>? advancedChaseParameters)
    {
        if (advancedChaseParameters is not { Count: > 0 })
            return;
        if (attachedAlgoOrders is { Count: > 0 })
            throw new ArgumentException("Attached TP/SL amendments are not applicable to a trigger chase order.", nameof(attachedAlgoOrders));

        foreach (var parameters in advancedChaseParameters)
        {
            if (parameters is null)
                throw new ArgumentException("Advanced chase amendment parameters cannot contain null items.", nameof(advancedChaseParameters));
            if (!parameters.NewChaseValue.HasValue && !parameters.NewMaximumChaseValue.HasValue)
                throw new ArgumentException("At least one chase value must be supplied for amendment.", nameof(advancedChaseParameters));
            if (parameters.NewChaseValue.HasValue && parameters.NewChaseValue.Value < 0)
                throw new ArgumentOutOfRangeException(nameof(advancedChaseParameters), "New chase value cannot be negative.");
            if (parameters.NewMaximumChaseValue.HasValue && parameters.NewMaximumChaseValue.Value <= 0)
                throw new ArgumentOutOfRangeException(nameof(advancedChaseParameters), "New maximum chase value must be greater than zero.");
        }
    }

    private static void ValidateSmartIcebergParameters(
        OkxAlgoOrderType algoOrderType,
        decimal? sizeLimit,
        int? limitOrderNumber,
        OkxAlgoSmartIcebergAggressiveness? aggressiveness,
        IReadOnlyCollection<OkxAlgoSmartIcebergTriggerParameters>? triggerParameters)
    {
        var hasSmartIcebergOnlyParameters = limitOrderNumber.HasValue || aggressiveness.HasValue || triggerParameters is { Count: > 0 };
        if (algoOrderType != OkxAlgoOrderType.SmartIceberg && hasSmartIcebergOnlyParameters)
            throw new ArgumentException("Smart iceberg parameters require ordType=smart_iceberg.", nameof(limitOrderNumber));
        if (algoOrderType != OkxAlgoOrderType.SmartIceberg)
            return;
        if (!sizeLimit.HasValue)
            throw new ArgumentException("Size limit is required for smart iceberg orders.", nameof(sizeLimit));
        if (!limitOrderNumber.HasValue)
            throw new ArgumentException("Limit order number is required for smart iceberg orders.", nameof(limitOrderNumber));
        if (!aggressiveness.HasValue || aggressiveness.Value.IsNotIn(
            OkxAlgoSmartIcebergAggressiveness.Radical,
            OkxAlgoSmartIcebergAggressiveness.Mid,
            OkxAlgoSmartIcebergAggressiveness.Conservative))
            throw new ArgumentOutOfRangeException(nameof(aggressiveness), "A documented smart iceberg aggressiveness is required.");

        if (triggerParameters is null)
            return;

        foreach (var parameters in triggerParameters)
        {
            if (parameters is null)
                throw new ArgumentException("Smart iceberg trigger parameters cannot contain null items.", nameof(triggerParameters));
            if (parameters.TriggerAction != OkxAlgoSmartIcebergTriggerAction.Start)
                throw new ArgumentOutOfRangeException(nameof(triggerParameters), "The current smart iceberg contract supports the start action only.");
            if (parameters.TriggerStrategy.IsNotIn(
                OkxAlgoSmartIcebergTriggerStrategy.Instant,
                OkxAlgoSmartIcebergTriggerStrategy.Price,
                OkxAlgoSmartIcebergTriggerStrategy.RSI))
                throw new ArgumentOutOfRangeException(nameof(triggerParameters), "Unsupported smart iceberg trigger strategy.");
            if (parameters.TriggerStrategy != OkxAlgoSmartIcebergTriggerStrategy.Price && parameters.TriggerPrice.HasValue)
                throw new ArgumentException("Trigger price is only valid for the smart iceberg price strategy.", nameof(triggerParameters));
            var hasRsiParameters = parameters.TriggerCondition.HasValue || parameters.TimeFrame.HasValue || parameters.Threshold.HasValue || parameters.TimePeriod.HasValue;
            if (parameters.TriggerStrategy != OkxAlgoSmartIcebergTriggerStrategy.RSI && hasRsiParameters)
                throw new ArgumentException("RSI fields are only valid for the smart iceberg RSI strategy.", nameof(triggerParameters));
            if (parameters.TriggerCondition.HasValue && parameters.TriggerCondition.Value.IsNotIn(
                OkxAlgoSmartIcebergTriggerCondition.CrossUp,
                OkxAlgoSmartIcebergTriggerCondition.CrossDown,
                OkxAlgoSmartIcebergTriggerCondition.Above,
                OkxAlgoSmartIcebergTriggerCondition.Below,
                OkxAlgoSmartIcebergTriggerCondition.Cross))
                throw new ArgumentOutOfRangeException(nameof(triggerParameters), "Unsupported smart iceberg trigger condition.");
            if (parameters.TimeFrame.HasValue && parameters.TimeFrame.Value.IsNotIn(
                OkxAlgoSmartIcebergTimeFrame.ThreeMinutes,
                OkxAlgoSmartIcebergTimeFrame.FiveMinutes,
                OkxAlgoSmartIcebergTimeFrame.FifteenMinutes,
                OkxAlgoSmartIcebergTimeFrame.ThirtyMinutes,
                OkxAlgoSmartIcebergTimeFrame.OneHour,
                OkxAlgoSmartIcebergTimeFrame.FourHours,
                OkxAlgoSmartIcebergTimeFrame.OneDay))
                throw new ArgumentOutOfRangeException(nameof(triggerParameters), "Unsupported smart iceberg timeframe.");
            if (parameters.Threshold.HasValue)
                parameters.Threshold.Value.ValidateIntBetween(nameof(parameters.Threshold), 1, 100);
            if (parameters.TimePeriod.HasValue && parameters.TimePeriod.Value != 14)
                throw new ArgumentOutOfRangeException(nameof(triggerParameters), "Smart iceberg RSI time period is fixed at 14.");
        }
    }

    private static string FormatAlgoOrderTypes(IEnumerable<OkxAlgoOrderType> algoOrderTypes)
    {
        if (algoOrderTypes is null)
            throw new ArgumentNullException(nameof(algoOrderTypes));

        var types = algoOrderTypes.Distinct().ToList();
        if (types.Count == 0)
            throw new ArgumentException("At least one algo order type is required.", nameof(algoOrderTypes));
        if (types.Any(type => type.IsNotIn(
            OkxAlgoOrderType.Conditional,
            OkxAlgoOrderType.OCO,
            OkxAlgoOrderType.Trigger,
            OkxAlgoOrderType.TrailingOrder,
            OkxAlgoOrderType.Iceberg,
            OkxAlgoOrderType.TWAP,
            OkxAlgoOrderType.Chase,
            OkxAlgoOrderType.SmartIceberg)))
            throw new ArgumentOutOfRangeException(nameof(algoOrderTypes), "Unsupported algo order type.");
        if (types.Count > 1 && !(types.Count == 2 && types.Contains(OkxAlgoOrderType.Conditional) && types.Contains(OkxAlgoOrderType.OCO)))
            throw new ArgumentException("Only conditional and OCO can be queried together.", nameof(algoOrderTypes));

        return string.Join(",", types.Select(type => MapConverter.GetString(type)));
    }

    private static void ValidateAlgoOrderInstrumentType(OkxInstrumentType? instrumentType)
    {
        if (instrumentType.HasValue && instrumentType.Value.IsNotIn(
            OkxInstrumentType.Spot,
            OkxInstrumentType.Margin,
            OkxInstrumentType.Swap,
            OkxInstrumentType.Futures))
            throw new ArgumentOutOfRangeException(nameof(instrumentType), "Algo order queries support SPOT, MARGIN, SWAP, and FUTURES only.");
    }
}
