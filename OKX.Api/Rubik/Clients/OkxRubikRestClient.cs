﻿using OKX.Api.Rubik.Models;

namespace OKX.Api.Rubik.Clients;

/// <summary>
/// OKX Rest Api Trading Statistics Client
/// </summary>
public class OkxRubikRestClient(OkxRestApiClient root) : OkxBaseRestClient(root)
{
    // Endpoints
    private const string v5RubikStatTradingDataSupportCoin = "api/v5/rubik/stat/trading-data/support-coin";
    private const string v5RubikStatContractsOpenInterestHistory = "api/v5/rubik/stat/contracts/open-interest-history";
    private const string v5RubikStatTakerVolume = "api/v5/rubik/stat/taker-volume";
    private const string v5RubikStatTakerVolumeContract = "api/v5/rubik/stat/taker-volume-contract";
    private const string v5RubikStatMarginLoanRatio = "api/v5/rubik/stat/margin/loan-ratio";
    private const string v5RubikStatContractsLongShortAccountRatioContractTopTrader = "api/v5/rubik/stat/contracts/long-short-account-ratio-contract-top-trader";
    private const string v5RubikStatContractsLongShortPositionRatioContractTopTrader = "api/v5/rubik/stat/contracts/long-short-position-ratio-contract-top-trader";
    private const string v5RubikStatContractsLongShortAccountRatioContract = "api/v5/rubik/stat/contracts/long-short-account-ratio-contract";
    private const string v5RubikStatContractsLongShortAccountRatio = "api/v5/rubik/stat/contracts/long-short-account-ratio";
    private const string v5RubikStatContractsOpenInterestVolume = "api/v5/rubik/stat/contracts/open-interest-volume";
    private const string v5RubikStatOptionOpenInterestVolume = "api/v5/rubik/stat/option/open-interest-volume";
    private const string v5RubikStatOptionOpenInterestVolumeRatio = "api/v5/rubik/stat/option/open-interest-volume-ratio";
    private const string v5RubikStatOptionOpenInterestVolumeExpiry = "api/v5/rubik/stat/option/open-interest-volume-expiry";
    private const string v5RubikStatOptionOpenInterestVolumeStrike = "api/v5/rubik/stat/option/open-interest-volume-strike";
    private const string v5RubikStatOptionTakerBlockVolume = "api/v5/rubik/stat/option/taker-block-volume";

    /// <summary>
    /// Get the currency supported by the transaction big data interface
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<OkxRubikSupportCoins>> GetSupportCoinAsync(CancellationToken ct = default)
    {
        return await ProcessModelRequestAsync<OkxRubikSupportCoins>(GetUri(v5RubikStatTradingDataSupportCoin), HttpMethod.Get, ct).ConfigureAwait(false);
    }

