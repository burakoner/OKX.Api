using OKX.Api.Models.FundingAccount;

namespace OKX.Api.Clients.RestApi;

/// <summary>
/// OKX Rest Api Funding Account Client
/// </summary>
public class OKXRestApiFundingAccountClient : OKXRestApiBaseClient
{
    // Endpoints
    private const string v5AssetCurrencies = "api/v5/asset/currencies";
    private const string v5AssetBalances = "api/v5/asset/balances";
    // TODO: api/v5/asset/non-tradable-assets
    // TODO: api/v5/asset/asset-valuation
    private const string v5AssetTransfer = "api/v5/asset/transfer";
    // TODO: api/v5/asset/transfer-state
    private const string v5AssetBills = "api/v5/asset/bills";
    private const string v5AssetDepositLightning = "api/v5/asset/deposit-lightning";
    private const string v5AssetDepositAddress = "api/v5/asset/deposit-address";
    private const string v5AssetDepositHistory = "api/v5/asset/deposit-history";
    private const string v5AssetWithdrawal = "api/v5/asset/withdrawal";
    private const string v5AssetWithdrawalLightning = "api/v5/asset/withdrawal-lightning";
    private const string v5AssetWithdrawalCancel = "api/v5/asset/cancel-withdrawal";
    private const string v5AssetWithdrawalHistory = "api/v5/asset/withdrawal-history";
    // TODO: api/v5/asset/deposit-withdraw-status
    // TODO: api/v5/asset/convert-dust-assets
    // TODO: api/v5/asset/convert/currencies
    // TODO: api/v5/asset/convert/currency-pair
    // TODO: api/v5/asset/convert/estimate-quote
    // TODO: api/v5/asset/convert/trade
    // TODO: api/v5/asset/convert/history

    internal OKXRestApiFundingAccountClient(OKXRestApiClient root) : base(root)
    {
    }

    #region Funding API Endpoints

    /// <summary>
    /// Retrieve a list of all currencies. Not all currencies can be traded. Currencies that have not been defined in ISO 4217 may use a custom symbol.
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<OkxCurrency>>> GetCurrenciesAsync(CancellationToken ct = default)
    {
        return await SendOKXRequest<IEnumerable<OkxCurrency>>(GetUri(v5AssetCurrencies), HttpMethod.Get, ct, true).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve the balances of all the assets, and the amount that is available or on hold.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<OkxFundingBalance>>> GetFundingBalanceAsync(string currency = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("ccy", currency);

        return await SendOKXRequest<IEnumerable<OkxFundingBalance>>(GetUri(v5AssetBalances), HttpMethod.Get, ct, signed: true, queryParameters: parameters).ConfigureAwait(false);
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
    public virtual async Task<RestCallResult<OkxTransferResponse>> FundTransferAsync(
        string currency,
        decimal amount,
        OkxTransferType type,
        OkxAccount fromAccount,
        OkxAccount toAccount,
        string subAccountName = null,
        bool? loanTransfer = null,
        bool? omitPositionRisk = null,
        string clientOrderId = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "ccy",currency},
            { "amt",amount.ToOkxString()},
            { "from", JsonConvert.SerializeObject(fromAccount, new AccountConverter(false)) },
            { "to", JsonConvert.SerializeObject(toAccount, new AccountConverter(false)) },
            { "type", JsonConvert.SerializeObject(type, new TransferTypeConverter(false)) },
        };
        parameters.AddOptionalParameter("subAcct", subAccountName);
        parameters.AddOptionalParameter("loanTrans", loanTransfer);
        parameters.AddOptionalParameter("clientId", clientOrderId);
        parameters.AddOptionalParameter("omitPosRisk", omitPositionRisk);

        return await SendOKXSingleRequest<OkxTransferResponse>(GetUri(v5AssetTransfer), HttpMethod.Post, ct, signed: true, bodyParameters: parameters).ConfigureAwait(false);
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
    public virtual async Task<RestCallResult<IEnumerable<OkxFundingBill>>> GetFundingBillDetailsAsync(
        string currency = null,
        OkxFundingBillType? type = null,
        string clientOrderId = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("ccy", currency);
        parameters.AddOptionalParameter("clientId", clientOrderId);
        parameters.AddOptionalParameter("type", JsonConvert.SerializeObject(type, new FundingBillTypeConverter(false)));
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return await SendOKXRequest<IEnumerable<OkxFundingBill>>(GetUri(v5AssetBills), HttpMethod.Get, ct, signed: true, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Users can create up to 10,000 different invoices within 24 hours.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="amount">deposit amount between 0.000001 - 0.1</param>
    /// <param name="account">Receiving account</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<OkxLightningDeposit>>> GetLightningDepositsAsync(
        string currency,
        decimal amount,
        OkxLightningDepositAccount? account = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "ccy", currency },
            { "amt", amount.ToOkxString() },
        };
        parameters.AddOptionalParameter("to", JsonConvert.SerializeObject(account, new LightningDepositAccountConverter(false)));

        return await SendOKXRequest<IEnumerable<OkxLightningDeposit>>(GetUri(v5AssetDepositLightning), HttpMethod.Get, ct, signed: true, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve the deposit addresses of currencies, including previously-used addresses.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<OkxDepositAddress>>> GetDepositAddressAsync(string currency = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "ccy", currency },
        };

        return await SendOKXRequest<IEnumerable<OkxDepositAddress>>(GetUri(v5AssetDepositAddress), HttpMethod.Get, ct, signed: true, queryParameters: parameters).ConfigureAwait(false);
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
    public virtual async Task<RestCallResult<IEnumerable<OkxDepositHistory>>> GetDepositHistoryAsync(
        string currency = null,
        string depositId = null,
        long? fromWithdrawalId = null,
        string transactionId = null,
        OkxDepositType? type = null,
        OkxDepositState? state = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("ccy", currency);
        parameters.AddOptionalParameter("depId", depositId);
        parameters.AddOptionalParameter("fromWdId", fromWithdrawalId);
        parameters.AddOptionalParameter("txId", transactionId);
        parameters.AddOptionalParameter("type", JsonConvert.SerializeObject(type, new DepositTypeConverter(false)));
        parameters.AddOptionalParameter("state", JsonConvert.SerializeObject(state, new DepositStateConverter(false)));
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return await SendOKXRequest<IEnumerable<OkxDepositHistory>>(GetUri(v5AssetDepositHistory), HttpMethod.Get, ct, signed: true, queryParameters: parameters).ConfigureAwait(false);
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
    /// <param name="clientOrderId">Client-supplied ID. A combination of case-sensitive alphanumerics, all numbers, or all letters of up to 32 characters.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<OkxWithdrawalResponse>> WithdrawAsync(
        string currency,
        decimal amount,
        OkxWithdrawalDestination destination,
        string toAddress,
        decimal fee,
        string chain = null,
        string areaCode = null,
        string clientOrderId = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "ccy",currency},
            { "amt",amount.ToOkxString()},
            { "dest", JsonConvert.SerializeObject(destination, new WithdrawalDestinationConverter(false)) },
            { "toAddr",toAddress},
            { "fee",fee   .ToOkxString()},
        };
        parameters.AddOptionalParameter("chain", chain);
        parameters.AddOptionalParameter("areaCode", areaCode);
        parameters.AddOptionalParameter("clientId", clientOrderId);

