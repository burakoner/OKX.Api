namespace OKX.Api.Rubik;

/// <summary>
/// OKX Rest Api Trading Statistics Client
/// </summary>
public class OkxRubikRestClient(OkxRestApiClient root) : OkxBaseRestClient(root)
{
    private static readonly HashSet<string> ContractStatisticPeriods = new(StringComparer.Ordinal)
    {
        "5m",
        "15m",
        "30m",
        "1H",
        "2H",
        "4H",
        "6H",
        "12H",
        "1D",
        "2D",
        "3D",
        "5D",
        "1W",
        "1M",
        "3M",
        "6Hutc",
        "12Hutc",
        "1Dutc",
        "2Dutc",
        "3Dutc",
        "5Dutc",
        "1Wutc",
        "1Mutc",
        "3Mutc",
    };

    private static readonly HashSet<string> SimpleStatisticPeriods = new(StringComparer.Ordinal)
    {
        "5m",
        "1H",
        "1D",
    };

    private static readonly HashSet<string> OptionStatisticPeriods = new(StringComparer.Ordinal)
    {
        "8H",
        "1D",
    };

    /// <summary>
    /// Get the currency supported by the transaction big data interface
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<OkxRubikSupportCoins>> GetSupportCoinAsync(CancellationToken ct = default)
    {
        return await ProcessModelRequestAsync<OkxRubikSupportCoins>(GetUri("api/v5/rubik/stat/trading-data/support-coin"), HttpMethod.Get, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve the contract open interest statistics of futures and perp. This endpoint returns a maximum of 1440 records.
    /// </summary>
    /// <param name="instrumentId">Instrument ID, eg: BTC-USDT-SWAP</param>
    /// <param name="period">Bar size, the default is 5m, e.g. [5m/15m/30m/1H/2H/4H]
    /// Hong Kong time opening price k-line: [6H/12H/1D/2D/3D/5D/1W/1M/3M]
    /// UTC time opening price k-line: [6Hutc/12Hutc/1Dutc/2Dutc/3Dutc/5Dutc/1Wutc/1Mutc/3Mutc]</param>
    /// <param name="begin">Pagination of data to return records earlier than the requested ts</param>
    /// <param name="end">return records newer than the requested ts</param>
    /// <param name="limit">Number of results per request. The maximum is 100. The default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<List<OkxRubikContractOpenInterestHistory>>> GetContractOpenInterestHistoryAsync(
        string instrumentId,
        string? period = null,
        long? begin = null,
        long? end = null,
        int limit = 100,
        CancellationToken ct = default)
        => await GetContractOpenInterestHistoryAsync(new OkxRubikInstrumentPeriodRangeRequest
        {
            InstrumentId = instrumentId,
            Period = period,
            Begin = begin,
            End = end,
            Limit = limit
        }, ct).ConfigureAwait(false);

    /// <summary>
    /// Retrieve the contract open interest statistics of futures and perp. This endpoint returns a maximum of 1440 records.
    /// </summary>
    public async Task<RestCallResult<List<OkxRubikContractOpenInterestHistory>>> GetContractOpenInterestHistoryAsync(OkxRubikInstrumentPeriodRangeRequest request, CancellationToken ct = default)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));
        if (string.IsNullOrWhiteSpace(request.InstrumentId))
            throw new ArgumentException("Instrument ID is required.", nameof(request));

        request.Limit.ValidateIntBetween(nameof(request.Limit), 1, 100);
        ValidatePeriod(request.Period, ContractStatisticPeriods, nameof(request.Period), "contract open interest history");

        var parameters = new ParameterCollection {
            { "instId", request.InstrumentId!},
        };
        parameters.AddOptional("period", request.Period);
        parameters.AddOptional("begin", request.Begin?.ToOkxString());
        parameters.AddOptional("end", request.End?.ToOkxString());
        parameters.AddOptional("limit", request.Limit.ToOkxString());

        return await ProcessListRequestAsync<OkxRubikContractOpenInterestHistory>(GetUri("api/v5/rubik/stat/contracts/open-interest-history"), HttpMethod.Get, ct, false, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// This is the taker volume for both buyers and sellers. This shows the influx and exit of funds in and out of {coin}.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="period">Bar size, the default is 5m, e.g. [5m/15m/30m/1H/2H/4H]
    /// Hong Kong time opening price k-line:[6H/12H/1D/2D/3D/5D/1W/1M/3M]
    /// UTC time opening price k-line: [6Hutc/12Hutc/1Dutc/2Dutc/3Dutc/5Dutc/1Wutc/1Mutc/3Mutc]</param>
    /// <param name="begin">begin, e.g. 1597026383085</param>
    /// <param name="end">end, e.g. 1597026383011</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<List<OkxRubikTakerVolume>>> GetTakerVolumeAsync(
        string currency,
        OkxInstrumentType instrumentType,
        string? period = null,
        long? begin = null,
        long? end = null,
        CancellationToken ct = default)
        => await GetTakerVolumeAsync(new OkxRubikTakerVolumeRequest
        {
            Currency = currency,
            InstrumentType = instrumentType,
            Period = period,
            Begin = begin,
            End = end
        }, ct).ConfigureAwait(false);

    /// <summary>
    /// This is the taker volume for both buyers and sellers.
    /// </summary>
    public async Task<RestCallResult<List<OkxRubikTakerVolume>>> GetTakerVolumeAsync(OkxRubikTakerVolumeRequest request, CancellationToken ct = default)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));
        if (string.IsNullOrWhiteSpace(request.Currency))
            throw new ArgumentException("Currency is required.", nameof(request));

        ValidateTakerVolumeInstrumentType(request.InstrumentType);
        ValidatePeriod(request.Period, SimpleStatisticPeriods, nameof(request.Period), "taker volume");

        var parameters = new ParameterCollection {
            { "ccy", request.Currency!},
        };
        parameters.AddEnum("instType", request.InstrumentType);
        parameters.AddOptional("period", request.Period);
        parameters.AddOptional("begin", request.Begin?.ToOkxString());
        parameters.AddOptional("end", request.End?.ToOkxString());

        return await ProcessListRequestAsync<OkxRubikTakerVolume>(GetUri("api/v5/rubik/stat/taker-volume"), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve the contract taker volume for both buyers and sellers. This endpoint returns a maximum of 1440 records.
    /// </summary>
    /// <param name="instrumentId">Instrument ID, eg: BTC-USDT-SWAP</param>
    /// <param name="period">Bar size, the default is 5m, e.g. [5m/15m/30m/1H/2H/4H]
    /// Hong Kong time opening price k-line:[6H/12H/1D/2D/3D/5D/1W/1M/3M]
    /// UTC time opening price k-line: [6Hutc/12Hutc/1Dutc/2Dutc/3Dutc/5Dutc/1Wutc/1Mutc/3Mutc]</param>
    /// <param name="unit">The unit of buy/sell volume, the default is 1</param>
    /// <param name="begin">return records earlier than the requested ts</param>
    /// <param name="end">return records newer than the requested ts</param>
    /// <param name="limit">Number of results per request. The maximum is 100. The default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<List<OkxRubikContractTakerVolume>>> GetContractTakerVolumeAsync(
        string instrumentId,
        string? period = null,
        string? unit = null,
        long? begin = null,
        long? end = null,
        int limit = 100,
        CancellationToken ct = default)
        => await GetContractTakerVolumeAsync(new OkxRubikContractTakerVolumeRequest
        {
            InstrumentId = instrumentId,
            Period = period,
            Unit = unit,
            Begin = begin,
            End = end,
            Limit = limit
        }, ct).ConfigureAwait(false);

    /// <summary>
    /// Retrieve the contract taker volume for both buyers and sellers. This endpoint returns a maximum of 1440 records.
    /// </summary>
    public async Task<RestCallResult<List<OkxRubikContractTakerVolume>>> GetContractTakerVolumeAsync(OkxRubikContractTakerVolumeRequest request, CancellationToken ct = default)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));
        if (string.IsNullOrWhiteSpace(request.InstrumentId))
            throw new ArgumentException("Instrument ID is required.", nameof(request));

        request.Limit.ValidateIntBetween(nameof(request.Limit), 1, 100);
        ValidatePeriod(request.Period, ContractStatisticPeriods, nameof(request.Period), "contract taker volume");

        var parameters = new ParameterCollection {
            { "instId", request.InstrumentId!},
        };
        parameters.AddOptional("period", request.Period);
        parameters.AddOptional("unit", request.Unit);
        parameters.AddOptional("begin", request.Begin?.ToOkxString());
        parameters.AddOptional("end", request.End?.ToOkxString());
        parameters.AddOptional("limit", request.Limit.ToOkxString());

        return await ProcessListRequestAsync<OkxRubikContractTakerVolume>(GetUri("api/v5/rubik/stat/taker-volume-contract"), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// This indicator shows the ratio of cumulative data value between currency pair leverage quote currency and underlying asset over a given period of time.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="period">Bar size, the default is 5m, e.g. [5m/15m/30m/1H/2H/4H]
    /// Hong Kong time opening price k-line:[6H/12H/1D/2D/3D/5D/1W/1M/3M]
    /// UTC time opening price k-line: [6Hutc/12Hutc/1Dutc/2Dutc/3Dutc/5Dutc/1Wutc/1Mutc/3Mutc]</param>
    /// <param name="begin">begin, e.g. 1597026383085</param>
    /// <param name="end">end, e.g. 1597026383085</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<List<OkxRubikRatio>>> GetMarginLendingRatioAsync(
        string currency,
        string? period = null,
        long? begin = null,
        long? end = null,
        CancellationToken ct = default)
        => await GetMarginLendingRatioAsync(new OkxRubikCurrencyPeriodRangeRequest
        {
            Currency = currency,
            Period = period,
            Begin = begin,
            End = end
        }, ct).ConfigureAwait(false);

    /// <summary>
    /// This indicator shows the ratio of cumulative data value between currency pair leverage quote currency and underlying asset over a given period of time.
    /// </summary>
    public async Task<RestCallResult<List<OkxRubikRatio>>> GetMarginLendingRatioAsync(OkxRubikCurrencyPeriodRangeRequest request, CancellationToken ct = default)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));
        if (string.IsNullOrWhiteSpace(request.Currency))
            throw new ArgumentException("Currency is required.", nameof(request));

        ValidatePeriod(request.Period, ContractStatisticPeriods, nameof(request.Period), "margin lending ratio");

        var parameters = new ParameterCollection {
            { "ccy", request.Currency!},
        };
        parameters.AddOptional("period", request.Period);
        parameters.AddOptional("begin", request.Begin?.ToOkxString());
        parameters.AddOptional("end", request.End?.ToOkxString());

        return await ProcessListRequestAsync<OkxRubikRatio>(GetUri("api/v5/rubik/stat/margin/loan-ratio"), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve the account net long/short ratio of a contract for top traders. Top traders refer to the top 5% of traders with the largest open position value. This endpoint returns a maximum of 1440 records.
    /// </summary>
    /// <param name="instrumentId">Instrument ID, eg: BTC-USDT-SWAP</param>
    /// <param name="period">Bar size, the default is 5m, e.g. [5m/15m/30m/1H/2H/4H]
    /// Hong Kong time opening price k-line:[6H/12H/1D/2D/3D/5D/1W/1M/3M]
    /// UTC time opening price k-line: [6Hutc/12Hutc/1Dutc/2Dutc/3Dutc/5Dutc/1Wutc/1Mutc/3Mutc]</param>
    /// <param name="begin">return records earlier than the requested ts</param>
    /// <param name="end">return records newer than the requested ts</param>
    /// <param name="limit">Number of results per request. The maximum is 100. The default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<List<OkxRubikRatio>>> GetTopTradersContractLongShortRatioAsync(
        string instrumentId,
        string? period = null,
        long? begin = null,
        long? end = null,
        int limit = 100,
        CancellationToken ct = default)
        => await GetTopTradersContractLongShortRatioAsync(new OkxRubikInstrumentPeriodRangeRequest
        {
            InstrumentId = instrumentId,
            Period = period,
            Begin = begin,
            End = end,
            Limit = limit
        }, ct).ConfigureAwait(false);

    /// <summary>
    /// Retrieve the account net long/short ratio of a contract for top traders.
    /// </summary>
    public async Task<RestCallResult<List<OkxRubikRatio>>> GetTopTradersContractLongShortRatioAsync(OkxRubikInstrumentPeriodRangeRequest request, CancellationToken ct = default)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));
        if (string.IsNullOrWhiteSpace(request.InstrumentId))
            throw new ArgumentException("Instrument ID is required.", nameof(request));

        request.Limit.ValidateIntBetween(nameof(request.Limit), 1, 100);
        ValidatePeriod(request.Period, ContractStatisticPeriods, nameof(request.Period), "top traders contract long/short ratio");

        var parameters = new ParameterCollection {
            { "instId", request.InstrumentId!},
        };
        parameters.AddOptional("period", request.Period);
        parameters.AddOptional("begin", request.Begin?.ToOkxString());
        parameters.AddOptional("end", request.End?.ToOkxString());
        parameters.AddOptional("limit", request.Limit.ToOkxString());

        return await ProcessListRequestAsync<OkxRubikRatio>(GetUri("api/v5/rubik/stat/contracts/long-short-account-ratio-contract-top-trader"), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve the position long/short ratio of a contract for top traders. Top traders refer to the top 5% of traders with the largest open position value. This endpoint returns a maximum of 1440 records.
    /// </summary>
    /// <param name="instrumentId">Instrument ID, eg: BTC-USDT-SWAP</param>
    /// <param name="period">Bar size, the default is 5m, e.g. [5m/15m/30m/1H/2H/4H]
    /// Hong Kong time opening price k-line:[6H/12H/1D/2D/3D/5D/1W/1M/3M]
    /// UTC time opening price k-line: [6Hutc/12Hutc/1Dutc/2Dutc/3Dutc/5Dutc/1Wutc/1Mutc/3Mutc]</param>
    /// <param name="begin">return records earlier than the requested ts</param>
    /// <param name="end">return records newer than the requested ts</param>
    /// <param name="limit">Number of results per request. The maximum is 100. The default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<List<OkxRubikRatio>>> GetTopTradersContractLongShortRatioByPositionAsync(
        string instrumentId,
        string? period = null,
        long? begin = null,
        long? end = null,
        int limit = 100,
        CancellationToken ct = default)
        => await GetTopTradersContractLongShortRatioByPositionAsync(new OkxRubikInstrumentPeriodRangeRequest
        {
            InstrumentId = instrumentId,
            Period = period,
            Begin = begin,
            End = end,
            Limit = limit
        }, ct).ConfigureAwait(false);

    /// <summary>
    /// Retrieve the position long/short ratio of a contract for top traders.
    /// </summary>
    public async Task<RestCallResult<List<OkxRubikRatio>>> GetTopTradersContractLongShortRatioByPositionAsync(OkxRubikInstrumentPeriodRangeRequest request, CancellationToken ct = default)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));
        if (string.IsNullOrWhiteSpace(request.InstrumentId))
            throw new ArgumentException("Instrument ID is required.", nameof(request));

        request.Limit.ValidateIntBetween(nameof(request.Limit), 1, 100);
        ValidatePeriod(request.Period, ContractStatisticPeriods, nameof(request.Period), "top traders contract long/short ratio by position");

        var parameters = new ParameterCollection {
            { "instId", request.InstrumentId!},
        };
        parameters.AddOptional("period", request.Period);
        parameters.AddOptional("begin", request.Begin?.ToOkxString());
        parameters.AddOptional("end", request.End?.ToOkxString());
        parameters.AddOptional("limit", request.Limit.ToOkxString());

        return await ProcessListRequestAsync<OkxRubikRatio>(GetUri("api/v5/rubik/stat/contracts/long-short-position-ratio-contract-top-trader"), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve the position long/short ratio of a contract for top traders. Top traders refer to the top 5% of traders with the largest open position value. This endpoint returns a maximum of 1440 records.
    /// </summary>
    /// <param name="instrumentId">Instrument ID, eg: BTC-USDT-SWAP</param>
    /// <param name="period">Bar size, the default is 5m, e.g. [5m/15m/30m/1H/2H/4H]
    /// Hong Kong time opening price k-line:[6H/12H/1D/2D/3D/5D/1W/1M/3M]
    /// UTC time opening price k-line: [6Hutc/12Hutc/1Dutc/2Dutc/3Dutc/5Dutc/1Wutc/1Mutc/3Mutc]</param>
    /// <param name="begin">return records earlier than the requested ts</param>
    /// <param name="end">return records newer than the requested ts</param>
    /// <param name="limit">Number of results per request. The maximum is 100. The default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<List<OkxRubikRatio>>> GetContractLongShortRatioAsync(
        string instrumentId,
        string? period = null,
        long? begin = null,
        long? end = null,
        int limit = 100,
        CancellationToken ct = default)
        => await GetContractLongShortRatioAsync(new OkxRubikInstrumentPeriodRangeRequest
        {
            InstrumentId = instrumentId,
            Period = period,
            Begin = begin,
            End = end,
            Limit = limit
        }, ct).ConfigureAwait(false);

    /// <summary>
    /// Retrieve the position long/short ratio of a contract for top traders.
    /// </summary>
    public async Task<RestCallResult<List<OkxRubikRatio>>> GetContractLongShortRatioAsync(OkxRubikInstrumentPeriodRangeRequest request, CancellationToken ct = default)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));
        if (string.IsNullOrWhiteSpace(request.InstrumentId))
            throw new ArgumentException("Instrument ID is required.", nameof(request));

        request.Limit.ValidateIntBetween(nameof(request.Limit), 1, 100);
        ValidatePeriod(request.Period, ContractStatisticPeriods, nameof(request.Period), "contract long/short ratio");

        var parameters = new ParameterCollection {
            { "instId", request.InstrumentId!},
        };
        parameters.AddOptional("period", request.Period);
        parameters.AddOptional("begin", request.Begin?.ToOkxString());
        parameters.AddOptional("end", request.End?.ToOkxString());
        parameters.AddOptional("limit", request.Limit.ToOkxString());

        return await ProcessListRequestAsync<OkxRubikRatio>(GetUri("api/v5/rubik/stat/contracts/long-short-account-ratio-contract"), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// This is the ratio of users with net long vs short positions. It includes data from futures and perpetual swaps.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="period">Period, the default is 5m, e.g. [5m/1H/1D]
    /// 5m granularity can only query data within two days at most
    /// 1H granularity can only query data within 30 days at most
    /// 1D granularity can only query data within 180 days at most</param>
    /// <param name="begin">begin, e.g. 1597026383085</param>
    /// <param name="end">end, e.g. 1597026383011</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<List<OkxRubikRatio>>> GetLongShortRatioAsync(
        string currency,
        string? period = null,
        long? begin = null,
        long? end = null,
        CancellationToken ct = default)
        => await GetLongShortRatioAsync(new OkxRubikCurrencyPeriodRangeRequest
        {
            Currency = currency,
            Period = period,
            Begin = begin,
            End = end
        }, ct).ConfigureAwait(false);

    /// <summary>
    /// This is the ratio of users with net long vs short positions.
    /// </summary>
    public async Task<RestCallResult<List<OkxRubikRatio>>> GetLongShortRatioAsync(OkxRubikCurrencyPeriodRangeRequest request, CancellationToken ct = default)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));
        if (string.IsNullOrWhiteSpace(request.Currency))
            throw new ArgumentException("Currency is required.", nameof(request));

        ValidatePeriod(request.Period, SimpleStatisticPeriods, nameof(request.Period), "long/short ratio");

        var parameters = new ParameterCollection {
            { "ccy", request.Currency!},
        };
        parameters.AddOptional("period", request.Period);
        parameters.AddOptional("begin", request.Begin?.ToOkxString());
        parameters.AddOptional("end", request.End?.ToOkxString());

        return await ProcessListRequestAsync<OkxRubikRatio>(GetUri("api/v5/rubik/stat/contracts/long-short-account-ratio"), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
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
    public async Task<RestCallResult<List<OkxRubikInterestVolume>>> GetContractSummaryAsync(
        string currency,
        string? period = null,
        long? begin = null,
        long? end = null,
        CancellationToken ct = default)
        => await GetContractSummaryAsync(new OkxRubikCurrencyPeriodRangeRequest
        {
            Currency = currency,
            Period = period,
            Begin = begin,
            End = end
        }, ct).ConfigureAwait(false);

    /// <summary>
    /// Open interest is the sum of all long and short futures and perpetual swap positions.
    /// </summary>
    public async Task<RestCallResult<List<OkxRubikInterestVolume>>> GetContractSummaryAsync(OkxRubikCurrencyPeriodRangeRequest request, CancellationToken ct = default)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));
        if (string.IsNullOrWhiteSpace(request.Currency))
            throw new ArgumentException("Currency is required.", nameof(request));

        ValidatePeriod(request.Period, SimpleStatisticPeriods, nameof(request.Period), "contracts open interest and volume");

        var parameters = new ParameterCollection {
            { "ccy", request.Currency!},
        };
        parameters.AddOptional("period", request.Period);
        parameters.AddOptional("begin", request.Begin?.ToOkxString());
        parameters.AddOptional("end", request.End?.ToOkxString());

        return await ProcessListRequestAsync<OkxRubikInterestVolume>(GetUri("api/v5/rubik/stat/contracts/open-interest-volume"), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// This shows the sum of all open positions and how much total trading volume has taken place.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="period">period, the default is 8H. e.g. [8H/1D]</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<List<OkxRubikInterestVolume>>> GetOptionsSummaryAsync(
        string currency,
        string? period = null,
        CancellationToken ct = default)
        => await GetOptionsSummaryAsync(new OkxRubikOptionPeriodRequest
        {
            Currency = currency,
            Period = period
        }, ct).ConfigureAwait(false);

    /// <summary>
    /// This shows the sum of all open positions and how much total trading volume has taken place.
    /// </summary>
    public async Task<RestCallResult<List<OkxRubikInterestVolume>>> GetOptionsSummaryAsync(OkxRubikOptionPeriodRequest request, CancellationToken ct = default)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));
        if (string.IsNullOrWhiteSpace(request.Currency))
            throw new ArgumentException("Currency is required.", nameof(request));

        ValidatePeriod(request.Period, OptionStatisticPeriods, nameof(request.Period), "options open interest and volume");

        var parameters = new ParameterCollection {
            { "ccy", request.Currency!},
        };
        parameters.AddOptional("period", request.Period);

        return await ProcessListRequestAsync<OkxRubikInterestVolume>(GetUri("api/v5/rubik/stat/option/open-interest-volume"), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// This shows the relative buy/sell volume for calls and puts. It shows whether traders are bullish or bearish on price and volatility.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="period">period, the default is 8H. e.g. [8H/1D]</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<List<OkxRubikPutCallRatio>>> GetPutCallRatioAsync(
        string currency,
        string? period = null,
        CancellationToken ct = default)
        => await GetPutCallRatioAsync(new OkxRubikOptionPeriodRequest
        {
            Currency = currency,
            Period = period
        }, ct).ConfigureAwait(false);

    /// <summary>
    /// This shows the relative buy/sell volume for calls and puts.
    /// </summary>
    public async Task<RestCallResult<List<OkxRubikPutCallRatio>>> GetPutCallRatioAsync(OkxRubikOptionPeriodRequest request, CancellationToken ct = default)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));
        if (string.IsNullOrWhiteSpace(request.Currency))
            throw new ArgumentException("Currency is required.", nameof(request));

        ValidatePeriod(request.Period, OptionStatisticPeriods, nameof(request.Period), "put/call ratio");

        var parameters = new ParameterCollection {
            { "ccy", request.Currency!},
        };
        parameters.AddOptional("period", request.Period);

        return await ProcessListRequestAsync<OkxRubikPutCallRatio>(GetUri("api/v5/rubik/stat/option/open-interest-volume-ratio"), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// This shows the volume and open interest for each upcoming expiration. You can use this to see which expirations are currently the most popular to trade.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="period">period, the default is 8H. e.g. [8H/1D]</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<List<OkxRubikInterestVolumeExpiry>>> GetInterestVolumeExpiryAsync(
        string currency,
        string? period = null,
        CancellationToken ct = default)
        => await GetInterestVolumeExpiryAsync(new OkxRubikOptionPeriodRequest
        {
            Currency = currency,
            Period = period
        }, ct).ConfigureAwait(false);

    /// <summary>
    /// This shows the volume and open interest for each upcoming expiration.
    /// </summary>
    public async Task<RestCallResult<List<OkxRubikInterestVolumeExpiry>>> GetInterestVolumeExpiryAsync(OkxRubikOptionPeriodRequest request, CancellationToken ct = default)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));
        if (string.IsNullOrWhiteSpace(request.Currency))
            throw new ArgumentException("Currency is required.", nameof(request));

        ValidatePeriod(request.Period, OptionStatisticPeriods, nameof(request.Period), "open interest and volume by expiry");

        var parameters = new ParameterCollection {
            { "ccy", request.Currency!},
        };
        parameters.AddOptional("period", request.Period);

        return await ProcessListRequestAsync<OkxRubikInterestVolumeExpiry>(GetUri("api/v5/rubik/stat/option/open-interest-volume-expiry"), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// This shows what option strikes are the most popular for each expiration.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="expiryTime">expiry time (Format: YYYYMMdd, for example: "20210623")</param>
    /// <param name="period">period, the default is 8H. e.g. [8H/1D]</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<List<OkxRubikInterestVolumeStrike>>> GetInterestVolumeStrikeAsync(
        string currency,
        string expiryTime,
        string? period = null,
        CancellationToken ct = default)
        => await GetInterestVolumeStrikeAsync(new OkxRubikOptionStrikeRequest
        {
            Currency = currency,
            ExpiryTime = expiryTime,
            Period = period
        }, ct).ConfigureAwait(false);

    /// <summary>
    /// This shows what option strikes are the most popular for each expiration.
    /// </summary>
    public async Task<RestCallResult<List<OkxRubikInterestVolumeStrike>>> GetInterestVolumeStrikeAsync(OkxRubikOptionStrikeRequest request, CancellationToken ct = default)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));
        if (string.IsNullOrWhiteSpace(request.Currency))
            throw new ArgumentException("Currency is required.", nameof(request));
        if (string.IsNullOrWhiteSpace(request.ExpiryTime))
            throw new ArgumentException("Expiry time is required.", nameof(request));

        ValidatePeriod(request.Period, OptionStatisticPeriods, nameof(request.Period), "open interest and volume by strike");

        var parameters = new ParameterCollection {
            { "ccy", request.Currency!},
            { "expTime", request.ExpiryTime!},
        };
        parameters.AddOptional("period", request.Period);

        return await ProcessListRequestAsync<OkxRubikInterestVolumeStrike>(GetUri("api/v5/rubik/stat/option/open-interest-volume-strike"), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// This shows the relative buy/sell volume for calls and puts. It shows whether traders are bullish or bearish on price and volatility.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="period">period, the default is 8H. e.g. [8H/1D]</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<OkxRubikTakerFlow>> GetTakerFlowAsync(
        string currency,
        string? period = null,
        CancellationToken ct = default)
        => await GetTakerFlowAsync(new OkxRubikOptionPeriodRequest
        {
            Currency = currency,
            Period = period
        }, ct).ConfigureAwait(false);

    /// <summary>
    /// This shows the relative buy/sell volume for calls and puts.
    /// </summary>
    public async Task<RestCallResult<OkxRubikTakerFlow>> GetTakerFlowAsync(OkxRubikOptionPeriodRequest request, CancellationToken ct = default)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));
        if (string.IsNullOrWhiteSpace(request.Currency))
            throw new ArgumentException("Currency is required.", nameof(request));

        ValidatePeriod(request.Period, OptionStatisticPeriods, nameof(request.Period), "taker flow");

        var parameters = new ParameterCollection {
            { "ccy", request.Currency!},
        };
        parameters.AddOptional("period", request.Period);

        return await ProcessArrayModelRequestAsync<OkxRubikTakerFlow>(GetUri("api/v5/rubik/stat/option/taker-block-volume"), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
    }

    private static void ValidateTakerVolumeInstrumentType(OkxInstrumentType instrumentType)
    {
        if (instrumentType is OkxInstrumentType.Spot or OkxInstrumentType.Contracts)
            return;

        throw new ArgumentException("Trading Statistics taker volume only supports SPOT or CONTRACTS.", nameof(instrumentType));
    }

    private static void ValidatePeriod(string? period, ISet<string> allowedValues, string parameterName, string endpointName)
    {
        if (string.IsNullOrWhiteSpace(period))
            return;

        if (allowedValues.Contains(period!))
            return;

        throw new ArgumentException($"Invalid period '{period}' for {endpointName}. Allowed values: {string.Join(", ", allowedValues)}", parameterName);
    }
}


