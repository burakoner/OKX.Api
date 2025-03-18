namespace OKX.Api.Funding;

/// <summary>
/// OKX Rest Api Funding Account Client
/// </summary>
public class OkxFundingRestClient(OkxRestApiClient root) : OkxBaseRestClient(root)
{
    // Endpoints
    private const string v5AssetCurrencies = "api/v5/asset/currencies";
    private const string v5AssetBalances = "api/v5/asset/balances";
    private const string v5AssetNonTradableAssets = "api/v5/asset/non-tradable-assets";
    private const string v5AssetAssetValuation = "api/v5/asset/asset-valuation";
    private const string v5AssetTransfer = "api/v5/asset/transfer";
    private const string v5AssetTransferState = "api/v5/asset/transfer-state";
    private const string v5AssetBills = "api/v5/asset/bills";
    private const string v5AssetDepositAddress = "api/v5/asset/deposit-address";
    private const string v5AssetDepositHistory = "api/v5/asset/deposit-history";
    private const string v5AssetWithdrawal = "api/v5/asset/withdrawal";
    private const string v5AssetWithdrawalCancel = "api/v5/asset/cancel-withdrawal";
    private const string v5AssetWithdrawalHistory = "api/v5/asset/withdrawal-history";
    private const string v5AssetDepositWithdrawStatus = "api/v5/asset/deposit-withdraw-status";
    private const string v5AssetExchangeList = "api/v5/asset/exchange-list";
    private const string v5AssetMonthlyStatement = "api/v5/asset/monthly-statement";
    private const string v5AssetConvertCurrencies = "api/v5/asset/convert/currencies";
    private const string v5AssetConvertCurrencyPair = "api/v5/asset/convert/currency-pair";
    private const string v5AssetConvertEstimateQuote = "api/v5/asset/convert/estimate-quote";
    private const string v5AssetConvertTrade = "api/v5/asset/convert/trade";
    private const string v5AssetConvertHistory = "api/v5/asset/convert/history";

    // Fiat Endpoints
    // TODO: api/v5/fiat/deposit-payment-methods
    // TODO: api/v5/fiat/withdrawal-payment-methods
    // TODO: api/v5/fiat/create-withdrawal
    // TODO: api/v5/fiat/cancel-withdrawal
    // TODO: api/v5/fiat/withdrawal-order-history
    // TODO: api/v5/fiat/withdrawal
    // TODO: api/v5/fiat/deposit-order-history
    // TODO: api/v5/fiat/deposit

    /// <summary>
    /// Retrieve a list of all currencies. Not all currencies can be traded. Currencies that have not been defined in ISO 4217 may use a custom symbol.
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxFundingCurrency>>> GetCurrenciesAsync(CancellationToken ct = default)
    {
        return ProcessListRequestAsync<OkxFundingCurrency>(GetUri(v5AssetCurrencies), HttpMethod.Get, ct, true);
    }

    /// <summary>
    /// Retrieve the balances of all the assets, and the amount that is available or on hold.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxFundingBalance>>> GetBalancesAsync(string? currency = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("ccy", currency);

