using OKX.Api.Account.Converters;
using OKX.Api.Account.Enums;
using OKX.Api.Account.Models;
using OKX.Api.AlgoTrading.Converters;
using OKX.Api.AlgoTrading.Enums;
using OKX.Api.Common.Clients;
using OKX.Api.CopyTrading.Models;
using System.Diagnostics.Metrics;

namespace OKX.Api.CopyTrading.Clients;

/// <summary>
/// OKX Rest Api Copy Trading Client
/// </summary>
public class OkxCopyTradingRestClient(OkxRestApiClient root) : OkxBaseRestClient(root)
{
    // Endpoints
    private const string v5CopyTradingCurrentSubpositions = "api/v5/copytrading/current-subpositions";
    private const string v5CopyTradingSubpositionsHistory = "api/v5/copytrading/subpositions-history";
    private const string v5CopyTradingAlgoOrder = "api/v5/copytrading/algo-order";
    private const string v5CopyTradingCloseSubposition = "api/v5/copytrading/close-subposition";
    private const string v5CopyTradingInstruments = "api/v5/copytrading/instruments";
    private const string v5CopyTradingSetInstruments = "api/v5/copytrading/set-instruments";
    private const string v5CopyTradingProfitSharingDetails = "api/v5/copytrading/profit-sharing-details";
    private const string v5CopyTradingTotalProfitSharing = "api/v5/copytrading/total-profit-sharing";
    private const string v5CopyTradingUnrealizedProfitSharingDetails = "api/v5/copytrading/unrealized-profit-sharing-details";
    private const string v5CopyTradingBatchLeverageInfo = "api/v5/copytrading/batch-leverage-info";
    private const string v5CopyTradingBatchSetLeverage = "api/v5/copytrading/batch-set-leverage";

    #region Copy Trading API Endpoints
    /// <summary>
    /// The leading trader gets leading positions that are not closed.
    /// Returns reverse chronological order with openTime
    /// </summary>
    /// <param name="instrumentId">Instrument ID, e.g. BTC-USDT-SWAP</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxLeadingPosition>>> GetLeadingPositionsAsync(
        string instrumentId = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("instId", instrumentId);

        return ProcessListRequestAsync<OkxLeadingPosition>(GetUri(v5CopyTradingCurrentSubpositions), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// The leading trader retrieves the completed leading position of the last 3 months.
    /// Returns reverse chronological order with closeTime.
    /// </summary>
    /// <param name="instrumentId">Instrument ID, e.g. BTC-USDT-SWAP</param>
    /// <param name="after">Pagination of data to return records earlier than the requested subPosId.</param>
    /// <param name="before">Pagination of data to return records newer than the requested subPosId.</param>
    /// <param name="limit">Number of results per request. Maximum is 100. Default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxLeadingPositionHistory>>> GetLeadingPositionsHistoryAsync(
        string instrumentId = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("instId", instrumentId);
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxLeadingPositionHistory>(GetUri(v5CopyTradingSubpositionsHistory), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// The leading trader sets TP/SL for the current leading position that are not closed.
    /// </summary>
    /// <param name="leadingPositionId">Leading position ID</param>
    /// <param name="takeProfitTriggerPrice">Take-profit trigger price. Take-profit order price will be the market price after triggering. At least one of tpTriggerPx and slTriggerPx must be filled</param>
    /// <param name="takeProfitTriggerPriceType">Take-profit trigger price type. Default is last</param>
    /// <param name="stopLossTriggerPrice">Stop-loss trigger price. Stop-loss order price will be the market price after triggering.</param>
    /// <param name="stopLossTriggerPriceType">Stop-loss trigger price type. Default is last</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxLeadingPositionId>> PlaceLeadingStopOrderAsync(
        long leadingPositionId,
        decimal? takeProfitTriggerPrice = null,
        OkxAlgoPriceType? takeProfitTriggerPriceType = null,
        decimal? stopLossTriggerPrice = null,
        OkxAlgoPriceType? stopLossTriggerPriceType = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "subPosId", leadingPositionId },
        };
        parameters.AddOptionalParameter("tpTriggerPx", takeProfitTriggerPrice?.ToOkxString());
        parameters.AddOptionalParameter("slTriggerPx", stopLossTriggerPrice?.ToOkxString());
        parameters.AddOptionalParameter("tpTriggerPxType", JsonConvert.SerializeObject(takeProfitTriggerPriceType, new OkxAlgoPriceTypeConverter(false)));
        parameters.AddOptionalParameter("slTriggerPxType", JsonConvert.SerializeObject(stopLossTriggerPriceType, new OkxAlgoPriceTypeConverter(false)));

