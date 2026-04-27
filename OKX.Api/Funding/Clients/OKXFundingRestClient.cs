namespace OKX.Api.Funding;

/// <summary>
/// OKX Rest Api Funding Account Client
/// </summary>
public class OkxFundingRestClient(OkxRestApiClient root) : OkxBaseRestClient(root)
{
    /// <summary>
    /// Retrieve a list of all currencies. Not all currencies can be traded. Currencies that have not been defined in ISO 4217 may use a custom symbol.
    /// </summary>
    /// <param name="currency">Single currency or multiple currencies separated by comma, e.g. BTC or BTC,ETH.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxFundingCurrency>>> GetCurrenciesAsync(string? currency = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("ccy", currency);

        return ProcessListRequestAsync<OkxFundingCurrency>(GetUri("api/v5/asset/currencies"), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
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

        return ProcessListRequestAsync<OkxFundingBalance>(GetUri("api/v5/asset/balances"), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
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

        return ProcessListRequestAsync<OkxFundingNonTradableAssetBalance>(GetUri("api/v5/asset/non-tradable-assets"), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
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

        return ProcessOneRequestAsync<OkxFundingAssetValuation>(GetUri("api/v5/asset/asset-valuation"), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// This endpoint supports transfers between funding and trading accounts, master and sub-accounts, and sub-account to sub-account transfers when permitted by OKX.
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
    public Task<RestCallResult<OkxFundingTransferResponse>> TransferAsync(
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
        => TransferAsync(new OkxFundingTransferRequest
        {
            Type = type,
            Currency = currency,
            Amount = amount,
            FromAccount = fromAccount,
            ToAccount = toAccount,
            SubAccountName = subAccountName,
            LoanTransfer = loanTransfer,
            OmitPositionRisk = omitPositionRisk,
            ClientOrderId = clientOrderId
        }, ct);

    /// <summary>
    /// This endpoint supports transfers between funding and trading accounts, master and sub-accounts, and sub-account to sub-account transfers when permitted by OKX.
    /// </summary>
    public Task<RestCallResult<OkxFundingTransferResponse>> TransferAsync(OkxFundingTransferRequest request, CancellationToken ct = default)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));
        if (string.IsNullOrWhiteSpace(request.Currency))
            throw new ArgumentException("Currency is required.", nameof(request));

        var parameters = new ParameterCollection {
            { "ccy",request.Currency!},
            { "amt",request.Amount.ToOkxString()},
        };
        parameters.AddEnum("type", request.Type);
        parameters.AddEnum("from", request.FromAccount);
        parameters.AddEnum("to", request.ToAccount);
        parameters.AddOptional("subAcct", request.SubAccountName);
        parameters.AddOptional("loanTrans", request.LoanTransfer);
        parameters.AddOptional("clientId", request.ClientOrderId);
        parameters.AddOptional("omitPosRisk", request.OmitPositionRisk);

        return ProcessOneRequestAsync<OkxFundingTransferResponse>(GetUri("api/v5/asset/transfer"), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Retrieve the transfer state data of the last 2 weeks.
    /// </summary>
    /// <param name="transferId">Transfer ID. Either transId or clientId is required. If both are passed, transId will be used.</param>
    /// <param name="clientOrderId">Client-supplied ID</param>
    /// <param name="type">Transfer type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxFundingTransferStateResponse>> TransferStateAsync(
        long? transferId = null,
        string? clientOrderId = null,
        OkxFundingTransferType? type = null,
        CancellationToken ct = default)
        => TransferStateAsync(new OkxFundingTransferStateRequest
        {
            TransferId = transferId,
            ClientOrderId = clientOrderId,
            Type = type
        }, ct);

    /// <summary>
    /// Retrieve the transfer state data of the last 2 weeks.
    /// </summary>
    public Task<RestCallResult<OkxFundingTransferStateResponse>> TransferStateAsync(OkxFundingTransferStateRequest request, CancellationToken ct = default)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));
        var parameters = new ParameterCollection();
        parameters.AddOptional("transId", request.TransferId?.ToOkxString());
        parameters.AddOptional("clientId", request.ClientOrderId);
        parameters.AddOptionalEnum("type", request.Type);

        return ProcessOneRequestAsync<OkxFundingTransferStateResponse>(GetUri("api/v5/asset/transfer-state"), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
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
    /// <param name="pagingType">PagingType. 1: Timestamp of the bill record. 2: Bill ID of the bill record. The default is 1</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxFundingBill>>> GetBillsAsync(
        string? currency = null,
        OkxFundingBillType? type = null,
        string? clientOrderId = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        int pagingType = 1,
        CancellationToken ct = default)
        => GetBillsAsync(new OkxFundingBillQueryRequest
        {
            Currency = currency,
            Type = type,
            ClientOrderId = clientOrderId,
            After = after,
            Before = before,
            Limit = limit,
            PagingType = pagingType
        }, ct);

    /// <summary>
    /// Query the billing record, you can get the latest 1 month historical data
    /// </summary>
    public Task<RestCallResult<List<OkxFundingBill>>> GetBillsAsync(OkxFundingBillQueryRequest request, CancellationToken ct = default)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));
        request.Limit.ValidateIntBetween(nameof(request.Limit), 1, 100);
        var parameters = new ParameterCollection();
        parameters.AddOptional("ccy", request.Currency);
        parameters.AddOptionalEnum("type", request.Type);
        parameters.AddOptional("clientId", request.ClientOrderId);
        parameters.AddOptional("after", request.After?.ToOkxString());
        parameters.AddOptional("before", request.Before?.ToOkxString());
        parameters.AddOptional("limit", request.Limit.ToOkxString());
        parameters.AddOptional("pagingType", request.PagingType.ToOkxString());

        return ProcessListRequestAsync<OkxFundingBill>(GetUri("api/v5/asset/bills"), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Query the billing records of all time since 1 February, 2021.
    /// ?? IMPORTANT: Data updates occur every 30 seconds.Update frequency may vary based on data volume - please be aware of potential delays during high-traffic periods.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="type">Bill type</param>
    /// <param name="clientOrderId">Client-supplied ID for transfer or withdrawal. A combination of case-sensitive alphanumerics, all numbers, or all letters of up to 32 characters.</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="pagingType">PagingType. 1: Timestamp of the bill record. 2: Bill ID of the bill record. The default is 1</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxFundingBill>>> GetBillsHistoryAsync(
        string? currency = null,
        OkxFundingBillType? type = null,
        string? clientOrderId = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        int pagingType = 1,
        CancellationToken ct = default)
        => GetBillsHistoryAsync(new OkxFundingBillQueryRequest
        {
            Currency = currency,
            Type = type,
            ClientOrderId = clientOrderId,
            After = after,
            Before = before,
            Limit = limit,
            PagingType = pagingType
        }, ct);

    /// <summary>
    /// Query the billing records of all time since 1 February, 2021.
    /// </summary>
    public Task<RestCallResult<List<OkxFundingBill>>> GetBillsHistoryAsync(OkxFundingBillQueryRequest request, CancellationToken ct = default)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));
        request.Limit.ValidateIntBetween(nameof(request.Limit), 1, 100);
        var parameters = new ParameterCollection();
        parameters.AddOptional("ccy", request.Currency);
        parameters.AddOptionalEnum("type", request.Type);
        parameters.AddOptional("clientId", request.ClientOrderId);
        parameters.AddOptional("after", request.After?.ToOkxString());
        parameters.AddOptional("before", request.Before?.ToOkxString());
        parameters.AddOptional("limit", request.Limit.ToOkxString());
        parameters.AddOptional("pagingType", request.PagingType.ToOkxString());

        return ProcessListRequestAsync<OkxFundingBill>(GetUri("api/v5/asset/bills-history"), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve the deposit addresses of currencies, including previously-used addresses.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxFundingDepositAddress>>> GetDepositAddressAsync(string currency, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.Add("ccy", currency);

        return ProcessListRequestAsync<OkxFundingDepositAddress>(GetUri("api/v5/asset/deposit-address"), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
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
        => GetDepositHistoryAsync(new OkxFundingDepositHistoryRequest
        {
            Currency = currency,
            DepositId = depositId,
            FromWithdrawalId = fromWithdrawalId,
            TransactionId = transactionId,
            Type = type,
            State = state,
            After = after,
            Before = before,
            Limit = limit
        }, ct);

    /// <summary>
    /// Retrieve the deposit history of all currencies, up to 100 recent records in a year.
    /// </summary>
    public Task<RestCallResult<List<OkxFundingDepositHistory>>> GetDepositHistoryAsync(OkxFundingDepositHistoryRequest request, CancellationToken ct = default)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));
        request.Limit.ValidateIntBetween(nameof(request.Limit), 1, 100);

        var parameters = new ParameterCollection();
        parameters.AddOptionalEnum("type", request.Type);
        parameters.AddOptionalEnum("state", request.State);
        parameters.AddOptional("ccy", request.Currency);
        parameters.AddOptional("depId", request.DepositId);
        parameters.AddOptional("fromWdId", request.FromWithdrawalId);
        parameters.AddOptional("txId", request.TransactionId);
        parameters.AddOptional("after", request.After?.ToOkxString());
        parameters.AddOptional("before", request.Before?.ToOkxString());
        parameters.AddOptional("limit", request.Limit.ToOkxString());

        return ProcessListRequestAsync<OkxFundingDepositHistory>(GetUri("api/v5/asset/deposit-history"), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Withdrawal of tokens.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="amount">Amount</param>
    /// <param name="destination">Withdrawal destination address</param>
    /// <param name="toAddress">Verified digital currency address, email or mobile number. Some digital currency addresses are formatted as 'address+tag', e.g. 'ARDOR-7JF3-8F2E-QUWZ-CAN7F:123456'</param>
    /// <param name="toAddressType">Address type 1: wallet address, email, phone, or login account name 2: UID(only applicable for dest= 3)</param>
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
        OkxFundingWithdrawalAddressType toAddressType,
        string? chain = null,
        string? areaCode = null,
        OkxFundingWithdrawalReceiver? receiverInfo = null,
        string? clientOrderId = null,
        CancellationToken ct = default)
        => WithdrawAsync(new OkxFundingWithdrawalRequest
        {
            Currency = currency,
            Amount = amount,
            Destination = destination,
            ToAddress = toAddress,
            ToAddressType = toAddressType,
            Chain = chain,
            AreaCode = areaCode,
            ReceiverInfo = receiverInfo,
            ClientOrderId = clientOrderId
        }, ct);

    /// <summary>
    /// Withdrawal of tokens.
    /// </summary>
    public Task<RestCallResult<OkxFundingWithdrawalResponse>> WithdrawAsync(OkxFundingWithdrawalRequest request, CancellationToken ct = default)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));
        if (string.IsNullOrWhiteSpace(request.Currency))
            throw new ArgumentException("Currency is required.", nameof(request));
        if (string.IsNullOrWhiteSpace(request.ToAddress))
            throw new ArgumentException("To address is required.", nameof(request));

        var parameters = new ParameterCollection {
            { "ccy",request.Currency!},
            { "amt",request.Amount.ToOkxString()},
            { "toAddr",request.ToAddress!},
        };
        parameters.AddEnum("dest", request.Destination);
        parameters.AddOptionalEnum("toAddrType", request.ToAddressType);
        parameters.AddOptional("chain", request.Chain);
        parameters.AddOptional("areaCode", request.AreaCode);
        parameters.AddOptional("rcvrInfo", request.ReceiverInfo);
        parameters.AddOptional("clientId", request.ClientOrderId);

        return ProcessOneRequestAsync<OkxFundingWithdrawalResponse>(GetUri("api/v5/asset/withdrawal"), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
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

        var result = await ProcessOneRequestAsync<OkxFundingWithdrawalIdContainer>(GetUri("api/v5/asset/cancel-withdrawal"), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
        if (!result) return new RestCallResult<long?>(result.Request, result.Response, result.Raw ?? "", result.Error);
        return new RestCallResult<long?>(result.Request, result.Response, result.Data.Payload, result.Raw ?? "", result.Error);
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
        => GetWithdrawalHistoryAsync(new OkxFundingWithdrawalHistoryRequest
        {
            Currency = currency,
            WithdrawalId = withdrawalId,
            ClientOrderId = clientOrderId,
            TransactionId = transactionId,
            Type = type,
            State = state,
            After = after,
            Before = before,
            Limit = limit
        }, ct);

    /// <summary>
    /// Retrieve the withdrawal records according to the currency, withdrawal status, and time range in reverse chronological order.
    /// </summary>
    public Task<RestCallResult<List<OkxFundingWithdrawalHistory>>> GetWithdrawalHistoryAsync(OkxFundingWithdrawalHistoryRequest request, CancellationToken ct = default)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));
        request.Limit.ValidateIntBetween(nameof(request.Limit), 1, 100);

        var parameters = new ParameterCollection();
        parameters.AddOptionalEnum("type", request.Type);
        parameters.AddOptionalEnum("state", request.State);
        parameters.AddOptional("ccy", request.Currency);
        parameters.AddOptional("wdId", request.WithdrawalId);
        parameters.AddOptional("clientId", request.ClientOrderId);
        parameters.AddOptional("txId", request.TransactionId);
        parameters.AddOptional("after", request.After?.ToOkxString());
        parameters.AddOptional("before", request.Before?.ToOkxString());
        parameters.AddOptional("limit", request.Limit.ToOkxString());

        return ProcessListRequestAsync<OkxFundingWithdrawalHistory>(GetUri("api/v5/asset/withdrawal-history"), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
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
    public async Task<RestCallResult<OkxFundingDepositStatus>> GetDepositStatusAsync(
        string currency,
        string txId,
        string to,
        string chain,
        CancellationToken ct = default)
    {
        var result = await GetDepositWithdrawStatusAsync(currency, txId, to, chain, ct).ConfigureAwait(false);
        if (!result)
            return new RestCallResult<OkxFundingDepositStatus>(result.Request, result.Response, result.Raw ?? "", result.Error);

        var data = result.Data!;
        return new RestCallResult<OkxFundingDepositStatus>(
            result.Request,
            result.Response,
            new OkxFundingDepositStatus
            {
                TransactionId = data.TransactionId,
                State = data.State,
                EstimatedCompleteTime = data.EstimatedCompleteTime,
            },
            result.Raw ?? "",
            result.Error);
    }

    /// <summary>
    /// Retrieve withdrawal's detailed status and estimated complete time.
    /// </summary>
    /// <param name="withdrawalId">Withdrawl ID, use to retrieve withdrawal status. Required to input one and only one of wdId and txId</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<OkxFundingWithdrawalStatus>> GetWithdrawalStatusAsync(
        long withdrawalId,
        CancellationToken ct = default)
    {
        var result = await GetDepositWithdrawStatusAsync(withdrawalId, ct).ConfigureAwait(false);
        if (!result)
            return new RestCallResult<OkxFundingWithdrawalStatus>(result.Request, result.Response, result.Raw ?? "", result.Error);

        var data = result.Data!;
        return new RestCallResult<OkxFundingWithdrawalStatus>(
            result.Request,
            result.Response,
            new OkxFundingWithdrawalStatus
            {
                WithdrawalId = data.WithdrawalId,
                State = data.State,
                EstimatedCompleteTime = data.EstimatedCompleteTime,
            },
            result.Raw ?? "",
            result.Error);
    }

    /// <summary>
    /// Retrieve deposit or withdrawal status by deposit hash.
    /// </summary>
    public Task<RestCallResult<OkxFundingDepositWithdrawStatus>> GetDepositWithdrawStatusAsync(
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

        return ProcessOneRequestAsync<OkxFundingDepositWithdrawStatus>(GetUri("api/v5/asset/deposit-withdraw-status"), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve deposit or withdrawal status by withdrawal ID.
    /// </summary>
    public Task<RestCallResult<OkxFundingDepositWithdrawStatus>> GetDepositWithdrawStatusAsync(
        long withdrawalId,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection()
        {
            { "wdId", withdrawalId.ToOkxString() },
        };

        return ProcessOneRequestAsync<OkxFundingDepositWithdrawStatus>(GetUri("api/v5/asset/deposit-withdraw-status"), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Authentication is not required for this public endpoint.
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxFundingExchange>>> GetExchangesAsync(CancellationToken ct = default)
    {
        return ProcessListRequestAsync<OkxFundingExchange>(GetUri("api/v5/asset/exchange-list"), HttpMethod.Get, ct, signed: false);
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

        return ProcessOneRequestAsync<OkxTimestamp>(GetUri("api/v5/asset/monthly-statement"), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
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

        return ProcessOneRequestAsync<OkxDownloadLink>(GetUri("api/v5/asset/monthly-statement"), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Get convert currencies
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<List<string>>> GetConvertCurrenciesAsync(CancellationToken ct = default)
    {
        var result = await GetConvertCurrencyDetailsAsync(ct).ConfigureAwait(false);
        if (!result) return new RestCallResult<List<string>>(result.Request, result.Response, result.Raw ?? "", result.Error);
        return new RestCallResult<List<string>>(result.Request, result.Response, result.Data.Select(x => x.Currency).ToList(), result.Raw ?? "", result.Error);
    }

    /// <summary>
    /// Get convert currencies with all currently documented response fields.
    /// </summary>
    public Task<RestCallResult<List<OkxFundingConvertCurrency>>> GetConvertCurrencyDetailsAsync(CancellationToken ct = default)
    {
        return ProcessListRequestAsync<OkxFundingConvertCurrency>(GetUri("api/v5/asset/convert/currencies"), HttpMethod.Get, ct, signed: true);
    }

    /// <summary>
    /// Get convert currency pair
    /// </summary>
    /// <param name="fromCurrency">Currency to convert from, e.g. USDT</param>
    /// <param name="toCurrency">Currency to convert to, e.g. BTC</param>
    /// <param name="useLargeOrderConvert">0: standard convert (default). 1: large order convert for VIP</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxFundingConvertCurrencyPair>> GetConvertCurrencyPairAsync(
        string fromCurrency,
        string toCurrency,
        bool? useLargeOrderConvert = null,
        CancellationToken ct = default)
        => GetConvertCurrencyPairAsync(new OkxFundingConvertCurrencyPairRequest
        {
            FromCurrency = fromCurrency,
            ToCurrency = toCurrency,
            UseLargeOrderConvert = useLargeOrderConvert
        }, ct);

    /// <summary>
    /// Get convert currency pair
    /// </summary>
    public Task<RestCallResult<OkxFundingConvertCurrencyPair>> GetConvertCurrencyPairAsync(OkxFundingConvertCurrencyPairRequest request, CancellationToken ct = default)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));
        var parameters = new ParameterCollection();
        parameters.AddOptional("fromCcy", request.FromCurrency);
        parameters.AddOptional("toCcy", request.ToCurrency);
        if (request.UseLargeOrderConvert != null)
            parameters.AddOptional("convertMode", request.UseLargeOrderConvert.Value ? "1" : "0");

        return ProcessOneRequestAsync<OkxFundingConvertCurrencyPair>(GetUri("api/v5/asset/convert/currency-pair"), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
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
    /// <param name="useLargeOrderConvert">0: standard convert (default). 1: large order convert for VIP</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxFundingConvertEstimateQuote>> EstimateQuoteAsync(
        string baseCurrency,
        string quoteCurrency,
        OkxTradeOrderSide side,
        decimal rfqAmount,
        string rfqCurrency,
        string? clientOrderId = null,
        bool? useLargeOrderConvert = null,
        CancellationToken ct = default)
        => EstimateQuoteAsync(new OkxFundingConvertEstimateQuoteRequest
        {
            BaseCurrency = baseCurrency,
            QuoteCurrency = quoteCurrency,
            Side = side,
            RfqAmount = rfqAmount,
            RfqCurrency = rfqCurrency,
            ClientOrderId = clientOrderId,
            UseLargeOrderConvert = useLargeOrderConvert
        }, ct);

    /// <summary>
    /// Estimate quote
    /// </summary>
    public Task<RestCallResult<OkxFundingConvertEstimateQuote>> EstimateQuoteAsync(OkxFundingConvertEstimateQuoteRequest request, CancellationToken ct = default)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));
        if (string.IsNullOrWhiteSpace(request.BaseCurrency))
            throw new ArgumentException("Base currency is required.", nameof(request));
        if (string.IsNullOrWhiteSpace(request.QuoteCurrency))
            throw new ArgumentException("Quote currency is required.", nameof(request));
        if (string.IsNullOrWhiteSpace(request.RfqCurrency))
            throw new ArgumentException("RFQ currency is required.", nameof(request));

        var parameters = new ParameterCollection
        {
            { "baseCcy", request.BaseCurrency! },
            { "quoteCcy", request.QuoteCurrency! },
            { "rfqSz", request.RfqAmount.ToOkxString() },
            { "rfqSzCcy", request.RfqCurrency! },
        };
        parameters.AddEnum("side", request.Side);
        parameters.AddOptional("clQReqId", request.ClientOrderId);
        if (request.UseLargeOrderConvert != null)
            parameters.AddOptional("convertMode", request.UseLargeOrderConvert.Value ? "1" : "0");
        parameters.AddOptional("tag", OkxConstants.BrokerId);

        return ProcessOneRequestAsync<OkxFundingConvertEstimateQuote>(GetUri("api/v5/asset/convert/estimate-quote"), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
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
    /// <param name="useLargeOrderConvert">0: standard convert (default). 1: large order convert for VIP</param>
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
        bool? useLargeOrderConvert = null,
        CancellationToken ct = default)
        => PlaceConvertOrderAsync(new OkxFundingConvertOrderRequest
        {
            QuoteId = quoteId,
            BaseCurrency = baseCurrency,
            QuoteCurrency = quoteCurrency,
            Side = side,
            Amount = amount,
            AmountCurrency = amountCurrency,
            ClientOrderId = clientOrderId,
            UseLargeOrderConvert = useLargeOrderConvert
        }, ct);

    /// <summary>
    /// You should make estimate quote before convert trade.
    /// </summary>
    public Task<RestCallResult<OkxFundingConvertOrder>> PlaceConvertOrderAsync(OkxFundingConvertOrderRequest request, CancellationToken ct = default)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));
        if (string.IsNullOrWhiteSpace(request.QuoteId))
            throw new ArgumentException("Quote ID is required.", nameof(request));
        if (string.IsNullOrWhiteSpace(request.BaseCurrency))
            throw new ArgumentException("Base currency is required.", nameof(request));
        if (string.IsNullOrWhiteSpace(request.QuoteCurrency))
            throw new ArgumentException("Quote currency is required.", nameof(request));
        if (string.IsNullOrWhiteSpace(request.AmountCurrency))
            throw new ArgumentException("Amount currency is required.", nameof(request));

        var parameters = new ParameterCollection
        {
            { "quoteId", request.QuoteId! },
            { "baseCcy", request.BaseCurrency! },
            { "quoteCcy", request.QuoteCurrency! },
            { "sz", request.Amount.ToOkxString() },
            { "szCcy", request.AmountCurrency! },
        };
        parameters.AddEnum("side", request.Side);
        parameters.AddOptional("clQReqId", request.ClientOrderId);
        if (request.UseLargeOrderConvert != null)
            parameters.AddOptional("convertMode", request.UseLargeOrderConvert.Value ? "1" : "0");
        parameters.AddOptional("tag", OkxConstants.BrokerId);

        return ProcessOneRequestAsync<OkxFundingConvertOrder>(GetUri("api/v5/asset/convert/trade"), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
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
        => GetConvertHistoryAsync(new OkxFundingConvertHistoryRequest
        {
            ClientOrderId = clientOrderId,
            After = after,
            Before = before,
            Limit = limit,
            Tag = tag
        }, ct);

    /// <summary>
    /// Get convert history
    /// </summary>
    public Task<RestCallResult<List<OkxFundingConvertOrderHistory>>> GetConvertHistoryAsync(OkxFundingConvertHistoryRequest request, CancellationToken ct = default)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));
        request.Limit.ValidateIntBetween(nameof(request.Limit), 1, 100);
        var parameters = new ParameterCollection();
        parameters.AddOptional("clTReqId", request.ClientOrderId);
        parameters.AddOptional("after", request.After?.ToOkxString());
        parameters.AddOptional("before", request.Before?.ToOkxString());
        parameters.AddOptional("limit", request.Limit.ToOkxString());
        parameters.AddOptional("tag", request.Tag);

        return ProcessListRequestAsync<OkxFundingConvertOrderHistory>(GetUri("api/v5/asset/convert/history"), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Get available fiat deposit payment methods.
    /// </summary>
    public Task<RestCallResult<List<OkxFundingFiatPaymentMethod>>> GetDepositPaymentMethodsAsync(string currency, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "ccy", currency },
        };

        return ProcessListRequestAsync<OkxFundingFiatPaymentMethod>(GetUri("api/v5/fiat/deposit-payment-methods"), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Get available fiat withdrawal payment methods.
    /// </summary>
    public Task<RestCallResult<List<OkxFundingFiatPaymentMethod>>> GetWithdrawalPaymentMethodsAsync(string currency, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "ccy", currency },
        };

        return ProcessListRequestAsync<OkxFundingFiatPaymentMethod>(GetUri("api/v5/fiat/withdrawal-payment-methods"), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Create a fiat withdrawal order.
    /// </summary>
    public Task<RestCallResult<OkxFundingFiatOrder>> CreateWithdrawalOrderAsync(
        string paymentAccountId,
        string currency,
        decimal amount,
        string paymentMethod,
        string clientOrderId,
        CancellationToken ct = default)
        => CreateWithdrawalOrderAsync(new OkxFundingFiatWithdrawalOrderRequest
        {
            PaymentAccountId = paymentAccountId,
            Currency = currency,
            Amount = amount,
            PaymentMethod = paymentMethod,
            ClientOrderId = clientOrderId
        }, ct);

    /// <summary>
    /// Create a fiat withdrawal order.
    /// </summary>
    public Task<RestCallResult<OkxFundingFiatOrder>> CreateWithdrawalOrderAsync(OkxFundingFiatWithdrawalOrderRequest request, CancellationToken ct = default)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));
        if (string.IsNullOrWhiteSpace(request.PaymentAccountId))
            throw new ArgumentException("Payment account ID is required.", nameof(request));
        if (string.IsNullOrWhiteSpace(request.Currency))
            throw new ArgumentException("Currency is required.", nameof(request));
        if (string.IsNullOrWhiteSpace(request.PaymentMethod))
            throw new ArgumentException("Payment method is required.", nameof(request));
        if (string.IsNullOrWhiteSpace(request.ClientOrderId))
            throw new ArgumentException("Client order ID is required.", nameof(request));

        var parameters = new ParameterCollection
        {
            { "paymentAcctId", request.PaymentAccountId! },
            { "ccy", request.Currency! },
            { "amt", request.Amount.ToOkxString() },
            { "paymentMethod", request.PaymentMethod! },
            { "clientId", request.ClientOrderId! },
        };

        return ProcessOneRequestAsync<OkxFundingFiatOrder>(GetUri("api/v5/fiat/create-withdrawal"), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Cancel a fiat withdrawal order.
    /// </summary>
    public Task<RestCallResult<OkxFundingFiatOrder>> CancelWithdrawalOrderAsync(string orderId, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "ordId", orderId },
        };

        return ProcessOneRequestAsync<OkxFundingFiatOrder>(GetUri("api/v5/fiat/cancel-withdrawal"), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Get fiat withdrawal order history.
    /// </summary>
    public Task<RestCallResult<List<OkxFundingFiatOrder>>> GetWithdrawalOrderHistoryAsync(
        string? currency = null,
        string? paymentMethod = null,
        string? state = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
        => GetWithdrawalOrderHistoryAsync(new OkxFundingFiatOrderHistoryRequest
        {
            Currency = currency,
            PaymentMethod = paymentMethod,
            State = state,
            After = after,
            Before = before,
            Limit = limit
        }, ct);

    /// <summary>
    /// Get fiat withdrawal order history.
    /// </summary>
    public Task<RestCallResult<List<OkxFundingFiatOrder>>> GetWithdrawalOrderHistoryAsync(OkxFundingFiatOrderHistoryRequest request, CancellationToken ct = default)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));
        request.Limit.ValidateIntBetween(nameof(request.Limit), 1, 100);
        var parameters = new ParameterCollection();
        parameters.AddOptional("ccy", request.Currency);
        parameters.AddOptional("paymentMethod", request.PaymentMethod);
        parameters.AddOptional("state", request.State);
        parameters.AddOptional("after", request.After?.ToOkxString());
        parameters.AddOptional("before", request.Before?.ToOkxString());
        parameters.AddOptional("limit", request.Limit.ToOkxString());

        return ProcessListRequestAsync<OkxFundingFiatOrder>(GetUri("api/v5/fiat/withdrawal-order-history"), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Get fiat withdrawal order detail.
    /// </summary>
    public Task<RestCallResult<OkxFundingFiatOrder>> GetWithdrawalOrderDetailAsync(string orderId, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "ordId", orderId },
        };

        return ProcessOneRequestAsync<OkxFundingFiatOrder>(GetUri("api/v5/fiat/withdrawal"), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Get fiat deposit order history.
    /// </summary>
    public Task<RestCallResult<List<OkxFundingFiatOrder>>> GetDepositOrderHistoryAsync(
        string? currency = null,
        string? paymentMethod = null,
        string? state = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
        => GetDepositOrderHistoryAsync(new OkxFundingFiatOrderHistoryRequest
        {
            Currency = currency,
            PaymentMethod = paymentMethod,
            State = state,
            After = after,
            Before = before,
            Limit = limit
        }, ct);

    /// <summary>
    /// Get fiat deposit order history.
    /// </summary>
    public Task<RestCallResult<List<OkxFundingFiatOrder>>> GetDepositOrderHistoryAsync(OkxFundingFiatOrderHistoryRequest request, CancellationToken ct = default)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));
        request.Limit.ValidateIntBetween(nameof(request.Limit), 1, 100);
        var parameters = new ParameterCollection();
        parameters.AddOptional("ccy", request.Currency);
        parameters.AddOptional("paymentMethod", request.PaymentMethod);
        parameters.AddOptional("state", request.State);
        parameters.AddOptional("after", request.After?.ToOkxString());
        parameters.AddOptional("before", request.Before?.ToOkxString());
        parameters.AddOptional("limit", request.Limit.ToOkxString());

        return ProcessListRequestAsync<OkxFundingFiatOrder>(GetUri("api/v5/fiat/deposit-order-history"), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Get fiat deposit order detail.
    /// </summary>
    public Task<RestCallResult<OkxFundingFiatOrder>> GetDepositOrderDetailAsync(string orderId, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "ordId", orderId },
        };

        return ProcessOneRequestAsync<OkxFundingFiatOrder>(GetUri("api/v5/fiat/deposit"), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Get available buy/sell fiat and crypto currencies.
    /// </summary>
    public Task<RestCallResult<OkxFundingBuySellCurrencies>> GetBuySellCurrenciesAsync(CancellationToken ct = default)
    {
        return ProcessOneRequestAsync<OkxFundingBuySellCurrencies>(GetUri("api/v5/fiat/buy-sell/currencies"), HttpMethod.Get, ct, signed: true);
    }

    /// <summary>
    /// Get buy/sell pair constraints.
    /// </summary>
    public Task<RestCallResult<OkxFundingBuySellCurrencyPair>> GetBuySellCurrencyPairAsync(string fromCurrency, string toCurrency, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "fromCcy", fromCurrency },
            { "toCcy", toCurrency },
        };

        return ProcessOneRequestAsync<OkxFundingBuySellCurrencyPair>(GetUri("api/v5/fiat/buy-sell/currency-pair"), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Get a buy/sell quote.
    /// </summary>
    public Task<RestCallResult<OkxFundingBuySellQuote>> GetBuySellQuoteAsync(
        OkxTradeOrderSide side,
        string fromCurrency,
        string toCurrency,
        decimal rfqAmount,
        string rfqCurrency,
        CancellationToken ct = default)
        => GetBuySellQuoteAsync(new OkxFundingBuySellQuoteRequest
        {
            Side = side,
            FromCurrency = fromCurrency,
            ToCurrency = toCurrency,
            RfqAmount = rfqAmount,
            RfqCurrency = rfqCurrency
        }, ct);

    /// <summary>
    /// Get a buy/sell quote.
    /// </summary>
    public Task<RestCallResult<OkxFundingBuySellQuote>> GetBuySellQuoteAsync(OkxFundingBuySellQuoteRequest request, CancellationToken ct = default)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));
        if (string.IsNullOrWhiteSpace(request.FromCurrency))
            throw new ArgumentException("From currency is required.", nameof(request));
        if (string.IsNullOrWhiteSpace(request.ToCurrency))
            throw new ArgumentException("To currency is required.", nameof(request));
        if (string.IsNullOrWhiteSpace(request.RfqCurrency))
            throw new ArgumentException("RFQ currency is required.", nameof(request));

        var parameters = new ParameterCollection
        {
            { "fromCcy", request.FromCurrency! },
            { "toCcy", request.ToCurrency! },
            { "rfqAmt", request.RfqAmount.ToOkxString() },
            { "rfqCcy", request.RfqCurrency! },
        };
        parameters.AddEnum("side", request.Side);

        return ProcessOneRequestAsync<OkxFundingBuySellQuote>(GetUri("api/v5/fiat/buy-sell/quote"), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Execute a buy/sell trade from a quote.
    /// </summary>
    public Task<RestCallResult<OkxFundingBuySellOrder>> PlaceBuySellTradeAsync(
        string quoteId,
        OkxTradeOrderSide side,
        string fromCurrency,
        string toCurrency,
        decimal rfqAmount,
        string rfqCurrency,
        string paymentMethod,
        string clientOrderId,
        CancellationToken ct = default)
        => PlaceBuySellTradeAsync(new OkxFundingBuySellTradeRequest
        {
            QuoteId = quoteId,
            Side = side,
            FromCurrency = fromCurrency,
            ToCurrency = toCurrency,
            RfqAmount = rfqAmount,
            RfqCurrency = rfqCurrency,
            PaymentMethod = paymentMethod,
            ClientOrderId = clientOrderId
        }, ct);

    /// <summary>
    /// Execute a buy/sell trade from a quote.
    /// </summary>
    public Task<RestCallResult<OkxFundingBuySellOrder>> PlaceBuySellTradeAsync(OkxFundingBuySellTradeRequest request, CancellationToken ct = default)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));
        if (string.IsNullOrWhiteSpace(request.QuoteId))
            throw new ArgumentException("Quote ID is required.", nameof(request));
        if (string.IsNullOrWhiteSpace(request.FromCurrency))
            throw new ArgumentException("From currency is required.", nameof(request));
        if (string.IsNullOrWhiteSpace(request.ToCurrency))
            throw new ArgumentException("To currency is required.", nameof(request));
        if (string.IsNullOrWhiteSpace(request.RfqCurrency))
            throw new ArgumentException("RFQ currency is required.", nameof(request));
        if (string.IsNullOrWhiteSpace(request.PaymentMethod))
            throw new ArgumentException("Payment method is required.", nameof(request));
        if (string.IsNullOrWhiteSpace(request.ClientOrderId))
            throw new ArgumentException("Client order ID is required.", nameof(request));

        var parameters = new ParameterCollection
        {
            { "quoteId", request.QuoteId! },
            { "fromCcy", request.FromCurrency! },
            { "toCcy", request.ToCurrency! },
            { "rfqAmt", request.RfqAmount.ToOkxString() },
            { "rfqCcy", request.RfqCurrency! },
            { "paymentMethod", request.PaymentMethod! },
            { "clOrdId", request.ClientOrderId! },
        };
        parameters.AddEnum("side", request.Side);

        return ProcessOneRequestAsync<OkxFundingBuySellOrder>(GetUri("api/v5/fiat/buy-sell/trade"), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Get buy/sell trade history.
    /// </summary>
    public Task<RestCallResult<List<OkxFundingBuySellOrder>>> GetBuySellTradeHistoryAsync(
        string? orderId = null,
        string? clientOrderId = null,
        string? state = null,
        long? begin = null,
        long? end = null,
        int limit = 100,
        CancellationToken ct = default)
        => GetBuySellTradeHistoryAsync(new OkxFundingBuySellHistoryRequest
        {
            OrderId = orderId,
            ClientOrderId = clientOrderId,
            State = state,
            Begin = begin,
            End = end,
            Limit = limit
        }, ct);

    /// <summary>
    /// Get buy/sell trade history.
    /// </summary>
    public Task<RestCallResult<List<OkxFundingBuySellOrder>>> GetBuySellTradeHistoryAsync(OkxFundingBuySellHistoryRequest request, CancellationToken ct = default)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));
        request.Limit.ValidateIntBetween(nameof(request.Limit), 1, 100);
        var parameters = new ParameterCollection();
        parameters.AddOptional("ordId", request.OrderId);
        parameters.AddOptional("clOrdId", request.ClientOrderId);
        parameters.AddOptional("state", request.State);
        parameters.AddOptional("begin", request.Begin?.ToOkxString());
        parameters.AddOptional("end", request.End?.ToOkxString());
        parameters.AddOptional("limit", request.Limit.ToOkxString());

        return ProcessListRequestAsync<OkxFundingBuySellOrder>(GetUri("api/v5/fiat/buy-sell/history"), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

}


