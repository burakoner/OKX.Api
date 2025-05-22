namespace OKX.Api.SubAccount;

/// <summary>
/// OKX Rest Api Sub Account Client
/// </summary>
public class OkxSubAccountRestClient(OkxRestApiClient root) : OkxBaseRestClient(root)
{
    // Endpoints
    // TODO: api/v5/users/subaccount/create-subaccount
    // TODO: Create an API Key for a sub-account
    // TODO: Query the API Key of a sub-account
    // TODO: Delete the API Key of sub-accounts
    private const string v5UsersSubaccountList = "api/v5/users/subaccount/list";
    private const string v5UsersSubaccountResetApiKey = "api/v5/users/subaccount/modify-apikey";
    private const string v5UsersSubaccountTradingBalances = "api/v5/account/subaccount/balances";
    private const string v5UsersSubaccountFundingBalances = "api/v5/asset/subaccount/balances";
    private const string v5AccountSubaccountMaxWithdrawal = "api/v5/account/subaccount/max-withdrawal";
    private const string v5UsersSubaccountBills = "api/v5/asset/subaccount/bills";
    private const string v5AssetSubaccountManagedSubaccountBills = "api/v5/asset/subaccount/managed-subaccount-bills";
    private const string v5UsersSubaccountTransfer = "api/v5/asset/subaccount/transfer";
    private const string v5UsersSubaccountSetTransferOut = "api/v5/users/subaccount/set-transfer-out";
    private const string v5UsersEntrustSubaccountList = "api/v5/users/entrust-subaccount-list";

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
    public Task<RestCallResult<List<OkxSubAccount>>> GetSubAccountsAsync(
        bool? enable = null,
        string? subAccountName = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new ParameterCollection();
        parameters.AddOptional("enable", enable);
        parameters.AddOptional("subAcct", subAccountName);
        parameters.AddOptional("after", after?.ToOkxString());
        parameters.AddOptional("before", before?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxSubAccount>(GetUri(v5UsersSubaccountList), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
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
    public Task<RestCallResult<OkxSubAccountApiKey>> ResetSubAccountApiKeyAsync(
        string subAccountName,
        string apiKey,
        string? apiLabel = null,
        bool? readPermission = null,
        bool? tradePermission = null,
        string? ipAddresses = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "subAcct", subAccountName },
            { "apiKey", apiKey},
        };
        parameters.AddOptional("label", apiLabel);
        parameters.AddOptional("ip", ipAddresses);

        var permissions = new List<string>();
        if (readPermission.HasValue && readPermission.Value) permissions.Add("read_only");
        if (tradePermission.HasValue && tradePermission.Value) permissions.Add("trade");
        if (permissions.Count > 0) parameters.AddOptional("perm", string.Join(",", permissions));

        return ProcessOneRequestAsync<OkxSubAccountApiKey>(GetUri(v5UsersSubaccountResetApiKey), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Query detailed balance info of Trading Account of a sub-account via the master account (applies to master accounts only)
    /// </summary>
    /// <param name="subAccountName">Sub Account Name</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxSubAccountTradingBalance>> GetSubAccountTradingBalancesAsync(
        string subAccountName,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            {"subAcct", subAccountName },
        };

        return ProcessOneRequestAsync<OkxSubAccountTradingBalance>(GetUri(v5UsersSubaccountTradingBalances), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Get sub-account funding balance
    /// Query detailed balance info of Funding Account of a sub-account via the master account (applies to master accounts only)
    /// </summary>
    /// <param name="subAccountName">Sub Account Name</param>
    /// <param name="currency">Single currency or multiple currencies (no more than 20) separated with comma, e.g. BTC or BTC,ETH.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxSubAccountFundingBalance>> GetSubAccountFundingBalancesAsync(
        string subAccountName,
        string? currency = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "subAcct", subAccountName },
        };

        parameters.AddOptional("ccy", currency);

        return ProcessOneRequestAsync<OkxSubAccountFundingBalance>(GetUri(v5UsersSubaccountFundingBalances), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve the maximum withdrawal information of a sub-account via the master account (applies to master accounts only). If no currency is specified, the transferable amount of all owned currencies will be returned.
    /// </summary>
    /// <param name="subAccountName">Sub Account Name</param>
    /// <param name="currency">Single currency or multiple currencies (no more than 20) separated with comma, e.g. BTC or BTC,ETH.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxSubAccountMaximumWithdrawal>>> GetSubAccountMaximumWithdrawalsAsync(
        string subAccountName,
        string? currency = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "subAcct", subAccountName },
        };

        parameters.AddOptional("ccy", currency);

