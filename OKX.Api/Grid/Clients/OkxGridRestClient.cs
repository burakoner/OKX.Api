namespace OKX.Api.Grid;

/// <summary>
/// OKX Rest Api Grid Trading Client
/// </summary>
public class OkxGridRestClient(OkxRestApiClient root) : OkxBaseRestClient(root)
{
    // Endpoints
    private const string v5TradingBotGridOrderAlgo = "api/v5/tradingBot/grid/order-algo";
    private const string v5TradingBotGridAmendOrderAlgo = "api/v5/tradingBot/grid/amend-order-algo";
    private const string v5TradingBotGridStopOrderAlgo = "api/v5/tradingBot/grid/stop-order-algo";
    private const string v5TradingBotGridClosePosition = "api/v5/tradingBot/grid/close-position";
    private const string v5TradingBotGridCancelCloseOrder = "api/v5/tradingBot/grid/cancel-close-order";
    private const string v5TradingBotGridOrderInstantTrigger = "api/v5/tradingBot/grid/order-instant-trigger";
    private const string v5TradingBotGridOrdersAlgoPending = "api/v5/tradingBot/grid/orders-algo-pending";
    private const string v5TradingBotGridOrdersAlgoHistory = "api/v5/tradingBot/grid/orders-algo-history";
    private const string v5TradingBotGridOrdersAlgoDetails = "api/v5/tradingBot/grid/orders-algo-details";
    private const string v5TradingBotGridSubOrders = "api/v5/tradingBot/grid/sub-orders";
    private const string v5TradingBotGridPositions = "api/v5/tradingBot/grid/positions";
    private const string v5TradingBotGridWithdrawIncome = "api/v5/tradingBot/grid/withdraw-income";
    private const string v5TradingBotGridComputeMarginBalance = "api/v5/tradingBot/grid/compute-margin-balance";
    private const string v5TradingBotGridMarginBalance = "api/v5/tradingBot/grid/margin-balance";
    private const string v5TradingBotGridAdjustInvestment = "api/v5/tradingBot/grid/adjust-investment";
    private const string v5TradingBotGridAiParam = "api/v5/tradingBot/grid/ai-param";
    private const string v5TradingBotGridMinInvestment = "api/v5/tradingBot/grid/min-investment";
    private const string v5TradingBotGridRsiBackTesting = "api/v5/tradingBot/grid/rsi-back-testing";

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
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            { "instId", instrumentId },
            { "algoOrdType", JsonConvert.SerializeObject(algoOrderType, new OkxGridAlgoOrderTypeConverter(false)) },
            { "maxPx", maximumPrice.ToOkxString() },
            { "minPx", minimumPrice.ToOkxString() },
            { "gridNum", gridNumber.ToOkxString() },
        };
        parameters.AddOptional("runType", JsonConvert.SerializeObject(gridRunType, new OkxGridRunTypeConverter(false)));
        parameters.AddOptional("tpTriggerPx", takeProfitTriggerPrice?.ToOkxString());
        parameters.AddOptional("slTriggerPx", stopLossTriggerPrice?.ToOkxString());
        parameters.AddOptional("algoClOrdId", algoClientOrderId);
        parameters.AddOptional("profitSharingRatio", profitSharingRatio?.ToOkxString());
        parameters.AddOptional("triggerParams", JsonConvert.SerializeObject(triggerParameters));

        // Spot Grid Order
        parameters.AddOptional("quoteSz", quoteSize?.ToOkxString());
        parameters.AddOptional("baseSz", baseSize?.ToOkxString());

        // Contract Grid Order
        parameters.AddOptional("sz", size?.ToOkxString());
        parameters.AddOptional("direction", JsonConvert.SerializeObject(contractGridDirection, new OkxGridContractDirectionConverter(false)));
        parameters.AddOptional("lever", leverage?.ToOkxString());
        parameters.AddOptional("basePos", basePosition);
        parameters.AddOptional("tpRatio", takeProfitRatio?.ToOkxString());
        parameters.AddOptional("slRatio", stopLossRatio?.ToOkxString());

        // Broker ID
        parameters.AddOptional("tag", Options.BrokerId);

        return ProcessOneRequestAsync<OkxGridPlaceOrderResponse>(GetUri(v5TradingBotGridOrderAlgo), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
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
        parameters.AddOptional("triggerParams", JsonConvert.SerializeObject(triggerParameters));

        return ProcessOneRequestAsync<OkxGridOrderAmendResponse>(GetUri(v5TradingBotGridAmendOrderAlgo), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
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
            { "algoOrdType", JsonConvert.SerializeObject(algoOrderType, new OkxGridAlgoOrderTypeConverter(false)) },
        };
        if (spotAlgoStopType.HasValue)
            parameters.AddOptional("stopType", JsonConvert.SerializeObject(spotAlgoStopType, new OkxGridSpotAlgoStopTypeConverter(false)));
        else if (contractAlgoStopType.HasValue)
            parameters.AddOptional("stopType", JsonConvert.SerializeObject(contractAlgoStopType, new OkxGridContractAlgoStopTypeConverter(false)));

        return ProcessOneRequestAsync<OkxGridOrderStopResponse>(GetUri(v5TradingBotGridStopOrderAlgo), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
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

        return ProcessOneRequestAsync<OkxGridOrderCloseResponse>(GetUri(v5TradingBotGridClosePosition), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
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
        return ProcessOneRequestAsync<OkxGridOrderCloseResponse>(GetUri(v5TradingBotGridCancelCloseOrder), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
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
        return ProcessOneRequestAsync<OkxGridOrderTriggerResponse>(GetUri(v5TradingBotGridOrderInstantTrigger), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
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
        var parameters = new ParameterCollection {
            {"algoOrdType", JsonConvert.SerializeObject(algoOrderType, new OkxGridAlgoOrderTypeConverter(false))}
        };
        parameters.AddOptional("algoId", algoOrderId?.ToOkxString());
        parameters.AddOptional("instId", instrumentId);
        parameters.AddOptional("instType", JsonConvert.SerializeObject(instrumentType, new OkxInstrumentTypeConverter(false)));
        parameters.AddOptional("after", after?.ToOkxString());
        parameters.AddOptional("before", before?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxGridOrder>(GetUri(v5TradingBotGridOrdersAlgoPending), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
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
        var parameters = new ParameterCollection {
            {"algoOrdType", JsonConvert.SerializeObject(algoOrderType, new OkxGridAlgoOrderTypeConverter(false))}
        };
        parameters.AddOptional("algoId", algoOrderId?.ToOkxString());
        parameters.AddOptional("instId", instrumentId);
        parameters.AddOptional("instType", JsonConvert.SerializeObject(instrumentType, new OkxInstrumentTypeConverter(false)));
        parameters.AddOptional("after", after?.ToOkxString());
        parameters.AddOptional("before", before?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxGridOrder>(GetUri(v5TradingBotGridOrdersAlgoHistory), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
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
            { "algoOrdType", JsonConvert.SerializeObject(algoOrderType, new OkxGridAlgoOrderTypeConverter(false)) },
            { "algoId", algoOrderId}
        };

        return ProcessOneRequestAsync<OkxGridOrder>(GetUri(v5TradingBotGridOrdersAlgoDetails), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
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
            { "algoOrdType", JsonConvert.SerializeObject(algoOrderType, new OkxGridAlgoOrderTypeConverter(false)) },
            { "algoId", algoOrderId.ToOkxString() },
            { "type", JsonConvert.SerializeObject(type, new OkxGridAlgoSubOrderTypeConverter(false)) },
        };
        parameters.AddOptional("groupId", groupId);
        parameters.AddOptional("after", after?.ToOkxString());
        parameters.AddOptional("before", before?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxGridAlgoSubOrder>(GetUri(v5TradingBotGridSubOrders), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
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
            { "algoOrdType", JsonConvert.SerializeObject(algoOrderType, new OkxGridAlgoOrderTypeConverter(false)) },
            { "algoId", algoOrderId.ToOkxString() }
        };

        return ProcessListRequestAsync<OkxGridAlgoPosition>(GetUri(v5TradingBotGridPositions), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
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

        return ProcessOneRequestAsync<OkxGridWithdrawIncome>(GetUri(v5TradingBotGridWithdrawIncome), HttpMethod.Post, ct, signed: true, queryParameters: parameters);
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
            { "type", JsonConvert.SerializeObject(type, new OkxAccountMarginAddReduceConverter(false)) },
            { "amt", quantity.ToOkxString() },
        };

        return ProcessOneRequestAsync<OkxGridComputedMarginBalance>(GetUri(v5TradingBotGridComputeMarginBalance), HttpMethod.Post, ct, signed: true, queryParameters: parameters);
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
            { "type", JsonConvert.SerializeObject(type, new OkxAccountMarginAddReduceConverter(false)) },
        };
        parameters.AddOptional("amt", quantity?.ToOkxString());
        parameters.AddOptional("percent", percent?.ToOkxString());

        return ProcessOneRequestAsync<OkxGridMarginOrderResponse>(GetUri(v5TradingBotGridMarginBalance), HttpMethod.Post, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// It is used to add investment and only applicable to contract gird.
    /// </summary>
    /// <param name="algoOrderId">Algo ID</param>
    /// <param name="amount">The amount is going to be added</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxGridInvestmentAddResponse>> AddInvestmentAsync(long algoOrderId, decimal amount, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            { "algoId", algoOrderId.ToOkxString() },
            { "amt", amount.ToOkxString() },
        };

        return ProcessOneRequestAsync<OkxGridInvestmentAddResponse>(GetUri(v5TradingBotGridAdjustInvestment), HttpMethod.Post, ct, signed: true, queryParameters: parameters);
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
            { "algoOrdType", JsonConvert.SerializeObject(algoOrderType, new OkxGridAlgoOrderTypeConverter(false)) },
            { "instId", instrumentId },
        };
        parameters.AddOptional("direction", JsonConvert.SerializeObject(direction, new OkxGridContractDirectionConverter(false)));
        parameters.AddOptional("duration", JsonConvert.SerializeObject(duration, new OkxGridBackTestingDurationConverter(false)));

        return ProcessOneRequestAsync<OkxGridAiParameter>(GetUri(v5TradingBotGridAiParam), HttpMethod.Get, ct, signed: false, queryParameters: parameters);
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
            { "algoOrdType", JsonConvert.SerializeObject(algoOrderType, new OkxGridAlgoOrderTypeConverter(false)) },
            { "maxPx", maximumPrice.ToOkxString() },
            { "minPx", minimumPrice.ToOkxString() },
            { "gridNum", gridNumber.ToOkxString() },
            { "runType", JsonConvert.SerializeObject(gridRunType, new OkxGridRunTypeConverter(false)) },
        };
        parameters.AddOptional("direction", JsonConvert.SerializeObject(direction, new OkxGridContractDirectionConverter(false)));
        parameters.AddOptional("lever", leverage?.ToOkxString());
        parameters.AddOptional("basePos", basePosition);
        parameters.AddOptional("investmentType", JsonConvert.SerializeObject(investmentType, new OkxGridInvestmentTypeConverter(false)));
        parameters.AddOptional("triggerStrategy", JsonConvert.SerializeObject(triggerStrategy, new OkxGridTriggerStrategyConverter(false)));
        parameters.AddOptional("investmentData", JsonConvert.SerializeObject(investmentData));

        return ProcessOneRequestAsync<OkxGridInvestment>(GetUri(v5TradingBotGridMinInvestment), HttpMethod.Post, ct, signed: false, bodyParameters: parameters);
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
    public Task<RestCallResult<OkxGridRsiBacktest>> RsiBacktestAsync(
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
            { "timeframe", JsonConvert.SerializeObject(timeframe, new OkxGridAlgoTimeFrameConverter(false)) },
            { "thold", threshold.ToOkxString() },
            { "timePeriod", timePeriod.ToOkxString() },
        };
        parameters.AddOptional("triggerCond", JsonConvert.SerializeObject(triggerCondition, new OkxGridAlgoTriggerConditionConverter(false)));
        parameters.AddOptional("duration", duration);

        return ProcessOneRequestAsync<OkxGridRsiBacktest>(GetUri(v5TradingBotGridRsiBackTesting), HttpMethod.Get, ct, signed: false, bodyParameters: parameters);
    }

}