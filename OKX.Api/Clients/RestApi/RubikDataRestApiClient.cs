namespace OKX.Api.Clients.RestApi;

public class RubikDataRestApiClient : RestApiClient
{
    // Rubik Endpoints
    protected const string Endpoints_V5_RubikStat_TradingDataSupportCoin = "api/v5/rubik/stat/trading-data/support-coin";
    protected const string Endpoints_V5_RubikStat_TakerVolume = "api/v5/rubik/stat/taker-volume";
    protected const string Endpoints_V5_RubikStat_MarginLoanRatio = "api/v5/rubik/stat/margin/loan-ratio";
    protected const string Endpoints_V5_RubikStat_ContractsLongShortAccountRatio = "api/v5/rubik/stat/contracts/long-short-account-ratio";
    protected const string Endpoints_V5_RubikStat_ContractsOpenInterestVolume = "api/v5/rubik/stat/contracts/open-interest-volume";
    protected const string Endpoints_V5_RubikStat_OptionOpenInterestVolume = "api/v5/rubik/stat/option/open-interest-volume";
    protected const string Endpoints_V5_RubikStat_OptionOpenInterestVolumeRatio = "api/v5/rubik/stat/option/open-interest-volume-ratio";
    protected const string Endpoints_V5_RubikStat_OptionOpenInterestVolumeExpiry = "api/v5/rubik/stat/option/open-interest-volume-expiry";
    protected const string Endpoints_V5_RubikStat_OptionOpenInterestVolumeStrike = "api/v5/rubik/stat/option/open-interest-volume-strike";
    protected const string Endpoints_V5_RubikStat_OptionTakerBlockVolume = "api/v5/rubik/stat/option/taker-block-volume";

    // Internal
    internal Log Log { get => this.log; }
    internal TimeSyncState TimeSyncState = new("OKX RestApi");

    // Root Client
    internal OKXRestApiClient RootClient { get; }
    internal CultureInfo CI { get { return RootClient.CI; } }
    public new OKXRestApiClientOptions ClientOptions { get { return RootClient.ClientOptions; } }

    internal RubikDataRestApiClient(OKXRestApiClient root) : base("OKX RestApi", root.ClientOptions)
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

