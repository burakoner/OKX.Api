using OKX.Api.Account.Converters;
using OKX.Api.Account.Enums;
using OKX.Api.SubAccount.Converters;
using OKX.Api.SubAccount.Enums;
using OKX.Api.SubAccount.Models;

namespace OKX.Api.SubAccount.Clients;

/// <summary>
/// OKX Rest Api Sub Account Client
/// </summary>
public class OkxSubAccountRestClient(OkxRestApiClient root) : OkxBaseRestClient(root)
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
    private const string v5UsersSubaccountSetTransferOut = "api/v5/users/subaccount/set-transfer-out";
    private const string v5UsersEntrustSubaccountList = "api/v5/users/entrust-subaccount-list";
    private const string v5AccountSubaccountSetLoanAllocation = "api/v5/account/subaccount/set-loan-allocation";
    private const string v5AccountSubaccountInterestLimits = "api/v5/account/subaccount/interest-limits";

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

    /// <summary>
    /// Retrieve the maximum withdrawal information of a sub-account via the master account (applies to master accounts only). If no currency is specified, the transferable amount of all owned currencies will be returned.
    /// </summary>
    /// <param name="subAccountName">Sub Account Name</param>
    /// <param name="currency">Single currency or multiple currencies (no more than 20) separated with comma, e.g. BTC or BTC,ETH.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
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
    public Task<RestCallResult<OkxSubAccountTransfer>> TransferBetweenSubAccountsAsync(
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
        var parameters = new Dictionary<string, object>
        {
            {"ccy", currency },
            {"amt", amount.ToOkxString() },
            {"from", JsonConvert.SerializeObject(fromAccount, new OkxAccountConverter(false)) },
            {"to", JsonConvert.SerializeObject(toAccount, new OkxAccountConverter(false)) },
            {"fromSubAccount", fromSubAccountName },
            {"toSubAccount", toSubAccountName },
        };
        parameters.AddOptionalParameter("loanTrans", loanTransfer);
        parameters.AddOptionalParameter("omitPosRisk", omitPositionRisk);

        return ProcessOneRequestAsync<OkxSubAccountTransfer>(GetUri(v5UsersSubaccountTransfer), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
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
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("subAcct", subAccountName);
        parameters.AddOptionalParameter("canTransOut", canTransferOut);

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
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("subAcct", subAccountName);

        return ProcessListRequestAsync<OkxSubAccountName>(GetUri(v5UsersEntrustSubaccountList), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }
    
    /// <summary>
    /// Set the VIP loan allocation of sub-accounts. Only Applicable to master account API keys with Trade access.
    /// </summary>
    /// <param name="enable">true or false</param>
    /// <param name="allocations">If enable = false, this will not be validated</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxBooleanResponse>> SetLoanAllocationAsync(
        bool enable,
        IEnumerable<OkxSubAccountLoanAllocation> allocations = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "enable", enable },
        };
        parameters.AddOptionalParameter("alloc", allocations);

        return ProcessOneRequestAsync<OkxBooleanResponse>(GetUri(v5AccountSubaccountSetLoanAllocation), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }
    
    /// <summary>
    /// Only applicable to master account API keys. Only return VIP loan information
    /// </summary>
    /// <param name="subAccountName">Name of the sub-account. Can only put 1 sub account.</param>
    /// <param name="currency">Loan currency, e.g. BTC</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxSubAccountInterestLimits>> GetInterestLimitsAsync(string subAccountName, string currency = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "subAcct", subAccountName },
        };
        parameters.AddOptionalParameter("ccy", currency);

        return ProcessOneRequestAsync<OkxSubAccountInterestLimits>(GetUri(v5AccountSubaccountInterestLimits), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }
    
}