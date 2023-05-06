namespace OKX.Api.Clients.RestApi;

public class OKXMarketDataRestApiClient : OKXBaseRestApiClient
{
    // Public Data Endpoints
    protected const string v5PublicInstrumentsEndpoint = "api/v5/public/instruments";
    protected const string v5PublicDeliveryExerciseHistoryEndpoint = "api/v5/public/delivery-exercise-history";
    protected const string v5PublicOpenInterestEndpoint = "api/v5/public/open-interest";
    protected const string v5PublicFundingRateEndpoint = "api/v5/public/funding-rate";
    protected const string v5PublicFundingRateHistoryEndpoint = "api/v5/public/funding-rate-history";
    protected const string v5PublicPriceLimitEndpoint = "api/v5/public/price-limit";
    protected const string v5PublicOptionSummaryEndpoint = "api/v5/public/opt-summary";
    protected const string v5PublicEstimatedPriceEndpoint = "api/v5/public/estimated-price";
    protected const string v5PublicDiscountRateInterestFreeQuotaEndpoint = "api/v5/public/discount-rate-interest-free-quota";
    protected const string v5PublicTimeEndpoint = "api/v5/public/time";
    protected const string v5PublicLiquidationOrdersEndpoint = "api/v5/public/liquidation-orders";
    protected const string v5PublicMarkPriceEndpoint = "api/v5/public/mark-price";
    protected const string v5PublicPositionTiersEndpoint = "api/v5/public/position-tiers";
    protected const string v5PublicInterestRateLoanQuotaEndpoint = "api/v5/public/interest-rate-loan-quota";
    protected const string v5PublicVIPInterestRateLoanQuotaEndpoint = "api/v5/public/vip-interest-rate-loan-quota";
    protected const string v5PublicUnderlyingEndpoint = "api/v5/public/underlying";
    protected const string v5PublicInsuranceFundEndpoint = "api/v5/public/insurance-fund";
    protected const string v5PublicConvertContractCoinEndpoint = "api/v5/public/convert-contract-coin";

    // Market Data Endpoints
    protected const string Endpoints_V5_Market_Tickers = "api/v5/market/tickers";
    protected const string Endpoints_V5_Market_Ticker = "api/v5/market/ticker";
    protected const string Endpoints_V5_Market_IndexTickers = "api/v5/market/index-tickers";
    protected const string Endpoints_V5_Market_Books = "api/v5/market/books";
    protected const string Endpoints_V5_Market_Candles = "api/v5/market/candles";
    protected const string Endpoints_V5_Market_HistoryCandles = "api/v5/market/history-candles";
    protected const string Endpoints_V5_Market_IndexCandles = "api/v5/market/index-candles";
    protected const string Endpoints_V5_Market_MarkPriceCandles = "api/v5/market/mark-price-candles";
    protected const string Endpoints_V5_Market_Trades = "api/v5/market/trades";
    protected const string Endpoints_V5_Market_TradesHistory = "api/v5/market/history-trades";
    protected const string Endpoints_V5_Market_Platform24Volume = "api/v5/market/platform-24-volume";
    protected const string Endpoints_V5_Market_OpenOracle = "api/v5/market/open-oracle";
    protected const string Endpoints_V5_Market_ExchangeRate = "api/v5/market/exchange-rate";
    protected const string Endpoints_V5_Market_IndexComponents = "api/v5/market/index-components";
    protected const string Endpoints_V5_Market_BlockTickers = "api/v5/market/block-tickers";
    protected const string Endpoints_V5_Market_BlockTicker = "api/v5/market/block-ticker";
    protected const string Endpoints_V5_Market_BlockTrades = "api/v5/market/block-trades";

    internal OKXMarketDataRestApiClient(OKXRestApiClient root) : base(root)
    {
    }

