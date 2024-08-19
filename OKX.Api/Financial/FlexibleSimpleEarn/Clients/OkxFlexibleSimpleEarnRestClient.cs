using OKX.Api.Financial.FlexibleSimpleEarn.Models;

namespace OKX.Api.Financial.FlexibleSimpleEarn.Clients;

/// <summary>
/// OKX Rest Api Savings Client
/// </summary>
public class OkxFlexibleSimpleEarnRestClient(OkxRestApiClient root) : OkxBaseRestClient(root)
{
    // Endpoints
    private const string v5FinanceSavingsBalance = "api/v5/finance/savings/balance";
    private const string v5FinanceSavingsPurchaseRedempt = "api/v5/finance/savings/purchase-redempt";
    private const string v5FinanceSavingsSetLendingRate = "api/v5/finance/savings/set-lending-rate";
    private const string v5FinanceSavingsLendingHistory = "api/v5/finance/savings/lending-history";
    private const string v5FinanceSavingsLendingRateSummary = "api/v5/finance/savings/lending-rate-summary";
    private const string v5FinanceSavingsLendingRateHistory = "api/v5/finance/savings/lending-rate-history";

    public Task<RestCallResult<List<OkxFlexibleSimpleEarnSavingsBalance>>> GetBalancesAsync(string currency = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("ccy", currency);

        return ProcessListRequestAsync<OkxFlexibleSimpleEarnSavingsBalance>(GetUri(v5FinanceSavingsBalance), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    public Task<RestCallResult<OkxFlexibleSimpleEarnSavingsOrder>> PurchaseAsync(string currency, decimal amount, decimal rate, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            {"ccy", currency },
            {"side", "purchase" },
            {"amt", amount.ToOkxString() },
            {"rate", rate.ToOkxString() },
        };

        return ProcessOneRequestAsync<OkxFlexibleSimpleEarnSavingsOrder>(GetUri(v5FinanceSavingsPurchaseRedempt), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    public Task<RestCallResult<OkxFlexibleSimpleEarnSavingsOrder>> RedeemAsync(string currency, decimal amount, decimal rate, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            {"ccy", currency },
            {"side", "redempt" },
            {"amt", amount.ToOkxString() },
            {"rate", rate.ToOkxString() },
        };

        return ProcessOneRequestAsync<OkxFlexibleSimpleEarnSavingsOrder>(GetUri(v5FinanceSavingsPurchaseRedempt), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    public Task<RestCallResult<OkxFlexibleSimpleEarnSavingsRate>> SetLendingRateAsync(string currency, decimal rate, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            {"ccy", currency },
            {"rate", rate.ToOkxString() },
        };

        return ProcessOneRequestAsync<OkxFlexibleSimpleEarnSavingsRate>(GetUri(v5FinanceSavingsSetLendingRate), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    public Task<RestCallResult<List<OkxFlexibleSimpleEarnSavingsLendingHistory>>> GetLendingHistoryAsync(
        string currency = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("ccy", currency);
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxFlexibleSimpleEarnSavingsLendingHistory>(GetUri(v5FinanceSavingsLendingHistory), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }
    
    public Task<RestCallResult<OkxFlexibleSimpleEarnSavingsBorrowSummary>> GetPublicBorrowSummaryAsync(string currency = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("ccy", currency);

        return ProcessOneRequestAsync<OkxFlexibleSimpleEarnSavingsBorrowSummary>(GetUri(v5FinanceSavingsLendingRateSummary), HttpMethod.Get, ct, signed: false, queryParameters: parameters);
    }

    public Task<RestCallResult<List<OkxFlexibleSimpleEarnSavingsBorrowHistory>>> GetPublicBorrowHistoryAsync(
        string currency = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("ccy", currency);
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxFlexibleSimpleEarnSavingsBorrowHistory>(GetUri(v5FinanceSavingsLendingRateHistory), HttpMethod.Get, ct, signed: false, queryParameters: parameters);
    }
}