namespace OKX.Api.Financial.FixedSimpleEarn;

/// <summary>
/// OKX Financial Fixed Simple Earn Rest Api Client
/// </summary>
public class OkxFinancialFixedSimpleEarnRestClient(OkxRestApiClient root) : OkxBaseRestClient(root)
{
    // Endpoints
    private const string v5FinanceFixedLoanLendingOffers = "api/v5/finance/fixed-loan/lending-offers";
    private const string v5FinanceFixedLoanLendingApyHistory = "api/v5/finance/fixed-loan/lending-apy-history";
    private const string v5FinanceFixedLoanPendingLendingVolume = "api/v5/finance/fixed-loan/pending-lending-volume";
    private const string v5FinanceFixedLoanLendingOrder = "api/v5/finance/fixed-loan/lending-order";
    private const string v5FinanceFixedLoanAmendLendingOrder = "api/v5/finance/fixed-loan/amend-lending-order";
    private const string v5FinanceFixedLoanLendingOrdersList = "api/v5/finance/fixed-loan/lending-orders-list";
    private const string v5FinanceFixedLoanLendingSubOrders = "api/v5/finance/fixed-loan/lending-sub-orders";

    /// <summary>
    /// Get lending-supported currencies and estimated APY.
    /// </summary>
    /// <param name="currency">Lending currency, e.g. BTC</param>
    /// <param name="term">Fixed term for lending order. 30D: 30 days</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxFinancialFixedSimpleEarnLendingOffer>>> GetLendingOffersAsync(string? currency = null, string? term = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("ccy", currency);
        parameters.AddOptionalParameter("term", term);

        return ProcessListRequestAsync<OkxFinancialFixedSimpleEarnLendingOffer>(GetUri(v5FinanceFixedLoanLendingOffers), HttpMethod.Get, ct, signed: false, queryParameters: parameters);
    }

    /// <summary>
    /// GET / Lending APY history (public)
    /// </summary>
    /// <param name="currency">Lending currency, e.g. BTC</param>
    /// <param name="term">Fixed term for lending order. 30D: 30 days</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxFinancialFixedSimpleEarnLendingApyHistory>>> GetLendingApyHistoryAsync(string? currency = null, string? term = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("ccy", currency);
        parameters.AddOptionalParameter("term", term);

        return ProcessListRequestAsync<OkxFinancialFixedSimpleEarnLendingApyHistory>(GetUri(v5FinanceFixedLoanLendingApyHistory), HttpMethod.Get, ct, signed: false, queryParameters: parameters);
    }

    /// <summary>
    /// GET / Lending volume (public)
    /// </summary>
    /// <param name="currency">Lending currency, e.g. BTC</param>
    /// <param name="term">Fixed term for lending order. 30D: 30 days</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxFinancialFixedSimpleEarnLendingVolume>> GetPendingLendingVolumeAsync(string? currency = null, string? term = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("ccy", currency);
        parameters.AddOptionalParameter("term", term);

        return ProcessOneRequestAsync<OkxFinancialFixedSimpleEarnLendingVolume>(GetUri(v5FinanceFixedLoanPendingLendingVolume), HttpMethod.Get, ct, signed: false, queryParameters: parameters);
    }

    /// <summary>
    /// Place lending order
    /// </summary>
    /// <param name="currency">Lending currency, e.g. BTC</param>
    /// <param name="amount">Lending amount</param>
    /// <param name="rate">Lending APR, in decimal. e.g. 0.01 represents 1%.</param>
    /// <param name="term">Fixed term for lending order. 30D: 30 days</param>
    /// <param name="autoRenewal">Whether or not auto-renewal when the term is due</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxFinancialFixedSimpleEarnLendingOrderId>> PlaceOrderAsync(
        string currency,
        decimal amount,
        decimal rate,
        string term,
        bool autoRenewal = false,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            {"ccy", currency },
            {"amt", amount.ToOkxString() },
            {"rate", rate.ToOkxString() },
            {"term", term },
            {"autoRenewal", autoRenewal },
        };

        return ProcessOneRequestAsync<OkxFinancialFixedSimpleEarnLendingOrderId>(GetUri(v5FinanceFixedLoanLendingOrder), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Amend lending order
    /// </summary>
    /// <param name="orderId">Order ID</param>
    /// <param name="changeAmount">Redemption Amount, e.g. -0.1 represents redemption amount is 0.1.</param>
    /// <param name="rate">Lending APR, in decimal. e.g. 0.01 represents 1%.</param>
    /// <param name="autoRenewal">Whether or not auto-renewal when the term is due</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxFinancialFixedSimpleEarnLendingOrderId>> AmendOrderAsync(
        long orderId,
        decimal? changeAmount = null,
        decimal? rate = null,
        bool autoRenewal = false,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            {"ordId", orderId.ToOkxString() },
            {"autoRenewal", autoRenewal },
        };
        parameters.AddOptionalParameter("changeAmt", changeAmount?.ToOkxString());
        parameters.AddOptionalParameter("rate", rate?.ToOkxString());

        return ProcessOneRequestAsync<OkxFinancialFixedSimpleEarnLendingOrderId>(GetUri(v5FinanceFixedLoanAmendLendingOrder), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// GET / Lending order list
    /// </summary>
    /// <param name="orderId">Order ID</param>
    /// <param name="currency">Lending currency, e.g. BTC</param>
    /// <param name="state">State</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ordId</param>
    /// <param name="before">Pagination of data to return records newer than the requested ordId</param>
    /// <param name="limit">Number of results per request. The maximum is 100. The default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxFinancialFixedSimpleEarnLendingOrder>>> GetOrdersAsync(
        long? orderId = null,
        string? currency = null,
        OkxFinancialFixedSimpleEarnLendingOrderState? state = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("ordId", orderId?.ToOkxString());
        parameters.AddOptionalParameter("ccy", currency);
        parameters.AddOptionalParameter("state", JsonConvert.SerializeObject(state, new OkxFinancialFixedSimpleEarnLendingOrderStateConverter(false)));
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxFinancialFixedSimpleEarnLendingOrder>(GetUri(v5FinanceFixedLoanLendingOrdersList), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// GET / Lending sub order list
    /// </summary>
    /// <param name="orderId">Order ID</param>
    /// <param name="state">State</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ordId</param>
    /// <param name="before">Pagination of data to return records newer than the requested ordId</param>
    /// <param name="limit">Number of results per request. The maximum is 100. The default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public Task<RestCallResult<List<OkxFinancialFixedSimpleEarnLendingSubOrder>>> GetSubOrdersAsync(
        long? orderId = null,
        OkxFinancialFixedSimpleEarnLendingOrderState? state = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        if (state == OkxFinancialFixedSimpleEarnLendingOrderState.Pending)
            throw new ArgumentException("State cannot be Pending for sub-orders", nameof(state));

        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("ordId", orderId?.ToOkxString());
        parameters.AddOptionalParameter("state", JsonConvert.SerializeObject(state, new OkxFinancialFixedSimpleEarnLendingOrderStateConverter(false)));
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxFinancialFixedSimpleEarnLendingSubOrder>(GetUri(v5FinanceFixedLoanLendingSubOrders), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }
}