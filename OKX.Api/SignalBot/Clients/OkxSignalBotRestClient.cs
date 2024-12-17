namespace OKX.Api.SignalBot;

/// <summary>
/// OKX Rest Api Signal Trading Client
/// </summary>
public class OkxSignalBotRestClient(OkxRestApiClient root) : OkxBaseRestClient(root)
{
    // Endpoints
    private const string v5TradingBotSignalCreateSignal = "api/v5/tradingBot/signal/create-signal";
    private const string v5TradingBotSignalSignals = "api/v5/tradingBot/signal/signals";
    private const string v5TradingBotSignalOrderAlgo = "api/v5/tradingBot/signal/order-algo";
    private const string v5TradingBotSignalOrdersAlgoPending = "api/v5/tradingBot/signal/orders-algo-pending";
    private const string v5TradingBotSignalStopOrderAlgo = "api/v5/tradingBot/signal/stop-order-algo";
    private const string v5TradingBotSignalMarginBalance = "api/v5/tradingBot/signal/margin-balance";
    private const string v5TradingBotSignalAmendTPSL = "api/v5/tradingBot/signal/amendTPSL";
    private const string v5TradingBotSignalSetInstruments = "api/v5/tradingBot/signal/set-instruments";
    private const string v5TradingBotSignalOrdersAlgoDetails = "api/v5/tradingBot/signal/orders-algo-details";
    private const string v5TradingBotSignalOrdersAlgoHistory = "api/v5/tradingBot/signal/orders-algo-history";
    private const string v5TradingBotSignalPositions = "api/v5/tradingBot/signal/positions";
    private const string v5TradingBotSignalPositionsHistory = "api/v5/tradingBot/signal/positions-history";
    private const string v5TradingBotSignalClosePosition = "api/v5/tradingBot/signal/close-position";
    private const string v5TradingBotSignalSubOrder = "api/v5/tradingBot/signal/sub-order";
    private const string v5TradingBotSignalCancelSubOrder = "api/v5/tradingBot/signal/cancel-sub-order";
    private const string v5TradingBotSignalSubOrders = "api/v5/tradingBot/signal/sub-orders";
    private const string v5TradingBotSignalEventHistory = "api/v5/tradingBot/signal/event-history";

