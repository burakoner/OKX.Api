using OKX.Api.Account.Converters;
using OKX.Api.Account.Enums;
using OKX.Api.Account.Models;

namespace OKX.Api.Account.Clients;

/// <summary>
/// OKX Trading Account Rest Api Client
/// </summary>
public class OkxAccountRestClient : OKXRestApiBaseClient
{
    // Endpoints
    private const string v5AccountBalance = "api/v5/account/balance";
    private const string v5AccountPositions = "api/v5/account/positions";
    private const string v5AccountPositionsHistory = "api/v5/account/positions-history";
    private const string v5AccountPositionRisk = "api/v5/account/account-position-risk";
    private const string v5AccountBills = "api/v5/account/bills";
    private const string v5AccountBillsArchive = "api/v5/account/bills-archive";
    private const string v5AccountConfig = "api/v5/account/config";
    private const string v5AccountSetPositionMode = "api/v5/account/set-position-mode";
    private const string v5AccountSetLeverage = "api/v5/account/set-leverage";
    private const string v5AccountMaxSize = "api/v5/account/max-size";
    private const string v5AccountMaxAvailSize = "api/v5/account/max-avail-size";
    private const string v5AccountPositionMarginBalance = "api/v5/account/position/margin-balance";
    private const string v5AccountLeverageInfo = "api/v5/account/leverage-info";
    private const string v5AccountAdjustLeverageInfo = "api/v5/account/adjust-leverage-info";
    private const string v5AccountMaxLoan = "api/v5/account/max-loan";
    private const string v5AccountTradeFee = "api/v5/account/trade-fee";
    private const string v5AccountInterestAccrued = "api/v5/account/interest-accrued";
    private const string v5AccountInterestRate = "api/v5/account/interest-rate";
    private const string v5AccountSetGreeks = "api/v5/account/set-greeks";
    private const string v5AccountSetIsolatedMode = "api/v5/account/set-isolated-mode";
    private const string v5AccountMaxWithdrawal = "api/v5/account/max-withdrawal";

    private const string v5AccountRiskState = "api/v5/account/risk-state";
    private const string v5AccountQuickMarginBorrowRepay = "api/v5/account/quick-margin-borrow-repay";
    private const string v5AccountQuickMarginBorrowRepayHistory = "api/v5/account/quick-margin-borrow-repay-history";
    private const string v5AccountBorrowRepay = "api/v5/account/borrow-repay";
    private const string v5AccountBorrowRepayHistory = "api/v5/account/borrow-repay-history";
    private const string v5AccountVipInterestAccrued = "api/v5/account/vip-interest-accrued";
    private const string v5AccountVipInterestDeducted = "api/v5/account/vip-interest-deducted";
    private const string v5AccountVipLoanOrderList = "api/v5/account/vip-loan-order-list";
    private const string v5AccountVipLoanOrderDetail = "api/v5/account/vip-loan-order-detail";
    private const string v5AccountInterestLimits = "api/v5/account/interest-limits";
    private const string v5AccountSimulatedMargin = "api/v5/account/simulated_margin";
    private const string v5AccountPositionBuilder = "api/v5/account/position-builder";
    private const string v5AccountGreeks = "api/v5/account/greeks";
    private const string v5AccountPositionTiers = "api/v5/account/position-tiers";
    private const string v5AccountSetRiskOffsetType = "api/v5/account/set-riskOffset-type";
    private const string v5AccountActivateOption = "api/v5/account/activate-option";
    private const string v5AccountSetAutoLoan = "api/v5/account/set-auto-loan";
    private const string v5AccountSetAccountLevel = "api/v5/account/set-account-level";
    private const string v5AccountMmpReset = "api/v5/account/mmp-reset";
    private const string v5AccountMmpConfig = "api/v5/account/mmp-config";

    internal OkxAccountRestClient(OkxRestApiClient root) : base(root)
    {
    }

