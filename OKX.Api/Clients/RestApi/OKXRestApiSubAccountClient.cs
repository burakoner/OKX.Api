using OKX.Api.Models.SubAccount;


namespace OKX.Api.Clients.RestApi;

/// <summary>
/// OKX Rest Api Sub Account Client
/// </summary>
public class OKXRestApiSubAccountClient : OKXRestApiBaseClient
{
    // Endpoints
    private const string v5UsersSubaccountList = "api/v5/users/subaccount/list";
    private const string v5UsersSubaccountResetApiKey = "api/v5/users/subaccount/modify-apikey";
    private const string v5UsersSubaccountTradingBalances = "api/v5/account/subaccount/balances";
    private const string v5UsersSubaccountFundingBalances = "api/v5/asset/subaccount/balances";
    // TODO: api/v5/account/subaccount/max-withdrawal
    private const string v5UsersSubaccountBills = "api/v5/asset/subaccount/bills";
    // TODO: api/v5/asset/subaccount/managed-subaccount-bills
    private const string v5UsersSubaccountTransfer = "api/v5/asset/subaccount/transfer";
    // TODO: api/v5/users/subaccount/set-transfer-out
    // TODO: api/v5/users/entrust-subaccount-list
    // TODO: api/v5/users/partner/if-rebate
    // TODO: api/v5/account/subaccount/set-loan-allocation
    // TODO: api/v5/account/subaccount/interest-limits
    protected const string v5BrokerCreateSubaccount = "api/v5/broker/nd/create-subaccount";
    protected const string v5BrokerCreateAddressSubacount = "api/v5/asset/broker/nd/subaccount-deposit-address";
    protected const string v5BrokerCreateApiSubacount = "api/v5/broker/nd/subaccount/apikey";
    protected const string v5BrokerSubaccountList = "api/v5/broker/nd/subaccount-info";
    protected const string v5BrokerSubaccountRemoveApiKey = "api/v5/broker/nd/subaccount/delete-apikey";
    protected const string v5BrokerSubaccountRemove = "/api/v5/broker/nd/delete-subaccount";

    internal OKXRestApiSubAccountClient(OKXRestApiClient root) : base(root)
    {
    }