        return ProcessListRequestAsync<OkxSubAccountMaximumWithdrawal>(GetUri(v5AccountSubaccountMaxWithdrawal), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

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
    public Task<RestCallResult<List<OkxSubAccountBill>>> GetSubAccountBillsAsync(
        string? subAccountName = null,
        string? currency = null,
        OkxSubAccountTransferType? type = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);

        var parameters = new ParameterCollection();
        parameters.AddOptionalEnum("type", type);
        parameters.AddOptional("subAcct", subAccountName);
        parameters.AddOptional("ccy", currency);
        parameters.AddOptional("after", after?.ToOkxString());
        parameters.AddOptional("before", before?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxSubAccountBill>(GetUri(v5UsersSubaccountBills), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Only applicable to the trading team's master account to getting transfer records of managed sub accounts entrusted to oneself.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="type">0: Transfers from master account to sub-account ;1 : Transfers from sub-account to master account.</param>
    /// <param name="subAccountName">Sub-account name</param>
    /// <param name="subAccountId">Sub-account UID</param>
    /// <param name="after">Query the data prior to the requested bill ID creation time (exclude), Unix timestamp in millisecond format, e.g. 1597026383085</param>
    /// <param name="before">Query the data after the requested bill ID creation time (exclude), Unix timestamp in millisecond format, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100. The default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxSubAccountManagedBill>>> GetSubAccountManagedBillsAsync(
        string? currency = null,
        OkxSubAccountTransferType? type = null,
        string? subAccountName = null,
        long? subAccountId = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);

        var parameters = new ParameterCollection();
        parameters.AddOptionalEnum("type", type);
        parameters.AddOptional("ccy", currency);
        parameters.AddOptional("subAcct", subAccountName);
        parameters.AddOptional("subUid", subAccountId?.ToOkxString());
        parameters.AddOptional("after", after?.ToOkxString());
        parameters.AddOptional("before", before?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxSubAccountManagedBill>(GetUri(v5AssetSubaccountManagedSubaccountBills), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Applies to master accounts only
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="amount">Amount</param>
    /// <param name="fromAccount">6:Funding Account 18:Unified Account</param>
    /// <param name="toAccount">6:Funding Account 18:Unified Account</param>
    /// <param name="fromSubAccountName">Sub-account name of the account that transfers funds out.</param>
    /// <param name="toSubAccountName">Sub-account name of the account that transfers funds in.</param>
    /// <param name="loanTransfer">Whether or not borrowed coins can be transferred out under Multi-currency margin and Portfolio margin. the default is false</param>
    /// <param name="omitPositionRisk">Ignore position risk. Default is false. Applicable to Portfolio margin</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<long?>> TransferBetweenSubAccountsAsync(
        string currency,
        decimal amount,
        OkxAccount fromAccount,
        OkxAccount toAccount,
        string fromSubAccountName,
        string toSubAccountName,
        bool? loanTransfer = null,
        bool? omitPositionRisk = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            {"ccy", currency },
            {"amt", amount.ToOkxString() },
            {"fromSubAccount", fromSubAccountName },
            {"toSubAccount", toSubAccountName },
        };
        parameters.AddEnum("from", fromAccount);
        parameters.AddEnum("to", toAccount);
        parameters.AddOptional("loanTrans", loanTransfer);
        parameters.AddOptional("omitPosRisk", omitPositionRisk);

        var result = await ProcessOneRequestAsync<OkxSubAccountTransferIdContainer>(GetUri(v5UsersSubaccountTransfer), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
        if (!result) return new RestCallResult<long?>(result.Request, result.Response, result.Raw ?? "", result.Error);
        return new RestCallResult<long?>(result.Request, result.Response, result.Data.Payload, result.Raw ?? "", result.Error);
    }

    /// <summary>
    /// Set permission of transfer out for sub-account (only applicable to master account API key). Sub-account can transfer out to master account by default.
    /// </summary>
    /// <param name="subAccountName">Name of the sub-account. Single sub-account or multiple sub-account (no more than 20) separated with comma.</param>
    /// <param name="canTransferOut">Whether the sub-account has the right to transfer out. The default is true.
    /// false: cannot transfer out
    /// true: can transfer out</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxSubAccountPermissionOfTransferOut>>> SetPermissionOfTransferOutAsync(
        string subAccountName,
        bool canTransferOut,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("subAcct", subAccountName);
        parameters.AddOptional("canTransOut", canTransferOut);

        return ProcessListRequestAsync<OkxSubAccountPermissionOfTransferOut>(GetUri(v5UsersSubaccountSetTransferOut), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// The trading team uses this interface to view the list of sub-accounts currently under escrow
    /// </summary>
    /// <param name="subAccountName">Sub-account name</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxSubAccountName>>> GetCustodySubAccountsAsync(string subAccountName, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("subAcct", subAccountName);

        return ProcessListRequestAsync<OkxSubAccountName>(GetUri(v5UsersEntrustSubaccountList), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

}