    #region Account API Endpoints
    /// <summary>
    /// Retrieve a list of assets (with non-zero balance), remaining balance, and available amount in the account.
    /// </summary>
    /// <param name="currencies">Currencies</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxAccountBalance>> GetBalancesAsync(IEnumerable<string> currencies = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        if (currencies != null && currencies.Count() > 0)
            parameters.AddOptionalParameter("ccy", string.Join(",", currencies));

        return ProcessFirstOrDefaultRequestAsync<OkxAccountBalance>(GetUri(v5AccountBalance), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve information on your positions. When the account is in net mode, net positions will be displayed, and when the account is in long/short mode, long or short positions will be displayed.
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="positionId">Position ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxPosition>>> GetPositionsAsync(
        OkxInstrumentType? instrumentType = null,
        string instrumentId = null,
        string positionId = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new OkxInstrumentTypeConverter(false)));
        parameters.AddOptionalParameter("instId", instrumentId);
        parameters.AddOptionalParameter("posId", positionId);

        return ProcessListRequestAsync<OkxPosition>(GetUri(v5AccountPositions), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
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
    public Task<RestCallResult<List<OkxPositionHistory>>> GetPositionsHistoryAsync(
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
        parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new OkxInstrumentTypeConverter(false)));
        parameters.AddOptionalParameter("instId", instrumentId);
        parameters.AddOptionalParameter("mgnMode", JsonConvert.SerializeObject(marginMode, new OkxMarginModeConverter(false)));
        parameters.AddOptionalParameter("type", JsonConvert.SerializeObject(type, new ClosingPositionTypeConverter(false)));
        parameters.AddOptionalParameter("posId", positionId);
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxPositionHistory>(GetUri(v5AccountPositionsHistory), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Get account and position risk
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxPositionBalance>>> GetPositionRiskAsync(OkxInstrumentType? instrumentType = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new OkxInstrumentTypeConverter(false)));

