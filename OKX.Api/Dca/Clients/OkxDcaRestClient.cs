namespace OKX.Api.Dca;

/// <summary>
/// OKX Rest Api DCA Trading Client
/// </summary>
public class OkxDcaRestClient(OkxRestApiClient root) : OkxBaseRestClient(root)
{
    /// <summary>
    /// POST / Place dca algo order
    /// </summary>
    /// <param name="request">Request object</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxDcaOrderResponse>> PlaceOrderAsync(OkxDcaCreateOrderRequest request, CancellationToken ct = default)
        => PlaceOrderAsync(
            request.InstrumentId,
            request.AlgoOrderType,
            request.InitialOrderAmount,
            request.MaximumSafetyOrders,
            request.TakeProfitTarget,
            request.TriggerParameters,
            request.AllowReinvest,
            request.SafetyOrderAmount,
            request.PriceStepRatio,
            request.PriceStepMultiplier,
            request.VolumeMultiplier,
            request.StopLossTarget,
            request.StopLossMode,
            request.Direction,
            request.Leverage,
            request.ProfitSharingRatio,
            request.TrackingMode,
            request.AlgoClientOrderId,
            request.TradeQuoteCurrency,
            ct);

    /// <summary>
    /// POST / Place dca algo order
    /// </summary>
    /// <param name="instrumentId">Instrument ID, e.g. BTC-USDT</param>
    /// <param name="algoOrderType">Algo order type</param>
    /// <param name="initialOrderAmount">Initial order amount</param>
    /// <param name="maximumSafetyOrders">Max number of safety orders</param>
    /// <param name="takeProfitTarget">Take-profit target per cycle. 0.05 represents 5%</param>
    /// <param name="triggerParameters">Trigger parameters</param>
    /// <param name="allowReinvest">Whether to reinvest profit. Only applicable to contract DCA</param>
    /// <param name="safetyOrderAmount">Safety order amount</param>
    /// <param name="priceStepRatio">Safety order price step</param>
    /// <param name="priceStepMultiplier">Price step multiplier</param>
    /// <param name="volumeMultiplier">Safety order amount multiplier</param>
    /// <param name="stopLossTarget">Stop-loss target. 0.05 represents 5%</param>
    /// <param name="stopLossMode">Stop-loss mode</param>
    /// <param name="direction">Contract DCA type</param>
    /// <param name="leverage">Leverage. Only applicable to contract DCA</param>
    /// <param name="profitSharingRatio">Lead trader profit sharing ratio. Only applicable to contract DCA</param>
    /// <param name="trackingMode">Tracking mode. Only applicable to contract DCA</param>
    /// <param name="algoClientOrderId">Client-supplied Algo ID</param>
    /// <param name="tradeQuoteCurrency">Quote currency for trading. Only applicable to spot DCA</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxDcaOrderResponse>> PlaceOrderAsync(
        string instrumentId,
        OkxDcaAlgoOrderType algoOrderType,
        decimal initialOrderAmount,
        int maximumSafetyOrders,
        decimal takeProfitTarget,
        IEnumerable<OkxDcaTriggerParametersRequest> triggerParameters,
        bool? allowReinvest = null,
        decimal? safetyOrderAmount = null,
        decimal? priceStepRatio = null,
        decimal? priceStepMultiplier = null,
        decimal? volumeMultiplier = null,
        decimal? stopLossTarget = null,
        OkxDcaStopLossMode? stopLossMode = null,
        OkxDcaDirection? direction = null,
        decimal? leverage = null,
        decimal? profitSharingRatio = null,
        OkxDcaTrackingMode? trackingMode = null,
        string? algoClientOrderId = null,
        string? tradeQuoteCurrency = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            { "instId", instrumentId },
            { "initOrdAmt", initialOrderAmount.ToOkxString() },
            { "maxSafetyOrds", maximumSafetyOrders.ToOkxString() },
            { "tpPct", takeProfitTarget.ToOkxString() },
            { "triggerParams", triggerParameters },
        };
        parameters.AddEnum("algoOrdType", algoOrderType);
        parameters.AddOptional("allowReinvest", allowReinvest);
        parameters.AddOptional("safetyOrdAmt", safetyOrderAmount?.ToOkxString());
        parameters.AddOptional("pxSteps", priceStepRatio?.ToOkxString());
        parameters.AddOptional("pxStepsMult", priceStepMultiplier?.ToOkxString());
        parameters.AddOptional("volMult", volumeMultiplier?.ToOkxString());
        parameters.AddOptional("slPct", stopLossTarget?.ToOkxString());
        parameters.AddOptionalEnum("slMode", stopLossMode);
        parameters.AddOptionalEnum("direction", direction);
        parameters.AddOptional("lever", leverage?.ToOkxString());
        parameters.AddOptional("profitSharingRatio", profitSharingRatio?.ToOkxString());
        parameters.AddOptionalEnum("trackingMode", trackingMode);
        parameters.AddOptional("algoClOrdId", algoClientOrderId);
        parameters.AddOptional("tradeQuoteCcy", tradeQuoteCurrency);
        parameters.AddOptional("tag", OkxConstants.BrokerId);

        return ProcessOneRequestAsync<OkxDcaOrderResponse>(GetUri("api/v5/tradingBot/dca/create"), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// POST / Amend spot dca basic param
    /// </summary>
    /// <param name="request">Request object</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxDcaOrderResponse>> AmendOrderAsync(OkxDcaAmendOrderRequest request, CancellationToken ct = default)
        => AmendOrderAsync(
            request.AlgoOrderId,
            request.PriceStepRatio,
            request.PriceStepMultiplier,
            request.VolumeMultiplier,
            request.TakeProfitTarget,
            request.StopLossTarget,
            request.InitialOrderAmount,
            request.SafetyOrderAmount,
            request.MaximumSafetyOrders,
            request.ReserveFunds,
            request.TriggerParameters,
            ct);

    /// <summary>
    /// POST / Amend spot dca basic param
    /// </summary>
    /// <param name="algoOrderId">Algo order ID</param>
    /// <param name="priceStepRatio">Price step ratio</param>
    /// <param name="priceStepMultiplier">Price step multiplier</param>
    /// <param name="volumeMultiplier">Amount multiplier</param>
    /// <param name="takeProfitTarget">Take-profit target</param>
    /// <param name="stopLossTarget">Stop-loss target</param>
    /// <param name="initialOrderAmount">Initial order amount</param>
    /// <param name="safetyOrderAmount">Safety order amount</param>
    /// <param name="maximumSafetyOrders">Maximum number of safety orders</param>
    /// <param name="reserveFunds">Whether to reserve all funds</param>
    /// <param name="triggerParameters">Signal trigger parameters</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxDcaOrderResponse>> AmendOrderAsync(
        long algoOrderId,
        decimal priceStepRatio,
        decimal priceStepMultiplier,
        decimal volumeMultiplier,
        decimal takeProfitTarget,
        decimal stopLossTarget,
        decimal initialOrderAmount,
        decimal safetyOrderAmount,
        int maximumSafetyOrders,
        bool reserveFunds,
        IEnumerable<OkxDcaTriggerParametersRequest> triggerParameters,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            { "algoId", algoOrderId.ToOkxString() },
            { "pxSteps", priceStepRatio.ToOkxString() },
            { "pxStepsMult", priceStepMultiplier.ToOkxString() },
            { "volMult", volumeMultiplier.ToOkxString() },
            { "tpPct", takeProfitTarget.ToOkxString() },
            { "slPct", stopLossTarget.ToOkxString() },
            { "initOrdAmt", initialOrderAmount.ToOkxString() },
            { "safetyOrdAmt", safetyOrderAmount.ToOkxString() },
            { "maxSafetyOrds", maximumSafetyOrders.ToOkxString() },
            { "reserveFunds", reserveFunds },
            { "triggerParams", triggerParameters },
        };

        return ProcessOneRequestAsync<OkxDcaOrderResponse>(GetUri("api/v5/tradingBot/dca/amend-order-algo"), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// POST / Stop dca algo order
    /// </summary>
    /// <param name="algoOrderId">Algo order ID</param>
    /// <param name="algoOrderType">Algo order type</param>
    /// <param name="stopType">Stop type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxDcaOrderResponse>> StopOrderAsync(
        long algoOrderId,
        OkxDcaAlgoOrderType algoOrderType,
        OkxDcaStopType stopType,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            { "algoId", algoOrderId.ToOkxString() },
        };
        parameters.AddEnum("algoOrdType", algoOrderType);
        parameters.AddEnum("stopType", stopType);

        return ProcessOneRequestAsync<OkxDcaOrderResponse>(GetUri("api/v5/tradingBot/dca/stop"), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// GET / DCA algo order details
    /// </summary>
    /// <param name="algoOrderId">Algo order ID</param>
    /// <param name="algoOrderType">Algo order type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxDcaOrder>> GetOrderAsync(
        long algoOrderId,
        OkxDcaAlgoOrderType algoOrderType,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            { "algoId", algoOrderId.ToOkxString() },
        };
        parameters.AddEnum("algoOrdType", algoOrderType);

        return ProcessOneRequestAsync<OkxDcaOrder>(GetUri("api/v5/tradingBot/dca/ongoing-list"), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// GET / DCA algo order details
    /// </summary>
    /// <param name="algoOrderType">Algo order type</param>
    /// <param name="algoOrderId">Algo order ID</param>
    /// <param name="after">Pagination of data to return records earlier than the requested algoId</param>
    /// <param name="before">Pagination of data to return records newer than the requested algoId</param>
    /// <param name="limit">Number of results per request. The maximum is 100. The default is 100</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxDcaOrder>>> GetOpenOrdersAsync(
        OkxDcaAlgoOrderType algoOrderType,
        long? algoOrderId = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);

        var parameters = new ParameterCollection();
        parameters.AddEnum("algoOrdType", algoOrderType);
        parameters.AddOptional("algoId", algoOrderId?.ToOkxString());
        parameters.AddOptional("after", after?.ToOkxString());
        parameters.AddOptional("before", before?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxDcaOrder>(GetUri("api/v5/tradingBot/dca/ongoing-list"), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// GET / DCA algo order history
    /// </summary>
    /// <param name="algoOrderType">Algo order type</param>
    /// <param name="algoOrderId">Algo order ID</param>
    /// <param name="after">Pagination of data to return records earlier than the requested algoId</param>
    /// <param name="before">Pagination of data to return records newer than the requested algoId</param>
    /// <param name="limit">Number of results per request. The maximum is 100. The default is 100</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxDcaOrder>>> GetOrderHistoryAsync(
        OkxDcaAlgoOrderType algoOrderType,
        long? algoOrderId = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);

        var parameters = new ParameterCollection();
        parameters.AddEnum("algoOrdType", algoOrderType);
        parameters.AddOptional("algoId", algoOrderId?.ToOkxString());
        parameters.AddOptional("after", after?.ToOkxString());
        parameters.AddOptional("before", before?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxDcaOrder>(GetUri("api/v5/tradingBot/dca/history-list"), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// GET / DCA sub orders
    /// </summary>
    /// <param name="algoOrderId">Algo order ID</param>
    /// <param name="algoOrderType">Algo order type</param>
    /// <param name="cycleId">Cycle ID</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ordId</param>
    /// <param name="before">Pagination of data to return records newer than the requested ordId</param>
    /// <param name="limit">Number of results per request. The maximum is 100. The default is 100</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxDcaSubOrder>>> GetSubOrdersAsync(
        long algoOrderId,
        OkxDcaAlgoOrderType algoOrderType,
        long? cycleId = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);

        var parameters = new ParameterCollection {
            { "algoId", algoOrderId.ToOkxString() },
        };
        parameters.AddEnum("algoOrdType", algoOrderType);
        parameters.AddOptional("cycleId", cycleId?.ToOkxString());
        parameters.AddOptional("after", after?.ToOkxString());
        parameters.AddOptional("before", before?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxDcaSubOrder>(GetUri("api/v5/tradingBot/dca/orders"), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// POST / Add investment
    /// </summary>
    /// <param name="algoOrderId">Algo order ID</param>
    /// <param name="algoOrderType">Algo order type</param>
    /// <param name="price">Price</param>
    /// <param name="amount">Amount</param>
    /// <param name="orderType">Order type. Only applicable to spot DCA</param>
    /// <param name="tradeQuoteCurrency">Quote currency for trading. Only applicable to spot DCA</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxDcaOrderResponse>> ManualBuyAsync(
        long algoOrderId,
        OkxDcaAlgoOrderType algoOrderType,
        decimal price,
        decimal amount,
        OkxTradeOrderType? orderType = null,
        string? tradeQuoteCurrency = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            { "algoId", algoOrderId.ToOkxString() },
            { "price", price.ToOkxString() },
            { "amt", amount.ToOkxString() },
        };
        parameters.AddEnum("algoOrdType", algoOrderType);
        parameters.AddOptionalEnum("ordType", orderType);
        parameters.AddOptional("tradeQuoteCcy", tradeQuoteCurrency);
        parameters.AddOptional("tag", OkxConstants.BrokerId);

        return ProcessOneRequestAsync<OkxDcaOrderResponse>(GetUri("api/v5/tradingBot/dca/orders/manual-buy"), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// POST / Amend dca reinvestment
    /// </summary>
    /// <param name="algoOrderId">Algo order ID</param>
    /// <param name="algoOrderType">Algo order type</param>
    /// <param name="allowReinvest">Whether to reinvest profit</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxDcaOrderResponse>> AmendReinvestmentAsync(
        long algoOrderId,
        OkxDcaAlgoOrderType algoOrderType,
        bool allowReinvest,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            { "algoId", algoOrderId.ToOkxString() },
            { "allowReinvest", allowReinvest },
        };
        parameters.AddEnum("algoOrdType", algoOrderType);

        return ProcessOneRequestAsync<OkxDcaOrderResponse>(GetUri("api/v5/tradingBot/dca/settings/reinvestment"), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// POST / Amend dca take profit settings
    /// </summary>
    /// <param name="algoOrderId">Algo order ID</param>
    /// <param name="algoOrderType">Algo order type</param>
    /// <param name="takeProfitPrice">Take-profit price</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxDcaOrderResponse>> AmendTakeProfitAsync(
        long algoOrderId,
        OkxDcaAlgoOrderType algoOrderType,
        decimal takeProfitPrice,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            { "algoId", algoOrderId.ToOkxString() },
            { "tpPrice", takeProfitPrice.ToOkxString() },
        };
        parameters.AddEnum("algoOrdType", algoOrderType);

        return ProcessOneRequestAsync<OkxDcaOrderResponse>(GetUri("api/v5/tradingBot/dca/settings/take-profit"), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Get / DCA algo order position details
    /// </summary>
    /// <param name="algoOrderId">Algo order ID</param>
    /// <param name="algoOrderType">Algo order type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxDcaPositionDetails>> GetPositionDetailsAsync(
        long algoOrderId,
        OkxDcaAlgoOrderType algoOrderType,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            { "algoId", algoOrderId.ToOkxString() },
        };
        parameters.AddEnum("algoOrdType", algoOrderType);

        return ProcessOneRequestAsync<OkxDcaPositionDetails>(GetUri("api/v5/tradingBot/dca/position-details"), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// GET / DCA cycle list
    /// </summary>
    /// <param name="algoOrderId">Algo order ID</param>
    /// <param name="algoOrderType">Algo order type</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="after">Pagination of data to return records earlier than the requested cycleId</param>
    /// <param name="before">Pagination of data to return records newer than the requested cycleId</param>
    /// <param name="limit">Number of results per request. The maximum is 100. The default is 100</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxDcaCycle>>> GetCyclesAsync(
        long algoOrderId,
        OkxDcaAlgoOrderType algoOrderType,
        string? instrumentId = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);

        var parameters = new ParameterCollection {
            { "algoId", algoOrderId.ToOkxString() },
        };
        parameters.AddEnum("algoOrdType", algoOrderType);
        parameters.AddOptional("instId", instrumentId);
        parameters.AddOptional("after", after?.ToOkxString());
        parameters.AddOptional("before", before?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxDcaCycle>(GetUri("api/v5/tradingBot/dca/cycle-list"), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// POST / Add dca margin
    /// </summary>
    /// <param name="algoOrderId">Algo order ID</param>
    /// <param name="amount">Margin amount</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxDcaOrderResponse>> AddMarginAsync(long algoOrderId, decimal amount, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            { "algoId", algoOrderId.ToOkxString() },
            { "amt", amount.ToOkxString() },
        };

        return ProcessOneRequestAsync<OkxDcaOrderResponse>(GetUri("api/v5/tradingBot/dca/margin/add"), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// POST / Reduce dca margin
    /// </summary>
    /// <param name="algoOrderId">Algo order ID</param>
    /// <param name="amount">Margin amount</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxDcaOrderResponse>> ReduceMarginAsync(long algoOrderId, decimal amount, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            { "algoId", algoOrderId.ToOkxString() },
            { "amt", amount.ToOkxString() },
        };

        return ProcessOneRequestAsync<OkxDcaOrderResponse>(GetUri("api/v5/tradingBot/dca/margin/reduce"), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }
}