        return await SendOKXSingleRequest<OkxWithdrawalResponse>(GetUri(v5AssetWithdrawal), HttpMethod.Post, ct, signed: true, bodyParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// The maximum withdrawal amount is 0.1 BTC per request, and 1 BTC in 24 hours. The minimum withdrawal amount is approximately 0.000001 BTC. Sub-account does not support withdrawal.
    /// </summary>
    /// <param name="currency">Token symbol. Currently only BTC is supported.</param>
    /// <param name="invoice">Invoice text</param>
    /// <param name="memo">Lightning withdrawal memo</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<OkxLightningWithdrawal>> GetLightningWithdrawalsAsync(
        string currency,
        string invoice,
        string memo = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "ccy", currency },
            { "invoice", invoice },
        };
        parameters.AddOptionalParameter("memo", memo);

        return await SendOKXSingleRequest<OkxLightningWithdrawal>(GetUri(v5AssetWithdrawalLightning), HttpMethod.Get, ct, signed: true, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Cancel withdrawal
    /// You can cancel normal withdrawal requests, but you cannot cancel withdrawal requests on Lightning.
    /// Rate Limit: 6 requests per second
    /// </summary>
    /// <param name="withdrawalId">Withdrawal ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<OkxWithdrawalId>> CancelWithdrawalAsync(long withdrawalId, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "wdId", withdrawalId},
        };

        return await SendOKXSingleRequest<OkxWithdrawalId>(GetUri(v5AssetWithdrawalCancel), HttpMethod.Post, ct, signed: true, bodyParameters: parameters).ConfigureAwait(false);
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
    public virtual async Task<RestCallResult<IEnumerable<OkxWithdrawalHistory>>> GetWithdrawalHistoryAsync(
        string currency = null,
        long? withdrawalId = null,
        string clientOrderId = null,
        string transactionId = null,
        OkxWithdrawalType? type = null,
        OkxWithdrawalState? state = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("ccy", currency);
        parameters.AddOptionalParameter("wdId", withdrawalId);
        parameters.AddOptionalParameter("clientId", clientOrderId);
        parameters.AddOptionalParameter("txId", transactionId);
        parameters.AddOptionalParameter("type", JsonConvert.SerializeObject(type, new WithdrawalTypeConverter(false)));
        parameters.AddOptionalParameter("state", JsonConvert.SerializeObject(state, new WithdrawalStateConverter(false)));
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return await SendOKXRequest<IEnumerable<OkxWithdrawalHistory>>(GetUri(v5AssetWithdrawalHistory), HttpMethod.Get, ct, signed: true, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

}