    #region Public Data Methods
    /// <summary>
    /// Retrieve a list of instruments with open contracts.
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="instrumentFamily">Instrument family. Only applicable to FUTURES/SWAP/OPTION.If instType is OPTION, either uly or instFamily is required.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<OkxInstrument>>> GetInstrumentsAsync(OkxInstrumentType instrumentType, string underlying = null, string instrumentId = null, string instrumentFamily = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)) },
        };
        parameters.AddOptionalParameter("uly", underlying);
        parameters.AddOptionalParameter("instId", instrumentId);
        parameters.AddOptionalParameter("instFamily", instrumentFamily);

        return await SendOKXRequest<IEnumerable<OkxInstrument>>(RootClient.GetUri(v5PublicInstrumentsEndpoint), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve the estimated delivery price, which will only have a return value one hour before the delivery/exercise.
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<OkxDeliveryExerciseHistory>>> GetDeliveryExerciseHistoryAsync(OkxInstrumentType instrumentType, string underlying, long? after = null, long? before = null, int limit = 100, CancellationToken ct = default)
    {
        if (instrumentType.IsNotIn(OkxInstrumentType.Futures, OkxInstrumentType.Option))
            throw new ArgumentException("Instrument Type can be only Futures or Option.");

        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>
        {
            { "instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)) },
            { "uly", underlying },
        };
        parameters.AddOptionalParameter("after", after?.ToString());
        parameters.AddOptionalParameter("before", before?.ToString());
        parameters.AddOptionalParameter("limit", limit.ToString());

        return await SendOKXRequest<IEnumerable<OkxDeliveryExerciseHistory>>(RootClient.GetUri(v5PublicDeliveryExerciseHistoryEndpoint), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve the total open interest for contracts on OKX
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<OkxOpenInterest>>> GetOpenInterestsAsync(OkxInstrumentType instrumentType, string underlying = null, string instrumentId = null, CancellationToken ct = default)
    {
        if (instrumentType.IsNotIn(OkxInstrumentType.Futures, OkxInstrumentType.Option, OkxInstrumentType.Swap))
            throw new ArgumentException("Instrument Type can be only Futures, Option or Swap.");

        if (instrumentType == OkxInstrumentType.Swap && string.IsNullOrEmpty(underlying))
            throw new ArgumentException("Underlying is required for Option.");

        var parameters = new Dictionary<string, object>
        {
            { "instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)) },
        };
        parameters.AddOptionalParameter("uly", underlying);
        parameters.AddOptionalParameter("instId", instrumentId);

        return await SendOKXRequest<IEnumerable<OkxOpenInterest>>(RootClient.GetUri(v5PublicOpenInterestEndpoint), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve funding rate.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<OkxFundingRate>>> GetFundingRatesAsync(string instrumentId, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "instId", instrumentId },
        };

        return await SendOKXRequest<IEnumerable<OkxFundingRate>>(RootClient.GetUri(v5PublicFundingRateEndpoint), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve funding rate history. This endpoint can retrieve data from the last 3 months.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<OkxFundingRateHistory>>> GetFundingRateHistoryAsync(string instrumentId, long? after = null, long? before = null, int limit = 100, CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>
        {
            { "instId", instrumentId },
        };
        parameters.AddOptionalParameter("after", after?.ToString());
        parameters.AddOptionalParameter("before", before?.ToString());
        parameters.AddOptionalParameter("limit", limit.ToString());

        return await SendOKXRequest<IEnumerable<OkxFundingRateHistory>>(RootClient.GetUri(v5PublicFundingRateHistoryEndpoint), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve the highest buy limit and lowest sell limit of the instrument.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<OkxLimitPrice>> GetLimitPriceAsync(string instrumentId, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "instId", instrumentId },
        };

        return await SendOKXSingleRequest<OkxLimitPrice>(RootClient.GetUri(v5PublicPriceLimitEndpoint), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve option market data.
    /// </summary>
    /// <param name="underlying">Underlying</param>
    /// <param name="expiryDate">Contract expiry date, the format is "YYMMDD", e.g. "200527"</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<OkxOptionSummary>>> GetOptionMarketDataAsync(string underlying, DateTime? expiryDate = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "uly", underlying },
        };
        parameters.AddOptionalParameter("expTime", expiryDate?.ToString("yyMMdd"));

        return await SendOKXRequest<IEnumerable<OkxOptionSummary>>(RootClient.GetUri(v5PublicOptionSummaryEndpoint), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve the estimated delivery price which will only have a return value one hour before the delivery/exercise.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<OkxEstimatedPrice>> GetEstimatedPriceAsync(string instrumentId, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "instId", instrumentId },
        };

        return await SendOKXSingleRequest<OkxEstimatedPrice>(RootClient.GetUri(v5PublicEstimatedPriceEndpoint), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve discount rate level and interest-free quota.
    /// </summary>
    /// <param name="discountLevel">Discount level</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<OkxDiscountInfo>>> GetDiscountInfoAsync(int? discountLevel = null, CancellationToken ct = default)
    {
        discountLevel?.ValidateIntBetween(nameof(discountLevel), 1, 100);

        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("discountLv", discountLevel?.ToString());

        return await SendOKXRequest<IEnumerable<OkxDiscountInfo>>(RootClient.GetUri(v5PublicDiscountRateInterestFreeQuotaEndpoint), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve API server time.
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default)
    {
        var result = await SendOKXRequest<IEnumerable<OkxTime>>(RootClient.GetUri(v5PublicTimeEndpoint), HttpMethod.Get, ct).ConfigureAwait(false);
        return result.As(result.Data?.FirstOrDefault()?.Time ?? default);
    }

    /// <summary>
    /// Retrieve information on liquidation orders in the last 1 days.
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="marginMode">Margin Mode</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="currency">Currency</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="alias">Alias</param>
    /// <param name="state">State</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<OkxLiquidationInfo>>> GetLiquidationOrdersAsync(
        OkxInstrumentType instrumentType,
        OkxMarginMode? marginMode = null,
        string instrumentId = null,
        string currency = null,
        string underlying = null,
        OkxInstrumentAlias? alias = null,
        OkxLiquidationState? state = null,
        long? after = null, long? before = null, int limit = 100,
        CancellationToken ct = default)
    {
        if (instrumentType.IsIn(OkxInstrumentType.Futures, OkxInstrumentType.Swap, OkxInstrumentType.Option) && string.IsNullOrEmpty(underlying))
            throw new ArgumentException("Underlying is required.");

        if (instrumentType.IsIn(OkxInstrumentType.Futures, OkxInstrumentType.Swap) && state == null)
            throw new ArgumentException("State is required.");

        if (instrumentType.IsIn(OkxInstrumentType.Futures) && alias == null)
            throw new ArgumentException("Alias is required.");

        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>
        {
            { "instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)) },
        };
        parameters.AddOptionalParameter("mgnMode", JsonConvert.SerializeObject(marginMode, new MarginModeConverter(false)));
        parameters.AddOptionalParameter("alias", JsonConvert.SerializeObject(alias, new InstrumentAliasConverter(false)));
        parameters.AddOptionalParameter("state", JsonConvert.SerializeObject(state, new LiquidationStateConverter(false)));
        parameters.AddOptionalParameter("instId", instrumentId);
        parameters.AddOptionalParameter("ccy", currency);
        parameters.AddOptionalParameter("uly", underlying);
        parameters.AddOptionalParameter("after", after?.ToString());
        parameters.AddOptionalParameter("before", before?.ToString());
        parameters.AddOptionalParameter("limit", limit.ToString());

        return await SendOKXRequest<IEnumerable<OkxLiquidationInfo>>(RootClient.GetUri(v5PublicLiquidationOrdersEndpoint), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve mark price.
    /// We set the mark price based on the SPOT index and at a reasonable basis to prevent individual users from manipulating the market and causing the contract price to fluctuate.
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<OkxMarkPrice>>> GetMarkPricesAsync(OkxInstrumentType instrumentType, string underlying = null, string instrumentId = null, CancellationToken ct = default)
    {
        if (instrumentType.IsNotIn(OkxInstrumentType.Margin, OkxInstrumentType.Futures, OkxInstrumentType.Option, OkxInstrumentType.Swap))
            throw new ArgumentException("Instrument Type can be only Margin, Futures, Option or Swap.");

        var parameters = new Dictionary<string, object>
        {
            { "instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)) },
        };
        parameters.AddOptionalParameter("uly", underlying);
        parameters.AddOptionalParameter("instId", instrumentId);

        return await SendOKXRequest<IEnumerable<OkxMarkPrice>>(RootClient.GetUri(v5PublicMarkPriceEndpoint), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Position information，Maximum leverage depends on your borrowings and margin ratio.
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="marginMode">Margin Mode</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="tier"></param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<OkxPositionTier>>> GetPositionTiersAsync(
        OkxInstrumentType instrumentType,
        OkxMarginMode marginMode,
        string underlying,
        string instrumentId = null,
        string tier = null,
        CancellationToken ct = default)
    {
        if (instrumentType.IsNotIn(OkxInstrumentType.Margin, OkxInstrumentType.Futures, OkxInstrumentType.Option, OkxInstrumentType.Swap))
            throw new ArgumentException("Instrument Type can be only Margin, Futures, Option or Swap.");

        var parameters = new Dictionary<string, object>
        {
            { "instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)) },
            { "tdMode", JsonConvert.SerializeObject(marginMode, new MarginModeConverter(false)) },
        };
        parameters.AddOptionalParameter("uly", underlying);
        parameters.AddOptionalParameter("instId", instrumentId);
        parameters.AddOptionalParameter("tier", tier);

        return await SendOKXRequest<IEnumerable<OkxPositionTier>>(RootClient.GetUri(v5PublicPositionTiersEndpoint), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Get margin interest rate
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<OKX.Api.Models.Public.OkxInterestRate>> GetInterestRatesAsync(CancellationToken ct = default)
    {
        return await SendOKXSingleRequest<OKX.Api.Models.Public.OkxInterestRate>(RootClient.GetUri(v5PublicInterestRateLoanQuotaEndpoint), HttpMethod.Get, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Get interest rate and loan quota for VIP loans
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<OkxVipInterestRate>>> GetVIPInterestRatesAsync(CancellationToken ct = default)
    {
        return await SendOKXRequest<IEnumerable<OkxVipInterestRate>>(RootClient.GetUri(v5PublicVIPInterestRateLoanQuotaEndpoint), HttpMethod.Get, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Get Underlying
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<string>>> GetUnderlyingAsync(OkxInstrumentType instrumentType, CancellationToken ct = default)
    {
        if (instrumentType.IsNotIn(OkxInstrumentType.Futures, OkxInstrumentType.Option, OkxInstrumentType.Swap))
            throw new ArgumentException("Instrument Type can be only Futures, Option or Swap.");

        var parameters = new Dictionary<string, object>
        {
            { "instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)) },
        };
        return await SendOKXSingleRequest<IEnumerable<string>>(RootClient.GetUri(v5PublicUnderlyingEndpoint), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Get insurance fund
    /// Get insurance fund balance information
    /// Rate Limit: 10 requests per 2 seconds
    /// </summary>
    /// <param name="instrumentType"></param>
    /// <param name="type"></param>
    /// <param name="underlying"></param>
    /// <param name="currency"></param>
    /// <param name="after"></param>
    /// <param name="before"></param>
    /// <param name="limit"></param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<OkxInsuranceFund>> GetInsuranceFundAsync(
        OkxInstrumentType instrumentType,
        OkxInsuranceType type = OkxInsuranceType.All,
        string underlying = "",
        string currency = "",
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        if (instrumentType.IsNotIn(OkxInstrumentType.Margin, OkxInstrumentType.Swap, OkxInstrumentType.Futures, OkxInstrumentType.Option))
            throw new ArgumentException("Instrument Type can be only Margin, Swap, Futures or Option.");

        var parameters = new Dictionary<string, object>
        {
            { "instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)) },
        };

        if (type != OkxInsuranceType.All)
            parameters.AddOptionalParameter("type", JsonConvert.SerializeObject(type, new InsuranceTypeConverter(false)));

        parameters.AddOptionalParameter("uly", underlying);
        parameters.AddOptionalParameter("ccy", currency);
        parameters.AddOptionalParameter("after", after?.ToString());
        parameters.AddOptionalParameter("before", before?.ToString());
        parameters.AddOptionalParameter("limit", limit.ToString());

        return await SendOKXSingleRequest<OkxInsuranceFund>(RootClient.GetUri(v5PublicInsuranceFundEndpoint), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Get Underlying
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<OkxUnitConvert>> UnitConvertAsync(
        OkxConvertType? type = OkxConvertType.CurrencyToContract,
        OkxConvertUnit? unit = null,
        string instrumentId = "",
        decimal? price = null,
        decimal? size = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("type", JsonConvert.SerializeObject(type, new ConvertTypeConverter(false)));
        parameters.AddOptionalParameter("unit", JsonConvert.SerializeObject(type, new ConvertUnitConverter(false)));
        parameters.AddOptionalParameter("instId", instrumentId);
        parameters.AddOptionalParameter("px", price?.ToString(OkxGlobals.OkxCultureInfo));
        parameters.AddOptionalParameter("sz", size?.ToString(OkxGlobals.OkxCultureInfo));

        return await SendOKXSingleRequest<OkxUnitConvert>(RootClient.GetUri(v5PublicConvertContractCoinEndpoint), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Market Data Methods
    /// <summary>
    /// Retrieve the latest price snapshot, best bid/ask price, and trading volume in the last 24 hours.
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<OkxTicker>>> GetTickersAsync(OkxInstrumentType instrumentType, string underlying = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)) },
        };
        parameters.AddOptionalParameter("uly", underlying);

        return await SendOKXRequest<IEnumerable<OkxTicker>>(RootClient.GetUri(Endpoints_V5_Market_Tickers), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve the latest price snapshot, best bid/ask price, and trading volume in the last 24 hours.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<OkxTicker>> GetTickerAsync(string instrumentId, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "instId", instrumentId },
        };

        return await SendOKXSingleRequest<OkxTicker>(RootClient.GetUri(Endpoints_V5_Market_Ticker), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve index tickers.
    /// </summary>
    /// <param name="quoteCurrency">Quote currency. Currently there is only an index with USD/USDT/BTC as the quote currency.</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<OkxIndexTicker>>> GetIndexTickersAsync(string quoteCurrency = null, string instrumentId = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("quoteCcy", quoteCurrency);
        parameters.AddOptionalParameter("instId", instrumentId);

        return await SendOKXRequest<IEnumerable<OkxIndexTicker>>(RootClient.GetUri(Endpoints_V5_Market_IndexTickers), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve a instrument is order book.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="depth">Order book depth per side. Maximum 400, e.g. 400 bids + 400 asks. Default returns to 1 depth data</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<OkxOrderBook>> GetOrderBookAsync(string instrumentId, int depth = 1, CancellationToken ct = default)
    {
        depth.ValidateIntBetween(nameof(depth), 1, 400);
        var parameters = new Dictionary<string, object>
        {
            {"instId", instrumentId},
            {"sz", depth},
        };

        var result = await SendOKXRequest<IEnumerable<OkxOrderBook>>(RootClient.GetUri(Endpoints_V5_Market_Books), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
        if (!result.Success || result.Data.Count() == 0) return result.AsError<OkxOrderBook>(new OkxRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Error != null && result.Error.Code > 0) return result.AsError<OkxOrderBook>(new OkxRestApiError(result.Error.Code, result.Error.Message, null));

        var orderbook = result.Data.FirstOrDefault();
        orderbook.Instrument = instrumentId;
        return result.As(orderbook);
    }

    /// <summary>
    /// Retrieve the candlestick charts. This endpoint can retrieve the latest 1,440 data entries. Charts are returned in groups based on the requested bar.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="period">Bar size, the default is 1m</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 300; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<OkxCandlestick>>> GetCandlesticksAsync(string instrumentId, OkxPeriod period, long? after = null, long? before = null, int limit = 100, CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 300);
        var parameters = new Dictionary<string, object>
        {
            { "instId", instrumentId },
            { "bar", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
        };
        parameters.AddOptionalParameter("after", after?.ToString());
        parameters.AddOptionalParameter("before", before?.ToString());
        parameters.AddOptionalParameter("limit", limit.ToString());

        var result = await SendOKXRequest<IEnumerable<OkxCandlestick>>(RootClient.GetUri(Endpoints_V5_Market_Candles), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
        if (!result.Success) return result;

        foreach (var candle in result.Data) candle.Instrument = instrumentId;
        return result;
    }

    /// <summary>
    /// Retrieve history candlestick charts from recent years.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="period">Bar size, the default is 1m</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<OkxCandlestick>>> GetCandlesticksHistoryAsync(string instrumentId, OkxPeriod period, long? after = null, long? before = null, int limit = 100, CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>
        {
            { "instId", instrumentId },
            { "bar", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
        };
        parameters.AddOptionalParameter("after", after?.ToString());
        parameters.AddOptionalParameter("before", before?.ToString());
        parameters.AddOptionalParameter("limit", limit.ToString());

        var result = await SendOKXRequest<IEnumerable<OkxCandlestick>>(RootClient.GetUri(Endpoints_V5_Market_HistoryCandles), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
        if (!result.Success) return result;

        foreach (var candle in result.Data) candle.Instrument = instrumentId;
        return result;
    }

    /// <summary>
    /// Retrieve the candlestick charts of the index. This endpoint can retrieve the latest 1,440 data entries. Charts are returned in groups based on the requested bar.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="period">Bar size, the default is 1m</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<OkxCandlestick>>> GetIndexCandlesticksAsync(string instrumentId, OkxPeriod period, long? after = null, long? before = null, int limit = 100, CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>
        {
            { "instId", instrumentId },
            { "bar", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
        };
        parameters.AddOptionalParameter("after", after?.ToString());
        parameters.AddOptionalParameter("before", before?.ToString());
        parameters.AddOptionalParameter("limit", limit.ToString());

        var result = await SendOKXRequest<IEnumerable<OkxCandlestick>>(RootClient.GetUri(Endpoints_V5_Market_IndexCandles), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
        if (!result.Success) return result;

        foreach (var candle in result.Data) candle.Instrument = instrumentId;
        return result;
    }

    /// <summary>
    /// Retrieve the candlestick charts of mark price. This endpoint can retrieve the latest 1,440 data entries. Charts are returned in groups based on the requested bar.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="period">Bar size, the default is 1m</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<OkxCandlestick>>> GetMarkPriceCandlesticksAsync(string instrumentId, OkxPeriod period, long? after = null, long? before = null, int limit = 100, CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>
        {
            { "instId", instrumentId },
            { "bar", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
        };
        parameters.AddOptionalParameter("after", after?.ToString());
        parameters.AddOptionalParameter("before", before?.ToString());
        parameters.AddOptionalParameter("limit", limit.ToString());

        var result = await SendOKXRequest<IEnumerable<OkxCandlestick>>(RootClient.GetUri(Endpoints_V5_Market_MarkPriceCandles), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
        if (!result.Success) return result;

        foreach (var candle in result.Data) candle.Instrument = instrumentId;
        return result;
    }

    /// <summary>
    /// Retrieve the recent transactions of an instrument.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<OkxTrade>>> GetTradesAsync(string instrumentId, int limit = 100, CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 500);
        var parameters = new Dictionary<string, object>
        {
            { "instId", instrumentId },
        };
        parameters.AddOptionalParameter("limit", limit.ToString());

        return await SendOKXRequest<IEnumerable<OkxTrade>>(RootClient.GetUri(Endpoints_V5_Market_Trades), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Get trades history
    /// Retrieve the recent transactions of an instrument from the last 3 months with pagination.
    /// Rate Limit: 10 requests per 2 seconds
    /// </summary>
    /// <param name="instrumentId">Instrument ID, e.g. BTC-USDT</param>
    /// <param name="type">Pagination Type</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<OkxTrade>>> GetTradesHistoryAsync(string instrumentId, OkxTradeHistoryPaginationType type = OkxTradeHistoryPaginationType.TradeId, long? after = null, long? before = null, int limit = 100, CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>
        {
            { "instId", instrumentId },
        };

        parameters.AddOptionalParameter("type", JsonConvert.SerializeObject(type, new TradeHistoryPaginationTypeConverter(false)));
        parameters.AddOptionalParameter("after", after?.ToString());
        parameters.AddOptionalParameter("before", before?.ToString());
        parameters.AddOptionalParameter("limit", limit.ToString());

        return await SendOKXRequest<IEnumerable<OkxTrade>>(RootClient.GetUri(Endpoints_V5_Market_TradesHistory), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// The 24-hour trading volume is calculated on a rolling basis, using USD as the pricing unit.
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<Okx24HourVolume>> Get24HourVolumeAsync(CancellationToken ct = default)
    {
        return await SendOKXSingleRequest<Okx24HourVolume>(RootClient.GetUri(Endpoints_V5_Market_Platform24Volume), HttpMethod.Get, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Get the crypto price of signing using Open Oracle smart contract.
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<OkxOracle>> GetOracleAsync(CancellationToken ct = default)
    {
        return await SendOKXSingleRequest<OkxOracle>(RootClient.GetUri(Endpoints_V5_Market_OpenOracle), HttpMethod.Get, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// This interface provides the average exchange rate data for 2 weeks
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<OkxExchangeRate>>> GetExchangeRatesAsync(CancellationToken ct = default)
    {
        return await SendOKXRequest<IEnumerable<OkxExchangeRate>>(RootClient.GetUri(Endpoints_V5_Market_ExchangeRate), HttpMethod.Get, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Get the index component information data on the market
    /// </summary>
    /// <param name="index"></param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<OkxIndexComponents>> GetIndexComponentsAsync(string index, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "index", index },
        };

        return await SendOKXRequest<OkxIndexComponents>(RootClient.GetUri(Endpoints_V5_Market_IndexComponents), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Get block tickers
    /// Retrieve the latest block trading volume in the last 24 hours.
    /// Rate Limit: 20 requests per 2 seconds
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<OkxBlockTicker>>> GetBlockTickersAsync(OkxInstrumentType instrumentType, string underlying = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)) },
        };
        parameters.AddOptionalParameter("uly", underlying);

        return await SendOKXRequest<IEnumerable<OkxBlockTicker>>(RootClient.GetUri(Endpoints_V5_Market_BlockTickers), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Get block ticker
    /// Retrieve the latest block trading volume in the last 24 hours.
    /// Rate Limit: 20 requests per 2 seconds
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<OkxBlockTicker>> GetBlockTickerAsync(string instrumentId, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "instId", instrumentId },
        };

        return await SendOKXSingleRequest<OkxBlockTicker>(RootClient.GetUri(Endpoints_V5_Market_BlockTicker), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Get block trades
    /// Retrieve the recent block trading transactions of an instrument. Descending order by tradeId.
    /// Rate Limit: 20 requests per 2 seconds
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<OkxTrade>>> GetBlockTradesAsync(string instrumentId, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "instId", instrumentId },
        };

        return await SendOKXRequest<IEnumerable<OkxTrade>>(RootClient.GetUri(Endpoints_V5_Market_BlockTrades), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

}