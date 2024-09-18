using OKX.Api.SignalBot.Converters;
using OKX.Api.SignalBot.Enums;
using OKX.Api.SignalBot.Models;

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

    public Task<RestCallResult<OkxSignalBotChannelResponse>> CreateSignalChannelAsync(string name, string description = "", CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "signalChanName", name },
            { "signalChanDesc", description },
        };
        return ProcessOneRequestAsync<OkxSignalBotChannelResponse>(GetUri(v5TradingBotSignalCreateSignal), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    public Task<RestCallResult<List<OkxSignalBotChannel>>> GetSignalChannelsAsync(
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

        return ProcessListRequestAsync<OkxSignalBotChannel>(GetUri(v5TradingBotSignalSignals), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    public Task<RestCallResult<OkxSignalBotOrderResponse>> CreateSignalBotAsync(
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

        return ProcessOneRequestAsync<OkxSignalBotOrderResponse>(GetUri(v5TradingBotSignalOrderAlgo), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    public Task<RestCallResult<List<OkxSignalBotAlgoResponse>>> CancelSignalOrdersAsync(
        long algoId,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "algoId", algoId.ToString() },
        };

        return ProcessListRequestAsync<OkxSignalBotAlgoResponse>(GetUri(v5TradingBotSignalStopOrderAlgo), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

}