    #region Sub-Account API Endpoints
    /// <summary>
    /// applies to master accounts only
    /// </summary>
    /// <param name="enable">Sub-account status，true: Normal ; false: Frozen</param>
    /// <param name="subAccountName">Sub Account Name</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<IEnumerable<OkxSubAccount>>> GetSubAccountsAsync(
        bool? enable = null,
        string subAccountName = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("enable", enable);
        parameters.AddOptionalParameter("subAcct", subAccountName);
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return await SendOKXRequest<IEnumerable<OkxSubAccount>>(GetUri(v5UsersSubaccountList), HttpMethod.Get, ct, signed: true, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// applies to master accounts only
    /// </summary>
    /// <param name="subAccountName">Sub Account Name</param>
    /// <param name="apiKey">APIKey note</param>
    /// <param name="apiLabel">APIKey note</param>
    /// <param name="readPermission">Read Permission</param>
    /// <param name="tradePermission">Trade Permission</param>
    /// <param name="ipAddresses">Link IP addresses, separate with commas if more than one. Support up to 20 IP addresses.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<OkxSubAccountApiKey>> ResetSubAccountApiKeyAsync(
        string subAccountName,
        string apiKey,
        string apiLabel = null,
        bool? readPermission = null,
        bool? tradePermission = null,
        string ipAddresses = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "subAcct", subAccountName },
            { "apiKey", apiKey},
            { "label", apiLabel },
        };
        parameters.AddOptionalParameter("ip", ipAddresses);

        var permissions = new List<string>();
        if (readPermission.HasValue && readPermission.Value) permissions.Add("read_only");
        if (tradePermission.HasValue && tradePermission.Value) permissions.Add("trade");
        if (permissions.Count > 0)
            parameters.AddOptionalParameter("perm", string.Join(",", permissions));

        return await SendOKXSingleRequest<OkxSubAccountApiKey>(GetUri(v5UsersSubaccountResetApiKey), HttpMethod.Post, ct, signed: true, bodyParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Query detailed balance info of Trading Account of a sub-account via the master account (applies to master accounts only)
    /// </summary>
    /// <param name="subAccountName">Sub Account Name</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<OkxSubAccountTradingBalance>> GetSubAccountTradingBalancesAsync(
        string subAccountName,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            {"subAcct", subAccountName },
        };

        return await SendOKXSingleRequest<OkxSubAccountTradingBalance>(GetUri(v5UsersSubaccountTradingBalances), HttpMethod.Get, ct, signed: true, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Get sub-account funding balance
    /// Query detailed balance info of Funding Account of a sub-account via the master account (applies to master accounts only)
    /// </summary>
    /// <param name="subAccountName">Sub Account Name</param>
    /// <param name="currency">Single currency or multiple currencies (no more than 20) separated with comma, e.g. BTC or BTC,ETH.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<OkxSubAccountFundingBalance>> GetSubAccountFundingBalancesAsync(
        string subAccountName,
        string currency = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "subAcct", subAccountName },
        };

        parameters.AddOptionalParameter("ccy", currency);

        return await SendOKXSingleRequest<OkxSubAccountFundingBalance>(GetUri(v5UsersSubaccountFundingBalances), HttpMethod.Get, ct, signed: true, queryParameters: parameters).ConfigureAwait(false);
    }

    // TODO: Get sub-account maximum withdrawals

    /// <summary>
    /// applies to master accounts only
    /// </summary>
    /// <param name="subAccountName">Sub Account Name</param>
    /// <param name="currency">Currency</param>
    /// <param name="type">0: Transfers from master account to sub-account ;1 : Transfers from sub-account to master account.</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<IEnumerable<OkxSubAccountBill>>> GetSubAccountBillsAsync(
        string subAccountName = null,
        string currency = null,
        OkxSubAccountTransferType? type = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("subAcct", subAccountName);
        parameters.AddOptionalParameter("ccy", currency);
        parameters.AddOptionalParameter("type", JsonConvert.SerializeObject(type, new SubAccountTransferTypeConverter(false)));
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return await SendOKXRequest<IEnumerable<OkxSubAccountBill>>(GetUri(v5UsersSubaccountBills), HttpMethod.Get, ct, signed: true, queryParameters: parameters).ConfigureAwait(false);
    }

    // TODO: Get history of managed sub-account transfer

    /// <summary>
    /// applies to master accounts only
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="amount">Amount</param>
    /// <param name="fromAccount">6:Funding Account 18:Unified Account</param>
    /// <param name="toAccount">6:Funding Account 18:Unified Account</param>
    /// <param name="fromSubAccountName">Sub-account name of the account that transfers funds out.</param>
    /// <param name="toSubAccountName">Sub-account name of the account that transfers funds in.</param>
    /// <param name="loanTrans">Whether or not borrowed coins can be transferred out under Multi-currency margin and Portfolio margin. the default is false</param>
    /// <param name="omitPosRisk">Ignore position risk. Default is false. Applicable to Portfolio margin</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<OkxSubAccountTransfer>> TransferBetweenSubAccountsAsync(
        string currency,
        decimal amount,
        OkxAccount fromAccount,
        OkxAccount toAccount,
        string fromSubAccountName,
        string toSubAccountName,
        bool? loanTrans = null,
        bool? omitPosRisk = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            {"ccy", currency },
            {"amt", amount.ToOkxString() },
            {"from", JsonConvert.SerializeObject(fromAccount, new AccountConverter(false)) },
            {"to", JsonConvert.SerializeObject(toAccount, new AccountConverter(false)) },
            {"fromSubAccount", fromSubAccountName },
            {"toSubAccount", toSubAccountName },
        };
        parameters.AddOptionalParameter("loanTrans", loanTrans);
        parameters.AddOptionalParameter("omitPosRisk", omitPosRisk);

        return await SendOKXSingleRequest<OkxSubAccountTransfer>(GetUri(v5UsersSubaccountTransfer), HttpMethod.Post, ct, signed: true, bodyParameters: parameters).ConfigureAwait(false);
    }
