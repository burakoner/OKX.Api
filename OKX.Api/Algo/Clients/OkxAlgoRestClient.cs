using System.Threading.Tasks;
using static ApiSharp.Security.Cryptology;

namespace OKX.Api.Algo;

/// <summary>
/// OKX Rest Api Algo Trading Client
/// </summary>
public class OkxAlgoRestClient(OkxRestApiClient root) : OkxBaseRestClient(root)
{
    private const string v5TradeOrderAlgo = "api/v5/trade/order-algo";
    private const string v5TradeCancelAlgos = "api/v5/trade/cancel-algos";
    private const string v5TradeAmendAlgos = "api/v5/trade/amend-algos";
    private const string v5TradeCancelAdvanceAlgos = "api/v5/trade/cancel-advance-algos";
    private const string v5TradeOrderAlgoGet = "api/v5/trade/order-algo";
    private const string v5TradeOrdersAlgoPending = "api/v5/trade/orders-algo-pending";
    private const string v5TradeOrdersAlgoHistory = "api/v5/trade/orders-algo-history";

    /// <summary>
    /// The algo order includes trigger order, oco order, conditional order,iceberg order and twap order.
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
    /// <param name="closeFraction">Fraction of position to be closed when the algo order is triggered.
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
    /// <param name="reduceOnly">Reduce Only</param>
    /// manual, auto_borrow, auto_repay
    /// The default value is manual</param>
    /// 
    /// <param name="triggerPrice">Trigger Price</param>
    /// <param name="orderPrice">Order Price</param>
    /// <param name="triggerPriceType">Trigger Price Type</param>
    /// <param name="attachedAlgoOrders">Attached SL/TP orders info. Applicable to Spot and futures mode/Multi-currency margin/Portfolio margin</param>
    /// 
    /// <param name="callbackRatio">Callback price ratio , e.g. 0.01</param>
    /// <param name="callbackSpread">Callback price variance</param>
    /// <param name="activePrice">Active price</param>
    /// 
    /// <param name="priceVariance">Price Variance</param>
    /// <param name="priceSpread">Price Spread</param>
    /// <param name="sizeLimit">Size Limit</param>
    /// <param name="priceLimit">Price Limit</param>
    /// 
    /// <param name="timeInterval">Time Interval
    /// Currently the system supports fully closing the position only so the only accepted value is 1.
    /// This is only applicable to FUTURES or SWAP instruments.
    /// This is only applicable if posSide is net.
    /// This is only applicable if reduceOnly is true.
    /// This is only applicable if ordType is conditional or oco.
    /// This is only applicable if the stop loss and take profit order is executed as market order
    /// Either sz or closeFraction is required.</param>
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

        // Trigger Order
        decimal? triggerPrice = null,
        decimal? orderPrice = null,
        OkxAlgoPriceType? triggerPriceType = null,
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

        // Chase Order
        OkxAlgoChaseType? chaseType = null,
        decimal? chaseValue = null,
        OkxAlgoChaseType? maxChaseType = null,
        decimal? maxChaseValue = null,

