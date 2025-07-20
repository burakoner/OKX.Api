namespace OKX.Api.CopyTrading;

/// <summary>
/// OKX Rest Api Copy Trading Client
/// </summary>
public class OkxCopyTradingRestClient(OkxRestApiClient root) : OkxBaseRestClient(root)
{
    /// <summary>
    /// The leading trader gets leading positions that are not closed.
    /// Returns reverse chronological order with openTime
    /// </summary>
    /// <param name="instrumentType">Instrument type</param>
    /// <param name="instrumentId">Instrument ID, e.g. BTC-USDT-SWAP</param>
    /// <param name="uniqueCode">Unique code, only applicable to copy trading. A combination of case-sensitive alphanumerics, all numbers and the length is 16 characters, e.g. 213E8C92DC61EFAC</param>
    /// <param name="positionType">Data type.</param>
    /// <param name="after">Pagination of data to return records earlier than the requested subPosId.</param>
    /// <param name="before">Pagination of data to return records newer than the requested subPosId.</param>
    /// <param name="limit">Number of results per request. Maximum is 500. Default is 500.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxCopyTradingLeadingPosition>>> GetLeadingPositionsAsync(
        OkxInstrumentType? instrumentType = null,
        string? instrumentId = null,
        string? uniqueCode = null,
        OkxCopyTradingPositionType? positionType = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptionalEnum("instType", instrumentType);
        parameters.AddOptionalEnum("subPosType", positionType);
        parameters.AddOptional("instId", instrumentId);
        parameters.AddOptional("uniqueCode", uniqueCode);
        parameters.AddOptional("after", after?.ToOkxString());
        parameters.AddOptional("before", before?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxCopyTradingLeadingPosition>(GetUri("api/v5/copytrading/current-subpositions"), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// The leading trader retrieves the completed leading position of the last 3 months.
    /// Returns reverse chronological order with closeTime.
    /// </summary>
    /// <param name="instrumentType">Instrument type</param>
    /// <param name="instrumentId">Instrument ID, e.g. BTC-USDT-SWAP</param>
    /// <param name="positionType">Data type.</param>
    /// <param name="after">Pagination of data to return records earlier than the requested subPosId.</param>
    /// <param name="before">Pagination of data to return records newer than the requested subPosId.</param>
    /// <param name="limit">Number of results per request. Maximum is 100. Default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxCopyTradingLeadingPositionHistory>>> GetLeadingPositionsHistoryAsync(
        OkxInstrumentType? instrumentType = null,
        string? instrumentId = null,
        OkxCopyTradingPositionType? positionType = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new ParameterCollection();
        parameters.AddOptionalEnum("instType", instrumentType);
        parameters.AddOptionalEnum("subPosType", positionType);
        parameters.AddOptional("instId", instrumentId);
        parameters.AddOptional("after", after?.ToOkxString());
        parameters.AddOptional("before", before?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxCopyTradingLeadingPositionHistory>(GetUri("api/v5/copytrading/subpositions-history"), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// The leading trader sets TP/SL for the current leading position that are not closed.
    /// </summary>
    /// <param name="positionId">Leading position ID</param>
    /// <param name="instrumentType">Instrument type</param>
    /// <param name="takeProfitTriggerPrice">Take-profit trigger price. Take-profit order price will be the market price after triggering. At least one of tpTriggerPx and slTriggerPx must be filled</param>
    /// <param name="takeProfitTriggerPriceType">Take-profit trigger price type. Default is last</param>
    /// <param name="takeProfitOrderPrice">Take-profit order price. If the price is -1, take-profit will be executed at the market price, the default is -1. Only applicable to SPOT lead trader</param>
    /// <param name="stopLossTriggerPrice">Stop-loss order price. If the price is -1, stop-loss will be executed at the market price, the default is -1. Only applicable to SPOT lead trader</param>
    /// <param name="stopLossTriggerPriceType">Stop-loss trigger price type. Default is last</param>
    /// <param name="stopLossOrderPrice">Stop-loss trigger price. Stop-loss order price will be the market price after triggering.</param>
    /// <param name="positionType">Data type.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<long?>> PlaceLeadingStopOrderAsync(
        long positionId,
        OkxInstrumentType? instrumentType = null,

        decimal? takeProfitTriggerPrice = null,
        OkxAlgoPriceType? takeProfitTriggerPriceType = null,
        decimal? takeProfitOrderPrice = null,

        decimal? stopLossTriggerPrice = null,
        OkxAlgoPriceType? stopLossTriggerPriceType = null,
        decimal? stopLossOrderPrice = null,

        OkxCopyTradingPositionType? positionType = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "subPosId", positionId },
        };
        parameters.AddOptionalEnum("instType", instrumentType);
        parameters.AddOptional("tpTriggerPx", takeProfitTriggerPrice?.ToOkxString());
        parameters.AddOptional("slTriggerPx", stopLossTriggerPrice?.ToOkxString());
        parameters.AddOptional("tpOrdPx", takeProfitOrderPrice?.ToOkxString());
        parameters.AddOptional("slOrdPx", stopLossOrderPrice?.ToOkxString());
        parameters.AddOptionalEnum("tpTriggerPxType", takeProfitTriggerPriceType);
        parameters.AddOptionalEnum("slTriggerPxType", stopLossTriggerPriceType);
        parameters.AddOptionalEnum("subPosType", positionType);

        var result = await ProcessOneRequestAsync<OkxCopyTradingPositionIdContainer>(GetUri("api/v5/copytrading/algo-order"), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
        if (!result) return new RestCallResult<long?>(result.Request, result.Response, result.Raw ?? "", result.Error);
        return new RestCallResult<long?>(result.Request, result.Response, result.Data.Payload, result.Raw ?? "", result.Error);
    }

    /// <summary>
    /// The leading trader can only close a leading position once a time.
    /// It is required to pass subPosId which can get from Get existing leading positions.
    /// </summary>
    /// <param name="positionId">Leading position ID</param>
    /// <param name="instrumentType">Instrument type</param>
    /// <param name="positionType">Data type.</param>
    /// <param name="orderType">Order type</param>
    /// <param name="price">Order price. Only applicable to limit order and SPOT lead trader. If the price is 0, the pending order will be canceled. It is modifying order if you set px after placing limit order.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<long?>> CloseLeadingPositionAsync(
        long positionId,
        OkxInstrumentType? instrumentType = null,
        OkxCopyTradingPositionType? positionType = null,
        OkxTradeOrderType? orderType = null,
        decimal? price = null,
        CancellationToken ct = default)
    {
        if (orderType.IsNotIn(OkxTradeOrderType.MarketOrder, OkxTradeOrderType.LimitOrder))
            throw new ArgumentException("Order type must be MarketOrder or LimitOrder", nameof(orderType));

        var parameters = new ParameterCollection
        {
            { "subPosId", positionId },
        };
        parameters.AddOptionalEnum("instType", instrumentType);
        parameters.AddOptionalEnum("subPosType", positionType);
        parameters.AddOptionalEnum("ordType", orderType);
        parameters.AddOptional("px", price?.ToOkxString());

        var result = await ProcessOneRequestAsync<OkxCopyTradingPositionIdContainer>(GetUri("api/v5/copytrading/close-subposition"), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
        if (!result) return new RestCallResult<long?>(result.Request, result.Response, result.Raw ?? "", result.Error);
        return new RestCallResult<long?>(result.Request, result.Response, result.Data.Payload, result.Raw ?? "", result.Error);
    }

    /// <summary>
    /// The leading trader gets contracts that are supported to lead by the platform.
    /// </summary>
    /// <param name="instrumentType">Instrument type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxCopyTradingLeadingInstrument>>> GetLeadingInstrumentsAsync(
        OkxInstrumentType? instrumentType = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptionalEnum("instType", instrumentType);

        return ProcessListRequestAsync<OkxCopyTradingLeadingInstrument>(GetUri("api/v5/copytrading/instruments"), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// The leading trder can amend current leading instruments, need to set initial leading instruments while applying to become a leading trader.
    /// All non-leading contracts can't have position or pending orders for the current request when setting non-leading contracts as leading contracts.
    /// </summary>
    /// <param name="instrumentIds">Instrument ID, e.g. BTC-USDT-SWAP. If there are multiple instruments, separate them with commas. Maximum of 31 instruments can be selected.</param>
    /// <param name="instrumentType">Instrument type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxCopyTradingLeadingInstrument>>> SetLeadingInstrumentsAsync(
        IEnumerable<string> instrumentIds,
        OkxInstrumentType? instrumentType = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "instId", string.Join(",", instrumentIds) },
        };
        parameters.AddOptionalEnum("instType", instrumentType);

        return ProcessListRequestAsync<OkxCopyTradingLeadingInstrument>(GetUri("api/v5/copytrading/set-instruments"), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// The leading trader gets all profits shared details since joining the platform.
    /// </summary>
    /// <param name="instrumentType">Instrument type</param>
    /// <param name="after">Pagination of data to return records earlier than the requested profitSharingId</param>
    /// <param name="before">Pagination of data to return records newer than the requested profitSharingId</param>
    /// <param name="limit">Number of results per request. Maximum is 100. Default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxCopyTradingProfitSharingDetails>>> GetProfitSharingDetailsAsync(
        OkxInstrumentType? instrumentType = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptionalEnum("instType", instrumentType);
        parameters.AddOptional("after", after?.ToOkxString());
        parameters.AddOptional("before", before?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxCopyTradingProfitSharingDetails>(GetUri("api/v5/copytrading/profit-sharing-details"), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// The leading trader gets the total amount of profit shared since joining the platform.
    /// </summary>
    /// <param name="instrumentType">Instrument type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxCopyTradingProfitSharingTotal>>> GetTotalProfitSharingAsync(
        OkxInstrumentType? instrumentType = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptionalEnum("instType", instrumentType);

        return ProcessListRequestAsync<OkxCopyTradingProfitSharingTotal>(GetUri("api/v5/copytrading/total-profit-sharing"), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// The leading trader gets the profit sharing details that are expected to be shared in the next settlement cycle.
    /// The unrealized profit sharing details will update once there copy position is closed.
    /// </summary>
    /// <param name="instrumentType">Instrument type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxCopyTradingProfitSharingUnrealized>>> GetUnrealizedProfitSharingDetailsAsync(
        OkxInstrumentType? instrumentType = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptionalEnum("instType", instrumentType);

        return ProcessListRequestAsync<OkxCopyTradingProfitSharingUnrealized>(GetUri("api/v5/copytrading/unrealized-profit-sharing-details"), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// The leading trader gets the total unrealized amount of profit shared.
    /// </summary>
    /// <param name="instrumentType">Instrument type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxCopyTradingProfitSharingTotalUnrealized>> GetTotalUnrealizedProfitSharingAsync(OkxInstrumentType? instrumentType = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptionalEnum("instType", instrumentType);

        return ProcessOneRequestAsync<OkxCopyTradingProfitSharingTotalUnrealized>(GetUri("api/v5/copytrading/total-unrealized-profit-sharing"), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// It is used to amend profit sharing ratio.
    /// </summary>
    /// <param name="profitSharingRatio">Profit sharing ratio. 0.1 represents 10%</param>
    /// <param name="instrumentType">Instrument type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxBooleanResponse>> AmendProfitSharingRatioAsync(
        decimal profitSharingRatio,
        OkxInstrumentType? instrumentType = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "profitSharingRatio", profitSharingRatio.ToOkxString() },
        };
        parameters.AddOptionalEnum("instType", instrumentType);

        return ProcessOneRequestAsync<OkxBooleanResponse>(GetUri("api/v5/copytrading/amend-profit-sharing-ratio"), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Retrieve current account configuration related to copy/lead trading.
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxCopyTradingAccount>> GetAccountConfigurationAsync(CancellationToken ct = default)
    {
        return ProcessOneRequestAsync<OkxCopyTradingAccount>(GetUri("api/v5/copytrading/config"), HttpMethod.Get, ct, signed: true);
    }

    /// <summary>
    /// The first copy settings for the certain lead trader. You need to first copy settings after stopping copying.
    /// </summary>
    /// <param name="uniqueCode">Lead trader unique code. A combination of case-sensitive alphanumerics, all numbers and the length is 16 characters, e.g. 213E8C92DC61EFAC</param>
    /// <param name="copyMarginMode">Copy margin mode</param>
    /// <param name="copyInstrumentIdType">Copy contract type setted</param>
    /// <param name="copyTotalAmount">Maximum total amount in USDT. The maximum total amount you'll invest at any given time across all orders in this copy trade. You won’t copy new orders if you exceed this amount</param>
    /// <param name="positionCloseType">Action type for open positions</param>
    /// <param name="instrumentType">Instrument type</param>
    /// <param name="instrumentIds">Instrument ID.</param>
    /// <param name="copyMode">Copy mode</param>
    /// <param name="copyAmount">Copy amount per order in USDT.</param>
    /// <param name="copyRatio">Copy ratio per order.</param>
    /// <param name="takeProfitRatio">Take profit per order. 0.1 represents 10%</param>
    /// <param name="stopLossRatio">Stop loss per order. 0.1 represents 10%</param>
    /// <param name="stopLossTotalAmount">Total stop loss in USDT for trader. If your net loss (total profit - total loss) reaches this amount, you'll stop copying this trader</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxBooleanResponse>> FirstCopySettingsAsync(
        string uniqueCode,
        OkxCopyTradingMarginMode copyMarginMode,
        OkxCopyTradingInstrumentIdType copyInstrumentIdType,
        decimal copyTotalAmount,
        OkxCopyTradingPositionCloseType positionCloseType,
        OkxInstrumentType? instrumentType = null,
        IEnumerable<string>? instrumentIds = null,
        OkxCopyTradingMode? copyMode = null,
        decimal? copyAmount = null,
        decimal? copyRatio = null,
        decimal? takeProfitRatio = null,
        decimal? stopLossRatio = null,
        decimal? stopLossTotalAmount = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "uniqueCode", uniqueCode },
            { "copyTotalAmt", copyTotalAmount.ToOkxString() },
        };
        parameters.AddEnum("copyMgnMode", copyMarginMode);
        parameters.AddEnum("copyInstIdType", copyInstrumentIdType);
        parameters.AddEnum("subPosCloseType", positionCloseType);
        parameters.AddOptionalEnum("instType", instrumentType);
        parameters.AddOptionalEnum("copyMode", copyMode);
        if (instrumentIds != null) parameters.AddOptional("instId", string.Join(",", instrumentIds));
        parameters.AddOptional("copyAmt", copyAmount?.ToOkxString());
        parameters.AddOptional("copyRatio", copyRatio?.ToOkxString());
        parameters.AddOptional("tpRatio", takeProfitRatio?.ToOkxString());
        parameters.AddOptional("slRatio", stopLossRatio?.ToOkxString());
        parameters.AddOptional("slTotalAmt", stopLossTotalAmount?.ToOkxString());

        return ProcessOneRequestAsync<OkxBooleanResponse>(GetUri("api/v5/copytrading/first-copy-settings"), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// You need to use this endpoint to amend copy settings
    /// </summary>
    /// <param name="uniqueCode">Lead trader unique code. A combination of case-sensitive alphanumerics, all numbers and the length is 16 characters, e.g. 213E8C92DC61EFAC</param>
    /// <param name="copyMarginMode">Copy margin mode</param>
    /// <param name="copyInstrumentIdType">Copy contract type setted</param>
    /// <param name="copyTotalAmount">Maximum total amount in USDT. The maximum total amount you'll invest at any given time across all orders in this copy trade. You won’t copy new orders if you exceed this amount</param>
    /// <param name="positionCloseType">Action type for open positions</param>
    /// <param name="instrumentType">Instrument type</param>
    /// <param name="instrumentIds">Instrument ID.</param>
    /// <param name="copyMode">Copy mode</param>
    /// <param name="copyAmount">Copy amount per order in USDT.</param>
    /// <param name="copyRatio">Copy ratio per order.</param>
    /// <param name="takeProfitRatio">Take profit per order. 0.1 represents 10%</param>
    /// <param name="stopLossRatio">Stop loss per order. 0.1 represents 10%</param>
    /// <param name="stopLossTotalAmount">Total stop loss in USDT for trader. If your net loss (total profit - total loss) reaches this amount, you'll stop copying this trader</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxBooleanResponse>> AmendCopySettingsAsync(
        string uniqueCode,
        OkxCopyTradingMarginMode copyMarginMode,
        OkxCopyTradingInstrumentIdType copyInstrumentIdType,
        decimal copyTotalAmount,
        OkxCopyTradingPositionCloseType positionCloseType,
        OkxInstrumentType? instrumentType = null,
        IEnumerable<string>? instrumentIds = null,
        OkxCopyTradingMode? copyMode = null,
        decimal? copyAmount = null,
        decimal? copyRatio = null,
        decimal? takeProfitRatio = null,
        decimal? stopLossRatio = null,
        decimal? stopLossTotalAmount = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "uniqueCode", uniqueCode },
            { "copyTotalAmt", copyTotalAmount.ToOkxString() },
        };
        parameters.AddEnum("copyMgnMode", copyMarginMode);
        parameters.AddEnum("copyInstIdType", copyInstrumentIdType);
        parameters.AddEnum("subPosCloseType", positionCloseType);
        parameters.AddOptionalEnum("instType", instrumentType);
        parameters.AddOptionalEnum("copyMode", copyMode);
        if (instrumentIds != null) parameters.AddOptional("instId", string.Join(",", instrumentIds));
        parameters.AddOptional("copyAmt", copyAmount?.ToOkxString());
        parameters.AddOptional("copyRatio", copyRatio?.ToOkxString());
        parameters.AddOptional("tpRatio", takeProfitRatio?.ToOkxString());
        parameters.AddOptional("slRatio", stopLossRatio?.ToOkxString());
        parameters.AddOptional("slTotalAmt", stopLossTotalAmount?.ToOkxString());

        return ProcessOneRequestAsync<OkxBooleanResponse>(GetUri("api/v5/copytrading/amend-copy-settings"), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// You need to use this endpoint to stop copy trading
    /// </summary>
    /// <param name="uniqueCode">Lead trader unique code. A combination of case-sensitive alphanumerics, all numbers and the length is 16 characters, e.g. 213E8C92DC61EFAC</param>
    /// <param name="positionCloseType">Action type for open positions, it is required</param>
    /// <param name="instrumentType">Instrument type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxBooleanResponse>> StopCopyingAsync(
        string uniqueCode,
        OkxCopyTradingPositionCloseType positionCloseType,
        OkxInstrumentType? instrumentType = null,
        CancellationToken ct = default)
    {
        if (instrumentType.IsNotIn(OkxInstrumentType.Swap))
            throw new ArgumentException("Instrument type must be Swap", nameof(instrumentType));

        var parameters = new ParameterCollection
        {
            { "uniqueCode", uniqueCode },
        };
        parameters.AddEnum("subPosCloseType", positionCloseType);
        parameters.AddOptionalEnum("instType", instrumentType);

        return ProcessOneRequestAsync<OkxBooleanResponse>(GetUri("api/v5/copytrading/stop-copy-trading"), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Retrieve the copy settings about certain lead trader.
    /// </summary>
    /// <param name="uniqueCode">Lead trader unique code. A combination of case-sensitive alphanumerics, all numbers and the length is 16 characters, e.g. 213E8C92DC61EFAC</param>
    /// <param name="instrumentType">Instrument type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxCopyTradingCopySettings>> GetCopySettingsAsync(
        string uniqueCode,
        OkxInstrumentType? instrumentType = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "uniqueCode", uniqueCode },
        };
        parameters.AddOptionalEnum("instType", instrumentType);

        return ProcessOneRequestAsync<OkxCopyTradingCopySettings>(GetUri("api/v5/copytrading/copy-settings"), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve my lead traders.
    /// </summary>
    /// <param name="instrumentType">Instrument type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxCopyTradingLeadTrader>>> GetMyLeadTradersAsync(
        OkxInstrumentType? instrumentType = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptionalEnum("instType", instrumentType);

        return ProcessListRequestAsync<OkxCopyTradingLeadTrader>(GetUri("api/v5/copytrading/current-lead-traders"), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Public endpoint. Retrieve copy trading parameter configuration information of copy settings
    /// </summary>
    /// <param name="instrumentType">Instrument type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxCopyTradingPublicConfiguration>> GetPublicConfigurationAsync(
        OkxInstrumentType? instrumentType = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptionalEnum("instType", instrumentType);

        return ProcessOneRequestAsync<OkxCopyTradingPublicConfiguration>(GetUri("api/v5/copytrading/public-config"), HttpMethod.Get, ct, signed: false, queryParameters: parameters);
    }

    /// <summary>
    /// Public endpoint. Retrieve lead trader ranks.
    /// </summary>
    /// <param name="instrumentType">Instrument type</param>
    /// <param name="sortType">Sort type</param>
    /// <param name="state">Lead trader state</param>
    /// <param name="minLeadDays">Minimum lead days</param>
    /// <param name="minAssets">Minimum assets in USDT</param>
    /// <param name="maxAssets">Maximum assets in USDT</param>
    /// <param name="minAum">Minimum assets in USDT under management.</param>
    /// <param name="maxAum">Maximum assets in USDT under management.</param>
    /// <param name="dataVersion">Data version. It is 14 numbers. e.g. 20231010182400. Generally, it is used for pagination
    /// A new version will be generated every 10 minutes. Only last 5 versions are stored
    /// The default is latest version. If it is not exist, error will not be throwed and the latest version will be used.</param>
    /// <param name="page">Page for pagination</param>
    /// <param name="limit">Number of results per request. The maximum is 20; the default is 10</param>
    /// <param name="ct"></param>
    /// <returns>Cancellation Token</returns>
    public Task<RestCallResult<List<OkxCopyTradingLeadTradersRanks>>> GetLeadTradersRanksAsync(
        OkxInstrumentType? instrumentType = null,
        string? sortType = null,
        string? state = null,
        string? minLeadDays = null,
        decimal? minAssets = null,
        decimal? maxAssets = null,
        decimal? minAum = null,
        decimal? maxAum = null,
        string? dataVersion = null,
        int? page = null,
        int limit = 10,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 20);
        var parameters = new ParameterCollection();
        parameters.AddOptionalEnum("instType", instrumentType);
        parameters.AddOptional("sortType", sortType);
        parameters.AddOptional("state", state);
        parameters.AddOptional("minLeadDays", minLeadDays);
        parameters.AddOptional("minAssets", minAssets?.ToOkxString());
        parameters.AddOptional("maxAssets", maxAssets?.ToOkxString());
        parameters.AddOptional("minAum", minAum?.ToOkxString());
        parameters.AddOptional("maxAum", maxAum?.ToOkxString());
        parameters.AddOptional("dataVer", dataVersion);
        parameters.AddOptional("page", page?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxCopyTradingLeadTradersRanks>(GetUri("api/v5/copytrading/public-lead-traders"), HttpMethod.Get, ct, signed: false, queryParameters: parameters);
    }

    /// <summary>
    /// Public endpoint. Retrieve lead trader weekly pnl. Results are returned in counter chronological order.
    /// </summary>
    /// <param name="uniqueCode">Lead trader unique code. A combination of case-sensitive alphanumerics, all numbers and the length is 16 characters, e.g. 213E8C92DC61EFAC</param>
    /// <param name="instrumentType">Instrument type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxCopyTradingLeadTraderPnl>>> GetLeadTraderWeeklyPnlAsync(
        string uniqueCode,
        OkxInstrumentType? instrumentType = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("uniqueCode", uniqueCode);
        parameters.AddOptionalEnum("instType", instrumentType);

        return ProcessListRequestAsync<OkxCopyTradingLeadTraderPnl>(GetUri("api/v5/copytrading/public-weekly-pnl"), HttpMethod.Get, ct, signed: false, queryParameters: parameters);
    }

    /// <summary>
    /// Public endpoint. Retrieve lead trader daily pnl. Results are returned in counter chronological order.
    /// </summary>
    /// <param name="uniqueCode">Lead trader unique code. A combination of case-sensitive alphanumerics, all numbers and the length is 16 characters, e.g. 213E8C92DC61EFAC</param>
    /// <param name="lastDays">Last days</param>
    /// <param name="instrumentType">Instrument type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxCopyTradingLeadTraderPnl>>> GetLeadTraderDailyPnlAsync(
        string uniqueCode,
        string lastDays,
        OkxInstrumentType? instrumentType = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("uniqueCode", uniqueCode);
        parameters.AddOptional("lastDays", lastDays);
        parameters.AddOptionalEnum("instType", instrumentType);

        return ProcessListRequestAsync<OkxCopyTradingLeadTraderPnl>(GetUri("api/v5/copytrading/public-pnl"), HttpMethod.Get, ct, signed: false, queryParameters: parameters);
    }

    /// <summary>
    /// Public endpoint. Key data related to lead trader performance.
    /// </summary>
    /// <param name="uniqueCode">Lead trader unique code. A combination of case-sensitive alphanumerics, all numbers and the length is 16 characters, e.g. 213E8C92DC61EFAC</param>
    /// <param name="lastDays">Last days</param>
    /// <param name="instrumentType">Instrument type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxCopyTradingLeadTraderStats>>> GetLeadTraderStatsAsync(
        string uniqueCode,
        string lastDays,
        OkxInstrumentType? instrumentType = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("uniqueCode", uniqueCode);
        parameters.AddOptional("lastDays", lastDays);
        parameters.AddOptionalEnum("instType", instrumentType);

        return ProcessListRequestAsync<OkxCopyTradingLeadTraderStats>(GetUri("api/v5/copytrading/public-stats"), HttpMethod.Get, ct, signed: false, queryParameters: parameters);
    }

    /// <summary>
    /// Public endpoint. The most frequently traded crypto of this lead trader. Results are sorted by ratio from large to small.
    /// </summary>
    /// <param name="uniqueCode">Lead trader unique code. A combination of case-sensitive alphanumerics, all numbers and the length is 16 characters, e.g. 213E8C92DC61EFAC</param>
    /// <param name="instrumentType">Instrument type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxCopyTradingLeadTraderCurrencyPreference>>> GetLeadTraderCurrencyPreferencesAsync(
        string uniqueCode,
        OkxInstrumentType? instrumentType = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("uniqueCode", uniqueCode);
        parameters.AddOptionalEnum("instType", instrumentType);

        return ProcessListRequestAsync<OkxCopyTradingLeadTraderCurrencyPreference>(GetUri("api/v5/copytrading/public-preference-currency"), HttpMethod.Get, ct, signed: false, queryParameters: parameters);
    }