        return ProcessOneRequestAsync<OkxLeadingPositionId>(GetUri(v5CopyTradingAlgoOrder), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// The leading trader can only close a leading position once a time.
    /// It is required to pass subPosId which can get from Get existing leading positions.
    /// </summary>
    /// <param name="leadingPositionId">Leading position ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxLeadingPositionId>> CloseLeadingPositionAsync(
        long leadingPositionId,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "subPosId", leadingPositionId },
        };

        return ProcessOneRequestAsync<OkxLeadingPositionId>(GetUri(v5CopyTradingCloseSubposition), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// The leading trader gets contracts that are supported to lead by the platform.
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxLeadingInstrument>>> GetLeadingInstrumentsAsync(
        CancellationToken ct = default)
    {
        return ProcessListRequestAsync<OkxLeadingInstrument>(GetUri(v5CopyTradingInstruments), HttpMethod.Get, ct, signed: true);
    }

    /// <summary>
    /// The leading trder can amend current leading instruments, need to set initial leading instruments while applying to become a leading trader.
    /// All non-leading contracts can't have position or pending orders for the current request when setting non-leading contracts as leading contracts.
    /// </summary>
    /// <param name="instrumentIds">Instrument ID, e.g. BTC-USDT-SWAP. If there are multiple instruments, separate them with commas. Maximum of 31 instruments can be selected.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxLeadingInstrument>>> SetLeadingInstrumentsAsync(
        IEnumerable<string> instrumentIds,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "instId", string.Join(",", instrumentIds) },
        };

        return ProcessListRequestAsync<OkxLeadingInstrument>(GetUri(v5CopyTradingSetInstruments), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// The leading trader gets all profits shared details since joining the platform.
    /// </summary>
    /// <param name="after">Pagination of data to return records earlier than the requested profitSharingId</param>
    /// <param name="before">Pagination of data to return records newer than the requested profitSharingId</param>
    /// <param name="limit">Number of results per request. Maximum is 100. Default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxProfitSharingDetails>>> GetProfitSharingDetailsAsync(
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxProfitSharingDetails>(GetUri(v5CopyTradingProfitSharingDetails), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// The leading trader gets the total amount of profit shared since joining the platform.
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxProfitSharingTotal>>> GetProfitSharingTotalAsync(
        CancellationToken ct = default)
    {
        return ProcessListRequestAsync<OkxProfitSharingTotal>(GetUri(v5CopyTradingTotalProfitSharing), HttpMethod.Get, ct, signed: true);
    }

    /// <summary>
    /// The leading trader gets the profit sharing details that are expected to be shared in the next settlement cycle.
    /// The unrealized profit sharing details will update once there copy position is closed.
    /// </summary>
    /// <param name="ct"></param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxProfitSharingUnrealized>>> GetUnrealizedProfitSharingDetailsAsync(
        CancellationToken ct = default)
    {
        return ProcessListRequestAsync<OkxProfitSharingUnrealized>(GetUri(v5CopyTradingUnrealizedProfitSharingDetails), HttpMethod.Get, ct, signed: true);
    }


    /// <summary>
    /// Get multiple leverages
    /// Retrieve leverages that belong to the lead trader and you.
    /// </summary>
    /// <param name="marginMode">Margin Mode</param>
    /// <param name="uniqueCode">Lead trader unique code</param>
    /// <param name="instrumentIds">ingle instrument ID or multiple instrument IDs (no more than 200) separated with comma</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxMultipleLeverage>> GetMultipleLeverages(
        OkxMarginMode marginMode,
        string uniqueCode,
        string instrumentIds,
        CancellationToken ct = default)
    {
        if (string.IsNullOrEmpty(uniqueCode) || uniqueCode.Length != 16)
            throw new ArgumentException("uniqueCode is required. If you are not a trader, please use OkxRestApiClient.Public.GetInstrumentsAsync method.");

        if (string.IsNullOrEmpty(instrumentIds))
            throw new ArgumentException("instrumentIds is required");

        var parameters = new Dictionary<string, object> {
            {"uniqueCode", uniqueCode },
            {"mgnMode", JsonConvert.SerializeObject(marginMode, new OkxMarginModeConverter(false)) },
            {"instId", instrumentIds },
        };

        return ProcessOneRequestAsync<OkxMultipleLeverage>(GetUri(v5CopyTradingBatchLeverageInfo), HttpMethod.Get, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Get multiple leverages
    /// Retrieve leverages that belong to the lead trader and you.
    /// </summary>
    /// <param name="marginMode">Margin Mode</param>
    /// <param name="uniqueCode">Lead trader unique code</param>
    /// <param name="instrumentIds">ingle instrument ID or multiple instrument IDs (no more than 200) separated with comma</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxMultipleLeverage>> GetMultipleLeverages(
        OkxMarginMode marginMode,
        string uniqueCode,
        IEnumerable<string> instrumentIds,
        CancellationToken ct = default)
    {
        if (!instrumentIds.Any())
            throw new ArgumentException("instrumentIds is required");

        if (instrumentIds.Count() > 200)
            throw new ArgumentException("Instrument ID maximum of 200 instruments can be selected.");

        return GetMultipleLeverages(marginMode, uniqueCode, string.Join(",", instrumentIds), ct);
    }

    /// <summary>
    /// Get multiple leverages
    /// Retrieve leverages that belong to the lead trader and you.
    /// </summary>
    /// <param name="marginMode">Margin Mode</param>
    /// <param name="uniqueCode">Lead trader unique code</param>
    /// <param name="ct">Cancellation Token</param>
    /// <param name="instrumentIds">ingle instrument ID or multiple instrument IDs (no more than 200) separated with comma</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxMultipleLeverage>> GetMultipleLeverages(
        OkxMarginMode marginMode,
        string uniqueCode,
        CancellationToken ct = default,
        params string[] instrumentIds)
    {
        return GetMultipleLeverages(marginMode, uniqueCode, instrumentIds, ct);
    }

    /// <summary>
    /// Set Multiple leverages
    /// </summary>
    /// <param name="marginMode">Margin Mode</param>
    /// <param name="leverage">Leverage</param>
    /// <param name="ct">Cancellation Token</param>
    /// <param name="instrumentIds">Instrument ID</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxMultipleOperation>> SetMultipleLeverageAsync(
        OkxMarginMode marginMode,
        decimal leverage,
        string instrumentIds,
        CancellationToken ct = default)
    {
        if (leverage < 0.01m)
            throw new ArgumentException("Invalid Leverage");

        var parameters = new Dictionary<string, object> {
            {"mgnMode", JsonConvert.SerializeObject(marginMode, new OkxMarginModeConverter(false)) },
            {"lever", leverage.ToOkxString() },
            {"instId", string.Join(",", instrumentIds)},
        };

        return ProcessOneRequestAsync<OkxMultipleOperation>(GetUri(v5CopyTradingBatchSetLeverage), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Set Multiple leverages
    /// </summary>
    /// <param name="marginMode">Margin Mode</param>
    /// <param name="leverage">Leverage</param>
    /// <param name="ct">Cancellation Token</param>
    /// <param name="instrumentIds">Instrument ID</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxMultipleOperation>> SetMultipleLeverageAsync(
        OkxMarginMode marginMode,
        decimal leverage,
        IEnumerable<string> instrumentIds,
        CancellationToken ct = default)
    {
        if (!instrumentIds.Any())
            throw new ArgumentException("instrumentIds is required");

        if (instrumentIds.Count() > 200)
            throw new ArgumentException("Instrument ID maximum of 200 instruments can be selected.");

        return SetMultipleLeverageAsync(marginMode, leverage, string.Join(",", instrumentIds), ct);
    }

    /// <summary>
    /// Set Multiple leverages
    /// </summary>
    /// <param name="marginMode">Margin Mode</param>
    /// <param name="leverage">Leverage</param>
    /// <param name="ct">Cancellation Token</param>
    /// <param name="instrumentIds">Instrument ID</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxMultipleOperation>> SetMultipleLeverageAsync(
        OkxMarginMode marginMode,
        decimal leverage,
        CancellationToken ct = default,
        params string[] instrumentIds)
    {
        return SetMultipleLeverageAsync(marginMode, leverage, instrumentIds, ct);
    }

    #endregion

}