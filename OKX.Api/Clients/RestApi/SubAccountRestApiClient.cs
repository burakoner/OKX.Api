namespace OKX.Api.Clients.RestApi;

public class SubAccountRestApiClient : RestApiClient
{
    // Endpoints
    protected const string Endpoints_V5_SubAccount_List = "api/v5/users/subaccount/list";
    protected const string Endpoints_V5_SubAccount_ResetApiKey = "api/v5/users/subaccount/modify-apikey";
    protected const string Endpoints_V5_SubAccount_TradingBalances = "api/v5/account/subaccount/balances";
    protected const string Endpoints_V5_SubAccount_FundingBalances = "api/v5/asset/subaccount/balances";
    protected const string Endpoints_V5_SubAccount_Bills = "api/v5/asset/subaccount/bills";
    protected const string Endpoints_V5_SubAccount_Transfer = "api/v5/asset/subaccount/transfer";

    // Internal
    internal Log Log { get => this.log; }
    internal TimeSyncState TimeSyncState = new("OKX RestApi");

    // Root Client
    internal OKXRestApiClient RootClient { get; }
    internal CultureInfo CI { get { return RootClient.CI; } }
    public new OKXRestApiClientOptions ClientOptions { get { return RootClient.ClientOptions; } }

    internal SubAccountRestApiClient(OKXRestApiClient root) : base("OKX RestApi", root.ClientOptions)
    {
        RootClient = root;

        RequestBodyFormat = RestRequestBodyFormat.Json;
        ArraySerialization = ArraySerialization.MultipleValues;

        Thread.CurrentThread.CurrentCulture = CI;
        Thread.CurrentThread.CurrentUICulture = CI;
    }

    #region Override Methods
    protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
        => new OkxAuthenticationProvider((OkxApiCredentials)credentials);

    protected override Error ParseErrorResponse(JToken error)
        => RootClient.ParseErrorResponse(error);

    protected override Task<RestCallResult<DateTime>> GetServerTimestampAsync()
        => RootClient.MarketData.GetServerTimeAsync();

    protected override TimeSyncInfo GetTimeSyncInfo()
        => new(log, ClientOptions.AutoTimestamp, ClientOptions.TimestampRecalculationInterval, TimeSyncState);

    protected override TimeSpan GetTimeOffset()
        => TimeSyncState.TimeOffset;
    #endregion

    #region Internal Methods
    /// <summary>
    /// Sets the API Credentials
    /// </summary>
    /// <param name="credentials">API Credentials Object</param>
    internal void SetApiCredentials(OkxApiCredentials credentials)
    {
        base.SetApiCredentials(credentials);
    }

    /// <summary>
    /// Sets the API Credentials
    /// </summary>
    /// <param name="apiKey">The api key</param>
    /// <param name="apiSecret">The api secret</param>
    /// <param name="passPhrase">The passphrase you specified when creating the API key</param>
    internal virtual void SetApiCredentials(string apiKey, string apiSecret, string passPhrase)
    {
        SetApiCredentials(new OkxApiCredentials(apiKey, apiSecret, passPhrase));
    }

    internal async Task<RestCallResult<T>> SendRawRequest<T>(Uri uri, HttpMethod method, CancellationToken cancellationToken, bool signed = false, Dictionary<string, object> queryParameters = null, Dictionary<string, object> bodyParameters = null, Dictionary<string, string> headerParameters = null, ArraySerialization? arraySerialization = null, JsonSerializer deserializer = null, bool ignoreRatelimit = false, int requestWeight = 1) where T : class
    {
        Thread.CurrentThread.CurrentCulture = CI;
        Thread.CurrentThread.CurrentUICulture = CI;
        return await SendRequestAsync<T>(uri, method, cancellationToken, signed, queryParameters, bodyParameters, headerParameters, arraySerialization, deserializer, ignoreRatelimit, requestWeight).ConfigureAwait(false);
    }

