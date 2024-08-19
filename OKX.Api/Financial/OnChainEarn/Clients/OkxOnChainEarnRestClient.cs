using OKX.Api.Financial.OnChainEarn.Converters;
using OKX.Api.Financial.OnChainEarn.Enums;
using OKX.Api.Financial.OnChainEarn.Models;

namespace OKX.Api.Financial.OnChainEarn.Clients;

/// <summary>
/// OKX Rest Api Earn Client
/// </summary>
public class OkxOnChainEarnRestClient(OkxRestApiClient root) : OkxBaseRestClient(root)
{
    // Endpoints
    private const string v5FinanceStakingDefiOffers = "api/v5/finance/staking-defi/offers";
    private const string v5FinanceStakingDefiPurchase = "api/v5/finance/staking-defi/purchase";
    private const string v5FinanceStakingDefiRedeem = "api/v5/finance/staking-defi/redeem";
    private const string v5FinanceStakingDefiCancel = "api/v5/finance/staking-defi/cancel";
    private const string v5FinanceStakingDefiOrdersActive = "api/v5/finance/staking-defi/orders-active";
    private const string v5FinanceStakingDefiOrdersHistory = "api/v5/finance/staking-defi/orders-history";

    public Task<RestCallResult<List<OkxFinancialOnChainEarnOffer>>> GetOffersAsync(
        string currency = null,
        string productId = null,
        string protocolType = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("ccy", currency);
        parameters.AddOptionalParameter("productId", productId);
        parameters.AddOptionalParameter("protocolType", protocolType);

        return ProcessListRequestAsync<OkxFinancialOnChainEarnOffer>(GetUri(v5FinanceStakingDefiOffers), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    public Task<RestCallResult<OkxFinancialOnChainEarnPurchase>> PurchaseAsync(
        string productId,
        IEnumerable<OkxFinancialOnChainEarnInvestData> investData,
        string term = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            {"productId", productId },
            {"investData", investData },
        };
        parameters.AddOptionalParameter("term", term);
        parameters.AddOptionalParameter("tag", Options.BrokerId);

        return ProcessOneRequestAsync<OkxFinancialOnChainEarnPurchase>(GetUri(v5FinanceStakingDefiPurchase), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    public Task<RestCallResult<OkxFinancialOnChainEarnRedeem>> RedeemAsync(
        string orderId,
        string protocolType,
        bool allowEarlyRedeem = false,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            {"ordId", orderId },
            {"protocolType", protocolType },
            {"allowEarlyRedeem", allowEarlyRedeem },
        };

        return ProcessOneRequestAsync<OkxFinancialOnChainEarnRedeem>(GetUri(v5FinanceStakingDefiRedeem), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    public Task<RestCallResult<OkxFinancialOnChainEarnCancel>> CancelAsync(
        string orderId,
        string protocolType,
        bool allowEarlyRedeem = false,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            {"ordId", orderId },
            {"protocolType", protocolType },
        };

        return ProcessOneRequestAsync<OkxFinancialOnChainEarnCancel>(GetUri(v5FinanceStakingDefiCancel), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    public Task<RestCallResult<List<OkxFinancialOnChainEarnOrder>>> GetOpenOrdersAsync(
        string currency = null,
        string productId = null,
        string protocolType = null,
        OkxFinancialOnChainEarnOrderState? state = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("ccy", currency);
        parameters.AddOptionalParameter("productId", productId);
        parameters.AddOptionalParameter("protocolType", protocolType);
        parameters.AddOptionalParameter("state", JsonConvert.SerializeObject(state, new OkxFinancialOnChainEarnOrderStateConverter(false)));

        return ProcessListRequestAsync<OkxFinancialOnChainEarnOrder>(GetUri(v5FinanceStakingDefiOrdersActive), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    public Task<RestCallResult<List<OkxFinancialOnChainEarnOrder>>> GetHistoryAsync(
        string currency = null,
        string productId = null,
        string protocolType = null,
        long? after = null,
        long? before = null,
        int limit = 100,


        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("ccy", currency);
        parameters.AddOptionalParameter("productId", productId);
        parameters.AddOptionalParameter("protocolType", protocolType);
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxFinancialOnChainEarnOrder>(GetUri(v5FinanceStakingDefiOrdersHistory), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }
}