namespace OKX.Api.RecurringBuy;

/// <summary>
/// OKX Rest Api Recurring Buy Client
/// </summary>
public class OkxRecurringBuyRestClient(OkxRestApiClient root) : OkxBaseRestClient(root)
{
    // Endpoints
    private const string v5TradingBotRecurringOrderAlgo = "api/v5/tradingBot/recurring/order-algo";
    private const string v5TradingBotRecurringAmendOrderAlgo = "api/v5/tradingBot/recurring/amend-order-algo";
    private const string v5TradingBotRecurringStopOrderAlgo = "api/v5/tradingBot/recurring/stop-order-algo";
    private const string v5TradingBotRecurringOrdersAlgoPending = "api/v5/tradingBot/recurring/orders-algo-pending";
    private const string v5TradingBotRecurringOrdersAlgoHistory = "api/v5/tradingBot/recurring/orders-algo-history";
    private const string v5TradingBotRecurringOrdersAlgoDetails = "api/v5/tradingBot/recurring/orders-algo-details";
    private const string v5TradingBotRecurringSubOrders = "api/v5/tradingBot/recurring/sub-orders";

    /// <summary>
    /// Place recurring buy order
    /// </summary>
    /// <param name="strategyName">Custom name for trading bot, no more than 40 characters</param>
    /// <param name="recurringList">Recurring buy info</param>
    /// <param name="period">Period</param>
    /// <param name="recurringDay">Recurring buy date
    /// When the period is monthly, the value range is an integer of [1,28]
    /// When the period is weekly, the value range is an integer of [1,7]
    /// When the period is daily/hourly, the parameter is not required.</param>
    /// <param name="recurringHour">Recurring buy by hourly
    /// 1/4/8/12, e.g. 4 represents "recurring buy every 4 hour"
    /// When the period is hourly, the parameter is required.</param>
    /// <param name="recurringTime">Recurring buy time, the value range is an integer of [0,23]
    /// When the period is hourly, the parameter is the time of the first investment occurs.</param>
    /// <param name="timeZone">UTC time zone, the value range is an integer of [-12,14]
    /// e.g. "8" representing UTC+8 (East 8 District), Beijing Time</param>
    /// <param name="amount">Quantity invested per cycle</param>
    /// <param name="investmentCurrency">The invested quantity unit, can only be USDT/USDC</param>
    /// <param name="tradeMode">Trading mode
    /// Margin mode: cross
    /// Non-Margin mode: cash</param>
    /// <param name="clientOrderId">Client-supplied Algo ID
    /// There will be a value when algo order attaching algoClOrdId is triggered, or it will be "".
    /// A combination of case-sensitive alphanumerics, all numbers, or all letters of up to 32 characters.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxRecurringBuyOrderResponse>> PlaceOrderAsync(
       string strategyName,
       IEnumerable<OkxRecurringBuyItem> recurringList,
       OkxRecurringBuyPeriod period,
       int? recurringDay,
       int? recurringHour,
       int recurringTime,
       string timeZone,
       decimal amount,
       string investmentCurrency,
       OkxTradeMode tradeMode,
       string? clientOrderId = null,
       CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            {"stgyName", strategyName },
            {"recurringList", recurringList },
            {"timeZone", timeZone },
            {"amt", amount.ToOkxString() },
            {"investmentCcy", investmentCurrency },
        };
        parameters.AddEnum("period", period);
        parameters.AddEnum("tdMode", tradeMode);
        parameters.AddOptional("recurringDay", recurringDay?.ToOkxString());
        parameters.AddOptional("recurringHour", recurringHour?.ToOkxString());
        parameters.AddOptional("recurringTime", recurringTime.ToOkxString());
        parameters.AddOptional("algoClOrdId", clientOrderId);
        parameters.AddOptional("tag", OkxConstants.BrokerId);

        return ProcessOneRequestAsync<OkxRecurringBuyOrderResponse>(GetUri(v5TradingBotRecurringOrderAlgo), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }
    
    /// <summary>
    /// Amend recurring buy order
    /// </summary>
    /// <param name="algoOrderId">Algo ID</param>
    /// <param name="strategyName">New custom name for trading bot after adjustment, no more than 40 characters</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxRecurringBuyOrderResponse>> AmendOrderAsync(long algoOrderId, string strategyName, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            {"algoId", algoOrderId.ToOkxString() },
            {"stgyName", strategyName },
        };

        return ProcessOneRequestAsync<OkxRecurringBuyOrderResponse>(GetUri(v5TradingBotRecurringAmendOrderAlgo), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Stop recurring buy order
    /// </summary>
    /// <param name="algoOrderId">Algo ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxRecurringBuyOrderResponse>> StopOrderAsync(long algoOrderId, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            {"algoId", algoOrderId.ToOkxString() },
        };

        return ProcessOneRequestAsync<OkxRecurringBuyOrderResponse>(GetUri(v5TradingBotRecurringStopOrderAlgo), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }
    
    /// <summary>
    /// Recurring buy order list
    /// </summary>
    /// <param name="algoOrderId">Algo ID</param>
    /// <param name="after">Pagination of data to return records earlier than the requested algoId.</param>
    /// <param name="before">Pagination of data to return records newer than the requested algoId.</param>
    /// <param name="limit">Number of results per request. The maximum is 100. The default is 100</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxRecurringBuyOrder>>> GetOpenOrdersAsync(
        long? algoOrderId = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new ParameterCollection();
        parameters.AddOptional("algoId", algoOrderId?.ToOkxString());
        parameters.AddOptional("after", after?.ToOkxString());
        parameters.AddOptional("before", before?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxRecurringBuyOrder>(GetUri(v5TradingBotRecurringOrdersAlgoPending), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }
    
    /// <summary>
    /// Recurring buy order history
    /// </summary>
    /// <param name="algoOrderId">Algo ID</param>
    /// <param name="after">Pagination of data to return records earlier than the requested algoId.</param>
    /// <param name="before">Pagination of data to return records newer than the requested algoId.</param>
    /// <param name="limit">Number of results per request. The maximum is 100. The default is 100</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxRecurringBuyOrder>>> GetOrderHistoryAsync(
        long? algoOrderId = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new ParameterCollection();
        parameters.AddOptional("algoId", algoOrderId?.ToOkxString());
        parameters.AddOptional("after", after?.ToOkxString());
        parameters.AddOptional("before", before?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxRecurringBuyOrder>(GetUri(v5TradingBotRecurringOrdersAlgoHistory), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Recurring buy order details
    /// </summary>
    /// <param name="algoOrderId">Algo ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxRecurringBuyOrderDetails>> GetOrderAsync(long algoOrderId, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("algoId", algoOrderId.ToOkxString());

        return ProcessOneRequestAsync<OkxRecurringBuyOrderDetails>(GetUri(v5TradingBotRecurringOrdersAlgoDetails), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Recurring buy sub orders
    /// </summary>
    /// <param name="algoOrderId">Algo ID</param>
    /// <param name="subOrderId">Sub order ID</param>
    /// <param name="after">Pagination of data to return records earlier than the requested algoId.</param>
    /// <param name="before">Pagination of data to return records newer than the requested algoId.</param>
    /// <param name="limit">Number of results per request. The maximum is 100. The default is 100</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxRecurringBuySubOrder>>> GetSubOrdersAsync(
        long algoOrderId,
        long? subOrderId = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new ParameterCollection();
        parameters.AddOptional("algoId", algoOrderId.ToOkxString());
        parameters.AddOptional("ordId", subOrderId?.ToOkxString());
        parameters.AddOptional("after", after?.ToOkxString());
        parameters.AddOptional("before", before?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxRecurringBuySubOrder>(GetUri(v5TradingBotRecurringSubOrders), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

}