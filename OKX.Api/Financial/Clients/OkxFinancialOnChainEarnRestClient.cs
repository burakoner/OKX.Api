namespace OKX.Api.Financial;

/// <summary>
/// OKX Rest Api Earn Client
/// </summary>
public class OkxFinancialOnChainEarnRestClient(OkxRestApiClient root) : OkxBaseRestClient(root)
{
    // Endpoints
    private const string v5FinanceStakingDefiOffers = "api/v5/finance/staking-defi/offers";
    private const string v5FinanceStakingDefiPurchase = "api/v5/finance/staking-defi/purchase";
    private const string v5FinanceStakingDefiRedeem = "api/v5/finance/staking-defi/redeem";
    private const string v5FinanceStakingDefiCancel = "api/v5/finance/staking-defi/cancel";
    private const string v5FinanceStakingDefiOrdersActive = "api/v5/finance/staking-defi/orders-active";
    private const string v5FinanceStakingDefiOrdersHistory = "api/v5/finance/staking-defi/orders-history";

    /// <summary>
    /// Get Orders
    /// </summary>
    /// <param name="currency">Investment currency, e.g. BTC</param>
    /// <param name="productId">Product ID</param>
    /// <param name="protocolType">Protocol type. defi: on-chain earn</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxFinancialOnChainEarnOffer>>> GetOffersAsync(
        string? currency = null,
        string? productId = null,
        string? protocolType = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("ccy", currency);
        parameters.AddOptional("productId", productId);
        parameters.AddOptional("protocolType", protocolType);

        return ProcessListRequestAsync<OkxFinancialOnChainEarnOffer>(GetUri(v5FinanceStakingDefiOffers), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Purchase
    /// </summary>
    /// <param name="productId">Product ID</param>
    /// <param name="investData">Investment data</param>
    /// <param name="term">Investment term. Investment term must be specified for fixed-term product</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<long?>> PurchaseAsync(
        string productId,
        IEnumerable<OkxFinancialOnChainEarnInvestData> investData,
        string? term = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            {"productId", productId },
            {"investData", investData },
        };
        parameters.AddOptional("term", term);
        parameters.AddOptional("tag", OkxConstants.BrokerId);

        var result = await ProcessOneRequestAsync<OkxFinancialOrderIdContainer>(GetUri(v5FinanceStakingDefiPurchase), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
        if (!result) return new RestCallResult<long?>(result.Request, result.Response, result.Raw, result.Error);
        return new RestCallResult<long?>(result.Request, result.Response, result.Data.Payload, result.Raw, result.Error);
    }

    /// <summary>
    /// Redeem
    /// </summary>
    /// <param name="orderId">Order ID</param>
    /// <param name="protocolType">Protocol type. defi: on-chain earn</param>
    /// <param name="allowEarlyRedeem">Whether allows early redemption. Default is false</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<long?>> RedeemAsync(
        string orderId,
        string protocolType,
        bool allowEarlyRedeem = false,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            {"ordId", orderId },
            {"protocolType", protocolType },
            {"allowEarlyRedeem", allowEarlyRedeem },
        };
        parameters.AddOptional("tag", OkxConstants.BrokerId);

        var result = await ProcessOneRequestAsync<OkxFinancialOrderIdContainer>(GetUri(v5FinanceStakingDefiRedeem), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
        if (!result) return new RestCallResult<long?>(result.Request, result.Response, result.Raw, result.Error);
        return new RestCallResult<long?>(result.Request, result.Response, result.Data.Payload, result.Raw, result.Error);
    }

    /// <summary>
    /// Cancel purchases/redemptions
    /// </summary>
    /// <param name="orderId">Order ID</param>
    /// <param name="protocolType">Protocol type. defi: on-chain earn</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<long?>> CancelAsync(
        string orderId,
        string protocolType,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection {
            {"ordId", orderId },
            {"protocolType", protocolType },
        };

        var result = await ProcessOneRequestAsync<OkxFinancialOrderIdContainer>(GetUri(v5FinanceStakingDefiCancel), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
        if (!result) return new RestCallResult<long?>(result.Request, result.Response, result.Raw, result.Error);
        return new RestCallResult<long?>(result.Request, result.Response, result.Data.Payload, result.Raw, result.Error);
    }

    /// <summary>
    /// GET / Active orders
    /// </summary>
    /// <param name="currency">Investment currency, e.g. BTC</param>
    /// <param name="productId">Product ID</param>
    /// <param name="protocolType">Protocol type. defi: on-chain earn</param>
    /// <param name="state">Order state</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxFinancialOnChainEarnOrder>>> GetOpenOrdersAsync(
        string? currency = null,
        string? productId = null,
        string? protocolType = null,
        OkxFinancialOnChainEarnOrderState? state = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("ccy", currency);
        parameters.AddOptional("productId", productId);
        parameters.AddOptional("protocolType", protocolType);
        parameters.AddOptionalEnum("state", state);

        return ProcessListRequestAsync<OkxFinancialOnChainEarnOrder>(GetUri(v5FinanceStakingDefiOrdersActive), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// GET / Order history
    /// </summary>
    /// <param name="currency">Investment currency, e.g. BTC</param>
    /// <param name="productId">Product ID</param>
    /// <param name="protocolType">Protocol type. defi: on-chain earn</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ID. The value passed is the corresponding ordId</param>
    /// <param name="before">Pagination of data to return records newer than the requested ID. The value passed is the corresponding ordId</param>
    /// <param name="limit">Number of results per request. The default is 100. The maximum is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxFinancialOnChainEarnOrder>>> GetHistoryAsync(
        string? currency = null,
        string? productId = null,
        string? protocolType = null,
        long? after = null,
        long? before = null,
        int limit = 100,


        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("ccy", currency);
        parameters.AddOptional("productId", productId);
        parameters.AddOptional("protocolType", protocolType);
        parameters.AddOptional("after", after?.ToOkxString());
        parameters.AddOptional("before", before?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxFinancialOnChainEarnOrder>(GetUri(v5FinanceStakingDefiOrdersHistory), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }
}