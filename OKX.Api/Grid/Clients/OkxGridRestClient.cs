namespace OKX.Api.Grid;

/// <summary>
/// OKX Rest Api Grid Trading Client
/// </summary>
public class OkxGridRestClient(OkxRestApiClient root) : OkxBaseRestClient(root)
{
    /// <summary>
    /// Place grid algo order
    /// </summary>
    /// <param name="request">Request Object</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxGridPlaceOrderResponse>> PlaceOrderAsync(OkxGridPlaceOrderRequest request, CancellationToken ct = default)
        => PlaceOrderAsync(
        request.InstrumentId,
        request.AlgoOrderType,
        request.MaximumPrice,
        request.MinimumPrice,
        request.GridNumber,
        request.GridRunType,
        request.TakeProfitTriggerPrice,
        request.StopLossTriggerPrice,
        request.AlgoClientOrderId,
        request.ProfitSharingRatio,
        request.TriggerParameters,
        request.QuoteSize,
        request.BaseSize,
        request.Size,
        request.ContractGridDirection,
        request.Leverage,
        request.BasePosition,
        request.TakeProfitRatio,
        request.StopLossRatio,
        request.TradeQuoteCurrency,
        ct);

    /// <summary>
    /// Place grid algo order
    /// </summary>
    /// <param name="instrumentId">Instrument ID, e.g. BTC-USDT-SWAP</param>
    /// <param name="algoOrderType">Algo order type</param>
    /// <param name="maximumPrice">Upper price of price range</param>
    /// <param name="minimumPrice">Lower price of price range</param>
    /// <param name="gridNumber">Grid quantity</param>
    /// <param name="gridRunType">Grid type</param>
    /// <param name="takeProfitTriggerPrice">TP tigger price. Applicable to Spot grid/Contract grid</param>
    /// <param name="stopLossTriggerPrice">SL tigger price. Applicable to Spot grid/Contract grid</param>
    /// <param name="algoClientOrderId">Client-supplied Algo ID. A combination of case-sensitive alphanumerics, all numbers, or all letters of up to 32 characters.</param>
    /// <param name="profitSharingRatio">Profit sharing ratio, it only supports these values. 0.1 represents 10%</param>
    /// <param name="triggerParameters">Trigger Parameters. Applicable to Spot grid/Contract grid</param>
    /// <param name="quoteSize">Invest amount for quote currency. Either quoteSz or baseSz is required</param>
    /// <param name="baseSize">Invest amount for base currency. Either quoteSz or baseSz is required</param>
    /// <param name="size">Used margin based on USDT</param>
    /// <param name="contractGridDirection">Contract grid type</param>
    /// <param name="leverage">Leverage</param>
    /// <param name="basePosition">Whether or not open a position when the strategy activates. Default is false. Neutral contract grid should omit the parameter</param>
    /// <param name="takeProfitRatio">Take profit ratio, 0.1 represents 10%</param>
    /// <param name="stopLossRatio">Stop loss ratio, 0.1 represents 10%</param>
    /// <param name="tradeQuoteCurrency">The quote currency for trading. Only applicable to SPOT. The default value is the quote currency of instId, e.g.USD for BTC-USD.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxGridPlaceOrderResponse>> PlaceOrderAsync(
        string instrumentId,
        OkxGridAlgoOrderType algoOrderType,
        decimal maximumPrice,
        decimal minimumPrice,
        decimal gridNumber,
        OkxGridRunType? gridRunType = null,
        decimal? takeProfitTriggerPrice = null,
        decimal? stopLossTriggerPrice = null,
        string? algoClientOrderId = null,
        decimal? profitSharingRatio = null,
        IEnumerable<OkxGridTriggerParametersRequest>? triggerParameters = null,

        // Spot Grid Order
        decimal? quoteSize = null,
        decimal? baseSize = null,

        // Contract Grid Order
        decimal? size = null,
        OkxGridContractDirection? contractGridDirection = null,
        decimal? leverage = null,
        bool? basePosition = null,
        decimal? takeProfitRatio = null,
        decimal? stopLossRatio = null,

        // Trade Quote Currency
        string? tradeQuoteCurrency = null,