        return ProcessListRequestAsync<OkxFundingBalance>(GetUri(v5AssetBalances), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Get non-tradable asset balances
    /// </summary>
    /// <param name="currency">Single currency or multiple currencies (no more than 20) separated with comma, e.g. BTC or BTC,ETH.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxFundingNonTradableAssetBalance>>> GetNonTradableBalancesAsync(string? currency = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("ccy", currency);

        return ProcessListRequestAsync<OkxFundingNonTradableAssetBalance>(GetUri(v5AssetNonTradableAssets), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// View account asset valuation
    /// </summary>
    /// <param name="currency">Asset valuation calculation unit</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxFundingAssetValuation>> GetAssetValuationAsync(string? currency = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("ccy", currency);

        return ProcessOneRequestAsync<OkxFundingAssetValuation>(GetUri(v5AssetAssetValuation), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// This endpoint supports the transfer of funds between your funding account and trading account, and from the master account to sub-accounts. Direct transfers between sub-accounts are not allowed.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="amount">Amount</param>
    /// <param name="type">Transfer type</param>
    /// <param name="fromAccount">The remitting account</param>
    /// <param name="toAccount">The beneficiary account</param>
    /// <param name="subAccountName">Sub Account Name</param>
    /// <param name="loanTransfer">Whether or not borrowed coins can be transferred out under Multi-currency margin and Portfolio margin the default is false</param>
    /// <param name="omitPositionRisk">Ignore position risk</param>
    /// <param name="clientOrderId">Client-supplied ID. A combination of case-sensitive alphanumerics, all numbers, or all letters of up to 32 characters.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxFundingTransferResponse>> FundTransferAsync(
        OkxFundingTransferType type,
        string currency,
        decimal amount,
        OkxAccount fromAccount,
        OkxAccount toAccount,
        string? subAccountName = null,
        bool? loanTransfer = null,
        bool? omitPositionRisk = null,
        string? clientOrderId = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            { "ccy",currency},
            { "amt",amount.ToOkxString()},
        };
        parameters.AddEnum("type", type);
        parameters.AddEnum("from", fromAccount);
        parameters.AddEnum("to", toAccount);
        parameters.AddOptional("subAcct", subAccountName);
        parameters.AddOptional("loanTrans", loanTransfer);
        parameters.AddOptional("clientId", clientOrderId);
        parameters.AddOptional("omitPosRisk", omitPositionRisk);

        return ProcessOneRequestAsync<OkxFundingTransferResponse>(GetUri(v5AssetTransfer), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Retrieve the transfer state data of the last 2 weeks.
    /// </summary>
    /// <param name="transferId">Transfer ID. Either transId or clientId is required. If both are passed, transId will be used.</param>
    /// <param name="clientOrderId">Client-supplied ID</param>
    /// <param name="type">Transfer type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxFundingTransferStateResponse>> FundTransferStateAsync(
        long? transferId = null,
        string? clientOrderId = null,
        OkxFundingTransferType? type = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("transId", transferId?.ToOkxString());
        parameters.AddOptional("clientId", clientOrderId);
        parameters.AddOptionalEnum("type", type);

        return ProcessOneRequestAsync<OkxFundingTransferStateResponse>(GetUri(v5AssetTransferState), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Query the billing record, you can get the latest 1 month historical data
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="type">Bill type</param>
    /// <param name="clientOrderId">Client-supplied ID for transfer or withdrawal. A combination of case-sensitive alphanumerics, all numbers, or all letters of up to 32 characters.</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxFundingBill>>> GetFundingBillDetailsAsync(
        string? currency = null,
        OkxFundingBillType? type = null,
        string? clientOrderId = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new ParameterCollection();
        parameters.AddOptional("ccy", currency);
        parameters.AddOptionalEnum("type", type);
        parameters.AddOptional("clientId", clientOrderId);
        parameters.AddOptional("after", after?.ToOkxString());
        parameters.AddOptional("before", before?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxFundingBill>(GetUri(v5AssetBills), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve the deposit addresses of currencies, including previously-used addresses.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxFundingDepositAddress>>> GetDepositAddressAsync(string? currency = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("ccy", currency);

        return ProcessListRequestAsync<OkxFundingDepositAddress>(GetUri(v5AssetDepositAddress), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve the deposit history of all currencies, up to 100 recent records in a year.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="depositId">Deposit Id</param>
    /// <param name="fromWithdrawalId">Internal transfer initiator's withdrawal ID. If the deposit comes from internal transfer, this field displays the withdrawal ID of the internal transfer initiator</param>
    /// <param name="transactionId">Transaction ID</param>
    /// <param name="type">Deposit Type</param>
    /// <param name="state">Status of deposit</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxFundingDepositHistory>>> GetDepositHistoryAsync(
        string? currency = null,
        string? depositId = null,
        long? fromWithdrawalId = null,
        string? transactionId = null,
        OkxFundingDepositType? type = null,
        OkxFundingDepositState? state = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);

        var parameters = new ParameterCollection();
        parameters.AddOptionalEnum("type", type);
        parameters.AddOptionalEnum("state", state);
        parameters.AddOptional("ccy", currency);
        parameters.AddOptional("depId", depositId);
        parameters.AddOptional("fromWdId", fromWithdrawalId);
        parameters.AddOptional("txId", transactionId);
        parameters.AddOptional("after", after?.ToOkxString());
        parameters.AddOptional("before", before?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxFundingDepositHistory>(GetUri(v5AssetDepositHistory), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Withdrawal of tokens.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="amount">Amount</param>
    /// <param name="destination">Withdrawal destination address</param>
    /// <param name="toAddress">Verified digital currency address, email or mobile number. Some digital currency addresses are formatted as 'address+tag', e.g. 'ARDOR-7JF3-8F2E-QUWZ-CAN7F:123456'</param>
    /// <param name="fee">Transaction fee</param>
    /// <param name="chain">Chain name. There are multiple chains under some currencies, such as USDT has USDT-ERC20, USDT-TRC20, and USDT-Omni. If this parameter is not filled in because it is not available, it will default to the main chain.</param>
    /// <param name="areaCode">Area code for the phone number. If toAddr is a phone number, this parameter is required.</param>
    /// <param name="receiverInfo">Recipient information. For the specific entity users to do on-chain withdrawal/lightning withdrawal, this information is required.</param>
    /// <param name="clientOrderId">Client-supplied ID. A combination of case-sensitive alphanumerics, all numbers, or all letters of up to 32 characters.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxFundingWithdrawalResponse>> WithdrawAsync(
        string currency,
        decimal amount,
        OkxFundingWithdrawalDestination destination,
        string toAddress,
        decimal fee,
        string? chain = null,
        string? areaCode = null,
        OkxFundingWithdrawalReceiver? receiverInfo = null,
        string? clientOrderId = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            { "ccy",currency},
            { "amt",amount.ToOkxString()},
            { "toAddr",toAddress},
            { "fee",fee   .ToOkxString()},
        };
        parameters.AddEnum("dest", destination);
        parameters.AddOptional("chain", chain);
        parameters.AddOptional("areaCode", areaCode);
        parameters.AddOptional("rcvrInfo", receiverInfo);
        parameters.AddOptional("clientId", clientOrderId);

        return ProcessOneRequestAsync<OkxFundingWithdrawalResponse>(GetUri(v5AssetWithdrawal), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Cancel withdrawal
    /// You can cancel normal withdrawal requests, but you cannot cancel withdrawal requests on Lightning.
    /// Rate Limit: 6 requests per second
    /// </summary>
    /// <param name="withdrawalId">Withdrawal ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<long?>> CancelWithdrawalAsync(long withdrawalId, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            { "wdId", withdrawalId},
        };

        var result = await ProcessOneRequestAsync<OkxFundingWithdrawalIdContainer>(GetUri(v5AssetWithdrawalCancel), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
        if (!result) return new RestCallResult<long?>(result.Request, result.Response, result.Raw, result.Error);
        return new RestCallResult<long?>(result.Request, result.Response, result.Data.Payload, result.Raw, result.Error);
    }

    /// <summary>
    /// Retrieve the withdrawal records according to the currency, withdrawal status, and time range in reverse chronological order. The 100 most recent records are returned by default.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="withdrawalId">Withdrawal ID</param>
    /// <param name="clientOrderId">Client Order Id</param>
    /// <param name="transactionId">Transaction ID</param>
    /// <param name="type">Type</param>
    /// <param name="state">State</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxFundingWithdrawalHistory>>> GetWithdrawalHistoryAsync(
        string? currency = null,
        long? withdrawalId = null,
        string? clientOrderId = null,
        string? transactionId = null,
        OkxFundingWithdrawalType? type = null,
        OkxFundingWithdrawalState? state = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);

        var parameters = new ParameterCollection();
        parameters.AddOptionalEnum("type", type);
        parameters.AddOptionalEnum("state", state);
        parameters.AddOptional("ccy", currency);
        parameters.AddOptional("wdId", withdrawalId);
        parameters.AddOptional("clientId", clientOrderId);
        parameters.AddOptional("txId", transactionId);
        parameters.AddOptional("after", after?.ToOkxString());
        parameters.AddOptional("before", before?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxFundingWithdrawalHistory>(GetUri(v5AssetWithdrawalHistory), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve deposit's detailed status and estimated complete time.
    /// </summary>
    /// <param name="currency">Currency type, e.g. USDT. Required when retrieving deposit status with txId</param>
    /// <param name="txId">Hash record of the deposit, use to retrieve deposit status. Required to input one and only one of wdId and txId</param>
    /// <param name="to">To address, the destination address in deposit. Required when retrieving deposit status with txId</param>
    /// <param name="chain">Currency chain infomation, e.g. USDT-ERC20. Required when retrieving deposit status with txId</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxFundingDepositStatus>> GetDepositStatusAsync(
        string currency,
        string txId,
        string to,
        string chain,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection()
        {
            { "ccy", currency },
            { "txId", txId },
            { "to", to },
            { "chain", chain }
        };

        return ProcessOneRequestAsync<OkxFundingDepositStatus>(GetUri(v5AssetDepositWithdrawStatus), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve withdrawal's detailed status and estimated complete time.
    /// </summary>
    /// <param name="withdrawalId">Withdrawl ID, use to retrieve withdrawal status. Required to input one and only one of wdId and txId</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxFundingWithdrawalStatus>> GetWithdrawalStatusAsync(
        long withdrawalId,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection()
        {
            { "wdId", withdrawalId.ToOkxString() },
        };

        return ProcessOneRequestAsync<OkxFundingWithdrawalStatus>(GetUri(v5AssetDepositWithdrawStatus), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Authentication is not required for this public endpoint.
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxFundingExchangeList>>> GetExchangeListAsync(CancellationToken ct = default)
    {
        return ProcessListRequestAsync<OkxFundingExchangeList>(GetUri(v5AssetExchangeList), HttpMethod.Get, ct, signed: false);
    }

    /// <summary>
    /// Apply for monthly statement in the past year.
    /// </summary>
    /// <param name="month">Month,last month by default. Valid value is Jan, Feb, Mar, Apr,May, Jun, Jul,Aug, Sep,Oct,Nov,Dec</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxTimestamp>> ApplyForMonthlyStatementAsync(
        string? month = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("month", month);

        return ProcessOneRequestAsync<OkxTimestamp>(GetUri(v5AssetMonthlyStatement), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Retrieve monthly statement in the past year.
    /// </summary>
    /// <param name="month">Month,last month by default. Valid value is Jan, Feb, Mar, Apr,May, Jun, Jul,Aug, Sep,Oct,Nov,Dec</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxDownloadLink>> GetMonthlyStatementAsync(
        string month,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("month", month);

        return ProcessOneRequestAsync<OkxDownloadLink>(GetUri(v5AssetMonthlyStatement), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Get convert currencies
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<List<string>>> GetConvertCurrenciesAsync(CancellationToken ct = default)
    {
        var result = await ProcessListRequestAsync<OkxFundingCurrencyContainer>(GetUri(v5AssetConvertCurrencies), HttpMethod.Get, ct, signed: true);
        if (!result) return new RestCallResult<List<string>>(result.Request, result.Response, result.Raw, result.Error);
        return new RestCallResult<List<string>>(result.Request, result.Response, result.Data.Select(x => x.Payload).ToList(), result.Raw, result.Error);
    }

    /// <summary>
    /// Get convert currency pair
    /// </summary>
    /// <param name="fromCurrency">Currency to convert from, e.g. USDT</param>
    /// <param name="toCurrency">Currency to convert to, e.g. BTC</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxFundingConvertCurrencyPair>> GetConvertCurrencyPairAsync(
        string fromCurrency,
        string toCurrency,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("fromCcy", fromCurrency);
        parameters.AddOptional("toCcy", toCurrency);

        return ProcessOneRequestAsync<OkxFundingConvertCurrencyPair>(GetUri(v5AssetConvertCurrencyPair), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Estimate quote
    /// </summary>
    /// <param name="baseCurrency">Base currency, e.g. BTC in BTC-USDT</param>
    /// <param name="quoteCurrency">Quote currency, e.g. USDT in BTC-USDT</param>
    /// <param name="side">Trade side based on baseCcy</param>
    /// <param name="rfqAmount">RFQ amount</param>
    /// <param name="rfqCurrency">RFQ currency</param>
    /// <param name="clientOrderId">Client Order ID as assigned by the client. A combination of case-sensitive alphanumerics, all numbers, or all letters of up to 32 characters.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxFundingConvertEstimateQuote>> EstimateQuoteAsync(
        string baseCurrency,
        string quoteCurrency,
        OkxTradeOrderSide side,
        decimal rfqAmount,
        string rfqCurrency,
        string? clientOrderId = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "baseCcy", baseCurrency },
            { "quoteCcy", quoteCurrency },
            { "rfqSz", rfqAmount.ToOkxString() },
            { "rfqSzCcy", rfqCurrency },
        };
        parameters.AddEnum("side", side);
        parameters.AddOptional("clQReqId", clientOrderId);
        parameters.AddOptional("tag", OkxConstants.BrokerId);

        return ProcessOneRequestAsync<OkxFundingConvertEstimateQuote>(GetUri(v5AssetConvertEstimateQuote), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// You should make estimate quote before convert trade.
    /// </summary>
    /// <param name="quoteId">	Quote ID</param>
    /// <param name="baseCurrency">Base currency, e.g. BTC in BTC-USDT</param>
    /// <param name="quoteCurrency">Quote currency, e.g. USDT in BTC-USDT</param>
    /// <param name="side">Trade side based on baseCcy</param>
    /// <param name="amount">Quote amount. The quote amount should no more then RFQ amount</param>
    /// <param name="amountCurrency">Quote currency</param>
    /// <param name="clientOrderId">Client Order ID as assigned by the client. A combination of case-sensitive alphanumerics, all numbers, or all letters of up to 32 characters.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxFundingConvertOrder>> PlaceConvertOrderAsync(
        string quoteId,
        string baseCurrency,
        string quoteCurrency,
        OkxTradeOrderSide side,
        decimal amount,
        string amountCurrency,
        string? clientOrderId = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "quoteId", quoteId },
            { "baseCcy", baseCurrency },
            { "quoteCcy", quoteCurrency },
            { "sz", amount.ToOkxString() },
            { "szCcy", amountCurrency },
        };
        parameters.AddEnum("side", side);
        parameters.AddOptional("clQReqId", clientOrderId);
        parameters.AddOptional("tag", OkxConstants.BrokerId);

        return ProcessOneRequestAsync<OkxFundingConvertOrder>(GetUri(v5AssetConvertTrade), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Get convert history
    /// </summary>
    /// <param name="clientOrderId">Client Order ID as assigned by the client. A combination of case-sensitive alphanumerics, all numbers, or all letters of up to 32 characters.</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100. The default is 100.</param>
    /// <param name="tag">Order tag. Applicable to broker user. If the convert trading used tag, this parameter is also required.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxFundingConvertOrderHistory>>> GetConvertHistoryAsync(
        string? clientOrderId = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        string? tag = null,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new ParameterCollection();
        parameters.AddOptional("clTReqId", clientOrderId);
        parameters.AddOptional("after", after?.ToOkxString());
        parameters.AddOptional("before", before?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());
        parameters.AddOptional("tag", tag);

        return ProcessListRequestAsync<OkxFundingConvertOrderHistory>(GetUri(v5AssetConvertHistory), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

}