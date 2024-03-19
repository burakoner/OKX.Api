using OKX.Api.Models.Public;

namespace OKX.Api.Clients.RestApi;

/// <summary>
/// OKX Rest Api Public Data Client
/// </summary>
public class OKXRestApiPublicClient : OKXRestApiBaseClient
{
    // Public Endpoints
    private const string v5PublicInstruments = "api/v5/public/instruments";
    private const string v5PublicDeliveryExerciseHistory = "api/v5/public/delivery-exercise-history";
    private const string v5PublicOpenInterest = "api/v5/public/open-interest";
    private const string v5PublicFundingRate = "api/v5/public/funding-rate";
    private const string v5PublicFundingRateHistory = "api/v5/public/funding-rate-history";
    private const string v5PublicPriceLimit = "api/v5/public/price-limit";
    private const string v5PublicOptionSummary = "api/v5/public/opt-summary";
    private const string v5PublicEstimatedPrice = "api/v5/public/estimated-price";
    private const string v5PublicDiscountRateInterestFreeQuota = "api/v5/public/discount-rate-interest-free-quota";
    private const string v5PublicTime = "api/v5/public/time";
    private const string v5PublicMarkPrice = "api/v5/public/mark-price";
    private const string v5PublicPositionTiers = "api/v5/public/position-tiers";
    private const string v5PublicInterestRateLoanQuota = "api/v5/public/interest-rate-loan-quota";
    private const string v5PublicVIPInterestRateLoanQuota = "api/v5/public/vip-interest-rate-loan-quota";
    private const string v5PublicUnderlying = "api/v5/public/underlying";
    private const string v5PublicInsuranceFund = "api/v5/public/insurance-fund";
    private const string v5PublicConvertContractCoin = "api/v5/public/convert-contract-coin";
    // TODO: api/v5/public/instrument-tick-bands
    private const string v5MarketIndexTickers = "api/v5/market/index-tickers";
    private const string v5MarketIndexCandles = "api/v5/market/index-candles";
    // TODO: api/v5/market/history-index-candles
    private const string v5MarketMarkPriceCandles = "api/v5/market/mark-price-candles";
    // TODO: api/v5/market/history-mark-price-candles
    private const string v5MarketOpenOracle = "api/v5/market/open-oracle";
    private const string v5MarketExchangeRate = "api/v5/market/exchange-rate";
    private const string v5MarketIndexComponents = "api/v5/market/index-components";
    
    // Market Endpoints
    private const string v5MarketTickers = "api/v5/market/tickers";
    private const string v5MarketTicker = "api/v5/market/ticker";
    private const string v5MarketBooks = "api/v5/market/books";
    private const string v5MarketBooksLite = "api/v5/market/books-lite";
    private const string v5MarketCandles = "api/v5/market/candles";
    private const string v5MarketHistoryCandles = "api/v5/market/history-candles";
    private const string v5MarketTrades = "api/v5/market/trades";
    private const string v5MarketTradesHistory = "api/v5/market/history-trades";
    private const string v5MarketOptionInstrumentFamilyTrades = "api/v5/market/option/instrument-family-trades";
    private const string v5PublicOptionTrades = "api/v5/public/option-trades";
    private const string v5MarketPlatform24Volume = "api/v5/market/platform-24-volume";