        return ProcessListRequestAsync<OkxPositionBalance>(GetUri(v5AccountPositionRisk), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
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
    /// <param name="begin">Filter with a begin timestamp. Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="end">Filter with an end timestamp. Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxBill>>> GetBillHistoryAsync(
        OkxInstrumentType? instrumentType = null,
        string currency = null,
        OkxMarginMode? marginMode = null,
        OkxContractType? contractType = null,
        OkxBillType? billType = null,
        OkxBillSubType? billSubType = null,
        long? after = null,
        long? before = null,
        long? begin = null,
        long? end = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new OkxInstrumentTypeConverter(false)));
        parameters.AddOptionalParameter("ccy", currency);
        parameters.AddOptionalParameter("mgnMode", JsonConvert.SerializeObject(marginMode, new OkxMarginModeConverter(false)));
        parameters.AddOptionalParameter("ctType", JsonConvert.SerializeObject(contractType, new OkxContractTypeConverter(false)));
        parameters.AddOptionalParameter("type", JsonConvert.SerializeObject(billType, new OkxBillTypeConverter(false)));
        parameters.AddOptionalParameter("subType", JsonConvert.SerializeObject(billSubType, new OkxBillSubTypeConverter(false)));
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("begin", begin?.ToOkxString());
        parameters.AddOptionalParameter("end", end?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxBill>(GetUri(v5AccountBills), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
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
    /// <param name="begin">Filter with a begin timestamp. Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="end">Filter with an end timestamp. Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxBill>>> GetBillArchiveAsync(
        OkxInstrumentType? instrumentType = null,
        string currency = null,
        OkxMarginMode? marginMode = null,
        OkxContractType? contractType = null,
        OkxBillType? billType = null,
        OkxBillSubType? billSubType = null,
        long? after = null,
        long? before = null,
        long? begin = null,
        long? end = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new OkxInstrumentTypeConverter(false)));
        parameters.AddOptionalParameter("ccy", currency);
        parameters.AddOptionalParameter("mgnMode", JsonConvert.SerializeObject(marginMode, new OkxMarginModeConverter(false)));
        parameters.AddOptionalParameter("ctType", JsonConvert.SerializeObject(contractType, new OkxContractTypeConverter(false)));
        parameters.AddOptionalParameter("type", JsonConvert.SerializeObject(billType, new OkxBillTypeConverter(false)));
        parameters.AddOptionalParameter("subType", JsonConvert.SerializeObject(billSubType, new OkxBillSubTypeConverter(false)));
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("begin", begin?.ToOkxString());
        parameters.AddOptionalParameter("end", end?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxBill>(GetUri(v5AccountBillsArchive), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve current account configuration.
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxAccountConfiguration>> GetConfigurationAsync(CancellationToken ct = default)
    {
        return ProcessFirstOrDefaultRequestAsync<OkxAccountConfiguration>(GetUri(v5AccountConfig), HttpMethod.Get, ct, true);
    }

    /// <summary>
    /// FUTURES and SWAP support both long/short mode and net mode. In net mode, users can only have positions in one direction; In long/short mode, users can hold positions in long and short directions.
    /// </summary>
    /// <param name="positionMode"></param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxAccountPositionMode>> SetPositionModeAsync(OkxPositionMode positionMode, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            {"posMode", JsonConvert.SerializeObject(positionMode, new OkxPositionModeConverter(false)) },
        };

        return ProcessFirstOrDefaultRequestAsync<OkxAccountPositionMode>(GetUri(v5AccountSetPositionMode), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// There are 10 different scenarios for leverage setting:
    /// 1. Set leverage for MARGIN instruments under isolated-margin trade mode at pairs level.
    /// 2. Set leverage for MARGIN instruments under cross-margin trade mode and Single-currency margin account mode at pairs level.
    /// 3. Set leverage for MARGIN instruments under cross-margin trade mode and Multi-currency margin at currency level.
    /// 4. Set leverage for MARGIN instruments under cross-margin trade mode and Portfolio margin at currency level.
    /// 5. Set leverage for FUTURES instruments under cross-margin trade mode at underlying level.
    /// 6. Set leverage for FUTURES instruments under isolated-margin trade mode and buy/sell position mode at contract level.
    /// 7. Set leverage for FUTURES instruments under isolated-margin trade mode and long/short position mode at contract and position side level.
    /// 8. Set leverage for SWAP instruments under cross-margin trade at contract level.
    /// 9. Set leverage for SWAP instruments under isolated-margin trade mode and buy/sell position mode at contract level.
    /// 10. Set leverage for SWAP instruments under isolated-margin trade mode and long/short position mode at contract and position side level.
    /// 
    /// Note that the request parameter posSide is only required when margin mode is isolated in long/short position mode for FUTURES/SWAP instruments (see scenario 7 and 10 above).
    /// Please refer to the request examples on the right for each case.
    /// </summary>
    /// <param name="leverage">Leverage</param>
    /// <param name="currency">Currency</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="marginMode">Margin Mode</param>
    /// <param name="positionSide">Position Side</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxLeverage>>> SetLeverageAsync(
        decimal leverage,
        string currency = null,
        string instrumentId = null,
        OkxMarginMode? marginMode = null,
        OkxPositionSide? positionSide = null,
        CancellationToken ct = default)
    {
        if (leverage < 0.01m)
            throw new ArgumentException("Invalid Leverage");

        if (string.IsNullOrEmpty(currency) && string.IsNullOrEmpty(instrumentId))
            throw new ArgumentException("Either instId or ccy is required; if both are passed, instId will be used by default.");

        if (marginMode == null)
            throw new ArgumentException("marginMode is required");

        var parameters = new Dictionary<string, object> {
            {"lever", leverage.ToOkxString() },
            {"mgnMode", JsonConvert.SerializeObject(marginMode, new OkxMarginModeConverter(false)) },
        };
        parameters.AddOptionalParameter("ccy", currency);
        parameters.AddOptionalParameter("instId", instrumentId);
        parameters.AddOptionalParameter("posSide", JsonConvert.SerializeObject(positionSide, new OkxPositionSideConverter(false)));

        return ProcessListRequestAsync<OkxLeverage>(GetUri(v5AccountSetLeverage), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Get maximum buy/sell amount or open amount
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="tradeMode">Trade Mode</param>
    /// <param name="currency">Currency</param>
    /// <param name="price">Price</param>
    /// <param name="leverage">Leverage for instrument</param>
    /// <param name="unSpotOffset">Spot-Derivatives risk offset</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxMaximumAmount>>> GetMaximumAmountAsync(
        string instrumentId,
        OkxTradeMode tradeMode,
        string currency = null,
        decimal? price = null,
        decimal? leverage = null,
        bool? unSpotOffset = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "instId", instrumentId },
            { "tdMode", JsonConvert.SerializeObject(tradeMode, new TradeModeConverter(false)) },
        };
        parameters.AddOptionalParameter("ccy", currency);
        parameters.AddOptionalParameter("px", price?.ToOkxString());
        parameters.AddOptionalParameter("leverage", leverage?.ToOkxString());
        parameters.AddOptionalParameter("unSpotOffset", unSpotOffset);

        return ProcessListRequestAsync<OkxMaximumAmount>(GetUri(v5AccountMaxSize), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Get Maximum Available Tradable Amount
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="tradeMode">Trade Mode</param>
    /// <param name="currency">Currency</param>
    /// <param name="price">The available amount corresponds to price of close position. Only applicable to reduceOnly MARGIN.</param>
    /// <param name="reduceOnly">Reduce Only</param>
    /// <param name="unSpotOffset">Spot-Derivatives risk offset</param>
    /// <param name="quickMgnType">Quick Margin type. Only applicable to Quick Margin Mode of isolated margin</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxMaximumAvailableAmount>>> GetMaximumAvailableAmountAsync(
        string instrumentId,
        OkxTradeMode tradeMode,
        string currency = null,
        decimal? price = null,
        bool? reduceOnly = null,
        bool? unSpotOffset = null,
        OkxQuickMarginType? quickMgnType = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "instId", instrumentId },
            { "tdMode", JsonConvert.SerializeObject(tradeMode, new TradeModeConverter(false)) },
        };
        parameters.AddOptionalParameter("ccy", currency);
        parameters.AddOptionalParameter("reduceOnly", reduceOnly);
        parameters.AddOptionalParameter("px", price?.ToOkxString());
        parameters.AddOptionalParameter("unSpotOffset", unSpotOffset);
        parameters.AddOptionalParameter("quickMgnType", JsonConvert.SerializeObject(quickMgnType, new QuickMarginTypeConverter(false)));

        return ProcessListRequestAsync<OkxMaximumAvailableAmount>(GetUri(v5AccountMaxAvailSize), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Increase or decrease the margin of the isolated position.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="positionSide">Position Side</param>
    /// <param name="marginAddReduce">Type</param>
    /// <param name="amount">Amount</param>
    /// <param name="currency">Currency, only applicable to MARGIN（Manual transfers and Quick Margin Mode</param>
    /// <param name="auto">Automatic loan transfer out, true or false, the default is false. Only applicable to MARGIN（Manual transfers）</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxMarginBalance>>> SetMarginAmountAsync(
        string instrumentId,
        OkxPositionSide positionSide,
        OkxMarginAddReduce marginAddReduce,
        decimal amount,
        string currency = null,
        bool? auto = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "instId", instrumentId },
            { "posSide", JsonConvert.SerializeObject(positionSide, new OkxPositionSideConverter(false)) },
            { "type", JsonConvert.SerializeObject(marginAddReduce, new OkxMarginAddReduceConverter(false)) },
            { "amt", amount.ToOkxString() },
        };
        parameters.AddOptionalParameter("ccy", currency);
        parameters.AddOptionalParameter("auto", auto);

        return ProcessListRequestAsync<OkxMarginBalance>(GetUri(v5AccountPositionMarginBalance), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Get Leverage
    /// </summary>
    /// <param name="instrumentIds">ingle instrument ID or multiple instrument IDs (no more than 20) separated with comma</param>
    /// <param name="marginMode">Margin Mode</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxLeverage>>> GetLeverageAsync(
        string instrumentIds,
        OkxMarginMode marginMode,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            {"instId", instrumentIds },
            {"mgnMode", JsonConvert.SerializeObject(marginMode, new OkxMarginModeConverter(false)) },
        };

        return ProcessListRequestAsync<OkxLeverage>(GetUri(v5AccountLeverageInfo), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Get leverage estimated info
    /// </summary>
    /// <param name="instrumentType">Instrument type</param>
    /// <param name="marginMode">Margin mode</param>
    /// <param name="leverage">Leverage</param>
    /// <param name="instrumentId">Instrument ID，e.g. BTC-USDT</param>
    /// <param name="currency">Currency used for margin, e.g. BTC</param>
    /// <param name="positionSide">posSide</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxLeverageEstimatedInformation>>> GetLeverageEstimatedInformationAsync(
        OkxInstrumentType instrumentType,
        OkxMarginMode marginMode,
        decimal leverage,
        string instrumentId = null,
        string currency = null,
        OkxPositionSide? positionSide = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            {"instType", JsonConvert.SerializeObject(instrumentType, new OkxInstrumentTypeConverter(false)) },
            {"mgnMode", JsonConvert.SerializeObject(marginMode, new OkxMarginModeConverter(false)) },
            {"lever", leverage.ToOkxString() },
        };
        parameters.AddOptionalParameter("instId", instrumentId);
        parameters.AddOptionalParameter("ccy", currency);
        parameters.AddOptionalParameter("posSide", JsonConvert.SerializeObject(positionSide, new OkxPositionSideConverter(false)));

        return ProcessListRequestAsync<OkxLeverageEstimatedInformation>(GetUri(v5AccountAdjustLeverageInfo), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Get the maximum loan of instrument
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="marginMode">Margin Mode</param>
    /// <param name="marginCurrency">Margin Currency</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxMaximumLoanAmount>>> GetMaximumLoanAmountAsync(
        string instrumentId,
        OkxMarginMode marginMode,
        string marginCurrency = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            {"instId", instrumentId },
            {"mgnMode", JsonConvert.SerializeObject(marginMode, new OkxMarginModeConverter(false)) },
        };
        parameters.AddOptionalParameter("mgnCcy", marginCurrency);

        return ProcessListRequestAsync<OkxMaximumLoanAmount>(GetUri(v5AccountMaxLoan), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Get Fee Rates
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="instrumentFamily">Instrument family</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxFeeRate>> GetFeeRatesAsync(
        OkxInstrumentType instrumentType,
        string instrumentId = null,
        string underlying = null,
        string instrumentFamily = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            {"instType", JsonConvert.SerializeObject(instrumentType, new OkxInstrumentTypeConverter(false)) },
        };
        parameters.AddOptionalParameter("instId", instrumentId);
        parameters.AddOptionalParameter("uly", underlying);
        parameters.AddOptionalParameter("instFamily", instrumentFamily);

        return ProcessFirstOrDefaultRequestAsync<OkxFeeRate>(GetUri(v5AccountTradeFee), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Get interest-accrued
    /// </summary>
    /// <param name="type">Loan type</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="currency">Currency</param>
    /// <param name="marginMode">Margin Mode</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxInterestAccrued>>> GetInterestAccruedAsync(
        OkxLoanType? type = null,
        string currency = null,
        string instrumentId = null,
        OkxMarginMode? marginMode = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("type", JsonConvert.SerializeObject(type, new OkxLoanTypeConverter(false)));
        parameters.AddOptionalParameter("ccy", currency);
        parameters.AddOptionalParameter("instId", instrumentId);
        parameters.AddOptionalParameter("mgnMode", JsonConvert.SerializeObject(marginMode, new OkxMarginModeConverter(false)));
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxInterestAccrued>(GetUri(v5AccountInterestAccrued), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Get the user's current leveraged currency borrowing interest rate
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxInterestRate>>> GetInterestRateAsync(
        string currency = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("ccy", currency);

        return ProcessListRequestAsync<OkxInterestRate>(GetUri(v5AccountInterestRate), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Set the display type of Greeks.
    /// </summary>
    /// <param name="greeksType">Display type of Greeks.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxAccountGreeksType>> SetGreeksAsync(OkxGreeksType greeksType, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "greeksType", JsonConvert.SerializeObject(greeksType, new OkxGreeksTypeConverter(false)) },
        };

        return ProcessFirstOrDefaultRequestAsync<OkxAccountGreeksType>(GetUri(v5AccountSetGreeks), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// You can set the currency margin and futures/perpetual Isolated margin trading mode
    /// </summary>
    /// <param name="instrumentType">Instrument type</param>
    /// <param name="marginMode">Isolated margin trading settings</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public Task<RestCallResult<OkxIsolatedMarginModeSettings>> SetIsolatedMarginModeAsync(OkxInstrumentType instrumentType, OkxIsolatedMarginMode marginMode, CancellationToken ct = default)
    {
        if (!instrumentType.IsIn(OkxInstrumentType.Margin, OkxInstrumentType.Contracts)) throw new ArgumentException("Only Margin and Contracts allowed", nameof(instrumentType));
        var parameters = new Dictionary<string, object> {
            { "isoMode", JsonConvert.SerializeObject(marginMode, new OkxIsolatedMarginModeConverter(false)) },
            { "type", JsonConvert.SerializeObject(instrumentType, new OkxInstrumentTypeConverter(false)) },
        };

        return ProcessFirstOrDefaultRequestAsync<OkxIsolatedMarginModeSettings>(GetUri(v5AccountSetIsolatedMode), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Retrieve the maximum transferable amount.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxWithdrawalAmount>>> GetMaximumWithdrawalsAsync(
        string currency = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("ccy", currency);

        return ProcessListRequestAsync<OkxWithdrawalAmount>(GetUri(v5AccountMaxWithdrawal), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    public Task<RestCallResult<OkxAccountRiskState>> GetRiskStateAsync(CancellationToken ct = default)
    {
        return ProcessFirstOrDefaultRequestAsync<OkxAccountRiskState>(GetUri(v5AccountRiskState), HttpMethod.Get, ct, signed: true);
    }

    public Task<RestCallResult<OkxMarginBorrowRepay>> QuickMarginBorrowAsync(string instrumentId, string currency, decimal amount, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "instId", instrumentId },
            { "ccy", currency },
            { "side", "borrow" },
            { "amt", amount.ToOkxString() }
        };

        return ProcessFirstOrDefaultRequestAsync<OkxMarginBorrowRepay>(GetUri(v5AccountQuickMarginBorrowRepay), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    public Task<RestCallResult<OkxMarginBorrowRepay>> QuickMarginRepayAsync(string instrumentId, string currency, decimal amount, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "instId", instrumentId },
            { "ccy", currency },
            { "side", "repay" },
            { "amt", amount.ToOkxString() }
        };

        return ProcessFirstOrDefaultRequestAsync<OkxMarginBorrowRepay>(GetUri(v5AccountQuickMarginBorrowRepay), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    public Task<RestCallResult<List<OkxMarginBorrowRepayHistory>>> GetQuickMarginBorrowRepayHistoryAsync(
    string instrumentId = null,
    string currency = null,
    string side = null,
    long? after = null,
    long? before = null,
    long? begin = null,
    long? end = null,
    int limit = 100,
    CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("instId", instrumentId);
        parameters.AddOptionalParameter("ccy", currency);
        parameters.AddOptionalParameter("side", side);
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("begin", begin?.ToOkxString());
        parameters.AddOptionalParameter("end", end?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxMarginBorrowRepayHistory>(GetUri(v5AccountQuickMarginBorrowRepayHistory), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    public Task<RestCallResult<OkxVipLoanBorrowRepay>> VipLoanBorrowAsync(string currency, decimal amount, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "ccy", currency },
            { "side", "borrow" },
            { "amt", amount.ToOkxString() }
        };

        return ProcessFirstOrDefaultRequestAsync<OkxVipLoanBorrowRepay>(GetUri(v5AccountBorrowRepay), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    public Task<RestCallResult<OkxVipLoanBorrowRepay>> VipLoanRepayAsync(string currency, decimal amount, long orderId, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "ccy", currency },
            { "side", "repay" },
            { "ordId", orderId.ToOkxString() },
            { "amt", amount.ToOkxString() }
        };

        return ProcessFirstOrDefaultRequestAsync<OkxVipLoanBorrowRepay>(GetUri(v5AccountBorrowRepay), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    public Task<RestCallResult<List<OkxVipLoanBorrowRepayHistory>>> GetVipLoanBorrowRepayHistoryAsync(
    string currency = null,
    long? after = null,
    long? before = null,
    int limit = 100,
    CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("ccy", currency);
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxVipLoanBorrowRepayHistory>(GetUri(v5AccountBorrowRepayHistory), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    public Task<RestCallResult<List<OkxVipInterestAccruedData>>> GetVipInterestAccruedDataAsync(
    string currency = null,
    long? orderId = null,
    long? after = null,
    long? before = null,
    int limit = 100,
    CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("ccy", currency);
        parameters.AddOptionalParameter("ordId", orderId?.ToOkxString());
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxVipInterestAccruedData>(GetUri(v5AccountVipInterestAccrued), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    public Task<RestCallResult<List<OkxVipInterestDeductedData>>> GetVipInterestDeductedDataAsync(
    string currency = null,
    long? orderId = null,
    long? after = null,
    long? before = null,
    int limit = 100,
    CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("ccy", currency);
        parameters.AddOptionalParameter("ordId", orderId?.ToOkxString());
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxVipInterestDeductedData>(GetUri(v5AccountVipInterestDeducted), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    public Task<RestCallResult<List<OkxVipLoanOrder>>> GetVipLoanOrdersAsync(
    long? orderId = null,
    OkxAccountVipLoanState? state = null,
    string currency = null,
    long? after = null,
    long? before = null,
    int limit = 100,
    CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("ordId", orderId?.ToOkxString());
        parameters.AddOptionalParameter("state", JsonConvert.SerializeObject(state, new OkxAccountVipLoanStateConverter(false)));
        parameters.AddOptionalParameter("ccy", currency);
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxVipLoanOrder>(GetUri(v5AccountVipLoanOrderList), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    public Task<RestCallResult<List<OkxVipLoanOrderDetails>>> GetVipLoanOrderDetailsAsync(
    long? orderId = null,
    string currency = null,
    long? after = null,
    long? before = null,
    int limit = 100,
    CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("ordId", orderId?.ToOkxString());
        parameters.AddOptionalParameter("ccy", currency);
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxVipLoanOrderDetails>(GetUri(v5AccountVipLoanOrderDetail), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    public Task<RestCallResult<List<OkxInterestLimits>>> GetInterestLimitsAsync(
    OkxLoanType? type = null,
    string currency = null,
    CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("type", JsonConvert.SerializeObject(type, new OkxLoanTypeConverter(false)));
        parameters.AddOptionalParameter("ccy", currency);

        return ProcessListRequestAsync<OkxInterestLimits>(GetUri(v5AccountInterestLimits), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }


    public Task<RestCallResult<List<OkxInterestLimits>>> SimulateMarginAsync(
    OkxInstrumentType? instrumentType = null,
    bool importExistingPositions = true,
    OkxDerivativesOffsetMode spotOffsetType = OkxDerivativesOffsetMode.DerivativesOnly,
    IEnumerable<OkxSimulatedPosition> simulatedPositions = null,
    CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new OkxInstrumentTypeConverter(false)));
        parameters.AddOptionalParameter("inclRealPos", importExistingPositions);
        parameters.AddOptionalParameter("spotOffsetType", JsonConvert.SerializeObject(spotOffsetType, new OkxDerivativesOffsetModeConverter(false)));
        parameters.AddOptionalParameter("simPos", simulatedPositions);

        return ProcessListRequestAsync<OkxInterestLimits>(GetUri(v5AccountSimulatedMargin), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    public Task<RestCallResult<List<OkxPositionBuilder>>> PositionBuilderAsync(
    bool importExistingPositionsAndAssets = true,
    OkxDerivativesOffsetMode spotOffsetType = OkxDerivativesOffsetMode.DerivativesOnly,
    IEnumerable<OkxSimulatedPosition> simulatedPositions = null,
    IEnumerable<OkxSimulatedAsset> simulatedAssets = null,
    OkxGreeksType? greeksType = OkxGreeksType.BlackScholesGreeksInDollars,
    CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("inclRealPosAndEq", importExistingPositionsAndAssets);
        parameters.AddOptionalParameter("spotOffsetType", JsonConvert.SerializeObject(spotOffsetType, new OkxDerivativesOffsetModeConverter(false)));
        parameters.AddOptionalParameter("simPos", simulatedPositions);
        parameters.AddOptionalParameter("simAsset", simulatedAssets);
        parameters.AddOptionalParameter("greeksType", JsonConvert.SerializeObject(greeksType, new OkxGreeksTypeConverter(false)));

        return ProcessListRequestAsync<OkxPositionBuilder>(GetUri(v5AccountPositionBuilder), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    public Task<RestCallResult<OkxGreeks>> GetGreeksAsync(
    string currency = null,
    CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("ccy", currency);

        return ProcessFirstOrDefaultRequestAsync<OkxGreeks>(GetUri(v5AccountGreeks), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    public Task<RestCallResult<OkxPositionTiers>> GetPositionTiersAsync(
    OkxInstrumentType instrumentType,
    string underlying = null,
    string instrumentFamily = null,
    CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new OkxInstrumentTypeConverter(false)));
        parameters.AddOptionalParameter("uly", underlying);
        parameters.AddOptionalParameter("instFamily", instrumentFamily);

        return ProcessFirstOrDefaultRequestAsync<OkxPositionTiers>(GetUri(v5AccountPositionTiers), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    public Task<RestCallResult<OkxDerivativesOffsetType>> SetRiskOffsetTypeAsync(
    OkxDerivativesOffsetMode type,
    CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("type", JsonConvert.SerializeObject(type, new OkxDerivativesOffsetModeConverter(false)));

        return ProcessFirstOrDefaultRequestAsync<OkxDerivativesOffsetType>(GetUri(v5AccountSetRiskOffsetType), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    public Task<RestCallResult<OkxTimestamp>> ActivateOptionAsync(CancellationToken ct = default)
    {
        return ProcessFirstOrDefaultRequestAsync<OkxTimestamp>(GetUri(v5AccountActivateOption), HttpMethod.Post, ct, signed: true);
    }

    public Task<RestCallResult<OkxAutoLoan>> SetAutoLoanAsync(bool autoLoan, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "autoLoan", autoLoan }
        };

        return ProcessFirstOrDefaultRequestAsync<OkxAutoLoan>(GetUri(v5AccountSetAutoLoan), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    public Task<RestCallResult<OkxAccountLevelData>> SetLevelAsync(OkxAccountLevel accountLevel, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("acctLv", JsonConvert.SerializeObject(accountLevel, new OkxAccountLevelConverter(false)));

        return ProcessFirstOrDefaultRequestAsync<OkxAccountLevelData>(GetUri(v5AccountSetAccountLevel), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    public Task<RestCallResult<OkxBooleanResult>> ResetMmpAsync(string instrumentFamily, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "instType", "OPTION" },
            { "instFamily", instrumentFamily }
        };

        return ProcessFirstOrDefaultRequestAsync<OkxBooleanResult>(GetUri(v5AccountMmpReset), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    public Task<RestCallResult<OkxMmpConfiguration>> SetMmpConfigurationAsync(string instrumentFamily, int timeInterval, int frozenInterval, int quantityLimit, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "instFamily", instrumentFamily },
            { "timeInterval", timeInterval.ToOkxString() },
            { "frozenInterval", frozenInterval.ToOkxString() },
            { "qtyLimit", quantityLimit.ToOkxString() },
        };

        return ProcessFirstOrDefaultRequestAsync<OkxMmpConfiguration>(GetUri(v5AccountMmpConfig), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    public Task<RestCallResult<OkxMmpConfigurationData>> GetMmpConfigurationAsync(string instrumentFamily, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "instFamily", instrumentFamily },
        };

        return ProcessFirstOrDefaultRequestAsync<OkxMmpConfigurationData>(GetUri(v5AccountMmpConfig), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }
    #endregion

}
