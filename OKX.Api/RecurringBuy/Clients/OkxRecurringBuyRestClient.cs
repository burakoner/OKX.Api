using OKX.Api.Public.Converters;
using OKX.Api.Public.Enums;
using OKX.Api.RecurringBuy.Converters;
using OKX.Api.RecurringBuy.Enums;
using OKX.Api.RecurringBuy.Models;

namespace OKX.Api.RecurringBuy.Clients;

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

    public Task<RestCallResult<OkxRecurringBuyOrderResponse>> PlaceOrderAsync(
       string strategyName,
       IEnumerable<OkxRecurringBuyItem> recurringList,
       OkxRecurringBuyPeriod period,
       int? recurringDay,
       int? recurringHour,
       int recurringTime,
       string timeZone,
       decimal amount,
       string currency,
       OkxTradeMode tradeMode,
       string clientOrderId = null,
       CancellationToken ct = default)
    {
        // Common
        var parameters = new Dictionary<string, object> {
            {"stgyName", strategyName },
            {"recurringList", recurringList },
            {"period", JsonConvert.SerializeObject(period, new OkxRecurringBuyPeriodConverter(false)) },
            {"timeZone", timeZone },
            {"amt", amount.ToOkxString() },
            {"investmentCcy", currency },
            {"tdMode", JsonConvert.SerializeObject(tradeMode, new OkxTradeModeConverter(false)) },
        };
        parameters.AddOptionalParameter("recurringDay", recurringDay?.ToOkxString());
        parameters.AddOptionalParameter("recurringHour", recurringHour?.ToOkxString());
        parameters.AddOptionalParameter("recurringTime", recurringTime.ToOkxString());
        parameters.AddOptionalParameter("algoClOrdId", clientOrderId);
        parameters.AddOptionalParameter("tag", Options.BrokerId);

        // Reequest
        return ProcessOneRequestAsync<OkxRecurringBuyOrderResponse>(GetUri(v5TradingBotRecurringOrderAlgo), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }
    
    public Task<RestCallResult<OkxRecurringBuyOrderResponse>> AmendOrderAsync(long algoOrderId, string strategyName, CancellationToken ct = default)
    {
        // Common
        var parameters = new Dictionary<string, object> {
            {"algoId", algoOrderId.ToOkxString() },
            {"stgyName", strategyName },
        };

        // Reequest
        return ProcessOneRequestAsync<OkxRecurringBuyOrderResponse>(GetUri(v5TradingBotRecurringAmendOrderAlgo), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    public Task<RestCallResult<OkxRecurringBuyOrderResponse>> StopOrderAsync(long algoOrderId, CancellationToken ct = default)
    {
        // Common
        var parameters = new Dictionary<string, object> {
            {"algoId", algoOrderId.ToOkxString() },
        };

        // Reequest
        return ProcessOneRequestAsync<OkxRecurringBuyOrderResponse>(GetUri(v5TradingBotRecurringStopOrderAlgo), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }
    
    public Task<RestCallResult<List<OkxRecurringBuyOrder>>> GetOpenOrdersAsync(
        long? algoOrderId = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("algoId", algoOrderId?.ToOkxString());
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxRecurringBuyOrder>(GetUri(v5TradingBotRecurringOrdersAlgoPending), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }
    
    public Task<RestCallResult<List<OkxRecurringBuyOrder>>> GetOrderHistoryAsync(
        long? algoOrderId = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("algoId", algoOrderId?.ToOkxString());
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxRecurringBuyOrder>(GetUri(v5TradingBotRecurringOrdersAlgoHistory), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    public Task<RestCallResult<OkxRecurringBuyOrder>> GetOrderAsync(long algoOrderId, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("algoId", algoOrderId.ToOkxString());

        return ProcessOneRequestAsync<OkxRecurringBuyOrder>(GetUri(v5TradingBotRecurringOrdersAlgoDetails), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    public Task<RestCallResult<List<OkxRecurringBuyOrder>>> GetSubOrdersAsync(
        long algoOrderId,
        long? subOrderId = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("algoId", algoOrderId.ToOkxString());
        parameters.AddOptionalParameter("ordId", subOrderId?.ToOkxString());
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxRecurringBuyOrder>(GetUri(v5TradingBotRecurringSubOrders), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

}