    #region Trading API Endpoints
    /// <summary>
    /// Get the currency supported by the transaction big data interface
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<OkxSupportCoins>> GetRubikSupportCoinAsync(CancellationToken ct = default)
    {
        return await SendOKXRequest<OkxSupportCoins>(RootClient.GetUri(Endpoints_V5_RubikStat_TradingDataSupportCoin), HttpMethod.Get, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// This is the taker volume for both buyers and sellers. This shows the influx and exit of funds in and out of {coin}.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="period">period, the default is 5m, e.g. [5m/1H/1D]</param>
    /// <param name="begin">begin, e.g. 1597026383085</param>
    /// <param name="end">end, e.g. 1597026383011</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<OkxTakerVolume>>> GetRubikTakerVolumeAsync(
        string currency,
        OkxInstrumentType instrumentType,
        OkxPeriod period = OkxPeriod.FiveMinutes,
        long? begin = null,
        long? end = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "ccy", currency},
            { "instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)) },
            { "period", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
        };
        parameters.AddOptionalParameter("begin", begin?.ToString(OkxGlobals.OkxCultureInfo));
        parameters.AddOptionalParameter("end", end?.ToString(OkxGlobals.OkxCultureInfo));

        return await SendOKXRequest<IEnumerable<OkxTakerVolume>>(RootClient.GetUri(Endpoints_V5_RubikStat_TakerVolume), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// This indicator shows the ratio of cumulative data value between currency pair leverage quote currency and underlying asset over a given period of time.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="period">period, the default is 5m, e.g. [5m/1H/1D]</param>
    /// <param name="begin">begin, e.g. 1597026383085</param>
    /// <param name="end">end, e.g. 1597026383085</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<OkxRatio>>> GetRubikMarginLendingRatioAsync(
        string currency,
        OkxPeriod period = OkxPeriod.FiveMinutes,
        long? begin = null,
        long? end = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "ccy", currency},
            { "period", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
        };
        parameters.AddOptionalParameter("begin", begin?.ToString(OkxGlobals.OkxCultureInfo));
        parameters.AddOptionalParameter("end", end?.ToString(OkxGlobals.OkxCultureInfo));

        return await SendOKXRequest<IEnumerable<OkxRatio>>(RootClient.GetUri(Endpoints_V5_RubikStat_MarginLoanRatio), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// This is the ratio of users with net long vs short positions. It includes data from futures and perpetual swaps.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="period">period, the default is 5m, e.g. [5m/1H/1D]</param>
    /// <param name="begin">begin, e.g. 1597026383085</param>
    /// <param name="end">end, e.g. 1597026383011</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<OkxRatio>>> GetRubikLongShortRatioAsync(
        string currency,
        OkxPeriod period = OkxPeriod.FiveMinutes,
        long? begin = null,
        long? end = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "ccy", currency},
            { "period", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
        };
        parameters.AddOptionalParameter("begin", begin?.ToString(OkxGlobals.OkxCultureInfo));
        parameters.AddOptionalParameter("end", end?.ToString(OkxGlobals.OkxCultureInfo));

        return await SendOKXRequest<IEnumerable<OkxRatio>>(RootClient.GetUri(Endpoints_V5_RubikStat_ContractsLongShortAccountRatio), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Open interest is the sum of all long and short futures and perpetual swap positions.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="period">period, the default is 5m, e.g. [5m/1H/1D]</param>
    /// <param name="begin">begin, e.g. 1597026383085</param>
    /// <param name="end">end, e.g. 1597026383011</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<OkxInterestVolume>>> GetRubikContractSummaryAsync(
        string currency,
        OkxPeriod period = OkxPeriod.FiveMinutes,
        long? begin = null,
        long? end = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "ccy", currency},
            { "period", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
        };
        parameters.AddOptionalParameter("begin", begin?.ToString(OkxGlobals.OkxCultureInfo));
        parameters.AddOptionalParameter("end", end?.ToString(OkxGlobals.OkxCultureInfo));

        return await SendOKXRequest<IEnumerable<OkxInterestVolume>>(RootClient.GetUri(Endpoints_V5_RubikStat_ContractsOpenInterestVolume), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// This shows the sum of all open positions and how much total trading volume has taken place.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="period">period, the default is 8H. e.g. [8H/1D]</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<OkxInterestVolume>>> GetRubikOptionsSummaryAsync(
        string currency,
        OkxPeriod period = OkxPeriod.FiveMinutes,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "ccy", currency},
            { "period", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
        };

        return await SendOKXRequest<IEnumerable<OkxInterestVolume>>(RootClient.GetUri(Endpoints_V5_RubikStat_OptionOpenInterestVolume), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// This shows the relative buy/sell volume for calls and puts. It shows whether traders are bullish or bearish on price and volatility.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="period">period, the default is 8H. e.g. [8H/1D]</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<OkxPutCallRatio>>> GetRubikPutCallRatioAsync(
        string currency,
        OkxPeriod period = OkxPeriod.FiveMinutes,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "ccy", currency},
            { "period", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
        };

        return await SendOKXRequest<IEnumerable<OkxPutCallRatio>>(RootClient.GetUri(Endpoints_V5_RubikStat_OptionOpenInterestVolumeRatio), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// This shows the volume and open interest for each upcoming expiration. You can use this to see which expirations are currently the most popular to trade.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="period">period, the default is 8H. e.g. [8H/1D]</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<OkxInterestVolumeExpiry>>> GetRubikInterestVolumeExpiryAsync(
        string currency,
        OkxPeriod period = OkxPeriod.FiveMinutes,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "ccy", currency},
            { "period", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
        };

        return await SendOKXRequest<IEnumerable<OkxInterestVolumeExpiry>>(RootClient.GetUri(Endpoints_V5_RubikStat_OptionOpenInterestVolumeExpiry), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// This shows what option strikes are the most popular for each expiration.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="expiryTime">expiry time (Format: YYYYMMdd, for example: "20210623")</param>
    /// <param name="period">period, the default is 8H. e.g. [8H/1D]</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<OkxInterestVolumeStrike>>> GetRubikInterestVolumeStrikeAsync(
        string currency,
        string expiryTime,
        OkxPeriod period = OkxPeriod.FiveMinutes,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "ccy", currency},
            { "expTime", expiryTime},
            { "period", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
        };

        return await SendOKXRequest<IEnumerable<OkxInterestVolumeStrike>>(RootClient.GetUri(Endpoints_V5_RubikStat_OptionOpenInterestVolumeStrike), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// This shows the relative buy/sell volume for calls and puts. It shows whether traders are bullish or bearish on price and volatility.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="period">period, the default is 8H. e.g. [8H/1D]</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<OkxTakerFlow>> GetRubikTakerFlowAsync(
        string currency,
        OkxPeriod period = OkxPeriod.FiveMinutes,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "ccy", currency},
            { "period", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
        };

        return await SendOKXRequest<OkxTakerFlow>(RootClient.GetUri(Endpoints_V5_RubikStat_OptionTakerBlockVolume), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion
}