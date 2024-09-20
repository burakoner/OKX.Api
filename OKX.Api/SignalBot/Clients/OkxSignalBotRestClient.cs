using OKX.Api.Account.Converters;
using OKX.Api.Account.Enums;
using OKX.Api.SignalBot.Converters;
using OKX.Api.SignalBot.Enums;
using OKX.Api.SignalBot.Models;
using OKX.Api.Trade.Converters;
using OKX.Api.Trade.Enums;

namespace OKX.Api.SignalBot.Clients;

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
        var parameters = new Dictionary<string, object> {
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
    public Task<RestCallResult<List<OkxSignalBotChannelInformation>>> GetChannelAsync(
        OkxSignalBotSourceType signalSourceType,
        long? signalChannelId = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            {"signalSourceType", JsonConvert.SerializeObject(signalSourceType, new OkxSignalBotSourceTypeConverter(false))}
        };
        parameters.AddOptionalParameter("signalChanId", signalChannelId?.ToOkxString());
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

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
        IEnumerable<string> instrumentIds = null,
        decimal? ratio = null,
        OkxSignalBotEntryParamaters entryParamaters = null,
        OkxSignalBotExitParamaters exitParamaters = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "signalChanId", signalChannelId.ToString() },
            { "lever", leverage.ToOkxString() },
            { "investAmt", amount.ToOkxString() },
            { "subOrdType", JsonConvert.SerializeObject(orderType, new OkxSignalBotOrderTypeConverter(false)) },
        };
        parameters.AddOptionalParameter("includeAll", includeAll);
        parameters.AddOptionalParameter("instIds", instrumentIds);
        parameters.AddOptionalParameter("ratio", ratio?.ToOkxString());
        parameters.AddOptionalParameter("entrySettingParam", entryParamaters);
        parameters.AddOptionalParameter("exitSettingParam", exitParamaters);

        return ProcessOneRequestAsync<OkxSignalBotOrderId>(GetUri(v5TradingBotSignalOrderAlgo), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// A maximum of 10 orders can be stopped per request.
    /// </summary>
    /// <param name="algoIds">Algo ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxSignalBotAlgoIdResponse>>> CancelSignalBotsAsync(
        IEnumerable<long> algoIds,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "algoId", algoIds.Select(x=>new OkxSignalBotAlgoId{ AlgoId = x.ToOkxString()}) },
        };

        return ProcessListRequestAsync<OkxSignalBotAlgoIdResponse>(GetUri(v5TradingBotSignalStopOrderAlgo), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
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
    public Task<RestCallResult<List<OkxSignalBotAlgoIdResponse>>> AdjustMarginBalanceAsync(
        long algoId,
        OkxAccountMarginAddReduce type,
        decimal amount,
        bool? allowReinvest = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "algoId", algoId.ToOkxString() },
            { "type", JsonConvert.SerializeObject(type, new OkxAccountMarginAddReduceConverter(false)) },
            { "amt", amount.ToOkxString() },
        };
        parameters.AddOptionalParameter("allowReinvest", allowReinvest);

        return ProcessListRequestAsync<OkxSignalBotAlgoIdResponse>(GetUri(v5TradingBotSignalMarginBalance), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Amend TPSL
    /// </summary>
    /// <param name="algoId">Algo ID</param>
    /// <param name="exitSettingParam">Exit setting</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxSignalBotAlgoIdResponse>>> AmendTPSLAsync(
        long algoId,
        OkxSignalBotExitParamaters exitSettingParam,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "algoId", algoId.ToOkxString() },
            { "exitSettingParam", exitSettingParam },
        };

        return ProcessListRequestAsync<OkxSignalBotAlgoIdResponse>(GetUri(v5TradingBotSignalAmendTPSL), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Set instruments
    /// </summary>
    /// <param name="algoId">Algo ID</param>
    /// <param name="InstrumentIds">Instrument IDs. When includeAll is true, it is ignored</param>
    /// <param name="includeAll">Whether to include all USDT-margined contract.The default value is false. true: include false : exclude</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxSignalBotAlgoIdResponse>>> SetInstrumentsAsync(
        long algoId,
        IEnumerable<string> InstrumentIds,
        bool includeAll,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "algoId", algoId.ToOkxString() },
            { "instIds", InstrumentIds },
            { "includeAll", includeAll },
        };

        return ProcessListRequestAsync<OkxSignalBotAlgoIdResponse>(GetUri(v5TradingBotSignalSetInstruments), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
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
        var parameters = new Dictionary<string, object> {
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
        var parameters = new Dictionary<string, object> {
            { "algoId", algoId.ToOkxString() },
            { "algoOrdType", "contract" },
        };
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

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
        var parameters = new Dictionary<string, object> {
            { "algoId", algoId.ToOkxString() },
            { "algoOrdType", "contract" },
        };
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

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
        var parameters = new Dictionary<string, object> {
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
        string instrumentId = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "algoId", algoId.ToOkxString() },
        };
        parameters.AddOptionalParameter("instId", instrumentId);
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxSignalBotPositionHistory>(GetUri(v5TradingBotSignalPositionsHistory), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Close the position of an instrument via a market order.
    /// </summary>
    /// <param name="algoId">Algo ID</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxSignalBotAlgoId>>> ClosePositionAsync(
        long algoId,
        string instrumentId = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "algoId", algoId.ToOkxString() },
            { "instId", instrumentId },
        };

        return ProcessListRequestAsync<OkxSignalBotAlgoId>(GetUri(v5TradingBotSignalClosePosition), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
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
    public Task<RestCallResult<OkxSignalBotSuborderResponse>> PlaceSuborderAsync(
        long algoId,
        string instrumentId,
        OkxTradeOrderSide side,
        OkxTradeOrderType type,
        decimal size,
        decimal? price,
        bool? reduceOnly = null,
        CancellationToken ct = default)
    {
        if(type.IsNotIn(OkxTradeOrderType.MarketOrder,OkxTradeOrderType.LimitOrder))
            throw new ArgumentException("Only market and limit orders are supported for suborders", nameof(type));

        var parameters = new Dictionary<string, object> {
            { "algoId", algoId.ToOkxString() },
            { "instId", instrumentId },
            { "sz", size.ToOkxString() },
        };
        parameters.AddOptionalParameter("side", JsonConvert.SerializeObject(side, new OkxTradeOrderSideConverter(false)));
        parameters.AddOptionalParameter("ordType", JsonConvert.SerializeObject(type, new OkxTradeOrderTypeConverter(false)));
        parameters.AddOptionalParameter("px", price?.ToOkxString());
        parameters.AddOptionalParameter("reduceOnly", reduceOnly);

        return ProcessOneRequestAsync<OkxSignalBotSuborderResponse>(GetUri(v5TradingBotSignalClosePosition), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }
}