    /// <summary>
    /// Public endpoint. Get current leading positions of lead trader
    /// </summary>
    /// <param name="uniqueCode">Lead trader unique code. A combination of case-sensitive alphanumerics, all numbers and the length is 16 characters, e.g. 213E8C92DC61EFAC</param>
    /// <param name="instrumentType">Instrument type</param>
    /// <param name="after">Pagination of data to return records earlier than the requested subPosId.</param>
    /// <param name="before">Pagination of data to return records newer than the requested subPosId.</param>
    /// <param name="limit">Number of results per request. Maximum is 100. Default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxCopyTradingLeadTraderPosition>>> GetLeadTraderCurrentPositionsAsync(
        string uniqueCode,
        OkxInstrumentType? instrumentType = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new ParameterCollection();
        parameters.AddOptionalEnum("instType", instrumentType);
        parameters.AddOptional("uniqueCode", uniqueCode);
        parameters.AddOptional("after", after?.ToOkxString());
        parameters.AddOptional("before", before?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxCopyTradingLeadTraderPosition>(GetUri("api/v5/copytrading/public-current-subpositions"), HttpMethod.Get, ct, signed: false, queryParameters: parameters);
    }

    /// <summary>
    /// Public endpoint. Retrieve the lead trader completed leading position of the last 3 months.
    /// Returns reverse chronological order with subPosId.
    /// </summary>
    /// <param name="uniqueCode">Lead trader unique code. A combination of case-sensitive alphanumerics, all numbers and the length is 16 characters, e.g. 213E8C92DC61EFAC</param>
    /// <param name="instrumentType">Instrument type</param>
    /// <param name="after">Pagination of data to return records earlier than the requested subPosId.</param>
    /// <param name="before">Pagination of data to return records newer than the requested subPosId.</param>
    /// <param name="limit">Number of results per request. Maximum is 100. Default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxCopyTradingLeadTraderPositionHistory>>> GetLeadTraderPositionHistoryAsync(
        string uniqueCode,
        OkxInstrumentType? instrumentType = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new ParameterCollection();
        parameters.AddOptionalEnum("instType", instrumentType);
        parameters.AddOptional("uniqueCode", uniqueCode);
        parameters.AddOptional("after", after?.ToOkxString());
        parameters.AddOptional("before", before?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxCopyTradingLeadTraderPositionHistory>(GetUri("api/v5/copytrading/public-subpositions-history"), HttpMethod.Get, ct, signed: false, queryParameters: parameters);
    }

    /// <summary>
    /// Public endpoint. Retrieve copy trader coming from certain lead trader. Return according to pnl from high to low
    /// </summary>
    /// <param name="uniqueCode">Lead trader unique code. A combination of case-sensitive alphanumerics, all numbers and the length is 16 characters, e.g. 213E8C92DC61EFAC</param>
    /// <param name="instrumentType">Instrument type</param>
    /// <param name="limit">Number of results per request. Maximum is 100. Default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxCopyTradingCopyTrader>>> GetCopyTradersAsync(
        string uniqueCode,
        OkxInstrumentType? instrumentType = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new ParameterCollection();
        parameters.AddOptionalEnum("instType", instrumentType);
        parameters.AddOptional("uniqueCode", uniqueCode);
        parameters.AddOptional("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxCopyTradingCopyTrader>(GetUri("api/v5/copytrading/public-copy-traders"), HttpMethod.Get, ct, signed: false, queryParameters: parameters);
    }
}