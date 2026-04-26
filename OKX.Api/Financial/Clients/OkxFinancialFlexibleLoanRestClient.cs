namespace OKX.Api.Financial;

/// <summary>
/// OKX Rest API Flexible Loan Client
/// </summary>
public class OkxFinancialFlexibleLoanRestClient(OkxRestApiClient root) : OkxBaseRestClient(root)
{
    /// <summary>
    /// Get the supported currencies that can be borrowed through flexible loan.
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxFinancialFlexibleLoanBorrowableCurrency>>> GetBorrowableCurrenciesAsync(CancellationToken ct = default)
    {
        return ProcessListRequestAsync<OkxFinancialFlexibleLoanBorrowableCurrency>(GetUri("api/v5/finance/flexible-loan/borrow-currencies"), HttpMethod.Get, ct, signed: true);
    }

    /// <summary>
    /// Get the collateral assets for an existing flexible loan, or the earliest live loan when <paramref name="orderId"/> is omitted.
    /// </summary>
    /// <param name="currency">Collateral currency, e.g. BTC</param>
    /// <param name="orderId">Flexible loan order ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxFinancialFlexibleLoanCollateralAssets>> GetCollateralAssetsAsync(
        string? currency = null,
        string? orderId = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("ccy", currency);
        parameters.AddOptional("ordId", orderId);

        return ProcessOneRequestAsync<OkxFinancialFlexibleLoanCollateralAssets>(GetUri("api/v5/finance/flexible-loan/collateral-assets"), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Calculate the maximum amount that can be borrowed for the selected currency and supplementary collateral set.
    /// </summary>
    /// <param name="borrowCurrency">Currency to borrow, e.g. USDT</param>
    /// <param name="orderId">Flexible loan order ID</param>
    /// <param name="supplementaryCollateral">Supplementary collateral assets</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxFinancialFlexibleLoanMaximumLoan>> GetMaximumLoanAmountAsync(
        string borrowCurrency,
        string? orderId = null,
        IEnumerable<OkxFinancialFlexibleLoanSupplementaryCollateral>? supplementaryCollateral = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "borrowCcy", borrowCurrency },
        };
        parameters.AddOptional("ordId", orderId);
        parameters.AddOptional("supCollateral", supplementaryCollateral?.Select(ToSupplementaryCollateralPayload).ToArray());

        return ProcessOneRequestAsync<OkxFinancialFlexibleLoanMaximumLoan>(GetUri("api/v5/finance/flexible-loan/max-loan"), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Get the maximum redeemable collateral amount for the selected currency.
    /// </summary>
    /// <param name="currency">Collateral currency, e.g. BTC</param>
    /// <param name="orderId">Flexible loan order ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxFinancialFlexibleLoanMaximumCollateralRedeemAmount>> GetMaximumCollateralRedeemAmountAsync(
        string currency,
        string? orderId = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "ccy", currency },
        };
        parameters.AddOptional("ordId", orderId);

        return ProcessOneRequestAsync<OkxFinancialFlexibleLoanMaximumCollateralRedeemAmount>(GetUri("api/v5/finance/flexible-loan/max-collateral-redeem-amount"), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Add or reduce collateral on an existing flexible loan.
    /// A successful response means the request was accepted.
    /// </summary>
    /// <param name="type">Adjustment type</param>
    /// <param name="collateralCurrency">Collateral currency</param>
    /// <param name="collateralAmount">Collateral amount</param>
    /// <param name="orderId">Flexible loan order ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<bool?>> AdjustCollateralAsync(
        OkxFinancialFlexibleLoanAdjustType type,
        string collateralCurrency,
        decimal collateralAmount,
        string? orderId = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "collateralCcy", collateralCurrency },
            { "collateralAmt", collateralAmount.ToOkxString() },
        };
        parameters.AddEnum("type", type);
        parameters.AddOptional("ordId", orderId);

        var result = await ProcessListRequestAsync<object>(GetUri("api/v5/finance/flexible-loan/adjust-collateral"), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
        if (!result) return new RestCallResult<bool?>(result.Request, result.Response, result.Raw ?? "", result.Error);
        return new RestCallResult<bool?>(result.Request, result.Response, true, result.Raw ?? "", result.Error);
    }

    /// <summary>
    /// Get information for one flexible loan order, or all existing orders when <paramref name="orderId"/> is omitted.
    /// </summary>
    /// <param name="orderId">Flexible loan order ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxFinancialFlexibleLoanInfo>>> GetLoanInfoAsync(string? orderId = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("ordId", orderId);

        return ProcessListRequestAsync<OkxFinancialFlexibleLoanInfo>(GetUri("api/v5/finance/flexible-loan/loan-info"), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Get flexible loan history records.
    /// </summary>
    /// <param name="type">Action type filter</param>
    /// <param name="after">Return records earlier than this reference ID</param>
    /// <param name="before">Return records newer than this reference ID</param>
    /// <param name="limit">Number of results per request. The maximum is 100. The default is 100.</param>
    /// <param name="orderId">Flexible loan order ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxFinancialFlexibleLoanHistory>>> GetLoanHistoryAsync(
        OkxFinancialFlexibleLoanHistoryType? type = null,
        string? after = null,
        string? before = null,
        int limit = 100,
        string? orderId = null,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);

        var parameters = new ParameterCollection();
        parameters.AddOptionalEnum("type", type);
        parameters.AddOptional("after", after);
        parameters.AddOptional("before", before);
        parameters.AddOptional("limit", limit.ToOkxString());
        parameters.AddOptional("ordId", orderId);

        return ProcessListRequestAsync<OkxFinancialFlexibleLoanHistory>(GetUri("api/v5/finance/flexible-loan/loan-history"), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Get accrued flexible-loan interest records.
    /// </summary>
    /// <param name="currency">Loan currency</param>
    /// <param name="after">Return records earlier than this reference ID</param>
    /// <param name="before">Return records newer than this reference ID</param>
    /// <param name="limit">Number of results per request. The maximum is 100. The default is 100.</param>
    /// <param name="orderId">Flexible loan order ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxFinancialFlexibleLoanAccruedInterest>>> GetAccruedInterestAsync(
        string? currency = null,
        string? after = null,
        string? before = null,
        int limit = 100,
        string? orderId = null,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);

        var parameters = new ParameterCollection();
        parameters.AddOptional("ccy", currency);
        parameters.AddOptional("after", after);
        parameters.AddOptional("before", before);
        parameters.AddOptional("limit", limit.ToOkxString());
        parameters.AddOptional("ordId", orderId);

        return ProcessListRequestAsync<OkxFinancialFlexibleLoanAccruedInterest>(GetUri("api/v5/finance/flexible-loan/interest-accrued"), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    private static object ToSupplementaryCollateralPayload(OkxFinancialFlexibleLoanSupplementaryCollateral collateral)
    {
        return new
        {
            ccy = collateral.Currency,
            amt = collateral.Amount.ToOkxString()
        };
    }
}