    internal async Task<RestCallResult<T>> SendOKXRequest<T>(Uri uri, HttpMethod method, CancellationToken cancellationToken, bool signed = false, Dictionary<string, object> queryParameters = null, Dictionary<string, object> bodyParameters = null, Dictionary<string, string> headerParameters = null, ArraySerialization? arraySerialization = null, JsonSerializer deserializer = null, bool ignoreRatelimit = false, int requestWeight = 1) where T : class
    {
        Thread.CurrentThread.CurrentCulture = CI;
        Thread.CurrentThread.CurrentUICulture = CI;
        var result = await SendRequestAsync<OkxRestApiResponse<T>>(uri, method, cancellationToken, signed, queryParameters, bodyParameters, headerParameters, arraySerialization, deserializer, ignoreRatelimit, requestWeight).ConfigureAwait(false);
        if (!result.Success) return new RestCallResult<T>(result.Request, result.Response, result.Error);
        if (result.Data == null) return new RestCallResult<T>(result.Request, result.Response, result.Error);
        if (result.Data.ErrorCode > 0) return new RestCallResult<T>(result.Request, result.Response, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage));

        return new RestCallResult<T>(result.Request, result.Response, result.Data.Data, result.Raw, result.Error);
    }

    internal async Task<RestCallResult<T>> SendOKXSingleRequest<T>(Uri uri, HttpMethod method, CancellationToken cancellationToken, bool signed = false, Dictionary<string, object> queryParameters = null, Dictionary<string, object> bodyParameters = null, Dictionary<string, string> headerParameters = null, ArraySerialization? arraySerialization = null, JsonSerializer deserializer = null, bool ignoreRatelimit = false, int requestWeight = 1) where T : class
    {
        Thread.CurrentThread.CurrentCulture = CI;
        Thread.CurrentThread.CurrentUICulture = CI;
        var result = await SendRequestAsync<OkxRestApiResponse<IEnumerable<T>>>(uri, method, cancellationToken, signed, queryParameters, bodyParameters, headerParameters, arraySerialization, deserializer, ignoreRatelimit, requestWeight).ConfigureAwait(false);
        if (!result.Success) return new RestCallResult<T>(result.Request, result.Response, result.Error);
        if (result.Data == null) return new RestCallResult<T>(result.Request, result.Response, result.Error);
        if (result.Data.ErrorCode > 0) return new RestCallResult<T>(result.Request, result.Response, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage));

        return new RestCallResult<T>(result.Request, result.Response, result.Data.Data.FirstOrDefault(), result.Raw, result.Error);
    }
    #endregion

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
    public virtual async Task<RestCallResult<IEnumerable<OkxSubAccount>>> GetSubAccountsAsync(
        bool? enable = null,
        string subAccountName = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        if (limit < 1 || limit > 100)
            throw new ArgumentException("Limit can be between 1-100.");

        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("enable", enable);
        parameters.AddOptionalParameter("subAcct", subAccountName);
        parameters.AddOptionalParameter("after", after?.ToString(OkxGlobals.OkxCultureInfo));
        parameters.AddOptionalParameter("before", before?.ToString(OkxGlobals.OkxCultureInfo));
        parameters.AddOptionalParameter("limit", limit.ToString(OkxGlobals.OkxCultureInfo));

        return await SendOKXRequest<IEnumerable<OkxSubAccount>>(RootClient.GetUri(Endpoints_V5_SubAccount_List), HttpMethod.Get, ct, signed: true, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// applies to master accounts only
    /// </summary>
    /// <param name="password">Funds password of the master account</param>
    /// <param name="subAccountName">Sub Account Name</param>
    /// <param name="apiKey">APIKey note</param>
    /// <param name="apiLabel">APIKey note</param>
    /// <param name="permission">APIKey access</param>
    /// <param name="ipAddresses">Link IP addresses, separate with commas if more than one. Support up to 20 IP addresses.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<OkxSubAccountApiKey>> ResetSubAccountApiKeyAsync(
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

        return await SendOKXSingleRequest<OkxSubAccountApiKey>(RootClient.GetUri(Endpoints_V5_SubAccount_ResetApiKey), HttpMethod.Post, ct, signed: true, bodyParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Query detailed balance info of Trading Account of a sub-account via the master account (applies to master accounts only)
    /// </summary>
    /// <param name="subAccountName">Sub Account Name</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<OkxSubAccountTradingBalance>> GetSubAccountTradingBalancesAsync(
        string subAccountName,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            {"subAcct", subAccountName },
        };

        return await SendOKXSingleRequest<OkxSubAccountTradingBalance>(RootClient.GetUri(Endpoints_V5_SubAccount_TradingBalances), HttpMethod.Get, ct, signed: true, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Get sub-account funding balance
    /// Query detailed balance info of Funding Account of a sub-account via the master account (applies to master accounts only)
    /// </summary>
    /// <param name="subAccountName">Sub Account Name</param>
    /// <param name="currency">Single currency or multiple currencies (no more than 20) separated with comma, e.g. BTC or BTC,ETH.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<OkxSubAccountFundingBalance>> GetSubAccountFundingBalancesAsync(
        string subAccountName,
        string currency = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            {"subAcct", subAccountName },
        };

        parameters.AddOptionalParameter("ccy", currency);

        return await SendOKXSingleRequest<OkxSubAccountFundingBalance>(RootClient.GetUri(Endpoints_V5_SubAccount_FundingBalances), HttpMethod.Get, ct, signed: true, queryParameters: parameters).ConfigureAwait(false);
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
    public virtual async Task<RestCallResult<IEnumerable<OkxSubAccountBill>>> GetSubAccountBillsAsync(
        string subAccountName = null,
        string currency = null,
        OkxSubAccountTransferType? type = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        if (limit < 1 || limit > 100)
            throw new ArgumentException("Limit can be between 1-100.");

        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("subAcct", subAccountName);
        parameters.AddOptionalParameter("ccy", currency);
        parameters.AddOptionalParameter("type", JsonConvert.SerializeObject(type, new SubAccountTransferTypeConverter(false)));
        parameters.AddOptionalParameter("after", after?.ToString(OkxGlobals.OkxCultureInfo));
        parameters.AddOptionalParameter("before", before?.ToString(OkxGlobals.OkxCultureInfo));
        parameters.AddOptionalParameter("limit", limit.ToString(OkxGlobals.OkxCultureInfo));

        return await SendOKXRequest<IEnumerable<OkxSubAccountBill>>(RootClient.GetUri(Endpoints_V5_SubAccount_Bills), HttpMethod.Get, ct, signed: true, queryParameters: parameters).ConfigureAwait(false);
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
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<OkxSubAccountTransfer>> TransferBetweenSubAccountsAsync(
        string currency,
        decimal amount,
        OkxAccount fromAccount,
        OkxAccount toAccount,
        string fromSubAccountName,
        string toSubAccountName,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            {"ccy", currency },
            {"amt", amount.ToString(OkxGlobals.OkxCultureInfo) },
            {"from", JsonConvert.SerializeObject(fromAccount, new AccountConverter(false)) },
            {"to", JsonConvert.SerializeObject(toAccount, new AccountConverter(false)) },
            {"fromSubAccount", fromSubAccountName },
            {"toSubAccount", toSubAccountName },
        };

        return await SendOKXSingleRequest<OkxSubAccountTransfer>(RootClient.GetUri(Endpoints_V5_SubAccount_Transfer), HttpMethod.Post, ct, signed: true, bodyParameters: parameters).ConfigureAwait(false);
    }

    // TODO: Set Permission Of Transfer Out
    // TODO: Get custody trading sub-account list
    #endregion
}