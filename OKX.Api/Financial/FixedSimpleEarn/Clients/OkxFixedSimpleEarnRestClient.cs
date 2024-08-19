using OKX.Api.Financial.FixedSimpleEarn.Converters;
using OKX.Api.Financial.FixedSimpleEarn.Enums;
using OKX.Api.Financial.FixedSimpleEarn.Models;

namespace OKX.Api.Financial.FixedSimpleEarn.Clients;

/// <summary>
/// OKX Financial Fixed Simple Earn Rest Api Client
/// </summary>
public class OkxFixedSimpleEarnRestClient(OkxRestApiClient root) : OkxBaseRestClient(root)
{
    // Endpoints
    private const string v5FinanceFixedLoanLendingOffers = "api/v5/finance/fixed-loan/lending-offers";
    private const string v5FinanceFixedLoanLendingApyHistory = "api/v5/finance/fixed-loan/lending-apy-history";
    private const string v5FinanceFixedLoanPendingLendingVolume = "api/v5/finance/fixed-loan/pending-lending-volume";
    private const string v5FinanceFixedLoanLendingOrder = "api/v5/finance/fixed-loan/lending-order";
    private const string v5FinanceFixedLoanAmendLendingOrder = "api/v5/finance/fixed-loan/amend-lending-order";
    private const string v5FinanceFixedLoanLendingOrdersList = "api/v5/finance/fixed-loan/lending-orders-list";
    private const string v5FinanceFixedLoanLendingSubOrders = "api/v5/finance/fixed-loan/lending-sub-orders";

    public Task<RestCallResult<List<OkxFinancialFixedSimpleEarnLendingOffer>>> GetLendingOffersAsync(string currency = null, string term = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("ccy", currency);
        parameters.AddOptionalParameter("term", term);

        return ProcessListRequestAsync<OkxFinancialFixedSimpleEarnLendingOffer>(GetUri(v5FinanceFixedLoanLendingOffers), HttpMethod.Get, ct, signed: false, queryParameters: parameters);
    }

    public Task<RestCallResult<List<OkxFinancialFixedSimpleEarnLendingApyHistory>>> GetLendingApyHistoryAsync(string currency = null, string term = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("ccy", currency);
        parameters.AddOptionalParameter("term", term);

        return ProcessListRequestAsync<OkxFinancialFixedSimpleEarnLendingApyHistory>(GetUri(v5FinanceFixedLoanLendingApyHistory), HttpMethod.Get, ct, signed: false, queryParameters: parameters);
    }

    public Task<RestCallResult<OkxFinancialFixedSimpleEarnLendingVolume>> GetPendingLendingVolumeAsync(string currency = null, string term = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("ccy", currency);
        parameters.AddOptionalParameter("term", term);

        return ProcessOneRequestAsync<OkxFinancialFixedSimpleEarnLendingVolume>(GetUri(v5FinanceFixedLoanPendingLendingVolume), HttpMethod.Get, ct, signed: false, queryParameters: parameters);
    }

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

    public Task<RestCallResult<List<OkxFinancialFixedSimpleEarnLendingOrder>>> GetOrdersAsync(
        long? orderId = null,
        string currency = null,
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