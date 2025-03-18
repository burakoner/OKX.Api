namespace OKX.Api.Financial;

/// <summary>
/// OKX Rest API SOL Staking Client
/// </summary>
public class OkxFinancialSolStakingRestClient(OkxRestApiClient root) : OkxBaseRestClient(root)
{
    // Endpoints
    private const string v5FinanceStakingDefiSolPurchase = "api/v5/finance/staking-defi/sol/purchase";
    private const string v5FinanceStakingDefiSolRedeem = "api/v5/finance/staking-defi/sol/redeem";
    private const string v5FinanceStakingDefiSolBalance = "api/v5/finance/staking-defi/sol/balance";
    private const string v5FinanceStakingDefiSolPurchaseRedeemHistory = "api/v5/finance/staking-defi/sol/purchase-redeem-history";
    private const string v5FinanceStakingDefiSolApyHistory = "api/v5/finance/staking-defi/sol/apy-history";

    /// <summary>
    /// Staking SOL for OKSOL
    /// Only the assets in the funding account can be used.
    /// </summary>
    /// <param name="amount">Investment amount</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<bool?>> PurchaseAsync(decimal amount, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            {"amt", amount.ToOkxString() },
        };

        var result = await ProcessOneRequestAsync<OkxFinancialStakingPurchase>(GetUri(v5FinanceStakingDefiSolPurchase), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
        if (!result) return new RestCallResult<bool?>(result.Request, result.Response, result.Raw, result.Error);
        return new RestCallResult<bool?>(result.Request, result.Response, result.Data is not null, result.Raw, result.Error);
    }

    /// <summary>
    /// Only the assets in the funding account can be used. If your OKSOL is in your trading account, you can make funding transfer first.
    /// </summary>
    /// <param name="amount">Redeeming amount</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<bool?>> RedeemAsync(decimal amount, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            {"amt", amount.ToOkxString() },
        };

        var result = await ProcessOneRequestAsync<OkxFinancialStakingRedeem>(GetUri(v5FinanceStakingDefiSolRedeem), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
        if (!result) return new RestCallResult<bool?>(result.Request, result.Response, result.Raw, result.Error);
        return new RestCallResult<bool?>(result.Request, result.Response, result.Data is not null, result.Raw, result.Error);
    }

    /// <summary>
    /// The balance is summarized all OKSOL assets (including assets in redeeming) in account.
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxFinancialStakingBalance>>> GetBalancesAsync(CancellationToken ct = default)
    {
        return ProcessListRequestAsync<OkxFinancialStakingBalance>(GetUri(v5FinanceStakingDefiSolBalance), HttpMethod.Get, ct, signed: true);
    }

    /// <summary>
    /// GET / Purchase&amp;Redeem history
    /// </summary>
    /// <param name="type">Type</param>
    /// <param name="status">Status</param>
    /// <param name="after">Pagination of data to return records earlier than the requestTime. The value passed is the corresponding timestamp</param>
    /// <param name="before">Pagination of data to return records newer than the requestTime. The value passed is the corresponding timestamp</param>
    /// <param name="limit">Number of results per request. The default is 100. The maximum is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxFinancialStakingHistory>>> GetHistoryAsync(
        OkxFinancialStakingType? type = null,
        OkxFinancialStakingStatus? status = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptionalEnum("type", type);
        parameters.AddOptionalEnum("status", status);
        parameters.AddOptional("after", after?.ToOkxString());
        parameters.AddOptional("before", before?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxFinancialStakingHistory>(GetUri(v5FinanceStakingDefiSolPurchaseRedeemHistory), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// GET / APY history (Public)
    /// </summary>
    /// <param name="days">Get the days of APY(Annual percentage yield) history record in the past. No more than 365 days</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxFinancialStakingApyHistory>>> GetApyHistoryAsync(int days, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            {"days", days.ToOkxString() },
        };

        return ProcessListRequestAsync<OkxFinancialStakingApyHistory>(GetUri(v5FinanceStakingDefiSolApyHistory), HttpMethod.Get, ct, signed: false, queryParameters: parameters);
    }
}