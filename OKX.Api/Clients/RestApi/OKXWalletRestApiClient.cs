namespace OKX.Api.Clients.RestApi;

public class OKXWalletRestApiClient : OKXBaseRestApiClient
{
    // Endpoints
    protected const string Endpoints_V5_Account_Balance = "api/v5/account/balance";
    protected const string Endpoints_V5_Account_Positions = "api/v5/account/positions";
    protected const string Endpoints_V5_Account_PositionsHistory = "api/v5/account/positions-history";
    protected const string Endpoints_V5_Account_PositionRisk = "api/v5/account/account-position-risk";
    protected const string Endpoints_V5_Account_Bills = "api/v5/account/bills";
    protected const string Endpoints_V5_Account_BillsArchive = "api/v5/account/bills-archive";
    protected const string Endpoints_V5_Account_Config = "api/v5/account/config";
    protected const string Endpoints_V5_Account_SetPositionMode = "api/v5/account/set-position-mode";
    protected const string Endpoints_V5_Account_SetLeverage = "api/v5/account/set-leverage";
    protected const string Endpoints_V5_Account_MaxSize = "api/v5/account/max-size";
    protected const string Endpoints_V5_Account_MaxAvailSize = "api/v5/account/max-avail-size";
    protected const string Endpoints_V5_Account_PositionMarginBalance = "api/v5/account/position/margin-balance";
    protected const string Endpoints_V5_Account_LeverageInfo = "api/v5/account/leverage-info";
    protected const string Endpoints_V5_Account_MaxLoan = "api/v5/account/max-loan";
    protected const string Endpoints_V5_Account_TradeFee = "api/v5/account/trade-fee";
    protected const string Endpoints_V5_Account_InterestAccrued = "api/v5/account/interest-accrued";
    protected const string Endpoints_V5_Account_InterestRate = "api/v5/account/interest-rate";
    protected const string Endpoints_V5_Account_SetGreeks = "api/v5/account/set-greeks";
    protected const string Endpoints_V5_Account_MaxWithdrawal = "api/v5/account/max-withdrawal";

    internal OKXWalletRestApiClient(OKXRestApiClient root) : base(root)
    {
    }

    #region Account API Endpoints
    /// <summary>
    /// Retrieve a list of assets (with non-zero balance), remaining balance, and available amount in the account.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<OkxAccountBalance>> GetAccountBalanceAsync(string currency = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("ccy", currency);

