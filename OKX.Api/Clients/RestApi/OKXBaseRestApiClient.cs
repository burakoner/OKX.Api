namespace OKX.Api.Clients.RestApi;

public class OKXBaseRestApiClient : RestApiClient
{
    // Internal
    internal Log Log { get => this.log; }
    internal TimeSyncState TimeSyncState = new("OKX RestApi");
    internal CultureInfo CI = CultureInfo.InvariantCulture;

    // Root Client
    internal OKXRestApiClient RootClient { get; }
    internal new OKXRestApiClientOptions ClientOptions { get { return RootClient.ClientOptions; } }

    internal OKXBaseRestApiClient(OKXRestApiClient root) : base("OKX RestApi", root.ClientOptions)
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
    {
        if (!error.HasValues)
            return new ServerError(error.ToString());

        if ((error["msg"] == null && error["code"] == null) || (string.IsNullOrWhiteSpace((string)error["msg"])))
            return new ServerError(error.ToString());

        if (error["msg"] != null && error["code"] == null || (!string.IsNullOrWhiteSpace((string)error["msg"])))
            return new ServerError((string)error["message"]!);

        return new ServerError((int)error["code"], (string)error["message"]);
    }

    protected override Task<RestCallResult<DateTime>> GetServerTimestampAsync()
        => RootClient.PublicData.GetServerTimeAsync();

    protected override TimeSyncInfo GetTimeSyncInfo()
        => new(log, ClientOptions.AutoTimestamp, ClientOptions.TimestampRecalculationInterval, TimeSyncState);

    protected override TimeSpan GetTimeOffset()
        => TimeSyncState.TimeOffset;
    #endregion

    #region Internal Methods
    protected Uri GetUri(string endpoint, string param = "")
    {
        var x = endpoint.IndexOf('<');
        var y = endpoint.IndexOf('>');
        if (x > -1 && y > -1) endpoint = endpoint.Replace(endpoint.Substring(x, y - x + 1), param);

        return new Uri($"{ClientOptions.BaseAddress.TrimEnd('/')}/{endpoint}");
    }

    /// <summary>
    /// Sets the API Credentials
    /// </summary>
    /// <param name="credentials">API Credentials Object</param>
    protected void SetApiCredentials(OkxApiCredentials credentials)
    {
        base.SetApiCredentials(credentials);
    }

    /// <summary>
    /// Sets the API Credentials
    /// </summary>
    /// <param name="apiKey">The api key</param>
    /// <param name="apiSecret">The api secret</param>
    /// <param name="passPhrase">The passphrase you specified when creating the API key</param>
    protected virtual void SetApiCredentials(string apiKey, string apiSecret, string passPhrase)
    {
        SetApiCredentials(new OkxApiCredentials(apiKey, apiSecret, passPhrase));
    }

    protected async Task<RestCallResult<T>> SendRawRequest<T>(
        Uri uri, 
        HttpMethod method, 
        CancellationToken cancellationToken, 
        bool signed = false, 
        Dictionary<string, object> queryParameters = null, 
        Dictionary<string, object> bodyParameters = null, 
        Dictionary<string, string> headerParameters = null, 
        ArraySerialization? arraySerialization = null, 
        JsonSerializer deserializer = null, 
        bool ignoreRatelimit = false, 
        int requestWeight = 1) where T : class
    {
        Thread.CurrentThread.CurrentCulture = CI;
        Thread.CurrentThread.CurrentUICulture = CI;
        return await SendRequestAsync<T>(uri, method, cancellationToken, signed, queryParameters, bodyParameters, headerParameters, arraySerialization, deserializer, ignoreRatelimit, requestWeight).ConfigureAwait(false);
    }

    protected async Task<RestCallResult<T>> SendOKXRequest<T>(
        Uri uri, 
        HttpMethod method, 
        CancellationToken cancellationToken, 
        bool signed = false, 
        Dictionary<string, object> queryParameters = null, 
        Dictionary<string, object> bodyParameters = null,
        Dictionary<string, string> headerParameters = null, 
        ArraySerialization? arraySerialization = null, 
        JsonSerializer deserializer = null, 
        bool ignoreRatelimit = false, 
        int requestWeight = 1) where T : class
    {
        Thread.CurrentThread.CurrentCulture = CI;
        Thread.CurrentThread.CurrentUICulture = CI;
        var result = await SendRequestAsync<OkxRestApiResponse<T>>(uri, method, cancellationToken, signed, queryParameters, bodyParameters, headerParameters, arraySerialization, deserializer, ignoreRatelimit, requestWeight).ConfigureAwait(false);
        if (!result.Success) return new RestCallResult<T>(result.Request, result.Response, result.Error);
        if (result.Data == null) return new RestCallResult<T>(result.Request, result.Response, result.Error);
        // if (result.Data.ErrorCode > 0) return new RestCallResult<T>(result.Request, result.Response, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage));
        return new RestCallResult<T>(result.Request, result.Response, result.Data.Data, result.Raw, result.Error);
    }

    protected async Task<RestCallResult<T>> SendOKXSingleRequest<T>(
        Uri uri, 
        HttpMethod method, 
        CancellationToken cancellationToken, 
        bool signed = false, 
        Dictionary<string, object> queryParameters = null, 
        Dictionary<string, object> bodyParameters = null, 
        Dictionary<string, string> headerParameters = null, 
        ArraySerialization? arraySerialization = null, 
        JsonSerializer deserializer = null, 
        bool ignoreRatelimit = false, 
        int requestWeight = 1) where T : class
    {
        Thread.CurrentThread.CurrentCulture = CI;
        Thread.CurrentThread.CurrentUICulture = CI;
        var result = await SendRequestAsync<OkxRestApiResponse<IEnumerable<T>>>(uri, method, cancellationToken, signed, queryParameters, bodyParameters, headerParameters, arraySerialization, deserializer, ignoreRatelimit, requestWeight).ConfigureAwait(false);
        if (!result.Success) return new RestCallResult<T>(result.Request, result.Response, result.Error);
        if (result.Data == null) return new RestCallResult<T>(result.Request, result.Response, result.Error);
        // if (result.Data.ErrorCode > 0) return new RestCallResult<T>(result.Request, result.Response, new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage));
        return new RestCallResult<T>(result.Request, result.Response, result.Data.Data.FirstOrDefault(), result.Raw, result.Error);
    }
    #endregion

}