        // Cancellation Token
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            { "instId", instrumentId },
            { "maxPx", maximumPrice.ToOkxString() },
            { "minPx", minimumPrice.ToOkxString() },
            { "gridNum", gridNumber.ToOkxString() },
        };
        parameters.AddEnum("algoOrdType", algoOrderType);
        parameters.AddOptionalEnum("runType", gridRunType);
        parameters.AddOptional("tpTriggerPx", takeProfitTriggerPrice?.ToOkxString());
        parameters.AddOptional("slTriggerPx", stopLossTriggerPrice?.ToOkxString());
        parameters.AddOptional("algoClOrdId", algoClientOrderId);
        parameters.AddOptional("profitSharingRatio", profitSharingRatio?.ToOkxString());
        parameters.AddOptional("triggerParams", triggerParameters);

        // Spot Grid Order
        parameters.AddOptional("quoteSz", quoteSize?.ToOkxString());
        parameters.AddOptional("baseSz", baseSize?.ToOkxString());

        // Contract Grid Order
        parameters.AddOptional("sz", size?.ToOkxString());
        parameters.AddOptionalEnum("direction", contractGridDirection);
        parameters.AddOptional("lever", leverage?.ToOkxString());
        parameters.AddOptional("basePos", basePosition);
        parameters.AddOptional("tpRatio", takeProfitRatio?.ToOkxString());
        parameters.AddOptional("slRatio", stopLossRatio?.ToOkxString());

        // Trade Quote Currency
        parameters.AddOptional("tradeQuoteCcy", tradeQuoteCurrency);

        // Broker ID
        parameters.AddOptional("tag", OkxConstants.BrokerId);

        return ProcessOneRequestAsync<OkxGridPlaceOrderResponse>(GetUri("api/v5/tradingBot/grid/order-algo"), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Amend grid algo order.
    /// Supported contract grid algo order amendment.
    /// </summary>
    /// <param name="request">Request Object</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxGridOrderAmendResponse>> AmendOrderAsync(OkxGridOrderAmendRequest request, CancellationToken ct = default)
        => AmendOrderAsync(
        request.AlgoOrderId,
        request.InstrumentId,
        request.StopLossTriggerPrice,
        request.TakeProfitTriggerPrice,
        request.TakeProfitRatio,
        request.StopLossRatio,
        request.TriggerParameters,
        ct);

    /// <summary>
    /// Amend grid algo order.
    /// Supported contract grid algo order amendment.
    /// </summary>
    /// <param name="algoOrderId">Algo ID</param>
    /// <param name="instrumentId">Instrument ID, e.g. BTC-USDT-SWAP</param>
    /// <param name="stopLossTriggerPrice">SL tigger price. Applicable to Spot grid/Contract grid</param>
    /// <param name="takeProfitTriggerPrice">TP tigger price. Applicable to Spot grid/Contract grid</param>
    /// <param name="takeProfitRatio">Take profit ratio, 0.1 represents 10%, only applicable to contract grid. if it is set "" means take-profit ratio is canceled.</param>
    /// <param name="stopLossRatio">Stop loss ratio, 0.1 represents 10%, only applicable to contract grid`. if it is set "" means stop-loss ratio is canceled.</param>
    /// <param name="triggerParameters">Trigger Parameters. Applicable to Spot grid/Contract grid</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxGridOrderAmendResponse>> AmendOrderAsync(
        long algoOrderId,
        string instrumentId,
        decimal? stopLossTriggerPrice = null,
        decimal? takeProfitTriggerPrice = null,
        decimal? takeProfitRatio = null,
        decimal? stopLossRatio = null,
        IEnumerable<OkxGridOrderAmendRequestTriggerParameters>? triggerParameters = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            { "algoId", algoOrderId.ToOkxString() },
            { "instId", instrumentId },
        };
        parameters.AddOptional("slTriggerPx", stopLossTriggerPrice?.ToOkxString());
        parameters.AddOptional("tpTriggerPx", takeProfitTriggerPrice?.ToOkxString());
        parameters.AddOptional("tpRatio", takeProfitRatio?.ToOkxString());
        parameters.AddOptional("slRatio", stopLossRatio?.ToOkxString());
        parameters.AddOptionalEnum("triggerParams", triggerParameters);

        return ProcessOneRequestAsync<OkxGridOrderAmendResponse>(GetUri("api/v5/tradingBot/grid/amend-order-algo"), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Stop grid algo order. A maximum of 10 orders can be stopped per request.
    /// </summary>
    /// <param name="algoOrderId">Algo ID</param>
    /// <param name="instrumentId">Instrument ID, e.g. BTC-USDT-SWAP</param>
    /// <param name="algoOrderType">Algo order type</param>
    /// <param name="spotAlgoStopType">Spot Grid Algo Stop Type</param>
    /// <param name="contractAlgoStopType">Contract Grid Algo Stop Type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxGridOrderStopResponse>> StopOrderAsync(
        long algoOrderId,
        string instrumentId,
        OkxGridAlgoOrderType algoOrderType,
        OkxGridSpotAlgoStopType? spotAlgoStopType = null,
        OkxGridContractAlgoStopType? contractAlgoStopType = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            { "algoId", algoOrderId.ToOkxString() },
            { "instId", instrumentId },
        };
        parameters.AddEnum("algoOrdType", algoOrderType);
        if (spotAlgoStopType.HasValue) parameters.AddOptionalEnum("stopType", spotAlgoStopType);
        else if (contractAlgoStopType.HasValue) parameters.AddOptionalEnum("stopType", contractAlgoStopType);

        return ProcessOneRequestAsync<OkxGridOrderStopResponse>(GetUri("api/v5/tradingBot/grid/stop-order-algo"), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Close position for contract grid. Close position when the contract grid stop type is 'keep position'.
    /// </summary>
    /// <param name="algoOrderId">Algo ID</param>
    /// <param name="marketClose">Market close all the positions or not. true: Market close all position, false：Close part of position</param>
    /// <param name="size">Close position amount, with unit of contract. If mktClose is false, the parameter is required.</param>
    /// <param name="price">Close position price. If mktClose is false, the parameter is required.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxGridOrderCloseResponse>> ClosePositionAsync(
        long algoOrderId,
        bool marketClose,
        decimal? size = null,
        decimal? price = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            { "algoId", algoOrderId.ToOkxString() },
            { "instId", marketClose },
        };
        parameters.AddOptional("sz", size?.ToOkxString());
        parameters.AddOptional("px", price?.ToOkxString());

        return ProcessOneRequestAsync<OkxGridOrderCloseResponse>(GetUri("api/v5/tradingBot/grid/close-position"), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Cancel close position order for contract grid
    /// </summary>
    /// <param name="algoOrderId">Algo ID</param>
    /// <param name="orderId">Close position order ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxGridOrderCloseResponse>> CancelClosePositionAsync(
        long algoOrderId,
        long orderId,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            { "algoId", algoOrderId.ToOkxString() },
            { "ordId", orderId.ToOkxString() },
        };

        return ProcessOneRequestAsync<OkxGridOrderCloseResponse>(GetUri("api/v5/tradingBot/grid/cancel-close-order"), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Instant trigger grid algo order
    /// </summary>
    /// <param name="algoOrderId">Algo ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxGridOrderTriggerResponse>> TriggerOrderAsync(
        long algoOrderId,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            { "algoId", algoOrderId.ToOkxString() },
        };

        return ProcessOneRequestAsync<OkxGridOrderTriggerResponse>(GetUri("api/v5/tradingBot/grid/order-instant-trigger"), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Get grid algo order list
    /// </summary>
    /// <param name="algoOrderType">Algo order type</param>
    /// <param name="algoOrderId">Algo ID</param>
    /// <param name="instrumentId">Instrument ID, e.g. BTC-USDT-SWAP</param>
    /// <param name="instrumentType"></param>
    /// <param name="after">Pagination of data to return records earlier than the requested algoId.</param>
    /// <param name="before">Pagination of data to return records newer than the requested algoId.</param>
    /// <param name="limit">Number of results per request. The maximum is 100; The default is 100</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxGridOrder>>> GetOpenOrdersAsync(
        OkxGridAlgoOrderType algoOrderType,
        long? algoOrderId = null,
        string? instrumentId = null,
        OkxInstrumentType? instrumentType = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddEnum("algoOrdType", algoOrderType);
        parameters.AddOptionalEnum("instType", instrumentType);
        parameters.AddOptional("algoId", algoOrderId?.ToOkxString());
        parameters.AddOptional("instId", instrumentId);
        parameters.AddOptional("after", after?.ToOkxString());
        parameters.AddOptional("before", before?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxGridOrder>(GetUri("api/v5/tradingBot/grid/orders-algo-pending"), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Get grid algo order history
    /// </summary>
    /// <param name="algoOrderType">Algo order type</param>
    /// <param name="algoOrderId">Algo ID</param>
    /// <param name="instrumentId">Instrument ID, e.g. BTC-USDT-SWAP</param>
    /// <param name="instrumentType"></param>
    /// <param name="after">Pagination of data to return records earlier than the requested algoId.</param>
    /// <param name="before">Pagination of data to return records newer than the requested algoId.</param>
    /// <param name="limit">Number of results per request. The maximum is 100; The default is 100</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxGridOrder>>> GetOrdersHistoryAsync(
        OkxGridAlgoOrderType algoOrderType,
        long? algoOrderId = null,
        string? instrumentId = null,
        OkxInstrumentType? instrumentType = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddEnum("algoOrdType", algoOrderType);
        parameters.AddOptionalEnum("instType", instrumentType);
        parameters.AddOptional("algoId", algoOrderId?.ToOkxString());
        parameters.AddOptional("instId", instrumentId);
        parameters.AddOptional("after", after?.ToOkxString());
        parameters.AddOptional("before", before?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxGridOrder>(GetUri("api/v5/tradingBot/grid/orders-algo-history"), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Get grid algo order details
    /// </summary>
    /// <param name="algoOrderType">Algo order type</param>
    /// <param name="algoOrderId">Algo ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxGridOrder>> GetOrderAsync(
        OkxGridAlgoOrderType algoOrderType,
        long algoOrderId,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            { "algoId", algoOrderId}
        };
        parameters.AddEnum("algoOrdType", algoOrderType);

        return ProcessOneRequestAsync<OkxGridOrder>(GetUri("api/v5/tradingBot/grid/orders-algo-details"), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Get grid algo sub orders
    /// </summary>
    /// <param name="algoOrderType">Algo order type</param>
    /// <param name="algoOrderId">Algo ID</param>
    /// <param name="type">Sub order state</param>
    /// <param name="groupId">Group ID</param>
    /// <param name="after">Pagination of data to return records earlier than the requested algoId.</param>
    /// <param name="before">Pagination of data to return records newer than the requested algoId.</param>
    /// <param name="limit">Number of results per request. The maximum is 100; The default is 100</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxGridAlgoSubOrder>>> GetSubOrdersAsync(
        OkxGridAlgoOrderType algoOrderType,
        long algoOrderId,
        OkxGridAlgoSubOrderType type,
        string? groupId = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            { "algoId", algoOrderId.ToOkxString() },
        };
        parameters.AddEnum("algoOrdType", algoOrderType);
        parameters.AddEnum("type", type);
        parameters.AddOptional("groupId", groupId);
        parameters.AddOptional("after", after?.ToOkxString());
        parameters.AddOptional("before", before?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxGridAlgoSubOrder>(GetUri("api/v5/tradingBot/grid/sub-orders"), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Get grid algo order positions
    /// </summary>
    /// <param name="algoOrderType">Algo order type</param>
    /// <param name="algoOrderId">Algo ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxGridAlgoPosition>>> GetPositionsAsync(
        OkxGridAlgoOrderType algoOrderType,
        long algoOrderId,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            { "algoId", algoOrderId.ToOkxString() }
        };
        parameters.AddEnum("algoOrdType", algoOrderType);

        return ProcessListRequestAsync<OkxGridAlgoPosition>(GetUri("api/v5/tradingBot/grid/positions"), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Spot/Moon grid withdraw income
    /// </summary>
    /// <param name="algoOrderId">Algo ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxGridWithdrawIncome>> WithdrawIncomeAsync(
        long algoOrderId,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            { "algoId", algoOrderId.ToOkxString() }
        };

        return ProcessOneRequestAsync<OkxGridWithdrawIncome>(GetUri("api/v5/tradingBot/grid/withdraw-income"), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Compute margin balance
    /// </summary>
    /// <param name="algoOrderId">Algo ID</param>
    /// <param name="type">Adjust margin balance type</param>
    /// <param name="quantity">Adjust margin balance amount. Default is zero.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxGridComputedMarginBalance>> ComputeMarginBalanceAsync(
        long algoOrderId,
        OkxAccountMarginAddReduce type,
        decimal quantity,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            { "algoId", algoOrderId.ToOkxString() },
            { "amt", quantity.ToOkxString() },
        };
        parameters.AddEnum("type", type);

        return ProcessOneRequestAsync<OkxGridComputedMarginBalance>(GetUri("api/v5/tradingBot/grid/compute-margin-balance"), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Adjust margin balance
    /// </summary>
    /// <param name="algoOrderId">Algo ID</param>
    /// <param name="type">Adjust margin balance type</param>
    /// <param name="quantity">	Adjust margin balance amount. Either quantity or percent is required.</param>
    /// <param name="percent">Adjust margin balance percentage</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxGridMarginOrderResponse>> AdjustMarginBalanceAsync(
        long algoOrderId,
        OkxAccountMarginAddReduce type,
        decimal? quantity = null,
        decimal? percent = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            { "algoId", algoOrderId.ToOkxString() },
        };
        parameters.AddEnum("type", type);
        parameters.AddOptional("amt", quantity?.ToOkxString());
        parameters.AddOptional("percent", percent?.ToOkxString());

        return ProcessOneRequestAsync<OkxGridMarginOrderResponse>(GetUri("api/v5/tradingBot/grid/margin-balance"), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// It is used to add investment and only applicable to contract gird.
    /// </summary>
    /// <param name="algoOrderId">Algo ID</param>
    /// <param name="amount">The amount is going to be added</param>
    /// <param name="allowReinvestProfit">Whether reinvesting profits, only applicable to spot grid. true or false. The default is true.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<long?>> AddInvestmentAsync(long algoOrderId, decimal amount, bool? allowReinvestProfit = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            { "algoId", algoOrderId.ToOkxString() },
            { "amt", amount.ToOkxString() },
        };
        parameters.AddOptional("allowReinvestProfit", allowReinvestProfit);

        var result = await ProcessOneRequestAsync<OkxGridAlgoIdContainer>(GetUri("api/v5/tradingBot/grid/adjust-investment"), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
        if (!result) return new RestCallResult<long?>(result.Request, result.Response, result.Raw ?? "", result.Error);
        return new RestCallResult<long?>(result.Request, result.Response, result.Data.Payload, result.Raw ?? "", result.Error);
    }

    /// <summary>
    /// Get grid AI parameter (public). Authentication is not required for this public endpoint.
    /// </summary>
    /// <param name="algoOrderType">Algo order type</param>
    /// <param name="instrumentId">Instrument ID, e.g. BTC-USDT-SWAP</param>
    /// <param name="direction">Contract grid type. Required in the case of contract_grid</param>
    /// <param name="duration">Back testing duration. The default is 7D for Spot grid,180D for Moon grid. Only 7D is available for Contract grid</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxGridAiParameter>> GetAIParameterAsync(
        OkxGridAlgoOrderType algoOrderType,
        string instrumentId,
        OkxGridContractDirection? direction = null,
        OkxGridBackTestingDuration? duration = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            { "instId", instrumentId },
        };
        parameters.AddEnum("algoOrdType", algoOrderType);
        parameters.AddOptionalEnum("direction", direction);
        parameters.AddOptionalEnum("duration", duration);

        return ProcessOneRequestAsync<OkxGridAiParameter>(GetUri("api/v5/tradingBot/grid/ai-param"), HttpMethod.Get, ct, signed: false, queryParameters: parameters);
    }

    /// <summary>
    /// Compute min investment (public). Authentication is not required for this public endpoint.
    /// </summary>
    /// <param name="instrumentId">Instrument ID, e.g. BTC-USDT-SWAP</param>
    /// <param name="algoOrderType">Algo order type</param>
    /// <param name="maximumPrice">Upper price of price range</param>
    /// <param name="minimumPrice">	Lower price of price range</param>
    /// <param name="gridNumber">Grid quantity</param>
    /// <param name="gridRunType">Grid type</param>
    /// <param name="direction">Contract grid type. Only applicable to contract grid</param>
    /// <param name="leverage">Leverage. Only applicable to contract grid</param>
    /// <param name="basePosition">	Whether or not open a position when the strategy activates. Default is false. Neutral contract grid should omit the parameter. Only applicable to contract grid</param>
    /// <param name="investmentType">Investment type</param>
    /// <param name="triggerStrategy">Trigger stragety</param>
    /// <param name="investmentData">Invest Data</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxGridInvestment>> ComputeMinimumInvestmentAsync(
        string instrumentId,
        OkxGridAlgoOrderType algoOrderType,
        decimal maximumPrice,
        decimal minimumPrice,
        decimal gridNumber,
        OkxGridRunType? gridRunType,
        OkxGridContractDirection? direction = null,
        decimal? leverage = null,
        bool? basePosition = null,
        OkxGridInvestmentType? investmentType = null,
        OkxGridTriggerStrategy? triggerStrategy = null,
        IEnumerable<OkxGridInvestmentData>? investmentData = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            { "instId", instrumentId },
            { "maxPx", maximumPrice.ToOkxString() },
            { "minPx", minimumPrice.ToOkxString() },
            { "gridNum", gridNumber.ToOkxString() },
        };
        parameters.AddEnum("algoOrdType", algoOrderType);
        parameters.AddEnum("runType", gridRunType);
        parameters.AddOptionalEnum("direction", direction);
        parameters.AddOptional("lever", leverage?.ToOkxString());
        parameters.AddOptional("basePos", basePosition);
        parameters.AddOptionalEnum("investmentType", investmentType);
        parameters.AddOptionalEnum("triggerStrategy", triggerStrategy);
        parameters.AddOptional("investmentData", JsonConvert.SerializeObject(investmentData));

        return ProcessOneRequestAsync<OkxGridInvestment>(GetUri("api/v5/tradingBot/grid/min-investment"), HttpMethod.Post, ct, signed: false, bodyParameters: parameters);
    }

    /// <summary>
    /// RSI back testing (public). Authentication is not required for this public endpoint.
    /// </summary>
    /// <param name="instrumentId">Instrument ID, e.g. BTC-USDT-SWAP</param>
    /// <param name="timeframe">K-line type</param>
    /// <param name="threshold">Threshold. The value should be an integer between 1 to 100</param>
    /// <param name="timePeriod">Time Period. Default: 14</param>
    /// <param name="triggerCondition">	Trigger condition</param>
    /// <param name="duration">	Back testing duration.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<long?>> RsiBacktestAsync(
        string instrumentId,
        OkxGridAlgoTimeFrame timeframe,
        decimal threshold,
        int timePeriod,
        OkxGridAlgoTriggerCondition? triggerCondition = null,
        string duration = "1M",
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            { "instId", instrumentId },
            { "thold", threshold.ToOkxString() },
            { "timePeriod", timePeriod.ToOkxString() },
        };
        parameters.AddEnum("timeframe", timeframe);
        parameters.AddOptionalEnum("triggerCond", triggerCondition);
        parameters.AddOptional("duration", duration);

        var result = await ProcessOneRequestAsync<OkxGridTriggerNumberContainer>(GetUri("api/v5/tradingBot/public/rsi-back-testing"), HttpMethod.Get, ct, signed: false, queryParameters: parameters);
        if (!result) return new RestCallResult<long?>(result.Request, result.Response, result.Raw ?? "", result.Error);
        return new RestCallResult<long?>(result.Request, result.Response, result.Data.Payload, result.Raw ?? "", result.Error);
    }

    /// <summary>
    /// Authentication is not required for this public endpoint.
    /// Maximum grid quantity can be retrieved from this endpoint.Minimum grid quantity always is 2.
    /// </summary>
    /// <param name="instrumentId">Instrument ID, e.g. BTC-USDT</param>
    /// <param name="runType">Grid type</param>
    /// <param name="algoOrderType">Algo order type</param>
    /// <param name="maxPrice">Upper price of price range</param>
    /// <param name="minPrice">Lower price of price range</param>
    /// <param name="leverage">Leverage, it is required for contract grid</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<decimal?>> GetMaximumGridQuantityAsync(
        string instrumentId,
        OkxGridRunType runType,
        OkxGridAlgoOrderType algoOrderType,
        decimal maxPrice,
        decimal minPrice,
        decimal? leverage,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            { "instId", instrumentId },
            { "maxPx", maxPrice.ToOkxString() },
            { "minPx", minPrice.ToOkxString() },
        };
        parameters.AddEnum("runType", runType);
        parameters.AddEnum("algoOrdType", algoOrderType);
        parameters.AddOptional("lever", leverage);

        var result = await ProcessOneRequestAsync<OkxGridMaxQuantityContainer>(GetUri("api/v5/tradingBot/grid/grid-quantity"), HttpMethod.Get, ct, signed: false, queryParameters: parameters);
        if (!result) return new RestCallResult<decimal?>(result.Request, result.Response, result.Raw ?? "", result.Error);
        return new RestCallResult<decimal?>(result.Request, result.Response, result.Data.Payload, result.Raw ?? "", result.Error);
    }
}