/// <summary>
    /// Create a sub-account from the broker master account.
    /// </summary>
    /// <param name="subAcct">Sub-account name 6-20 letters(case sensitive) or numbers, which can be pure letters and not pure numbers.</param>
    /// <param name="Label">Sub-account notes No more than 50 letters(case sensitive) or numbers, which can be pure letters or pure numbers.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<OkxSubCreateAccount>> BrokerCreateSubAccountAsync(string subAcct,string Label, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            {"subAcct",subAcct },
            {"label", Label}
        };
        return await SendOKXSingleRequest<OkxSubCreateAccount>(GetUri(v5BrokerCreateSubaccount), HttpMethod.Post, ct, signed: true, bodyParameters: parameters).ConfigureAwait(false);
    }
    /// <summary>
    /// Create deposit address for sub-account
    /// </summary>
    /// <param name="subAcct">Sub-account name</param>
    /// <param name="currency">Currency, e.g. USDT</param>
    /// <param name="chain">Chain name, e.g. USDT-ERC20, USDT-TRC20</param>
    /// <param name="toAccount">The beneficiary account</param>
    /// <param name="depositAddressType">Deposit address type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<OkxSubAccountCreateAddress>> BrokerCreateAddressSubAccount(string subAcct,string currency,string chain,
        OkxAccount toAccount,
        OkxDepositAddressType depositAddressType,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            {"ccy", currency },
            {"subAcct",subAcct },
            {"chain",chain },
            {"to", JsonConvert.SerializeObject(toAccount, new AccountConverter(false)) },
            {"addrType", JsonConvert.SerializeObject(depositAddressType, new DepositAddressTypeConverter(false)) },
        };
        return await SendOKXSingleRequest<OkxSubAccountCreateAddress>(GetUri(v5BrokerCreateAddressSubacount), HttpMethod.Post, ct, signed: true, bodyParameters: parameters).ConfigureAwait(false);

    }
    /// <summary>
    /// Create an API Key for a sub-account
    /// </summary>
    /// <param name="subAcc">Sub-account name, supports 6 to 20 characters that include numbers and letters (case sensitive, space character is not supported).</param>
    /// <param name="label">API Key note</param>
    /// <param name="password">API Key password, supports 8 to 32 alphanumeric characters containing at least 1 number, 1 uppercase letter, 1 lowercase letter and 1 special character.</param>
    /// <param name="ips">Link IP addresses, separate with commas if more than one. Support up to 20 addresses.</param>
    /// <param name="perm">API Key permissions read_only: Read only, trade: Trade, withdraw: Withdraw</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<OkxSubAccountCreateApi>> BrokerCreateSubAccountApiAsync(string subAcc, string label, string password, string ips, string perm = "trade,withdraw", CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "subAcct",subAcc},
            { "label", label },
            { "passphrase", password },
            {"perm",perm },
            {"ip",ips }

        };
        return await SendOKXSingleRequest<OkxSubAccountCreateApi>(GetUri(v5BrokerCreateApiSubacount), HttpMethod.Post, ct, signed: true, bodyParameters: parameters).ConfigureAwait(false);

    }
    /// <summary>
    /// Broker: Get sub-account list
    /// </summary>
    /// <param name="subAcct">Sub-account name</param>
    /// <param name="uid">Sub-account UID</param>
    /// <param name="page">Page for pagination</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<OkxSubAccountDetailListModel>> BrokerListSubAccountAsync( string subAcct ="",string uid = "",int page = 1,int limit = 100, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            {"subAcct",subAcct}, 
            {"uid",uid},
            {"page",page.ToString(OkxGlobals.OkxCultureInfo)},
            {"limit",limit.ToString(OkxGlobals.OkxCultureInfo)}
        };
        //parameters.AddOptionalParameter("after", after?.ToString(OkxGlobals.OkxCultureInfo));
        return await SendOKXSingleRequest<OkxSubAccountDetailListModel>(GetUri(v5BrokerSubaccountList), HttpMethod.Get, ct, signed: true, queryParameters: parameters).ConfigureAwait(false);
    }
    /// <summary>
    /// Delete the API Key of sub-accounts
    /// </summary>
    /// <param name="subAcct">Sub-account name</param>
    /// <param name="apiKey">Api Key</param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<OkxSubAccountName>> BrokerRemoveSubAccountApiKeyAsync(string subAcct,string apiKey, CancellationToken ct = default) 
    {
        var parameters = new Dictionary<string, object>
        {
            {"subAcct",subAcct },
            {"apiKey",apiKey}

        };
        return await SendOKXSingleRequest<OkxSubAccountName>(GetUri(v5BrokerSubaccountRemoveApiKey), HttpMethod.Post, ct, signed: true, bodyParameters: parameters).ConfigureAwait(false);
    }
    /// <summary>
    /// Delete sub-account
    /// </summary>
    /// <param name="subAcct">Sub-account name</param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<OkxSubAccountName>> BrokerRemoveSubAccountAsync (string subAcct, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            {"subAcct",subAcct },
            
        };
        return await SendOKXSingleRequest<OkxSubAccountName>(GetUri(v5BrokerSubaccountRemove), HttpMethod.Post, ct, signed: true, bodyParameters: parameters).ConfigureAwait(false);
    }
    // TODO: Set Permission Of Transfer Out
    // TODO: Get custody trading sub-account list
    // TODO: Get the user's affiliate rebate information
    #endregion

}