namespace OKX.Api.Base;

/// <summary>
/// OKX Rest Api Base Client
/// </summary>
public abstract class OkxBaseRestClient : RestApiClient
{
    internal ILogger Logger { get => _logger; }
    internal OkxRestApiClient _ { get; }
    internal OkxRestApiOptions Options { get { return _.Options; } }
    internal TimeSyncState TimeSyncState { get; } = new("OKX RestApi");

    internal OkxBaseRestClient(OkxRestApiClient root) : base(root.Logger, root.Options)
    {
        _ = root;
        ManualParseError = true;

        RequestBodyFormat = RestRequestBodyFormat.Json;
        ArraySerialization = ArraySerialization.MultipleValues;

        Thread.CurrentThread.CurrentCulture = OkxConstants.OkxCultureInfo;
        Thread.CurrentThread.CurrentUICulture = OkxConstants.OkxCultureInfo;
    }

    #region Override Methods
    /// <inheritdoc />
    protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials) => new OkxAuthenticationProvider((OkxApiCredentials)credentials);
    /// <inheritdoc />
    protected override Error ParseErrorResponse(JToken message)
    {
        if (!message.HasValues)
            return new ServerError(message.ToString());

        if (message["msg"] is null || message["code"] is null)
            return new ServerError(message.ToString());

        return new ServerError((int)message["code"]!, (string)message["msg"]!);
    }
    /// <inheritdoc />
    protected override async Task<ServerError?> TryParseErrorAsync(JToken message)
    {
        await Task.CompletedTask;
        if (!message.HasValues) return null;

        if (message["data"] is not null && message["data"]!.Type == JTokenType.Array)
        {
            var data = message["data"]!.FirstOrDefault();
            if (data is not null && data["sCode"] != null && data["sMsg"] != null)
            {
                if (data["sCode"]!.Type == JTokenType.String && data["sMsg"]!.Type == JTokenType.String)
                {
                    if (int.TryParse((string)data["sCode"]!, out int code) && code != 0 && !string.IsNullOrWhiteSpace((string)data["sMsg"]!))
                        return new ServerError(code, (string)data["sMsg"]!);
                }
                else if (data["sCode"]!.Type == JTokenType.Integer && ((int)data["sCode"]!) != 0 && data["sMsg"]!.Type == JTokenType.String)
                {
                    if (!string.IsNullOrWhiteSpace((string)data["sMsg"]!))
                        return new ServerError((int)data["sCode"]!, (string)data["sMsg"]!);
                }
            }
        }

        if (message["msg"] is not null
            && !string.IsNullOrWhiteSpace((string)message["msg"]!)
            && message["code"] is not null)
            return new ServerError((int)message["code"]!, (string)message["msg"]!);

        return null;
    }
    /// <inheritdoc />
    protected override Task<RestCallResult<DateTime>> GetServerTimestampAsync() => _.Public.GetServerTimeAsync();
    /// <inheritdoc />
    protected override TimeSyncInfo GetTimeSyncInfo() => new(Logger, Options.AutoTimestamp, Options.AutoTimestampInterval, TimeSyncState);
    /// <inheritdoc />
    protected override TimeSpan GetTimeOffset() => TimeSyncState.TimeOffset;
    #endregion

    #region Internal Methods
    internal Uri GetUri(string endpoint, string param = "")
    {
        var x = endpoint.IndexOf('<');
        var y = endpoint.IndexOf('>');
        if (x > -1 && y > -1) endpoint = endpoint.Replace(endpoint.Substring(x, y - x + 1), param);

        return new Uri($"{Options.BaseAddress.TrimEnd('/')}/{endpoint}");
    }
    internal void SetApiCredentials(OkxApiCredentials credentials) => base.SetApiCredentials(credentials);
    internal void SetApiCredentials(string apiKey, string apiSecret, string passPhrase) => SetApiCredentials(new OkxApiCredentials(apiKey, apiSecret, passPhrase));

    internal async Task<RestCallResult<T>> SendRawRequestAsync<T>(Uri uri, HttpMethod method, CancellationToken cancellationToken, bool signed = false, Dictionary<string, object>? queryParameters = null, Dictionary<string, object>? bodyParameters = null, Dictionary<string, string>? headerParameters = null, ArraySerialization? arraySerialization = null, JsonSerializer? deserializer = null, bool ignoreRatelimit = false, int requestWeight = 1) where T : class
    {
        // Pre-Actions
        var culture = Thread.CurrentThread.CurrentCulture;
        var cultureUI = Thread.CurrentThread.CurrentUICulture;
        Thread.CurrentThread.CurrentCulture = OkxConstants.OkxCultureInfo;
        Thread.CurrentThread.CurrentUICulture = OkxConstants.OkxCultureInfo;

        // Main Action
        var result = await SendRequestAsync<T>(uri, method, cancellationToken, signed, queryParameters, bodyParameters, headerParameters, arraySerialization, deserializer, ignoreRatelimit, requestWeight).ConfigureAwait(false);

        // Post-Actions
        Thread.CurrentThread.CurrentCulture = culture;
        Thread.CurrentThread.CurrentUICulture = cultureUI;

        // Return
        return result;
    }
    internal async Task<RestCallResult<List<T>>> ProcessListRequestAsync<T>(Uri uri, HttpMethod method, CancellationToken cancellationToken, bool signed = false, Dictionary<string, object>? queryParameters = null, Dictionary<string, object>? bodyParameters = null, Dictionary<string, string>? headerParameters = null, ArraySerialization? arraySerialization = null, JsonSerializer? deserializer = null, bool ignoreRatelimit = false, int requestWeight = 1) where T : class
    {
        // Pre-Actions
        var culture = Thread.CurrentThread.CurrentCulture;
        var cultureUI = Thread.CurrentThread.CurrentUICulture;
        Thread.CurrentThread.CurrentCulture = OkxConstants.OkxCultureInfo;
        Thread.CurrentThread.CurrentUICulture = OkxConstants.OkxCultureInfo;

        // Main Action
        var result = await SendRequestAsync<OkxRestApiResponse<List<T>>>(uri, method, cancellationToken, signed, queryParameters, bodyParameters, headerParameters, arraySerialization, deserializer, ignoreRatelimit, requestWeight).ConfigureAwait(false);

        // Post-Actions
        Thread.CurrentThread.CurrentCulture = culture;
        Thread.CurrentThread.CurrentUICulture = cultureUI;

        // Return Error
        if (!result.Success) return new RestCallResult<List<T>>(result.Request, result.Response, result.Raw ?? "", result.Error);
        if (result.Data is null) return new RestCallResult<List<T>>(result.Request, result.Response, result.Raw ?? "", result.Error);

        // Return Success
        return new RestCallResult<List<T>>(result.Request, result.Response, result.Data.Data!, result.Raw ?? "", result.Error);
    }
    internal async Task<RestCallResult<T>> ProcessOneRequestAsync<T>(Uri uri, HttpMethod method, CancellationToken cancellationToken, bool signed = false, Dictionary<string, object>? queryParameters = null, Dictionary<string, object>? bodyParameters = null, Dictionary<string, string>? headerParameters = null, ArraySerialization? arraySerialization = null, JsonSerializer? deserializer = null, bool ignoreRatelimit = false, int requestWeight = 1) where T : class
    {
        // Pre-Actions
        var culture = Thread.CurrentThread.CurrentCulture;
        var cultureUI = Thread.CurrentThread.CurrentUICulture;
        Thread.CurrentThread.CurrentCulture = OkxConstants.OkxCultureInfo;
        Thread.CurrentThread.CurrentUICulture = OkxConstants.OkxCultureInfo;

        // Main Action
        var result = await SendRequestAsync<OkxRestApiResponse<List<T>>>(uri, method, cancellationToken, signed, queryParameters, bodyParameters, headerParameters, arraySerialization, deserializer, ignoreRatelimit, requestWeight).ConfigureAwait(false);

        // Post-Actions
        Thread.CurrentThread.CurrentCulture = culture;
        Thread.CurrentThread.CurrentUICulture = cultureUI;

        // Return Error
        if (!result.Success) return new RestCallResult<T>(result.Request, result.Response, result.Raw ?? "", result.Error);
        if (result.Data is null) return new RestCallResult<T>(result.Request, result.Response, result.Raw ?? "", result.Error);
        if (result.Data is OkxRestApiErrorBase data && data.ErrorCode is not null && data.ErrorCode.Trim() != "" && data.ErrorCode.Trim() != "0" && !string.IsNullOrEmpty(data.ErrorMessage))
            return new RestCallResult<T>(result.Request, result.Response, result.Raw ?? "", new ServerError(int.Parse(data.ErrorCode), data.ErrorMessage!));

        // Return Success
        return new RestCallResult<T>(result.Request, result.Response, result.Data.Data!.FirstOrDefault()!, result.Raw ?? "", result.Error);
    }
    internal async Task<RestCallResult<T>> ProcessArrayModelRequestAsync<T>(Uri uri, HttpMethod method, CancellationToken cancellationToken, bool signed = false, Dictionary<string, object>? queryParameters = null, Dictionary<string, object>? bodyParameters = null, Dictionary<string, string>? headerParameters = null, ArraySerialization? arraySerialization = null, JsonSerializer? deserializer = null, bool ignoreRatelimit = false, int requestWeight = 1) where T : class
    {
        // Pre-Actions
        var culture = Thread.CurrentThread.CurrentCulture;
        var cultureUI = Thread.CurrentThread.CurrentUICulture;
        Thread.CurrentThread.CurrentCulture = OkxConstants.OkxCultureInfo;
        Thread.CurrentThread.CurrentUICulture = OkxConstants.OkxCultureInfo;

        // Main Action
        var result = await SendRequestAsync<OkxRestApiResponse<T>>(uri, method, cancellationToken, signed, queryParameters, bodyParameters, headerParameters, arraySerialization, deserializer, ignoreRatelimit, requestWeight).ConfigureAwait(false);

        // Post-Actions
        Thread.CurrentThread.CurrentCulture = culture;
        Thread.CurrentThread.CurrentUICulture = cultureUI;

        // Return Error
        if (!result.Success) return new RestCallResult<T>(result.Request, result.Response, result.Raw ?? "", result.Error);
        if (result.Data is null) return new RestCallResult<T>(result.Request, result.Response, result.Raw ?? "", result.Error);

        // Return Success
        return new RestCallResult<T>(result.Request, result.Response, result.Data.Data!, result.Raw ?? "", result.Error);
    }
    internal async Task<RestCallResult<T>> ProcessModelRequestAsync<T>(Uri uri, HttpMethod method, CancellationToken cancellationToken, bool signed = false, Dictionary<string, object>? queryParameters = null, Dictionary<string, object>? bodyParameters = null, Dictionary<string, string>? headerParameters = null, ArraySerialization? arraySerialization = null, JsonSerializer? deserializer = null, bool ignoreRatelimit = false, int requestWeight = 1) where T : class
    {
        // Pre-Actions
        var culture = Thread.CurrentThread.CurrentCulture;
        var cultureUI = Thread.CurrentThread.CurrentUICulture;
        Thread.CurrentThread.CurrentCulture = OkxConstants.OkxCultureInfo;
        Thread.CurrentThread.CurrentUICulture = OkxConstants.OkxCultureInfo;

        // Main Action
        var result = await SendRequestAsync<OkxRestApiResponse<T>>(uri, method, cancellationToken, signed, queryParameters, bodyParameters, headerParameters, arraySerialization, deserializer, ignoreRatelimit, requestWeight).ConfigureAwait(false);

        // Post-Actions
        Thread.CurrentThread.CurrentCulture = culture;
        Thread.CurrentThread.CurrentUICulture = cultureUI;

        // Return Error
        if (!result.Success) return new RestCallResult<T>(result.Request, result.Response, result.Raw ?? "", result.Error);
        if (result.Data is null) return new RestCallResult<T>(result.Request, result.Response, result.Raw ?? "", result.Error);

        // Return Success
        return new RestCallResult<T>(result.Request, result.Response, result.Data.Data!, result.Raw ?? "", result.Error);
    }
    #endregion

}