using OKX.Api.Financial.EthStaking.Converters;
using OKX.Api.Financial.EthStaking.Enums;
using OKX.Api.Financial.EthStaking.Models;

namespace OKX.Api.Financial.EthStaking.Clients;

/// <summary>
/// OKX Rest Api Staking Client
/// </summary>
public class OkxEthStakingRestClient(OkxRestApiClient root) : OkxBaseRestClient(root)
{
    // Endpoints
    private const string v5FinanceStakingDefiEthPurchase = "api/v5/finance/staking-defi/eth/purchase";
    private const string v5FinanceStakingDefiEthRedeem = "api/v5/finance/staking-defi/eth/redeem";
    private const string v5FinanceStakingDefiEthBalance = "api/v5/finance/staking-defi/eth/balance";
    private const string v5FinanceStakingDefiEthPurchaseRedeemHistory = "api/v5/finance/staking-defi/eth/purchase-redeem-history";
    private const string v5FinanceStakingDefiEthApyHistory = "api/v5/finance/staking-defi/eth/apy-history";

    public Task<RestCallResult<OkxFinancialEthStakingPurchase>> PurchaseAsync(decimal amount, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            {"amt", amount.ToOkxString() },
        };

        return ProcessOneRequestAsync<OkxFinancialEthStakingPurchase>(GetUri(v5FinanceStakingDefiEthPurchase), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    public Task<RestCallResult<OkxFinancialEthStakingRedeem>> RedeemAsync(decimal amount, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            {"amt", amount.ToOkxString() },
        };

        return ProcessOneRequestAsync<OkxFinancialEthStakingRedeem>(GetUri(v5FinanceStakingDefiEthRedeem), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    public Task<RestCallResult<List<OkxFinancialEthStakingBalance>>> GetBalancesAsync(CancellationToken ct = default)
    {
        return ProcessListRequestAsync<OkxFinancialEthStakingBalance>(GetUri(v5FinanceStakingDefiEthBalance), HttpMethod.Get, ct, signed: true);
    }

    public Task<RestCallResult<List<OkxFinancialEthStakingHistory>>> GetHistoryAsync(
        OkxFinancialEthStakingType type,
        OkxFinancialEthStakingStatus? status = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            {"type", JsonConvert.SerializeObject(type, new OkxFinancialEthStakingTypeConverter(false)) },
        };
        parameters.AddOptionalParameter("status", JsonConvert.SerializeObject(status, new OkxFinancialEthStakingStatusConverter(false)));
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxFinancialEthStakingHistory>(GetUri(v5FinanceStakingDefiEthPurchaseRedeemHistory), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    public Task<RestCallResult<List<OkxFinancialEthStakingApyHistory>>> GetApyHistoryAsync(int days, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            {"days", days.ToOkxString() },
        };

        return ProcessListRequestAsync<OkxFinancialEthStakingApyHistory>(GetUri(v5FinanceStakingDefiEthApyHistory), HttpMethod.Get, ct, signed: false, queryParameters: parameters);
    }
}