    internal OKXRestApiPublicClient(OKXRestApiClient root) : base(root)
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
    public async Task<RestCallResult<IEnumerable<OkxInstrument>>> GetInstrumentsAsync(
        OkxInstrumentType instrumentType, 
        string underlying = null, 
        string instrumentId = null, 
        string instrumentFamily = null, 
        bool signed = false,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)) },
        };
        parameters.AddOptionalParameter("uly", underlying);
        parameters.AddOptionalParameter("instId", instrumentId);
        parameters.AddOptionalParameter("instFamily", instrumentFamily);

        return await SendOKXRequest<IEnumerable<OkxInstrument>>(GetUri(v5PublicInstruments), HttpMethod.Get, ct, signed: signed, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve the estimated delivery price, which will only have a return value one hour before the delivery/exercise.
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="instrumentFamily">Instrument Family</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<IEnumerable<OkxDeliveryExerciseHistory>>> GetDeliveryExerciseHistoryAsync(
        OkxInstrumentType instrumentType, 
        string underlying = null, 
        string instrumentFamily = null, 
        long? after = null,
        long? before = null, 
        int limit = 100, 
        CancellationToken ct = default)
    {
        if (instrumentType.IsNotIn(OkxInstrumentType.Futures, OkxInstrumentType.Option))
            throw new ArgumentException("Instrument Type can be only Futures or Option.");

        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>
        {
            { "instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)) },
        };
        parameters.AddOptionalParameter("uly", underlying);
        parameters.AddOptionalParameter("instFamily", instrumentFamily);
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return await SendOKXRequest<IEnumerable<OkxDeliveryExerciseHistory>>(GetUri(v5PublicDeliveryExerciseHistory), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve the total open interest for contracts on OKX
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="instrumentFamily">Instrument Family</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<IEnumerable<OkxOpenInterest>>> GetOpenInterestsAsync(
        OkxInstrumentType instrumentType, 
        string underlying = null, 
        string instrumentId = null, 
        string instrumentFamily = null, 
        CancellationToken ct = default)
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
        parameters.AddOptionalParameter("instFamily", instrumentFamily);

        return await SendOKXRequest<IEnumerable<OkxOpenInterest>>(GetUri(v5PublicOpenInterest), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve funding rate.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<IEnumerable<OkxFundingRate>>> GetFundingRatesAsync(string instrumentId, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "instId", instrumentId },
        };

        return await SendOKXRequest<IEnumerable<OkxFundingRate>>(GetUri(v5PublicFundingRate), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
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
    public async Task<RestCallResult<IEnumerable<OkxFundingRateHistory>>> GetFundingRateHistoryAsync(string instrumentId, long? after = null, long? before = null, int limit = 100, CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>
        {
            { "instId", instrumentId },
        };
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return await SendOKXRequest<IEnumerable<OkxFundingRateHistory>>(GetUri(v5PublicFundingRateHistory), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve the highest buy limit and lowest sell limit of the instrument.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<OkxLimitPrice>> GetLimitPriceAsync(string instrumentId, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "instId", instrumentId },
        };

        return await SendOKXSingleRequest<OkxLimitPrice>(GetUri(v5PublicPriceLimit), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve option market data.
    /// </summary>
    /// <param name="underlying">Underlying</param>
    /// <param name="instrumentFamily">Instrument family, only applicable to OPTION. Either uly or instFamily is required. If both are passed, instFamily will be used.</param>
    /// <param name="expiryDate">Contract expiry date, the format is "YYMMDD", e.g. "200527"</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<IEnumerable<OkxOptionSummary>>> GetOptionMarketDataAsync(
        string underlying = null,
        string instrumentFamily = null, 
        DateTime? expiryDate = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("uly", underlying);
        parameters.AddOptionalParameter("instFamily", instrumentFamily);
        parameters.AddOptionalParameter("expTime", expiryDate?.ToString("yyMMdd"));

        return await SendOKXRequest<IEnumerable<OkxOptionSummary>>(GetUri(v5PublicOptionSummary), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve the estimated delivery price which will only have a return value one hour before the delivery/exercise.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<OkxEstimatedPrice>> GetEstimatedPriceAsync(string instrumentId, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "instId", instrumentId },
        };

        return await SendOKXSingleRequest<OkxEstimatedPrice>(GetUri(v5PublicEstimatedPrice), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve discount rate level and interest-free quota.
    /// </summary>
    /// <param name="discountLevel">Discount level</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<IEnumerable<OkxDiscountInfo>>> GetDiscountInfoAsync(int? discountLevel = null, CancellationToken ct = default)
    {
        discountLevel?.ValidateIntBetween(nameof(discountLevel), 1, 100);

        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("discountLv", discountLevel?.ToOkxString());

        return await SendOKXRequest<IEnumerable<OkxDiscountInfo>>(GetUri(v5PublicDiscountRateInterestFreeQuota), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve API server time.
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default)
    {
        var result = await SendOKXRequest<IEnumerable<OkxTime>>(GetUri(v5PublicTime), HttpMethod.Get, ct).ConfigureAwait(false);
        return result.As(result.Data?.FirstOrDefault()?.Time ?? default);
    }

    /// <summary>
    /// Retrieve mark price.
    /// We set the mark price based on the SPOT index and at a reasonable basis to prevent individual users from manipulating the market and causing the contract price to fluctuate.
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="instrumentFamily">Instrument Family</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<IEnumerable<OkxMarkPrice>>> GetMarkPricesAsync(
        OkxInstrumentType instrumentType, 
        string underlying = null, 
        string instrumentId = null, 
        string instrumentFamily = null, 
        CancellationToken ct = default)
    {
        if (instrumentType.IsNotIn(OkxInstrumentType.Margin, OkxInstrumentType.Futures, OkxInstrumentType.Option, OkxInstrumentType.Swap))
            throw new ArgumentException("Instrument Type can be only Margin, Futures, Option or Swap.");

        var parameters = new Dictionary<string, object>
        {
            { "instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)) },
        };
        parameters.AddOptionalParameter("uly", underlying);
        parameters.AddOptionalParameter("instId", instrumentId);
        parameters.AddOptionalParameter("instFamily", instrumentFamily);

        return await SendOKXRequest<IEnumerable<OkxMarkPrice>>(GetUri(v5PublicMarkPrice), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Position information，Maximum leverage depends on your borrowings and margin ratio.
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="marginMode">Margin Mode</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="instrumentFamily">Instrument Family</param>
    /// <param name="currency">Currency</param>
    /// <param name="tier"></param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<IEnumerable<OkxPositionTier>>> GetPositionTiersAsync(
        OkxInstrumentType instrumentType,
        OkxMarginMode marginMode,
        string underlying,
        string instrumentId = null,
        string instrumentFamily = null, 
        string currency = null,
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
        parameters.AddOptionalParameter("instFamily", instrumentFamily);
        parameters.AddOptionalParameter("instId", instrumentId);
        parameters.AddOptionalParameter("ccy", currency);
        parameters.AddOptionalParameter("tier", tier);

        return await SendOKXRequest<IEnumerable<OkxPositionTier>>(GetUri(v5PublicPositionTiers), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Get margin interest rate
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<Models.Public.OkxInterestRate>> GetInterestRatesAsync(CancellationToken ct = default)
    {
        return await SendOKXSingleRequest<Models.Public.OkxInterestRate>(GetUri(v5PublicInterestRateLoanQuota), HttpMethod.Get, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Get interest rate and loan quota for VIP loans
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<IEnumerable<OkxVipInterestRate>>> GetVIPInterestRatesAsync(CancellationToken ct = default)
    {
        return await SendOKXRequest<IEnumerable<OkxVipInterestRate>>(GetUri(v5PublicVIPInterestRateLoanQuota), HttpMethod.Get, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Get Underlying
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<IEnumerable<string>>> GetUnderlyingAsync(OkxInstrumentType instrumentType, CancellationToken ct = default)
    {
        if (instrumentType.IsNotIn(OkxInstrumentType.Futures, OkxInstrumentType.Option, OkxInstrumentType.Swap))
            throw new ArgumentException("Instrument Type can be only Futures, Option or Swap.");

        var parameters = new Dictionary<string, object>
        {
            { "instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)) },
        };
        return await SendOKXSingleRequest<IEnumerable<string>>(GetUri(v5PublicUnderlying), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Get insurance fund
    /// Get insurance fund balance information
    /// Rate Limit: 10 requests per 2 seconds
    /// </summary>
    /// <param name="instrumentType"></param>
    /// <param name="type"></param>
    /// <param name="underlying"></param>
    /// <param name="instrumentFamily">Instrument Family</param>
    /// <param name="currency"></param>
    /// <param name="after"></param>
    /// <param name="before"></param>
    /// <param name="limit"></param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<OkxInsuranceFund>> GetInsuranceFundAsync(
        OkxInstrumentType instrumentType,
        OkxInsuranceType? type = null,
        string underlying = "",
        string instrumentFamily = "",
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

        parameters.AddOptionalParameter("type", JsonConvert.SerializeObject(type, new InsuranceTypeConverter(false)));
        parameters.AddOptionalParameter("uly", underlying);
        parameters.AddOptionalParameter("instFamily", instrumentFamily);
        parameters.AddOptionalParameter("ccy", currency);
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return await SendOKXSingleRequest<OkxInsuranceFund>(GetUri(v5PublicInsuranceFund), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Convert the crypto value to the number of contracts, or vice versa
    /// </summary>
    /// <param name="instrumentId">Instrument ID, only applicable to FUTURES/SWAP/OPTION</param>
    /// <param name="size">Quantity to buy or sell
    /// It is quantity of currency while converting currency to contract;
    /// It is quantity of contract while contract to currency.Quantity of coin needs to be positive integer</param>
    /// <param name="price">Order price
    /// For crypto-margined contracts, it is necessary while converting;
    /// For USDT-margined contracts, it is necessary while converting between usdt and contract, it is optional while converting between coin and contract.
    /// For OPTION, it is optional.</param>
    /// <param name="type">Convert type
    /// 1: Convert currency to contract.It will be rounded up when the value of contract is decimal
    /// 2: Convert contract to currency
    /// The defautl is 1</param>
    /// <param name="unit">The unit of currency. coin usds: usdt or usdc, only applicable to USDⓈ-margined contracts from FUTURES/SWAP</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<OkxUnitConvert>> UnitConvertAsync(
        string instrumentId,
        decimal size,
        decimal? price = null,
        OkxConvertType? type = null,
        OkxConvertUnit? unit = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("type", JsonConvert.SerializeObject(type, new ConvertTypeConverter(false)));
        parameters.AddOptionalParameter("instId", instrumentId);
        parameters.AddOptionalParameter("sz", size.ToOkxString());
        parameters.AddOptionalParameter("px", price?.ToOkxString());
        parameters.AddOptionalParameter("unit", JsonConvert.SerializeObject(unit, new ConvertUnitConverter(false)));

        return await SendOKXSingleRequest<OkxUnitConvert>(GetUri(v5PublicConvertContractCoin), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve index tickers.
    /// </summary>
    /// <param name="quoteCurrency">Quote currency. Currently there is only an index with USD/USDT/BTC as the quote currency.</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<IEnumerable<OkxIndexTicker>>> GetIndexTickersAsync(string quoteCurrency = null, string instrumentId = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("quoteCcy", quoteCurrency);
        parameters.AddOptionalParameter("instId", instrumentId);

        return await SendOKXRequest<IEnumerable<OkxIndexTicker>>(GetUri(v5MarketIndexTickers), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
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
    public async Task<RestCallResult<IEnumerable<OkxIndexCandlestick>>> GetIndexCandlesticksAsync(string instrumentId, OkxPeriod period, long? after = null, long? before = null, int limit = 100, CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>
        {
            { "instId", instrumentId },
            { "bar", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
        };
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        var result = await SendOKXRequest<IEnumerable<OkxIndexCandlestick>>(GetUri(v5MarketIndexCandles), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
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
    public async Task<RestCallResult<IEnumerable<OkxMarkCandlestick>>> GetMarkPriceCandlesticksAsync(string instrumentId, OkxPeriod period, long? after = null, long? before = null, int limit = 100, CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>
        {
            { "instId", instrumentId },
            { "bar", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
        };
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        var result = await SendOKXRequest<IEnumerable<OkxMarkCandlestick>>(GetUri(v5MarketMarkPriceCandles), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
        if (!result.Success) return result;

        foreach (var candle in result.Data) candle.Instrument = instrumentId;
        return result;
    }

    /// <summary>
    /// Get the crypto price of signing using Open Oracle smart contract.
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<OkxOracle>> GetOracleAsync(CancellationToken ct = default)
    {
        return await SendOKXSingleRequest<OkxOracle>(GetUri(v5MarketOpenOracle), HttpMethod.Get, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// This interface provides the average exchange rate data for 2 weeks
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<IEnumerable<OkxExchangeRate>>> GetExchangeRatesAsync(CancellationToken ct = default)
    {
        return await SendOKXRequest<IEnumerable<OkxExchangeRate>>(GetUri(v5MarketExchangeRate), HttpMethod.Get, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Get the index component information data on the market
    /// </summary>
    /// <param name="index"></param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<OkxIndexComponents>> GetIndexComponentsAsync(string index, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "index", index },
        };

        return await SendOKXRequest<OkxIndexComponents>(GetUri(v5MarketIndexComponents), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
    }

    #endregion
    
    #region Market Data Methods
    /// <summary>
    /// Retrieve the latest price snapshot, best bid/ask price, and trading volume in the last 24 hours.
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="instrumentFamily">Instrument Family</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<IEnumerable<OkxTicker>>> GetTickersAsync(
        OkxInstrumentType instrumentType, 
        string instrumentFamily = null,
        string underlying = null, 
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "instType", JsonConvert.SerializeObject(instrumentType, new InstrumentTypeConverter(false)) },
        };
        parameters.AddOptionalParameter("instFamily", instrumentFamily);
        parameters.AddOptionalParameter("uly", underlying);

        return await SendOKXRequest<IEnumerable<OkxTicker>>(GetUri(v5MarketTickers), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve the latest price snapshot, best bid/ask price, and trading volume in the last 24 hours.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<OkxTicker>> GetTickerAsync(string instrumentId, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "instId", instrumentId },
        };

        return await SendOKXSingleRequest<OkxTicker>(GetUri(v5MarketTicker), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve a instrument is order book.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="depth">Order book depth per side. Maximum 400, e.g. 400 bids + 400 asks. Default returns to 1 depth data</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<OkxOrderBook>> GetOrderBookAsync(string instrumentId, int depth = 1, CancellationToken ct = default)
    {
        depth.ValidateIntBetween(nameof(depth), 1, 400);
        var parameters = new Dictionary<string, object>
        {
            { "instId", instrumentId},
            { "sz", depth},
        };

        var result = await SendOKXRequest<IEnumerable<OkxOrderBook>>(GetUri(v5MarketBooks), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
        if (!result.Success || result.Data.Count() == 0) return result.AsError<OkxOrderBook>(new OkxRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Error != null && result.Error.Code > 0) return result.AsError<OkxOrderBook>(new OkxRestApiError(result.Error.Code, result.Error.Message, null));

        var orderbook = result.Data.FirstOrDefault();
        orderbook.Instrument = instrumentId;
        return result.As(orderbook);
    }

    // BURAK
    /// <summary>
    /// Retrieve order top 25 book of the instrument more quickly
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<OkxOrderBook>> GetOrderBookLiteAsync(string instrumentId, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "instId", instrumentId},
        };

        var result = await SendOKXRequest<IEnumerable<OkxOrderBook>>(GetUri(v5MarketBooksLite), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
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
    public async Task<RestCallResult<IEnumerable<OkxCandlestick>>> GetCandlesticksAsync(
        string instrumentId, 
        OkxPeriod period, 
        long? after = null, 
        long? before = null, 
        int limit = 100, 
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 300);
        var parameters = new Dictionary<string, object>
        {
            { "instId", instrumentId },
            { "bar", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
        };
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        var result = await SendOKXRequest<IEnumerable<OkxCandlestick>>(GetUri(v5MarketCandles), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
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
    public async Task<RestCallResult<IEnumerable<OkxCandlestick>>> GetCandlesticksHistoryAsync(string instrumentId, OkxPeriod period, long? after = null, long? before = null, int limit = 100, CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>
        {
            { "instId", instrumentId },
            { "bar", JsonConvert.SerializeObject(period, new PeriodConverter(false)) },
        };
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        var result = await SendOKXRequest<IEnumerable<OkxCandlestick>>(GetUri(v5MarketHistoryCandles), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
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
    public async Task<RestCallResult<IEnumerable<OkxTrade>>> GetTradesAsync(string instrumentId, int limit = 100, CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 500);
        var parameters = new Dictionary<string, object>
        {
            { "instId", instrumentId },
        };
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return await SendOKXRequest<IEnumerable<OkxTrade>>(GetUri(v5MarketTrades), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
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
    public async Task<RestCallResult<IEnumerable<OkxTrade>>> GetTradesHistoryAsync(
        string instrumentId,
        OkxTradeHistoryPaginationType? type = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>
        {
            { "instId", instrumentId },
        };

        parameters.AddOptionalParameter("type", JsonConvert.SerializeObject(type, new TradeHistoryPaginationTypeConverter(false)));
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return await SendOKXRequest<IEnumerable<OkxTrade>>(GetUri(v5MarketTradesHistory), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
    }

    // BURAK
    /// <summary>
    /// Retrieve the recent transactions of an instrument under same instFamily. The maximum is 100.
    /// </summary>
    /// <param name="instrumentFamily">Instrument family, e.g. BTC-USD. Applicable to OPTION</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<IEnumerable<OkxOptionTradeByInstrumentFamily>>> GetOptionTradesByInstrumentFamilyAsync(
        string instrumentFamily,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "instFamily", instrumentFamily },
        };

        return await SendOKXRequest<IEnumerable<OkxOptionTradeByInstrumentFamily>>(GetUri(v5MarketOptionInstrumentFamilyTrades), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
    }

    // BURAK
    /// <summary>
    /// The maximum is 100.
    /// </summary>
    /// <param name="instrumentId">Instrument ID, e.g. BTC-USD-221230-4000-C, Either instId or instFamily is required. If both are passed, instId will be used.</param>
    /// <param name="instrumentFamily">Instrument family, e.g. BTC-USD</param>
    /// <param name="optionType">Option type, C: Call P: put</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<IEnumerable<OkxOptionTrade>>> GetOptionTradesByInstrumentFamilyAsync(
    string instrumentId = null,
    string instrumentFamily = null,
    OkxOptionType? optionType = null,
    CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("instId", instrumentId);
        parameters.AddOptionalParameter("instFamily", instrumentFamily);
        parameters.AddOptionalParameter("optType", JsonConvert.SerializeObject(optionType, new OptionTypeConverter(false)));

        return await SendOKXRequest<IEnumerable<OkxOptionTrade>>(GetUri(v5PublicOptionTrades), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// The 24-hour trading volume is calculated on a rolling basis, using USD as the pricing unit.
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<OkxVolume>> Get24HourVolumeAsync(CancellationToken ct = default)
    {
        return await SendOKXSingleRequest<OkxVolume>(GetUri(v5MarketPlatform24Volume), HttpMethod.Get, ct).ConfigureAwait(false);
    }
    #endregion

}