using OKX.Api.Authentication;
using OKX.Api.Common.Models;

namespace OKX.Api.Common.Clients;

/// <summary>
/// OKX Rest Api Base Client
/// </summary>
public abstract class OkxBaseRestClient : RestApiClient
{
    // Internal
    internal ILogger Logger { get => _logger; }
    internal TimeSyncState TimeSyncState = new("OKX RestApi");

    // Root Client
    internal OkxRestApiClient RootClient { get; }
    internal OkxRestApiOptions Options { get { return RootClient.Options; } }

    internal OkxBaseRestClient(OkxRestApiClient root) : base(root.Logger, root.Options)
    {
        RootClient = root;
        ManualParseError = true;

        RequestBodyFormat = RestRequestBodyFormat.Json;
        ArraySerialization = ArraySerialization.MultipleValues;

        Thread.CurrentThread.CurrentCulture = OkxConstants.OkxCultureInfo;
        Thread.CurrentThread.CurrentUICulture = OkxConstants.OkxCultureInfo;
    }

    #region Override Methods
    /// <inheritdoc />
    protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
        => new OkxAuthenticationProvider((OkxApiCredentials)credentials);

    /// <inheritdoc />
    protected override Error ParseErrorResponse(JToken error)
    {
        if (!error.HasValues)
            return new ServerError(error.ToString());

        if (error["msg"] is null && error["code"] is null || string.IsNullOrWhiteSpace((string)error["msg"]))
            return new ServerError(error.ToString());

        if (error["msg"] is not null && error["code"] is null || !string.IsNullOrWhiteSpace((string)error["msg"]))
            return new ServerError((string)error["msg"]!);

        return new ServerError((int)error["code"], (string)error["msg"]);
    }

    /// <inheritdoc />
    protected override Task<ServerError> TryParseErrorAsync(JToken error)
    {
        if (!error.HasValues)
            return Task.FromResult<ServerError>(null);

        if (error["msg"] is not null
            && !string.IsNullOrWhiteSpace((string)error["msg"])
            && error["code"] is not null)
            return Task.FromResult(new ServerError((int)error["code"], (string)error["msg"]));

        return Task.FromResult<ServerError>(null);
    }

    /// <inheritdoc />
    protected override Task<RestCallResult<DateTime>> GetServerTimestampAsync()
        => RootClient.Public.GetServerTimeAsync();

    /// <inheritdoc />
    protected override TimeSyncInfo GetTimeSyncInfo()
        => new(Logger, Options.AutoTimestamp, Options.AutoTimestampInterval, TimeSyncState);

    /// <inheritdoc />
    protected override TimeSpan GetTimeOffset()
        => TimeSyncState.TimeOffset;
    #endregion

    #region Internal Methods
    internal Uri GetUri(string endpoint, string param = "")
    {
        var x = endpoint.IndexOf('<');
        var y = endpoint.IndexOf('>');
        if (x > -1 && y > -1) endpoint = endpoint.Replace(endpoint.Substring(x, y - x + 1), param);

        return new Uri($"{Options.BaseAddress.TrimEnd('/')}/{endpoint}");
    }

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
    internal void SetApiCredentials(string apiKey, string apiSecret, string passPhrase)
    {
        SetApiCredentials(new OkxApiCredentials(apiKey, apiSecret, passPhrase));
    }

    internal async Task<RestCallResult<T>> SendRawRequestAsync<T>(
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

    internal async Task<RestCallResult<List<T>>> ProcessListRequestAsync<T>(
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

        // Return
        if (!result.Success) return new RestCallResult<List<T>>(result.Request, result.Response, result.Raw, result.Error);
        if (result.Data is null) return new RestCallResult<List<T>>(result.Request, result.Response, result.Raw, result.Error);
        return new RestCallResult<List<T>>(result.Request, result.Response, result.Data.Data, result.Raw, result.Error);
    }

    internal async Task<RestCallResult<T>> ProcessOneRequestAsync<T>(
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

        // Return
        if (!result.Success) return new RestCallResult<T>(result.Request, result.Response, result.Raw, result.Error);
        if (result.Data is null) return new RestCallResult<T>(result.Request, result.Response, result.Raw, result.Error);
        return new RestCallResult<T>(result.Request, result.Response, result.Data.Data.FirstOrDefault(), result.Raw, result.Error);
    }

    internal async Task<RestCallResult<T>> ProcessArrayModelRequestAsync<T>(
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

        // Return
        if (!result.Success) return new RestCallResult<T>(result.Request, result.Response, result.Raw, result.Error);
        if (result.Data is null) return new RestCallResult<T>(result.Request, result.Response, result.Raw, result.Error);
        return new RestCallResult<T>(result.Request, result.Response, result.Data.Data, result.Raw, result.Error);
    }

    internal async Task<RestCallResult<T>> ProcessModelRequestAsync<T>(
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

        // Return
        if (!result.Success) return new RestCallResult<T>(result.Request, result.Response, result.Raw, result.Error);
        if (result.Data is null) return new RestCallResult<T>(result.Request, result.Response, result.Raw, result.Error);
        return new RestCallResult<T>(result.Request, result.Response, result.Data.Data, result.Raw, result.Error);
    }

    #endregion

}