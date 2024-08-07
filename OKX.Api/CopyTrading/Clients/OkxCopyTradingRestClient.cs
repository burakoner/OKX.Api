﻿using OKX.Api.Account.Converters;
using OKX.Api.Account.Enums;
using OKX.Api.Account.Models;
using OKX.Api.CopyTrading.Converters;
using OKX.Api.CopyTrading.Enums;
using OKX.Api.CopyTrading.Models;

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
    private const string v5CopyTradingTotalUnrealizedProfitSharing = "api/v5/copytrading/total-unrealized-profit-sharing";
    private const string v5CopyTradingApplyLeadTrading = "api/v5/copytrading/apply-lead-trading";
    private const string v5CopyTradingStopLeadTrading = "api/v5/copytrading/stop-lead-trading";
    private const string v5CopyTradingAmendProfitSharingRatio = "api/v5/copytrading/amend-profit-sharing-ratio";
    private const string v5CopyTradingConfig = "api/v5/copytrading/config";
    private const string v5CopyTradingFirstCopySettings = "api/v5/copytrading/first-copy-settings";
    private const string v5CopyTradingAmendCopySettings = "api/v5/copytrading/amend-copy-settings";
    private const string v5CopyTradingStopCopyTrading = "api/v5/copytrading/stop-copy-trading";
    private const string v5CopyTradingCopySettings = "api/v5/copytrading/copy-settings";
    private const string v5CopyTradingBatchLeverageInfo = "api/v5/copytrading/batch-leverage-info";
    private const string v5CopyTradingBatchSetLeverage = "api/v5/copytrading/batch-set-leverage";
    private const string v5CopyTradingCurrentLeadTraders = "api/v5/copytrading/current-lead-traders";
    private const string v5CopyTradingLeadTradersHistory = "api/v5/copytrading/lead-traders-history";
    private const string v5CopyTradingPublicConfig = "api/v5/copytrading/public-config";
    private const string v5CopyTradingPublicLeadTraders = "api/v5/copytrading/public-lead-traders";
    private const string v5CopyTradingPublicWeeklyPnl = "api/v5/copytrading/public-weekly-pnl";
    private const string v5CopyTradingPublicPnl = "api/v5/copytrading/public-pnl";
    private const string v5CopyTradingPublicStats = "api/v5/copytrading/public-stats";
    private const string v5CopyTradingPublicPreferenceCurrency = "api/v5/copytrading/public-preference-currency";
    private const string v5CopyTradingPublicCurrentSubpositions = "api/v5/copytrading/public-current-subpositions";
    private const string v5CopyTradingPublicSubpositionsHistory = "api/v5/copytrading/public-subpositions-history";
    private const string v5CopyTradingPublicCopyTraders = "api/v5/copytrading/public-copy-traders";
    private const string v5CopyTradingLeadTraders = "api/v5/copytrading/lead-traders";
    private const string v5CopyTradingWeeklyPnl = "api/v5/copytrading/weekly-pnl";
    private const string v5CopyTradingPnl = "api/v5/copytrading/pnl";
    private const string v5CopyTradingStats = "api/v5/copytrading/stats";
    private const string v5CopyTradingPreferenceCurrency = "api/v5/copytrading/preference-currency";
    private const string v5CopyTradingPerformanceCurrentSubpositions = "api/v5/copytrading/performance-current-subpositions";
    private const string v5CopyTradingPerformanceSubpositionsHistory = "api/v5/copytrading/performance-subpositions-history";
    private const string v5CopyTradingCopyTraders = "api/v5/copytrading/copy-traders";

    /// <summary>
    /// The leading trader gets leading positions that are not closed.
    /// Returns reverse chronological order with openTime
    /// </summary>
    /// <param name="instrumentId">Instrument ID, e.g. BTC-USDT-SWAP</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxCopyTradingLeadingPosition>>> GetLeadingPositionsAsync(
        string instrumentId = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("instId", instrumentId);

        return ProcessListRequestAsync<OkxCopyTradingLeadingPosition>(GetUri(v5CopyTradingCurrentSubpositions), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
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
    public Task<RestCallResult<List<OkxCopyTradingLeadingPositionHistory>>> GetLeadingPositionsHistoryAsync(
        string instrumentId = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("instId", instrumentId);
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxCopyTradingLeadingPositionHistory>(GetUri(v5CopyTradingSubpositionsHistory), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
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
    public Task<RestCallResult<OkxCopyTradingLeadingPositionId>> PlaceLeadingStopOrderAsync(
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

        return ProcessOneRequestAsync<OkxCopyTradingLeadingPositionId>(GetUri(v5CopyTradingAlgoOrder), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// The leading trader can only close a leading position once a time.
    /// It is required to pass subPosId which can get from Get existing leading positions.
    /// </summary>
    /// <param name="leadingPositionId">Leading position ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxCopyTradingLeadingPositionId>> CloseLeadingPositionAsync(
        long leadingPositionId,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "subPosId", leadingPositionId },
        };

        return ProcessOneRequestAsync<OkxCopyTradingLeadingPositionId>(GetUri(v5CopyTradingCloseSubposition), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// The leading trader gets contracts that are supported to lead by the platform.
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxCopyTradingLeadingInstrument>>> GetLeadingInstrumentsAsync(
        CancellationToken ct = default)
    {
        return ProcessListRequestAsync<OkxCopyTradingLeadingInstrument>(GetUri(v5CopyTradingInstruments), HttpMethod.Get, ct, signed: true);
    }

    /// <summary>
    /// The leading trder can amend current leading instruments, need to set initial leading instruments while applying to become a leading trader.
    /// All non-leading contracts can't have position or pending orders for the current request when setting non-leading contracts as leading contracts.
    /// </summary>
    /// <param name="instrumentIds">Instrument ID, e.g. BTC-USDT-SWAP. If there are multiple instruments, separate them with commas. Maximum of 31 instruments can be selected.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxCopyTradingLeadingInstrument>>> SetLeadingInstrumentsAsync(
        IEnumerable<string> instrumentIds,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "instId", string.Join(",", instrumentIds) },
        };

        return ProcessListRequestAsync<OkxCopyTradingLeadingInstrument>(GetUri(v5CopyTradingSetInstruments), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// The leading trader gets all profits shared details since joining the platform.
    /// </summary>
    /// <param name="after">Pagination of data to return records earlier than the requested profitSharingId</param>
    /// <param name="before">Pagination of data to return records newer than the requested profitSharingId</param>
    /// <param name="limit">Number of results per request. Maximum is 100. Default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxCopyTradingProfitSharingDetails>>> GetProfitSharingDetailsAsync(
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxCopyTradingProfitSharingDetails>(GetUri(v5CopyTradingProfitSharingDetails), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// The leading trader gets the total amount of profit shared since joining the platform.
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxCopyTradingProfitSharingTotal>>> GetProfitSharingTotalAsync(
        CancellationToken ct = default)
    {
        return ProcessListRequestAsync<OkxCopyTradingProfitSharingTotal>(GetUri(v5CopyTradingTotalProfitSharing), HttpMethod.Get, ct, signed: true);
    }

    /// <summary>
    /// The leading trader gets the profit sharing details that are expected to be shared in the next settlement cycle.
    /// The unrealized profit sharing details will update once there copy position is closed.
    /// </summary>
    /// <param name="ct"></param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxCopyTradingProfitSharingUnrealized>>> GetUnrealizedProfitSharingDetailsAsync(
        CancellationToken ct = default)
    {
        return ProcessListRequestAsync<OkxCopyTradingProfitSharingUnrealized>(GetUri(v5CopyTradingUnrealizedProfitSharingDetails), HttpMethod.Get, ct, signed: true);
    }
    
    public Task<RestCallResult<OkxCopyTradingProfitSharingTotalUnrealized>> GetTotalUnrealizedProfitSharingAsync(OkxInstrumentType? instrumentType = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new OkxInstrumentTypeConverter(false)));

        return ProcessOneRequestAsync<OkxCopyTradingProfitSharingTotalUnrealized>(GetUri(v5CopyTradingTotalUnrealizedProfitSharing), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    public Task<RestCallResult<OkxBooleanResponse>> ApplyForLeadTradingAsync(
        IEnumerable<string> instrumentIds,
        OkxInstrumentType? instrumentType = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "instId", string.Join(",", instrumentIds) },
        };
        parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new OkxInstrumentTypeConverter(false)));

        return ProcessOneRequestAsync<OkxBooleanResponse>(GetUri(v5CopyTradingApplyLeadTrading), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }
    
    public Task<RestCallResult<OkxBooleanResponse>> StopLeadTradingAsync(
        OkxInstrumentType? instrumentType = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new OkxInstrumentTypeConverter(false)));

        return ProcessOneRequestAsync<OkxBooleanResponse>(GetUri(v5CopyTradingStopLeadTrading), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    public Task<RestCallResult<OkxBooleanResponse>> AmendProfitSharingRatioAsync(
        decimal profitSharingRatio,
        OkxInstrumentType? instrumentType = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "profitSharingRatio", profitSharingRatio.ToOkxString() },
        };
        parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new OkxInstrumentTypeConverter(false)));

        return ProcessOneRequestAsync<OkxBooleanResponse>(GetUri(v5CopyTradingAmendProfitSharingRatio), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }
    
    public Task<RestCallResult<OkxCopyTradingAccountConfiguration>> GetAccountConfigurationAsync(CancellationToken ct = default)
    {
        return ProcessOneRequestAsync<OkxCopyTradingAccountConfiguration>(GetUri(v5CopyTradingConfig), HttpMethod.Get, ct, signed: true);
    }
    
    public Task<RestCallResult<OkxBooleanResponse>> FirstCopySettingsAsync(
        string uniqueCode,
        OkxCopyTradingMarginMode copyMarginMode,
        OkxCopyTradingInstrumentIdType copyInstrumentIdType,
        decimal copyTotalAmount,
        OkxCopyTradingPositionCloseType positionCloseType,
        OkxInstrumentType? instrumentType = null,
        IEnumerable<string> instrumentIds = null,
        OkxCopyTradingCopyMode? copyMode = null,
        decimal? copyAmount = null,
        decimal? copyRatio = null,
        decimal? takeProfitRatio = null,
        decimal? stopLossRatio = null,
        decimal? stopLossTotalAmount = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "uniqueCode", uniqueCode },
            { "copyMgnMode", JsonConvert.SerializeObject(copyMarginMode, new OkxCopyTradingMarginModeConverter(false)) },
            { "copyInstIdType", JsonConvert.SerializeObject(copyMarginMode, new OkxCopyTradingInstrumentIdTypeConverter(false)) },
            { "copyTotalAmt", copyTotalAmount.ToOkxString() },
            { "subPosCloseType", JsonConvert.SerializeObject(positionCloseType, new OkxCopyTradingPositionCloseTypeConverter(false)) },
        };
        parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new OkxInstrumentTypeConverter(false)));
        if(instrumentIds is not null)parameters.AddOptionalParameter("instId", string.Join(",", instrumentIds));
        parameters.AddOptionalParameter("copyMode", JsonConvert.SerializeObject(copyMode, new OkxCopyTradingCopyModeConverter(false)));
        parameters.AddOptionalParameter("copyAmt", copyAmount?.ToOkxString());
        parameters.AddOptionalParameter("copyRatio", copyRatio?.ToOkxString());
        parameters.AddOptionalParameter("tpRatio", takeProfitRatio?.ToOkxString());
        parameters.AddOptionalParameter("slRatio", stopLossRatio?.ToOkxString());
        parameters.AddOptionalParameter("slTotalAmt", stopLossTotalAmount?.ToOkxString());

        return ProcessOneRequestAsync<OkxBooleanResponse>(GetUri(v5CopyTradingFirstCopySettings), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }
    
    public Task<RestCallResult<OkxBooleanResponse>> AmendCopySettingsAsync(
        string uniqueCode,
        OkxCopyTradingMarginMode copyMarginMode,
        OkxCopyTradingInstrumentIdType copyInstrumentIdType,
        decimal copyTotalAmount,
        OkxCopyTradingPositionCloseType positionCloseType,
        OkxInstrumentType? instrumentType = null,
        IEnumerable<string> instrumentIds = null,
        OkxCopyTradingCopyMode? copyMode = null,
        decimal? copyAmount = null,
        decimal? copyRatio = null,
        decimal? takeProfitRatio = null,
        decimal? stopLossRatio = null,
        decimal? stopLossTotalAmount = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "uniqueCode", uniqueCode },
            { "copyMgnMode", JsonConvert.SerializeObject(copyMarginMode, new OkxCopyTradingMarginModeConverter(false)) },
            { "copyInstIdType", JsonConvert.SerializeObject(copyMarginMode, new OkxCopyTradingInstrumentIdTypeConverter(false)) },
            { "copyTotalAmt", copyTotalAmount.ToOkxString() },
            { "subPosCloseType", JsonConvert.SerializeObject(positionCloseType, new OkxCopyTradingPositionCloseTypeConverter(false)) },
        };
        parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new OkxInstrumentTypeConverter(false)));
        if(instrumentIds is not null)parameters.AddOptionalParameter("instId", string.Join(",", instrumentIds));
        parameters.AddOptionalParameter("copyMode", JsonConvert.SerializeObject(copyMode, new OkxCopyTradingCopyModeConverter(false)));
        parameters.AddOptionalParameter("copyAmt", copyAmount?.ToOkxString());
        parameters.AddOptionalParameter("copyRatio", copyRatio?.ToOkxString());
        parameters.AddOptionalParameter("tpRatio", takeProfitRatio?.ToOkxString());
        parameters.AddOptionalParameter("slRatio", stopLossRatio?.ToOkxString());
        parameters.AddOptionalParameter("slTotalAmt", stopLossTotalAmount?.ToOkxString());

        return ProcessOneRequestAsync<OkxBooleanResponse>(GetUri(v5CopyTradingAmendCopySettings), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }
    
    public Task<RestCallResult<OkxBooleanResponse>> StopCopyingAsync(
        string uniqueCode,
        OkxCopyTradingPositionCloseType positionCloseType,
        OkxInstrumentType? instrumentType = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "uniqueCode", uniqueCode },
            { "subPosCloseType", JsonConvert.SerializeObject(positionCloseType, new OkxCopyTradingPositionCloseTypeConverter(false)) },
        };
        parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new OkxInstrumentTypeConverter(false)));

        return ProcessOneRequestAsync<OkxBooleanResponse>(GetUri(v5CopyTradingStopCopyTrading), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }
    
    public Task<RestCallResult<OkxCopyTradingCopySettings>> GetCopySettingsAsync(
        string uniqueCode,
        OkxInstrumentType? instrumentType = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "uniqueCode", uniqueCode },
        };
        parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new OkxInstrumentTypeConverter(false)));

        return ProcessOneRequestAsync<OkxCopyTradingCopySettings>(GetUri(v5CopyTradingCopySettings), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
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
    public Task<RestCallResult<OkxCopyTradingMultipleLeverage>> GetMultipleLeverages(
        OkxAccountMarginMode marginMode,
        string uniqueCode,
        string instrumentIds,
        CancellationToken ct = default)
    {
        if (string.IsNullOrEmpty(uniqueCode) || uniqueCode.Length != 16)
            throw new ArgumentException("uniqueCode is required. If you are not a lead trader, please use OkxRestApiClient.Public.GetInstrumentsAsync method.");

        if (string.IsNullOrEmpty(instrumentIds))
            throw new ArgumentException("instrumentIds is required");

        var parameters = new Dictionary<string, object> {
            {"mgnMode", JsonConvert.SerializeObject(marginMode, new OkxAccountMarginModeConverter(false)) },
            {"uniqueCode", uniqueCode },
            {"instId", instrumentIds },
        };

        return ProcessOneRequestAsync<OkxCopyTradingMultipleLeverage>(GetUri(v5CopyTradingBatchLeverageInfo), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
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
    public Task<RestCallResult<OkxCopyTradingMultipleLeverage>> GetMultipleLeverages(
        OkxAccountMarginMode marginMode,
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
    public Task<RestCallResult<OkxCopyTradingMultipleLeverage>> GetMultipleLeverages(
        OkxAccountMarginMode marginMode,
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
    public Task<RestCallResult<OkxCopyTradingMultipleOperation>> SetMultipleLeverageAsync(
        OkxAccountMarginMode marginMode,
        decimal leverage,
        IEnumerable<string> instrumentIds,
        CancellationToken ct = default)
    {
        if (leverage < 0.01m)
            throw new ArgumentException("Invalid Leverage");

        var parameters = new Dictionary<string, object> {
            {"mgnMode", JsonConvert.SerializeObject(marginMode, new OkxAccountMarginModeConverter(false)) },
            {"lever", leverage.ToOkxString() },
            {"instId", string.Join(",", instrumentIds)},
        };

        return ProcessOneRequestAsync<OkxCopyTradingMultipleOperation>(GetUri(v5CopyTradingBatchSetLeverage), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Set Multiple leverages
    /// </summary>
    /// <param name="marginMode">Margin Mode</param>
    /// <param name="leverage">Leverage</param>
    /// <param name="ct">Cancellation Token</param>
    /// <param name="instrumentIds">Instrument ID</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxCopyTradingMultipleOperation>> SetMultipleLeverageAsync(
        OkxAccountMarginMode marginMode,
        decimal leverage,
        CancellationToken ct = default,
        params string[] instrumentIds)
    {
        return SetMultipleLeverageAsync(marginMode, leverage, instrumentIds, ct);
    }
    
    public Task<RestCallResult<List<OkxCopyTradingLeadTrader>>> GetMyLeadTradersAsync(
        OkxInstrumentType? instrumentType = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new OkxInstrumentTypeConverter(false)));

        return ProcessListRequestAsync<OkxCopyTradingLeadTrader>(GetUri(v5CopyTradingCurrentLeadTraders), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }
    
    public Task<RestCallResult<List<OkxCopyTradingLeadTrader>>> GetMyLeadTradersHistoryAsync(
        OkxInstrumentType? instrumentType = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new OkxInstrumentTypeConverter(false)));
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxCopyTradingLeadTrader>(GetUri(v5CopyTradingLeadTradersHistory), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }
    
    public Task<RestCallResult<OkxCopyTradingPublicConfiguration>> GetPublicConfigurationAsync(
        OkxInstrumentType? instrumentType = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new OkxInstrumentTypeConverter(false)));

        return ProcessOneRequestAsync<OkxCopyTradingPublicConfiguration>(GetUri(v5CopyTradingPublicConfig), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }
    
    public Task<RestCallResult<OkxCopyTradingLeadTradersRanks>> GetLeadTradersRanksAsync(
        OkxInstrumentType? instrumentType = null,
        string sortType = null,
        string state = null,
        string minLeadDays = null,
        decimal? minAssets = null,
        decimal? maxAssets = null,
        decimal? minAum = null,
        decimal? maxAum = null,
        string dataVersion = null,
        int? page = null,
        int limit = 10,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 20);
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new OkxInstrumentTypeConverter(false)));
        parameters.AddOptionalParameter("sortType", sortType);
        parameters.AddOptionalParameter("state", state);
        parameters.AddOptionalParameter("minLeadDays", minLeadDays);
        parameters.AddOptionalParameter("minAssets", minAssets?.ToOkxString());
        parameters.AddOptionalParameter("maxAssets", maxAssets?.ToOkxString());
        parameters.AddOptionalParameter("minAum", minAum?.ToOkxString());
        parameters.AddOptionalParameter("maxAum", maxAum?.ToOkxString());
        parameters.AddOptionalParameter("dataVer", dataVersion);
        parameters.AddOptionalParameter("page", page?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return ProcessOneRequestAsync<OkxCopyTradingLeadTradersRanks>(GetUri(v5CopyTradingPublicLeadTraders), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }
    
    public Task<RestCallResult<List<OkxCopyTradingLeadTraderPnl>>> GetLeadTraderWeeklyPnlAsync(
        string uniqueCode,
        OkxInstrumentType? instrumentType = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("uniqueCode", uniqueCode);
        parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new OkxInstrumentTypeConverter(false)));

        return ProcessListRequestAsync<OkxCopyTradingLeadTraderPnl>(GetUri(v5CopyTradingPublicWeeklyPnl), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    public Task<RestCallResult<List<OkxCopyTradingLeadTraderPnl>>> GetLeadTraderDailyPnlAsync(
        string uniqueCode,
        string lastDays,
        OkxInstrumentType? instrumentType = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("uniqueCode", uniqueCode);
        parameters.AddOptionalParameter("lastDays", lastDays);
        parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new OkxInstrumentTypeConverter(false)));

        return ProcessListRequestAsync<OkxCopyTradingLeadTraderPnl>(GetUri(v5CopyTradingPublicPnl), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }
    
    public Task<RestCallResult<List<OkxCopyTradingLeadTraderStats>>> GetLeadTraderStatsAsync(
        string uniqueCode,
        string lastDays,
        OkxInstrumentType? instrumentType = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("uniqueCode", uniqueCode);
        parameters.AddOptionalParameter("lastDays", lastDays);
        parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new OkxInstrumentTypeConverter(false)));

        return ProcessListRequestAsync<OkxCopyTradingLeadTraderStats>(GetUri(v5CopyTradingPublicStats), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    public Task<RestCallResult<List<OkxCopyTradingLeadTraderCurrencyPreference>>> GetLeadTraderCurrencyPreferencesAsync(
        string uniqueCode,
        OkxInstrumentType? instrumentType = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("uniqueCode", uniqueCode);
        parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new OkxInstrumentTypeConverter(false)));

        return ProcessListRequestAsync<OkxCopyTradingLeadTraderCurrencyPreference>(GetUri(v5CopyTradingPublicPreferenceCurrency), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }
    
    public Task<RestCallResult<List<OkxCopyTradingLeadTraderPosition>>> GetLeadTraderCurrentPositionsAsync(
        string uniqueCode,
        OkxInstrumentType? instrumentType = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("uniqueCode", uniqueCode);
        parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new OkxInstrumentTypeConverter(false)));
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxCopyTradingLeadTraderPosition>(GetUri(v5CopyTradingPublicCurrentSubpositions), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }
    
    public Task<RestCallResult<List<OkxCopyTradingLeadTraderPositionHistory>>> GetLeadTraderPositionHistoryAsync(
        string uniqueCode,
        OkxInstrumentType? instrumentType = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("uniqueCode", uniqueCode);
        parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new OkxInstrumentTypeConverter(false)));
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxCopyTradingLeadTraderPositionHistory>(GetUri(v5CopyTradingPublicSubpositionsHistory), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }
    
    public Task<RestCallResult<List<OkxCopyTradingCopyTrader>>> GetCopyTradersAsync(
        string uniqueCode,
        OkxInstrumentType? instrumentType = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("uniqueCode", uniqueCode);
        parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new OkxInstrumentTypeConverter(false)));
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxCopyTradingCopyTrader>(GetUri(v5CopyTradingPublicCopyTraders), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }
    
    public Task<RestCallResult<OkxCopyTradingLeadTradersRanks>> GetMyLeadTradersRanksAsync(
        OkxInstrumentType? instrumentType = null,
        string sortType = null,
        string state = null,
        string minLeadDays = null,
        decimal? minAssets = null,
        decimal? maxAssets = null,
        decimal? minAum = null,
        decimal? maxAum = null,
        string dataVersion = null,
        int? page = null,
        int limit = 10,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 20);
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new OkxInstrumentTypeConverter(false)));
        parameters.AddOptionalParameter("sortType", sortType);
        parameters.AddOptionalParameter("state", state);
        parameters.AddOptionalParameter("minLeadDays", minLeadDays);
        parameters.AddOptionalParameter("minAssets", minAssets?.ToOkxString());
        parameters.AddOptionalParameter("maxAssets", maxAssets?.ToOkxString());
        parameters.AddOptionalParameter("minAum", minAum?.ToOkxString());
        parameters.AddOptionalParameter("maxAum", maxAum?.ToOkxString());
        parameters.AddOptionalParameter("dataVer", dataVersion);
        parameters.AddOptionalParameter("page", page?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return ProcessOneRequestAsync<OkxCopyTradingLeadTradersRanks>(GetUri(v5CopyTradingLeadTraders), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }
    
    public Task<RestCallResult<List<OkxCopyTradingLeadTraderPnl>>> GetMyLeadTraderWeeklyPnlAsync(
        string uniqueCode,
        OkxInstrumentType? instrumentType = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("uniqueCode", uniqueCode);
        parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new OkxInstrumentTypeConverter(false)));

        return ProcessListRequestAsync<OkxCopyTradingLeadTraderPnl>(GetUri(v5CopyTradingWeeklyPnl), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    public Task<RestCallResult<List<OkxCopyTradingLeadTraderPnl>>> GetMyLeadTraderDailyPnlAsync(
        string uniqueCode,
        string lastDays,
        OkxInstrumentType? instrumentType = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("uniqueCode", uniqueCode);
        parameters.AddOptionalParameter("lastDays", lastDays);
        parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new OkxInstrumentTypeConverter(false)));

        return ProcessListRequestAsync<OkxCopyTradingLeadTraderPnl>(GetUri(v5CopyTradingPnl), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }
    
    public Task<RestCallResult<List<OkxCopyTradingLeadTraderStats>>> GetMyLeadTraderStatsAsync(
        string uniqueCode,
        string lastDays,
        OkxInstrumentType? instrumentType = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("uniqueCode", uniqueCode);
        parameters.AddOptionalParameter("lastDays", lastDays);
        parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new OkxInstrumentTypeConverter(false)));

        return ProcessListRequestAsync<OkxCopyTradingLeadTraderStats>(GetUri(v5CopyTradingStats), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }
    
    public Task<RestCallResult<List<OkxCopyTradingLeadTraderCurrencyPreference>>> GetMyLeadTraderCurrencyPreferencesAsync(
        string uniqueCode,
        OkxInstrumentType? instrumentType = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("uniqueCode", uniqueCode);
        parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new OkxInstrumentTypeConverter(false)));

        return ProcessListRequestAsync<OkxCopyTradingLeadTraderCurrencyPreference>(GetUri(v5CopyTradingPreferenceCurrency), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }
    
    public Task<RestCallResult<List<OkxCopyTradingLeadTraderPosition>>> GetMyLeadTraderCurrentPositionsAsync(
        string uniqueCode,
        OkxInstrumentType? instrumentType = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("uniqueCode", uniqueCode);
        parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new OkxInstrumentTypeConverter(false)));
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxCopyTradingLeadTraderPosition>(GetUri(v5CopyTradingPerformanceCurrentSubpositions), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    public Task<RestCallResult<List<OkxCopyTradingLeadTraderPositionHistory>>> GetMyLeadTraderPositionHistoryAsync(
        string uniqueCode,
        OkxInstrumentType? instrumentType = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("uniqueCode", uniqueCode);
        parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new OkxInstrumentTypeConverter(false)));
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxCopyTradingLeadTraderPositionHistory>(GetUri(v5CopyTradingPerformanceSubpositionsHistory), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }
    
    public Task<RestCallResult<List<OkxCopyTradingCopyTrader>>> GetMyCopyTradersAsync(
        string uniqueCode,
        OkxInstrumentType? instrumentType = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("uniqueCode", uniqueCode);
        parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new OkxInstrumentTypeConverter(false)));
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxCopyTradingCopyTrader>(GetUri(v5CopyTradingCopyTraders), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }
    
}