        return await SendOKXSingleRequest<OkxAccountBalance>(RootClient.GetUri(Endpoints_V5_Account_Balance), HttpMethod.Get, ct, signed: true, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve information on your positions. When the account is in net mode, net positions will be displayed, and when the account is in long/short mode, long or short positions will be displayed.
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="positionId">Position ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<OkxPosition>>> GetAccountPositionsAsync(
        OkxInstrumentType? instrumentType = null,
        string instrumentId = null,
        string positionId = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)));
        parameters.AddOptionalParameter("instId", instrumentId);
        parameters.AddOptionalParameter("posId", positionId);

        return await SendOKXRequest<IEnumerable<OkxPosition>>(RootClient.GetUri(Endpoints_V5_Account_Positions), HttpMethod.Get, ct, signed: true, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve the updated position data for the last 3 months. Return in reverse chronological order using utime.
    /// </summary>
    /// <param name="instrumentType">Instrument type</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="marginMode">Margin mode</param>
    /// <param name="type">The type of closing position. It is the latest type if there are several types for the same position.</param>
    /// <param name="positionId">Position ID</param>
    /// <param name="after">Pagination of data to return records earlier than the requested uTime, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records earlier than the requested uTime, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100. The default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public virtual async Task<RestCallResult<IEnumerable<OkxClosingPosition>>> GetAccountPositionsHistoryAsync(
        OkxInstrumentType? instrumentType = null,
        string instrumentId = null,
        OkxMarginMode? marginMode = null,
        OkxClosingPositionType? type = null,
        string positionId = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)));
        parameters.AddOptionalParameter("instId", instrumentId);
        parameters.AddOptionalParameter("mgnMode", JsonConvert.SerializeObject(marginMode, new MarginModeConverter(false)));
        parameters.AddOptionalParameter("type", JsonConvert.SerializeObject(type, new ClosingPositionTypeConverter(false)));
        parameters.AddOptionalParameter("posId", positionId);
        parameters.AddOptionalParameter("after", after?.ToString());
        parameters.AddOptionalParameter("before", before?.ToString());
        parameters.AddOptionalParameter("limit", limit.ToString());

        return await SendOKXRequest<IEnumerable<OkxClosingPosition>>(RootClient.GetUri(Endpoints_V5_Account_PositionsHistory), HttpMethod.Get, ct, signed: true, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Get account and position risk
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<OkxPositionRisk>>> GetAccountPositionRiskAsync(OkxInstrumentType? instrumentType = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)));

        return await SendOKXRequest<IEnumerable<OkxPositionRisk>>(RootClient.GetUri(Endpoints_V5_Account_PositionRisk), HttpMethod.Get, ct, signed: true, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve the bills of the account. The bill refers to all transaction records that result in changing the balance of an account. Pagination is supported, and the response is sorted with the most recent first. This endpoint can retrieve data from the last 7 days.
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="currency">Currency</param>
    /// <param name="marginMode">Margin Mode</param>
    /// <param name="contractType">Contract Type</param>
    /// <param name="billType">Bill Type</param>
    /// <param name="billSubType">Bill Sub Type</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<OkxAccountBill>>> GetBillHistoryAsync(
        OkxInstrumentType? instrumentType = null,
        string currency = null,
        OkxMarginMode? marginMode = null,
        OkxContractType? contractType = null,
        OkxAccountBillType? billType = null,
        OkxAccountBillSubType? billSubType = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("ccy", currency);
        parameters.AddOptionalParameter("after", after?.ToString());
        parameters.AddOptionalParameter("before", before?.ToString());
        parameters.AddOptionalParameter("limit", limit.ToString());
        parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)));
        parameters.AddOptionalParameter("mgnMode", JsonConvert.SerializeObject(marginMode, new MarginModeConverter(false)));
        parameters.AddOptionalParameter("ctType", JsonConvert.SerializeObject(contractType, new ContractTypeConverter(false)));
        parameters.AddOptionalParameter("type", JsonConvert.SerializeObject(billType, new AccountBillTypeConverter(false)));
        parameters.AddOptionalParameter("subType", JsonConvert.SerializeObject(billSubType, new AccountBillSubTypeConverter(false)));

        return await SendOKXRequest<IEnumerable<OkxAccountBill>>(RootClient.GetUri(Endpoints_V5_Account_Bills), HttpMethod.Get, ct, signed: true, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve the account’s bills. The bill refers to all transaction records that result in changing the balance of an account. Pagination is supported, and the response is sorted with most recent first. This endpoint can retrieve data from the last 3 months.
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="currency">Currency</param>
    /// <param name="marginMode">Margin Mode</param>
    /// <param name="contractType">Contract Type</param>
    /// <param name="billType">Bill Type</param>
    /// <param name="billSubType">Bill Sub Type</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<OkxAccountBill>>> GetBillArchiveAsync(
        OkxInstrumentType? instrumentType = null,
        string currency = null,
        OkxMarginMode? marginMode = null,
        OkxContractType? contractType = null,
        OkxAccountBillType? billType = null,
        OkxAccountBillSubType? billSubType = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("ccy", currency);
        parameters.AddOptionalParameter("after", after?.ToString());
        parameters.AddOptionalParameter("before", before?.ToString());
        parameters.AddOptionalParameter("limit", limit.ToString());
        parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)));
        parameters.AddOptionalParameter("mgnMode", JsonConvert.SerializeObject(marginMode, new MarginModeConverter(false)));
        parameters.AddOptionalParameter("ctType", JsonConvert.SerializeObject(contractType, new ContractTypeConverter(false)));
        parameters.AddOptionalParameter("type", JsonConvert.SerializeObject(billType, new AccountBillTypeConverter(false)));
        parameters.AddOptionalParameter("subType", JsonConvert.SerializeObject(billSubType, new AccountBillSubTypeConverter(false)));

        return await SendOKXRequest<IEnumerable<OkxAccountBill>>(RootClient.GetUri(Endpoints_V5_Account_BillsArchive), HttpMethod.Get, ct, signed: true, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve current account configuration.
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<OkxAccountConfiguration>> GetAccountConfigurationAsync(CancellationToken ct = default)
    {
        return await SendOKXSingleRequest<OkxAccountConfiguration>(RootClient.GetUri(Endpoints_V5_Account_Config), HttpMethod.Get, ct, true).ConfigureAwait(false);
    }

    /// <summary>
    /// FUTURES and SWAP support both long/short mode and net mode. In net mode, users can only have positions in one direction; In long/short mode, users can hold positions in long and short directions.
    /// </summary>
    /// <param name="positionMode"></param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<OkxAccountPositionMode>> SetAccountPositionModeAsync(OkxPositionMode positionMode, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            {"posMode", JsonConvert.SerializeObject(positionMode, new PositionModeConverter(false)) },
        };

        return await SendOKXSingleRequest<OkxAccountPositionMode>(RootClient.GetUri(Endpoints_V5_Account_SetPositionMode), HttpMethod.Post, ct, signed: true, bodyParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Get Leverage
    /// </summary>
    /// <param name="instrumentIds">Single instrument ID or multiple instrument IDs (no more than 20) separated with comma</param>
    /// <param name="marginMode">Margin Mode</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<OkxLeverage>>> GetAccountLeverageAsync(
        string instrumentIds,
        OkxMarginMode marginMode,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            {"instId", instrumentIds },
            {"mgnMode", JsonConvert.SerializeObject(marginMode, new MarginModeConverter(false)) },
        };

        return await SendOKXRequest<IEnumerable<OkxLeverage>>(RootClient.GetUri(Endpoints_V5_Account_LeverageInfo), HttpMethod.Get, ct, signed: true, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// The following are the setting leverage cases for an instrument:
    /// Set leverage for isolated MARGIN at pairs level.
    /// Set leverage for cross MARGIN in Single-currency margin at pairs level.
    /// Set leverage for cross MARGIN in Multi-currency margin at currency level.
    /// Set leverage for cross/isolated FUTURES/SWAP at underlying/contract level.
    /// </summary>
    /// <param name="leverage">Leverage</param>
    /// <param name="currency">Currency</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="marginMode">Margin Mode</param>
    /// <param name="positionSide">Position Side</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<OkxLeverage>>> SetAccountLeverageAsync(
        int leverage,
        string currency = null,
        string instrumentId = null,
        OkxMarginMode? marginMode = null,
        OkxPositionSide? positionSide = null,
        CancellationToken ct = default)
    {
        if (leverage < 1)
            throw new ArgumentException("Invalid Leverage");

        if (string.IsNullOrEmpty(currency) && string.IsNullOrEmpty(instrumentId))
            throw new ArgumentException("Either instId or ccy is required; if both are passed, instId will be used by default.");

        if (marginMode == null)
            throw new ArgumentException("marginMode is required");

        var parameters = new Dictionary<string, object> {
            {"lever", leverage.ToString() },
            {"mgnMode", JsonConvert.SerializeObject(marginMode, new MarginModeConverter(false)) },
        };
        parameters.AddOptionalParameter("ccy", currency);
        parameters.AddOptionalParameter("instId", instrumentId);
        parameters.AddOptionalParameter("posSide", JsonConvert.SerializeObject(positionSide, new PositionSideConverter(false)));

        return await SendOKXRequest<IEnumerable<OkxLeverage>>(RootClient.GetUri(Endpoints_V5_Account_SetLeverage), HttpMethod.Post, ct, signed: true, bodyParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Get maximum buy/sell amount or open amount
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="tradeMode">Trade Mode</param>
    /// <param name="currency">Currency</param>
    /// <param name="price">Price</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<OkxMaximumAmount>>> GetMaximumAmountAsync(
        string instrumentId,
        OkxTradeMode tradeMode,
        string currency = null,
        decimal? price = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            {"instId", instrumentId },
            {"tdMode", JsonConvert.SerializeObject(tradeMode, new TradeModeConverter(false)) },
        };
        parameters.AddOptionalParameter("ccy", currency);
        parameters.AddOptionalParameter("px", price?.ToString(OkxGlobals.OkxCultureInfo));

        return await SendOKXRequest<IEnumerable<OkxMaximumAmount>>(RootClient.GetUri(Endpoints_V5_Account_MaxSize), HttpMethod.Get, ct, signed: true, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Get Maximum Available Tradable Amount
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="tradeMode">Trade Mode</param>
    /// <param name="currency">Currency</param>
    /// <param name="reduceOnly">Reduce Only</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<OkxMaximumAvailableAmount>>> GetMaximumAvailableAmountAsync(
        string instrumentId,
        OkxTradeMode tradeMode,
        string currency = null,
        bool? reduceOnly = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            {"instId", instrumentId },
            {"tdMode", JsonConvert.SerializeObject(tradeMode, new TradeModeConverter(false)) },
        };
        parameters.AddOptionalParameter("ccy", currency);
        parameters.AddOptionalParameter("reduceOnly", reduceOnly);

        return await SendOKXRequest<IEnumerable<OkxMaximumAvailableAmount>>(RootClient.GetUri(Endpoints_V5_Account_MaxAvailSize), HttpMethod.Get, ct, signed: true, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Increase or decrease the margin of the isolated position.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="positionSide">Position Side</param>
    /// <param name="marginAddReduce">Type</param>
    /// <param name="amount">Amount</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<OkxMarginAmount>>> SetMarginAmountAsync(
        string instrumentId,
        OkxPositionSide positionSide,
        OkxMarginAddReduce marginAddReduce,
        decimal amount,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            {"instId", instrumentId },
            {"posSide", JsonConvert.SerializeObject(positionSide, new PositionSideConverter(false)) },
            {"type", JsonConvert.SerializeObject(marginAddReduce, new MarginAddReduceConverter(false)) },
            {"amt", amount.ToString(OkxGlobals.OkxCultureInfo) },
        };

        return await SendOKXRequest<IEnumerable<OkxMarginAmount>>(RootClient.GetUri(Endpoints_V5_Account_PositionMarginBalance), HttpMethod.Post, ct, signed: true, bodyParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Get the maximum loan of instrument
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="marginMode">Margin Mode</param>
    /// <param name="marginCurrency">Margin Currency</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<OkxMaximumLoanAmount>>> GetMaximumLoanAmountAsync(
        string instrumentId,
        OkxMarginMode marginMode,
        string marginCurrency = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            {"instId", instrumentId },
            {"mgnMode", JsonConvert.SerializeObject(marginMode, new MarginModeConverter(false)) },
        };
        parameters.AddOptionalParameter("mgnCcy", marginCurrency);

        return await SendOKXRequest<IEnumerable<OkxMaximumLoanAmount>>(RootClient.GetUri(Endpoints_V5_Account_MaxLoan), HttpMethod.Get, ct, signed: true, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Get Fee Rates
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="category">Category</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<OkxFeeRate>> GetFeeRatesAsync(
        OkxInstrumentType instrumentType,
        string instrumentId = null,
        string underlying = null,
        OkxFeeRateCategory? category = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            {"instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)) },
        };
        parameters.AddOptionalParameter("instId", instrumentId);
        parameters.AddOptionalParameter("uly", underlying);
        parameters.AddOptionalParameter("category", JsonConvert.SerializeObject(category, new FeeRateCategoryConverter(false)));

        return await SendOKXSingleRequest<OkxFeeRate>(RootClient.GetUri(Endpoints_V5_Account_TradeFee), HttpMethod.Get, ct, signed: true, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Get interest-accrued
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="currency">Currency</param>
    /// <param name="marginMode">Margin Mode</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<OkxInterestAccrued>>> GetInterestAccruedAsync(
        string instrumentId = null,
        string currency = null,
        OkxMarginMode? marginMode = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("instId", instrumentId);
        parameters.AddOptionalParameter("ccy", currency);
        parameters.AddOptionalParameter("after", after?.ToString());
        parameters.AddOptionalParameter("before", before?.ToString());
        parameters.AddOptionalParameter("limit", limit.ToString());
        parameters.AddOptionalParameter("mgnMode", JsonConvert.SerializeObject(marginMode, new MarginModeConverter(false)));

        return await SendOKXRequest<IEnumerable<OkxInterestAccrued>>(RootClient.GetUri(Endpoints_V5_Account_InterestAccrued), HttpMethod.Get, ct, signed: true, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Get the user's current leveraged currency borrowing interest rate
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<Models.Account.OkxInterestRate>>> GetInterestRateAsync(
        string currency = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("ccy", currency);

        return await SendOKXRequest<IEnumerable<Models.Account.OkxInterestRate>>(RootClient.GetUri(Endpoints_V5_Account_InterestRate), HttpMethod.Get, ct, signed: true, queryParameters: parameters).ConfigureAwait(false);
    }

    // TODO: Get Greeks

    /// <summary>
    /// Set the display type of Greeks.
    /// </summary>
    /// <param name="greeksType">Display type of Greeks.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<OkxAccountGreeksType>> SetGreeksAsync(Enums.OkxGreeksType greeksType, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            {"greeksType", JsonConvert.SerializeObject(greeksType, new GreeksTypeConverter(false)) },
        };

        return await SendOKXSingleRequest<OkxAccountGreeksType>(RootClient.GetUri(Endpoints_V5_Account_SetGreeks), HttpMethod.Post, ct, signed: true, bodyParameters: parameters).ConfigureAwait(false);
    }

    // TODO: Isolated margin trading settings

    /// <summary>
    /// Retrieve the maximum transferable amount.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<OkxWithdrawalAmount>>> GetMaximumWithdrawalsAsync(
        string currency = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("ccy", currency);

        return await SendOKXRequest<IEnumerable<OkxWithdrawalAmount>>(RootClient.GetUri(Endpoints_V5_Account_MaxWithdrawal), HttpMethod.Get, ct, signed: true, queryParameters: parameters).ConfigureAwait(false);
    }


    // TODO: Get account risk state
    // TODO: VIP loans borrow and repay
    // TODO: Get borrow and repay history for VIP loans
    // TODO: Get borrow interest and limit
    // TODO: Position builder
    // TODO: Get PM limitation


    #endregion

}