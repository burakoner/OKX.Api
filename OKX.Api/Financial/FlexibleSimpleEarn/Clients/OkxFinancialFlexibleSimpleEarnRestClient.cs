namespace OKX.Api.Financial.FlexibleSimpleEarn;

/// <summary>
/// OKX Rest Api Savings Client
/// </summary>
public class OkxFinancialFlexibleSimpleEarnRestClient(OkxRestApiClient root) : OkxBaseRestClient(root)
{
    // Endpoints
    private const string v5FinanceSavingsBalance = "api/v5/finance/savings/balance";
    private const string v5FinanceSavingsPurchaseRedempt = "api/v5/finance/savings/purchase-redempt";
    private const string v5FinanceSavingsSetLendingRate = "api/v5/finance/savings/set-lending-rate";
    private const string v5FinanceSavingsLendingHistory = "api/v5/finance/savings/lending-history";
    private const string v5FinanceSavingsLendingRateSummary = "api/v5/finance/savings/lending-rate-summary";
    private const string v5FinanceSavingsLendingRateHistory = "api/v5/finance/savings/lending-rate-history";

    /// <summary>
    /// GET / Saving balance
    /// </summary>
    /// <param name="currency">Currency, e.g. BTC</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxFinancialFlexibleSimpleEarnSavingsBalance>>> GetBalancesAsync(string? currency = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("ccy", currency);

        return ProcessListRequestAsync<OkxFinancialFlexibleSimpleEarnSavingsBalance>(GetUri(v5FinanceSavingsBalance), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Only the assets in the funding account can be used for saving.
    /// </summary>
    /// <param name="currency">Currency, e.g. BTC</param>
    /// <param name="amount">Purchase/redemption amount</param>
    /// <param name="rate">Annual purchase rate</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxFinancialFlexibleSimpleEarnSavingsOrder>> PurchaseAsync(string currency, decimal amount, decimal rate, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            {"ccy", currency },
            {"side", "purchase" },
            {"amt", amount.ToOkxString() },
            {"rate", rate.ToOkxString() },
        };

        return ProcessOneRequestAsync<OkxFinancialFlexibleSimpleEarnSavingsOrder>(GetUri(v5FinanceSavingsPurchaseRedempt), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

        /// <summary>
    /// Only the assets in the funding account can be used for saving.
    /// </summary>
    /// <param name="currency">Currency, e.g. BTC</param>
    /// <param name="amount">Purchase/redemption amount</param>
    /// <param name="rate">Annual purchase rate</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxFinancialFlexibleSimpleEarnSavingsOrder>> RedeemAsync(string currency, decimal amount, decimal rate, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            {"ccy", currency },
            {"side", "redempt" },
            {"amt", amount.ToOkxString() },
            {"rate", rate.ToOkxString() },
        };

        return ProcessOneRequestAsync<OkxFinancialFlexibleSimpleEarnSavingsOrder>(GetUri(v5FinanceSavingsPurchaseRedempt), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Set lending rate
    /// </summary>
    /// <param name="currency">Currency, e.g. BTC</param>
    /// <param name="rate">Annual lending rate. The rate value range is between 1% and 365%</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxFinancialFlexibleSimpleEarnSavingsRate>> SetLendingRateAsync(string currency, decimal rate, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            {"ccy", currency },
            {"rate", rate.ToOkxString() },
        };

        return ProcessOneRequestAsync<OkxFinancialFlexibleSimpleEarnSavingsRate>(GetUri(v5FinanceSavingsSetLendingRate), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Return data in the past month.
    /// </summary>
    /// <param name="currency">Currency, e.g. BTC</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100. The default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxFinancialFlexibleSimpleEarnSavingsLendingHistory>>> GetLendingHistoryAsync(
        string? currency = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("ccy", currency);
        parameters.AddOptional("after", after?.ToOkxString());
        parameters.AddOptional("before", before?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxFinancialFlexibleSimpleEarnSavingsLendingHistory>(GetUri(v5FinanceSavingsLendingHistory), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }
    
    /// <summary>
    /// Authentication is not required for this public endpoint.
    /// </summary>
    /// <param name="currency">Currency, e.g. BTC</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxFinancialFlexibleSimpleEarnSavingsBorrowSummary>> GetPublicBorrowSummaryAsync(string? currency = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("ccy", currency);

        return ProcessOneRequestAsync<OkxFinancialFlexibleSimpleEarnSavingsBorrowSummary>(GetUri(v5FinanceSavingsLendingRateSummary), HttpMethod.Get, ct, signed: false, queryParameters: parameters);
    }

    /// <summary>
    /// Authentication is not required for this public endpoint.
    /// Only returned records after December 14, 2021.
    /// </summary>
    /// <param name="currency">Currency, e.g. BTC</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100. The default is 100. If ccy is not specified, all data under the same ts will be returned, not limited by limit</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxFinancialFlexibleSimpleEarnSavingsBorrowHistory>>> GetPublicBorrowHistoryAsync(
        string? currency = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("ccy", currency);
        parameters.AddOptional("after", after?.ToOkxString());
        parameters.AddOptional("before", before?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxFinancialFlexibleSimpleEarnSavingsBorrowHistory>(GetUri(v5FinanceSavingsLendingRateHistory), HttpMethod.Get, ct, signed: false, queryParameters: parameters);
    }
}