    public async Task<RestCallResult<List<OkxRubikContractOpenInterestHistory>>> GetContractOpenInterestHistoryAsync(
        string instrumentId,
        string period = null,
        long? begin = null,
        long? end = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object> {
            { "instId", instrumentId},
        };
        parameters.AddOptionalParameter("period", period);
        parameters.AddOptionalParameter("begin", begin?.ToOkxString());
        parameters.AddOptionalParameter("end", end?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return await ProcessListRequestAsync<OkxRubikContractOpenInterestHistory>(GetUri(v5RubikStatContractsOpenInterestHistory), HttpMethod.Get, ct, false, queryParameters: parameters).ConfigureAwait(false);
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
    public async Task<RestCallResult<List<OkxRubikTakerVolume>>> GetTakerVolumeAsync(
        string currency,
        OkxInstrumentType instrumentType,
        string period = null,
        long? begin = null,
        long? end = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "ccy", currency},
            { "instType", JsonConvert.SerializeObject(instrumentType, new OkxInstrumentTypeConverter(false)) },
        };
        parameters.AddOptionalParameter("period", period);
        parameters.AddOptionalParameter("begin", begin?.ToOkxString());
        parameters.AddOptionalParameter("end", end?.ToOkxString());

        return await ProcessListRequestAsync<OkxRubikTakerVolume>(GetUri(v5RubikStatTakerVolume), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
    }

    public async Task<RestCallResult<List<OkxRubikContractTakerVolume>>> GetContractTakerVolumeAsync(
        string instrumentId,
        string period = null,
        string unit = null,
        long? begin = null,
        long? end = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object> {
            { "instId", instrumentId},
        };
        parameters.AddOptionalParameter("period", JsonConvert.SerializeObject(period, new OkxPeriodConverter(false)));
        parameters.AddOptionalParameter("unit", unit);
        parameters.AddOptionalParameter("begin", begin?.ToOkxString());
        parameters.AddOptionalParameter("end", end?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return await ProcessListRequestAsync<OkxRubikContractTakerVolume>(GetUri(v5RubikStatTakerVolumeContract), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
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
    public async Task<RestCallResult<List<OkxRubikRatio>>> GetMarginLendingRatioAsync(
        string currency,
        string period = null,
        long? begin = null,
        long? end = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "ccy", currency},
        };
        parameters.AddOptionalParameter("period", period);
        parameters.AddOptionalParameter("begin", begin?.ToOkxString());
        parameters.AddOptionalParameter("end", end?.ToOkxString());

        return await ProcessListRequestAsync<OkxRubikRatio>(GetUri(v5RubikStatMarginLoanRatio), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
    }

    public async Task<RestCallResult<List<OkxRubikRatio>>> GetTopTradersContractLongShortRatioAsync(
        string instrumentId,
        string period = null,
        long? begin = null,
        long? end = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object> {
            { "instId", instrumentId},
        };
        parameters.AddOptionalParameter("period", period);
        parameters.AddOptionalParameter("begin", begin?.ToOkxString());
        parameters.AddOptionalParameter("end", end?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return await ProcessListRequestAsync<OkxRubikRatio>(GetUri(v5RubikStatContractsLongShortAccountRatioContractTopTrader), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
    }
    
    public async Task<RestCallResult<List<OkxRubikRatio>>> GetTopTradersContractLongShortRatioByPositionAsync(
        string instrumentId,
        string period = null,
        long? begin = null,
        long? end = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object> {
            { "instId", instrumentId},
        };
        parameters.AddOptionalParameter("period", period);
        parameters.AddOptionalParameter("begin", begin?.ToOkxString());
        parameters.AddOptionalParameter("end", end?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return await ProcessListRequestAsync<OkxRubikRatio>(GetUri(v5RubikStatContractsLongShortPositionRatioContractTopTrader), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
    }

    public async Task<RestCallResult<List<OkxRubikRatio>>> GetContractLongShortRatioAsync(
        string instrumentId,
        string period = null,
        long? begin = null,
        long? end = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object> {
            { "instId", instrumentId},
        };
        parameters.AddOptionalParameter("period", period);
        parameters.AddOptionalParameter("begin", begin?.ToOkxString());
        parameters.AddOptionalParameter("end", end?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return await ProcessListRequestAsync<OkxRubikRatio>(GetUri(v5RubikStatContractsLongShortAccountRatioContract), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
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
    public async Task<RestCallResult<List<OkxRubikRatio>>> GetLongShortRatioAsync(
        string currency,
        string period = null,
        long? begin = null,
        long? end = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "ccy", currency},
        };
        parameters.AddOptionalParameter("period", period);
        parameters.AddOptionalParameter("begin", begin?.ToOkxString());
        parameters.AddOptionalParameter("end", end?.ToOkxString());

        return await ProcessListRequestAsync<OkxRubikRatio>(GetUri(v5RubikStatContractsLongShortAccountRatio), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
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
        string period = null,
        long? begin = null,
        long? end = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "ccy", currency},
        };
        parameters.AddOptionalParameter("period", period);
        parameters.AddOptionalParameter("begin", begin?.ToOkxString());
        parameters.AddOptionalParameter("end", end?.ToOkxString());

        return await ProcessListRequestAsync<OkxRubikInterestVolume>(GetUri(v5RubikStatContractsOpenInterestVolume), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
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
       string period = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "ccy", currency},
        };
        parameters.AddOptionalParameter("period", period);

        return await ProcessListRequestAsync<OkxRubikInterestVolume>(GetUri(v5RubikStatOptionOpenInterestVolume), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
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
        string period = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "ccy", currency},
        };
        parameters.AddOptionalParameter("period", period);

        return await ProcessListRequestAsync<OkxRubikPutCallRatio>(GetUri(v5RubikStatOptionOpenInterestVolumeRatio), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
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
        string period = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "ccy", currency},
        };
        parameters.AddOptionalParameter("period", period);

        return await ProcessListRequestAsync<OkxRubikInterestVolumeExpiry>(GetUri(v5RubikStatOptionOpenInterestVolumeExpiry), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
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
        string period = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "ccy", currency},
            { "expTime", expiryTime},
        };
        parameters.AddOptionalParameter("period", period);

        return await ProcessListRequestAsync<OkxRubikInterestVolumeStrike>(GetUri(v5RubikStatOptionOpenInterestVolumeStrike), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
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
        string period = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "ccy", currency},
        };
        parameters.AddOptionalParameter("period", period);

        return await ProcessArrayModelRequestAsync<OkxRubikTakerFlow>(GetUri(v5RubikStatOptionTakerBlockVolume), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
    }

}