        // Cancellation Token
        CancellationToken ct = default)
    {
        // Common
        var parameters = new ParameterCollection {
            {"instId", instrumentId },
        };
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

        // Trigger Order
        parameters.AddOptional("triggerPx", triggerPrice?.ToOkxString());
        parameters.AddOptional("orderPx", orderPrice?.ToOkxString());
        parameters.AddOptionalEnum("triggerPxType", triggerPriceType);
        parameters.AddOptional("attachAlgoOrds", attachedAlgoOrders);

        // Trailing Stop Order
        parameters.AddOptional("callbackRatio", callbackRatio?.ToOkxString());
        parameters.AddOptional("callbackSpread", callbackSpread?.ToOkxString());
        parameters.AddOptional("activePx", activePrice?.ToOkxString());

        // TWAP Order
        parameters.AddOptionalEnum("pxVar", priceVariance);
        parameters.AddOptional("pxSpread", priceSpread?.ToOkxString());
        parameters.AddOptional("szLimit", sizeLimit?.ToOkxString());
        parameters.AddOptional("pxLimit", priceLimit?.ToOkxString());
        parameters.AddOptional("timeInterval", timeInterval?.ToOkxString());

        // Chase Order
        parameters.AddOptionalEnum("chaseType", chaseType);
        parameters.AddOptional("chaseVal", chaseValue?.ToOkxString());
        parameters.AddOptionalEnum("maxChaseType", maxChaseType);
        parameters.AddOptional("maxChaseVal", maxChaseValue?.ToOkxString());

        // Broker Id
        parameters.AddOptional("tag", OkxConstants.BrokerId);

        // Reequest
        return ProcessOneRequestAsync<OkxAlgoOrderResponse>(GetUri(v5TradeOrderAlgo), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
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

        return ProcessListRequestAsync<OkxAlgoCancelOrderResponse>(GetUri(v5TradeCancelAlgos), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Amend unfilled algo orders (Support stop order only, not including Move_order_stop order, Trigger order, Iceberg order, TWAP order, Trailing Stop order).
    /// Only applicable to Futures and Perpetual swap.
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
    /// <param name="attachedAlgoOrders">Attached SL/TP orders info. Applicable to Spot and futures mode/Multi-currency margin/Portfolio margin</param>
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
        decimal? newTriggerPrice = null,
        decimal? newOrderPrice = null,
        OkxAlgoPriceType? newTriggerPriceType = null,
        IEnumerable<OkxAlgoAttachedAlgoAmendRequest>? attachedAlgoOrders = null,

        // Cancellation Token
        CancellationToken ct = default)
    {
        // Common
        var parameters = new ParameterCollection {
            {"instId", instrumentId },
        };
        parameters.AddOptional("algoId", algoOrderId);
        parameters.AddOptional("algoClOrdId", algoClientOrderId);
        parameters.AddOptional("cxlOnFail", cancelOnFail);
        parameters.AddOptional("reqId", clientRequestId);
        parameters.AddOptional("newSz", newSize);

        // Take Profit
        parameters.AddOptionalEnum("newTpTriggerPxType", newTakeProfitTriggerPriceType);
        parameters.AddOptional("newTpTriggerPx", newTakeProfitTriggerPrice?.ToOkxString());
        parameters.AddOptional("newTpOrdPx", newTakeProfitOrderPrice?.ToOkxString());

        // Stop Loss
        parameters.AddOptionalEnum("newSlTriggerPxType", newStopLossTriggerPriceType);
        parameters.AddOptional("newSlTriggerPx", newStopLossTriggerPrice?.ToOkxString());
        parameters.AddOptional("newSlOrdPx", newStopLossOrderPrice?.ToOkxString());
        
        // Trigger Order
        parameters.AddOptional("newTriggerPx", newTriggerPrice?.ToOkxString());
        parameters.AddOptional("newOrdPx", newOrderPrice?.ToOkxString());
        parameters.AddOptionalEnum("newTriggerPxType", newTriggerPriceType);
        parameters.AddOptional("attachAlgoOrds", attachedAlgoOrders);

        // Reequest
        return ProcessOneRequestAsync<OkxAlgoAmendOrderResponse>(GetUri(v5TradeAmendAlgos), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Cancel unfilled algo orders(iceberg order and twap order). A maximum of 10 orders can be canceled at a time. Request parameters should be passed in the form of an array.
    /// </summary>
    /// <param name="orders">Orders</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxAlgoCancelOrderResponse>>> CancelAdvanceAsync(IEnumerable<OkxAlgoCancelOrderRequest> orders, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.SetBody(orders);

        return ProcessListRequestAsync<OkxAlgoCancelOrderResponse>(GetUri(v5TradeCancelAdvanceAlgos), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
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
        var parameters = new ParameterCollection();
        parameters.AddOptional("algoId", algoOrderId?.ToOkxString());
        parameters.AddOptional("algoClOrdId", algoClientOrderId);

        return ProcessOneRequestAsync<OkxAlgoOrder>(GetUri(v5TradeOrderAlgoGet), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

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
    public Task<RestCallResult<List<OkxAlgoOrder>>> GetOpenOrdersAsync(
        OkxAlgoOrderType algoOrderType,
        long? algoId = null,
        string? algoClientOrderId = null,
        OkxInstrumentType? instrumentType = null,
        string? instrumentId = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new ParameterCollection();
        parameters.AddEnum("ordType", algoOrderType);
        parameters.AddOptional("algoId", algoId?.ToOkxString());
        parameters.AddOptional("algoClOrdId", algoClientOrderId);
        parameters.AddOptionalEnum("instType", instrumentType);
        parameters.AddOptional("instId", instrumentId);
        parameters.AddOptional("after", after?.ToOkxString());
        parameters.AddOptional("before", before?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxAlgoOrder>(GetUri(v5TradeOrdersAlgoPending), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
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
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new ParameterCollection();
        parameters.AddEnum("ordType", algoOrderType);
        parameters.AddOptional("algoId", algoId?.ToOkxString());
        parameters.AddOptional("instId", instrumentId);
        parameters.AddOptional("after", after?.ToOkxString());
        parameters.AddOptional("before", before?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());
        parameters.AddOptionalEnum("state", algoOrderState);
        parameters.AddOptionalEnum("instType", instrumentType);

        return ProcessListRequestAsync<OkxAlgoOrder>(GetUri(v5TradeOrdersAlgoHistory), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }
}