    /// <summary>
    /// POST / Create signal
    /// </summary>
    /// <param name="name">Signal channel name</param>
    /// <param name="description">Signal channel description</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxSignalBotChannel>> CreateChannelAsync(string name, string description = "", CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            { "signalChanName", name },
            { "signalChanDesc", description },
        };
        return ProcessOneRequestAsync<OkxSignalBotChannel>(GetUri(v5TradingBotSignalCreateSignal), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// GET / Signals
    /// </summary>
    /// <param name="signalSourceType">Signal source type</param>
    /// <param name="signalChannelId">Signal channel id</param>
    /// <param name="after">Pagination of data to return records signalChanId earlier than the requested timestamp, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records signalChanId newer than the requested timestamp, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100. The default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxSignalBotChannelInformation>>> GetChannelsAsync(
        OkxSignalBotSourceType signalSourceType,
        long? signalChannelId = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddEnum("signalSourceType", signalSourceType);
        parameters.AddOptional("signalChanId", signalChannelId?.ToOkxString());
        parameters.AddOptional("after", after?.ToOkxString());
        parameters.AddOptional("before", before?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxSignalBotChannelInformation>(GetUri(v5TradingBotSignalSignals), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Create signal bot
    /// </summary>
    /// <param name="signalChannelId">Signal channel Id</param>
    /// <param name="leverage">Leverage. Only applicable to contract signal</param>
    /// <param name="amount">Investment amount</param>
    /// <param name="orderType">Sub order type 1：limit order 2：market order 9：tradingView signal</param>
    /// <param name="includeAll">Whether to include all USDT-margined contract.The default value is false. true: include false : exclude</param>
    /// <param name="instrumentIds">Instrument IDs. Single currency or multiple currencies separated with comma. When includeAll is true, it is ignored</param>
    /// <param name="ratio">Price offset ratio, calculate the limit price as a percentage offset from the best bid/ask price. Only applicable to subOrdType is limit order</param>
    /// <param name="entryParamaters">Entry setting</param>
    /// <param name="exitParamaters">Exit setting</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxSignalBotOrderId>> CreateSignalBotAsync(
        long signalChannelId,
        decimal leverage,
        decimal amount,
        OkxSignalBotOrderType orderType,
        bool includeAll = false,
        IEnumerable<string>? instrumentIds = null,
        decimal? ratio = null,
        OkxSignalBotEntryParamaters? entryParamaters = null,
        OkxSignalBotExitParamaters? exitParamaters = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            { "signalChanId", signalChannelId.ToString() },
            { "lever", leverage.ToOkxString() },
            { "investAmt", amount.ToOkxString() },
        };
        parameters.AddEnum("subOrdType", orderType);
        parameters.AddOptional("includeAll", includeAll);
        parameters.AddOptional("instIds", instrumentIds);
        parameters.AddOptional("ratio", ratio?.ToOkxString());
        parameters.AddOptional("entrySettingParam", entryParamaters);
        parameters.AddOptional("exitSettingParam", exitParamaters);

        return ProcessOneRequestAsync<OkxSignalBotOrderId>(GetUri(v5TradingBotSignalOrderAlgo), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// A maximum of 10 orders can be stopped per request.
    /// </summary>
    /// <param name="algoIds">Algo ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<List<long>>> CancelSignalBotsAsync(
        IEnumerable<long> algoIds,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            { "algoId", algoIds.Select(x => new OkxSignalBotAlgoId { Data = x }) },
        };

        var result = await ProcessListRequestAsync<OkxSignalBotAlgoIdResponse>(GetUri(v5TradingBotSignalStopOrderAlgo), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
        if (!result || result.Data is null) return new RestCallResult<List<long>>(result.Request, result.Response, result.Raw, result.Error);
        return new RestCallResult<List<long>>(result.Request, result.Response, result.Data.Select(x => x.Data).ToList(), result.Raw, result.Error);
    }

    /// <summary>
    /// Adjust margin balance
    /// </summary>
    /// <param name="algoId">Algo ID</param>
    /// <param name="type">Adjust margin balance type</param>
    /// <param name="amount">Adjust margin balance amount Either amt or percent is required.</param>
    /// <param name="allowReinvest">Whether to reinvest with newly added margin. The default value is false.
    /// false:it will be used as passive margin to prevent liquidation and will not be used as active investment
    /// true:the margin added here will furthermore be accounted for in calculations of your total investment amount, and furthermore your order size。
    /// Only applicable to your signal comes in with an “investmentType” of “percentage_investment”</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<List<long>>> AdjustMarginBalanceAsync(
        long algoId,
        OkxAccountMarginAddReduce type,
        decimal amount,
        bool? allowReinvest = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            { "algoId", algoId.ToOkxString() },
            { "amt", amount.ToOkxString() },
        };
        parameters.AddEnum("type", type);
        parameters.AddOptional("allowReinvest", allowReinvest);

        var result = await ProcessListRequestAsync<OkxSignalBotAlgoIdResponse>(GetUri(v5TradingBotSignalMarginBalance), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
        if (!result || result.Data is null) return new RestCallResult<List<long>>(result.Request, result.Response, result.Raw, result.Error);
        return new RestCallResult<List<long>>(result.Request, result.Response, result.Data.Select(x => x.Data).ToList(), result.Raw, result.Error);
    }

    /// <summary>
    /// Amend TPSL
    /// </summary>
    /// <param name="algoId">Algo ID</param>
    /// <param name="exitSettingParam">Exit setting</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<List<long>>> AmendTPSLAsync(
        long algoId,
        OkxSignalBotExitParamaters exitSettingParam,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            { "algoId", algoId.ToOkxString() },
            { "exitSettingParam", exitSettingParam },
        };

        var result = await ProcessListRequestAsync<OkxSignalBotAlgoIdResponse>(GetUri(v5TradingBotSignalAmendTPSL), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
        if (!result || result.Data is null) return new RestCallResult<List<long>>(result.Request, result.Response, result.Raw, result.Error);
        return new RestCallResult<List<long>>(result.Request, result.Response, result.Data.Select(x => x.Data).ToList(), result.Raw, result.Error);
    }

    /// <summary>
    /// Set instruments
    /// </summary>
    /// <param name="algoId">Algo ID</param>
    /// <param name="InstrumentIds">Instrument IDs. When includeAll is true, it is ignored</param>
    /// <param name="includeAll">Whether to include all USDT-margined contract.The default value is false. true: include false : exclude</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<List<long>>> SetInstrumentsAsync(
        long algoId,
        IEnumerable<string> InstrumentIds,
        bool includeAll,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            { "algoId", algoId.ToOkxString() },
            { "instIds", InstrumentIds },
            { "includeAll", includeAll },
        };

        var result = await ProcessListRequestAsync<OkxSignalBotAlgoIdResponse>(GetUri(v5TradingBotSignalSetInstruments), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
        if (!result || result.Data is null) return new RestCallResult<List<long>>(result.Request, result.Response, result.Raw, result.Error);
        return new RestCallResult<List<long>>(result.Request, result.Response, result.Data.Select(x => x.Data).ToList(), result.Raw, result.Error);
    }

    /// <summary>
    /// Signal bot order details
    /// </summary>
    /// <param name="algoId">Algo ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxSignalBot>> GetSignalBotAsync(
        long algoId,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            { "algoId", algoId.ToOkxString() },
            { "algoOrdType", "contract" },
        };

        return ProcessOneRequestAsync<OkxSignalBot>(GetUri(v5TradingBotSignalOrdersAlgoDetails), HttpMethod.Get, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// GET / Active signal bot
    /// </summary>
    /// <param name="algoId">Algo ID</param>
    /// <param name="after">Pagination of data to return records algoId earlier than the requested timestamp, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records algoId newer than the requested timestamp, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100. The default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    public Task<RestCallResult<List<OkxSignalBot>>> GetSignalBotsAsync(
        long algoId,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            { "algoId", algoId.ToOkxString() },
            { "algoOrdType", "contract" },
        };
        parameters.AddOptional("after", after?.ToOkxString());
        parameters.AddOptional("before", before?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxSignalBot>(GetUri(v5TradingBotSignalOrdersAlgoPending), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Signal bot history
    /// </summary>
    /// <param name="algoId">Algo ID</param>
    /// <param name="after">Pagination of data to return records algoId earlier than the requested timestamp, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records algoId newer than the requested timestamp, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100. The default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxSignalBot>>> GetSignalBotHistoryAsync(
        long algoId,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            { "algoId", algoId.ToOkxString() },
            { "algoOrdType", "contract" },
        };
        parameters.AddOptional("after", after?.ToOkxString());
        parameters.AddOptional("before", before?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxSignalBot>(GetUri(v5TradingBotSignalOrdersAlgoHistory), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// GET / Signal bot order positions
    /// </summary>
    /// <param name="algoId">Algo ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxSignalBotPosition>>> GetPositionsAsync(
        long algoId,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            { "algoId", algoId.ToOkxString() },
            { "algoOrdType", "contract" },
        };

        return ProcessListRequestAsync<OkxSignalBotPosition>(GetUri(v5TradingBotSignalPositions), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve the updated position data for the last 3 months. Return in reverse chronological order using utime.
    /// </summary>
    /// <param name="algoId">Algo ID</param>
    /// <param name="instrumentId">Instrument ID, e.g.：BTC-USD-SWAP</param>
    /// <param name="after">Pagination of data to return records algoId earlier than the requested timestamp, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records algoId newer than the requested timestamp, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100. The default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxSignalBotPositionHistory>>> GetPositionHistoryAsync(
        long algoId,
        string? instrumentId = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            { "algoId", algoId.ToOkxString() },
        };
        parameters.AddOptional("instId", instrumentId);
        parameters.AddOptional("after", after?.ToOkxString());
        parameters.AddOptional("before", before?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxSignalBotPositionHistory>(GetUri(v5TradingBotSignalPositionsHistory), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Close the position of an instrument via a market order.
    /// </summary>
    /// <param name="algoId">Algo ID</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<List<long>>> ClosePositionAsync(
        long algoId,
        string? instrumentId = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            { "algoId", algoId.ToOkxString() },
        };
        parameters.AddOptional("instId", instrumentId);

        var result = await ProcessListRequestAsync<OkxSignalBotAlgoId>(GetUri(v5TradingBotSignalClosePosition), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
        if (!result || result.Data is null) return new RestCallResult<List<long>>(result.Request, result.Response, result.Raw, result.Error);
        return new RestCallResult<List<long>>(result.Request, result.Response, result.Data.Where(x => x.Data.HasValue).Select(x => x.Data!.Value).ToList(), result.Raw, result.Error);
    }

    /// <summary>
    /// You can place an order only if you have sufficient funds.
    /// </summary>
    /// <param name="algoId">Algo ID</param>
    /// <param name="instrumentId">Instrument ID, e.g. BTC-USDT-SWAP</param>
    /// <param name="side">Order side, buy sell</param>
    /// <param name="type">Order type</param>
    /// <param name="size">Quantity to buy or sell</param>
    /// <param name="price">Order price. Only applicable to limit order.</param>
    /// <param name="reduceOnly">Whether orders can only reduce in position size.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public Task<RestCallResult<object>> PlaceSuborderAsync(
        long algoId,
        string instrumentId,
        OkxTradeOrderSide side,
        OkxTradeOrderType type,
        decimal size,
        decimal? price,
        bool? reduceOnly = null,
        CancellationToken ct = default)
    {
        if (type.IsNotIn(OkxTradeOrderType.MarketOrder, OkxTradeOrderType.LimitOrder))
            throw new ArgumentException("Only market and limit orders are supported for suborders", nameof(type));

        var parameters = new ParameterCollection {
            { "algoId", algoId.ToOkxString() },
            { "instId", instrumentId },
            { "sz", size.ToOkxString() },
        };
        parameters.AddOptionalEnum("side", side);
        parameters.AddOptionalEnum("ordType", type);
        parameters.AddOptional("px", price?.ToOkxString());
        parameters.AddOptional("reduceOnly", reduceOnly);
        parameters.AddOptional("tag", OkxConstants.BrokerId);

        return ProcessOneRequestAsync<object>(GetUri(v5TradingBotSignalSubOrder), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Cancel an incomplete order.
    /// </summary>
    /// <param name="algoId">Algo ID</param>
    /// <param name="instrumentId">Instrument ID, e.g. BTC-USDT-SWAP</param>
    /// <param name="signalOrderId">Order ID</param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<RestCallResult<long?>> CancelSuborderAsync(
        long algoId,
        string instrumentId,
        long signalOrderId,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            { "algoId", algoId.ToOkxString() },
            { "signalOrdId", signalOrderId.ToOkxString() },
            { "instId", instrumentId },
        };

        var result = await ProcessOneRequestAsync<OkxSignalBotSignalOrderId>(GetUri(v5TradingBotSignalCancelSubOrder), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
        if (!result) return new RestCallResult<long?>(result.Request, result.Response, result.Raw, result.Error);
        return new RestCallResult<long?>(result.Request, result.Response, result.Data.Data, result.Raw, result.Error);
    }

    /// <summary>
    /// GET / Signal bot sub orders
    /// </summary>
    /// <param name="algoId">Algo ID</param>
    /// <param name="state">Sub order state</param>
    /// <param name="type">Sub order type</param>
    /// <param name="signalOrderId">Sub order ID</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ordId</param>
    /// <param name="before">Pagination of data to return records newer than the requested ordId.</param>
    /// <param name="begin">Return records of ctime after than the requested timestamp (include), Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="end">Return records of ctime before than the requested timestamp (include), Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100. The default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxSignalBotSuborder>>> GetSubordersAsync(
        long algoId,
        OkxSignalBotSuborderState? state = null,
        OkxSignalBotSuborderType? type = null,
        long? signalOrderId = null,
        long? after = null,
        long? before = null,
        long? begin = null,
        long? end = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            { "algoId", algoId.ToOkxString() },
            { "algoOrdType", "contract" },
        };
        parameters.AddOptionalEnum("state", state);
        parameters.AddOptionalEnum("type", type);
        parameters.AddOptional("signalOrdId", signalOrderId);
        parameters.AddOptional("after", after?.ToOkxString());
        parameters.AddOptional("before", before?.ToOkxString());
        parameters.AddOptional("begin", begin?.ToOkxString());
        parameters.AddOptional("end", end?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxSignalBotSuborder>(GetUri(v5TradingBotSignalSubOrders), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// GET / Signal bot event history
    /// </summary>
    /// <param name="algoId">Algo ID</param>
    /// <param name="after">Pagination of data to return records eventCtime earlier than the requested timestamp, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records eventCtime newer than the requested timestamp, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100. The default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxSignalBotSuborder>>> GetEventHistoryAsync(
        long algoId,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            { "algoId", algoId.ToOkxString() },
        };
        parameters.AddOptional("after", after?.ToOkxString());
        parameters.AddOptional("before", before?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxSignalBotSuborder>(GetUri(v5TradingBotSignalEventHistory), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }
}
