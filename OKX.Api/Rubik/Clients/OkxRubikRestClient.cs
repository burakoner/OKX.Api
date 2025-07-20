namespace OKX.Api.Rubik;

/// <summary>
/// OKX Rest Api Trading Statistics Client
/// </summary>
public class OkxRubikRestClient(OkxRestApiClient root) : OkxBaseRestClient(root)
{
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
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);

        var parameters = new ParameterCollection {
            { "instId", instrumentId},
        };
        parameters.AddOptional("period", period);
        parameters.AddOptional("begin", begin?.ToOkxString());
        parameters.AddOptional("end", end?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

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
    {
        var parameters = new ParameterCollection {
            { "ccy", currency},
        };
        parameters.AddEnum("instType", instrumentType);
        parameters.AddOptional("period", period);
        parameters.AddOptional("begin", begin?.ToOkxString());
        parameters.AddOptional("end", end?.ToOkxString());

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
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);

        var parameters = new ParameterCollection {
            { "instId", instrumentId},
        };
        parameters.AddOptional("period", period);
        parameters.AddOptional("unit", unit);
        parameters.AddOptional("begin", begin?.ToOkxString());
        parameters.AddOptional("end", end?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

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
    {
        var parameters = new ParameterCollection {
            { "ccy", currency},
        };
        parameters.AddOptional("period", period);
        parameters.AddOptional("begin", begin?.ToOkxString());
        parameters.AddOptional("end", end?.ToOkxString());

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
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);

        var parameters = new ParameterCollection {
            { "instId", instrumentId},
        };
        parameters.AddOptional("period", period);
        parameters.AddOptional("begin", begin?.ToOkxString());
        parameters.AddOptional("end", end?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

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
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);

        var parameters = new ParameterCollection {
            { "instId", instrumentId},
        };
        parameters.AddOptional("period", period);
        parameters.AddOptional("begin", begin?.ToOkxString());
        parameters.AddOptional("end", end?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

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
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);

        var parameters = new ParameterCollection {
            { "instId", instrumentId},
        };
        parameters.AddOptional("period", period);
        parameters.AddOptional("begin", begin?.ToOkxString());
        parameters.AddOptional("end", end?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

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
    {
        var parameters = new ParameterCollection {
            { "ccy", currency},
        };
        parameters.AddOptional("period", period);
        parameters.AddOptional("begin", begin?.ToOkxString());
        parameters.AddOptional("end", end?.ToOkxString());

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
    {
        var parameters = new ParameterCollection {
            { "ccy", currency},
        };
        parameters.AddOptional("period", period);
        parameters.AddOptional("begin", begin?.ToOkxString());
        parameters.AddOptional("end", end?.ToOkxString());

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
    {
        var parameters = new ParameterCollection {
            { "ccy", currency},
        };
        parameters.AddOptional("period", period);

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
    {
        var parameters = new ParameterCollection {
            { "ccy", currency},
        };
        parameters.AddOptional("period", period);

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
    {
        var parameters = new ParameterCollection {
            { "ccy", currency},
        };
        parameters.AddOptional("period", period);

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
    {
        var parameters = new ParameterCollection {
            { "ccy", currency},
            { "expTime", expiryTime},
        };
        parameters.AddOptional("period", period);

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
    {
        var parameters = new ParameterCollection {
            { "ccy", currency},
        };
        parameters.AddOptional("period", period);

        return await ProcessArrayModelRequestAsync<OkxRubikTakerFlow>(GetUri("api/v5/rubik/stat/option/taker-block-volume"), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
    }

}