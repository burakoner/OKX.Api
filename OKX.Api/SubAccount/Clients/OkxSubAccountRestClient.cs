using OKX.Api.Common.Clients.RestApi;
using OKX.Api.SubAccount.Converters;
using OKX.Api.SubAccount.Enums;
using OKX.Api.SubAccount.Models;

namespace OKX.Api.SubAccount.Clients;

/// <summary>
/// OKX Rest Api Sub Account Client
/// </summary>
public class OkxSubAccountRestClient(OkxRestApiClient root) : OkxRestApiBaseClient(root)
{
    // Endpoints
    private const string v5UsersSubaccountList = "api/v5/users/subaccount/list";
    private const string v5UsersSubaccountResetApiKey = "api/v5/users/subaccount/modify-apikey";
    private const string v5UsersSubaccountTradingBalances = "api/v5/account/subaccount/balances";
    private const string v5UsersSubaccountFundingBalances = "api/v5/asset/subaccount/balances";
    private const string v5AccountSubaccountMaxWithdrawal = "api/v5/account/subaccount/max-withdrawal";
    private const string v5UsersSubaccountBills = "api/v5/asset/subaccount/bills";
    private const string v5AssetSubaccountManagedSubaccountBills = "api/v5/asset/subaccount/managed-subaccount-bills";
    private const string v5UsersSubaccountTransfer = "api/v5/asset/subaccount/transfer";
    // TODO: api/v5/users/subaccount/set-transfer-out
    // TODO: api/v5/users/entrust-subaccount-list
    // TODO: api/v5/account/subaccount/set-loan-allocation
    // TODO: api/v5/account/subaccount/interest-limits

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
    public Task<RestCallResult<List<OkxSubAccount>>> GetSubAccountsAsync(
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
        var parameters = new Dictionary<string, object>
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
        string currency = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "subAcct", subAccountName },
        };

        parameters.AddOptionalParameter("ccy", currency);

        return ProcessOneRequestAsync<OkxSubAccountFundingBalance>(GetUri(v5UsersSubaccountFundingBalances), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    public Task<RestCallResult<List<OkxSubAccountMaximumWithdrawal>>> GetSubAccountMaximumWithdrawalsAsync(
    string subAccountName,
    string currency = null,
    CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "subAcct", subAccountName },
        };

        parameters.AddOptionalParameter("ccy", currency);

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
        parameters.AddOptionalParameter("type", JsonConvert.SerializeObject(type, new OkxSubAccountTransferTypeConverter(false)));
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxSubAccountBill>(GetUri(v5UsersSubaccountBills), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    public Task<RestCallResult<List<OkxManagedSubAccountBill>>> GetManagedSubAccountBillsAsync(
    string currency = null,
    OkxSubAccountTransferType? type = null,
    string subAccountName = null,
    long? subAccountId = null,
    long? after = null,
    long? before = null,
    int limit = 100,
    CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("ccy", currency);
        parameters.AddOptionalParameter("type", JsonConvert.SerializeObject(type, new OkxSubAccountTransferTypeConverter(false)));
        parameters.AddOptionalParameter("subAcct", subAccountName);
        parameters.AddOptionalParameter("subUid", subAccountId?.ToOkxString());
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxManagedSubAccountBill>(GetUri(v5AssetSubaccountManagedSubaccountBills), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

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
    public Task<RestCallResult<OkxSubAccountTransfer>> TransferBetweenSubAccountsAsync(
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
            {"from", JsonConvert.SerializeObject(fromAccount, new OkxAccountConverter(false)) },
            {"to", JsonConvert.SerializeObject(toAccount, new OkxAccountConverter(false)) },
            {"fromSubAccount", fromSubAccountName },
            {"toSubAccount", toSubAccountName },
        };
        parameters.AddOptionalParameter("loanTrans", loanTrans);
        parameters.AddOptionalParameter("omitPosRisk", omitPosRisk);

        return ProcessOneRequestAsync<OkxSubAccountTransfer>(GetUri(v5UsersSubaccountTransfer), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }
    #endregion

}